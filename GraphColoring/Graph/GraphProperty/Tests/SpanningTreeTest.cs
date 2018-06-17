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
        private Dictionary<SpanningTreeEnum, string> testsDictionary;

        // Paths
        private string testPathGraphSpanningTree = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Test\GraphSpanningTree.txt";
        private string graphSpanningTree1Path = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\SpanningTree\graphSpanningTree1.graph";
        private string graphSpanningTree2Path = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\SpanningTree\graphSpanningTree2.graph";
        private string graphSpanningTree3Path = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\SpanningTree\graphSpanningTree3.graph";
        private string graphSpanningTree4Path = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\SpanningTree\graphSpanningTree4.graph";
        #endregion

        // Enum
        #region
        public enum SpanningTreeEnum
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

            // Fill testsDictionary
            testsDictionary = new Dictionary<SpanningTreeEnum, string>
            {
                { SpanningTreeEnum.graphSpanningTree1, graphSpanningTree1Path },
                { SpanningTreeEnum.graphSpanningTree2, graphSpanningTree2Path },
                { SpanningTreeEnum.graphSpanningTree3, graphSpanningTree3Path },
                { SpanningTreeEnum.graphSpanningTree4, graphSpanningTree4Path }
            };
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

            foreach (SpanningTreeEnum spanningTreeEnum in testsDictionary.Keys)
            {
                Testing(spanningTreeEnum);
            }

            return stringBuilder;
        }

        /// <summary>
        /// Otestuje daný typ grafu
        /// </summary>
        /// <param name="spanningTreeEnum">daný typ grafu</param>
        /// <returns>Vrátí report</returns>
        public StringBuilder Test(SpanningTreeEnum spanningTreeEnum)
        {
            stringBuilder.Clear();

            Testing(spanningTreeEnum);

            return stringBuilder;
        }

        private void Testing(SpanningTreeEnum spanningTreeEnum)
        {
            try
            {
                testPath = testsDictionary[spanningTreeEnum];
                reader = new ReaderWriter.Reader(testPath);
                graph = reader.ReadFile();

                stringBuilder.AppendLine(spanningTreeEnum.ToString());
                stringBuilder.AppendLine("Graph created.");
                stringBuilder.AppendLine(graph.ToString());

                stringBuilder.AppendLine("SpanningTree: ");

                List<Edge> spanningTreeList = graph.GetGraphProperty().GetSpanningTree();

                foreach (Edge edge in spanningTreeList)
                {
                    stringBuilder.AppendLine(edge.ToString());
                }
            }
            catch (KeyNotFoundException)
            {
                throw new MyException.TestsMissingTestException(spanningTreeEnum.ToString());
            }
            catch (MyException.ReaderWriterException e)
            {
                stringBuilder.AppendLine(e.Message);
            }
        }
        #endregion

        // Property
        #region
        public string GetPath()
        {
            return testPathGraphSpanningTree;
        }
        #endregion
    }
}