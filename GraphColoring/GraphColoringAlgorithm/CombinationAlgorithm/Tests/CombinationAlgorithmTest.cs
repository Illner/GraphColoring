﻿using System;
using System.Text;
using System.Collections.Generic;

namespace GraphColoring.GraphColoringAlgorithm.CombinationAlgorithm.Tests
{
    class CombinationAlgorithmTest : GraphColoring.Tests.ITestInterface
    {
        #region Variable
        private Graph.IGraphInterface graph;
        private String testPath;
        private ReaderWriter.IReaderGraphInterface reader;
        private StringBuilder stringBuilder;
        private Dictionary<CombinationEnum, string> testsDictionary;

        // Paths
        private string testPathAlgorithmCombination = @"Testing\Test\Combination.txt";
        private string algorithmCombination1 = CombinationAlgorithmResource.combination1;
        private string algorithmCombination2 = CombinationAlgorithmResource.combination2;
        private string algorithmCombination3 = CombinationAlgorithmResource.combination3;
        private string algorithmCombination4 = CombinationAlgorithmResource.combination4;
        private string algorithmCombination5 = CombinationAlgorithmResource.combination5;
        #endregion
        
        #region Enum
        public enum CombinationEnum
        {
            combinationEnum1,
            combinationEnum2,
            combinationEnum3,
            combinationEnum4,
            combinationEnum5
        }
        #endregion
        
        #region Constructor
        public CombinationAlgorithmTest()
        {
            stringBuilder = new StringBuilder();

            // Fill testsDictionary
            testsDictionary = new Dictionary<CombinationEnum, string>
            {
                { CombinationEnum.combinationEnum1, algorithmCombination1 },
                { CombinationEnum.combinationEnum2, algorithmCombination2 },
                { CombinationEnum.combinationEnum3, algorithmCombination3 },
                { CombinationEnum.combinationEnum4, algorithmCombination4 },
                { CombinationEnum.combinationEnum5, algorithmCombination5 }
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

            foreach (CombinationEnum combination in testsDictionary.Keys)
            {
                Testing(combination);
            }

            return stringBuilder;
        }

        /// <summary>
        /// Test a particular enum (file)
        /// </summary>
        /// <returns>report</returns>
        public StringBuilder Test(CombinationEnum combination)
        {
            stringBuilder.Clear();

            Testing(combination);

            return stringBuilder;
        }

        private void Testing(CombinationEnum combination)
        {
            try
            {
                testPath = ReaderWriter.ReaderWriter.CreateTestFile(testsDictionary[combination]);

                reader = new ReaderWriter.ReaderGraph(testPath, false);
                graph = reader.ReadFile();

                CombinationAlgorithm combinationAlgorithm = new CombinationAlgorithm(graph);
                combinationAlgorithm.Color();

                stringBuilder.AppendLine(combinationAlgorithm.ToString());
                stringBuilder.AppendLine("Graph colored.");
                stringBuilder.AppendLine(graph.GetColoredGraph().ToString());
            }
            catch (KeyNotFoundException)
            {
                throw new MyException.TestsException.TestsMissingTestException(combination.ToString());
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
            return testPathAlgorithmCombination;
        }
        #endregion
    }
}
