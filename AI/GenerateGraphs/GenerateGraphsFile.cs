using System;
using System.IO;
using System.Linq;

namespace AI.GenerateGraphs
{
    class GenerateGraphsFile : GenerateGraphs
    {
        // Variable
        #region
        public static string graphHeader = "Graph: ";
        private static string fileNameExtension = "graphDB";
        private static string pathFolder = @"Data\";
        private static string pathFile = pathFolder + "GeneratedGraphs" + "." + fileNameExtension; 
        #endregion

        // Constructor
        #region
        /// <summary>
        /// Initialize GenerateGraphs
        /// </summary>
        /// <param name="writer">write generated graphs on the screen</param>
        public GenerateGraphsFile(int constant, int exponent, bool writer = true, bool clear = false) : base(constant, exponent, writer)
        {
            if (clear && File.Exists(pathFile))
                File.WriteAllText(pathFile, string.Empty);
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
            string graphString;
            int countIterations;
            Tuple<int, int> result;

            using (StreamWriter file = File.AppendText(pathFile))
            {
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
                        while (!graph.GetGraphProperty().GetIsConnected());

                        graphString = graph.GetGraphProperty().GetCountVertices() + " " + graph.GetGraphProperty().GetCountEdges() + " " + graph.GetGraphProperty().GetGraphClass().ToString() + " " + graph.GetGraphProperty().GetIsChordal() + " " +
                                        graph.GetGraphProperty().GetIsRegular() + " " + graph.GetGraphProperty().GetIsCyclic() + " " + graph.GetGraphProperty().GetIsEulerian().ToString() + " " + graph.GetGraphProperty().GetMaximumVertexDegree() + " " +
                                        graph.GetGraphProperty().GetMinimumVertexDegree() + " " + graph.GetGraphProperty().GetAverageVertexDegree() + " " + graph.GetGraphProperty().GetMedianVertexDegree() + " " + graph.GetGraphProperty().GetCutVertices().Count + " " +
                                        graph.GetGraphProperty().GetBridges().Count + " " + graph.GetGraphProperty().GetGirth() + " " + string.Join(" ", graph.GetGraphProperty().GetDegreeSequenceInt(false));
                        
                        // Add graph to the file
                        file.WriteLine(graphHeader + graphString);

                        // Algorithm
                        // Random
                        result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.RandomSequence.RandomSequence(graph), true);
                        file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequence.ToString() + " " + COUNTITERATIONSPROBABILITY + " " + result.Item1 + " " + result.Item2);

                        // Largest first sequence
                        result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence(graph));
                        file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequence.ToString() + " " + result.Item1);

                        // Smallest last sequence
                        result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.SmallestLastSequence(graph));
                        file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequence.ToString() + " " + result.Item1);

                        // Random interchange
                        result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.RandomSequence.RandomSequence(graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchange), true);
                        file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequenceInterchange.ToString() + " " + COUNTITERATIONSPROBABILITY + " " + result.Item1 + " " + result.Item2);

                        // Largest first sequence interchange
                        result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence(graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchange));
                        file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequenceInterchange.ToString() + " " + result.Item1);

                        // Smallest last sequence interchange
                        result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.SmallestLastSequence(graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchange));
                        file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchange.ToString() + " " + result.Item1);

                        // Random interchangeExtended
                        result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.RandomSequence.RandomSequence(graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtended), true);
                        file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequenceInterchangeExtended.ToString() + " " + COUNTITERATIONSPROBABILITY + " " + result.Item1 + " " + result.Item2);

                        // Largest first sequence interchangeExtended
                        result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence(graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtended));
                        file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequenceInterchangeExtended.ToString() + " " + result.Item1);

                        // Smallest last sequence interchangeExtended
                        result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.SmallestLastSequence(graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtended));
                        file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchangeExtended.ToString() + " " + result.Item1);

                        // Connected sequential
                        result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.ConnectedSequential.ConnectedSequential(graph));
                        file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedSequential.ToString() + " " + result.Item1);

                        // Saturation largest first sequence
                        result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.SaturationLargestFirstSequence.SaturationLargestFirstSequence(graph));
                        file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.saturationLargestFirstSequence.ToString() + " " + result.Item1);

                        // Greedy independent set
                        result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.GreedyIndependentSet.GreedyIndependentSet(graph));
                        file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.greedyIndependentSet.ToString() + " " + result.Item1);

                        // Combination
                        result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.CombinationAlgorithm.CombinationAlgorithm(graph));
                        file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.combinationAlgorithm.ToString() + " " + result.Item1);

                        // Genetic
                        result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.GeneticAlgorithm.GeneticAlgorithm(graph, 1), true);
                        file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.geneticAlgorithm.ToString() + " " + COUNTITERATIONSPROBABILITY + " " + result.Item1 + " " + result.Item2);

                        // Genetic2
                        result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.GeneticAlgorithm.GeneticAlgorithm(graph, 2), true);
                        file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.geneticAlgorithm2.ToString() + " " + COUNTITERATIONSPROBABILITY + " " + result.Item1 + " " + result.Item2);

                        // Illner
                        result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.IllnerAlgorithm.IllnerAlgorithm(graph));
                        file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.illnerAlgorithm.ToString() + " " + result.Item1);

                        if (writer)
                        {
                            Console.WriteLine("Added graph - countVertices: {0}, iteration: {1}/{2}", countVertices, (iteration + 1), countIterations);
                        }
                        
                        file.Flush();
                    }
                }
                
                file.Flush();
            }
        }

        public void Pokus()
        {
            string path = @"C:\Users\illner\Desktop\MyDIMACS\LEI";
            string pathFile = @"C:\Users\illner\Desktop\MyDIMACS\LEI\Text.txt";
            string graphString;
            Tuple<int, int> result;

            using (StreamWriter file = File.AppendText(pathFile))
            {
                foreach (string fileGraph in Directory.EnumerateFiles(path, "*.graph"))
                {
                    GraphColoring.ReaderWriter.ReaderGraph readerGraph = new GraphColoring.ReaderWriter.ReaderGraph(fileGraph);
                    graph = readerGraph.ReadFile();

                    graphString = graph.GetGraphProperty().GetCountVertices() + " " + graph.GetGraphProperty().GetCountEdges() + " " + graph.GetGraphProperty().GetGraphClass().ToString() + " " + graph.GetGraphProperty().GetIsChordal() + " " +
                                            graph.GetGraphProperty().GetIsRegular() + " " + graph.GetGraphProperty().GetIsCyclic() + " " + graph.GetGraphProperty().GetIsEulerian().ToString() + " " + graph.GetGraphProperty().GetMaximumVertexDegree() + " " +
                                            graph.GetGraphProperty().GetMinimumVertexDegree() + " " + graph.GetGraphProperty().GetAverageVertexDegree() + " " + graph.GetGraphProperty().GetMedianVertexDegree() + " " + graph.GetGraphProperty().GetCutVertices().Count + " " +
                                            graph.GetGraphProperty().GetBridges().Count + " " + graph.GetGraphProperty().GetGirth() + " " + string.Join(" ", graph.GetGraphProperty().GetDegreeSequenceInt(false));

                    // Add graph to the file
                    file.WriteLine(graphHeader + graphString);
                    Console.WriteLine("Coloring graph - {0}", graph.GetName());

                    // Algorithm
                    // Random
                    result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.RandomSequence.RandomSequence(graph), true);
                    file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequence.ToString() + " " + COUNTITERATIONSPROBABILITY + " " + result.Item1 + " " + result.Item2);

                    // Largest first sequence
                    result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence(graph));
                    file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequence.ToString() + " " + result.Item1);

                    // Smallest last sequence
                    result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.SmallestLastSequence(graph));
                    file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequence.ToString() + " " + result.Item1);

                    // Random interchange
                    result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.RandomSequence.RandomSequence(graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchange), true);
                    file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequenceInterchange.ToString() + " " + COUNTITERATIONSPROBABILITY + " " + result.Item1 + " " + result.Item2);

                    // Largest first sequence interchange
                    result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence(graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchange));
                    file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequenceInterchange.ToString() + " " + result.Item1);

                    // Smallest last sequence interchange
                    result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.SmallestLastSequence(graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchange));
                    file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchange.ToString() + " " + result.Item1);

                    // Random interchangeExtended
                    result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.RandomSequence.RandomSequence(graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtended), true);
                    file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequenceInterchangeExtended.ToString() + " " + COUNTITERATIONSPROBABILITY + " " + result.Item1 + " " + result.Item2);

                    // Largest first sequence interchangeExtended
                    result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence(graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtended));
                    file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequenceInterchangeExtended.ToString() + " " + result.Item1);

                    // Smallest last sequence interchangeExtended
                    result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.SmallestLastSequence(graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtended));
                    file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchangeExtended.ToString() + " " + result.Item1);

                    // Connected sequential
                    result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.ConnectedSequential.ConnectedSequential(graph));
                    file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedSequential.ToString() + " " + result.Item1);

                    // Saturation largest first sequence
                    result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.SaturationLargestFirstSequence.SaturationLargestFirstSequence(graph));
                    file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.saturationLargestFirstSequence.ToString() + " " + result.Item1);

                    // Greedy independent set
                    result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.GreedyIndependentSet.GreedyIndependentSet(graph));
                    file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.greedyIndependentSet.ToString() + " " + result.Item1);

                    // Combination
                    result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.CombinationAlgorithm.CombinationAlgorithm(graph));
                    file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.combinationAlgorithm.ToString() + " " + result.Item1);

                    // Genetic
                    result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.GeneticAlgorithm.GeneticAlgorithm(graph, 1), true);
                    file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.geneticAlgorithm.ToString() + " " + COUNTITERATIONSPROBABILITY + " " + result.Item1 + " " + result.Item2);

                    // Genetic2
                    result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.GeneticAlgorithm.GeneticAlgorithm(graph, 2), true);
                    file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.geneticAlgorithm2.ToString() + " " + COUNTITERATIONSPROBABILITY + " " + result.Item1 + " " + result.Item2);

                    Console.WriteLine("Added graph - {0}", graph.GetName());
                    file.Flush();
                }
            }
        }
        #endregion

        // Property
        #region
        /// <summary>
        /// Return a path file
        /// </summary>
        /// <returns>path file</returns>
        public static string GetPathFile()
        {
            return pathFile;
        }

        /// <summary>
        /// Return a path folder
        /// </summary>
        /// <returns>path folder</returns>
        public static string GetPathFolder()
        {
            return pathFolder;
        }

        /// <summary>
        /// Return a file name extension
        /// </summary>
        /// <returns>file name extension</returns>
        public static string GetFileNameExtension()
        {
            return fileNameExtension;
        }
        #endregion
    }
}
