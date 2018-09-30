using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.ConnectedSequential.Tests
{
    class ConnectedSequentialTest : GraphColoring.Tests.ITestInterface
    {
        // Variable
        #region
        private Graph.IGraphInterface graph;
        private String testPath;
        private ReaderWriter.Reader reader;
        private StringBuilder stringBuilder;
        private Dictionary<ConnectedSequentialEnum, string> testsDictionary;

        // Paths
        private string testPathAlgorithmConnectedSequential = @"Testing\Test\ConnectedSequential.txt";
        private string algorithmConnectedSequential1 = ConnectedSequentialResource.connectedSequential1;
        private string algorithmConnectedSequential2 = ConnectedSequentialResource.connectedSequential2;
        private string algorithmConnectedSequential3 = ConnectedSequentialResource.connectedSequential3;
        private string algorithmConnectedSequential4 = ConnectedSequentialResource.connectedSequential4;
        #endregion

        // Enum
        #region
        public enum ConnectedSequentialEnum
        {
            connectedSequential1,
            connectedSequential2,
            connectedSequential3,
            connectedSequential4
        }
        #endregion

        // Constructor
        #region
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

        // Method
        #region
        /// <summary>
        /// Otestuje všechny typy grafů
        /// </summary>
        /// <returns>Vrátí report</returns>
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
        /// Otestuje daný typ grafu
        /// </summary>
        /// <param name="connectedSequential">daný typ grafu</param>
        /// <returns>Vrátí report</returns>
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
                testPath = GraphColoring.Tests.Tests.CreateTestFile(testsDictionary[connectedSequential]);

                reader = new ReaderWriter.Reader(testPath, false);
                graph = reader.ReadFile();

                ConnectedSequential optimalAlgorithm = new ConnectedSequential(graph);
                optimalAlgorithm.Color();

                stringBuilder.AppendLine(connectedSequential.ToString());
                stringBuilder.AppendLine("Graph colored.");
                stringBuilder.AppendLine(graph.GetColoredGraph().ToString());
            }
            catch (KeyNotFoundException)
            {
                throw new MyException.TestsMissingTestException(connectedSequential.ToString());
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
            return testPathAlgorithmConnectedSequential;
        }
        #endregion
    }
}
