using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring.GraphColoringAlgorithm.Optimal.Tests
{
    class OptimalTest : GraphColoring.Tests.ITestInterface
    {
        // Variable
        #region
        private Graph.Graph graph;
        private String testPath;
        private ReaderWriter.Reader reader;
        private StringBuilder stringBuilder;
        private Dictionary<OptimalEnum, string> testsDictionary;

        // Paths
        private string testPathAlgorithmOptimal = @"Testing\Test\Optimal.txt";
        private string algorithmOptimal1 = OptimalResource.optimal1;
        private string algorithmOptimal2 = OptimalResource.optimal2;
        private string algorithmOptimal3 = OptimalResource.optimal3;
        private string algorithmOptimal4 = OptimalResource.optimal4;
        #endregion

        // Enum
        #region
        public enum OptimalEnum
        {
            optimal1,
            optimal2,
            optimal3,
            optimal4
        }
        #endregion

        // Constructor
        #region
        public OptimalTest()
        {
            stringBuilder = new StringBuilder();

            // Fill testsDictionary
            testsDictionary = new Dictionary<OptimalEnum, string>
            {
                { OptimalEnum.optimal1, algorithmOptimal1 },
                { OptimalEnum.optimal2, algorithmOptimal2 },
                { OptimalEnum.optimal3, algorithmOptimal3 },
                { OptimalEnum.optimal4, algorithmOptimal4 }
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

            foreach (OptimalEnum optimal in testsDictionary.Keys)
            {
                Testing(optimal);
            }

            return stringBuilder;
        }

        /// <summary>
        /// Otestuje daný typ grafu
        /// </summary>
        /// <param name="optimal">daný typ grafu</param>
        /// <returns>Vrátí report</returns>
        public StringBuilder Test(OptimalEnum optimal)
        {
            stringBuilder.Clear();

            Testing(optimal);

            return stringBuilder;
        }

        private void Testing(OptimalEnum optimal)
        {
            try
            {
                testPath = GraphColoring.Tests.Tests.CreateTestFile(testsDictionary[optimal]);

                reader = new ReaderWriter.Reader(testPath, false);
                graph = reader.ReadFile();

                Optimal optimalAlgorithm = new Optimal(graph);
                optimalAlgorithm.Color();

                stringBuilder.AppendLine(optimal.ToString());
                stringBuilder.AppendLine("Graph colored.");
                stringBuilder.AppendLine(graph.GetColoredGraph().ToString());
            }
            catch (KeyNotFoundException)
            {
                throw new MyException.TestsMissingTestException(optimal.ToString());
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
            return testPathAlgorithmOptimal;
        }
        #endregion
    }
}
