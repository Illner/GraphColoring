﻿using System;
using System.Text;
using System.Collections.Generic;

namespace GraphColoring.Graph.GraphProperty.Tests
{
    class DegreeSequenceTest : GraphColoring.Tests.ITestInterface
    {
        // Variable
        #region
        private Graph graph;
        private String testPath;
        private ReaderWriter.Reader reader;
        private StringBuilder stringBuilder;
        private Dictionary<DegreeSequenceEnum, string> testsDictionary;

        // Paths
        private string testPathGraphDegreeSequence = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Test\GraphDegreeSequence.txt";
        private string graphDegreeSequence1Path = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\SequencesAndPolynomials\graphDegreeSequence1.graph";
        private string graphDegreeSequence2Path = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\SequencesAndPolynomials\graphDegreeSequence2.graph";
        private string graphDegreeSequence3Path = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\SequencesAndPolynomials\graphDegreeSequence3.graph";
        private string graphDegreeSequence4Path = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\SequencesAndPolynomials\graphDegreeSequence4.graph";
        #endregion

        // Enum
        #region
        public enum DegreeSequenceEnum
        {
            graphDegreeSequence1,
            graphDegreeSequence2,
            graphDegreeSequence3,
            graphDegreeSequence4
        }
        #endregion

        // Constructor
        #region
        public DegreeSequenceTest()
        {
            stringBuilder = new StringBuilder();

            // Fill testsDictionary
            testsDictionary = new Dictionary<DegreeSequenceEnum, string>
            {
                { DegreeSequenceEnum.graphDegreeSequence1, graphDegreeSequence1Path },
                { DegreeSequenceEnum.graphDegreeSequence2, graphDegreeSequence2Path },
                { DegreeSequenceEnum.graphDegreeSequence3, graphDegreeSequence3Path },
                { DegreeSequenceEnum.graphDegreeSequence4, graphDegreeSequence4Path }
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

            foreach (DegreeSequenceEnum degreeSequenceEnum in testsDictionary.Keys)
            {
                Testing(degreeSequenceEnum);
            }

            return stringBuilder;
        }

        /// <summary>
        /// Otestuje daný typ grafu
        /// </summary>
        /// <param name="degreeSequenceEnum">daný typ grafu</param>
        /// <returns>Vrátí report</returns>
        public StringBuilder Test(DegreeSequenceEnum degreeSequenceEnum)
        {
            stringBuilder.Clear();

            Testing(degreeSequenceEnum);

            return stringBuilder;
        }

        private void Testing(DegreeSequenceEnum degreeSequenceEnum)
        {
            try
            {
                testPath = testsDictionary[degreeSequenceEnum];
                
                reader = new ReaderWriter.Reader(testPath);
                graph = reader.ReadFile();

                stringBuilder.AppendLine(degreeSequenceEnum.ToString());
                stringBuilder.AppendLine("Graph created.");
                stringBuilder.AppendLine(graph.ToString());

                List<int> degreeSequenceList = graph.GetGraphProperty().GetDegreeSequence();
                
                stringBuilder.AppendLine("Degree sequence");
                foreach (int degree in degreeSequenceList)
                {
                    stringBuilder.Append(degree + " ");
                }

                stringBuilder.AppendLine("");
                stringBuilder.AppendLine("Minimum vertex degree: " + graph.GetGraphProperty().GetMinimumVertexDegree());
                stringBuilder.AppendLine("Maximum vertex degree: " + graph.GetGraphProperty().GetMaximumVertexDegree());
                stringBuilder.AppendLine("Is graph regular: " + graph.GetGraphProperty().GetIsRegular());
            }
            catch (KeyNotFoundException)
            {
                throw new MyException.TestsMissingTestException(degreeSequenceEnum.ToString());
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
            return testPathGraphDegreeSequence;
        }
        #endregion
    }
}
