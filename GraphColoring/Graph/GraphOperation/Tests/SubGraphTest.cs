using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GraphColoring.Graph.GraphOperation.Tests
{
    class SubGraphTest : GraphColoring.Tests.ITestInterface
    {
        // Variable
        #region
        private Graph graph;
        private String testPath;
        private int countVertices;
        private ReaderWriter.Reader reader;
        private StringBuilder stringBuilder;
        // Tuple<cesta k souboru, pocet vrcholu v podgrafu>
        private Dictionary<SubGraphEnum, Tuple<string,int>> testsDictionary;

        // Paths
        private string testPathGraphSubGraph = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Test\GraphSubGraph.txt";
        private string graphSubGraph1Path = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Operation\SubGraph\graphSubGraph1.graph";
        private string graphSubGraph2Path = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Operation\SubGraph\graphSubGraph1.graph";
        private string graphSubGraph3Path = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Operation\SubGraph\graphSubGraph2.graph";
        private string graphSubGraph4Path = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Operation\SubGraph\graphSubGraph3.graph";
        private string graphSubGraph5Path = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Operation\SubGraph\graphSubGraph4.graph";
        private string graphSubGraph6Path = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Operation\SubGraph\graphSubGraph4.graph";
        #endregion

        // Enum
        #region
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

        // Constructor
        #region
        public SubGraphTest()
        {
            stringBuilder = new StringBuilder();

            // Fill testsDictionary
            testsDictionary = new Dictionary<SubGraphEnum, Tuple<string, int>>
            {
                { SubGraphEnum.graphSubGraph1, new Tuple<string, int>(graphSubGraph1Path, 4) },
                { SubGraphEnum.graphSubGraph2, new Tuple<string, int>(graphSubGraph2Path, 6) },
                { SubGraphEnum.graphSubGraph3, new Tuple<string, int>(graphSubGraph3Path, 3) },
                { SubGraphEnum.graphSubGraph4, new Tuple<string, int>(graphSubGraph4Path, 4) },
                { SubGraphEnum.graphSubGraph5, new Tuple<string, int>(graphSubGraph5Path, 1) },
                { SubGraphEnum.graphSubGraph6, new Tuple<string, int>(graphSubGraph6Path, 0) }
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

            foreach (SubGraphEnum subGraphEnum in testsDictionary.Keys)
            {
                Testing(subGraphEnum);
            }

            return stringBuilder;
        }

        /// <summary>
        /// Otestuje daný typ grafu
        /// </summary>
        /// <param name="subGraphEnum">daný typ grafu</param>
        /// <returns>Vrátí report</returns>
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
                testPath = testsDictionary[subGraphEnum].Item1;
                countVertices = testsDictionary[subGraphEnum].Item2;

                reader = new ReaderWriter.Reader(testPath);
                graph = reader.ReadFile();

                stringBuilder.AppendLine(subGraphEnum.ToString());
                stringBuilder.AppendLine("Graph created.");
                stringBuilder.AppendLine(graph.ToString());

                Graph subGraph = GraphOperation.SubGraph(graph, graph.AllVertices().Take(countVertices).ToList());

                stringBuilder.AppendLine("Subgraph created.");
                stringBuilder.AppendLine(subGraph.ToString());
            }
            catch (KeyNotFoundException)
            {
                throw new MyException.TestsMissingTestException(subGraphEnum.ToString());
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
            return testPathGraphSubGraph;
        }
        #endregion
    }
}
