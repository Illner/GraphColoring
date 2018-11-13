using System;
using System.Collections.Generic;
using System.Text;

namespace GraphColoring.Graph.GraphOperation.Tests
{
    class LineGraphTest : GraphColoring.Tests.ITestInterface
    {
        // Variable
        #region
        private IGraphInterface graph;
        private String testPath;
        private ReaderWriter.IReaderGraphInterface reader;
        private StringBuilder stringBuilder;
        private Dictionary<LineGraphEnum, string> testsDictionary;

        // Paths
        private string testPathGraphLineGraph = @"Testing\Test\GraphLineGraph.txt";
        private string graphLineGraph1 = LineGraphResource.graphLineGraph1;
        private string graphLineGraph2 = LineGraphResource.graphLineGraph2;
        private string graphLineGraph3 = LineGraphResource.graphLineGraph3;
        private string graphLineGraph4 = LineGraphResource.graphLineGraph4;
        private string graphLineGraph5 = LineGraphResource.graphLineGraph5;
        #endregion

        // Enum
        #region
        public enum LineGraphEnum
        {
            graphLineGraph1,
            graphLineGraph2,
            graphLineGraph3,
            graphLineGraph4,
            graphLineGraph5
        }
        #endregion

        // Constructor
        #region
        public LineGraphTest()
        {
            stringBuilder = new StringBuilder();

            // Fill testsDictionary
            testsDictionary = new Dictionary<LineGraphEnum, string>
            {
                { LineGraphEnum.graphLineGraph1, graphLineGraph1 },
                { LineGraphEnum.graphLineGraph2, graphLineGraph2 },
                { LineGraphEnum.graphLineGraph3, graphLineGraph3 },
                { LineGraphEnum.graphLineGraph4, graphLineGraph4 },
                { LineGraphEnum.graphLineGraph5, graphLineGraph5 }
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

            foreach (LineGraphEnum lineGraphEnum in testsDictionary.Keys)
            {
                Testing(lineGraphEnum);
            }

            return stringBuilder;
        }

        /// <summary>
        /// Otestuje daný typ grafu
        /// </summary>
        /// <param name="lineGraphEnum">daný typ grafu</param>
        /// <returns>Vrátí report</returns>
        public StringBuilder Test(LineGraphEnum lineGraphEnum)
        {
            stringBuilder.Clear();

            Testing(lineGraphEnum);

            return stringBuilder;
        }

        private void Testing(LineGraphEnum lineGraphEnum)
        {
            try
            {
                testPath = GraphColoring.Tests.Tests.CreateTestFile(testsDictionary[lineGraphEnum]);

                reader = new ReaderWriter.ReaderGraph(testPath, false);
                graph = reader.ReadFile();

                stringBuilder.AppendLine(lineGraphEnum.ToString());
                stringBuilder.AppendLine("Graph created.");
                stringBuilder.AppendLine(graph.ToString());

                IGraphInterface lineGraph = GraphOperation.LineGraph(graph);

                stringBuilder.AppendLine("Line graph.");
                stringBuilder.AppendLine(lineGraph.ToString());
            }
            catch (KeyNotFoundException)
            {
                throw new MyException.TestsException.TestsMissingTestException(lineGraphEnum.ToString());
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
            return testPathGraphLineGraph;
        }
        #endregion
    }
}