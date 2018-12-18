using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace GraphColoring
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            /*
            Tests.Tests tests = new Tests.Tests(true);
            tests.Test();
            */

            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GUI.GraphColoringForm());
            

            /*
            Graph.ColoredGraph.Tests.ColoredGraphTest coloredGraphTest = new Graph.ColoredGraph.Tests.ColoredGraphTest();
            Console.WriteLine(coloredGraphTest.Test(Graph.ColoredGraph.Tests.ColoredGraphTest.ColoredGraphEnum.valid));
            */
            /*
            Tests.Tests tests = new Tests.Tests(false);
            tests.Test(Tests.Tests.TestEnum.chordalTest);
            */
            /*
            string chordal1 = Graph.GraphProperty.Tests.ChordalResource.graphChordal1;
            string chordal2 = Graph.GraphProperty.Tests.ChordalResource.graphChordal2;
            string chordal3 = Graph.GraphProperty.Tests.ChordalResource.graphChordal3;
            string chordal4 = Graph.GraphProperty.Tests.ChordalResource.graphChordal4;
            string chordal5 = Graph.GraphProperty.Tests.ChordalResource.graphChordal5;
            string chordal6 = Graph.GraphProperty.Tests.ChordalResource.graphChordal6;
            string chordal7 = Graph.GraphProperty.Tests.ChordalResource.graphChordal7;
            string chordal8 = Graph.GraphProperty.Tests.ChordalResource.graphChordal8;
            string chordal9 = Graph.GraphProperty.Tests.ChordalResource.graphChordal9;

            List<string> testsList = new List<string>
            {
                { chordal1 },
                { chordal2 },
                { chordal6 },
                { chordal7 },
                { chordal8 },
                { chordal3 },
                { chordal4 },
                { chordal5 },
                { chordal9 }
            };
            int myCount = 0;
            foreach (string path in testsList)
            {
                Console.WriteLine("--------------------------------------------");
                Console.WriteLine("Chordal" + ++myCount);

                // Variable
                int[] distribution = new int[2];
                distribution[0] = 0;
                distribution[1] = 0;
                int counter = 0;

                ReaderWriter.ReaderGraph reader = new ReaderWriter.ReaderGraph(Tests.Tests.CreateTestFile(path), false);
                Graph.IGraphInterface graph = reader.ReadFile();

                foreach (var vertexList in MyMath.MyMath.GeneratePermutations(graph.AllVertices()))
                {
                    graph.GetGraphProperty().PerfectEliminationOrdering(vertexList.ToList());
                    graph.GetGraphProperty().IsPerfectEliminationOrderingParallel();

                    if (graph.GetGraphProperty().GetIsChordal())
                        distribution[1]++;
                    else
                        distribution[0]++;

                    if (++counter % 1000 == 0)
                        Console.WriteLine("IsChordal: " + distribution[1] + ", nonChordal: " + distribution[0]);
                }
                Console.WriteLine("---- end");
                Console.WriteLine("IsChordal: " + distribution[1] + ", nonChordal: " + distribution[0]);
                Console.WriteLine("--------------------------------------------");
            }
            */
            /*
            ReaderWriter.ReaderGraph reader = new ReaderWriter.ReaderGraph(@"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\GraphColoring\GraphColoring\bin\Debug\Testing\Graph\graph.graph");
            Graph.IGraphInterface graph = reader.ReadFile();
            Console.WriteLine(graph);
            List<Graph.IVertexInterface> peoList = graph.GetGraphProperty().GetPerfectEliminationOrdering();
            //Console.WriteLine(peoList.Count);
            Console.WriteLine("----------");
            peoList.ForEach(x => { Console.WriteLine(x.GetUserName()); });
            */
        }
    }
}
