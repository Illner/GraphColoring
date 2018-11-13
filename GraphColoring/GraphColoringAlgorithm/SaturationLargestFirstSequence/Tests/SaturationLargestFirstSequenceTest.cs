using System;
using System.Collections.Generic;
using System.Text;

namespace GraphColoring.GraphColoringAlgorithm.SaturationLargestFirstSequence.Tests
{
    class SaturationLargestFirstSequenceTest : GraphColoring.Tests.ITestInterface
    {
        // Variable
        #region
        private Graph.IGraphInterface graph;
        private String testPath;
        private ReaderWriter.IReaderGraphInterface reader;
        private StringBuilder stringBuilder;
        private Dictionary<SaturationLargestFirstSequenceEnum, string> testsDictionary;

        // Paths
        private string testPathAlgorithmSaturationLargestFirstSequence = @"Testing\Test\SaturationLargestFirstSequence.txt";
        private string algorithmSaturationLargestFirstSequence1 = SaturationLargestFirstSequenceResource.saturationLF1;
        private string algorithmSaturationLargestFirstSequence2 = SaturationLargestFirstSequenceResource.saturationLF2;
        private string algorithmSaturationLargestFirstSequence3 = SaturationLargestFirstSequenceResource.saturationLF3;
        private string algorithmSaturationLargestFirstSequence4 = SaturationLargestFirstSequenceResource.saturationLF4;
        private string algorithmSaturationLargestFirstSequence5 = SaturationLargestFirstSequenceResource.saturationLF5;
        #endregion

        // Enum
        #region
        public enum SaturationLargestFirstSequenceEnum
        {
            saturationLargestFirstSequence1,
            saturationLargestFirstSequence2,
            saturationLargestFirstSequence3,
            saturationLargestFirstSequence4,
            saturationLargestFirstSequence5
        }
        #endregion

        // Constructor
        #region
        public SaturationLargestFirstSequenceTest()
        {
            stringBuilder = new StringBuilder();

            // Fill testsDictionary
            testsDictionary = new Dictionary<SaturationLargestFirstSequenceEnum, string>
            {
                { SaturationLargestFirstSequenceEnum.saturationLargestFirstSequence1, algorithmSaturationLargestFirstSequence1 },
                { SaturationLargestFirstSequenceEnum.saturationLargestFirstSequence2, algorithmSaturationLargestFirstSequence2 },
                { SaturationLargestFirstSequenceEnum.saturationLargestFirstSequence3, algorithmSaturationLargestFirstSequence3 },
                { SaturationLargestFirstSequenceEnum.saturationLargestFirstSequence4, algorithmSaturationLargestFirstSequence4 },
                { SaturationLargestFirstSequenceEnum.saturationLargestFirstSequence5, algorithmSaturationLargestFirstSequence5 }
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

            foreach (SaturationLargestFirstSequenceEnum saturationLargestFirstSequence in testsDictionary.Keys)
            {
                Testing(saturationLargestFirstSequence);
            }

            return stringBuilder;
        }

        /// <summary>
        /// Otestuje daný typ grafu
        /// </summary>
        /// <param name="saturationLargestFirstSequence">daný typ grafu</param>
        /// <returns>Vrátí report</returns>
        public StringBuilder Test(SaturationLargestFirstSequenceEnum saturationLargestFirstSequence)
        {
            stringBuilder.Clear();

            Testing(saturationLargestFirstSequence);

            return stringBuilder;
        }

        private void Testing(SaturationLargestFirstSequenceEnum saturationLargestFirstSequence)
        {
            try
            {
                testPath = GraphColoring.Tests.Tests.CreateTestFile(testsDictionary[saturationLargestFirstSequence]);

                reader = new ReaderWriter.ReaderGraph(testPath, false);
                graph = reader.ReadFile();

                SaturationLargestFirstSequence saturationLargestFirstSequenceAlgorithm = new SaturationLargestFirstSequence(graph);
                saturationLargestFirstSequenceAlgorithm.Color();

                stringBuilder.AppendLine(saturationLargestFirstSequence.ToString());
                stringBuilder.AppendLine("Graph colored.");
                stringBuilder.AppendLine(graph.GetColoredGraph().ToString());
            }
            catch (KeyNotFoundException)
            {
                throw new MyException.TestsException.TestsMissingTestException(saturationLargestFirstSequence.ToString());
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
            return testPathAlgorithmSaturationLargestFirstSequence;
        }
        #endregion
    }
}
