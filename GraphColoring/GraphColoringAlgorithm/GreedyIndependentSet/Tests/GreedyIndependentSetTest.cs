﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring.GraphColoringAlgorithm.GreedyIndependentSet.Tests
{
    class GreedyIndependentSetTest : GraphColoring.Tests.ITestInterface
    {
        // Variable
        #region
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

        // Enum
        #region
        public enum GreedyIndependentSetEnum
        {
            greedyIndependentSetEnum1,
            greedyIndependentSetEnum2,
            greedyIndependentSetEnum3,
            greedyIndependentSetEnum4,
            greedyIndependentSetEnum5
        }
        #endregion

        // Constructor
        #region
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

        // Method
        #region
        /// <summary>
        /// Otestuje všechny typy grafů
        /// </summary>
        /// <returns>Vrátí report</returns>
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
        /// Otestuje daný typ grafu
        /// </summary>
        /// <param name="greedyIndependentSet">daný typ grafu</param>
        /// <returns>Vrátí report</returns>
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
                testPath = GraphColoring.Tests.Tests.CreateTestFile(testsDictionary[greedyIndependentSet]);

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

        // Property
        #region
        public string GetPath()
        {
            return testPathAlgorithmGreedyIndependentSet;
        }
        #endregion
    }
}