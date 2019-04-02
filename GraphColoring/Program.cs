using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

namespace GraphColoring
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            // Variable
            bool result;

            Tests.Tests tests = new Tests.Tests(true);
            result = tests.Test();

            Console.WriteLine();

            if (!result)
            {
                Console.WriteLine("Testing failed. Some features may not be working correctly");
                Console.WriteLine("Press any key to start application.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Testing passed.");
            }

            Console.WriteLine();
            Console.WriteLine("The application is starting ...");
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GUI.GraphColoringForm());

            Console.WriteLine("The application is closing ...");

            /*
            List<Graph.IGraphInterface> graphList = new List<Graph.IGraphInterface>();
            GenerateGraph.IGenerateGraphInterface generateGraph;
            List<Graph.IGraphInterface> copyGraphList;
            Stopwatch myStopwatch = new Stopwatch();
            Random random = new Random();
            /*
            for (int i = 0; i < 3; i++)
            {
                generateGraph = new GenerateGraph.ErdosRenyiModel.ErdosRenyiModel(random.Next(100, 200), GenerateGraph.ErdosRenyiModel.ErdosRenyiModel.ErdosRenyiModelProbabilityEnum.cLogNDividedByN);
                Graph.IGraphInterface graph =  generateGraph.GenerateGraph();

                if (!graph.GetGraphProperty().GetIsConnected())
                {
                    i--;
                    continue;
                }

                graphList.Add(graph);
            }
            */

            /*
            int minCount = int.MaxValue, maxCount = int.MinValue;
            Graph.IGraphInterface graphNevim, minGraph = null, maxGraph= null;
            for (int i = 0; i < 1000; i++)
            {
                generateGraph = new GenerateGraph.ErdosRenyiModel.ErdosRenyiModel(random.Next(250, 250), GenerateGraph.ErdosRenyiModel.ErdosRenyiModel.ErdosRenyiModelProbabilityEnum.cLogNDividedByN);
                
                graphNevim = generateGraph.GenerateGraph();

                if (!graphNevim.GetGraphProperty().GetIsConnected())
                {
                    i--;
                    continue;
                }

                if (graphNevim.GetGraphProperty().GetCountEdges() < minCount)
                {
                    minCount = graphNevim.GetGraphProperty().GetCountEdges();
                    minGraph = graphNevim;
                }

                if (graphNevim.GetGraphProperty().GetCountEdges() > maxCount)
                {
                    maxCount = graphNevim.GetGraphProperty().GetCountEdges();
                    maxGraph = graphNevim;
                }
            }
            minCount = int.MaxValue;
            maxCount = int.MinValue;

            graphList.Add(minGraph);
            graphList.Add(maxGraph);
            for (int i = 0; i < 1000; i++)
            {
                generateGraph = new GenerateGraph.ErdosRenyiModel.ErdosRenyiModel(random.Next(500, 500), GenerateGraph.ErdosRenyiModel.ErdosRenyiModel.ErdosRenyiModelProbabilityEnum.cLogNDividedByN);

                graphNevim = generateGraph.GenerateGraph();

                if (!graphNevim.GetGraphProperty().GetIsConnected())
                {
                    i--;
                    continue;
                }

                if (graphNevim.GetGraphProperty().GetCountEdges() < minCount)
                {
                    minCount = graphNevim.GetGraphProperty().GetCountEdges();
                    minGraph = graphNevim;
                }

                if (graphNevim.GetGraphProperty().GetCountEdges() > maxCount)
                {
                    maxCount = graphNevim.GetGraphProperty().GetCountEdges();
                    maxGraph = graphNevim;
                }
            }
            graphList.Add(minGraph);
            graphList.Add(maxGraph);

            
            foreach (GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum algorithm in (GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum[])Enum.GetValues(typeof(GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum)))
            {
                if (algorithm == GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.optimal ||
                    algorithm == GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.AI)
                    continue;

                copyGraphList = new List<Graph.IGraphInterface>();
                foreach(Graph.IGraphInterface graph in graphList)
                {
                    copyGraphList.Add(Graph.GraphOperation.GraphOperation.CopyGraph(graph));
                }

                Console.WriteLine("Algorithm: " + algorithm);

                GraphColoringAlgorithm.IGraphColoringAlgorithmInterface algorithmMethod = null;

                foreach (Graph.IGraphInterface graph in copyGraphList)
                {
                    switch (algorithm)
                    {
                        case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequence:
                            algorithmMethod = new GraphColoringAlgorithm.SequenceAlgorithm.RandomSequence.RandomSequence(graph);
                            break;
                        case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequence:
                            algorithmMethod = new GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence(graph);
                            break;
                        case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequence:
                            algorithmMethod = new GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.SmallestLastSequence(graph);
                            break;
                        case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequenceInterchange:
                            algorithmMethod = new GraphColoringAlgorithm.SequenceAlgorithm.RandomSequence.RandomSequence(graph, GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchange);
                            break;
                        case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequenceInterchange:
                            algorithmMethod = new GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence(graph, GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchange);
                            break;
                        case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchange:
                            algorithmMethod = new GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.SmallestLastSequence(graph, GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchange);
                            break;
                        case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequenceInterchangeExtended:
                            algorithmMethod = new GraphColoringAlgorithm.SequenceAlgorithm.RandomSequence.RandomSequence(graph, GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtended);
                            break;
                        case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequenceInterchangeExtended:
                            algorithmMethod = new GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence(graph, GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtended);
                            break;
                        case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchangeExtended:
                            algorithmMethod = new GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.SmallestLastSequence(graph, GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtended);
                            break;
                        case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedSequential:
                            algorithmMethod = new GraphColoringAlgorithm.SequenceAlgorithm.ConnectedSequential.ConnectedSequential(graph);
                            break;
                        case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.saturationLargestFirstSequence:
                            algorithmMethod = new GraphColoringAlgorithm.SaturationLargestFirstSequence.SaturationLargestFirstSequence(graph);
                            break;
                        case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.greedyIndependentSet:
                            algorithmMethod = new GraphColoringAlgorithm.GreedyIndependentSet.GreedyIndependentSet(graph);
                            break;
                        case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.geneticAlgorithm:
                            algorithmMethod = new GraphColoringAlgorithm.GeneticAlgorithm.GeneticAlgorithm(graph);
                            break;
                        case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.combinationAlgorithm:
                            algorithmMethod = new GraphColoringAlgorithm.CombinationAlgorithm.CombinationAlgorithm(graph);
                            break;
                        case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.illnerAlgorithm:
                            algorithmMethod = new GraphColoringAlgorithm.IllnerAlgorithm.IllnerAlgorithm(graph);
                            break;
                    }

                    algorithmMethod.Color();
                    graph.GetColoredGraph().DeinitializationColoredGraph();
                }

                foreach(Graph.IGraphInterface graph in copyGraphList)
                {
                    switch (algorithm)
                    {
                        case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequence:
                            algorithmMethod = new GraphColoringAlgorithm.SequenceAlgorithm.RandomSequence.RandomSequence(graph);
                            break;
                        case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequence:
                            algorithmMethod = new GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence(graph);
                            break;
                        case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequence:
                            algorithmMethod = new GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.SmallestLastSequence(graph);
                            break;
                        case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequenceInterchange:
                            algorithmMethod = new GraphColoringAlgorithm.SequenceAlgorithm.RandomSequence.RandomSequence(graph, GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchange);
                            break;
                        case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequenceInterchange:
                            algorithmMethod = new GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence(graph, GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchange);
                            break;
                        case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchange:
                            algorithmMethod = new GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.SmallestLastSequence(graph, GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchange);
                            break;
                        case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequenceInterchangeExtended:
                            algorithmMethod = new GraphColoringAlgorithm.SequenceAlgorithm.RandomSequence.RandomSequence(graph, GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtended);
                            break;
                        case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequenceInterchangeExtended:
                            algorithmMethod = new GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence(graph, GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtended);
                            break;
                        case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchangeExtended:
                            algorithmMethod = new GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.SmallestLastSequence(graph, GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtended);
                            break;
                        case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedSequential:
                            algorithmMethod = new GraphColoringAlgorithm.SequenceAlgorithm.ConnectedSequential.ConnectedSequential(graph);
                            break;
                        case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.saturationLargestFirstSequence:
                            algorithmMethod = new GraphColoringAlgorithm.SaturationLargestFirstSequence.SaturationLargestFirstSequence(graph);
                            break;
                        case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.greedyIndependentSet:
                            algorithmMethod = new GraphColoringAlgorithm.GreedyIndependentSet.GreedyIndependentSet(graph);
                            break;
                        case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.geneticAlgorithm:
                            algorithmMethod = new GraphColoringAlgorithm.GeneticAlgorithm.GeneticAlgorithm(graph);
                            break;
                        case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.combinationAlgorithm:
                            algorithmMethod = new GraphColoringAlgorithm.CombinationAlgorithm.CombinationAlgorithm(graph);
                            break;
                        case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.illnerAlgorithm:
                            algorithmMethod = new GraphColoringAlgorithm.IllnerAlgorithm.IllnerAlgorithm(graph);
                            break;
                    }

                    Console.WriteLine("Graph: countVertices {0}, countEdges {1}", graph.GetGraphProperty().GetCountVertices(), graph.GetGraphProperty().GetCountEdges());
                    
                    myStopwatch.Reset();
                    myStopwatch.Start();
                    for (int i = 0; i < 1000; i++)
                    {
                        algorithmMethod.Color();
                        graph.GetColoredGraph().DeinitializationColoredGraph();
                    }
                    myStopwatch.Stop();
                    Console.WriteLine("Time: " + myStopwatch.ElapsedMilliseconds);
                }
            }
            */

            /*
            string pathDictionary = @"C:\Users\illner\Desktop\DIMACS\";
            string myPathDictionary = @"C:\Users\illner\Desktop\MyDIMACS\";
            Graph.IGraphEdgeListInterface graph = null;
            
            foreach (string file in Directory.EnumerateFiles(pathDictionary, "*.col"))
            {
                try
                {
                    string line = "";
                    string firstEdge = "";
                    string secondEdge = "";
                    string name = "";
                    int countOfVertices = 0, countOfEdges = 0;
                    StreamReader myFile = new StreamReader(file);
                    while ((line = myFile.ReadLine()) != null)
                    {
                        string[] splitLine = line.Split(' ');

                        if (splitLine.Length == 1)
                            continue;

                        // Header
                        if (splitLine[0] == "c" && (splitLine[1] == "FILE:" || splitLine[1] == "File:"))
                        {
                            string[] splitsplitLine;
                            if (splitLine[2] == "")
                                splitsplitLine = splitLine[3].Split('.');
                            else
                            splitsplitLine = splitLine[2].Split('.');
                            name = splitsplitLine[0];
                        }

                        // Count of vertices and count of edges
                        if (splitLine[0] == "p")
                        {
                            countOfVertices = int.Parse(splitLine[2]);
                            countOfEdges = int.Parse(splitLine[3]);
                            graph = new Graph.GraphEdgeList(countOfVertices);
                            graph.SetName(name);
                        }

                        // Add edges
                        if (splitLine[0] == "e")
                        {
                            firstEdge = splitLine[1];
                            secondEdge = splitLine[2];

                            graph.AddEdge(firstEdge, secondEdge);
                        }
                    }

                    graph.InitializeGraph();

                    if ((2 * graph.GetGraphProperty().GetCountEdges() != countOfEdges) && (graph.GetGraphProperty().GetCountEdges() != countOfEdges))
                        throw new ArgumentException("Nesouhlasí počet hran v grafu! " + countOfEdges + " vs " + graph.GetGraphProperty().GetCountEdges());

                    if (!graph.GetGraphProperty().GetIsConnected())
                        throw new ArgumentException("Graph je nesouvislý!");

                    ReaderWriter.WriterGraph writerGraph = new ReaderWriter.WriterGraph(myPathDictionary + name + ".graph");
                    writerGraph.WriteFile(graph);
                    Console.WriteLine("Graph added: " + name);
                    //Console.ReadKey();
                }
                catch (ArgumentException ex)
                {
                }
                catch (Exception ex)
                {
                }
            }
            */

            /*
            Graph.IGraphInterface graph;
            int countVertices = 12;
            GraphColoringAlgorithm.IllnerAlgorithm.IllnerAlgorithm illnerAlgorithm;
            GraphColoringAlgorithm.GeneticAlgorithm.GeneticAlgorithm geneticAlgorithm;

            int illnerNumber, geneticNumber;

            while (true)
            {
                GenerateGraph.ErdosRenyiModel.ErdosRenyiModel erdosRenyiModel = new GenerateGraph.ErdosRenyiModel.ErdosRenyiModel(countVertices);
                graph = erdosRenyiModel.GenerateGraph();

                if (!graph.GetGraphProperty().GetIsConnected())
                    continue;

                illnerAlgorithm = new GraphColoringAlgorithm.IllnerAlgorithm.IllnerAlgorithm(graph);
                Console.WriteLine(graph);
                illnerAlgorithm.Color();

                illnerNumber = graph.GetColoredGraph().GetCountUsedColors();

                graph.GetColoredGraph().DeinitializationColoredGraph();
                
                geneticAlgorithm = new GraphColoringAlgorithm.GeneticAlgorithm.GeneticAlgorithm(graph, 2);
                geneticAlgorithm.Color();

                geneticNumber = graph.GetColoredGraph().GetCountUsedColors();
                
                if (illnerNumber > geneticNumber)
                {
                    Console.WriteLine(graph);
                    Console.ReadKey();
                }
                
            }
            */
        }
    }
}
