using System;
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
            
            Tests.Tests tests = new Tests.Tests(true);
            tests.Test();
            

            /*
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GUI.GraphColoringForm());
            */
            
            /*
            List<Graph.IGraphInterface> graphList = new List<Graph.IGraphInterface>();
            List<Graph.IGraphInterface> copyGraphList;
            Stopwatch myStopwatch = new Stopwatch();
            Random random = new Random();
            
            for (int i = 0; i < 3; i++)
            {
                GenerateGraph.IGenerateGraphInterface generateGraph = new GenerateGraph.ErdosRenyiModel.ErdosRenyiModel(random.Next(100, 200), GenerateGraph.ErdosRenyiModel.ErdosRenyiModel.ErdosRenyiModelProbabilityEnum.cLogNDividedByN);
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
            GenerateGraph.IGenerateGraphInterface generateGraph;
            int minCount = int.MaxValue, maxCount = int.MinValue;
            Graph.IGraphInterface graphNevim, minGraph = null, maxGraph= null;
            for (int i = 0; i < 1000; i++)
            {
                generateGraph = new GenerateGraph.ErdosRenyiModel.ErdosRenyiModel(random.Next(250, 250), GenerateGraph.ErdosRenyiModel.ErdosRenyiModel.ErdosRenyiModelProbabilityEnum.cLogNDividedByN);

                graphNevim = generateGraph.GenerateGraph();
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
                            algorithmMethod = new GraphColoringAlgorithm.SequenceAlgorithm.RandomSequence.RandomSequence(graph, true);
                            break;
                        case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequenceInterchange:
                            algorithmMethod = new GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence(graph, true);
                            break;
                        case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchange:
                            algorithmMethod = new GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.SmallestLastSequence(graph, true);
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
                        case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.geneticAlgortihm:
                            algorithmMethod = new GraphColoringAlgorithm.GeneticAlgorithm.GeneticAlgorithm(graph);
                            break;
                        case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.combinationAlgorithm:
                            algorithmMethod = new GraphColoringAlgorithm.CombinationAlgorithm.CombinationAlgorithm(graph);
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
                            algorithmMethod = new GraphColoringAlgorithm.SequenceAlgorithm.RandomSequence.RandomSequence(graph, true);
                            break;
                        case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequenceInterchange:
                            algorithmMethod = new GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence(graph, true);
                            break;
                        case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchange:
                            algorithmMethod = new GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.SmallestLastSequence(graph, true);
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
                        case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.geneticAlgortihm:
                            algorithmMethod = new GraphColoringAlgorithm.GeneticAlgorithm.GeneticAlgorithm(graph);
                            break;
                        case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.combinationAlgorithm:
                            algorithmMethod = new GraphColoringAlgorithm.CombinationAlgorithm.CombinationAlgorithm(graph);
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
        }
    }
}
