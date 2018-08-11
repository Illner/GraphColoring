using System;
using System.Collections.Generic;
using System.Text;

namespace GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence.Tests
{
    class LargestFirstSequenceTest : GraphColoring.Tests.ITestInterface
    {
        // Variable
        #region
        private Graph.Graph graph;
        private String testPath;
        private ReaderWriter.Reader reader;
        private StringBuilder stringBuilder;
        private Dictionary<LargestFirstSequenceEnum, string> testsDictionary;

        // Paths
        private string testPathAlgorithmLargestFirstSequence = @"Testing\Test\LargestFirstSequence.txt";
        private string algorithmLargestFirstSequence1 = LargestFirstSequenceResource.largestFirstSequence1;
        private string algorithmLargestFirstSequence2 = LargestFirstSequenceResource.largestFirstSequence2;
        private string algorithmLargestFirstSequence3 = LargestFirstSequenceResource.largestFirstSequence3;
        private string algorithmLargestFirstSequence4 = LargestFirstSequenceResource.largestFirstSequence4;
        #endregion

        // Enum
        #region
        public enum LargestFirstSequenceEnum
        {
            largestFirstSequence1,
            largestFirstSequence2,
            largestFirstSequence3,
            largestFirstSequence4
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
                { LargestFirstSequenceEnum.largestFirstSequence4, algorithmLargestFirstSequence4 }
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

            foreach (LargestFirstSequenceEnum largestFirstSequence in testsDictionary.Keys)
            {
                Testing(largestFirstSequence);
            }

            return stringBuilder;
        }

        /// <summary>
        /// Otestuje daný typ grafu
        /// </summary>
        /// <param name="largestFirstSequence">daný typ grafu</param>
        /// <returns>Vrátí report</returns>
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
                testPath = GraphColoring.Tests.Tests.CreateTestFile(testsDictionary[largestFirstSequence]);

                reader = new ReaderWriter.Reader(testPath, false);
                graph = reader.ReadFile();

                LargestFirstSequence largestFirstSequenceAlgorithm = new LargestFirstSequence(graph);
                largestFirstSequenceAlgorithm.Color();

                stringBuilder.AppendLine(largestFirstSequence.ToString());
                stringBuilder.AppendLine("Graph colored.");
                stringBuilder.AppendLine(graph.GetColoredGraph().ToString());
            }
            catch (KeyNotFoundException)
            {
                throw new MyException.TestsMissingTestException(largestFirstSequence.ToString());
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
            return testPathAlgorithmLargestFirstSequence;
        }
        #endregion
    }
}
