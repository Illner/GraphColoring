using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace GraphColoring.Graph
{
    public abstract partial class Graph : IGraphInterface
    {
        private class ColoredGgraph : IColoredGraphInterface
        {
            // Variable
            #region
            /// <summary>
            /// isInitializedColoredGraph - information if the colored graph is initialized
            /// graph - the graph
            /// countUsedColor - actual count of colors uses for coloring the graph
            /// coloredVertexList - list which contains all colored vertices
            /// unColoredVertexList - list which contains all UNcolored vertices
            /// isSaturation - is saturation on
            /// saturationDegreeSequence - dictionary where the first item is a vertex and the second item is its saturation (isSaturation = true)
            /// </summary>
            private bool isInitializedColoredGraph;
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
            /// Check if the vertex is correctly colored
            /// If the particular vertex does not exist throws GraphVertexDoesntExistException
            /// </summary>
            /// <param name="vertex">vertex</param>
            /// <returns>true if the vertex color is valid, otherwise false</returns>
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
            /// Check if all vertices are colored correctly
            /// </summary>
            /// <returns>list of vertices which are colored INcorrectly</returns>
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
            /// Color the vertex
            /// If the colored graph is initialized throws ColoredGraphAlreadyInitializedException
            /// </summary>
            /// <param name="vertex">vertex</param>
            /// <param name="color">color</param>
            public void ColorVertex(IVertexInterface vertex, int color)
            {
                if (isInitializedColoredGraph)
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
            /// Return a color of the vertex
            /// If the vertex has not a color return VertexExtended.GetDefaultColor()
            /// If the vertex does not exist in the graph return GraphVertexDoesntExistException
            /// </summary>
            /// <param name="vertex">vertex</param>
            /// <returns>color of vertex</returns>
            public int GetColorVertex(IVertexInterface vertex)
            {
                VertexExtended vertexExtended = graph.ConvertVertexToVertexExtended(vertex);

                return vertexExtended.GetColor();
            }

            /// <summary>
            /// Greedy coloring using a sequence
            /// If the graph is initialized throws ColoredGraphAlreadyInitializedException
            /// </summary>
            /// <param name="vertexList">sequence</param>
            /// <param name="interchange">coloring with interchange</param>
            public void GreedyColoring(List<IVertexInterface> vertexList, bool interchange = false)
            {
                // Variable
                VertexExtended vertexExtended;

                if (isInitializedColoredGraph)
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
            /// Greedy color a vertex
            /// </summary>
            /// <param name="vertex">vertex</param>
            /// <returns>color</returns>
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
            /// Return true if the vertex is colored, otherwise false
            /// If the vertex does not exist in the graph throws GraphVertexDoesntExistException
            /// </summary>
            /// <param name="vertex">vertex</param>
            /// <returns>true if the vertex is colored, otherwise false</returns>
            public bool IsVertexColored(IVertexInterface vertex)
            {
                VertexExtended vertexExtended = graph.ConvertVertexToVertexExtended(vertex);

                if (vertexExtended.GetColor() == VertexExtended.GetDefaultColor())
                    return false;

                return true;
            }

            /// <summary>
            /// Reset the color of the vertex (set the color to default)
            /// If the vertex does not exist in the graph throws GraphVertexDoesntExistException
            /// If the graph is initialized throws ColoredGraphAlreadyInitializedException
            /// </summary>
            /// <param name="vertex">vertex</param>
            public void ResetColorVertex(IVertexInterface vertex)
            {
                if (isInitializedColoredGraph)
                    throw new MyException.GraphException.ColoredGraphAlreadyInitializedException();

                VertexExtended vertexExtended = graph.ConvertVertexToVertexExtended(vertex);

                if (!IsVertexColored(vertex))
                    return;
                
                ChangeVertexInHashSets(vertex, VertexExtended.GetDefaultColor());
                vertexExtended.ResetColor();
            }

            /// <summary>
            /// Move a vertex in usedColorsDictionary (depends on color)
            /// Move the vertex from coloredVertexHashSet to unColoredVertexHashSet or from unColoredVertexHashSet to coloredVertexHashSet (depends on color)
            /// </summary>
            /// <param name="vertex">vertex</param>
            /// <param name="color">new color</param>
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
            /// Add a vertex to usedColorsDictionary
            /// Add the vertex to coloredVertexHashSet or unColoredVertexHashSet
            /// </summary>
            /// <param name="vertex">vertex</param>
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
            /// Remove a vertex from usedColorsDictionary
            /// Remove the vertex from coloredVertexHashSet or unColoredVertexHashSet
            /// </summary>
            /// <param name="vertex">vertex</param>
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
            /// Return true if the graph is colored correctly
            /// </summary>
            /// <returns>true if the graph is colored correctly, otherwise false</returns>
            public bool IsValidColored()
            {
                if (unColoredVertexHashSet.Count != 0)
                    return false;
                
                if (CheckValidColor().Count != 0)
                    return false;

                return true;
            }

            /// <summary>
            /// Initialize colored graph
            /// If the colored graph is initialized throws ColoredGraphAlreadyInitializedException
            /// </summary>
            /// <returns>true if the graph has been initialized, otherwise false/returns>
            public bool InitializeColoredGraph()
            {
                if (isInitializedColoredGraph)
                    throw new MyException.GraphException.ColoredGraphAlreadyInitializedException();

                if (IsValidColored())
                    isInitializedColoredGraph = true;
                else
                    isInitializedColoredGraph = false;

                return isInitializedColoredGraph;
            }

            /// <summary>
            /// Deinitialize colored graph
            /// If the colored graph is not initialized throws ColoredGraphNotInitializationException
            /// </summary>
            public void DeinitializationColoredGraph()
            {
                if (!isInitializedColoredGraph)
                    throw new MyException.GraphException.ColoredGraphNotInitializationException();

                isInitializedColoredGraph = false;

                isSaturation = false;
            }

            /// <summary>
            /// Reset all vertex colors
            /// If the colored graph is initialized throws ColoredGraphAlreadyInitializedException
            /// </summary>
            public void ResetColors()
            {
                // Variable
                List<IVertexInterface> vertexList;
                VertexExtended vertexExtended;

                if (isInitializedColoredGraph)
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
            /// Evaluate saturation for the vertex
            /// </summary>
            /// <param name="vertex">vertex</param>
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
            /// Set saturation
            /// </summary>
            /// <param name="saturation">Is saturation on</param>
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
            /// Return a vertex which is the most saturated
            /// </summary>
            /// <returns>vertex which is the most saturated</returns>
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
            /// Return a list of used colors
            /// </summary>
            /// <returns>list of used colors</returns>
            public List<int> UsedColors()
            {
                // Variable
                List<int> usedColorsList;

                usedColorsList = usedColorsDictionary.Keys.ToList();
                usedColorsList.Remove(VertexExtended.GetDefaultColor());

                return usedColorsList;
            }

            /// <summary>
            /// Return a list of vertices which are colored with the color = color
            /// </summary>
            /// <param name="color">color</param>
            /// <returns>list of vertices</returns>
            public List<IVertexInterface> ColoredVertices(int color)
            {
                // Variable
                HashSet<IVertexInterface> vertexHashSet = new HashSet<IVertexInterface>();

                usedColorsDictionary.TryGetValue(color, out vertexHashSet);

                if (vertexHashSet == null)
                    return new List<IVertexInterface>();

                return vertexHashSet.ToList();
            }

            /// <summary>
            /// Return a list of colors (except default color) which are used in neighbours of vertex
            /// </summary>
            /// <param name="vertex">vertex</param>
            /// <returns>list of colors</returns>
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
            /// Return a list of vertices which are neighbour of vertex and have color = color
            /// </summary>
            /// <param name="color">color</param>
            /// <param name="vertex">vertex</param>
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
            /// If all vertices are colored then return true
            /// </summary>
            /// <returns>true if all vertices are colored, otherwise false</returns>
            public bool AreAllVerticesColored()
            {
                if (unColoredVertexHashSet.Count == 0)
                    return true;

                return false;
            }

            public int TryChangeColoring(IVertexInterface mainVertex, int color)
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
            /// Return a count of used colors
            /// </summary>
            /// <returns>count of used colors</returns>
            public int GetCountUsedColors()
            {
                return usedColorsDictionary.Count() - 1;
            }

            /// <summary>
            /// Return a list which contains all colored vertices
            /// </summary>
            /// <returns>list which contains all colored vertifces</returns>
            public List<IVertexInterface> GetColoredVertexList()
            {
                return coloredVertexHashSet.ToList();
            }

            /// <summary>
            /// Return a list which contains all UNcolored vertices
            /// </summary>
            /// <returns>list which contains all UNcolored vertices</returns>
            public List<IVertexInterface> GetUnColoredVertexList()
            {
                return unColoredVertexHashSet.ToList();
            }
            
            /// <summary>
            /// Return true if the colored graph is initialized
            /// </summary>
            /// <returns>true if the colored graph is initialized, otherwise false</returns>
            public bool GetIsInitializedColoredGraph()
            {
                return isInitializedColoredGraph;
            }
            #endregion
        }
    }
}
