using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;

namespace GraphColoringConsole.GenerateTimeComplexity
{
    public class GenerateTimeComplexity
    {
        #region Variable
        private int countOfVertices;
        private bool writer, clearFile;
        private Stopwatch myStopwatch = new Stopwatch();
        private static string pathFolder = @"Data\";
        private const int COUNTITERATIONSGENERATEGRAPHS = 100;
        private const int COUNTITERATIONSTIMECOMPLEXITY = 100;
        private bool useGeneticAlgorithm2, useInterchangeExtendedK3;
        private GraphColoring.Graph.IGraphInterface minGraph, maxGraph;
        private static string timeComplexityFile = "TimeComplexity.txt";
        private static string pathFile = pathFolder + timeComplexityFile;
        #endregion
        
        #region Constructor
        public GenerateTimeComplexity(bool writer = true, bool clearFile = false, bool useGeneticAlgorithm2 = true, bool useInterchangeExtendedK3 = true)
        {
            this.writer = writer;
            this.clearFile = clearFile;
            this.useGeneticAlgorithm2 = useGeneticAlgorithm2;
            this.useInterchangeExtendedK3 = useInterchangeExtendedK3;
        }
        #endregion
        
        #region Method
        public void Generate(int countOfVertices, int countOfGraphs)
        {
            if (countOfVertices <= 0)
                throw new MyException.GenerateGraphsException.GenerateGraphsInvalidCountOfVerticesException(countOfVertices.ToString());

            this.countOfVertices = countOfVertices;

            // Create folder if does not exist
            if (!Directory.Exists(pathFolder))
                Directory.CreateDirectory(pathFolder);

            // Clear, create file
            if (clearFile && File.Exists(pathFile))
            {
                File.Delete(pathFile);
            }

            if (!File.Exists(pathFile))
                File.WriteAllText(pathFile, string.Empty);

            // Create time dictionary
            Dictionary<GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum, Pair<long, long>> timeDictionary = new Dictionary<GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum, Pair<long, long>>();
            Dictionary<GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum, Pair<long, long>> countInterchangeCallsDictionary = new Dictionary<GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum, Pair<long, long>>();

            for (int c = 0; c < countOfGraphs; c++)
            {
                if (writer)
                {
                    Console.WriteLine("------------------------");
                    Console.WriteLine("------------------------");
                    Console.WriteLine("Count of graphs: {0} / {1}", (c + 1), countOfGraphs);
                    Console.WriteLine("------------------------");
                    Console.WriteLine("------------------------");
                }

                GenerateGraphs();

                if (writer)
                {
                    Console.WriteLine("Graph (1): countVertices {0}, countEdges {1}", minGraph.GetGraphProperty().GetCountVertices(), minGraph.GetGraphProperty().GetCountEdges());
                    Console.WriteLine("Graph (2): countVertices {0}, countEdges {1}", maxGraph.GetGraphProperty().GetCountVertices(), maxGraph.GetGraphProperty().GetCountEdges());
                }

                using (StreamWriter file = File.AppendText(pathFile))
                {
                    file.WriteLine("Graph {0} / {1} - (1): countVertices {2}, countEdges {3}", (c + 1), countOfGraphs, minGraph.GetGraphProperty().GetCountVertices(), minGraph.GetGraphProperty().GetCountEdges());
                    file.WriteLine("Graph {0} / {1} - (2): countVertices {2}, countEdges {3}", (c + 1), countOfGraphs, maxGraph.GetGraphProperty().GetCountVertices(), maxGraph.GetGraphProperty().GetCountEdges());
                }

                // Generate time complexity
                foreach (GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum algorithmEnum in (GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum[])Enum.GetValues(typeof(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum)))
                {
                    if (algorithmEnum == GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.optimal ||
                        algorithmEnum == GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.AI)
                        continue;

                    if (!useGeneticAlgorithm2 && (algorithmEnum == GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.geneticAlgorithm2 ||
                        algorithmEnum == GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.geneticAlgorithm))
                        continue;

                    if (!useInterchangeExtendedK3 && (algorithmEnum == GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedLargestFirstInterchangeExtendedK3 ||
                        algorithmEnum == GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequenceInterchangeExtendedK3 ||
                        algorithmEnum == GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequenceInterchangeExtendedK3 ||
                        algorithmEnum == GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchangeExtendedK3))
                        continue;

                    // Variable
                    GraphColoring.GraphColoringAlgorithm.IGraphColoringAlgorithmInterface algroithm = null;

                    if (writer)
                    {
                        Console.WriteLine("----------");
                        Console.WriteLine("Algorithm: " + algorithmEnum.ToString());
                        Console.WriteLine("----------");
                    }

                    for (int i = 0; i < 2; i++)
                    {
                        // Variable
                        GraphColoring.Graph.IGraphInterface graph;
                        List<GraphColoring.Graph.IGraphInterface> graphList = new List<GraphColoring.Graph.IGraphInterface>(COUNTITERATIONSTIMECOMPLEXITY);

                        if (i == 0)
                        {
                            graph = minGraph;
                        }
                        else
                        {
                            graph = maxGraph;
                        }

                        // Create duplicates graphs
                        for (int j = 0; j < COUNTITERATIONSTIMECOMPLEXITY; j++)
                        {
                            graphList.Add(GraphColoring.Graph.GraphOperation.GraphOperation.CopyGraph(graph));
                        }
                        
                        myStopwatch.Reset();
                        myStopwatch.Start();

                        for (int j = 0; j < COUNTITERATIONSTIMECOMPLEXITY; j++)
                        {
                            // Choose algorithm
                            switch (algorithmEnum)
                            {
                                case GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequence:
                                    algroithm = new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.RandomSequence.RandomSequence(graphList[j]);
                                    break;
                                case GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequence:
                                    algroithm = new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence(graphList[j]);
                                    break;
                                case GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequence:
                                    algroithm = new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.SmallestLastSequence(graphList[j]);
                                    break;
                                case GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequenceInterchange:
                                    algroithm = new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.RandomSequence.RandomSequence(graphList[j], GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchange);
                                    break;
                                case GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequenceInterchange:
                                    algroithm = new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence(graphList[j], GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchange);
                                    break;
                                case GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchange:
                                    algroithm = new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.SmallestLastSequence(graphList[j], GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchange);
                                    break;
                                case GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequenceInterchangeExtended:
                                    algroithm = new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.RandomSequence.RandomSequence(graphList[j], GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtended);
                                    break;
                                case GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequenceInterchangeExtended:
                                    algroithm = new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence(graphList[j], GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtended);
                                    break;
                                case GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchangeExtended:
                                    algroithm = new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.SmallestLastSequence(graphList[j], GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtended);
                                    break;
                                case GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequenceInterchangeExtendedK3:
                                    algroithm = new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.RandomSequence.RandomSequence(graphList[j], GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtendedK3);
                                    break;
                                case GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequenceInterchangeExtendedK3:
                                    algroithm = new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence(graphList[j], GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtendedK3);
                                    break;
                                case GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchangeExtendedK3:
                                    algroithm = new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.SmallestLastSequence(graphList[j], GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtendedK3);
                                    break;
                                case GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedSequential:
                                    algroithm = new GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.ConnectedSequential.ConnectedSequential(graphList[j]);
                                    break;
                                case GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.saturationLargestFirstSequence:
                                    algroithm = new GraphColoring.GraphColoringAlgorithm.SaturationLargestFirstSequence.SaturationLargestFirstSequence(graphList[j]);
                                    break;
                                case GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.greedyIndependentSet:
                                    algroithm = new GraphColoring.GraphColoringAlgorithm.GreedyIndependentSet.GreedyIndependentSet(graphList[j]);
                                    break;
                                case GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.combinationAlgorithm:
                                    algroithm = new GraphColoring.GraphColoringAlgorithm.CombinationAlgorithm.CombinationAlgorithm(graphList[j]);
                                    break;
                                case GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedLargestFirst:
                                    algroithm = new GraphColoring.GraphColoringAlgorithm.ConnectedLargestFirst.ConnectedLargestFirst(graphList[j]);
                                    break;
                                case GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedLargestFirstInterchange:
                                    algroithm = new GraphColoring.GraphColoringAlgorithm.ConnectedLargestFirst.ConnectedLargestFirst(graphList[j], GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchange);
                                    break;
                                case GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedLargestFirstInterchangeExtended:
                                    algroithm = new GraphColoring.GraphColoringAlgorithm.ConnectedLargestFirst.ConnectedLargestFirst(graphList[j], GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtended);
                                    break;
                                case GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedLargestFirstInterchangeExtendedK3:
                                    algroithm = new GraphColoring.GraphColoringAlgorithm.ConnectedLargestFirst.ConnectedLargestFirst(graphList[j], GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtendedK3);
                                    break;
                                case GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.geneticAlgorithm:
                                    algroithm = new GraphColoring.GraphColoringAlgorithm.GeneticAlgorithm.GeneticAlgorithm(graphList[j], 1);
                                    break;
                                case GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.geneticAlgorithm2:
                                    algroithm = new GraphColoring.GraphColoringAlgorithm.GeneticAlgorithm.GeneticAlgorithm(graphList[j], 2);
                                    break;
                                default:
                                    throw new MyException.GenerateGraphsException.GenerateGraphsAlgorithmDoesNotExistException(algorithmEnum.ToString());
                            }

                            algroithm.Color();
                        }

                        myStopwatch.Stop();

                        if (writer)
                        {
                            Console.WriteLine("Time: " + myStopwatch.ElapsedMilliseconds / COUNTITERATIONSTIMECOMPLEXITY + " ms");
                            Console.WriteLine("Count of interchange calls: " + algroithm.GetCountInterchangeCalls());
                            Console.WriteLine();
                        }

                        Pair<long, long> time;
                        Pair<long, long> countOfInterchangeCalls;
                        if (timeDictionary.TryGetValue(algorithmEnum, out time))
                        {
                            if (i == 0)
                            {
                                time.First += myStopwatch.ElapsedMilliseconds;
                                countOfInterchangeCalls = countInterchangeCallsDictionary[algorithmEnum];
                                countOfInterchangeCalls.First += algroithm.GetCountInterchangeCalls();

                            }
                            else
                            {
                                time.Second += myStopwatch.ElapsedMilliseconds;
                                countOfInterchangeCalls = countInterchangeCallsDictionary[algorithmEnum];
                                countOfInterchangeCalls.Second += algroithm.GetCountInterchangeCalls();
                            }
                        }
                        else
                        {
                            time = new Pair<long, long>();
                            countOfInterchangeCalls = new Pair<long, long>();

                            if (i == 0)
                            {
                                time.First = myStopwatch.ElapsedMilliseconds;
                                countOfInterchangeCalls.First = algroithm.GetCountInterchangeCalls();
                            }
                            else
                            {

                                time.Second = myStopwatch.ElapsedMilliseconds;
                                countOfInterchangeCalls.Second = algroithm.GetCountInterchangeCalls();
                            }

                            timeDictionary.Add(algorithmEnum, time);
                            countInterchangeCallsDictionary.Add(algorithmEnum, countOfInterchangeCalls);
                        }
                    }
                }
            }

            using (StreamWriter file = File.AppendText(pathFile))
            {
                file.WriteLine();
                foreach (var record in timeDictionary)
                {
                    file.WriteLine("Algorithm: " + record.Key);
                    file.WriteLine("Sparse: ");
                    file.WriteLine("Time (average): " + record.Value.First / (COUNTITERATIONSTIMECOMPLEXITY * countOfGraphs));
                    file.WriteLine("Count of interchange calls: " + countInterchangeCallsDictionary[record.Key].First / countOfGraphs);
                    file.WriteLine("Dense: ");
                    file.WriteLine("Time (average): " + record.Value.Second / (COUNTITERATIONSTIMECOMPLEXITY * countOfGraphs));
                    file.WriteLine("Count of interchange calls: " + countInterchangeCallsDictionary[record.Key].Second / countOfGraphs);
                }
            }
        }

        public void GenerateGraphs()
        {
            // Variable
            int minCount = int.MaxValue, maxCount = int.MinValue;
            GraphColoring.GenerateGraph.IGenerateGraphInterface generateGraph;
            GraphColoring.Graph.IGraphInterface graphTemp, minGraph = null, maxGraph = null;

            for (int i = 0; i < COUNTITERATIONSGENERATEGRAPHS; i++)
            {
                generateGraph = new GraphColoring.GenerateGraph.ErdosRenyiModel.ErdosRenyiModel(countOfVertices, GraphColoring.GenerateGraph.ErdosRenyiModel.ErdosRenyiModel.ErdosRenyiModelProbabilityEnum.cLogNDividedByN);

                graphTemp = generateGraph.GenerateGraph();

                if (!graphTemp.GetGraphProperty().GetIsConnected())
                {
                    i--;
                    continue;
                }

                if (graphTemp.GetGraphProperty().GetCountEdges() < minCount)
                {
                    minCount = graphTemp.GetGraphProperty().GetCountEdges();
                    minGraph = graphTemp;
                }

                if (graphTemp.GetGraphProperty().GetCountEdges() > maxCount)
                {
                    maxCount = graphTemp.GetGraphProperty().GetCountEdges();
                    maxGraph = graphTemp;
                }
            }

            this.minGraph = minGraph;
            this.maxGraph = maxGraph;
        }

        private class Pair<T1, T2>
        {
            public T1 First { get; set; }
            public T2 Second { get; set; }
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
        #endregion
    }
}
