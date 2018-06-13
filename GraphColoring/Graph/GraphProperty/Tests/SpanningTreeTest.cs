using System;
using System.Text;
using System.Collections.Generic;

namespace GraphColoring.Graph.GraphProperty.Tests
{
    class SpanningTreeTest : GraphColoring.Tests.ITestInterface
    {
        // Variable
        #region
        private Graph graph;
        private String testPath;
        private ReaderWriter.Reader reader;
        private StringBuilder stringBuilder;

        // Paths
        string pathGraphSpanningTree1 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\SpanningTree\graphSpanningTree1.graph";
        string pathGraphSpanningTree2 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\SpanningTree\graphSpanningTree2.graph";
        string pathGraphSpanningTree3 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\SpanningTree\graphSpanningTree3.graph";
        string pathGraphSpanningTree4 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\SpanningTree\graphSpanningTree4.graph";
        #endregion

        // Enum
        #region
        public enum GraphEnum
        {
            graphSpanningTree1,
            graphSpanningTree2,
            graphSpanningTree3,
            graphSpanningTree4
        }
        #endregion

        // Constructor
        #region
        public SpanningTreeTest()
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
                    case GraphEnum.graphSpanningTree1:
                        testPath = pathGraphSpanningTree1;
                        break;
                    case GraphEnum.graphSpanningTree2:
                        testPath = pathGraphSpanningTree2;
                        break;
                    case GraphEnum.graphSpanningTree3:
                        testPath = pathGraphSpanningTree3;
                        break;
                    case GraphEnum.graphSpanningTree4:
                        testPath = pathGraphSpanningTree4;
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

                    stringBuilder.AppendLine("SpanningTree: ");

                    List<Edge> spanningTreeList = graph.GetGraphProperty().GetSpanningTree();

                    foreach (Edge edge in spanningTreeList)
                    {
                        stringBuilder.AppendLine(edge.ToString());
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