﻿using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace GraphColoring.Graph.GraphOperation.Tests
{
    class SubGraphTest : GraphColoring.Tests.ITestInterface
    {
        #region Variable
        private IGraphInterface graph;
        private String testPath;
        private int countVertices;
        private ReaderWriter.IReaderGraphInterface reader;
        private StringBuilder stringBuilder;
        // Tuple<cesta k souboru, pocet vrcholu v podgrafu>
        private Dictionary<SubGraphEnum, Tuple<string,int>> testsDictionary;

        // Paths
        private string testPathGraphSubGraph = @"Testing\Test\GraphSubGraph.txt";
        private string graphSubGraph1 = SubGraphResource.graphSubGraph1;
        private string graphSubGraph2 = SubGraphResource.graphSubGraph1;
        private string graphSubGraph3 = SubGraphResource.graphSubGraph2;
        private string graphSubGraph4 = SubGraphResource.graphSubGraph3;
        private string graphSubGraph5 = SubGraphResource.graphSubGraph4;
        private string graphSubGraph6 = SubGraphResource.graphSubGraph4;
        #endregion
        
        #region Enum
        public enum SubGraphEnum
        {
            graphSubGraph1,
            graphSubGraph2,
            graphSubGraph3,
            graphSubGraph4,
            graphSubGraph5,
            graphSubGraph6
        }
        #endregion
        
        #region Constructor
        public SubGraphTest()
        {
            stringBuilder = new StringBuilder();

            // Fill testsDictionary
            testsDictionary = new Dictionary<SubGraphEnum, Tuple<string, int>>
            {
                { SubGraphEnum.graphSubGraph1, new Tuple<string, int>(graphSubGraph1, 4) },
                { SubGraphEnum.graphSubGraph2, new Tuple<string, int>(graphSubGraph2, 6) },
                { SubGraphEnum.graphSubGraph3, new Tuple<string, int>(graphSubGraph3, 3) },
                { SubGraphEnum.graphSubGraph4, new Tuple<string, int>(graphSubGraph4, 4) },
                { SubGraphEnum.graphSubGraph5, new Tuple<string, int>(graphSubGraph5, 1) },
                { SubGraphEnum.graphSubGraph6, new Tuple<string, int>(graphSubGraph6, 0) }
            };
        }
        #endregion
        
        #region Method
        /// <summary>
        /// Test all values of enum (file)
        /// </summary>
        /// <returns>report</returns>
        public StringBuilder Test()
        {
            stringBuilder.Clear();

            foreach (SubGraphEnum subGraphEnum in testsDictionary.Keys)
            {
                Testing(subGraphEnum);
            }

            return stringBuilder;
        }

        /// <summary>
        /// Test a particular enum (file)
        /// </summary>
        /// <param name="subGraphEnum">enum (file)</param>
        /// <returns>report</returns>
        public StringBuilder Test(SubGraphEnum subGraphEnum)
        {
            stringBuilder.Clear();

            Testing(subGraphEnum);

            return stringBuilder;
        }

        private void Testing(SubGraphEnum subGraphEnum)
        {
            try
            {
                testPath = ReaderWriter.ReaderWriter.CreateTestFile(testsDictionary[subGraphEnum].Item1);
                countVertices = testsDictionary[subGraphEnum].Item2;

                reader = new ReaderWriter.ReaderGraph(testPath, false);
                graph = reader.ReadFile();

                stringBuilder.AppendLine(subGraphEnum.ToString());
                stringBuilder.AppendLine("Graph created.");
                stringBuilder.AppendLine(graph.ToString());

                IGraphInterface subGraph = GraphOperation.SubGraph(graph, graph.AllVertices().Take(countVertices).ToList());

                stringBuilder.AppendLine("Subgraph created.");
                stringBuilder.AppendLine(subGraph.ToString());
            }
            catch (KeyNotFoundException)
            {
                throw new MyException.TestsException.TestsMissingTestException(subGraphEnum.ToString());
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
            return testPathGraphSubGraph;
        }
        #endregion
    }
}
