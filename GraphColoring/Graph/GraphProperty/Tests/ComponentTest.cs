using System;
using System.Text;
using System.Collections.Generic;

namespace GraphColoring.Graph.GraphProperty.Tests
{
    class ComponentTest : GraphColoring.Tests.ITestInterface
    {
        // Variable
        #region
        private Graph graph;
        private String testPath;
        private ReaderWriter.Reader reader;
        private StringBuilder stringBuilder;
        private List<Graph> graphComponentList;

        // Paths
        string pathGraphComponent1 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Component\graphComponent1.graph";
        string pathGraphComponent2 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Component\graphComponent2.graph";
        string pathGraphComponent3 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Component\graphComponent3.graph";
        string pathGraphComponent4 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Component\graphComponent4.graph";
        string pathGraphComponent5 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Component\graphComponent5.graph";
        #endregion

        // Enum
        #region
        public enum GraphEnum
        {
            graphComponent1,
            graphComponent2,
            graphComponent3,
            graphComponent4,
            graphComponent5
        }
        #endregion

        // Constructor
        #region
        public ComponentTest()
        {
            stringBuilder = new StringBuilder();
        }
        #endregion

        // Method
        #region
        /// <summary>
        /// Otestuje všechny typy grafů
        /// </summary>
        /// <returns>Vrátí report</returns>
        public StringBuilder Test()
        {
            stringBuilder.Clear();

            foreach (GraphEnum graphEnum in Enum.GetValues(typeof(GraphEnum)))
            {
                stringBuilder.AppendLine(graphEnum.ToString());

                Testing(graphEnum);
            }

            return stringBuilder;
        }

        /// <summary>
        /// Otestuje daný typ grafu
        /// </summary>
        /// <param name="graphEnum">daný typ grafu</param>
        /// <returns>Vrátí report</returns>
        public StringBuilder Test(GraphEnum graphEnum)
        {
            stringBuilder.Clear();

            Testing(graphEnum);

            return stringBuilder;
        }
        
        private void Testing(GraphEnum graphEnum)
        {
            try
            {
                switch (graphEnum)
                {
                    case GraphEnum.graphComponent1:
                        testPath = pathGraphComponent1;
                        break;
                    case GraphEnum.graphComponent2:
                        testPath = pathGraphComponent2;
                        break;
                    case GraphEnum.graphComponent3:
                        testPath = pathGraphComponent3;
                        break;
                    case GraphEnum.graphComponent4:
                        testPath = pathGraphComponent4;
                        break;
                    case GraphEnum.graphComponent5:
                        testPath = pathGraphComponent5;
                        break;
                    default:
                        stringBuilder.AppendLine("This graph doesn't exist!");
                        break;
                }

                if (testPath != "")
                {
                    reader = new ReaderWriter.Reader(testPath);
                    graph = reader.ReadFile();

                    stringBuilder.AppendLine("Graph created.");
                    stringBuilder.AppendLine(graph.ToString());

                    stringBuilder.AppendLine("Number of components: " + graph.GetGraphProperty().GetCountComponents());
                    stringBuilder.AppendLine("Is graph connected: " + graph.GetGraphProperty().GetIsConnected());
                    stringBuilder.AppendLine("Circuit rank: " + graph.GetGraphProperty().GetCircuitRank());

                    graphComponentList = graph.GetGraphProperty().GetComponents();

                    foreach (Graph graphComponent in graphComponentList)
                    {
                        stringBuilder.AppendLine("Graph component.");
                        stringBuilder.AppendLine(graphComponent.ToString());
                    }
                }

                testPath = "";
            }
            catch (MyException.ReaderWriterException e)
            {
                stringBuilder.AppendLine(e.Message);
            }
        }
        #endregion
    }
}
