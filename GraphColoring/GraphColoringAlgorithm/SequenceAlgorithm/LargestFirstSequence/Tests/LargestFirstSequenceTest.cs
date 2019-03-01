using System;
using System.Text;
using System.Collections.Generic;

namespace GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence.Tests
{
    class LargestFirstSequenceTest : GraphColoring.Tests.ITestInterface
    {
        // Variable
        #region
        private Graph.IGraphInterface graph;
        private String testPath;
        private ReaderWriter.IReaderGraphInterface reader;
        private StringBuilder stringBuilder;
        private Dictionary<LargestFirstSequenceEnum, string> testsDictionary;

        // Paths
        private string testPathAlgorithmLargestFirstSequence = @"Testing\Test\LargestFirstSequence.txt";
        private string algorithmLargestFirstSequence1 = LargestFirstSequenceResource.largestFirstSequence1;
        private string algorithmLargestFirstSequence2 = LargestFirstSequenceResource.largestFirstSequence2;
        private string algorithmLargestFirstSequence3 = LargestFirstSequenceResource.largestFirstSequence3;
        private string algorithmLargestFirstSequence4 = LargestFirstSequenceResource.largestFirstSequence4;
        private string algorithmLargestFirstSequence5 = LargestFirstSequenceResource.largestFirstSequence5;
        #endregion

        // Enum
        #region
        public enum LargestFirstSequenceEnum
        {
            largestFirstSequence1,
            largestFirstSequence2,
            largestFirstSequence3,
            largestFirstSequence4,
            largestFirstSequence5
        }
        #endregion

        // Constructor
        #region
        public LargestFirstSequenceTest()
        {
            stringBuilder = new StringBuilder();

            // Fill testsDictionary
            testsDictionary = new Dictionary<LargestFirstSequenceEnum, string>
            {
                { LargestFirstSequenceEnum.largestFirstSequence1, algorithmLargestFirstSequence1 },
                { LargestFirstSequenceEnum.largestFirstSequence2, algorithmLargestFirstSequence2 },
                { LargestFirstSequenceEnum.largestFirstSequence3, algorithmLargestFirstSequence3 },
                { LargestFirstSequenceEnum.largestFirstSequence4, algorithmLargestFirstSequence4 },
                { LargestFirstSequenceEnum.largestFirstSequence5, algorithmLargestFirstSequence5 }
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

            foreach (LargestFirstSequenceEnum largestFirstSequence in testsDictionary.Keys)
            {
                Testing(largestFirstSequence);
            }

            return stringBuilder;
        }

        /// <summary>
        /// Test a particular enum (file)
        /// </summary>
        /// <param name="largestFirstSequence">enum (file)</param>
        /// <returns>report</returns>
        public StringBuilder Test(LargestFirstSequenceEnum largestFirstSequence)
        {
            stringBuilder.Clear();

            Testing(largestFirstSequence);

            return stringBuilder;
        }

        private void Testing(LargestFirstSequenceEnum largestFirstSequence)
        {
            try
            {
                testPath = ReaderWriter.ReaderWriter.CreateTestFile(testsDictionary[largestFirstSequence]);

                reader = new ReaderWriter.ReaderGraph(testPath, false);
                graph = reader.ReadFile();

                LargestFirstSequence largestFirstSequenceAlgorithm = new LargestFirstSequence(graph);
                largestFirstSequenceAlgorithm.Color();

                stringBuilder.AppendLine(largestFirstSequence.ToString());
                stringBuilder.AppendLine("Graph colored.");
                stringBuilder.AppendLine(graph.GetColoredGraph().ToString());
            }
            catch (KeyNotFoundException)
            {
                throw new MyException.TestsException.TestsMissingTestException(largestFirstSequence.ToString());
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
            return testPathAlgorithmLargestFirstSequence;
        }
        #endregion
    }
}
