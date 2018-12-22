using System;
using System.Collections.Generic;
using System.Text;

namespace GraphColoring.Graph.GraphOperation.Tests
{
    class CopyTest : GraphColoring.Tests.ITestInterface
    {
        // Variable
        #region
        private IGraphInterface graph;
        private String testPath;
        private ReaderWriter.IReaderGraphInterface reader;
        private StringBuilder stringBuilder;
        private Dictionary<CopyEnum, string> testsDictionary;

        // Paths
        private string testPathGraphCopy = @"Testing\Test\GraphCopy.txt";
        private string graphCopy1 = CopyResource.graphCopy1;
        private string graphCopy2 = CopyResource.graphCopy2;
        private string graphCopy3 = CopyResource.graphCopy3;
        private string graphCopy4 = CopyResource.graphCopy4;
        #endregion

        // Enum
        #region
        public enum CopyEnum
        {
            graphCopy1,
            graphCopy2,
            graphCopy3,
            graphCopy4
        }
        #endregion

        // Constructor
        #region
        public CopyTest()
        {
            stringBuilder = new StringBuilder();

            // Fill testsDictionary
            testsDictionary = new Dictionary<CopyEnum, string>
            {
                { CopyEnum.graphCopy1, graphCopy1 },
                { CopyEnum.graphCopy2, graphCopy2 },
                { CopyEnum.graphCopy3, graphCopy3 },
                { CopyEnum.graphCopy4, graphCopy4 }
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

            foreach (CopyEnum copyEnum in testsDictionary.Keys)
            {
                Testing(copyEnum);
            }

            return stringBuilder;
        }

        /// <summary>
        /// Otestuje daný typ grafu
        /// </summary>
        /// <param name="copyEnum">daný typ grafu</param>
        /// <returns>Vrátí report</returns>
        public StringBuilder Test(CopyEnum copyEnum)
        {
            stringBuilder.Clear();

            Testing(copyEnum);

            return stringBuilder;
        }

        private void Testing(CopyEnum copyEnum)
        {
            try
            {
                testPath = ReaderWriter.ReaderWriter.CreateTestFile(testsDictionary[copyEnum]);

                reader = new ReaderWriter.ReaderGraph(testPath, false);
                graph = reader.ReadFile();

                stringBuilder.AppendLine(copyEnum.ToString());
                stringBuilder.AppendLine("Graph created.");
                stringBuilder.AppendLine(graph.ToString());

                IGraphInterface graphCopy = GraphOperation.CopyGraph(graph);

                stringBuilder.AppendLine("Copied graph.");
                stringBuilder.AppendLine(graphCopy.ToString());
            }
            catch (KeyNotFoundException)
            {
                throw new MyException.TestsException.TestsMissingTestException(copyEnum.ToString());
            }
            catch (MyException.ReaderWriterException.ReaderWriterException e)
            {
                stringBuilder.AppendLine(e.Message);
            }
        }
        #endregion

        // Property
        #region
        public string GetPath()
        {
            return testPathGraphCopy;
        }
        #endregion
    }
}
