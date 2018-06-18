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
        private string testPathGraphComplement = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Test\GraphComplement.txt";
        private string graphComplement1Path = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Operation\Complement\graphComplement1.graph";
        private string graphComplement2Path = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Operation\Complement\graphComplement2.graph";
        private string graphComplement3Path = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Operation\Complement\graphComplement3.graph";
        private string graphComplement4Path = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Operation\Complement\graphComplement4.graph";
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
                { ComplementEnum.graphComplement1, graphComplement1Path },
                { ComplementEnum.graphComplement2, graphComplement2Path },
                { ComplementEnum.graphComplement3, graphComplement3Path },
                { ComplementEnum.graphComplement4, graphComplement4Path }
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
                testPath = testsDictionary[complementEnum];

                reader = new ReaderWriter.Reader(testPath);
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
