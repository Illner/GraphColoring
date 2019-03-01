using System;
using System.Threading;

namespace AI.GenerateGraphs
{
    class GenerateGraphs
    {
        // Variable
        #region
        private bool writer;
        private Database.Database database;
        private const int COUNTITERATIONSPROBABILITY = 10;
        
        GraphColoring.Graph.IGraphInterface graph;
        private GraphColoring.GenerateGraph.ErdosRenyiModel.ErdosRenyiModel erdosRenyiModel;
        #endregion

        // Constructor
        #region
        /// <summary>
        /// Initialize GenerateGraphs
        /// </summary>
        /// <param name="writer">write generated graphs on the screen</param>
        /// <param name="clearDatabase">remove all records in the DB</param>
        public GenerateGraphs(bool writer = true, bool clearDatabase = false)
        {
            database = new Database.Database();
            this.writer = writer;

            while (database.GetConnectionState() == System.Data.ConnectionState.Connecting)
            {
                if (writer)
                    Console.WriteLine("Connecting ...");
                Thread.Sleep(10);
            }

            if (database.GetConnectionState() != System.Data.ConnectionState.Open)
                throw new GraphColoring.MyException.DatabaseException.DatabaseNotOpenException(database.GetConnectionState().ToString());

            if (clearDatabase)
                database.CleanDB();
        }
        #endregion

        // Method
        #region
        /// <summary>
        /// Generate graphs with count of vertices greater than or equal to minCount and less than or equal to maxCount
        /// </summary>
        /// <param name="minCount">the lowest count of vertices</param>
        /// <param name="maxCount">the highest count of vertices</param>
        public void Generate(int minCount, int maxCount)
        {
            if (minCount > maxCount)
                throw new GraphColoring.MyException.DatabaseException.DatabaseInvalidArgumentsMinCountMaxCountException("MinCount: " + minCount + ", maxCount: " + maxCount);

            if (minCount < 1)
                throw new GraphColoring.MyException.DatabaseException.DatabaseInvalidArgumentsMinCountMaxCountException("MinCount: " + minCount);

            // Variable
            bool graphExists;
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
                        erdosRenyiModel = new GraphColoring.GenerateGraph.ErdosRenyiModel.ErdosRenyiModel(countVertices, GraphColoring.GenerateGraph.ErdosRenyiModel.ErdosRenyiModel.ErdosRenyiModelProbabilityEnum.cLogNDividedByN);
                        graph = erdosRenyiModel.GenerateGraph();
                    }
                    while (!graph.GetGraphProperty().GetIsConnected()); // || database.ExistsGraph(graph));

                    graphExists = database.ExistsGraph(graph);
                    int ID_Graph = database.InsertGraph(graph);

                    // Algorithm
                    // Random
                    result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.RandomSequence.RandomSequence(graph), true);
                    if (!graphExists)
                        database.InsertGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequence, COUNTITERATIONSPROBABILITY, result.Item1, result.Item2);
                    else
                        database.UpdateGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequence, COUNTITERATIONSPROBABILITY, result.Item1, result.Item2);

                    // Largest first sequence
                    result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence(graph));
                    if (!graphExists)
                        database.InsertGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequence, result.Item1);
                    else
                        database.UpdateGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequence, result.Item1);

                    // Smallest last sequence
                    result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.SmallestLastSequence(graph));
                    if (!graphExists)
                        database.InsertGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequence, result.Item1);
                    else
                        database.UpdateGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequence, result.Item1);

                    // Random interchange
                    result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.RandomSequence.RandomSequence(graph, true), true);
                    if (!graphExists)
                        database.InsertGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequenceInterchange, COUNTITERATIONSPROBABILITY, result.Item1, result.Item2);
                    else
                        database.UpdateGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequenceInterchange, COUNTITERATIONSPROBABILITY, result.Item1, result.Item2);

                    // Largest first sequence interchange
                    result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence(graph, true));
                    if (!graphExists)
                        database.InsertGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequenceInterchange, result.Item1);
                    else
                        database.UpdateGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequenceInterchange, result.Item1);

                    // Smallest last sequence interchange
                    result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.SmallestLastSequence(graph, true));
                    if (!graphExists)
                        database.InsertGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchange, result.Item1);
                    else
                        database.UpdateGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchange, result.Item1);

                    // Connected sequential
                    result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.ConnectedSequential.ConnectedSequential(graph));
                    if (!graphExists)
                        database.InsertGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedSequential, result.Item1);
                    else
                        database.UpdateGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedSequential, result.Item1);

                    // Saturation largest first sequence
                    result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.SaturationLargestFirstSequence.SaturationLargestFirstSequence(graph));
                    if (!graphExists)
                        database.InsertGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.saturationLargestFirstSequence, result.Item1);
                    else
                        database.UpdateGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.saturationLargestFirstSequence, result.Item1);
                    
                    // Greedy independent set
                    result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.GreedyIndependentSet.GreedyIndependentSet(graph));
                    if (!graphExists)
                        database.InsertGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.greedyIndependentSet, result.Item1);
                    else
                        database.UpdateGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.greedyIndependentSet, result.Item1);

                    // Combination
                    result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.CombinationAlgorithm.CombinationAlgorithm(graph));
                    if (!graphExists)
                        database.InsertGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.combinationAlgorithm, result.Item1);
                    else
                        database.UpdateGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.combinationAlgorithm, result.Item1);
                    
                    // Genetic
                    result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.GeneticAlgorithm.GeneticAlgorithm(graph), true);
                    if (!graphExists)
                        database.InsertGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.geneticAlgortihm, COUNTITERATIONSPROBABILITY, result.Item1, result.Item2);
                    else
                        database.UpdateGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.geneticAlgortihm, COUNTITERATIONSPROBABILITY, result.Item1, result.Item2);

                    if (writer)
                    {
                        if (!graphExists)
                            Console.WriteLine("Added graph with ID {0} - countVertices: {1}, iteration: {2}/{3}", ID_Graph, countVertices, (iteration + 1), countIterations);
                        else

                            Console.WriteLine("Updated graph with ID {0} - countVertices: {1}, iteration: {2}/{3}", ID_Graph, countVertices, (iteration + 1), countIterations);
                    }
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
                    return (int)Math.Pow(countVertices, 1);
            }
        }
        
        /// <summary>
        /// Color a graph with an algorithm
        /// </summary>
        /// <param name="algorithm">algorithm</param>
        /// <param name="probability">probability algorithm?</param>
        /// <returns>(minColors, maxColors), for non-probability algorithm minColors = maxColors</returns>
        private Tuple<int, int> ColorGraph(GraphColoring.GraphColoringAlgorithm.IGraphColoringAlgorithmInterface algorithm, bool probability = false)
        {
            if (graph.GetColoredGraph().GetIsInitializedColoredGraph())
                graph.GetColoredGraph().DeinitializationColoredGraph();

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

                graph.GetColoredGraph().DeinitializationColoredGraph();
            }

            return new Tuple<int, int>(minColors, maxColors);
        }
        #endregion

        // Property
        #region
        /// <summary>
        /// Return the DB
        /// </summary>
        /// <returns>DB</returns>
        public Database.Database GetDatabase()
        {
            return database;
        }
        #endregion
    }
}
