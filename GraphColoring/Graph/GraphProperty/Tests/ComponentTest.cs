using System;
using System.Text;
using System.Collections.Generic;

namespace GraphColoring.Graph.GraphProperty.Tests
{
    class ComponentTest : GraphColoring.Tests.ITestInterface
    {
        // Variable
        #region
        private IGraphInterface graph;
        private String testPath;
        private ReaderWriter.Reader reader;
        private StringBuilder stringBuilder;
        private List<Graph> graphComponentList;
        private Dictionary<ComponentEnum, string> testsDictionary;

        // Paths
        private string testPathGraphComponent = @"Testing\Test\GraphComponent.txt";
        private string graphComponent1 = ComponentResource.graphComponent1;
        private string graphComponent2 = ComponentResource.graphComponent2;
        private string graphComponent3 = ComponentResource.graphComponent3;
        private string graphComponent4 = ComponentResource.graphComponent4;
        private string graphComponent5 = ComponentResource.graphComponent5;
        #endregion

        // Enum
        #region
        public enum ComponentEnum
        {
            graphComponent1,
            graphComponent2,
            graphComponent3,
            graphComponent4,
            graphComponent5
        }
        #endregion

        // Constructor
        #region
        public ComponentTest()
        {
            stringBuilder = new StringBuilder();

            // Fill testsDictionary
            testsDictionary = new Dictionary<ComponentEnum, string>
            {
                { ComponentEnum.graphComponent1, graphComponent1 },
                { ComponentEnum.graphComponent2, graphComponent2 },
                { ComponentEnum.graphComponent3, graphComponent3 },
                { ComponentEnum.graphComponent4, graphComponent4 },
                { ComponentEnum.graphComponent5, graphComponent5 }
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

            foreach (ComponentEnum componentEnum in testsDictionary.Keys)
            {
                Testing(componentEnum);
            }

            return stringBuilder;
        }

        /// <summary>
        /// Otestuje daný typ grafu
        /// </summary>
        /// <param name="componentEnum">daný typ grafu</param>
        /// <returns>Vrátí report</returns>
        public StringBuilder Test(ComponentEnum componentEnum)
        {
            stringBuilder.Clear();

            Testing(componentEnum);

            return stringBuilder;
        }
        
        private void Testing(ComponentEnum componentEnum)
        {
            try
            {
                testPath = GraphColoring.Tests.Tests.CreateTestFile(testsDictionary[componentEnum]);

                reader = new ReaderWriter.Reader(testPath, false);
                graph = reader.ReadFile();

                stringBuilder.AppendLine(componentEnum.ToString());
                stringBuilder.AppendLine("Graph created.");
                stringBuilder.AppendLine(graph.ToString());

                stringBuilder.AppendLine("Number of components: " + graph.GetGraphProperty().GetCountComponents());
                stringBuilder.AppendLine("Is graph connected: " + graph.GetGraphProperty().GetIsConnected());
                stringBuilder.AppendLine("Circuit rank: " + graph.GetGraphProperty().GetCircuitRank());

                graphComponentList = graph.GetGraphProperty().GetComponents();

                foreach (Graph graphComponent in graphComponentList)
                {
                    stringBuilder.AppendLine("Graph component.");
                    stringBuilder.AppendLine(graphComponent.ToString());
                }
            }
            catch (KeyNotFoundException)
            {
                throw new MyException.TestsMissingTestException(componentEnum.ToString());
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
            return testPathGraphComponent;
        }
        #endregion
    }
}
