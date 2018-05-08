using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        // Paths
        string pathGraphCycle1 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Cycle\graphCycle1.graph";
        string pathGraphCycle2 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Cycle\graphCycle2.graph";
        string pathGraphCycle3 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Cycle\graphCycle3.graph";
        string pathGraphCycle4 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Cycle\graphCycle4.graph";
        string pathGraphCycle5 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Cycle\graphCycle5.graph";
        #endregion

        // Enum
        #region
        public enum GraphEnum
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

            foreach (GraphEnum graphEnum in Enum.GetValues(typeof(GraphEnum)))
            {
                stringBuilder.AppendLine(graphEnum.ToString());

                Testing(graphEnum);
            }

            return stringBuilder;
        }

        /// <summary>
        /// Otestuje daný typ grafu
        /// </summary>
        /// <param name="graphEnum">daný typ grafu</param>
        /// <returns>Vrátí report</returns>
        public StringBuilder Test(GraphEnum graphEnum)
        {
            stringBuilder.Clear();

            Testing(graphEnum);

            return stringBuilder;
        }

        private void Testing(GraphEnum graphEnum)
        {
            try
            {
                switch (graphEnum)
                {
                    case GraphEnum.graphCycle1:
                        testPath = pathGraphCycle1;
                        break;
                    case GraphEnum.graphCycle2:
                        testPath = pathGraphCycle2;
                        break;
                    case GraphEnum.graphCycle3:
                        testPath = pathGraphCycle3;
                        break;
                    case GraphEnum.graphCycle4:
                        testPath = pathGraphCycle4;
                        break;
                    case GraphEnum.graphCycle5:
                        testPath = pathGraphCycle5;
                        break;
                    default:
                        stringBuilder.AppendLine("This graph doesn't exist!");
                        break;
                }

                if (testPath != "")
                {
                    reader = new ReaderWriter.Reader(testPath);
                    graph = reader.ReadFile();

                    stringBuilder.AppendLine("Graph created.");
                    stringBuilder.AppendLine(graph.ToString());
                    /*
                    stringBuilder.AppendLine("Is graph cyclic: " + graph.GetGraphProperty().GetIsCyclic());
                    stringBuilder.AppendLine("Gridth: " + graph.GetGraphProperty().GetGirth());
                    */
                    // TODO Clear
                    List<Edge> spanningTree;
                    spanningTree = graph.GetGraphProperty().GetSpanningTreeDFS();
                    stringBuilder.AppendLine("Spanning tree: ");

                    foreach(Edge edge in spanningTree)
                    {
                        stringBuilder.AppendLine("Edge: (" + edge.GetVertex1().GetIdentifier() + "), (" + edge.GetVertex2().GetIdentifier() + ")");
                    }
                }

                testPath = "";
            }
            catch (MyException.ReaderWriterException e)
            {
                stringBuilder.AppendLine(e.Message);
            }
        }
        #endregion
    }
}
