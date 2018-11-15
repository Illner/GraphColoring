﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring.Graph.GraphProperty.Tests
{
    class BridgesCutVerticesTest : GraphColoring.Tests.ITestInterface
    {
        // Variable
        #region
        private IGraphInterface graph;
        private String testPath;
        private ReaderWriter.IReaderGraphInterface reader;
        private StringBuilder stringBuilder;
        private Dictionary<BridgesCutVerticesEnum, string> testsDictionary;

        // Paths
        private string testPathBridgesCutVertices = @"Testing\Test\GraphBridgesCutVertices.txt";
        private string bridgesCutVertices1 = BridgesCutVerticesResource.bridgesCutVerticesTest1;
        private string bridgesCutVertices2 = BridgesCutVerticesResource.bridgesCutVerticesTest2;
        private string bridgesCutVertices3 = BridgesCutVerticesResource.bridgesCutVerticesTest3;
        private string bridgesCutVertices4 = BridgesCutVerticesResource.bridgesCutVerticesTest4;
        private string bridgesCutVertices5 = BridgesCutVerticesResource.bridgesCutVerticesTest5;
        #endregion

        // Enum
        #region
        public enum BridgesCutVerticesEnum
        {
            bridgesCutVertices1,
            bridgesCutVertices2,
            bridgesCutVertices3,
            bridgesCutVertices4,
            bridgesCutVertices5
        }
        #endregion

        // Constructor
        #region
        public BridgesCutVerticesTest()
        {
            stringBuilder = new StringBuilder();

            // Fill testsDictionary
            testsDictionary = new Dictionary<BridgesCutVerticesEnum, string>
            {
                { BridgesCutVerticesEnum.bridgesCutVertices1, bridgesCutVertices1 },
                { BridgesCutVerticesEnum.bridgesCutVertices2, bridgesCutVertices2 },
                { BridgesCutVerticesEnum.bridgesCutVertices3, bridgesCutVertices3 },
                { BridgesCutVerticesEnum.bridgesCutVertices4, bridgesCutVertices4 },
                { BridgesCutVerticesEnum.bridgesCutVertices5, bridgesCutVertices5 }
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

            foreach (BridgesCutVerticesEnum bridgesCutVerticesEnum in testsDictionary.Keys)
            {
                Testing(bridgesCutVerticesEnum);
            }

            return stringBuilder;
        }

        /// <summary>
        /// Otestuje daný typ grafu
        /// </summary>
        /// <param name="bridgesCutVerticesEnum">daný typ grafu</param>
        /// <returns>Vrátí report</returns>
        public StringBuilder Test(BridgesCutVerticesEnum bridgesCutVerticesEnum)
        {
            stringBuilder.Clear();

            Testing(bridgesCutVerticesEnum);

            return stringBuilder;
        }

        private void Testing(BridgesCutVerticesEnum bridgesCutVerticesEnum)
        {
            try
            {
                testPath = GraphColoring.Tests.Tests.CreateTestFile(testsDictionary[bridgesCutVerticesEnum]);

                reader = new ReaderWriter.ReaderGraph(testPath, false);
                graph = reader.ReadFile();

                stringBuilder.AppendLine(bridgesCutVerticesEnum.ToString());
                stringBuilder.AppendLine("Graph created.");
                stringBuilder.AppendLine(graph.ToString());

                stringBuilder.AppendLine("Number of cut vertices: " + graph.GetGraphProperty().GetCutVertices().Count);
                stringBuilder.AppendLine("Cut vertices: ");
                graph.GetGraphProperty().GetCutVertices().ForEach(x => { stringBuilder.AppendLine(x.GetUserName()); });
                stringBuilder.AppendLine("Number of bridges: " + graph.GetGraphProperty().GetBridges().Count);
                stringBuilder.AppendLine("Bridges: ");
                graph.GetGraphProperty().GetBridges().ForEach(x => { stringBuilder.AppendLine(x.GetVertex1().GetUserName() + " " + x.GetVertex2().GetUserName()); });

                graph.GetGraphProperty().Reset();
                stringBuilder.AppendLine("Number of cut vertices: " + graph.GetGraphProperty().GetCutVertices().Count);
                stringBuilder.AppendLine("Number of bridges: " + graph.GetGraphProperty().GetBridges().Count);
            }
            catch (KeyNotFoundException)
            {
                throw new MyException.TestsException.TestsMissingTestException(bridgesCutVerticesEnum.ToString());
            }
            catch (MyException.ReaderWriterException.ReaderWriterException e)
            {
                stringBuilder.AppendLine(e.Message);
            }
            catch (MyException.GraphException.GraphIsNotConnected e)
            {
                stringBuilder.AppendLine(e.Message);
            }
        }
        #endregion

        // Property
        #region
        public string GetPath()
        {
            return testPathBridgesCutVertices;
        }
        #endregion
    }
}