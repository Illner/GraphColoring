﻿using System;
using System.IO;
using System.Threading;

namespace AI.GenerateGraphs
{
    class GenerateGraphsDatabase : GenerateGraphs
    {
        // Variable
        #region
        private Database.Database database;
        #endregion

        // Constructor
        #region
        /// <summary>
        /// Initialize GenerateGraphs
        /// </summary>
        /// <param name="writer">write generated graphs on the screen</param>
        /// <param name="clearDatabase">remove all records in the DB</param>
        public GenerateGraphsDatabase(bool writer = true, bool clearDatabase = false) : base(writer)
        {

            database = new Database.Database();

            while (database.GetConnectionState() == System.Data.ConnectionState.Connecting)
            {
                if (writer)
                    Console.WriteLine("Connecting ...");
                Thread.Sleep(10);
            }

            if (database.GetConnectionState() != System.Data.ConnectionState.Open)
                throw new MyException.GenerateGraphsException.GenerateGraphsDatabaseNotOpenException(database.GetConnectionState().ToString());

            if (clearDatabase)
                database.CleanDB();
        }
        #endregion

        // Method
        #region
        /// <summary>
        /// Generate graphs with count of vertices greater than or equal to minCount and less than or equal to maxCount
        /// For invalid minCount and maxCount returns GenerateGraphsInvalidArgumentsMinCountMaxCountException
        /// </summary>
        /// <param name="minCount">the lowest count of vertices</param>
        /// <param name="maxCount">the highest count of vertices</param>
        public override void Generate(int minCount, int maxCount)
        {
            if (minCount > maxCount)
                throw new MyException.GenerateGraphsException.GenerateGraphsInvalidArgumentsMinCountMaxCountException("MinCount: " + minCount + ", maxCount: " + maxCount);

            if (minCount < 1)
                throw new MyException.GenerateGraphsException.GenerateGraphsInvalidArgumentsMinCountMaxCountException("MinCount: " + minCount);

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
                    if (!graphExists)
                        database.InsertGraph(graph);
                    int ID_Graph = database.GetGraph(graph);
                    
                    // Algorithm
                    // Random
                    result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.RandomSequence.RandomSequence(graph), true);
                    database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequence, COUNTITERATIONSPROBABILITY, result.Item1, result.Item2);

                    // Largest first sequence
                    result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence(graph));
                    database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequence, result.Item1);

                    // Smallest last sequence
                    result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.SmallestLastSequence(graph));
                    database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequence, result.Item1);

                    // Random interchange
                    result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.RandomSequence.RandomSequence(graph, true), true);
                    database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequenceInterchange, COUNTITERATIONSPROBABILITY, result.Item1, result.Item2);

                    // Largest first sequence interchange
                    result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence(graph, true));
                    database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequenceInterchange, result.Item1);

                    // Smallest last sequence interchange
                    result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.SmallestLastSequence(graph, true));
                    database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchange, result.Item1);

                    // Connected sequential
                    result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.ConnectedSequential.ConnectedSequential(graph));
                    database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedSequential, result.Item1);

                    // Saturation largest first sequence
                    result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.SaturationLargestFirstSequence.SaturationLargestFirstSequence(graph));
                    database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.saturationLargestFirstSequence, result.Item1);

                    // Greedy independent set
                    result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.GreedyIndependentSet.GreedyIndependentSet(graph));
                    database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.greedyIndependentSet, result.Item1);

                    // Combination
                    result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.CombinationAlgorithm.CombinationAlgorithm(graph));
                    database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.combinationAlgorithm, result.Item1);

                    // Genetic
                    result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.GeneticAlgorithm.GeneticAlgorithm(graph), true);
                    database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.geneticAlgorithm, COUNTITERATIONSPROBABILITY, result.Item1, result.Item2);

                    // Illner

                    result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.IllnerAlgorithm.IllnerAlgorithm(graph));
                    database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.IllnerAlgorithm, result.Item1);

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

        /// <summary>
        /// Save data from the file to the DB
        /// If the file does not exist throws ReaderWriterFileDoesNotExist
        /// File path - GenerateGraphsFile.GetPath()
        /// </summary>
        public void SaveDataFromFileToDB()
        {
            string pathFile = GenerateGraphsFile.GetPath();

            if (!File.Exists(pathFile))
                throw new GraphColoring.MyException.ReaderWriterException.ReaderWriterFileDoesNotExist(pathFile);

            // Variable
            string line;
            int lineNumber = 0;
            int ID_Graph = 0;
            bool graphExists = false, error = false;

            int CountVertices, CountEdges, MaximumVertexDegree, MinimumVertexDegree, MedianVertexDegree, CountCutVertices, CountBridges, Girth;
            bool IsChordal, IsRegular, IsCyclic;
            double AverageVertexDegree;
            int[] VertexDegreeArray;
            GraphColoring.Graph.GraphClass.GraphClass.GraphClassEnum GraphClass;
            GraphColoring.Graph.GraphProperty.GraphProperty.EulerianGraphEnum EulerianGraph;

            StreamReader file = new StreamReader(pathFile);
            while ((line = file.ReadLine()) != null)
            {
                lineNumber++;

                // Graph
                if (line.StartsWith(GenerateGraphsFile.graphHeader))
                {
                    try
                    {
                        error = false;
                        
                        if (writer && ID_Graph != 0)
                        {
                            if (!graphExists)
                                Console.WriteLine("Added graph with ID {0}", ID_Graph);
                            else

                                Console.WriteLine("Updated graph with ID {0}", ID_Graph);
                        }

                        string[] lineArray = line.Split(' ');
                        if (lineArray.Length <= 15)
                            throw new ArgumentException();

                        // Fill variables
                        CountVertices = int.Parse(lineArray[1]);
                        CountEdges = int.Parse(lineArray[2]);
                        GraphClass = (GraphColoring.Graph.GraphClass.GraphClass.GraphClassEnum)Enum.Parse(typeof(GraphColoring.Graph.GraphClass.GraphClass.GraphClassEnum), lineArray[3]);
                        IsChordal = bool.Parse(lineArray[4]);
                        IsRegular = bool.Parse(lineArray[5]);
                        IsCyclic = bool.Parse(lineArray[6]);
                        EulerianGraph = (GraphColoring.Graph.GraphProperty.GraphProperty.EulerianGraphEnum)Enum.Parse(typeof(GraphColoring.Graph.GraphProperty.GraphProperty.EulerianGraphEnum), lineArray[7]);
                        MaximumVertexDegree = int.Parse(lineArray[8]);
                        MinimumVertexDegree = int.Parse(lineArray[9]);
                        AverageVertexDegree = double.Parse(lineArray[10]);
                        MedianVertexDegree = int.Parse(lineArray[11]);
                        CountCutVertices = int.Parse(lineArray[12]);
                        CountBridges = int.Parse(lineArray[13]);
                        Girth = int.Parse(lineArray[14]);
                        VertexDegreeArray = new int[lineArray.Length - 15];
                        for (int i = 0; i < VertexDegreeArray.Length; i++)
                        {
                            VertexDegreeArray[i] = int.Parse(lineArray[15 + i]);
                        }

                        if (VertexDegreeArray.Length != CountVertices)
                            throw new ArgumentException();

                        // DB
                        graphExists = database.ExistsGraph(CountVertices, CountEdges, GraphClass, IsChordal, IsRegular, IsCyclic, EulerianGraph,
                                                           MaximumVertexDegree, MinimumVertexDegree, AverageVertexDegree, MedianVertexDegree,
                                                           CountCutVertices, CountBridges, Girth, VertexDegreeArray);
                        if (!graphExists)
                            database.InsertGraph(CountVertices, CountEdges, GraphClass, IsChordal, IsRegular, IsCyclic, EulerianGraph,
                                                 MaximumVertexDegree, MinimumVertexDegree, AverageVertexDegree, MedianVertexDegree,
                                                 CountCutVertices, CountBridges, Girth, VertexDegreeArray);
                        ID_Graph = database.GetGraph(CountVertices, CountEdges, GraphClass, IsChordal, IsRegular, IsCyclic, EulerianGraph,
                                                     MaximumVertexDegree, MinimumVertexDegree, AverageVertexDegree, MedianVertexDegree,
                                                     CountCutVertices, CountBridges, Girth, VertexDegreeArray);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Can't parse this line [{0}] (invalid type): {1}", lineNumber, line);

                        error = true;
                        ID_Graph = 0;
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("Can't parse this line [{0}] (invalid count of parameters): {1}", lineNumber, line);

                        error = true;
                        ID_Graph = 0;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Something wrong with this line [{0}]: {1}", lineNumber, line);

                        error = true;
                        ID_Graph = 0;
                    }
                }
                // Algorithms
                else
                {
                    if (error || ID_Graph <= 0)
                        continue;

                    try
                    {
                        string[] lineArray = line.Split(' ');
                        if (lineArray.Length != 2 && lineArray.Length != 4)
                            throw new ArgumentException();

                        switch ((GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum)Enum.Parse(typeof(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum), lineArray[0], false))
                        {
                            case GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequence:
                                database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequence, int.Parse(lineArray[1]), int.Parse(lineArray[2]), int.Parse(lineArray[3]));
                                break;
                            case GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequence:
                                database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequence, int.Parse(lineArray[1]));
                                break;
                            case GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequence:
                                database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequence, int.Parse(lineArray[1]));
                                break;
                            case GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequenceInterchange:
                                database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequenceInterchange, int.Parse(lineArray[1]), int.Parse(lineArray[2]), int.Parse(lineArray[3]));
                                break;
                            case GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequenceInterchange:
                                database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequenceInterchange, int.Parse(lineArray[1]));
                                break;
                            case GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchange:
                                database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchange, int.Parse(lineArray[1]));
                                break;
                            case GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedSequential:
                                database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedSequential, int.Parse(lineArray[1]));
                                break;
                            case GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.saturationLargestFirstSequence:
                                database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.saturationLargestFirstSequence, int.Parse(lineArray[1]));
                                break;
                            case GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.greedyIndependentSet:
                                database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.greedyIndependentSet, int.Parse(lineArray[1]));
                                break;
                            case GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.combinationAlgorithm:
                                database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.combinationAlgorithm, int.Parse(lineArray[1]));
                                break;
                            case GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.geneticAlgorithm:
                                database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.geneticAlgorithm, int.Parse(lineArray[1]), int.Parse(lineArray[2]), int.Parse(lineArray[3]));
                                break;
                            default:
                                throw new MyException.GenerateGraphsException.GenerateGraphsAlgorithmDoesNotExistException(lineArray[0]);
                        }

                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("Can't parse this line [{0}] (invalid count of parameters): {1}", lineNumber, line);
                    }
                    catch (MyException.GenerateGraphsException.GenerateGraphsAlgorithmDoesNotExistException)
                    {
                        Console.WriteLine("Algorithm does not exist: {0}", line);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Something wrong with this line [{0}] : {1}", lineNumber, line);
                    }
                }
            }
            
            if (writer && ID_Graph != 0)
            {
                if (!graphExists)
                    Console.WriteLine("Added graph with ID {0}", ID_Graph);
                else

                    Console.WriteLine("Updated graph with ID {0}", ID_Graph);
            }
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