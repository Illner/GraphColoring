using System;
using System.Text;
using System.Collections.Generic;

namespace GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.Tests
{
    class SmallestLastSequenceTest : GraphColoring.Tests.ITestInterface
    {
        // Variable
        #region
        private Graph.IGraphInterface graph;
        private String testPath;
        private ReaderWriter.IReaderGraphInterface reader;
        private StringBuilder stringBuilder;
        private Dictionary<SmallestLastSequenceEnum, string> testsDictionary;

        // Paths
        private string testPathAlgorithmSmallestLastSequence = @"Testing\Test\SmallestLastSequence.txt";
        private string algorithmSmallestLastSequence1 = SmallestLastSequenceResource.smallestLastSequence1;
        private string algorithmSmallestLastSequence2 = SmallestLastSequenceResource.smallestLastSequence2;
        private string algorithmSmallestLastSequence3 = SmallestLastSequenceResource.smallestLastSequence3;
        private string algorithmSmallestLastSequence4 = SmallestLastSequenceResource.smallestLastSequence4;
        private string algorithmSmallestLastSequence5 = SmallestLastSequenceResource.smallestLastSequence5;
        #endregion

        // Enum
        #region
        public enum SmallestLastSequenceEnum
        {
            smallestLastSequence1,
            smallestLastSequence2,
            smallestLastSequence3,
            smallestLastSequence4,
            smallestLastSequence5
        }
        #endregion

        // Constructor
        #region
        public SmallestLastSequenceTest()
        {
            stringBuilder = new StringBuilder();

            // Fill testsDictionary
            testsDictionary = new Dictionary<SmallestLastSequenceEnum, string>
            {
                { SmallestLastSequenceEnum.smallestLastSequence1, algorithmSmallestLastSequence1 },
                { SmallestLastSequenceEnum.smallestLastSequence2, algorithmSmallestLastSequence2 },
                { SmallestLastSequenceEnum.smallestLastSequence3, algorithmSmallestLastSequence3 },
                { SmallestLastSequenceEnum.smallestLastSequence4, algorithmSmallestLastSequence4 },
                { SmallestLastSequenceEnum.smallestLastSequence5, algorithmSmallestLastSequence5 }
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

            foreach (SmallestLastSequenceEnum smallestLastSequence in testsDictionary.Keys)
            {
                Testing(smallestLastSequence);
            }

            return stringBuilder;
        }

        /// <summary>
        /// Test a particular enum (file)
        /// </summary>
        /// <param name="smallestLastSequence">enum (file)</param>
        /// <returns>report</returns>
        public StringBuilder Test(SmallestLastSequenceEnum smallestLastSequence)
        {
            stringBuilder.Clear();

            Testing(smallestLastSequence);

            return stringBuilder;
        }

        private void Testing(SmallestLastSequenceEnum smallestLastSequence)
        {
            try
            {
                testPath = ReaderWriter.ReaderWriter.CreateTestFile(testsDictionary[smallestLastSequence]);

                reader = new ReaderWriter.ReaderGraph(testPath, false);
                graph = reader.ReadFile();

                SmallestLastSequence smallestLastSequenceAlgorithm = new SmallestLastSequence(graph);
                smallestLastSequenceAlgorithm.Color();

                stringBuilder.AppendLine(smallestLastSequence.ToString());
                stringBuilder.AppendLine("Graph colored.");
                stringBuilder.AppendLine(graph.GetColoredGraph().ToString());
            }
            catch (KeyNotFoundException)
            {
                throw new MyException.TestsException.TestsMissingTestException(smallestLastSequence.ToString());
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
            return testPathAlgorithmSmallestLastSequence;
        }
        #endregion
    }
}
