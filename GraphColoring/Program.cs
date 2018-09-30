using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace GraphColoring
{
    class Program
    {
        static void Main(string[] args) 
        {
            /*
            Tests.Tests tests = new Tests.Tests(true);
            tests.Test();
            */
            List<int> myList = new List<int>();
            for (int i = 0; i < 1000; i++)
            {
                GenerateGraph.ErdosRenyiModel generateGraph = new GenerateGraph.ErdosRenyiModel(1000, GenerateGraph.ErdosRenyiModel.ErdosRenyiModelProbabilityEnum.cDividedByNLessThanOne);
                Graph.IGraphInterface graph = generateGraph.GenerateGraph();

                //Console.WriteLine(i);
                myList.Add(generateGraph.probability);
            }
            Console.WriteLine("95");
            Console.WriteLine("Average: " + myList.Average());
            Console.WriteLine("Max: " + myList.Max());
            Console.WriteLine("Min: " + myList.Min());

            myList = new List<int>();
            for (int i = 0; i < 1000; i++)
            {
                GenerateGraph.ErdosRenyiModel generateGraph = new GenerateGraph.ErdosRenyiModel(1000, GenerateGraph.ErdosRenyiModel.ErdosRenyiModelProbabilityEnum.cDividedByNMoreThanOne);
                Graph.IGraphInterface graph = generateGraph.GenerateGraph();

                //Console.WriteLine(i);
                myList.Add(generateGraph.probability);
            }
            Console.WriteLine("150");
            Console.WriteLine("Average: " + myList.Average());
            Console.WriteLine("Max: " + myList.Max());
            Console.WriteLine("Min: " + myList.Min());

            myList = new List<int>();
            for (int i = 0; i < 1000; i++)
            {
                GenerateGraph.ErdosRenyiModel generateGraph = new GenerateGraph.ErdosRenyiModel(1000, GenerateGraph.ErdosRenyiModel.ErdosRenyiModelProbabilityEnum.cLogNDividedByN);
                Graph.IGraphInterface graph = generateGraph.GenerateGraph();

                //Console.WriteLine(i);
                myList.Add(generateGraph.probability);
            }
            Console.WriteLine("700");
            Console.WriteLine("Average: " + myList.Average());
            Console.WriteLine("Max: " + myList.Max());
            Console.WriteLine("Min: " + myList.Min());

            /*
            Graph.ColoredGraph.Tests.ColoredGraphTest coloredGraphTest = new Graph.ColoredGraph.Tests.ColoredGraphTest();
            Console.WriteLine(coloredGraphTest.Test(Graph.ColoredGraph.Tests.ColoredGraphTest.ColoredGraphEnum.valid));
            */
            //Tests.Tests tests = new Tests.Tests(false);
            //tests.Test(Tests.Tests.TestEnum.greedyIndependentSet);
            /*
            ReaderWriter.Reader reader = new ReaderWriter.Reader(Tests.Tests.CreateTestFile(GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.Tests.SmallestLastSequenceResource.smallestLastSequence5), false);
            ReaderWriter.Writer writer = new ReaderWriter.Writer(Tests.Tests.CreateTestFile(GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.Tests.SmallestLastSequenceResource.smallestLastSequence5), false);

            Graph.Graph graph = reader.ReadFile();

            GraphColoringAlgorithm.GraphColoringAlgorithm algorithm = new GraphColoringAlgorithm.GreedyIndependentSet.GreedyIndependentSet(graph);

            algorithm.Color();

            //writer.WriteFile(graph, GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.Optimal, true);
            Console.WriteLine(graph.GetColoredGraph());
            Console.WriteLine("Colors: " + graph.GetColoredGraph().GetCountUsedColors());
            */
        }
    }
}
