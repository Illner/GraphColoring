﻿using System;
using System.Text;
using System.Collections.Generic;

namespace GraphColoring.GraphColoringAlgorithm.SaturationLargestFirstSequence.Tests
{
    class SaturationLargestFirstSequenceTest : GraphColoring.Tests.ITestInterface
    {
        #region Variable
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
        
        #region Enum
        public enum SaturationLargestFirstSequenceEnum
        {
            saturationLargestFirstSequence1,
            saturationLargestFirstSequence2,
            saturationLargestFirstSequence3,
            saturationLargestFirstSequence4,
            saturationLargestFirstSequence5
        }
        #endregion
        
        #region Constructor
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
        
        #region Method
        /// <summary>
        /// Test all values of enum (files)
        /// </summary>
        /// <returns>report</returns>
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
        /// Test a particular enum (file)
        /// </summary>
        /// <returns>report</returns>
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
                testPath = ReaderWriter.ReaderWriter.CreateTestFile(testsDictionary[saturationLargestFirstSequence]);

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
        
        #region Property
        public string GetPath()
        {
            return testPathAlgorithmSaturationLargestFirstSequence;
        }
        #endregion
    }
}
