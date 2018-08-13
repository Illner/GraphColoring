using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring
{
    class Program
    {
        static void Main(string[] args) 
        {
            
            Tests.Tests tests = new Tests.Tests(true);
            tests.Test();
            
            /*
            Graph.ColoredGraph.Tests.ColoredGraphTest coloredGraphTest = new Graph.ColoredGraph.Tests.ColoredGraphTest();
            Console.WriteLine(coloredGraphTest.Test(Graph.ColoredGraph.Tests.ColoredGraphTest.ColoredGraphEnum.valid));
            */
            //Tests.Tests tests = new Tests.Tests(false);
            //tests.Test(Tests.Tests.TestEnum.saturationLargestFirstSequence);
            /*
            ReaderWriter.Reader reader = new ReaderWriter.Reader(Tests.Tests.CreateTestFile(GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.Tests.SmallestLastSequenceResource.smallestLastSequence3), false);
            ReaderWriter.Writer writer = new ReaderWriter.Writer(Tests.Tests.CreateTestFile(GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.Tests.SmallestLastSequenceResource.smallestLastSequence3), false);

            Graph.Graph graph = reader.ReadFile();

            GraphColoringAlgorithm.GraphColoringAlgorithm algorithm = new GraphColoringAlgorithm.SequenceAlgorithm.ConnectedSequential.ConnectedSequential(graph);

            algorithm.Color();

            //writer.WriteFile(graph, GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.Optimal, true);
            Console.WriteLine(graph.GetColoredGraph());
            Console.WriteLine("Colors: " + graph.GetColoredGraph().GetCountUsedColors());
            */
        }
    }
}
