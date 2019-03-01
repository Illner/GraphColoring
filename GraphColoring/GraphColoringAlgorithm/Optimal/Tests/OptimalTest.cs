using System;
using System.Text;
using System.Collections.Generic;

namespace GraphColoring.GraphColoringAlgorithm.Optimal.Tests
{
    class OptimalTest : GraphColoring.Tests.ITestInterface
    {
        // Variable
        #region
        private Graph.IGraphInterface graph;
        private String testPath;
        private ReaderWriter.IReaderGraphInterface reader;
        private StringBuilder stringBuilder;
        private Dictionary<OptimalEnum, string> testsDictionary;

        // Paths
        private string testPathAlgorithmOptimal = @"Testing\Test\Optimal.txt";
        private string algorithmOptimal1 = OptimalResource.optimal1;
        private string algorithmOptimal2 = OptimalResource.optimal2;
        private string algorithmOptimal3 = OptimalResource.optimal3;
        private string algorithmOptimal4 = OptimalResource.optimal4;
        private string algorithmOptimal5 = OptimalResource.optimal5;
        #endregion

        // Enum
        #region
        public enum OptimalEnum
        {
            optimal1,
            optimal2,
            optimal3,
            optimal4,
            optimal5
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
                { OptimalEnum.optimal4, algorithmOptimal4 },
                { OptimalEnum.optimal5, algorithmOptimal5 }
            };
        }
        #endregion

        // Method
        #region
        /// <summary>
        /// Test all values of enum (files)
        /// </summary>
        /// <returns>report</returns>
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
        /// Test a particular enum (file)
        /// </summary>
        /// <returns>report</returns>
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
                testPath = ReaderWriter.ReaderWriter.CreateTestFile(testsDictionary[optimal]);

                reader = new ReaderWriter.ReaderGraph(testPath, false);
                graph = reader.ReadFile();

                Optimal optimalAlgorithm = new Optimal(graph);
                optimalAlgorithm.Color();

                stringBuilder.AppendLine(optimal.ToString());
                stringBuilder.AppendLine("Graph colored.");
                stringBuilder.AppendLine(graph.GetColoredGraph().ToString());
            }
            catch (KeyNotFoundException)
            {
                throw new MyException.TestsException.TestsMissingTestException(optimal.ToString());
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
            return testPathAlgorithmOptimal;
        }
        #endregion
    }
}
