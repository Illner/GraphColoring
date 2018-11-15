﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring.GraphVisualization.Tests
{
    class ConvertGraphToDotTest : GraphColoring.Tests.ITestInterface
    {
        // Variable
        #region
        private Graph.IGraphInterface graph;
        private String testPath;
        private ReaderWriter.IReaderGraphInterface reader;
        private StringBuilder stringBuilder;
        private Dictionary<ConverterGraphToDotEnum, string> testsDictionary;

        // Paths
        private string testPathConverterGraphToDot = @"Testing\Test\ConverterGraphToDot.txt";
        private string converterGraphToDot1 = ConvertGraphToDotResource.converter1;
        private string converterGraphToDot2 = ConvertGraphToDotResource.converter2;
        private string converterGraphToDot3 = ConvertGraphToDotResource.converter3;
        private string converterGraphToDot4 = ConvertGraphToDotResource.converter4;
        private string converterGraphToDot5 = ConvertGraphToDotResource.converter5;
        #endregion

        // Enum
        #region
        public enum ConverterGraphToDotEnum
        {
            converterGraphToDotEnum1,
            converterGraphToDotEnum2,
            converterGraphToDotEnum3,
            converterGraphToDotEnum4,
            converterGraphToDotEnum5
        }
        #endregion

        // Constructor
        #region
        public ConvertGraphToDotTest()
        {
            stringBuilder = new StringBuilder();

            // Fill testsDictionary
            testsDictionary = new Dictionary<ConverterGraphToDotEnum, string>
            {
                { ConverterGraphToDotEnum.converterGraphToDotEnum1, converterGraphToDot1 },
                { ConverterGraphToDotEnum.converterGraphToDotEnum2, converterGraphToDot2 },
                { ConverterGraphToDotEnum.converterGraphToDotEnum3, converterGraphToDot3 },
                { ConverterGraphToDotEnum.converterGraphToDotEnum4, converterGraphToDot4 },
                { ConverterGraphToDotEnum.converterGraphToDotEnum5, converterGraphToDot5 }
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

            foreach (ConverterGraphToDotEnum converterGraphToDotEnum in testsDictionary.Keys)
            {
                Testing(converterGraphToDotEnum);
            }

            return stringBuilder;
        }

        /// <summary>
        /// Otestuje daný typ grafu
        /// </summary>
        /// <param name="combination">daný typ grafu</param>
        /// <returns>Vrátí report</returns>
        public StringBuilder Test(ConverterGraphToDotEnum converterGraphToDotEnum)
        {
            stringBuilder.Clear();

            Testing(converterGraphToDotEnum);

            return stringBuilder;
        }

        private void Testing(ConverterGraphToDotEnum converterGraphToDotEnum)
        {
            try
            {
                testPath = GraphColoring.Tests.Tests.CreateTestFile(testsDictionary[converterGraphToDotEnum]);

                reader = new ReaderWriter.ReaderGraph(testPath, false);
                graph = reader.ReadFile();

                List<Graph.IGraphInterface> graphList = graph.GetGraphProperty().GetComponents();

                stringBuilder.AppendLine(converterGraphToDotEnum.ToString());
                stringBuilder.AppendLine(graph.ToString());
                ConvertGraphToDot convertGraphToDot = new ConvertGraphToDot(graphList);
                stringBuilder.AppendLine(convertGraphToDot.Convert());
            }
            catch (KeyNotFoundException)
            {
                throw new MyException.TestsException.TestsMissingTestException(converterGraphToDotEnum.ToString());
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
            return testPathConverterGraphToDot;
        }
        #endregion
    }
}