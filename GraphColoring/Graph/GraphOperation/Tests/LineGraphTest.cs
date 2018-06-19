using System;
using System.Collections.Generic;
using System.Text;

namespace GraphColoring.Graph.GraphOperation.Tests
{
    class LineGraphTest : GraphColoring.Tests.ITestInterface
    {
        // Variable
        #region
        private Graph graph;
        private String testPath;
        private ReaderWriter.Reader reader;
        private StringBuilder stringBuilder;
        private Dictionary<LineGraphEnum, string> testsDictionary;

        // Paths
        private string testPathGraphLineGraph = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Test\GraphLineGraph.txt";
        private string graphLineGraph1Path = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Operation\LineGraph\graphLineGraph1.graph";
        private string graphLineGraph2Path = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Operation\LineGraph\graphLineGraph2.graph";
        private string graphLineGraph3Path = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Operation\LineGraph\graphLineGraph3.graph";
        private string graphLineGraph4Path = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Operation\LineGraph\graphLineGraph4.graph";
        private string graphLineGraph5Path = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Operation\LineGraph\graphLineGraph5.graph";
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
                { LineGraphEnum.graphLineGraph1, graphLineGraph1Path },
                { LineGraphEnum.graphLineGraph2, graphLineGraph2Path },
                { LineGraphEnum.graphLineGraph3, graphLineGraph3Path },
                { LineGraphEnum.graphLineGraph4, graphLineGraph4Path },
                { LineGraphEnum.graphLineGraph5, graphLineGraph5Path }
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
                testPath = testsDictionary[lineGraphEnum];

                reader = new ReaderWriter.Reader(testPath);
                graph = reader.ReadFile();

                stringBuilder.AppendLine(lineGraphEnum.ToString());
                stringBuilder.AppendLine("Graph created.");
                stringBuilder.AppendLine(graph.ToString());

                Graph lineGraph = GraphOperation.LineGraph(graph);

                stringBuilder.AppendLine("Line graph.");
                stringBuilder.AppendLine(lineGraph.ToString());
            }
            catch (KeyNotFoundException)
            {
                throw new MyException.TestsMissingTestException(lineGraphEnum.ToString());
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
            return testPathGraphLineGraph;
        }
        #endregion
    }
}