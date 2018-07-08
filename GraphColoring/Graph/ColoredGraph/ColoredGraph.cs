using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphColoring.Graph
{
    abstract partial class Graph : IGraphInterface
    {
        private class ColoredGgraph : IColoredGraphInterface
        {
            // Variable
            #region
            /// <summary>
            /// graph - daný graf
            /// countUsedColor - dosavadní počet použitých barev pro obarvení grafu
            /// coloredVertexList - list všech obarvených vrcholů grafu
            /// unColoredVertexList - list všech NEobarvených vrcholů grafu
            /// </summary>
            private Graph graph;
            private Dictionary<int, HashSet<Vertex>> usedColorsDictionary;
            private HashSet<Vertex> coloredVertexHashSet;
            private HashSet<Vertex> unColoredVertexHashSet;
            #endregion

            // Constructor
            #region
            public ColoredGgraph(Graph graph)
            {
                this.graph = graph;
                coloredVertexHashSet = new HashSet<Vertex>();
                unColoredVertexHashSet = new HashSet<Vertex>(graph.AllVertices());
                usedColorsDictionary = new Dictionary<int, HashSet<Vertex>>()
                {
                    { 0, new HashSet<Vertex>(unColoredVertexHashSet) }
                };
            }
            #endregion

            // Method 
            #region
            /// <summary>
            /// Zkontroluje, zda daný vrchol je dobře obarven. Tj. je pro něho splněna podmínka obarvení.
            /// </summary>
            /// <param name="vertex">daný vrchol</param>
            /// <returns>true, pokud vrchol je korektně obarvený, jinak false</returns>
            public bool CheckValidColor(Vertex vertex)
            {
                // Variable
                List<Vertex> neighboursList;
                int colorVertex = vertex.GetColor();

                if (colorVertex == 0)
                    return true;

                neighboursList = graph.Neighbours(vertex);

                foreach(Vertex neighbour in neighboursList)
                {
                    if (neighbour.GetColor() == colorVertex)
                        return false;
                }

                return true;
            }

            /// <summary>
            /// Zkontroluje, zda všechny vrcholy jsou obarveny správně
            /// </summary>
            /// <returns>seznam vrcholů, které jsou obarveny ŠPATNĚ</returns>
            public List<Vertex> CheckValidColor()
            {
                // Variable
                List<Vertex> allVerticesList = graph.AllVertices();
                List<Vertex> invalidColoredVerticesList = new List<Vertex>();

                foreach(Vertex vertex in allVerticesList)
                {
                    if (!CheckValidColor(vertex))
                        invalidColoredVerticesList.Add(vertex);
                }

                return invalidColoredVerticesList;
            }

            /// <summary>
            /// Obarví daný vrchol danou barvou
            /// </summary>
            /// <param name="vertex">daný vrchol</param>
            /// <param name="color">daná barva</param>
            public void ColorVertex(Vertex vertex, int color)
            {
                VertexExtended vertexExtended = graph.ConvertVertexToVertexExtended(vertex);
                
                vertexExtended.SetColor(color);
                ChangeVertexInHashSets(vertex, color);
            }

            /// <summary>
            /// Vrátí obarvení daného vrcholu
            /// Pokud vrchol není obarven, tak vrátí 0
            /// Pokud daný vrchol neexistuje v grafu, tak vrátí výjimku GraphVertexDoesntExistException
            /// </summary>
            /// <param name="vertex">daný vrchol</param>
            /// <returns>obarvení vrcholu</returns>
            public int GetColorVertex(Vertex vertex)
            {
                VertexExtended vertexExtended = graph.ConvertVertexToVertexExtended(vertex);

                return vertexExtended.GetColor();
            }

            /// <summary>
            /// Hladově obarví vrcholy podle dané posloupnosti
            /// </summary>
            /// <param name="vertexList">daná posloupnost</param>
            public void GreedyColoring(List<Vertex> vertexList)
            {
                // Variable
                VertexExtended vertexExtended;

                foreach(Vertex vertex in vertexList)
                {
                    vertexExtended = graph.ConvertVertexToVertexExtended(vertex);
                    vertexExtended.SetColor(GreedyColoring(vertex));
                }
            }

            /// <summary>
            /// Hladově obarví vrchol
            /// </summary>
            /// <param name="vertex">daný vrchol</param>
            /// <returns></returns>
            public int GreedyColoring(Vertex vertex)
            {
                // Variable
                int neighbourColor;
                List<Vertex> neighboursList;
                List<int> neighboursColorList;

                neighboursList = graph.Neighbours(vertex);
                neighboursColorList = new List<int>() { 0 };

                // Get neighbours colors
                foreach(Vertex neighbour in neighboursList)
                {
                    neighbourColor = neighbour.GetColor();

                    if (neighbourColor != 0)
                        neighboursColorList.Add(neighbourColor);
                }

                neighboursColorList.Sort();

                int color = Enumerable.Range(neighboursColorList.Min(), neighboursColorList.Count + 1).Except(neighboursColorList).First();

                return color;
            }

            /// <summary>
            /// Vrátí true, pokud daný vrchol má nastavenou barvu, jinak vrátí false
            /// Pokud daný vrchol neexistuje v grafu, tak vrátí výjimku GraphVertexDoesntExistException
            /// </summary>
            /// <param name="vertex">daný vrchol</param>
            /// <returns>true, pokud je vrchol již obarvený, jinak false</returns>
            public bool IsVertexColored(Vertex vertex)
            {
                VertexExtended vertexExtended = graph.ConvertVertexToVertexExtended(vertex);

                if (vertexExtended.GetColor() == 0)
                    return false;

                return true;
            }

            /// <summary>
            /// Resetne obarvení pro daný vrchol (barvu nastaví na 0)
            /// Pokud daný vrchol neexistuje v grafu, tak vrátí výjimku GraphVertexDoesntExistException
            /// </summary>
            /// <param name="vertex">daný vrchol</param>
            public void ResetColorVertex(Vertex vertex)
            {
                VertexExtended vertexExtended = graph.ConvertVertexToVertexExtended(vertex);

                if (!IsVertexColored(vertex))
                    return;
                
                vertexExtended.ResetColor();
                ChangeVertexInHashSets(vertex, 0);
            }

            /// <summary>
            /// Přemístí vertex v usedColorsDictionary
            /// Přemístí případně vertex v coloredVertexHashSet a unColoredVertexHashSet
            /// </summary>
            /// <param name="vertex">daný vrchol</param>
            /// <param name="color">dané obarvení</param>
            private void ChangeVertexInHashSets(Vertex vertex, int color)
            {
                // Variable
                VertexExtended vertexExtended;
                int vertexColor;

                vertexExtended = graph.ConvertVertexToVertexExtended(vertex);
                vertexColor = vertex.GetColor();

                if (vertexColor == color)
                    return;

                // coloredVertexHashSet, unColoredVertexHashSet
                if (color == 0)
                {
                    coloredVertexHashSet.Remove(vertex);
                    unColoredVertexHashSet.Add(vertex);
                }
                else
                {
                    if (vertexColor == 0)
                    {
                        unColoredVertexHashSet.Remove(vertex);
                        coloredVertexHashSet.Add(vertex);
                    }
                }

                // usedColorsDictionary
                if (usedColorsDictionary[vertexColor].Count == 1)
                    usedColorsDictionary.Remove(vertexColor);
                else
                    usedColorsDictionary[vertexColor].Remove(vertex);

                if (usedColorsDictionary.ContainsKey(color))
                    usedColorsDictionary[color].Add(vertex);
                else
                    usedColorsDictionary.Add(color, new HashSet<Vertex>() { vertex });
            }

            /// <summary>
            /// Vrátí true pokud je graf korektně obarvený. Tj. každý vrchol je obarven nějakou barvou a jsou splněny podmínky pro obarvení.
            /// </summary>
            /// <returns>true pokud je graf validně obarven, jinak false</returns>
            public bool IsValidColored()
            {
                if (unColoredVertexHashSet.Count != 0)
                    return false;

                if (CheckValidColor().Count != 0)
                    return false;

                return true;
            }
            #endregion

            // Property
            #region
            /// <summary>
            /// Vrátí dosavadní počet použítých barev
            /// </summary>
            /// <returns>počet barev</returns>
            public int GetCountUsedColors()
            {
                return usedColorsDictionary.Count() - 1;
            }

            /// <summary>
            /// Vrátí seznam vrcholů, které jsou obarveny
            /// </summary>
            /// <returns></returns>
            public List<Vertex> GetColoredVertexList()
            {
                return coloredVertexHashSet.ToList();
            }

            /// <summary>
            /// Vrátí seznam vrcholů, které NEJSOU obarveny
            /// </summary>
            /// <returns></returns>
            public List<Vertex> GetUnColoredVertexList()
            {
                return unColoredVertexHashSet.ToList();
            }
            #endregion
        }
    }
}
