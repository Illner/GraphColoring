using System;
using System.Threading;

namespace GraphColoring.Database
{
    class GenerateGraphs
    {
        // Variable
        #region
        private Database database;
        private bool writer;
        private const int COUNTITERATIONSPROBABILITY = 10;

        // Graph, GenerateGraph and Algorithms
        Graph.IGraphInterface graph;
        private GenerateGraph.ErdosRenyiModel.ErdosRenyiModel erdosRenyiModel;
        #endregion

        // Constructor
        #region
        public GenerateGraphs(bool writer = false, bool clearDatabase = false)
        {
            database = new Database();
            this.writer = writer;

            while (database.GetConnectionState() == System.Data.ConnectionState.Connecting)
            {
                if (writer)
                    Console.WriteLine("Connecting ...");
                Thread.Sleep(10);
            }

            if (database.GetConnectionState() != System.Data.ConnectionState.Open)
                throw new MyException.DatabaseException.DatabaseNotOpenException(database.GetConnectionState().ToString());

            if (clearDatabase)
                database.CleanDB();
        }
        #endregion

        // Method
        #region
        public void Generate(int minCount, int maxCount)
        {
            if (minCount > maxCount)
                throw new MyException.DatabaseException.DatabaseInvalidArgumentsMinCountMaxCountException("MinCount: " + minCount + ", maxCount: " + maxCount);

            if (minCount < 1)
                throw new MyException.DatabaseException.DatabaseInvalidArgumentsMinCountMaxCountException("MinCount: " + minCount);

            // Variable
            int countIterations;
            Tuple<int, int> result;

            for (int countVertices = minCount; countVertices <= maxCount; countVertices++)
            {
                countIterations = GetCountIterations(countVertices);

                if (writer)
                    Console.WriteLine("-------------------------");

                for (int iteration = 0; iteration < countIterations; iteration++)
                {
                    // Generate graph
                    do
                    {
                        erdosRenyiModel = new GenerateGraph.ErdosRenyiModel.ErdosRenyiModel(countVertices);
                        graph = erdosRenyiModel.GenerateGraph();
                    }
                    while (!graph.GetGraphProperty().GetIsConnected() || database.ExistsGraph(graph));

                    int ID_Graph = database.InsertGraph(graph);

                    // Algorithm
                    // Random
                    result = ColorGraph(new GraphColoringAlgorithm.SequenceAlgorithm.RandomSequence.RandomSequence(graph), true);
                    database.InsertGraphColoring(ID_Graph, GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequence, COUNTITERATIONSPROBABILITY, result.Item1, result.Item2);
                    
                    // Largest first sequence
                    result = ColorGraph(new GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence(graph));
                    database.InsertGraphColoring(ID_Graph, GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequence, result.Item1);
                    
                    // Smallest last sequence
                    result = ColorGraph(new GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.SmallestLastSequence(graph));
                    database.InsertGraphColoring(ID_Graph, GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequence, result.Item1);
                    
                    // Random interchange
                    result = ColorGraph(new GraphColoringAlgorithm.SequenceAlgorithm.RandomSequence.RandomSequence(graph, true), true);
                    database.InsertGraphColoring(ID_Graph, GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequenceInterchange, COUNTITERATIONSPROBABILITY, result.Item1, result.Item2);
                    
                    // Largest first sequence interchange
                    result = ColorGraph(new GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence(graph, true));
                    database.InsertGraphColoring(ID_Graph, GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequenceInterchange, result.Item1);
                    
                    // Smallest last sequence interchange
                    result = ColorGraph(new GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.SmallestLastSequence(graph, true));
                    database.InsertGraphColoring(ID_Graph, GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchange, result.Item1);
                    
                    // Connected sequential
                    result = ColorGraph(new GraphColoringAlgorithm.SequenceAlgorithm.ConnectedSequential.ConnectedSequential(graph));
                    database.InsertGraphColoring(ID_Graph, GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedSequential, result.Item1);
                    
                    // Saturation largest first sequence
                    result = ColorGraph(new GraphColoringAlgorithm.SaturationLargestFirstSequence.SaturationLargestFirstSequence(graph));
                    database.InsertGraphColoring(ID_Graph, GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.saturationLargestFirstSequence, result.Item1);
                    
                    // Greedy independent set
                    result = ColorGraph(new GraphColoringAlgorithm.GreedyIndependentSet.GreedyIndependentSet(graph));
                    database.InsertGraphColoring(ID_Graph, GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.greedyIndependentSet, result.Item1);
                    
                    // Combination
                    result = ColorGraph(new GraphColoringAlgorithm.CombinationAlgorithm.CombinationAlgorithm(graph));
                    database.InsertGraphColoring(ID_Graph, GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.combinationAlgorithm, result.Item1);
                    
                    // Genetic
                    result = ColorGraph(new GraphColoringAlgorithm.GeneticAlgorithm.GeneticAlgorithm(graph), true);
                    database.InsertGraphColoring(ID_Graph, GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.geneticAlgortihm, COUNTITERATIONSPROBABILITY, result.Item1, result.Item2);
                    
                    if (writer)
                        Console.WriteLine("Add graph with ID {0} - countVertices: {1}, iteration: {2}/{3}", ID_Graph, countVertices, (iteration + 1), countIterations);
                }
            }
        }

        private int GetCountIterations(int countVertices)
        {
            switch (countVertices)
            {
                case 1:
                    return 1;
                case 2:
                    return 1;
                case 3:
                    return 2;
                case 4:
                    return 4;
                case 5:
                    return 11;
                case 6:
                    return 34;
                default:
                    return (int)Math.Pow(countVertices, 2);
            }
        }
        
        /// <returns>(MinColors, MaxColors)</returns>
        private Tuple<int, int> ColorGraph(GraphColoringAlgorithm.IGraphColoringAlgorithmInterface algorithm, bool probability = false)
        {
            if (graph.GetColoredGraph().GetIsInicializedColoredGraph())
                graph.GetColoredGraph().DeinicializationColoredGraph();

            // Variable
            int countIterations = 1;
            int colors, minColors = int.MaxValue, maxColors = int.MinValue;

            if (probability)
                countIterations = COUNTITERATIONSPROBABILITY;

            for (int i = 0; i < countIterations; i++)
            {
                algorithm.Color();
                colors = graph.GetColoredGraph().GetCountUsedColors();

                if (colors < minColors)
                    minColors = colors;

                if (colors > maxColors)
                    maxColors = colors;

                graph.GetColoredGraph().DeinicializationColoredGraph();
            }

            return new Tuple<int, int>(minColors, maxColors);
        }
        #endregion

        // Property
        #region
        public Database GetDatabase()
        {
            return database;
        }
        #endregion
    }
}
