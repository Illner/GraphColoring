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
            bool error = false;

            Tests.Tests tests = new Tests.Tests(true);
            result = tests.Test();

            Console.WriteLine();

            if (!result)
            {
                Console.WriteLine("Testing failed. Some features may not be working correctly!");
                Console.WriteLine("Press any key to start application.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Testing passed.");
            }

            Console.Clear();

            do
            {
                try
                {
                    Console.WriteLine("The application is starting ...");

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new GUI.GraphColoringForm());

                    error = false;
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine("Something wrong: " + ex.GetType());
                    Console.WriteLine("Press any key to restart.");
                    Console.ReadKey();
                    Console.Clear();
                    error = true;
                }
            }
            while (error);

            Console.WriteLine("The application is closing ...");
            

            // Find SHC
            /*
            Graph.IGraphInterface graph;
            int countVertices = 6;
            GraphColoringAlgorithm.GeneticAlgorithm.GeneticAlgorithm G;
            GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.SmallestLastSequence SL;
            GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence LF;
            GraphColoringAlgorithm.ConnectedLargestFirst.ConnectedLargestFirst CL;
            int i = 0;

            int GNumber, SLNumber, LFNumber, CLNumber;
            while (true) 
            {
                GenerateGraph.ErdosRenyiModel.ErdosRenyiModel erdosRenyiModel = new GenerateGraph.ErdosRenyiModel.ErdosRenyiModel(countVertices);
                graph = erdosRenyiModel.GenerateGraph();

                if (!graph.GetGraphProperty().GetIsConnected())
                    continue;

                G = new GraphColoringAlgorithm.GeneticAlgorithm.GeneticAlgorithm(graph, 3);
                SL = new GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.SmallestLastSequence(graph, GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtended);
                LF = new GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence(graph, GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtended);
                CL = new GraphColoringAlgorithm.ConnectedLargestFirst.ConnectedLargestFirst(graph, GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtended);
                //Console.WriteLine(graph);
                G.Color();
                GNumber = graph.GetColoredGraph().GetCountUsedColors();

                graph.GetColoredGraph().DeinitializationColoredGraph();

                SL.Color();
                SLNumber = graph.GetColoredGraph().GetCountUsedColors();
                
                graph.GetColoredGraph().DeinitializationColoredGraph();

                LF.Color();
                LFNumber = graph.GetColoredGraph().GetCountUsedColors();

                graph.GetColoredGraph().DeinitializationColoredGraph();

                CL.Color();
                CLNumber = graph.GetColoredGraph().GetCountUsedColors();

                Console.WriteLine(++i);

                if (SLNumber > GNumber)
                {
                    Console.WriteLine(SL);
                    Console.WriteLine(graph);
                    Console.ReadKey();
                }

                if (LFNumber > GNumber)
                {
                    Console.WriteLine(LF);
                    Console.WriteLine(graph);
                    Console.ReadKey();
                }

                if (CLNumber > GNumber)
                {
                    Console.WriteLine(CL);
                    Console.WriteLine(graph);
                    Console.ReadKey();
                }
            }     
            */
        }
    }
}
