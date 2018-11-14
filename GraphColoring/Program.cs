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
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GUI.GraphColoringForm());
            
            
            /*
            Tests.Tests tests = new Tests.Tests(true);
            tests.Test();
            */

            /*
            Graph.ColoredGraph.Tests.ColoredGraphTest coloredGraphTest = new Graph.ColoredGraph.Tests.ColoredGraphTest();
            Console.WriteLine(coloredGraphTest.Test(Graph.ColoredGraph.Tests.ColoredGraphTest.ColoredGraphEnum.valid));
            */
            //Tests.Tests tests = new Tests.Tests(true);
            //tests.Test(Tests.Tests.TestEnum.bridgesCutVerticesTest);
            
            //ReaderWriter.ReaderGraph reader = new ReaderWriter.ReaderGraph(Tests.Tests.CreateTestFile(GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.Tests.SmallestLastSequenceResource.smallestLastSequence5), false);
            //ReaderWriter.Writer writer = new ReaderWriter.Writer(Tests.Tests.CreateTestFile(GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.Tests.SmallestLastSequenceResource.smallestLastSequence5), false);

            //Graph.IGraphInterface graph = reader.ReadFile();


            /*
            GraphColoringAlgorithm.GeneticAlgorithm.GeneticAlgorithm algorithm = new GraphColoringAlgorithm.GeneticAlgorithm.GeneticAlgorithm(graph, 100);

            algorithm.Color();
            
            
            //writer.WriteFile(graph, GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.Optimal, true);
            Console.WriteLine(graph.GetColoredGraph());
            Console.WriteLine("Colors: " + graph.GetColoredGraph().GetCountUsedColors());
            */
            /*
            ReaderWriter.Reader reader = new ReaderWriter.Reader(@"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\GraphColoring\GraphColoring\bin\Debug\Testing\Graph\graph.graph");
            Graph.IGraphInterface graph = reader.ReadFile();

            List<Graph.Edge> cutVertices = graph.GetGraphProperty().GetBridges();
            
            cutVertices.ForEach(x => { Console.WriteLine(x.GetVertex1().GetUserName() + " " + x.GetVertex2().GetUserName()); });
            */
        }

        public static void TestGraph(int countVerticies)
        {
            // Variable
            Graph.IGraphInterface generatedGraph;
            Graph.GraphProperty.GraphProperty graphProperty;
            GenerateGraph.ErdosRenyiModel.ErdosRenyiModel erdosRenyiModel;
            //
            bool isConnected;
            bool isReguelar;
            bool isCyclic;


            // Generate graph
            erdosRenyiModel = new GenerateGraph.ErdosRenyiModel.ErdosRenyiModel(countVerticies);
            generatedGraph = erdosRenyiModel.GenerateGraph();
            graphProperty = generatedGraph.GetGraphProperty();

            // Graph property
        }
    }
}
