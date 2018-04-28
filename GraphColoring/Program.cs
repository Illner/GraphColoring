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
            // Tests.Tests tests = new Tests.Tests(false);
            // tests.Test();
            
            ReaderWriter.Reader reader = new ReaderWriter.Reader(@"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\GraphProperty\Component\graphValidComponent.graph");
            Graph.Graph graph = reader.ReadFile();

            Console.WriteLine("-------------------Graph-----------------");
            Console.WriteLine(graph.ToString());

            Console.WriteLine("Count components: " + graph.GetGraphProperty().GetCountComponents());

            List<Graph.Graph> graphComponent = graph.GetGraphProperty().GetComponents();
            
            foreach (Graph.Graph graph1 in graphComponent)
            {
                Console.WriteLine("------" + graph1.GetName() + "------");
                Console.WriteLine(graph1.ToString());
            }
        }
    }
}
