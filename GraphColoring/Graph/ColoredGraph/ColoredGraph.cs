using System;
using System.Text;
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
            /// isInicializedColoredGraph - informace, zda je graf validně obarven
            /// graph - daný graf
            /// countUsedColor - dosavadní počet použitých barev pro obarvení grafu
            /// coloredVertexList - list všech obarvených vrcholů grafu
            /// unColoredVertexList - list všech NEobarvených vrcholů grafu
            /// isSaturation - informace, zda se pracuje s nasyceností grafu
            /// saturationDegreeSequence - dictionary obsahující vrchol a jeho nasycenost
            /// </summary>
            private bool isInicializedColoredGraph;
            private Graph graph;
            private Dictionary<int, HashSet<Vertex>> usedColorsDictionary;
            private HashSet<Vertex> coloredVertexHashSet;
            private HashSet<Vertex> unColoredVertexHashSet;
            private bool isSaturation;
            private Dictionary<Vertex, int> saturationDegreeSequence;
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
                    { VertexExtended.GetDefaultColor(), new HashSet<Vertex>(unColoredVertexHashSet) }
                };
            }
            #endregion

            // Method 
            #region
            /// <summary>
            /// Zkontroluje, zda daný vrchol je dobře obarven. Tj. je pro něho splněna podmínka obarvení.
            /// Pokud daný vrchol neexistuje v grafu, tak vyvolá vyjímu GraphVertexDoesntExistException
            /// </summary>
            /// <param name="vertex">daný vrchol</param>
            /// <returns>true, pokud vrchol je korektně obarvený, jinak false</returns>
            public bool CheckValidColor(Vertex vertex)
            {
                // Variable
                List<Vertex> neighboursList;
                int colorVertex = vertex.GetColor();

                if (!graph.ExistsVertex(vertex))
                    throw new MyException.GraphVertexDoesntExistException();

                if (colorVertex == VertexExtended.GetDefaultColor())
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
            /// Pokud je graf inicializovaný, vrátí vyjímku ColoredGraphAlreadyInitializedException
            /// </summary>
            /// <param name="vertex">daný vrchol</param>
            /// <param name="color">daná barva</param>
            public void ColorVertex(Vertex vertex, int color)
            {
                if (isInicializedColoredGraph)
                    throw new MyException.ColoredGraphAlreadyInitializedException();

                VertexExtended vertexExtended = graph.ConvertVertexToVertexExtended(vertex);
                
                ChangeVertexInHashSets(vertex, color);
                vertexExtended.SetColor(color);

                // Saturation
                if (isSaturation)
                {
                    List<Vertex> saturationVertex = graph.Neighbours(vertex);
                    saturationVertex.Add(vertex);

                    foreach (Vertex vertexSaturation in saturationVertex)
                    {
                        EvaluateSaturation(vertexSaturation);
                    }
                }
            }

            /// <summary>
            /// Vrátí obarvení daného vrcholu
            /// Pokud vrchol není obarven, tak vrátí VertexExtended.GetDefaultColor()
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
            /// Pokud je graf inicializovaný, vrátí vyjímku ColoredGraphAlreadyInitializedException
            /// </summary>
            /// <param name="vertexList">daná posloupnost</param>
            public void GreedyColoring(List<Vertex> vertexList)
            {
                // Variable
                VertexExtended vertexExtended;

                if (isInicializedColoredGraph)
                    throw new MyException.ColoredGraphAlreadyInitializedException();

                ResetColors();

                foreach (Vertex vertex in vertexList)
                {
                    vertexExtended = graph.ConvertVertexToVertexExtended(vertex);
                    ColorVertex(vertex, GreedyColoring(vertex));
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
                neighboursColorList = new List<int>() { VertexExtended.GetDefaultColor() };

                // Get neighbours colors
                foreach(Vertex neighbour in neighboursList)
                {
                    neighbourColor = neighbour.GetColor();

                    if (neighbourColor != VertexExtended.GetDefaultColor())
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

                if (vertexExtended.GetColor() == VertexExtended.GetDefaultColor())
                    return false;

                return true;
            }

            /// <summary>
            /// Resetne obarvení pro daný vrchol (barvu nastaví na VertexExtended.GetDefaultColor())
            /// Pokud daný vrchol neexistuje v grafu, tak vrátí výjimku GraphVertexDoesntExistException
            /// Pokud je graf inicializovaný, vrátí vyjímku ColoredGraphAlreadyInitializedException
            /// </summary>
            /// <param name="vertex">daný vrchol</param>
            public void ResetColorVertex(Vertex vertex)
            {
                if (isInicializedColoredGraph)
                    throw new MyException.ColoredGraphAlreadyInitializedException();

                VertexExtended vertexExtended = graph.ConvertVertexToVertexExtended(vertex);

                if (!IsVertexColored(vertex))
                    return;
                
                ChangeVertexInHashSets(vertex, VertexExtended.GetDefaultColor());
                vertexExtended.ResetColor();
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
                if (color == VertexExtended.GetDefaultColor())
                {
                    coloredVertexHashSet.Remove(vertex);
                    unColoredVertexHashSet.Add(vertex);
                }
                else
                {
                    if (vertexColor == VertexExtended.GetDefaultColor())
                    {
                        unColoredVertexHashSet.Remove(vertex);
                        coloredVertexHashSet.Add(vertex);
                    }
                }

                // usedColorsDictionary
                if (vertexColor != VertexExtended.GetDefaultColor() && usedColorsDictionary[vertexColor].Count == 1)
                    usedColorsDictionary.Remove(vertexColor);
                else
                    usedColorsDictionary[vertexColor].Remove(vertex);

                if (usedColorsDictionary.ContainsKey(color))
                    usedColorsDictionary[color].Add(vertex);
                else
                    usedColorsDictionary.Add(color, new HashSet<Vertex>() { vertex });
            }

            /// <summary>
            /// Přidá vertex do usedColorsDictionary
            /// Přidá vertex buď do coloredVertexHashSet, nebo do unColoredVertexHashSet
            /// </summary>
            /// <param name="vertex">daný vrchol</param>
            public void AddVertexInHashSets(Vertex vertex)
            {
                // Variable
                int vertexColor = vertex.GetColor();

                // coloredVertexHashSet, unColoredVertexHashSet
                if (vertexColor == VertexExtended.GetDefaultColor())
                    unColoredVertexHashSet.Add(vertex);
                else
                    coloredVertexHashSet.Add(vertex);

                // usedColorsDictionary
                if (usedColorsDictionary.ContainsKey(vertexColor))
                    usedColorsDictionary[vertexColor].Add(vertex);
                else
                    usedColorsDictionary.Add(vertexColor, new HashSet<Vertex>() { vertex });
            }

            /// <summary>
            /// Odebere vertex z usedColorsDictionary
            /// Odebere vertex z coloredVertexHashSet, nebo z unColoredVertexHashSet
            /// </summary>
            /// <param name="vertex">daný vrchol</param>
            public void RemoveVertexInHashSets(Vertex vertex)
            {
                // Variable
                int vertexColor = vertex.GetColor();

                // coloredVertexHashSet, unColoredVertexHashSet
                if (vertexColor == VertexExtended.GetDefaultColor())
                    unColoredVertexHashSet.Remove(vertex);
                else
                    coloredVertexHashSet.Remove(vertex);

                // usedColorsDictionary
                if (usedColorsDictionary[vertexColor].Count == 1)
                    usedColorsDictionary.Remove(vertexColor);
                else
                    usedColorsDictionary[vertexColor].Remove(vertex);
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

            /// <summary>
            /// Inicializuje obarvený graf. Pokud už graf byl inicializovaný, tak vrátí vyjímku ColoredGraphAlreadyInitializedException
            /// </summary>
            /// <returns>true pokud byl graf inicializován, jinak false/returns>
            public bool InicializeColoredGraph()
            {
                if (isInicializedColoredGraph)
                    throw new MyException.ColoredGraphAlreadyInitializedException();

                if (IsValidColored())
                    isInicializedColoredGraph = true;
                else
                    isInicializedColoredGraph = false;

                return isInicializedColoredGraph;
            }

            /// <summary>
            /// Deinicializuje obarvený graf.
            /// Pokud graf NEbyl inicializovaný, tak vrátí vyjímku ColoredGraphNotInitializationException
            /// </summary>
            public void DeinicializationColoredGraph()
            {
                if (!isInicializedColoredGraph)
                    throw new MyException.ColoredGraphNotInitializationException();
                
                isInicializedColoredGraph = false;
            }

            /// <summary>
            /// Resetuje všechna obarvení vrcholů
            /// Pokud je graf inicializovaný, vrátí vyjímku ColoredGraphAlreadyInitializedException
            /// </summary>
            public void ResetColors()
            {
                // Variable
                List<Vertex> vertexList;
                VertexExtended vertexExtended;

                if (isInicializedColoredGraph)
                    throw new MyException.ColoredGraphAlreadyInitializedException();

                vertexList = graph.AllVertices();

                foreach (Vertex vertex in vertexList)
                {
                    vertexExtended = graph.ConvertVertexToVertexExtended(vertex);
                    ChangeVertexInHashSets(vertex, VertexExtended.GetDefaultColor());
                    vertexExtended.ResetColor();
                }

                // Saturation
                if (isSaturation)
                    SetSaturation(true);
            }

            /// <summary>
            /// Spočítá nasycenost pro daný vrchol
            /// </summary>
            /// <param name="vertex">daný vrchol</param>
            private void EvaluateSaturation(Vertex vertex)
            {
                // Variable
                HashSet<int> neighboursColors = new HashSet<int>();
                List<Vertex> neighboursVertex = graph.Neighbours(vertex);

                if (vertex.GetColor() != VertexExtended.GetDefaultColor())
                {
                    saturationDegreeSequence.Remove(vertex);
                    return;
                }
                else
                    neighboursVertex.Add(vertex);
                

                foreach(Vertex neighbourVertex in neighboursVertex)
                {
                    int color = neighbourVertex.GetColor();

                    if (color == VertexExtended.GetDefaultColor())
                        continue;

                    neighboursColors.Add(color);
                }

                saturationDegreeSequence[vertex] = neighboursColors.Count;
            }

            /// <summary>
            /// Nastaví saturation
            /// </summary>
            /// <param name="saturation">true - pracuje se s nasyceností vrcholů, false - nepracuje se s nasyceností vrcholů</param>
            public void SetSaturation(bool saturation)
            {
                isSaturation = saturation;

                if (isSaturation)
                {
                    saturationDegreeSequence = new Dictionary<Vertex, int>();

                    foreach (Vertex vertex in graph.AllVertices())
                    {
                        saturationDegreeSequence.Add(vertex, 0);
                    }
                }
            }
               
            /// <summary>
            /// Vrátí vrchol, který je nejvíce nasycený
            /// </summary>
            /// <returns>vrchol s největší nasyceností</returns>
            public Vertex GetSaturationDegreeSequence()
            {
                // Variable
                int max = int.MinValue;
                Vertex vertexMax = null;

                foreach (KeyValuePair<Vertex, int> record in saturationDegreeSequence)
                {
                    if (record.Value > max)
                        vertexMax = record.Key;
                }
                
                return vertexMax;
            }

            override
            public string ToString()
            {
                // Variable
                List<Vertex> vertexList;
                StringBuilder stringBuilder = new StringBuilder();

                stringBuilder.Append(graph.ToString());

                // Color
                vertexList = graph.AllVertices();
                stringBuilder.AppendLine("Vertex color: ");
                foreach (Vertex vertex in vertexList)
                {
                    stringBuilder.AppendLine("-- Vertex: " + vertex.GetIdentifier() + ", color: " + vertex.GetColor());
                }

                return stringBuilder.ToString();
            }

            /// <summary>
            /// Vrátí seznam použitých barev
            /// </summary>
            /// <returns>seznam použitých barev</returns>
            public List<int> UsedColors()
            {
                // Variable
                List<int> usedColorsList;

                usedColorsList = usedColorsDictionary.Keys.ToList();
                usedColorsList.Remove(VertexExtended.GetDefaultColor());

                return usedColorsList;
            }

            /// <summary>
            /// Vrátí seznam vrcholů, které jsou obarveny danou barvou
            /// </summary>
            /// <param name="color">daná barva</param>
            /// <returns>seznam vrcholů</returns>
            public List<Vertex> ColoredVertices(int color)
            {
                // Variable
                HashSet<Vertex> vertexHashSet = new HashSet<Vertex>();

                usedColorsDictionary.TryGetValue(color, out vertexHashSet);

                return vertexHashSet.ToList();
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
            
            /// <summary>
            /// Vrátí informaci zda je graf obarven
            /// </summary>
            /// <returns>true pokud je graf obarvený, jinak false</returns>
            public bool GetIsInicializedColoredGraph()
            {
                return isInicializedColoredGraph;
            }
            #endregion
        }
    }
}
