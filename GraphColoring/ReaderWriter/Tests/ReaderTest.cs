using System;
using System.Text;

namespace GraphColoring.ReaderWriter.Tests
{
    class ReaderTest : GraphColoring.Tests.ITestInterface
    {
        // Variable
        #region
        private Reader reader;
        private Graph.Graph graph;
        private StringBuilder stringBuilder;

        // GraphEdgeList
        private string readerPathGraphEdgeListValid1 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListValidNumberColors.graph";
        private string readerPathGraphEdgeListValid2 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListValidCHromaticNumber.graph";
        private string readerPathGraphEdgeListValidVertexName = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListValidVertexName.graph";

        private string readerPathGraphEdgeListInvalidHeader1 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListInvalidHeader1.graph";
        private string readerPathGraphEdgeListInvalidHeader2 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListInvalidHeader2.graph";

        private string readerPathGraphEdgeListInvalidBallast1 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListInvalidBallast1.graph";
        private string readerPathGraphEdgeListInvalidBallast2 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListInvalidBallast2.graph";
        private string readerPathGraphEdgeListInvalidBallast3 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListInvalidBallast3.graph";

        private string readerPathGraphEdgeListInvalidGraphName = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListInvalidGraphName.graph";

        private string readerPathGraphEdgeListInvalidCountVertices1 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListInvalidCountVertices1.graph";
        private string readerPathGraphEdgeListInvalidCountVertices2 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListInvalidCountVertices2.graph";
        private string readerPathGraphEdgeListInvalidCountVertices3 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListInvalidCountVertices3.graph";
        private string readerPathGraphEdgeListInvalidCountVertices4 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListInvalidCountVertices4.graph";

        private string readerPathGraphEdgeListInvalidGraph = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListInvalidGraph.graph";

        private string readerPathGraphEdgeListInvalidCoreData1 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListInvalidCoreData1.graph";
        private string readerPathGraphEdgeListInvalidCoreData2 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListInvalidCoreData2.graph";
        private string readerPathGraphEdgeListInvalidCoreData3 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListInvalidCoreData3.graph";
        private string readerPathGraphEdgeListInvalidCoreData4 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListInvalidCoreData4.graph";

        private string readerPathGraphEdgeListInvalidColoredGraph = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListInvalidColoredGraph.graph";

        private string readerPathGraphEdgeListInvalidNumberColors = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListInvalidNumberColors.graph";
        private string readerPathGraphEdgeListInvalidChromaticNumber = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListInvalidChromaticNumber.graph";

        // GraphAdjacencyMatrix
        private string readerPathGraphAdjacencyMatrixValid1 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyMatrix\GraphAdjacencyMatrixValidNumberColors.graph";
        private string readerPathGraphAdjacencyMatrixValid2 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyMatrix\GraphAdjacencyMatrixValidChromaticNumber.graph";

        private string readerPathGraphAdjacencyMatrixInvalidHeader1 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyMatrix\GraphAdjacencyMatrixInvalidHeader1.graph";
        private string readerPathGraphAdjacencyMatrixInvalidHeader2 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyMatrix\GraphAdjacencyMatrixInvalidHeader2.graph";

        private string readerPathGraphAdjacencyMatrixInvalidBallast1 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyMatrix\GraphAdjacencyMatrixInvalidBallast1.graph";
        private string readerPathGraphAdjacencyMatrixInvalidBallast2 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyMatrix\GraphAdjacencyMatrixInvalidBallast2.graph";
        private string readerPathGraphAdjacencyMatrixInvalidBallast3 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyMatrix\GraphAdjacencyMatrixInvalidBallast3.graph";

        private string readerPathGraphAdjacencyMatrixInvalidGraphName = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyMatrix\GraphAdjacencyMatrixInvalidGraphName.graph";

        private string readerPathGraphAdjacencyMatrixInvalidCountVertices1 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyMatrix\GraphAdjacencyMatrixInvalidCountVertices1.graph";
        private string readerPathGraphAdjacencyMatrixInvalidCountVertices2 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyMatrix\GraphAdjacencyMatrixInvalidCountVertices2.graph";
        private string readerPathGraphAdjacencyMatrixInvalidCountVertices3 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyMatrix\GraphAdjacencyMatrixInvalidCountVertices3.graph";
        private string readerPathGraphAdjacencyMatrixInvalidCountVertices4 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyMatrix\GraphAdjacencyMatrixInvalidCountVertices4.graph";

        private string readerPathGraphAdjacencyMatrixInvalidGraph = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyMatrix\GraphAdjacencyMatrixInvalidGraph.graph";

        private string readerPathGraphAdjacencyMatrixInvalidCoreData1 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyMatrix\GraphAdjacencyMatrixInvalidCoreData1.graph";
        private string readerPathGraphAdjacencyMatrixInvalidCoreData2 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyMatrix\GraphAdjacencyMatrixInvalidCoreData2.graph";
        private string readerPathGraphAdjacencyMatrixInvalidCoreData3 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyMatrix\GraphAdjacencyMatrixInvalidCoreData3.graph";
        private string readerPathGraphAdjacencyMatrixInvalidCoreData4 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyMatrix\GraphAdjacencyMatrixInvalidCoreData4.graph";

        private string readerPathGraphAdjacencyMatrixInvalidColoredGraph = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyMatrix\GraphAdjacencyMatrixInvalidColoredGraph.graph";

        private string readerPathGraphAdjacencyMatrixInvalidNumberColors = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyMatrix\GraphAdjacencyMatrixInvalidNumberColors.graph";
        private string readerPathGraphAdjacencyMatrixInvalidChromaticNumber = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyMatrix\GraphAdjacencyMatrixInvalidChromaticNumber.graph";

        // GraphAdjacencyList
        private string readerPathGraphAdjacencyListValid1 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyList\GraphAdjacencyListValidNumberColors.graph";
        private string readerPathGraphAdjacencyListValid2 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyList\GraphAdjacencyListValidChromaticNumber.graph";
        private string readerPathGraphAdjacencyListValidVertexName = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyList\GraphAdjacencyListValidVertexName.graph";

        private string readerPathGraphAdjacencyListInvalidHeader1 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyList\GraphAdjacencyListInvalidHeader1.graph";
        private string readerPathGraphAdjacencyListInvalidHeader2 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyList\GraphAdjacencyListInvalidHeader2.graph";

        private string readerPathGraphAdjacencyListInvalidBallast1 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyList\GraphAdjacencyListInvalidBallast1.graph";
        private string readerPathGraphAdjacencyListInvalidBallast2 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyList\GraphAdjacencyListInvalidBallast2.graph";
        private string readerPathGraphAdjacencyListInvalidBallast3 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyList\GraphAdjacencyListInvalidBallast3.graph";

        private string readerPathGraphAdjacencyListInvalidGraphName = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyList\GraphAdjacencyListInvalidGraphName.graph";

        private string readerPathGraphAdjacencyListInvalidCountVertices1 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyList\GraphAdjacencyListInvalidCountVertices1.graph";
        private string readerPathGraphAdjacencyListInvalidCountVertices2 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyList\GraphAdjacencyListInvalidCountVertices2.graph";
        private string readerPathGraphAdjacencyListInvalidCountVertices3 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyList\GraphAdjacencyListInvalidCountVertices3.graph";
        private string readerPathGraphAdjacencyListInvalidCountVertices4 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyList\GraphAdjacencyListInvalidCountVertices4.graph";

        private string readerPathGraphAdjacencyListInvalidGraph = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyList\GraphAdjacencyListInvalidGraph.graph";

        private string readerPathGraphAdjacencyListInvalidCoreData1 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyList\GraphAdjacencyListInvalidCoreData1.graph";
        private string readerPathGraphAdjacencyListInvalidCoreData2 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyList\GraphAdjacencyListInvalidCoreData2.graph";

        private string readerPathGraphAdjacencyListInvalidColoredGraph = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyList\GraphAdjacencyListInvalidColoredGraph.graph";

        private string readerPathGraphAdjacencyListInvalidNumberColors = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyList\GraphAdjacencyListInvalidNumberColors.graph";
        private string readerPathGraphAdjacencyListInvalidChromaticNumber = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyList\GraphAdjacencyListInvalidChromaticNumber.graph";
        #endregion

        // Enum
        #region
        public enum PathEnum
        {
            valid,
            invalidHeader,
            invalidBallast,
            invalidGraphName,
            invalidCountVertices,
            invalidGraph,
            invalidCoreData,
            invalidColoredGraph,
            invalidNumberColors
        }
        #endregion

        // Constructor
        #region
        public ReaderTest()
        {
            stringBuilder = new StringBuilder();
        }
        #endregion

        // Method
        #region
        /// <summary>
        /// Otestuje všechny typy souborů (PathEnum) a reprezentace grafů (Graph.GraphRepresentationEnum)
        /// </summary>
        /// <returns>Vrátí report</returns>
        public StringBuilder Test()
        {
            stringBuilder.Clear();

            foreach (PathEnum pathEnum in Enum.GetValues(typeof(PathEnum)))
            {
                stringBuilder.AppendLine(pathEnum.ToString());

                foreach (Graph.Graph.GraphRepresentationEnum graphEnum in Enum.GetValues(typeof(Graph.Graph.GraphRepresentationEnum)))
                {

                    stringBuilder.AppendLine(graphEnum.ToString());

                    Testing(graphEnum, pathEnum);
                }
            }

            return stringBuilder;
        }

        /// <summary>
        /// Otestuje daný typ souboru a danou reprezentaci grafu
        /// </summary>
        /// <param name="graphRepresentation">Reprezentace grafu, kterou chceme otestovat</param>
        /// <param name="pathEnum">Typ souboru, který chceme otestovat</param>
        /// <returns>Vrátí report</returns>
        public StringBuilder Test(Graph.Graph.GraphRepresentationEnum graphRepresentation, PathEnum pathEnum)
        {
            stringBuilder.Clear();

            Testing(graphRepresentation, pathEnum);

            return stringBuilder;
        }

        private void Testing(Graph.Graph.GraphRepresentationEnum graphEnum, PathEnum pathEnum)
        {
            try
            {
                switch (graphEnum)
                {
                    case Graph.Graph.GraphRepresentationEnum.adjacencyList:
                        #region
                        switch (pathEnum)
                        {
                            case PathEnum.valid:
                                reader = new Reader(readerPathGraphAdjacencyListValid1);
                                Testing();
                                reader = new Reader(readerPathGraphAdjacencyListValid2);
                                Testing();
                                reader = new Reader(readerPathGraphAdjacencyListValidVertexName);
                                Testing();
                                break;
                            case PathEnum.invalidHeader:
                                reader = new Reader(readerPathGraphAdjacencyListInvalidHeader1);
                                Testing();
                                reader = new Reader(readerPathGraphAdjacencyListInvalidHeader2);
                                Testing();
                                break;
                            case PathEnum.invalidBallast:
                                reader = new Reader(readerPathGraphAdjacencyListInvalidBallast1);
                                Testing();
                                reader = new Reader(readerPathGraphAdjacencyListInvalidBallast2);
                                Testing();
                                reader = new Reader(readerPathGraphAdjacencyListInvalidBallast3);
                                Testing();
                                break;
                            case PathEnum.invalidGraphName:
                                reader = new Reader(readerPathGraphAdjacencyListInvalidGraphName);
                                Testing();
                                break;
                            case PathEnum.invalidCountVertices:
                                reader = new Reader(readerPathGraphAdjacencyListInvalidCountVertices1);
                                Testing();
                                reader = new Reader(readerPathGraphAdjacencyListInvalidCountVertices2);
                                Testing();
                                reader = new Reader(readerPathGraphAdjacencyListInvalidCountVertices3);
                                Testing();
                                reader = new Reader(readerPathGraphAdjacencyListInvalidCountVertices4);
                                Testing();
                                break;
                            case PathEnum.invalidGraph:
                                reader = new Reader(readerPathGraphAdjacencyListInvalidGraph);
                                Testing();
                                break;
                            case PathEnum.invalidCoreData:
                                reader = new Reader(readerPathGraphAdjacencyListInvalidCoreData1);
                                Testing();
                                reader = new Reader(readerPathGraphAdjacencyListInvalidCoreData2);
                                Testing();
                                break;
                            case PathEnum.invalidNumberColors:
                                reader = new Reader(readerPathGraphAdjacencyListInvalidColoredGraph);
                                Testing();
                                reader = new Reader(readerPathGraphAdjacencyListInvalidNumberColors);
                                Testing();
                                reader = new Reader(readerPathGraphAdjacencyListInvalidChromaticNumber);
                                Testing();
                                break;
                            default:
                                reader = null;
                                stringBuilder.AppendLine("This isn't implemented!");
                                break;
                        }
                        #endregion
                        break;
                    case Graph.Graph.GraphRepresentationEnum.adjacencyMatrix:
                        #region
                        switch (pathEnum)
                        {
                            case PathEnum.valid:
                                reader = new Reader(readerPathGraphAdjacencyMatrixValid1);
                                Testing();
                                reader = new Reader(readerPathGraphAdjacencyMatrixValid2);
                                Testing();
                                break;
                            case PathEnum.invalidHeader:
                                reader = new Reader(readerPathGraphAdjacencyMatrixInvalidHeader1);
                                Testing();
                                reader = new Reader(readerPathGraphAdjacencyMatrixInvalidHeader2);
                                Testing();
                                break;
                            case PathEnum.invalidBallast:
                                reader = new Reader(readerPathGraphAdjacencyMatrixInvalidBallast1);
                                Testing();
                                reader = new Reader(readerPathGraphAdjacencyMatrixInvalidBallast2);
                                Testing();
                                reader = new Reader(readerPathGraphAdjacencyMatrixInvalidBallast3);
                                Testing();
                                break;
                            case PathEnum.invalidGraphName:
                                reader = new Reader(readerPathGraphAdjacencyMatrixInvalidGraphName);
                                Testing();
                                break;
                            case PathEnum.invalidCountVertices:
                                reader = new Reader(readerPathGraphAdjacencyMatrixInvalidCountVertices1);
                                Testing();
                                reader = new Reader(readerPathGraphAdjacencyMatrixInvalidCountVertices2);
                                Testing();
                                reader = new Reader(readerPathGraphAdjacencyMatrixInvalidCountVertices3);
                                Testing();
                                reader = new Reader(readerPathGraphAdjacencyMatrixInvalidCountVertices4);
                                Testing();
                                break;
                            case PathEnum.invalidGraph:
                                reader = new Reader(readerPathGraphAdjacencyMatrixInvalidGraph);
                                Testing();
                                break;
                            case PathEnum.invalidCoreData:
                                reader = new Reader(readerPathGraphAdjacencyMatrixInvalidCoreData1);
                                Testing();
                                reader = new Reader(readerPathGraphAdjacencyMatrixInvalidCoreData2);
                                Testing();
                                reader = new Reader(readerPathGraphAdjacencyMatrixInvalidCoreData3);
                                Testing();
                                reader = new Reader(readerPathGraphAdjacencyMatrixInvalidCoreData4);
                                Testing();
                                break;
                            case PathEnum.invalidNumberColors:
                                reader = new Reader(readerPathGraphAdjacencyMatrixInvalidColoredGraph);
                                Testing();
                                reader = new Reader(readerPathGraphAdjacencyMatrixInvalidNumberColors);
                                Testing();
                                reader = new Reader(readerPathGraphAdjacencyMatrixInvalidChromaticNumber);
                                Testing();
                                break;
                            default:
                                reader = null;
                                stringBuilder.AppendLine("This isn't implemented!");
                                break;
                        }
                        #endregion
                        break;
                    case Graph.Graph.GraphRepresentationEnum.edgeList:
                        #region
                        switch (pathEnum)
                        {
                            case PathEnum.valid:
                                reader = new Reader(readerPathGraphEdgeListValid1);
                                Testing();
                                reader = new Reader(readerPathGraphEdgeListValid2);
                                Testing();
                                reader = new Reader(readerPathGraphEdgeListValidVertexName);
                                Testing();
                                break;
                            case PathEnum.invalidHeader:
                                reader = new Reader(readerPathGraphEdgeListInvalidHeader1);
                                Testing();
                                reader = new Reader(readerPathGraphEdgeListInvalidHeader2);
                                Testing();
                                break;
                            case PathEnum.invalidBallast:
                                reader = new Reader(readerPathGraphEdgeListInvalidBallast1);
                                Testing();
                                reader = new Reader(readerPathGraphEdgeListInvalidBallast2);
                                Testing();
                                reader = new Reader(readerPathGraphEdgeListInvalidBallast3);
                                Testing();
                                break;
                            case PathEnum.invalidGraphName:
                                reader = new Reader(readerPathGraphEdgeListInvalidGraphName);
                                Testing();
                                break;
                            case PathEnum.invalidCountVertices:
                                reader = new Reader(readerPathGraphEdgeListInvalidCountVertices1);
                                Testing();
                                reader = new Reader(readerPathGraphEdgeListInvalidCountVertices2);
                                Testing();
                                reader = new Reader(readerPathGraphEdgeListInvalidCountVertices3);
                                Testing();
                                reader = new Reader(readerPathGraphEdgeListInvalidCountVertices4);
                                Testing();
                                break;
                            case PathEnum.invalidGraph:
                                reader = new Reader(readerPathGraphEdgeListInvalidGraph);
                                Testing();
                                break;
                            case PathEnum.invalidCoreData:
                                reader = new Reader(readerPathGraphEdgeListInvalidCoreData1);
                                Testing();
                                reader = new Reader(readerPathGraphEdgeListInvalidCoreData2);
                                Testing();
                                reader = new Reader(readerPathGraphEdgeListInvalidCoreData3);
                                Testing();
                                reader = new Reader(readerPathGraphEdgeListInvalidCoreData4);
                                Testing();
                                break;
                            case PathEnum.invalidNumberColors:
                                reader = new Reader(readerPathGraphEdgeListInvalidColoredGraph);
                                Testing();
                                reader = new Reader(readerPathGraphEdgeListInvalidNumberColors);
                                Testing();
                                reader = new Reader(readerPathGraphEdgeListInvalidChromaticNumber);
                                Testing();
                                break;
                            default:
                                reader = null;
                                stringBuilder.AppendLine("This isn't implemented!");
                                break;
                        }
                        #endregion
                        break;
                    default:
                        reader = null;
                        stringBuilder.AppendLine("This graph representation isn't implemented!");
                        break;
                }
            }
            catch (MyException.ReaderWriterException e)
            {
                stringBuilder.AppendLine(e.Message);
            }
        }

        /// <summary>
        /// Přečte soubor s grafem a daný graf vytvoří
        /// </summary>
        private void Testing()
        {
            try
            {
                stringBuilder.AppendLine("Reading file: " + reader.GetPath());

                graph = reader.ReadFile();

                stringBuilder.AppendLine("Graph created.");
                stringBuilder.AppendLine(graph.ToString());
            }
            catch (Exception e)
            {
                stringBuilder.AppendLine(e.Message);
            }
        }
        #endregion
    }
}
