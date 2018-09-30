using System;
using System.Text;
using System.Collections.Generic;

namespace GraphColoring.Graph.GraphProperty.Tests
{
    class SpanningTreeTest : GraphColoring.Tests.ITestInterface
    {
        // Variable
        #region
        private IGraphInterface graph;
        private String testPath;
        private ReaderWriter.Reader reader;
        private StringBuilder stringBuilder;
        private Dictionary<SpanningTreeEnum, string> testsDictionary;

        // Paths
        private string testPathGraphSpanningTree = @"Testing\Test\GraphSpanningTree.txt";
        private string graphSpanningTree1 = SpanningTreeResource.graphSpanningTree1;
        private string graphSpanningTree2 = SpanningTreeResource.graphSpanningTree2;
        private string graphSpanningTree3 = SpanningTreeResource.graphSpanningTree3;
        private string graphSpanningTree4 = SpanningTreeResource.graphSpanningTree4;
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
                { SpanningTreeEnum.graphSpanningTree1, graphSpanningTree1 },
                { SpanningTreeEnum.graphSpanningTree2, graphSpanningTree2 },
                { SpanningTreeEnum.graphSpanningTree3, graphSpanningTree3 },
                { SpanningTreeEnum.graphSpanningTree4, graphSpanningTree4 }
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
                testPath = GraphColoring.Tests.Tests.CreateTestFile(testsDictionary[spanningTreeEnum]);
                reader = new ReaderWriter.Reader(testPath, false);
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