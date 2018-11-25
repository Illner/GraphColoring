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
            private Dictionary<int, HashSet<IVertexInterface>> usedColorsDictionary;
            private HashSet<IVertexInterface> coloredVertexHashSet;
            private HashSet<IVertexInterface> unColoredVertexHashSet;
            private bool isSaturation;
            private Dictionary<IVertexInterface, int> saturationDegreeSequence;
            #endregion

            // Constructor
            #region
            public ColoredGgraph(Graph graph)
            {
                this.graph = graph;
                coloredVertexHashSet = new HashSet<IVertexInterface>();
                unColoredVertexHashSet = new HashSet<IVertexInterface>(graph.AllVertices());
                usedColorsDictionary = new Dictionary<int, HashSet<IVertexInterface>>()
                {
                    { VertexExtended.GetDefaultColor(), new HashSet<IVertexInterface>(unColoredVertexHashSet) }
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
            public bool CheckValidColor(IVertexInterface vertex)
            {
                // Variable
                List<IVertexInterface> neighboursList;
                int colorVertex = vertex.GetColor();

                if (!graph.ExistsVertex(vertex))
                    throw new MyException.GraphException.GraphVertexDoesntExistException();

                if (colorVertex == VertexExtended.GetDefaultColor())
                    return true;

                neighboursList = graph.Neighbours(vertex);

                foreach(IVertexInterface neighbour in neighboursList)
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
            public List<IVertexInterface> CheckValidColor()
            {
                // Variable
                List<IVertexInterface> allVerticesList = graph.AllVertices();
                List<IVertexInterface> invalidColoredVerticesList = new List<IVertexInterface>();

                foreach(IVertexInterface vertex in allVerticesList)
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
            public void ColorVertex(IVertexInterface vertex, int color)
            {
                if (isInicializedColoredGraph)
                    throw new MyException.GraphException.ColoredGraphAlreadyInitializedException();

                VertexExtended vertexExtended = graph.ConvertVertexToVertexExtended(vertex);
                
                ChangeVertexInHashSets(vertex, color);
                vertexExtended.SetColor(color);

                // Saturation
                if (isSaturation)
                {
                    List<IVertexInterface> saturationVertex = graph.Neighbours(vertex);
                    saturationVertex.Add(vertex);

                    foreach (IVertexInterface vertexSaturation in saturationVertex)
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
            public int GetColorVertex(IVertexInterface vertex)
            {
                VertexExtended vertexExtended = graph.ConvertVertexToVertexExtended(vertex);

                return vertexExtended.GetColor();
            }

            /// <summary>
            /// Hladově obarví vrcholy podle dané posloupnosti
            /// Pokud je graf inicializovaný, vrátí vyjímku ColoredGraphAlreadyInitializedException
            /// </summary>
            /// <param name="vertexList">daná posloupnost</param>
            /// <param name="interchange">coloring with interchange</param>
            public void GreedyColoring(List<IVertexInterface> vertexList, bool interchange = false)
            {
                // Variable
                VertexExtended vertexExtended;

                if (isInicializedColoredGraph)
                    throw new MyException.GraphException.ColoredGraphAlreadyInitializedException();

                ResetColors();

                foreach (IVertexInterface vertex in vertexList)
                {
                    vertexExtended = graph.ConvertVertexToVertexExtended(vertex);
                    int color = GreedyColoring(vertex);

                    if (interchange && GetCountUsedColors() > 1 && (GetCountUsedColors() < color))
                    {
                        color = TryChangeColoring(vertex, color);
                    }

                    ColorVertex(vertex, color);
                }
            }

            /// <summary>
            /// Hladově obarví vrchol
            /// </summary>
            /// <param name="vertex">daný vrchol</param>
            /// <returns></returns>
            public int GreedyColoring(IVertexInterface vertex)
            {
                // Variable
                int neighbourColor;
                List<IVertexInterface> neighboursList;
                List<int> neighboursColorList;

                neighboursList = graph.Neighbours(vertex);
                neighboursColorList = new List<int>() { VertexExtended.GetDefaultColor() };

                // Get neighbours colors
                foreach(IVertexInterface neighbour in neighboursList)
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
            public bool IsVertexColored(IVertexInterface vertex)
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
            public void ResetColorVertex(IVertexInterface vertex)
            {
                if (isInicializedColoredGraph)
                    throw new MyException.GraphException.ColoredGraphAlreadyInitializedException();

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
            private void ChangeVertexInHashSets(IVertexInterface vertex, int color)
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
                    usedColorsDictionary.Add(color, new HashSet<IVertexInterface>() { vertex });
            }

            /// <summary>
            /// Přidá vertex do usedColorsDictionary
            /// Přidá vertex buď do coloredVertexHashSet, nebo do unColoredVertexHashSet
            /// </summary>
            /// <param name="vertex">daný vrchol</param>
            public void AddVertexInHashSets(IVertexInterface vertex)
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
                    usedColorsDictionary.Add(vertexColor, new HashSet<IVertexInterface>() { vertex });
            }

            /// <summary>
            /// Odebere vertex z usedColorsDictionary
            /// Odebere vertex z coloredVertexHashSet, nebo z unColoredVertexHashSet
            /// </summary>
            /// <param name="vertex">daný vrchol</param>
            public void RemoveVertexInHashSets(IVertexInterface vertex)
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
                    throw new MyException.GraphException.ColoredGraphAlreadyInitializedException();

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
                    throw new MyException.GraphException.ColoredGraphNotInitializationException();
                
                isInicializedColoredGraph = false;

                isSaturation = false;
            }

            /// <summary>
            /// Resetuje všechna obarvení vrcholů
            /// Pokud je graf inicializovaný, vrátí vyjímku ColoredGraphAlreadyInitializedException
            /// </summary>
            public void ResetColors()
            {
                // Variable
                List<IVertexInterface> vertexList;
                VertexExtended vertexExtended;

                if (isInicializedColoredGraph)
                    throw new MyException.GraphException.ColoredGraphAlreadyInitializedException();

                vertexList = graph.AllVertices();

                foreach (IVertexInterface vertex in vertexList)
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
            private void EvaluateSaturation(IVertexInterface vertex)
            {
                // Variable
                List<int> neighboursColors = new List<int>();
                List<IVertexInterface> neighboursVertex = graph.Neighbours(vertex);

                if (vertex.GetColor() != VertexExtended.GetDefaultColor())
                {
                    saturationDegreeSequence.Remove(vertex);
                    return;
                }
                

                foreach(IVertexInterface neighbourVertex in neighboursVertex)
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
                if (isSaturation == saturation)
                    return;

                isSaturation = saturation;

                if (isSaturation)
                {
                    saturationDegreeSequence = new Dictionary<IVertexInterface, int>();

                    foreach (IVertexInterface vertex in graph.AllVertices())
                    {
                        saturationDegreeSequence.Add(vertex, 0);
                    }
                }
            }
               
            /// <summary>
            /// Vrátí vrchol, který je nejvíce nasycený
            /// </summary>
            /// <returns>vrchol s největší nasyceností</returns>
            public IVertexInterface GetSaturationDegreeSequence()
            {
                // Variable
                int max = int.MinValue;
                IVertexInterface vertexMax = null;

                if (isSaturation == false)
                    throw new MyException.GraphException.ColoredGraphNotInitializationSaturation();

                foreach (KeyValuePair<IVertexInterface, int> record in saturationDegreeSequence)
                {
                    if (record.Value > max)
                    {
                        vertexMax = record.Key;
                        max = record.Value;
                    }
                }

                return vertexMax;
            }

            override
            public string ToString()
            {
                // Variable
                List<IVertexInterface> vertexList;
                StringBuilder stringBuilder = new StringBuilder();

                stringBuilder.Append(graph.ToString());

                // Color
                vertexList = graph.AllVertices();
                stringBuilder.AppendLine("Vertex color: ");
                foreach (IVertexInterface vertex in vertexList)
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
            public List<IVertexInterface> ColoredVertices(int color)
            {
                // Variable
                HashSet<IVertexInterface> vertexHashSet = new HashSet<IVertexInterface>();

                usedColorsDictionary.TryGetValue(color, out vertexHashSet);

                return vertexHashSet.ToList();
            }

            /// <summary>
            /// Return List of colors (except default color) which are used in neighbours of vertex
            /// </summary>
            /// <param name="vertex">The vertex</param>
            /// <returns>List of colors</returns>
            public List<int> ColorsNeighbours(IVertexInterface vertex)
            {
                HashSet<int> colorsHashSet = new HashSet<int>();
                List<IVertexInterface> neighboursList = graph.Neighbours(vertex);

                foreach(IVertexInterface neighbour in neighboursList)
                {
                    colorsHashSet.Add(neighbour.GetColor());
                }

                colorsHashSet.Remove(VertexExtended.GetDefaultColor());

                return colorsHashSet.ToList();
            }

            /// <summary>
            /// Return list of vertices which are neighbour of vertex and have color = color
            /// </summary>
            /// <param name="color">The color</param>
            /// <param name="vertex">The vertex</param>
            /// <returns>list of vertices</returns>
            public List<IVertexInterface> ColoredNeighbours(int color, IVertexInterface vertex)
            {
                // Variable
                List<IVertexInterface> vertexNeighbourList = new List<IVertexInterface>();
                List<IVertexInterface> neighboursList = graph.Neighbours(vertex);

                vertexNeighbourList = neighboursList.Where(neighbour => neighbour.GetColor() == color).ToList();

                return vertexNeighbourList;
            }

            /// <summary>
            /// Pokud jsou všechny vrcholy grafu obarveny, tak vrátí true, jinak false
            /// </summary>
            /// <returns>true, pokud jsou všechny vrcholy grafu obarveny, jinak false</returns>
            public bool AreAllVerticesColored()
            {
                if (unColoredVertexHashSet.Count == 0)
                    return true;

                return false;
            }

            private int TryChangeColoring(IVertexInterface mainVertex, int color)
            {
                // Variable
                bool connected;
                int actualCountOfColors;
                int color1, color2, color3;
                Queue<IVertexInterface> vertexQueue;
                List<IVertexInterface> neighboursList;
                List<IVertexInterface> neighboursColorList;
                HashSet<IVertexInterface> visitedVertexHashSet = null;
                
                neighboursList = graph.Neighbours(mainVertex);
                connected = true;
                color1 = VertexExtended.GetDefaultColor();
                color2 = VertexExtended.GetDefaultColor();
                actualCountOfColors = GetCountUsedColors();
                
                while (connected && color1 < actualCountOfColors)
                {
                    color1++;
                    color2 = color1;

                    while (connected && color2 < actualCountOfColors)
                    {
                        color2++;
                        neighboursColorList = ColoredNeighbours(color1, mainVertex);
                        visitedVertexHashSet = new HashSet<IVertexInterface>(neighboursColorList);
                        vertexQueue = new Queue<IVertexInterface>(neighboursColorList);

                        // Get bichromatic graph with color1 and color2
                        while (vertexQueue.Count != 0)
                        {
                            IVertexInterface vertex = vertexQueue.Dequeue();

                            if (vertex.GetColor() == color1)
                                color3 = color2;
                            else
                                color3 = color1;

                            neighboursColorList = ColoredNeighbours(color3, vertex);

                            foreach(IVertexInterface neighbour in neighboursColorList)
                            {
                                if (!visitedVertexHashSet.Contains(neighbour))
                                {
                                    visitedVertexHashSet.Add(neighbour);
                                    vertexQueue.Enqueue(neighbour);
                                }
                            }
                        }

                        // Check if exists path with color1 - ... - color2 => we can't change colors
                        neighboursColorList = ColoredNeighbours(color2, mainVertex);
                        connected = visitedVertexHashSet.Any(x => neighboursColorList.Contains(x));               
                    }
                }

                // Path doesn't exist, we can change colors
                if (!connected)
                {
                    foreach(IVertexInterface vertex in visitedVertexHashSet)
                    {
                        if (vertex.GetColor() == color1)
                            ColorVertex(vertex, color2);
                        else
                            ColorVertex(vertex, color1);
                    }

                    color = color1;
                }

                return color;
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
            public List<IVertexInterface> GetColoredVertexList()
            {
                return coloredVertexHashSet.ToList();
            }

            /// <summary>
            /// Vrátí seznam vrcholů, které NEJSOU obarveny
            /// </summary>
            /// <returns></returns>
            public List<IVertexInterface> GetUnColoredVertexList()
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
