using System;
using System.Collections.Generic;
using System.Text;

namespace GraphColoring.Graph.GraphOperation.Tests
{
    class ComplementTest : GraphColoring.Tests.ITestInterface
    {
        // Variable
        #region
        private Graph graph;
        private String testPath;
        private ReaderWriter.Reader reader;
        private StringBuilder stringBuilder;
        private Dictionary<ComplementEnum, string> testsDictionary;

        // Paths
        private string testPathGraphComplement = @"Testing\Test\GraphComplement.txt";
        private string graphComplement1 = ComplementResource.graphComplement1;
        private string graphComplement2 = ComplementResource.graphComplement2;
        private string graphComplement3 = ComplementResource.graphComplement3;
        private string graphComplement4 = ComplementResource.graphComplement4;
        #endregion

        // Enum
        #region
        public enum ComplementEnum
        {
            graphComplement1,
            graphComplement2,
            graphComplement3,
            graphComplement4
        }
        #endregion

        // Constructor
        #region
        public ComplementTest()
        {
            stringBuilder = new StringBuilder();

            // Fill testsDictionary
            testsDictionary = new Dictionary<ComplementEnum, string>
            {
                { ComplementEnum.graphComplement1, graphComplement1 },
                { ComplementEnum.graphComplement2, graphComplement2 },
                { ComplementEnum.graphComplement3, graphComplement3 },
                { ComplementEnum.graphComplement4, graphComplement4 }
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

            foreach (ComplementEnum complementEnum in testsDictionary.Keys)
            {
                Testing(complementEnum);
            }

            return stringBuilder;
        }

        /// <summary>
        /// Otestuje daný typ grafu
        /// </summary>
        /// <param name="complementEnum">daný typ grafu</param>
        /// <returns>Vrátí report</returns>
        public StringBuilder Test(ComplementEnum complementEnum)
        {
            stringBuilder.Clear();

            Testing(complementEnum);

            return stringBuilder;
        }

        private void Testing(ComplementEnum complementEnum)
        {
            try
            {
                testPath = GraphColoring.Tests.Tests.CreateTestFile(testsDictionary[complementEnum]);

                reader = new ReaderWriter.Reader(testPath, false);
                graph = reader.ReadFile();

                stringBuilder.AppendLine(complementEnum.ToString());
                stringBuilder.AppendLine("Graph created.");
                stringBuilder.AppendLine(graph.ToString());

                Graph graphComplement = GraphOperation.ComplementGraph(graph);
                
                stringBuilder.AppendLine("Complement graph.");
                stringBuilder.AppendLine(graphComplement.ToString());
            }
            catch (KeyNotFoundException)
            {
                throw new MyException.TestsMissingTestException(complementEnum.ToString());
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
            return testPathGraphComplement;
        }
        #endregion
    }
}
