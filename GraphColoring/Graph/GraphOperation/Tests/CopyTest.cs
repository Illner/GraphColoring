using System;
using System.Collections.Generic;
using System.Text;

namespace GraphColoring.Graph.GraphOperation.Tests
{
    class CopyTest : GraphColoring.Tests.ITestInterface
    {
        // Variable
        #region
        private Graph graph;
        private String testPath;
        private ReaderWriter.Reader reader;
        private StringBuilder stringBuilder;
        private Dictionary<CopyEnum, string> testsDictionary;

        // Paths
        private string testPathGraphCopy = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Test\GraphCopy.txt";
        private string graphCopy1Path = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Operation\Copy\graphCopy1.graph";
        private string graphCopy2Path = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Operation\Copy\graphCopy2.graph";
        private string graphCopy3Path = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Operation\Copy\graphCopy3.graph";
        private string graphCopy4Path = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Operation\Copy\graphCopy4.graph";
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
                { CopyEnum.graphCopy1, graphCopy1Path },
                { CopyEnum.graphCopy2, graphCopy2Path },
                { CopyEnum.graphCopy3, graphCopy3Path },
                { CopyEnum.graphCopy4, graphCopy4Path }
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
                testPath = testsDictionary[copyEnum];

                reader = new ReaderWriter.Reader(testPath);
                graph = reader.ReadFile();

                stringBuilder.AppendLine(copyEnum.ToString());
                stringBuilder.AppendLine("Graph created.");
                stringBuilder.AppendLine(graph.ToString());

                Graph graphCopy = GraphOperation.CopyGraph(graph);

                stringBuilder.AppendLine("Copied graph.");
                stringBuilder.AppendLine(graphCopy.ToString());
            }
            catch (KeyNotFoundException)
            {
                throw new MyException.TestsMissingTestException(copyEnum.ToString());
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
            return testPathGraphCopy;
        }
        #endregion
    }
}
