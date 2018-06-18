using System;
using System.Text;
using System.Collections.Generic;

namespace GraphColoring.Graph.GraphProperty.Tests
{
    class CycleTest : GraphColoring.Tests.ITestInterface
    {
        // Variable
        #region
        private Graph graph;
        private String testPath;
        private ReaderWriter.Reader reader;
        private StringBuilder stringBuilder;
        private Dictionary<CycleEnum, string> testsDictionary;

        // Paths
        private string testPathGraphCycle = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Test\GraphCycle.txt";
        private string graphCycle1Path = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Property\Cycle\graphCycle1.graph";
        private string graphCycle2Path = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Property\Cycle\graphCycle2.graph";
        private string graphCycle3Path = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Property\Cycle\graphCycle3.graph";
        private string graphCycle4Path = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Property\Cycle\graphCycle4.graph";
        private string graphCycle5Path = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Property\Cycle\graphCycle5.graph";
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
                { CycleEnum.graphCycle1, graphCycle1Path },
                { CycleEnum.graphCycle2, graphCycle2Path },
                { CycleEnum.graphCycle3, graphCycle3Path },
                { CycleEnum.graphCycle4, graphCycle4Path },
                { CycleEnum.graphCycle5, graphCycle5Path }
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
                testPath = testsDictionary[cycleEnum];

                reader = new ReaderWriter.Reader(testPath);
                graph = reader.ReadFile();

                stringBuilder.AppendLine(cycleEnum.ToString());
                stringBuilder.AppendLine("Graph created.");
                stringBuilder.AppendLine(graph.ToString());
                    
                stringBuilder.AppendLine("Is graph cyclic: " + graph.GetGraphProperty().GetIsCyclic());
                stringBuilder.AppendLine("Gridth: " + graph.GetGraphProperty().GetGirth());
            }
            catch (KeyNotFoundException)
            {
                throw new MyException.TestsMissingTestException(cycleEnum.ToString());
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
            return testPathGraphCycle;
        }
        #endregion
    }
}
