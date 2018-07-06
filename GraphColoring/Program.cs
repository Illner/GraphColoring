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
            
            Tests.Tests tests = new Tests.Tests(false);
            tests.Test();

            /*
            Graph.GraphModification.Tests.ModificationTest lineGraphTest = new Graph.GraphModification.Tests.ModificationTest();
            Console.WriteLine(lineGraphTest.Test());
            */
            //Tests.Tests tests = new Tests.Tests(true);
            //tests.Test(Tests.Tests.TestEnum.graphSpanningTree);
            /*
            ReaderWriter.Reader reader = new ReaderWriter.Reader(@"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\graph.graph");
            Graph.Graph graph = reader.ReadFile();

            Console.WriteLine(graph.ToString());

            Graph.Graph lineGraph = Graph.GraphOperation.GraphOperation.LineGraph(graph);

            Console.WriteLine(lineGraph.ToString());
            */
        }
    }
}
