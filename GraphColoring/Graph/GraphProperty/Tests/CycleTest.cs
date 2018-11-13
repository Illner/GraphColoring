using System;
using System.Text;
using System.Collections.Generic;

namespace GraphColoring.Graph.GraphProperty.Tests
{
    class CycleTest : GraphColoring.Tests.ITestInterface
    {
        // Variable
        #region
        private IGraphInterface graph;
        private String testPath;
        private ReaderWriter.IReaderGraphInterface reader;
        private StringBuilder stringBuilder;
        private Dictionary<CycleEnum, string> testsDictionary;

        // Paths
        private string testPathGraphCycle = @"Testing\Test\GraphCycle.txt";
        private string graphCycle1 = CycleResource.graphCycle1;
        private string graphCycle2 = CycleResource.graphCycle2;
        private string graphCycle3 = CycleResource.graphCycle3;
        private string graphCycle4 = CycleResource.graphCycle4;
        private string graphCycle5 = CycleResource.graphCycle5;
        #endregion

        // Enum
        #region
        public enum CycleEnum
        {
            graphCycle1,
            graphCycle2,
            graphCycle3,
            graphCycle4,
            graphCycle5
        }
        #endregion

        // Constructor
        #region
        public CycleTest()
        {
            stringBuilder = new StringBuilder();

            // Fill testsDictionary
            testsDictionary = new Dictionary<CycleEnum, string>
            {
                { CycleEnum.graphCycle1, graphCycle1 },
                { CycleEnum.graphCycle2, graphCycle2 },
                { CycleEnum.graphCycle3, graphCycle3 },
                { CycleEnum.graphCycle4, graphCycle4 },
                { CycleEnum.graphCycle5, graphCycle5 }
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

            foreach (CycleEnum cycleEnum in testsDictionary.Keys)
            {
                Testing(cycleEnum);
            }

            return stringBuilder;
        }

        /// <summary>
        /// Otestuje daný typ grafu
        /// </summary>
        /// <param name="cycleEnum">daný typ grafu</param>
        /// <returns>Vrátí report</returns>
        public StringBuilder Test(CycleEnum cycleEnum)
        {
            stringBuilder.Clear();

            Testing(cycleEnum);

            return stringBuilder;
        }

        private void Testing(CycleEnum cycleEnum)
        {
            try
            {
                testPath = GraphColoring.Tests.Tests.CreateTestFile(testsDictionary[cycleEnum]);

                reader = new ReaderWriter.ReaderGraph(testPath, false);
                graph = reader.ReadFile();

                stringBuilder.AppendLine(cycleEnum.ToString());
                stringBuilder.AppendLine("Graph created.");
                stringBuilder.AppendLine(graph.ToString());
                    
                stringBuilder.AppendLine("Is graph cyclic: " + graph.GetGraphProperty().GetIsCyclic());
                stringBuilder.AppendLine("Gridth: " + graph.GetGraphProperty().GetGirth());
            }
            catch (KeyNotFoundException)
            {
                throw new MyException.TestsException.TestsMissingTestException(cycleEnum.ToString());
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
            return testPathGraphCycle;
        }
        #endregion
    }
}
