using System;
using System.Text;
using System.Collections.Generic;

namespace GraphColoring.Graph.GraphProperty.Tests
{
    class DegreeSequenceTest : GraphColoring.Tests.ITestInterface
    {
        // Variable
        #region
        private Graph graph;
        private String testPath;
        private ReaderWriter.Reader reader;
        private StringBuilder stringBuilder;
        private List<Graph> graphComponentList;

        string pathGraphDegreeSequence1 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\SequencesAndPolynomials\graphDegreeSequence1.graph";
        string pathGraphDegreeSequence2 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\SequencesAndPolynomials\graphDegreeSequence2.graph";
        string pathGraphDegreeSequence3 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\SequencesAndPolynomials\graphDegreeSequence3.graph";
        string pathGraphDegreeSequence4 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\SequencesAndPolynomials\graphDegreeSequence4.graph";
        string pathGraphDegreeSequence5 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\SequencesAndPolynomials\graphDegreeSequence5.graph";
        #endregion

        // Enum
        #region
        public enum GraphEnum
        {
            graphDegreeSequence1,
            graphDegreeSequence2,
            graphDegreeSequence3,
            graphDegreeSequence4
        }
        #endregion

        // Constructor
        #region
        public DegreeSequenceTest()
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
                    case GraphEnum.graphDegreeSequence1:
                        testPath = pathGraphDegreeSequence1;
                        break;
                    case GraphEnum.graphDegreeSequence2:
                        testPath = pathGraphDegreeSequence2;
                        break;
                    case GraphEnum.graphDegreeSequence3:
                        testPath = pathGraphDegreeSequence3;
                        break;
                    case GraphEnum.graphDegreeSequence4:
                        testPath = pathGraphDegreeSequence4;
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

                    List<int> degreeSequenceList = graph.GetGraphProperty().GetDegreeSequence();


                    stringBuilder.AppendLine("Degree sequence");

                    foreach (int degree in degreeSequenceList)
                    {
                        stringBuilder.Append(degree + " ");
                    }

                    stringBuilder.AppendLine("");
                    stringBuilder.AppendLine("Minimum vertex degree: " + graph.GetGraphProperty().GetMinimumVertexDegree());
                    stringBuilder.AppendLine("Maximum vertex degree: " + graph.GetGraphProperty().GetMaximumVertexDegree());
                    stringBuilder.AppendLine("Is graph regular: " + graph.GetGraphProperty().GetIsRegular());
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
