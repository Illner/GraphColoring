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
            /*
            Tests.Tests tests = new Tests.Tests(false);
            tests.Test();
            */
            /*
            Graph.ColoredGraph.Tests.ColoredGraphTest coloredGraphTest = new Graph.ColoredGraph.Tests.ColoredGraphTest();
            Console.WriteLine(coloredGraphTest.Test(Graph.ColoredGraph.Tests.ColoredGraphTest.ColoredGraphEnum.valid));
            */
            //Tests.Tests tests = new Tests.Tests(true);
            //tests.Test(Tests.Tests.TestEnum.graphSpanningTree);
            
            ReaderWriter.Reader reader = new ReaderWriter.Reader(@"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\graph.graph");
            Graph.Graph graph = reader.ReadFile();

            graph.GetColoredGraph().GetUnColoredVertexList();

            GraphColoringAlgorithm.SequenceAlgorithm.GraphColoringSequenceAlgorithm algorithm = new GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence(graph);

            algorithm.Color();

            Console.WriteLine(graph.GetColoredGraph().ToString());
            
        }
    }
}
