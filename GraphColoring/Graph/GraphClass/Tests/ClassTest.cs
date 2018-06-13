using System;
using System.Text;
using System.Collections.Generic;
namespace GraphColoring.Graph.GraphClass.Tests
{
    class ClassTest : GraphColoring.Tests.ITestInterface
    {
        // Variable
        #region
        private Graph graph;
        private String testPath;
        private ReaderWriter.Reader reader;
        private StringBuilder stringBuilder;
        private List<Graph> graphComponentList;

        // Paths
        string pathGraphClass1 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Class\graphClass1.graph";
        string pathGraphClass2 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Class\graphClass2.graph";
        string pathGraphClass3 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Class\graphClass3.graph";
        string pathGraphClass4 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Class\graphClass4.graph";
        string pathGraphClass5 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Class\graphClass5.graph";
        string pathGraphClass6 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Class\graphClass6.graph";
        string pathGraphClass7 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Class\graphClass7.graph";
        string pathGraphClass8 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Class\graphClass8.graph";
        #endregion

        // Enum
        #region
        public enum GraphEnum
        {
            graphClass1,
            graphClass2,
            graphClass3,
            graphClass4,
            graphClass5,
            graphClass6,
            graphClass7,
            graphClass8
        }
        #endregion

        // Constructor
        #region
        public ClassTest()
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
                    case GraphEnum.graphClass1:
                        testPath = pathGraphClass1;
                        break;
                    case GraphEnum.graphClass2:
                        testPath = pathGraphClass2;
                        break;
                    case GraphEnum.graphClass3:
                        testPath = pathGraphClass3;
                        break;
                    case GraphEnum.graphClass4:
                        testPath = pathGraphClass4;
                        break;
                    case GraphEnum.graphClass5:
                        testPath = pathGraphClass5;
                        break;
                    case GraphEnum.graphClass6:
                        testPath = pathGraphClass6;
                        break;
                    case GraphEnum.graphClass7:
                        testPath = pathGraphClass7;
                        break;
                    case GraphEnum.graphClass8:
                        testPath = pathGraphClass8;
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

                    GraphClass.GraphClassEnum graphClassEnum = GraphClass.GetGraphClass(graph);

                    stringBuilder.AppendLine("Graph class: " + graphClassEnum.ToString());
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