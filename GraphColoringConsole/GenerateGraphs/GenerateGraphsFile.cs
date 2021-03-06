﻿using System;
using System.IO;
using System.Linq;

namespace GraphColoringConsole.GenerateGraphs
{
    class GenerateGraphsFile : GenerateGraphs
    {
        #region Variable
        private static string fileNameExtension = "graphDB";
        private static string pathFolder = @"Data\";
        private static string pathFile = pathFolder + "GeneratedGraphs" + "." + fileNameExtension; 
        #endregion
        
        #region Constructor
        /// <summary>
        /// Initialize GenerateGraphs
        /// </summary>
        /// <param name="writer">write generated graphs on the screen</param>
        public GenerateGraphsFile(int constant, int exponent, bool writer = true, bool clear = false, bool useGeneticAlgorithm2 = true, bool useInterchangeExtendedK3 = true) : base(constant, exponent, writer, useGeneticAlgorithm2, useInterchangeExtendedK3)
        {
            if (clear && File.Exists(pathFile))
                File.WriteAllText(pathFile, string.Empty);
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
            string graphString;
            int countIterations;
            Tuple<int, int> result;
            
            Directory.CreateDirectory(pathFolder);

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
                        file.WriteLine(GRAPHHEADER + graphString);

                        // Algorithm
                        // Random
                        result = colorGraphs.ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.RandomSequence.RandomSequence(graph), true);
                        file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequence.ToString() + " " + colorGraphs.GetCountIterationsProbability() + " " + result.Item1 + " " + result.Item2);

                        // Random interchange
                        result = colorGraphs.ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.RandomSequence.RandomSequence(graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchange), true);
                        file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequenceInterchange.ToString() + " " + colorGraphs.GetCountIterationsProbability() + " " + result.Item1 + " " + result.Item2);

                        // Random interchangeExtended
                        result = colorGraphs.ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.RandomSequence.RandomSequence(graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtended), true);
                        file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequenceInterchangeExtended.ToString() + " " + colorGraphs.GetCountIterationsProbability() + " " + result.Item1 + " " + result.Item2);

                        // Random interchangeExtended with K3
                        if (useInterchangeExtendedK3)
                        {
                            result = colorGraphs.ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.RandomSequence.RandomSequence(graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtendedK3), true);
                            file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequenceInterchangeExtendedK3.ToString() + " " + colorGraphs.GetCountIterationsProbability() + " " + result.Item1 + " " + result.Item2);
                        }

                        // Largest first sequence
                        result = colorGraphs.ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence(graph));
                        file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequence.ToString() + " " + result.Item1);

                        // Largest first sequence interchange
                        result = colorGraphs.ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence(graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchange));
                        file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequenceInterchange.ToString() + " " + result.Item1);

                        // Largest first sequence interchangeExtended
                        result = colorGraphs.ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence(graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtended));
                        file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequenceInterchangeExtended.ToString() + " " + result.Item1);

                        // Largest first sequence interchangeExtended with K3
                        if (useInterchangeExtendedK3)
                        {
                            result = colorGraphs.ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence(graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtendedK3));
                            file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequenceInterchangeExtendedK3.ToString() + " " + result.Item1);
                        }

                        // Smallest last sequence
                        result = colorGraphs.ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.SmallestLastSequence(graph));
                        file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequence.ToString() + " " + result.Item1);

                        // Smallest last sequence interchange
                        result = colorGraphs.ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.SmallestLastSequence(graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchange));
                        file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchange.ToString() + " " + result.Item1);

                        // Smallest last sequence interchangeExtended
                        result = colorGraphs.ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.SmallestLastSequence(graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtended));
                        file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchangeExtended.ToString() + " " + result.Item1);

                        // Smallest last sequence interchangeExtended with K3
                        if (useInterchangeExtendedK3)
                        {
                            result = colorGraphs.ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.SmallestLastSequence(graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtendedK3));
                            file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchangeExtendedK3.ToString() + " " + result.Item1);
                        }
                        // Connected sequential
                        result = colorGraphs.ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.ConnectedSequential.ConnectedSequential(graph));
                        file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedSequential.ToString() + " " + result.Item1);

                        // Saturation largest first sequence
                        result = colorGraphs.ColorGraph(new GraphColoring.GraphColoringAlgorithm.SaturationLargestFirstSequence.SaturationLargestFirstSequence(graph));
                        file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.saturationLargestFirstSequence.ToString() + " " + result.Item1);

                        // Greedy independent set
                        result = colorGraphs.ColorGraph(new GraphColoring.GraphColoringAlgorithm.GreedyIndependentSet.GreedyIndependentSet(graph));
                        file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.greedyIndependentSet.ToString() + " " + result.Item1);

                        // Combination
                        result = colorGraphs.ColorGraph(new GraphColoring.GraphColoringAlgorithm.CombinationAlgorithm.CombinationAlgorithm(graph));
                        file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.combinationAlgorithm.ToString() + " " + result.Item1);

                        // Genetic
                        result = colorGraphs.ColorGraph(new GraphColoring.GraphColoringAlgorithm.GeneticAlgorithm.GeneticAlgorithm(graph, 1), true);
                        file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.geneticAlgorithm.ToString() + " " + colorGraphs.GetCountIterationsProbability() + " " + result.Item1 + " " + result.Item2);

                        // Genetic2
                        if (useGeneticAlgorithm2)
                        {
                            result = colorGraphs.ColorGraph(new GraphColoring.GraphColoringAlgorithm.GeneticAlgorithm.GeneticAlgorithm(graph, 2), true);
                            file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.geneticAlgorithm2.ToString() + " " + colorGraphs.GetCountIterationsProbability() + " " + result.Item1 + " " + result.Item2);
                        }

                        // Connected largest first
                        result = colorGraphs.ColorGraph(new GraphColoring.GraphColoringAlgorithm.ConnectedLargestFirst.ConnectedLargestFirst(graph));
                        file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedLargestFirst.ToString() + " " + result.Item1);

                        // Connected largest first interchange
                        result = colorGraphs.ColorGraph(new GraphColoring.GraphColoringAlgorithm.ConnectedLargestFirst.ConnectedLargestFirst(graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchange));
                        file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedLargestFirstInterchange.ToString() + " " + result.Item1);

                        // Connected largest first interchangeExtended
                        result = colorGraphs.ColorGraph(new GraphColoring.GraphColoringAlgorithm.ConnectedLargestFirst.ConnectedLargestFirst(graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtended));
                        file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedLargestFirstInterchangeExtended.ToString() + " " + result.Item1);

                        // Connected largest first interchangeExtended with K3
                        if (useInterchangeExtendedK3)
                        {
                            result = colorGraphs.ColorGraph(new GraphColoring.GraphColoringAlgorithm.ConnectedLargestFirst.ConnectedLargestFirst(graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtendedK3));
                            file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedLargestFirstInterchangeExtendedK3.ToString() + " " + result.Item1);
                        }

                        /*
                        // AI
                        result = colorGraphs.ColorGraph(new GraphColoring.GraphColoringAlgorithm.AI.AI(graph));
                        file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.AI.ToString() + " " + result.Item1);
                        */

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
        #endregion
        
        #region Property
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
