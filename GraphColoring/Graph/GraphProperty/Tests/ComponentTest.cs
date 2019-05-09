using System;
using System.Text;
using System.Collections.Generic;

namespace GraphColoring.Graph.GraphProperty.Tests
{
    class ComponentTest : GraphColoring.Tests.ITestInterface
    {
        #region Variable
        private IGraphInterface graph;
        private String testPath;
        private ReaderWriter.IReaderGraphInterface reader;
        private StringBuilder stringBuilder;
        private List<IGraphInterface> graphComponentList;
        private Dictionary<ComponentEnum, string> testsDictionary;

        // Paths
        private string testPathGraphComponent = @"Testing\Test\GraphComponent.txt";
        private string graphComponent1 = ComponentResource.graphComponent1;
        private string graphComponent2 = ComponentResource.graphComponent2;
        private string graphComponent3 = ComponentResource.graphComponent3;
        private string graphComponent4 = ComponentResource.graphComponent4;
        private string graphComponent5 = ComponentResource.graphComponent5;
        #endregion
        
        #region Enum
        public enum ComponentEnum
        {
            graphComponent1,
            graphComponent2,
            graphComponent3,
            graphComponent4,
            graphComponent5
        }
        #endregion
        
        #region Constructor
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
        
        #region Method
        /// <summary>
        /// Test all values of enum (files)
        /// </summary>
        /// <returns>report</returns>
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
        /// Test a particular enum (file)
        /// </summary>
        /// <param name="componentEnum">enum (file)</param>
        /// <returns>report</returns>
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
                testPath = ReaderWriter.ReaderWriter.CreateTestFile(testsDictionary[componentEnum]);

                reader = new ReaderWriter.ReaderGraph(testPath, false);
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
                throw new MyException.TestsException.TestsMissingTestException(componentEnum.ToString());
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
            return testPathGraphComponent;
        }
        #endregion
    }
}
