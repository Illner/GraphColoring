using System;
using System.IO;
using System.Text;
using System.Threading;

namespace GraphColoringConsole.GenerateGraphs
{
    class GenerateGraphsDatabase : GenerateGraphs
    {
        #region Variable
        private Database.Database database;
        #endregion
        
        #region Constructor
        /// <summary>
        /// Initialize GenerateGraphs
        /// </summary>
        /// <param name="writer">write generated graphs on the screen</param>
        /// <param name="clearDatabase">remove all records in the DB</param>
        public GenerateGraphsDatabase(string databaseLocation, string databaseName, string databaseUserName, string databasePassword, bool writer = true, bool clearDatabase = false, int constant = 1, int exponent = 1, bool useGeneticAlgorithm2 = true, bool useInterchangeExtendedK3 = true) : base(constant, exponent, writer, useGeneticAlgorithm2, useInterchangeExtendedK3)
        {

            database = new Database.Database(databaseLocation, databaseName, databaseUserName, databasePassword);

            while (database.GetConnectionState() == System.Data.ConnectionState.Connecting)
            {
                if (writer)
                    Console.WriteLine("Connecting ...");
                Thread.Sleep(10);
            }

            if (database.GetConnectionState() != System.Data.ConnectionState.Open)
                throw new MyException.DatabaseException.DatabaseException(database.GetConnectionState().ToString());

            if (clearDatabase)
                database.CleanDB();
        }
        #endregion
        
        #region Method
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
                    try
                    {
                        result = colorGraphs.ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.RandomSequence.RandomSequence(graph), true);
                        database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequence, colorGraphs.GetCountIterationsProbability(), result.Item1, result.Item2);
                    }
                    catch (MyException.DatabaseException.DatabaseAlgorithmDoesNotExistException)
                    {
                        Console.WriteLine("Algorithm does not exist in DB: {0}", GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequence);
                    }

                    // Random interchange
                    try
                    {
                        result = colorGraphs.ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.RandomSequence.RandomSequence(graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchange), true);
                        database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequenceInterchange, colorGraphs.GetCountIterationsProbability(), result.Item1, result.Item2);
                    }
                    catch (MyException.DatabaseException.DatabaseAlgorithmDoesNotExistException)
                    {
                        Console.WriteLine("Algorithm does not exist in DB: {0}", GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequenceInterchange);
                    }

                    // Random interchangeExtended
                    try
                    {
                        result = colorGraphs.ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.RandomSequence.RandomSequence(graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtended), true);
                        database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequenceInterchangeExtended, colorGraphs.GetCountIterationsProbability(), result.Item1, result.Item2);
                    }
                    catch (MyException.DatabaseException.DatabaseAlgorithmDoesNotExistException)
                    {
                        Console.WriteLine("Algorithm does not exist in DB: {0}", GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequenceInterchangeExtended);
                    }

                    // Random interchangeExtended with K3
                    try
                    {
                        if (useInterchangeExtendedK3)
                        {
                            result = colorGraphs.ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.RandomSequence.RandomSequence(graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtendedK3), true);
                            database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequenceInterchangeExtendedK3, colorGraphs.GetCountIterationsProbability(), result.Item1, result.Item2);
                        }
                    }
                    catch (MyException.DatabaseException.DatabaseAlgorithmDoesNotExistException)
                    {
                        Console.WriteLine("Algorithm does not exist in DB: {0}", GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequenceInterchangeExtendedK3);
                    }

                    // Largest first sequence
                    try
                    {
                        result = colorGraphs.ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence(graph));
                        database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequence, result.Item1);
                    }
                    catch (MyException.DatabaseException.DatabaseAlgorithmDoesNotExistException)
                    {
                        Console.WriteLine("Algorithm does not exist in DB: {0}", GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequence);
                    }

                    // Largest first sequence interchange
                    try
                    {
                        result = colorGraphs.ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence(graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchange));
                        database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequenceInterchange, result.Item1);
                    }
                    catch (MyException.DatabaseException.DatabaseAlgorithmDoesNotExistException)
                    {
                        Console.WriteLine("Algorithm does not exist in DB: {0}", GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequenceInterchange);
                    }

                    // Largest first sequence interchangeExtended
                    try
                    {
                        result = colorGraphs.ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence(graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtended));
                        database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequenceInterchangeExtended, result.Item1);
                    }
                    catch (MyException.DatabaseException.DatabaseAlgorithmDoesNotExistException)
                    {
                        Console.WriteLine("Algorithm does not exist in DB: {0}", GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequenceInterchangeExtended);
                    }

                    // Largest first sequence interchangeExtended with K3
                    try
                    {
                        if (useInterchangeExtendedK3)
                        {
                            result = colorGraphs.ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence(graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtendedK3));
                            database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequenceInterchangeExtendedK3, result.Item1);
                        }
                    }
                    catch (MyException.DatabaseException.DatabaseAlgorithmDoesNotExistException)
                    {
                        Console.WriteLine("Algorithm does not exist in DB: {0}", GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequenceInterchangeExtendedK3);
                    }

                    // Smallest last sequence
                    try
                    {
                        result = colorGraphs.ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.SmallestLastSequence(graph));
                        database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequence, result.Item1);
                    }
                    catch (MyException.DatabaseException.DatabaseAlgorithmDoesNotExistException)
                    {
                        Console.WriteLine("Algorithm does not exist in DB: {0}", GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequence);
                    }

                    // Smallest last sequence interchange
                    try
                    {
                        result = colorGraphs.ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.SmallestLastSequence(graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchange));
                        database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchange, result.Item1);
                    }
                    catch (MyException.DatabaseException.DatabaseAlgorithmDoesNotExistException)
                    {
                        Console.WriteLine("Algorithm does not exist in DB: {0}", GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchange);
                    }

                    // Smallest last sequence interchangeExtended
                    try
                    {
                        result = colorGraphs.ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.SmallestLastSequence(graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtended));
                        database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchangeExtended, result.Item1);
                    }
                    catch (MyException.DatabaseException.DatabaseAlgorithmDoesNotExistException)
                    {
                        Console.WriteLine("Algorithm does not exist in DB: {0}", GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchangeExtended);
                    }

                    // Smallest last sequence interchangeExtended with K3
                    try
                    {
                        if (useInterchangeExtendedK3)
                        {
                            result = colorGraphs.ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.SmallestLastSequence(graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtendedK3));
                            database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchangeExtendedK3, result.Item1);
                        }
                    }
                    catch (MyException.DatabaseException.DatabaseAlgorithmDoesNotExistException)
                    {
                        Console.WriteLine("Algorithm does not exist in DB: {0}", GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchangeExtendedK3);
                    }

                    // Connected sequential
                    try
                    {
                        result = colorGraphs.ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.ConnectedSequential.ConnectedSequential(graph));
                        database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedSequential, result.Item1);
                    }
                    catch (MyException.DatabaseException.DatabaseAlgorithmDoesNotExistException)
                    {
                        Console.WriteLine("Algorithm does not exist in DB: {0}", GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedSequential);
                    }

                    // Saturation largest first sequence
                    try
                    {
                        result = colorGraphs.ColorGraph(new GraphColoring.GraphColoringAlgorithm.SaturationLargestFirstSequence.SaturationLargestFirstSequence(graph));
                        database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.saturationLargestFirstSequence, result.Item1);
                    }
                    catch (MyException.DatabaseException.DatabaseAlgorithmDoesNotExistException)
                    {
                        Console.WriteLine("Algorithm does not exist in DB: {0}", GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.saturationLargestFirstSequence);
                    }

                    // Greedy independent set
                    try
                    {
                        result = colorGraphs.ColorGraph(new GraphColoring.GraphColoringAlgorithm.GreedyIndependentSet.GreedyIndependentSet(graph));
                        database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.greedyIndependentSet, result.Item1);
                    }
                    catch (MyException.DatabaseException.DatabaseAlgorithmDoesNotExistException)
                    {
                        Console.WriteLine("Algorithm does not exist in DB: {0}", GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.greedyIndependentSet);
                    }

                    // Combination
                    try
                    {
                        result = colorGraphs.ColorGraph(new GraphColoring.GraphColoringAlgorithm.CombinationAlgorithm.CombinationAlgorithm(graph));
                        database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.combinationAlgorithm, result.Item1);
                    }
                    catch (MyException.DatabaseException.DatabaseAlgorithmDoesNotExistException)
                    {
                        Console.WriteLine("Algorithm does not exist in DB: {0}", GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.combinationAlgorithm);
                    }

                    // Genetic
                    try
                    {
                        result = colorGraphs.ColorGraph(new GraphColoring.GraphColoringAlgorithm.GeneticAlgorithm.GeneticAlgorithm(graph, 1), true);
                        database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.geneticAlgorithm, colorGraphs.GetCountIterationsProbability(), result.Item1, result.Item2);
                    }
                    catch (MyException.DatabaseException.DatabaseAlgorithmDoesNotExistException)
                    {
                        Console.WriteLine("Algorithm does not exist in DB: {0}", GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.geneticAlgorithm);
                    }

                    // Genetic2
                    try
                    {
                        if (useGeneticAlgorithm2)
                        {
                            result = colorGraphs.ColorGraph(new GraphColoring.GraphColoringAlgorithm.GeneticAlgorithm.GeneticAlgorithm(graph, 2), true);
                            database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.geneticAlgorithm2, colorGraphs.GetCountIterationsProbability(), result.Item1, result.Item2);
                        }
                    }
                    catch (MyException.DatabaseException.DatabaseAlgorithmDoesNotExistException)
                    {
                        Console.WriteLine("Algorithm does not exist in DB: {0}", GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.geneticAlgorithm2);
                    }

                    // Connected largest first
                    try
                    {
                        result = colorGraphs.ColorGraph(new GraphColoring.GraphColoringAlgorithm.ConnectedLargestFirst.ConnectedLargestFirst(graph));
                        database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedLargestFirst, result.Item1);
                    }
                    catch (MyException.DatabaseException.DatabaseAlgorithmDoesNotExistException)
                    {
                        Console.WriteLine("Algorithm does not exist in DB: {0}", GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedLargestFirst);
                    }

                    // Connected largest first interchange
                    try
                    {
                        result = colorGraphs.ColorGraph(new GraphColoring.GraphColoringAlgorithm.ConnectedLargestFirst.ConnectedLargestFirst(graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchange));
                        database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedLargestFirstInterchange, result.Item1);
                    }
                    catch (MyException.DatabaseException.DatabaseAlgorithmDoesNotExistException)
                    {
                        Console.WriteLine("Algorithm does not exist in DB: {0}", GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedLargestFirstInterchange);
                    }

                    // Connected largest first interchangeExtended
                    try
                    {
                        result = colorGraphs.ColorGraph(new GraphColoring.GraphColoringAlgorithm.ConnectedLargestFirst.ConnectedLargestFirst(graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtended));
                        database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedLargestFirstInterchangeExtended, result.Item1);
                    }
                    catch (MyException.DatabaseException.DatabaseAlgorithmDoesNotExistException)
                    {
                        Console.WriteLine("Algorithm does not exist in DB: {0}", GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedLargestFirstInterchangeExtended);
                    }

                    // Connected largest first interchangeExtended with K3
                    try
                    {
                        if (useInterchangeExtendedK3)
                        {
                            result = colorGraphs.ColorGraph(new GraphColoring.GraphColoringAlgorithm.ConnectedLargestFirst.ConnectedLargestFirst(graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtendedK3));
                            database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedLargestFirstInterchangeExtendedK3, result.Item1);
                        }
                    }

                    catch (MyException.DatabaseException.DatabaseAlgorithmDoesNotExistException)
                    {
                        Console.WriteLine("Algorithm does not exist in DB: {0}", GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedLargestFirstInterchangeExtendedK3);
                    }

                    /*
                    // AI
                    try
                    {
                        result = colorGraphs.ColorGraph(new GraphColoring.GraphColoringAlgorithm.AI.AI(graph));
                        database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.AI, result.Item1);
                    }
                    catch (MyException.DatabaseException.DatabaseAlgorithmDoesNotExistException)
                    {
                        Console.WriteLine("Algorithm does not exist in DB: {0}", GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedLargestFirst);
                    }
                    */

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
            string pathFolder = GenerateGraphsFile.GetPathFolder();
            string pathFileNameExtended = GenerateGraphsFile.GetFileNameExtension();

            if (!Directory.Exists(pathFolder))
            {
                Directory.CreateDirectory(pathFolder);
                return;
            }

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

            foreach (string filePath in Directory.EnumerateFiles(pathFolder, "*." + pathFileNameExtended))
            {
                if (writer)
                    Console.WriteLine("Reading file: {0}", filePath);

                using (FileStream fileStream = File.OpenRead(filePath))
                using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        lineNumber++;

                        if (line.StartsWith(GRAPHCOMMENT))
                            continue;

                        // Graph
                        if (line.StartsWith(GRAPHHEADER))
                        {
                            try
                            {
                                error = false;

                                if (writer && ID_Graph != 0)
                                {
                                    if (writer)
                                    {
                                        if (!graphExists)
                                            Console.WriteLine("Added graph with ID {0}", ID_Graph);
                                        else

                                            Console.WriteLine("Updated graph with ID {0}", ID_Graph);
                                    }
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
                            catch (MyException.DatabaseException.GenerateGraphsDatabaseNotOpenException ex)
                            {
                                throw ex;
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Something wrong with this line [{0}]: {1}", lineNumber , line);

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
                                    case GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequenceInterchangeExtended:
                                        database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequenceInterchangeExtended, int.Parse(lineArray[1]), int.Parse(lineArray[2]), int.Parse(lineArray[3]));
                                        break;
                                    case GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequenceInterchangeExtended:
                                        database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequenceInterchangeExtended, int.Parse(lineArray[1]));
                                        break;
                                    case GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchangeExtended:
                                        database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchangeExtended, int.Parse(lineArray[1]));
                                        break;
                                    case GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequenceInterchangeExtendedK3:
                                        database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequenceInterchangeExtendedK3, int.Parse(lineArray[1]), int.Parse(lineArray[2]), int.Parse(lineArray[3]));
                                        break;
                                    case GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequenceInterchangeExtendedK3:
                                        database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequenceInterchangeExtendedK3, int.Parse(lineArray[1]));
                                        break;
                                    case GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchangeExtendedK3:
                                        database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchangeExtendedK3, int.Parse(lineArray[1]));
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
                                    case GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.geneticAlgorithm2:
                                        database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.geneticAlgorithm2, int.Parse(lineArray[1]), int.Parse(lineArray[2]), int.Parse(lineArray[3]));
                                        break;
                                    case GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedLargestFirst:
                                        database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedLargestFirst, int.Parse(lineArray[1]));
                                        break;
                                    case GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedLargestFirstInterchange:
                                        database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedLargestFirstInterchange, int.Parse(lineArray[1]));
                                        break;
                                    case GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedLargestFirstInterchangeExtended:
                                        database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedLargestFirstInterchangeExtended, int.Parse(lineArray[1]));
                                        break;
                                    case GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedLargestFirstInterchangeExtendedK3:
                                        database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedLargestFirstInterchangeExtendedK3, int.Parse(lineArray[1]));
                                        break;
                                    case GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.AI:
                                        database.AddGraphColoring(ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.AI, int.Parse(lineArray[1]));
                                        break;
                                    default:
                                        throw new MyException.GenerateGraphsException.GenerateGraphsAlgorithmDoesNotExistException(lineArray[0]);
                                }

                            }
                            catch (ArgumentException)
                            {
                                Console.WriteLine("Can't parse this line [{0}] (invalid count of parameters): {1}", lineNumber, line);
                            }
                            catch (MyException.DatabaseException.DatabaseAlgorithmDoesNotExistException)
                            {
                                Console.WriteLine("Algorithm does not exist: {0}", line);
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Something wrong with this line [{0}] : {1}", lineNumber, line);
                            }
                        }
                    }
                }

                if (writer && ID_Graph != 0)
                {
                    if (writer)
                    {
                        if (!graphExists)
                            Console.WriteLine("Added graph with ID {0}", ID_Graph);
                        else

                            Console.WriteLine("Updated graph with ID {0}", ID_Graph);
                    }
                }

                Console.WriteLine();
            }
        }
        #endregion
        
        #region Property
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
