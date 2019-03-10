using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring.GraphColoringAlgorithm.IllnerAlgorithm
{
    public class IllnerAlgorithm : GraphColoringAlgorithm
    {
        // Variable
        #region
        
        #endregion

        // Constructor
        #region
        public IllnerAlgorithm(Graph.IGraphInterface graph) : base(graph)
        {
            name = "Illner's algorithm";
            timeComplexity = TimeComplexityEnum.undefined;
        }
        #endregion

        // Method
        #region
        /// <summary>
        /// Color a graph
        /// </summary>
        override
        public void Color()
        {
            // Variable
            int countVertices = graph.GetRealCountVertices();
            int maxDegreeVertex = graph.GetGraphProperty().GetDegreeSequenceInt(false).Max();
            int maxCountOfNeighborsOfNeighbors = int.MinValue;
            Graph.IVertexInterface startingVertex = null;
            int sumOfNeighborsOfMyNeighbors;
            Graph.IVertexInterface[] mappingVertexArray = new Graph.IVertexInterface[countVertices];
            MyDataStructure.FibonacciHeap fibonacciHeap = new MyDataStructure.FibonacciHeap(countVertices);
            Dictionary<Graph.IVertexInterface, int> mappingVertexDictionary = new Dictionary<Graph.IVertexInterface, int>();

            if (coloredGraph.GetIsInitializedColoredGraph())
                throw new MyException.GraphException.ColoredGraphAlreadyInitializedException();
            
            // Graph is not connected
            if (!graph.GetGraphProperty().GetIsConnected())
                throw new MyException.GraphException.GraphIsNotConnected(graph.ToString());

            coloredGraph.ResetColors();
            
            // Initilize mapping
            int help = 0;
            foreach (Graph.IVertexInterface vertex in graph.AllVertices())
            {
                // Mapping
                mappingVertexDictionary.Add(vertex, help);
                mappingVertexArray[help] = vertex;
                help++;
            }

            // Find starting vertex
            foreach (var record in graph.GetGraphProperty().GetDegreeSequence(false))
            {
                if (record.Value == maxDegreeVertex)
                {
                    sumOfNeighborsOfMyNeighbors = SumOfNeighborsOfMyNeighbors(record.Key);
                    if (maxCountOfNeighborsOfNeighbors < sumOfNeighborsOfMyNeighbors)
                    {
                        maxCountOfNeighborsOfNeighbors = sumOfNeighborsOfMyNeighbors;
                        startingVertex = record.Key;
                    }
                }
            }

            Graph.IVertexInterface actualVertex = startingVertex;
            List<Graph.IVertexInterface> neighborsVertexList;
            Dictionary<int, int> colorsDictionary;
            for (int i = 0; i < countVertices; i++)
            {
                colorsDictionary = new Dictionary<int, int>();
                neighborsVertexList = graph.Neighbours(actualVertex);

                // Get all colors which are used in the graph
                foreach (int color in coloredGraph.UsedColors())
                {
                    colorsDictionary.Add(color, 0);
                }

                // Get all possible colors which are used in the graph
                // Add uncolored neighbors to the heap
                foreach (Graph.IVertexInterface neighbor in neighborsVertexList)
                {
                    if (neighbor.GetColor() != Graph.VertexExtended.GetDefaultColor())
                    {
                        colorsDictionary.Remove(neighbor.GetColor());
                    }
                    else
                    {
                        if (!fibonacciHeap.ElementExists(mappingVertexDictionary[neighbor]))
                        {
                            fibonacciHeap.Insert(mappingVertexDictionary[neighbor], countVertices - graph.CountNeighbours(neighbor));
                        }
                    }
                }

                // Color the vertex with a new color
                if (colorsDictionary.Count == 0)
                {
                    int color = coloredGraph.TryChangeColoring(actualVertex, coloredGraph.GreedyColoring(actualVertex));
                    coloredGraph.ColorVertex(actualVertex, color); // coloredGraph.GreedyColoring(actualVertex));

                    if (fibonacciHeap.GetCountNodes() != 0)
                        actualVertex = mappingVertexArray[fibonacciHeap.ExtractMin().GetIdentifier()];
                    continue;
                }


                // Set priorities for possible colors
                foreach (Graph.IVertexInterface neighbor in neighborsVertexList)
                {
                    foreach (Graph.IVertexInterface secondNeighbor in graph.Neighbours(neighbor))
                    {
                        if (secondNeighbor.GetColor() != Graph.VertexExtended.GetDefaultColor())
                        {
                            if (colorsDictionary.ContainsKey(secondNeighbor.GetColor()))
                            {
                                colorsDictionary[secondNeighbor.GetColor()]++;
                            }
                        }
                    }
                }

                // Get color with the highest priority
                int maxUsedColor = Graph.VertexExtended.GetDefaultColor();
                int countMaxUsedColor = int.MinValue;
                foreach (var record in colorsDictionary)
                {
                    if (countMaxUsedColor < record.Value)
                    {
                        countMaxUsedColor = record.Value;
                        maxUsedColor = record.Key;
                    }
                }

                coloredGraph.ColorVertex(actualVertex, maxUsedColor);

                if (fibonacciHeap.GetCountNodes() != 0)
                    actualVertex = mappingVertexArray[fibonacciHeap.ExtractMin().GetIdentifier()];
            }

            bool isColored = coloredGraph.InitializeColoredGraph();

            if (!isColored)
                throw new MyException.GraphColoringAlgorithmException.AlgorithmGraphIsNotColored();
        }

        /// <summary>
        /// Return a count of neighbors of my neighbors
        /// </summary>
        /// <param name="vertex">vertex</param>
        /// <returns>count of neighbors of my neighbors</returns>
        private int SumOfNeighborsOfMyNeighbors(Graph.IVertexInterface vertex)
        {
            // Variable
            int sum = 0;
            List<Graph.IVertexInterface> neighbors = graph.Neighbours(vertex);


            foreach (Graph.IVertexInterface neighbor in neighbors)
            {
                sum = graph.CountNeighbours(neighbor);
            }

            return sum;
        }
        #endregion
    }
}
