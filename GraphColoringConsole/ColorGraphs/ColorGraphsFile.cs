using System;
using System.IO;

namespace GraphColoringConsole.ColorGraphs
{
    class ColorGraphsFile : ColorGraphs
    {
        // Variable
        #region
        private bool writer, clearFile;
        private static string pathFolder = @"Data\";
        private static string fileNameExtension = "graphDB";
        private bool useGeneticAlgorithm2, useInterchangeExtendedK3;
        private static string pathFile = pathFolder + "ColoredGraphs" + "." + fileNameExtension;
        #endregion

        // Constructor
        #region
        public ColorGraphsFile(bool writer = true, bool clearFile = false, bool useGeneticAlgorithm2 = true, bool useInterchangeExtendedK3 = true)
        {
            this.writer = writer;
            this.clearFile = clearFile;
            this.useGeneticAlgorithm2 = useGeneticAlgorithm2;
            this.useInterchangeExtendedK3 = useInterchangeExtendedK3;
        }
        #endregion

        // Method
        #region
        public void Color()
        {
            string graphString;
            Tuple<int, int> result;
            GraphColoring.Graph.IGraphInterface graph;

            // Create folder if does not exist
            if (!Directory.Exists(pathFolder))
            {
                Directory.CreateDirectory(pathFolder);
                return;
            }

            // Clear, create file
            if (clearFile && File.Exists(pathFile))
            {
                File.Delete(pathFile);
            }

            if (!File.Exists(pathFile))
                File.WriteAllText(pathFile, string.Empty);

            using (StreamWriter file = File.AppendText(pathFile))
            {
                foreach (string fileGraph in Directory.EnumerateFiles(pathFolder, "*.graph"))
                {
                    GraphColoring.ReaderWriter.ReaderGraph readerGraph = new GraphColoring.ReaderWriter.ReaderGraph(fileGraph, false);
                    graph = readerGraph.ReadFile();

                    graphString = graph.GetGraphProperty().GetCountVertices() + " " + graph.GetGraphProperty().GetCountEdges() + " " + graph.GetGraphProperty().GetGraphClass().ToString() + " " + graph.GetGraphProperty().GetIsChordal() + " " +
                                            graph.GetGraphProperty().GetIsRegular() + " " + graph.GetGraphProperty().GetIsCyclic() + " " + graph.GetGraphProperty().GetIsEulerian().ToString() + " " + graph.GetGraphProperty().GetMaximumVertexDegree() + " " +
                                            graph.GetGraphProperty().GetMinimumVertexDegree() + " " + graph.GetGraphProperty().GetAverageVertexDegree() + " " + graph.GetGraphProperty().GetMedianVertexDegree() + " " + graph.GetGraphProperty().GetCutVertices().Count + " " +
                                            graph.GetGraphProperty().GetBridges().Count + " " + graph.GetGraphProperty().GetGirth() + " " + string.Join(" ", graph.GetGraphProperty().GetDegreeSequenceInt(false));

                    // Add graph to the file
                    file.WriteLine(GenerateGraphs.GenerateGraphs.GRAPHCOMMENT + graph.GetName());
                    file.WriteLine(GenerateGraphs.GenerateGraphs.GRAPHHEADER + graphString);

                    if (writer)
                        Console.WriteLine("Coloring graph - {0}", graph.GetName());

                    // Algorithm
                    // Random
                    result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.RandomSequence.RandomSequence(graph), true);
                    file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequence.ToString() + " " + COUNTITERATIONSPROBABILITY + " " + result.Item1 + " " + result.Item2);

                    // Random interchange
                    result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.RandomSequence.RandomSequence(graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchange), true);
                    file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequenceInterchange.ToString() + " " + COUNTITERATIONSPROBABILITY + " " + result.Item1 + " " + result.Item2);

                    // Random interchangeExtended
                    result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.RandomSequence.RandomSequence(graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtended), true);
                    file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequenceInterchangeExtended.ToString() + " " + COUNTITERATIONSPROBABILITY + " " + result.Item1 + " " + result.Item2);

                    // Random interchangeExtended with K3
                    if (useInterchangeExtendedK3)
                    {
                        result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.RandomSequence.RandomSequence(graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtendedK3), true);
                        file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequenceInterchangeExtendedK3.ToString() + " " + COUNTITERATIONSPROBABILITY + " " + result.Item1 + " " + result.Item2);
                    }

                    // Largest first sequence
                    result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence(graph));
                    file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequence.ToString() + " " + result.Item1);

                    // Largest first sequence interchange
                    result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence(graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchange));
                    file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequenceInterchange.ToString() + " " + result.Item1);

                    // Largest first sequence interchangeExtended
                    result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence(graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtended));
                    file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequenceInterchangeExtended.ToString() + " " + result.Item1);

                    // Largest first sequence interchangeExtended with K3
                    if (useInterchangeExtendedK3)
                    {
                        result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence(graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtendedK3));
                        file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequenceInterchangeExtendedK3.ToString() + " " + result.Item1);
                    }

                    // Smallest last sequence
                    result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.SmallestLastSequence(graph));
                    file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequence.ToString() + " " + result.Item1);

                    // Smallest last sequence interchange
                    result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.SmallestLastSequence(graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchange));
                    file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchange.ToString() + " " + result.Item1);

                    // Smallest last sequence interchangeExtended
                    result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.SmallestLastSequence(graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtended));
                    file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchangeExtended.ToString() + " " + result.Item1);

                    // Smallest last sequence interchangeExtended with K3
                    if (useInterchangeExtendedK3)
                    {
                        result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.SmallestLastSequence(graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtendedK3));
                        file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchangeExtendedK3.ToString() + " " + result.Item1);
                    }

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
                    if (useGeneticAlgorithm2)
                    {
                        result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.GeneticAlgorithm.GeneticAlgorithm(graph, 2), true);
                        file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.geneticAlgorithm2.ToString() + " " + COUNTITERATIONSPROBABILITY + " " + result.Item1 + " " + result.Item2);
                    }

                    // Connected largest first
                    result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.ConnectedLargestFirst.ConnectedLargestFirst(graph));
                    file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedLargestFirst.ToString() + " " + result.Item1);

                    // Connected largest first interchange
                    result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.ConnectedLargestFirst.ConnectedLargestFirst(graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchange));
                    file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedLargestFirstInterchange.ToString() + " " + result.Item1);

                    // Connected largest first interchangeExtended
                    result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.ConnectedLargestFirst.ConnectedLargestFirst(graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtended));
                    file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedLargestFirstInterchangeExtended.ToString() + " " + result.Item1);

                    // Connected largest first interchangeExtended with K3
                    if (useInterchangeExtendedK3)
                    {
                        result = ColorGraph(new GraphColoring.GraphColoringAlgorithm.ConnectedLargestFirst.ConnectedLargestFirst(graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtendedK3));
                        file.WriteLine(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedLargestFirstInterchangeExtendedK3.ToString() + " " + result.Item1);
                    }

                    if (writer)
                        Console.WriteLine("Graph was colored - {0}", graph.GetName());

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
