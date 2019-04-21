using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring.GraphColoringAlgorithm.ConnectedLargestFirst
{
    public class ConnectedLargestFirst : GraphColoringAlgorithm
    {
        // Variable
        #region
        private GraphColoringAlgorithInterchangeEnum interchangeEnum;
        #endregion

        // Constructor
        #region
        public ConnectedLargestFirst(Graph.IGraphInterface graph, GraphColoringAlgorithInterchangeEnum interchangeEnum = GraphColoringAlgorithInterchangeEnum.none) : base(graph)
        {
            // Interchange
            this.interchangeEnum = interchangeEnum;

            switch (interchangeEnum)
            {
                case GraphColoringAlgorithInterchangeEnum.none:
                    name = "Connected largest first sequence algorithm";
                    break;
                case GraphColoringAlgorithInterchangeEnum.interchange:
                    name = "Connected largest first interchange algorithm";
                    break;
                case GraphColoringAlgorithInterchangeEnum.interchangeExtended:
                    name = "Connected largest first interchange extended algorithm";
                    break;
                case GraphColoringAlgorithInterchangeEnum.interchangeExtendedK3:
                    name = "Connected largest first interchange extended with K3 algorithm";
                    break;
            }

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
            for (int i = 0; i < countVertices; i++)
            {
                foreach (Graph.IVertexInterface neighbor in graph.Neighbours(actualVertex))
                {
                    if (neighbor.GetColor() == Graph.VertexExtended.GetDefaultColor())
                    {
                        if (!fibonacciHeap.ElementExists(mappingVertexDictionary[neighbor]))
                        {
                            fibonacciHeap.Insert(mappingVertexDictionary[neighbor], countVertices - graph.CountNeighbours(neighbor));
                        }
                    }
                }

                List<int> availableColorList = coloredGraph.UsedColors();
                availableColorList = availableColorList.Except(coloredGraph.ColorsNeighbours(actualVertex)).ToList();

                int color = coloredGraph.GetMostUsedColorNeighborsNeighbor(actualVertex, availableColorList);

                if (color != Graph.VertexExtended.GetDefaultColor())
                    coloredGraph.ColorVertex(actualVertex, color);
                else
                {
                    switch (interchangeEnum)
                    {
                        case GraphColoringAlgorithInterchangeEnum.none:
                            coloredGraph.ColorVertex(actualVertex, coloredGraph.GreedyColoring(actualVertex));
                            break;
                        case GraphColoringAlgorithInterchangeEnum.interchange:
                            coloredGraph.TryChangeColoring(actualVertex, coloredGraph.GreedyColoring(actualVertex));
                            break;
                        case GraphColoringAlgorithInterchangeEnum.interchangeExtended:
                            coloredGraph.TryChangeColoringExtended(actualVertex, coloredGraph.GreedyColoring(actualVertex), false);
                            break;
                        case GraphColoringAlgorithInterchangeEnum.interchangeExtendedK3:
                            coloredGraph.TryChangeColoringExtended(actualVertex, coloredGraph.GreedyColoring(actualVertex), true);
                            break;
                    }
                }

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
