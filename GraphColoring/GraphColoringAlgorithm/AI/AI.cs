using System;
using System.Linq;

namespace GraphColoring.GraphColoringAlgorithm.AI
{
    class AI : GraphColoringAlgorithm
    {
        // Variable
        #region
        GraphColoringAlgorithmEnum algorithmEnum;
        IGraphColoringAlgorithmInterface algorithm;
        #endregion

        // Constructor
        #region
        public AI(Graph.IGraphInterface graph) : base(graph)
        {
            name = "AI";
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
            if (coloredGraph.GetIsInitializedColoredGraph())
                throw new MyException.GraphException.ColoredGraphAlreadyInitializedException();

            bool isColored;
            coloredGraph.ResetColors();

            // Try optimal color
            if (Optimal.Optimal.TryOptimalColorInPolynomalTime(graph))
            {
                return;
            }

            // VertexDegree
            int vertexDegree0 = 0, vertexDegree1 = 0, vertexDegree2 = 0, vertexDegree3 = 0, vertexDegree4 = 0, vertexDegree5 = 0, vertexDegree6 = 0, vertexDegree7 = 0, vertexDegree8 = 0, vertexDegree9 = 0, vertexDegree10 = 0;
            foreach(var degree in graph.GetGraphProperty().GetDegreeSequenceInt(false))
            {
                switch (degree)
                {
                    case 0:
                        vertexDegree0++;
                        break;
                    case 1:
                        vertexDegree1++;
                        break;
                    case 2:
                        vertexDegree2++;
                        break;
                    case 3:
                        vertexDegree3++;
                        break;
                    case 4:
                        vertexDegree4++;
                        break;
                    case 5:
                        vertexDegree5++;
                        break;
                    case 6:
                        vertexDegree6++;
                        break;
                    case 7:
                        vertexDegree7++;
                        break;
                    case 8:
                        vertexDegree8++;
                        break;
                    case 9:
                        vertexDegree9++;
                        break;
                    case 10:
                        vertexDegree10++;
                        break;
                }
            }

            GraphData graphData = new GraphData()
            {
                ID_GraphClass = (int)graph.GetGraphProperty().GetGraphClass() + 1,
                ID_EulerianGraph = (int)graph.GetGraphProperty().GetIsEulerian() + 1,
                IsRegular = graph.GetGraphProperty().GetIsRegular(),
                IsCyclic = graph.GetGraphProperty().GetIsCyclic(),
                IsChordal = graph.GetGraphProperty().GetIsChordal(),
                CountVertices = graph.GetGraphProperty().GetCountVertices(),
                CountEdges = graph.GetGraphProperty().GetCountEdges(),
                CountCutVertices = graph.GetGraphProperty().GetCutVertices().Count(),
                CountBridges = graph.GetGraphProperty().GetBridges().Count(),
                Girth = graph.GetGraphProperty().GetGirth(),
                Dense = (2 * (float)graph.GetGraphProperty().GetCountEdges()) / (graph.GetGraphProperty().GetCountVertices() * (graph.GetGraphProperty().GetCountVertices() - 1)),
                MedianVertexDegree = graph.GetGraphProperty().GetMedianVertexDegree(),  // must be first because sort
                MinimumVertexDegree = graph.GetGraphProperty().GetMinimumVertexDegree(),
                MaximumVertexDegree = graph.GetGraphProperty().GetMaximumVertexDegree(),
                AverageVertexDegree = (float)graph.GetGraphProperty().GetAverageVertexDegree(),
                VertexDegree0 = vertexDegree0,
                VertexDegree1 = vertexDegree1,
                VertexDegree2 = vertexDegree2,
                VertexDegree3 = vertexDegree3,
                VertexDegree4 = vertexDegree4,
                VertexDegree5 = vertexDegree5,
                VertexDegree6 = vertexDegree6,
                VertexDegree7 = vertexDegree7,
                VertexDegree8 = vertexDegree8,
                VertexDegree9 = vertexDegree9,
                VertexDegree10 = vertexDegree10
            };

            // Select "the best" algorithm
            algorithmEnum =  AIAlgorithm.ChooseAlgorithm(graphData);
            Console.WriteLine("AI: " + graph.GetName() + " - " + algorithmEnum);
            
            switch (algorithmEnum)
            {
                case GraphColoringAlgorithmEnum.randomSequence:
                    algorithm = new SequenceAlgorithm.RandomSequence.RandomSequence(graph);
                    break;
                case GraphColoringAlgorithmEnum.largestFirstSequence:
                    algorithm = new SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence(graph);
                    break;
                case GraphColoringAlgorithmEnum.smallestLastSequence:
                    algorithm = new SequenceAlgorithm.SmallestLastSequence.SmallestLastSequence(graph);
                    break;
                case GraphColoringAlgorithmEnum.randomSequenceInterchange:
                    algorithm = new SequenceAlgorithm.RandomSequence.RandomSequence(graph, true);
                    break;
                case GraphColoringAlgorithmEnum.largestFirstSequenceInterchange:
                    algorithm = new SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence(graph, true);
                    break;
                case GraphColoringAlgorithmEnum.smallestLastSequenceInterchange:
                    algorithm = new SequenceAlgorithm.SmallestLastSequence.SmallestLastSequence(graph, true);
                    break;
                case GraphColoringAlgorithmEnum.connectedSequential:
                    algorithm = new SequenceAlgorithm.ConnectedSequential.ConnectedSequential(graph);
                    break;
                case GraphColoringAlgorithmEnum.saturationLargestFirstSequence:
                    algorithm = new SaturationLargestFirstSequence.SaturationLargestFirstSequence(graph);
                    break;
                case GraphColoringAlgorithmEnum.greedyIndependentSet:
                    algorithm = new GreedyIndependentSet.GreedyIndependentSet(graph);
                    break;
                case GraphColoringAlgorithmEnum.geneticAlgorithm:
                    algorithm = new GeneticAlgorithm.GeneticAlgorithm(graph);
                    break;
                case GraphColoringAlgorithmEnum.combinationAlgorithm:
                    algorithm = new CombinationAlgorithm.CombinationAlgorithm(graph);
                    break;
                default:
                    throw new MyException.GraphColoringAlgorithmException.AlgorithmDoesntExist(algorithmEnum.ToString());
            }

            algorithm.Color();

            isColored = coloredGraph.GetIsInitializedColoredGraph();

            if (!isColored)
                throw new MyException.GraphColoringAlgorithmException.AlgorithmGraphIsNotColored();
        }
        #endregion

        // Property
        #region

        #endregion
    }
}
