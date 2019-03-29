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
            // InterchangeExtended
            private Dictionary<IVertexInterface, List<int>> availableRecoloringDictionary;
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

                foreach (IVertexInterface neighbour in neighboursList)
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

                foreach (IVertexInterface vertex in allVerticesList)
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
            public void GreedyColoring(List<IVertexInterface> vertexList, GraphColoringAlgorithm.SequenceAlgorithm.GraphColoringSequenceAlgorithm.GraphColoringAlgorithInterchangeEnum interchangeEnum = GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.none)
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

                    if (interchangeEnum != GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.none && GetCountUsedColors() > 1 && (GetCountUsedColors() < color))
                    {
                        switch (interchangeEnum)
                        {
                            case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchange:
                                    TryChangeColoring(vertex, color);
                                break;
                            case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtended:
                                    TryChangeColoringExtended(vertex, color);
                                break;
                        }
                        
                    }
                    else
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
                foreach (IVertexInterface neighbour in neighboursList)
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


                foreach (IVertexInterface neighbourVertex in neighboursVertex)
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
                    stringBuilder.AppendLine("-- Vertex (identifier): " + vertex.GetIdentifier() + ", vertex (userName): " + vertex.GetUserName() + ", color: " + vertex.GetColor());
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

                foreach (IVertexInterface neighbour in neighboursList)
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

            // Interchange
            public bool TryChangeColoring(IVertexInterface mainVertex, int color)
            {
                // Variable
                bool connected = true;
                int actualCountOfColors;
                int color1, color2, color3;
                Queue<IVertexInterface> vertexQueue;
                List<IVertexInterface> neighboursColorList;
                HashSet<IVertexInterface> visitedVertexHashSet = null;

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

                            foreach (IVertexInterface neighbour in neighboursColorList)
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
                    foreach (IVertexInterface vertex in visitedVertexHashSet)
                    {
                        if (vertex.GetColor() == color1)
                            ColorVertex(vertex, color2);
                        else
                            ColorVertex(vertex, color1);
                    }

                    color = color1;
                }

                ColorVertex(mainVertex, color);

                if (!connected)
                    return true;

                return false;
            }

            // Interchange extended
            public bool TryChangeColoringExtended(IVertexInterface mainVertex, int color)
            {
                // Test
                //Console.WriteLine("------------------------------------------");
                //Console.WriteLine("TryChangeColoringExtended: " + mainVertex.GetUserName());
                //Console.WriteLine("------------------------------------------");
                //Console.WriteLine(graph.GetColoredGraph());
                //Console.WriteLine("------------------------------------------");

                // Variable - standard
                bool connected = true;
                int actualCountOfColors;
                int color1, color2, color3;
                Queue<IVertexInterface> vertexQueue;
                List<IVertexInterface> neighboursColorList;
                HashSet<IVertexInterface> visitedVertexHashSet = null;

                // Variable - extended
                bool recoloredK3 = false;
                bool bichromaticGraphCanBeDivided = false;
                IVertexInterface cutVertexWithProperty = null;
                List<IVertexInterface> potentionalCutVertices;
                List<IVertexInterface> neighborsVerticesColor1;
                List<IVertexInterface> neighborsVerticesColor2;
                List<IVertexInterface> verticesWeWantToChange = null;
                HashSet<IVertexInterface> possibleRecoloredVerticesHashSet = null;
                bool existsEdgeBetweenneighborsVerticesColor1AndneighborsVerticesColor2;

                Tuple<IVertexInterface, IVertexInterface, IVertexInterface, IVertexInterface> BackUpK3Tuple = null;

                color1 = VertexExtended.GetDefaultColor();
                color2 = VertexExtended.GetDefaultColor();
                actualCountOfColors = GetCountUsedColors();

                while ((!bichromaticGraphCanBeDivided && connected) && color1 < actualCountOfColors)
                {
                    color1++;
                    color2 = color1;

                    while ((!bichromaticGraphCanBeDivided && connected) && color2 < actualCountOfColors)
                    {
                        color2++;
                        neighboursColorList = ColoredNeighbours(color1, mainVertex);
                        visitedVertexHashSet = new HashSet<IVertexInterface>(neighboursColorList);
                        vertexQueue = new Queue<IVertexInterface>(neighboursColorList);

                        // Test
                        //Console.WriteLine("Color1: " + color1 + ", color2: " + color2);

                        // Get bichromatic graph with color1 and color2
                        while (vertexQueue.Count != 0)
                        {
                            IVertexInterface vertex = vertexQueue.Dequeue();

                            if (vertex.GetColor() == color1)
                                color3 = color2;
                            else
                                color3 = color1;

                            neighboursColorList = ColoredNeighbours(color3, vertex);

                            foreach (IVertexInterface neighbour in neighboursColorList)
                            {
                                if (!visitedVertexHashSet.Contains(neighbour))
                                {
                                    visitedVertexHashSet.Add(neighbour);
                                    vertexQueue.Enqueue(neighbour);
                                }
                            }
                        }

                        // Standard interchange
                        // Check if exists path with color1 - ... - color2 => we can't change colors
                        neighboursColorList = ColoredNeighbours(color2, mainVertex);
                        connected = visitedVertexHashSet.Any(x => neighboursColorList.Contains(x));

                        // Test
                        //Console.WriteLine("Connected: " + connected);

                        if (!connected)
                            break;

                        // Initialize valiableRecoloringDictionary
                        availableRecoloringDictionary = new Dictionary<IVertexInterface, List<int>>();

                        // Extended interchange
                        neighborsVerticesColor1 = ColoredNeighbours(color1, mainVertex);
                        neighborsVerticesColor2 = new List<IVertexInterface>();
                        // Filter vertices which are neighbors of mainVertex, have color2 and are in the bichromatic graph!
                        List<IVertexInterface> neighborsVerticesColor2Temp = ColoredNeighbours(color2, mainVertex);
                        foreach (IVertexInterface vertex in neighborsVerticesColor2Temp)
                        {
                            if (visitedVertexHashSet.Contains(vertex))
                                neighborsVerticesColor2.Add(vertex);
                        }

                        // Check if any vertex (neighbor of mainVertex) with color1 is connected to any vertex (neighbor of mainVertex) with color2
                        existsEdgeBetweenneighborsVerticesColor1AndneighborsVerticesColor2 = false;
                        IVertexInterface firstVertexTemp = null, secondVertexTemp = null;

                        foreach (IVertexInterface neighbor in neighborsVerticesColor1)
                        {
                            foreach (IVertexInterface neighborOfNeighbor in ColoredNeighbours(color2, neighbor))
                            {
                                if (graph.ExistsEdge(new Edge(neighborOfNeighbor, mainVertex)))
                                {
                                    existsEdgeBetweenneighborsVerticesColor1AndneighborsVerticesColor2 = true;

                                    if (firstVertexTemp != null)
                                    {
                                        firstVertexTemp = null;
                                        secondVertexTemp = null;
                                        break;
                                    }
                                    else
                                    {
                                        firstVertexTemp = neighbor;
                                        secondVertexTemp = neighborOfNeighbor;
                                    }
                                }
                                
                                /*
                                if (graph.Neighbours(neighbor).Any(x => neighborsVerticesColor2.Contains(x)))
                                {
                                    existsEdgeBetweenneighborsVerticesColor1AndneighborsVerticesColor2 = true;

                                    if (firstVertexTemp != null)
                                    {
                                        break;
                                    }
                                }
                                */
                            }

                        }

                        // Test
                        //Console.WriteLine("existsEdgeBetweenneighborsVerticesColor1AndneighborsVerticesColor2: " + existsEdgeBetweenneighborsVerticesColor1AndneighborsVerticesColor2);
                        //Console.WriteLine("firstVertexTemp: " + firstVertexTemp);
                        //Console.WriteLine("secondVertexTemp: " + secondVertexTemp);

                        // Exist at least two edges => does not exist a cut vertex that divide the bichromatic graph into two subgraphs where the first component contains 
                        // all vertices from the neighborsVerticesColor1 and the second component contains all vertices from the neighborsVerticesColor2
                        if (existsEdgeBetweenneighborsVerticesColor1AndneighborsVerticesColor2 && firstVertexTemp == null)
                        //if (existsEdgeBetweenneighborsVerticesColor1AndneighborsVerticesColor2)
                            continue;

                        // Exists one edge => try to recolor the K3 (firstVertexTemp, secondVertexTemp, mainVertex)
                        if (existsEdgeBetweenneighborsVerticesColor1AndneighborsVerticesColor2 && BackUpK3Tuple == null)
                        {
                            // Variable
                            IVertexInterface vertexK3;

                            // firstVertexTemp
                            List<IVertexInterface> neighborsList = null;
                            recoloredK3 = false;

                            for (int i = 0; i < 2; i++)
                            {
                                if (i == 0)
                                {
                                    neighborsList = graph.Neighbours(firstVertexTemp);
                                    neighborsList.Remove(secondVertexTemp);
                                    vertexK3 = firstVertexTemp;
                                }
                                else
                                {
                                    neighborsList = graph.Neighbours(secondVertexTemp);
                                    neighborsList.Remove(firstVertexTemp);
                                    vertexK3 = secondVertexTemp;
                                }

                                foreach (IVertexInterface neighbor1 in neighborsList)
                                {
                                    foreach (IVertexInterface neighbor2 in neighborsList)
                                    {
                                        if (neighbor1 == neighbor2)
                                            continue;
                                        
                                        if (graph.ExistsEdge(new Edge(neighbor1, neighbor2)))
                                        {
                                            //Console.WriteLine("Computing: {0}, {1}, {2}", vertexK3, neighbor1, neighbor2);
                                            recoloredK3 = RecolorK3(mainVertex, vertexK3, neighbor1, neighbor2, false);
                                        }
                                        
                                        if (recoloredK3)
                                        {
                                            BackUpK3Tuple = new Tuple<IVertexInterface, IVertexInterface, IVertexInterface, IVertexInterface>(mainVertex, vertexK3, neighbor1, neighbor2);
                                            break;
                                        }
                                        
                                    }
                                    
                                    if (recoloredK3)
                                        break;
                                }
                                
                                if (recoloredK3)
                                    break;
                            }

                            // Test
                            //Console.WriteLine("recoloredK3: " + recoloredK3);

                            //if (recoloredK3)
                                //bichromaticGraphCanBeDivided = true;

                            continue;
                        }

                        if (existsEdgeBetweenneighborsVerticesColor1AndneighborsVerticesColor2)
                            continue;

                        // An edge does not exist => we can find all cut vertices in the bichromatic graph
                        IGraphInterface subGraph = GraphOperation.GraphOperation.SubGraph(graph, visitedVertexHashSet.ToList());

                        // Get vertices correspond to neighborsVerticesColor1 and neighborsVerticesColor2 in the subgraph
                        List<IVertexInterface> neighborsVerticesColor1SubGraph = new List<IVertexInterface>();
                        List<IVertexInterface> neighborsVerticesColor2SubGraph = new List<IVertexInterface>();
                        foreach (IVertexInterface vertex in neighborsVerticesColor1)
                        {
                            neighborsVerticesColor1SubGraph.Add(subGraph.GetVertexByUserName(vertex.GetUserName()));
                        }
                        foreach (IVertexInterface vertex in neighborsVerticesColor2)
                        {
                            neighborsVerticesColor2SubGraph.Add(subGraph.GetVertexByUserName(vertex.GetUserName()));
                        }

                        // Add two main vertices... the first vertex for the first color and the second one for the second color
                        IVertexInterface mainVertexColor1 = new Vertex();
                        IVertexInterface mainVertexColor2 = new Vertex();
                        subGraph.VertexAdd(mainVertexColor1);
                        subGraph.VertexAdd(mainVertexColor2);
                        // Add edges
                        foreach (IVertexInterface vertexColor1 in neighborsVerticesColor1SubGraph)
                        {
                            subGraph.EdgeAdd(new Edge(mainVertexColor1, vertexColor1));
                        }
                        // Add edges
                        foreach (IVertexInterface vertexColor2 in neighborsVerticesColor2SubGraph)
                        {
                            subGraph.EdgeAdd(new Edge(mainVertexColor2, vertexColor2));
                        }

                        // Get potentional cut vertices
                        potentionalCutVertices = subGraph.GetGraphProperty().GetCutVertices();

                        // Test
                        //Console.WriteLine("mainVertexColor1: " + mainVertexColor1);
                        //Console.WriteLine("mainVertexColor2: " + mainVertexColor2);
                        //Console.WriteLine("potentionalCutVertices");
                        //potentionalCutVertices.ForEach(x => Console.WriteLine(x.GetUserName() + " "));
                        //Console.WriteLine();

                        IGraphInterface firstComponent = null, secondComponent = null;

                        // Check if the cut vertex has the property
                        foreach (IVertexInterface cutVertex in potentionalCutVertices)
                        {
                            // mainVertexColor1 and mainVertexColor2 are not allowed as cut vertices
                            if (cutVertex.Equals(mainVertexColor1) || cutVertex.Equals(mainVertexColor2))
                                continue;

                            List<IVertexInterface> cutVertexNeighborsList = subGraph.Neighbours(cutVertex);
                            subGraph.VertexDelete(cutVertex);
                            //List<IGraphInterface> componentsList = subGraph.GetGraphProperty().GetComponents();

                            firstComponent = subGraph.GetGraphProperty().GetComponents().First();
                            secondComponent = subGraph.GetGraphProperty().GetComponents().Skip(1).First();

                            // The mainVertexColor1 is in the first component and the mainVertexColor2 is in the second component (or vice versa)
                            if ((firstComponent.ExistsUserName(mainVertexColor1.GetUserName()) && secondComponent.ExistsUserName(mainVertexColor2.GetUserName())) ||
                                (secondComponent.ExistsUserName(mainVertexColor1.GetUserName()) && firstComponent.ExistsUserName(mainVertexColor2.GetUserName())))
                            {
                                List<IVertexInterface> possibleRecoloredVerticesList;

                                if (firstComponent.ExistsUserName(mainVertexColor1.GetUserName()))
                                    possibleRecoloredVerticesList = firstComponent.AllVertices();
                                else
                                    possibleRecoloredVerticesList = secondComponent.AllVertices();

                                possibleRecoloredVerticesHashSet = new HashSet<IVertexInterface>();
                                foreach (IVertexInterface vertex in possibleRecoloredVerticesList)
                                {
                                    if (vertex.GetUserName().Equals(mainVertexColor1.GetUserName()) || vertex.GetUserName().Equals(mainVertexColor2.GetUserName()))
                                        continue;

                                    possibleRecoloredVerticesHashSet.Add(graph.GetVertexByUserName(vertex.GetUserName()));
                                }

                                // Check if the cut vertex can be recolored
                                if (CanBeCutVertexRecolored(graph.GetVertexByUserName(cutVertex.GetUserName()), color1, color2, false, possibleRecoloredVerticesHashSet, mainVertex))
                                {
                                    cutVertexWithProperty = cutVertex;
                                    break;
                                }
                            }

                            // The cut vertex does not have the property => recovery the bichromatic graph
                            subGraph.VertexAdd(cutVertex);

                            foreach (IVertexInterface cutVertexNeighbors in cutVertexNeighborsList)
                            {
                                subGraph.EdgeAdd(new Edge(cutVertex, cutVertexNeighbors));
                            }
                        }

                        // Does not exist a cut vertex with the property
                        if (cutVertexWithProperty == null)
                            continue;
                        // A cut vertex with the property exists => get all vertices which can be recolored
                        else
                        {
                            IVertexInterface mainVertexColorComponent;

                            // Get vertices correspond to mainVertexColor1 and mainVertexColor2 in the subgraph components
                            // We must recolor vertices in the first component
                            if (firstComponent.ExistsUserName(mainVertexColor1.GetUserName()))
                            {
                                mainVertexColorComponent = firstComponent.GetVertexByUserName(mainVertexColor1.GetUserName());

                                verticesWeWantToChange = firstComponent.AllVertices();
                            }
                            // We must recolor vertices in the second component
                            else
                            {
                                mainVertexColorComponent = secondComponent.GetVertexByUserName(mainVertexColor1.GetUserName());

                                verticesWeWantToChange = secondComponent.AllVertices();
                            }

                            verticesWeWantToChange.Remove(mainVertexColorComponent);

                            bichromaticGraphCanBeDivided = true;
                        }

                        // Test
                        //Console.WriteLine("bichromaticGraphCanBeDivided: " + bichromaticGraphCanBeDivided);
                    }

                    // Test
                    //Console.WriteLine();
                }

                if (connected && !bichromaticGraphCanBeDivided && BackUpK3Tuple != null)
                {
                    // Test
                    //Console.WriteLine("K3 recoloring!!!!!!!!!!!!!!!!!!!!!!!!");
                    //Console.WriteLine("MainVertex: {0}, cutVertex: {1}, vertex1: {2}, vertex2: {3}", BackUpK3Tuple.Item1, BackUpK3Tuple.Item2, BackUpK3Tuple.Item3, BackUpK3Tuple.Item4);

                    color = BackUpK3Tuple.Item2.GetColor();
                    RecolorK3(BackUpK3Tuple.Item1, BackUpK3Tuple.Item2, BackUpK3Tuple.Item3, BackUpK3Tuple.Item4, true);
                }

                // Path doesn't exist, we can change colors
                if (!connected)
                {
                    foreach (IVertexInterface vertex in visitedVertexHashSet)
                    {
                        if (vertex.GetColor() == color1)
                            ColorVertex(vertex, color2);
                        else
                            ColorVertex(vertex, color1);
                    }

                    color = color1;
                }

                if (bichromaticGraphCanBeDivided)
                {
                    // Test
                    //if (cutVertexWithProperty != null)
                        //Console.WriteLine("cutVertexWithProperty: " + cutVertexWithProperty.GetUserName());

                    // Test
                    //if (cutVertexWithProperty != null)
                        //Console.WriteLine("Before cutVertexWithProperty: " + graph.GetVertexByUserName(cutVertexWithProperty.GetUserName()).GetColor());
                    
                    // Recolor the cut vertex
                    CanBeCutVertexRecolored(graph.GetVertexByUserName(cutVertexWithProperty.GetUserName()), color1, color2, true, possibleRecoloredVerticesHashSet, mainVertex);

                    // Test
                    //Console.WriteLine("After cutVertexWithProperty: " + graph.GetVertexByUserName(cutVertexWithProperty.GetUserName()).GetColor());

                    // Recolor all vertices in a component
                    foreach (IVertexInterface vertexInSubGraph in verticesWeWantToChange)
                    {
                        IVertexInterface vertex = graph.GetVertexByUserName(vertexInSubGraph.GetUserName());

                        // Test
                        //Console.WriteLine("vertex: " + vertex.GetColor());

                        if (vertex.GetColor() == color1)
                            ColorVertex(vertex, color2);
                        else
                            ColorVertex(vertex, color1);
                    }

                    // Recolor the main vertex
                    color = color1;

                    // Test
                    //Console.WriteLine("mainVertex: " + color);
                }

                ColorVertex(mainVertex, color);

                // Test
                //Console.WriteLine("------------------------------------------");
                //Console.WriteLine("TryChangeColoringExtended: " + mainVertex.GetUserName());
                //Console.WriteLine("------------------------------------------");
                //Console.WriteLine(graph.GetColoredGraph());
                //Console.WriteLine("------------------------------------------");

                if (bichromaticGraphCanBeDivided || !connected)
                    return true;

                return false;
            }

            /// <summary>
            /// Check if the cut vertex can be recolored
            /// 1) recolor the vertex without recoloring his neighbors
            /// 2) recolor his all neighbors which have the same color and their original color use for recoloring the cut vertex
            /// </summary>
            /// <param name="cutVertex">Cut vertex which we want to recolor</param>
            /// <param name="invalidColor1">The first color which the actualVertex must not be colored (the first color of bichromatic graph)</param>
            /// <param name="invalidColor2">The second color which the actualVertex must not be colored (the second color in bichromatic graph)</param>
            /// <param name="recolor">Recolor vertices if it is possible?</param>
            /// <param name="possibleRecoloredVerticesHashSet">Set of vertices which will be recolored if the vertex can be recolored</param>
            /// <param name="mainVertex">main vertex</param>
            /// <returns>true if the cut vertex can be (was) recolored, otherwise false</returns>
            private bool CanBeCutVertexRecolored(IVertexInterface cutVertex, int invalidColor1, int invalidColor2, bool recolor, HashSet<IVertexInterface> possibleRecoloredVerticesHashSet, IVertexInterface mainVertex)
            {
                // Test
                //Console.WriteLine("CanBeCutVertexRecolored: " + cutVertex.GetUserName());

                // Variable
                List<int> availableRecoloringList;
                int color2 = invalidColor1;

                if (cutVertex.GetColor() == invalidColor1)
                    color2 = invalidColor2;

                // Try to recolor the cut vertex without recoloring his neighbors
                availableRecoloringList = CanBeVertexRecolored(cutVertex, VertexExtended.GetDefaultColor(), invalidColor1, color2, possibleRecoloredVerticesHashSet, mainVertex);
                availableRecoloringList.Remove(invalidColor1);
                availableRecoloringList.Remove(invalidColor2);

                // The cut vertex can be recolored without recoloring his neighbors
                if (availableRecoloringList.Count() > 0)
                {
                    // Test
                    //Console.WriteLine("1)");

                    if (!recolor)
                        return true;
                    
                    ColorVertex(cutVertex, GetMostUsedColorNeighborsNeighbor(cutVertex, availableRecoloringList));

                    return true;
                }

                // The cut vertex can not be recolored without recoloring his neighbors => try to recolor his neighbors with the same color 
                foreach (int color in ColorsNeighbours(cutVertex))
                {
                    bool canBeNeighborsRecolored = true;

                    if (color == invalidColor1 || color == invalidColor2)
                        continue;

                    foreach (IVertexInterface neighbor in ColoredNeighbours(color, cutVertex))
                    {
                        // Test
                        //Console.WriteLine("->");

                        if (CanBeVertexRecolored(neighbor, cutVertex.GetColor(), invalidColor1, color2, possibleRecoloredVerticesHashSet, mainVertex).Count() == 0)
                        {
                            // Test
                            //Console.WriteLine("<-");
                            //Console.WriteLine("Fail");

                            canBeNeighborsRecolored = false;
                            break;
                        }
                        // Test
                        //Console.WriteLine("<-");
                    }

                    // Neighbors with the same color can be recolored
                    if (canBeNeighborsRecolored)
                    {
                        // Test
                        //Console.WriteLine("2)");

                        if (recolor)
                        {
                            foreach (IVertexInterface neighbor in ColoredNeighbours(color, cutVertex))
                            {
                                availableRecoloringList = CanBeVertexRecolored(neighbor, cutVertex.GetColor(), invalidColor1, color2, possibleRecoloredVerticesHashSet, mainVertex);
                                
                                ColorVertex(neighbor, GetMostUsedColorNeighborsNeighbor(neighbor, availableRecoloringList));

                                // Test
                                //Console.WriteLine("Set: vertex {0} -> {1}", neighbor.GetUserName(), availableRecoloringList.First());
                            }

                            ColorVertex(cutVertex, color);
                        }

                        return true;
                    }
                }

                // Test
                //Console.WriteLine("0)");

                return false;
            }
            
            /// <summary>
            /// Return a list contains colors which can be used for recoloring the vertex due to bichromatic graph
            /// Conditions:
            /// 1) If the count of neighbors with the parentColor's color is 1 than this color be added to the list
            /// 2) If exists neighbor (not cut vertex) with the parentColor's color then removes color2 from the list
            /// 3) If the vertex is conneted with the mainVertex then removes color1 from the list
            /// </summary>
            /// <param name="vertex">vertex</param>
            /// <param name="parentColor">color of parentColor (cut vertex)</param>
            /// <param name="color1">the first color in bichromatic graph</param>
            /// <param name="color2">the second color in bichromatic graph</param>
            /// <param name="possibleRecoloredVerticesHashSet">Set of vertices which will be recolored if the vertex can be recolored</param>
            /// <param name="mainVertex">main vertex</param>
            /// <returns>available color list due to bichromatic graph</returns>
            private List<int> CanBeVertexRecolored(IVertexInterface vertex, int parentColor, int color1, int color2, HashSet<IVertexInterface> possibleRecoloredVerticesHashSet, IVertexInterface mainVertex)
            {
                // Test
                //Console.WriteLine("CanBeVertexRecolored: Vertex: {0}, parentColor: {1}", vertex.GetUserName(), parentColor);

                // Variable
                List<int> availableRecoloringList;

                availableRecoloringList = GetAvailableRecolors(vertex);

                // 1)
                if (parentColor != VertexExtended.GetDefaultColor() && ColoredNeighbours(parentColor, vertex).Count() == 1)
                {
                    bool isNeighborAnyPossibleRecoloredVertexParent = false;

                    foreach (IVertexInterface neighbor in graph.Neighbours(vertex))
                    {
                        if (neighbor.GetColor() == color2 && possibleRecoloredVerticesHashSet.Contains(neighbor))
                        {
                            isNeighborAnyPossibleRecoloredVertexParent = true;
                            break;
                        }
                    }

                    if (!isNeighborAnyPossibleRecoloredVertexParent)
                        availableRecoloringList.Add(parentColor);
                }
                // 2)
                else
                {
                    bool isNeighborAnyPossibleRecoloredVertexChange = false;

                    foreach (IVertexInterface neighbor in graph.Neighbours(vertex))
                    {
                        if (neighbor.GetColor() == parentColor && possibleRecoloredVerticesHashSet.Contains(neighbor))
                        {
                            isNeighborAnyPossibleRecoloredVertexChange = true;
                            break;
                        }
                    }

                    if (isNeighborAnyPossibleRecoloredVertexChange)
                        availableRecoloringList.Remove(color2);
                }

                // 3)
                if (graph.Neighbours(vertex).Exists(x => x.GetUserName() == mainVertex.GetUserName() ? true : false))
                {
                    availableRecoloringList.Remove(color1);
                }
                
                return availableRecoloringList;
            }

            /// <summary>
            /// Return a list contains all colors which can be used for valid recoloring the vertex
            /// Uses data structure availableRecoloringDictionary
            /// </summary>
            /// <param name="vertex">vertex</param>
            /// <returns>avaliable color list</returns>
            private List<int> GetAvailableRecolors(IVertexInterface vertex)
            {
                // Variable
                List<int> availableRecoloringList, availableRecoloringListCopy;

                // Check if the vertex available recolor list is not already computed
                availableRecoloringDictionary.TryGetValue(vertex, out availableRecoloringList);

                // Vertex has been computed before
                if (availableRecoloringList != null)
                {
                    availableRecoloringListCopy = new List<int>(availableRecoloringList);

                    return availableRecoloringListCopy;
                }

                // Vertex has not been computed yet
                // Try to recolor the cut vertex without recoloring his neighbors

                List<int> usedColors = UsedColors();
                List<int> invalidColorsForVertex = ColorsNeighbours(vertex);
                invalidColorsForVertex.Add(vertex.GetColor());

                availableRecoloringList = usedColors.Except(invalidColorsForVertex).ToList();

                availableRecoloringDictionary.Add(vertex, availableRecoloringList);
                availableRecoloringListCopy = new List<int>(availableRecoloringList);

                return availableRecoloringListCopy;
            }

            /// <summary>
            /// Check if the K3 can be recolored
            /// Vertices connected with mainVertex must not have been recolored to cutVertex's color
            /// If any of vertices is not colored then returns false
            /// </summary>
            /// <param name="mainVertex">mainVertex</param>
            /// <param name="cutVertex">The first vertex of K3 = cut vertex</param>
            /// <param name="vertex2">The second vertex of K3</param>
            /// <param name="vertex3">The third vertex of K3</param>
            /// <param name="recolor">Recolor vertices if it is possible?</param>
            /// <returns>true if the K3 can be recolored, otherwise false</returns>
            private bool RecolorK3(IVertexInterface mainVertex, IVertexInterface cutVertex, IVertexInterface vertex2, IVertexInterface vertex3, bool recolor)
            {
                // Variable
                List<int> availableRecoloringVertex1List;
                List<int> availableRecoloringVertex2List;
                List<int> availableRecoloringVertex3List;
                int color1 = cutVertex.GetColor();
                int color2 = vertex2.GetColor();
                int color3 = vertex3.GetColor();

                if (color1 == VertexExtended.GetDefaultColor() || color2 == VertexExtended.GetDefaultColor() || color3 == VertexExtended.GetDefaultColor())
                    return false;

                // Recoloring the mainVertex with cutVertex.GetColor() is invalid coloring
                if (ColoredNeighbours(cutVertex.GetColor(), mainVertex).Count() != 1)
                    return false;

                availableRecoloringVertex1List = GetAvailableRecolors(cutVertex);
                availableRecoloringVertex2List = GetAvailableRecolors(vertex2);
                availableRecoloringVertex3List = GetAvailableRecolors(vertex3);

                // Vertex1
                if (ColoredNeighbours(color2, cutVertex).Count == 1)
                    availableRecoloringVertex1List.Add(color2);
                if (ColoredNeighbours(color3, cutVertex).Count == 1)
                    availableRecoloringVertex1List.Add(color3);
                
                // Vertex2
                if (ColoredNeighbours(color1, vertex2).Count == 1)
                    availableRecoloringVertex2List.Add(color1);
                if (ColoredNeighbours(color3, vertex2).Count == 1)
                    availableRecoloringVertex2List.Add(color3);
                if (graph.ExistsEdge(new Edge(vertex2, mainVertex)))
                    availableRecoloringVertex2List.Remove(color1);
                availableRecoloringVertex2List.Add(color2);

                // Vertex3
                if (ColoredNeighbours(color1, vertex3).Count == 1)
                    availableRecoloringVertex3List.Add(color1);
                if (ColoredNeighbours(color2, vertex3).Count == 1)
                    availableRecoloringVertex3List.Add(color2);
                if (graph.ExistsEdge(new Edge(vertex3, mainVertex)))
                    availableRecoloringVertex3List.Remove(color1);
                availableRecoloringVertex3List.Add(color3);

                // There is no valid recoloring
                if (availableRecoloringVertex1List.Count == 0 || availableRecoloringVertex2List.Count == 0 || availableRecoloringVertex3List.Count == 0)
                    return false;

                // CSP
                int newColor1 = -1, newColor2 = -1, newColor3 = -1;
                foreach (int colorAvaliable1 in availableRecoloringVertex1List)
                {
                    newColor1 = colorAvaliable1;
                    bool deletedColor1InAvailableRecoloringVertex2, deletedColor1InAvailableRecoloringVertex3;

                    deletedColor1InAvailableRecoloringVertex2 = availableRecoloringVertex2List.Exists(x => x == newColor1);
                    deletedColor1InAvailableRecoloringVertex3 = availableRecoloringVertex3List.Exists(x => x == newColor1);
                    availableRecoloringVertex2List.Remove(newColor1);
                    availableRecoloringVertex3List.Remove(newColor1);

                    newColor2 = -1; newColor3 = -1;

                    foreach (int colorAvaliable2 in availableRecoloringVertex2List)
                    {
                        newColor2 = colorAvaliable2;
                        bool deletedColor2InAvailableRecoloringVertex3;
                        deletedColor2InAvailableRecoloringVertex3 = availableRecoloringVertex3List.Exists(x => x == newColor2);
                        availableRecoloringVertex3List.Remove(newColor2);

                        newColor3 = -1;

                        foreach (int colorAvaliable3 in availableRecoloringVertex3List)
                        {
                            newColor3 = colorAvaliable3;
                        }

                        if (newColor3 != -1)
                            break;

                        if (deletedColor2InAvailableRecoloringVertex3)
                            availableRecoloringVertex3List.Add(newColor2);
                    }

                    if (newColor2 != -1 && newColor3 != -1)
                        break;

                    if (deletedColor1InAvailableRecoloringVertex2)
                        availableRecoloringVertex2List.Add(newColor1);
                    if (deletedColor1InAvailableRecoloringVertex3)
                        availableRecoloringVertex3List.Add(newColor1);
                }

                if (newColor1 == -1 || newColor2 == -1 || newColor3 == -1)
                    return false;

                if (!recolor)
                    return true;

                ColorVertex(cutVertex, newColor1);
                ColorVertex(vertex2, newColor2);
                ColorVertex(vertex3, newColor3);

                return true;
            }

            /// <summary>
            /// For the vertex return the most used color by neighbor's neighbor
            /// If the availableRecoloringList is null throws GraphException
            /// If there is not available color return default color (= 0)
            /// </summary>
            /// <param name="availableColorList">list of available colors</param>
            /// <param name="vertex">vertex</param>
            /// <returns>the most used color by GetMostUsedColorNeighborsNeighbor's neighbor</returns>
            public int GetMostUsedColorNeighborsNeighbor(IVertexInterface vertex, List<int> availableColorList)
            {
                if (availableColorList == null)
                    throw new MyException.GraphException.GraphException("Invalid availableRecoloringList");

                if (availableColorList.Count() == 0)
                    return VertexExtended.GetDefaultColor();

                // Get optimal color
                Dictionary<int, int> colorsDictionary = new Dictionary<int, int>();
                List<IVertexInterface> neighborsVertexList = graph.Neighbours(vertex);

                foreach (int colorTemp in availableColorList)
                {
                    colorsDictionary.Add(colorTemp, 0);
                }

                // Set priorities for possible colors
                foreach (IVertexInterface neighborTemp in neighborsVertexList)
                {
                    foreach (IVertexInterface secondNeighbor in graph.Neighbours(neighborTemp))
                    {
                        if (secondNeighbor.GetColor() != VertexExtended.GetDefaultColor())
                        {
                            if (colorsDictionary.ContainsKey(secondNeighbor.GetColor()))
                            {
                                colorsDictionary[secondNeighbor.GetColor()]++;
                            }
                        }
                    }
                }

                // Get color with the highest priority
                int maxUsedColor = VertexExtended.GetDefaultColor();
                int countMaxUsedColor = int.MinValue;
                foreach (var record in colorsDictionary)
                {
                    if (countMaxUsedColor < record.Value)
                    {
                        countMaxUsedColor = record.Value;
                        maxUsedColor = record.Key;
                    }
                }

                // Test
                //Console.WriteLine("colorsDictionary");
                //foreach (var record in colorsDictionary)
                //{
                //    Console.WriteLine("Color: {0}, count: {1}", record.Key, record.Value);
                //}

                return maxUsedColor;
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
