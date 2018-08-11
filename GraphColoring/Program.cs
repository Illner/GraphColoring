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
            //tests.Test(Tests.Tests.TestEnum.smallestLastSequence);
            /*
            ReaderWriter.Reader reader = new ReaderWriter.Reader(@"Testing\Graph\graph.graph", false);
            ReaderWriter.Writer writer = new ReaderWriter.Writer(@"Testing\Graph\graph.graph", false);

            Graph.Graph graph = reader.ReadFile();

            graph.GetColoredGraph().GetUnColoredVertexList();

            GraphColoringAlgorithm.SequenceAlgorithm.GraphColoringSequenceAlgorithm algorithm = new GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.SmallestLastSequence(graph);

            algorithm.Color();

            writer.WriteFile(graph, GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.SmallestLastSequence, false);
            Console.WriteLine(graph.GetColoredGraph());
            */
        }
    }
}
