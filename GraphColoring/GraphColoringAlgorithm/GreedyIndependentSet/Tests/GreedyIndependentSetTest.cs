using System;
using System.Text;
using System.Collections.Generic;

namespace GraphColoring.GraphColoringAlgorithm.GreedyIndependentSet.Tests
{
    class GreedyIndependentSetTest : GraphColoring.Tests.ITestInterface
    {
        #region Variable
        private Graph.IGraphInterface graph;
        private String testPath;
        private ReaderWriter.IReaderGraphInterface reader;
        private StringBuilder stringBuilder;
        private Dictionary<GreedyIndependentSetEnum, string> testsDictionary;

        // Paths
        private string testPathAlgorithmGreedyIndependentSet = @"Testing\Test\GreedyIndependentSet.txt";
        private string algorithmGreedyIndependentSet1 = GreedyIndependentSetResource.greedyIndependentSet1;
        private string algorithmGreedyIndependentSet2 = GreedyIndependentSetResource.greedyIndependentSet2;
        private string algorithmGreedyIndependentSet3 = GreedyIndependentSetResource.greedyIndependentSet3;
        private string algorithmGreedyIndependentSet4 = GreedyIndependentSetResource.greedyIndependentSet4;
        private string algorithmGreedyIndependentSet5 = GreedyIndependentSetResource.greedyIndependentSet5;
        #endregion
        
        #region Enum
        public enum GreedyIndependentSetEnum
        {
            greedyIndependentSetEnum1,
            greedyIndependentSetEnum2,
            greedyIndependentSetEnum3,
            greedyIndependentSetEnum4,
            greedyIndependentSetEnum5
        }
        #endregion
        
        #region Constructor
        public GreedyIndependentSetTest()
        {
            stringBuilder = new StringBuilder();

            // Fill testsDictionary
            testsDictionary = new Dictionary<GreedyIndependentSetEnum, string>
            {
                { GreedyIndependentSetEnum.greedyIndependentSetEnum1, algorithmGreedyIndependentSet1 },
                { GreedyIndependentSetEnum.greedyIndependentSetEnum2, algorithmGreedyIndependentSet2 },
                { GreedyIndependentSetEnum.greedyIndependentSetEnum3, algorithmGreedyIndependentSet3 },
                { GreedyIndependentSetEnum.greedyIndependentSetEnum4, algorithmGreedyIndependentSet4 },
                { GreedyIndependentSetEnum.greedyIndependentSetEnum5, algorithmGreedyIndependentSet5 }
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

            foreach (GreedyIndependentSetEnum greedyIndependentSet in testsDictionary.Keys)
            {
                Testing(greedyIndependentSet);
            }

            return stringBuilder;
        }

        /// <summary>
        /// Test a particular enum (file)
        /// </summary>
        /// <returns>report</returns>
        public StringBuilder Test(GreedyIndependentSetEnum greedyIndependentSet)
        {
            stringBuilder.Clear();

            Testing(greedyIndependentSet);

            return stringBuilder;
        }

        private void Testing(GreedyIndependentSetEnum greedyIndependentSet)
        {
            try
            {
                testPath = ReaderWriter.ReaderWriter.CreateTestFile(testsDictionary[greedyIndependentSet]);

                reader = new ReaderWriter.ReaderGraph(testPath, false);
                graph = reader.ReadFile();

                GreedyIndependentSet greedyIndependentSetAlgorithm = new GreedyIndependentSet(graph);
                greedyIndependentSetAlgorithm.Color();

                stringBuilder.AppendLine(greedyIndependentSetAlgorithm.ToString());
                stringBuilder.AppendLine("Graph colored.");
                stringBuilder.AppendLine(graph.GetColoredGraph().ToString());
            }
            catch (KeyNotFoundException)
            {
                throw new MyException.TestsException.TestsMissingTestException(greedyIndependentSet.ToString());
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
            return testPathAlgorithmGreedyIndependentSet;
        }
        #endregion
    }
}
