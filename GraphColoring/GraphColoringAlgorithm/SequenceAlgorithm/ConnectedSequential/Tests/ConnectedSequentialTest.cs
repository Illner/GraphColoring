using System;
using System.Text;
using System.Collections.Generic;

namespace GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.ConnectedSequential.Tests
{
    class ConnectedSequentialTest : GraphColoring.Tests.ITestInterface
    {
        #region Variable
        private Graph.IGraphInterface graph;
        private string testPath;
        private ReaderWriter.IReaderGraphInterface reader;
        private StringBuilder stringBuilder;
        private Dictionary<ConnectedSequentialEnum, string> testsDictionary;

        // Paths
        private string testPathAlgorithmConnectedSequential = @"Testing\Test\ConnectedSequential.txt";
        private string algorithmConnectedSequential1 = ConnectedSequentialResource.connectedSequential1;
        private string algorithmConnectedSequential2 = ConnectedSequentialResource.connectedSequential2;
        private string algorithmConnectedSequential3 = ConnectedSequentialResource.connectedSequential3;
        private string algorithmConnectedSequential4 = ConnectedSequentialResource.connectedSequential4;
        #endregion
        
        #region Enum
        public enum ConnectedSequentialEnum
        {
            connectedSequential1,
            connectedSequential2,
            connectedSequential3,
            connectedSequential4
        }
        #endregion
        
        #region Constructor
        public ConnectedSequentialTest()
        {
            stringBuilder = new StringBuilder();

            // Fill testsDictionary
            testsDictionary = new Dictionary<ConnectedSequentialEnum, string>
            {
                { ConnectedSequentialEnum.connectedSequential1, algorithmConnectedSequential1 },
                { ConnectedSequentialEnum.connectedSequential2, algorithmConnectedSequential2 },
                { ConnectedSequentialEnum.connectedSequential3, algorithmConnectedSequential3 },
                { ConnectedSequentialEnum.connectedSequential4, algorithmConnectedSequential4 }
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

            foreach (ConnectedSequentialEnum connectedSequential in testsDictionary.Keys)
            {
                Testing(connectedSequential);
            }

            return stringBuilder;
        }

        /// <summary>
        /// Test a particular enum (file)
        /// </summary>
        /// <param name="connectedSequential">enum (file)</param>
        /// <returns>report</returns>
        public StringBuilder Test(ConnectedSequentialEnum connectedSequential)
        {
            stringBuilder.Clear();

            Testing(connectedSequential);

            return stringBuilder;
        }

        private void Testing(ConnectedSequentialEnum connectedSequential)
        {
            try
            {
                testPath = ReaderWriter.ReaderWriter.CreateTestFile(testsDictionary[connectedSequential]);

                reader = new ReaderWriter.ReaderGraph(testPath, false);
                graph = reader.ReadFile();

                ConnectedSequential optimalAlgorithm = new ConnectedSequential(graph);
                optimalAlgorithm.Color();

                stringBuilder.AppendLine(connectedSequential.ToString());
                stringBuilder.AppendLine("Graph colored.");
                stringBuilder.AppendLine(graph.GetColoredGraph().ToString());
            }
            catch (KeyNotFoundException)
            {
                throw new MyException.TestsException.TestsMissingTestException(connectedSequential.ToString());
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
            return testPathAlgorithmConnectedSequential;
        }
        #endregion
    }
}
