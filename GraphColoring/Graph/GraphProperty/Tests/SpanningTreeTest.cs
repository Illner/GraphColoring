using System;
using System.Text;
using System.Collections.Generic;

namespace GraphColoring.Graph.GraphProperty.Tests
{
    class SpanningTreeTest : GraphColoring.Tests.ITestInterface
    {
        #region Variable
        private IGraphInterface graph;
        private String testPath;
        private ReaderWriter.IReaderGraphInterface reader;
        private StringBuilder stringBuilder;
        private Dictionary<SpanningTreeEnum, string> testsDictionary;

        // Paths
        private string testPathGraphSpanningTree = @"Testing\Test\GraphSpanningTree.txt";
        private string graphSpanningTree1 = SpanningTreeResource.graphSpanningTree1;
        private string graphSpanningTree2 = SpanningTreeResource.graphSpanningTree2;
        private string graphSpanningTree3 = SpanningTreeResource.graphSpanningTree3;
        private string graphSpanningTree4 = SpanningTreeResource.graphSpanningTree4;
        #endregion
        
        #region Enum
        public enum SpanningTreeEnum
        {
            graphSpanningTree1,
            graphSpanningTree2,
            graphSpanningTree3,
            graphSpanningTree4
        }
        #endregion
        
        #region Constructor
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
        
        #region Method
        /// <summary>
        /// Test all values of enum (files)
        /// </summary>
        /// <returns>report</returns>
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
        /// Test a particular enum (file)
        /// </summary>
        /// <param name="spanningTreeEnum">enum (file)</param>
        /// <returns>report</returns>
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
                testPath = ReaderWriter.ReaderWriter.CreateTestFile(testsDictionary[spanningTreeEnum]);
                reader = new ReaderWriter.ReaderGraph(testPath, false);
                graph = reader.ReadFile();

                stringBuilder.AppendLine(spanningTreeEnum.ToString());
                stringBuilder.AppendLine("Graph created.");
                stringBuilder.AppendLine(graph.ToString());

                stringBuilder.AppendLine("SpanningTree: ");

                List<IEdgeInterface> spanningTreeList = graph.GetGraphProperty().GetSpanningTree();

                foreach (IEdgeInterface edge in spanningTreeList)
                {
                    stringBuilder.AppendLine(edge.ToString());
                }
            }
            catch (KeyNotFoundException)
            {
                throw new MyException.TestsException.TestsMissingTestException(spanningTreeEnum.ToString());
            }
            catch (MyException.ReaderWriterException.ReaderWriterException e)
            {
                stringBuilder.AppendLine(e.Message);
            }
        }
        #endregion
        
        #region Property
        public string GetPath()
        {
            return testPathGraphSpanningTree;
        }
        #endregion
    }
}