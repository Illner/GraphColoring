using System;
using System.Text;
using System.Collections.Generic;


namespace GraphColoring.ReaderWriter.Tests
{
    class ReaderTest : GraphColoring.Tests.ITestInterface
    {
        // Variable
        #region
        private Reader reader;
        private Graph.Graph graph;
        private StringBuilder stringBuilder;
        private Dictionary<Graph.Graph.GraphRepresentationEnum, Dictionary<PathEnum, List<string>>> testsDictionary;
        private Dictionary<PathEnum, List<string>> testsGraphEdgeListDictionary;
        private Dictionary<PathEnum, List<string>> testsGraphAdjacencyMatrixDictionary;
        private Dictionary<PathEnum, List<string>> testsGraphAdjacencyListDictionary;

        // Paths
        private string testPathReader = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Test\Reader.txt";
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

        private string readerPathGraphEdgeListInvalidUsedAlgorithm = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListInvalidUsedAlgorithm.graph";

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

        private string readerPathGraphAdjacencyMatrixInvalidUsedAlgorithm = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyMatrix\GraphAdjacencyMatrixInvalidUsedAlgorithm.graph";

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

        private string readerPathGraphAdjacencyListInvalidUsedAlgorithm = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyList\GraphAdjacencyListInvalidUsedAlgorithm.graph";
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
            invalidNumberColors,
            invalidUsedAlgorithm
        }
        #endregion

        // Constructor
        #region
        public ReaderTest()
        {
            stringBuilder = new StringBuilder();

            // Fill testsGraphEdgeListDictionary
            testsGraphEdgeListDictionary = new Dictionary<PathEnum, List<string>>
            {
                { PathEnum.valid, new List<string>() { readerPathGraphEdgeListValid1, readerPathGraphEdgeListValid2, readerPathGraphEdgeListValidVertexName } },
                { PathEnum.invalidHeader, new List<string>() { readerPathGraphEdgeListInvalidHeader1, readerPathGraphEdgeListInvalidHeader2 } },
                { PathEnum.invalidBallast, new List<string>() { readerPathGraphEdgeListInvalidBallast1, readerPathGraphEdgeListInvalidBallast2, readerPathGraphEdgeListInvalidBallast3 } },
                { PathEnum.invalidGraphName, new List<string>() { readerPathGraphEdgeListInvalidGraphName } },
                { PathEnum.invalidCountVertices, new List<string>() { readerPathGraphEdgeListInvalidCountVertices1, readerPathGraphEdgeListInvalidCountVertices2, readerPathGraphEdgeListInvalidCountVertices3, readerPathGraphEdgeListInvalidCountVertices4 } },
                { PathEnum.invalidGraph, new List<string>() { readerPathGraphEdgeListInvalidGraph } },
                { PathEnum.invalidCoreData, new List<string>() { readerPathGraphEdgeListInvalidCoreData1, readerPathGraphEdgeListInvalidCoreData2, readerPathGraphEdgeListInvalidCoreData3, readerPathGraphEdgeListInvalidCoreData4 } },
                { PathEnum.invalidColoredGraph, new List<string>() { readerPathGraphEdgeListInvalidColoredGraph } },
                { PathEnum.invalidNumberColors, new List<string>() { readerPathGraphEdgeListInvalidNumberColors, readerPathGraphEdgeListInvalidChromaticNumber } },
                { PathEnum.invalidUsedAlgorithm, new List<string>() { readerPathGraphEdgeListInvalidUsedAlgorithm } }
            };

            // Fill testsGraphAdjacencyMatrixDictionary
            testsGraphAdjacencyMatrixDictionary = new Dictionary<PathEnum, List<string>>
            {
                { PathEnum.valid, new List<string>() { readerPathGraphAdjacencyMatrixValid1, readerPathGraphAdjacencyMatrixValid2 } },
                { PathEnum.invalidHeader, new List<string>() { readerPathGraphAdjacencyMatrixInvalidHeader1, readerPathGraphAdjacencyMatrixInvalidHeader2 } },
                { PathEnum.invalidBallast, new List<string>() { readerPathGraphAdjacencyMatrixInvalidBallast1, readerPathGraphAdjacencyMatrixInvalidBallast2, readerPathGraphAdjacencyMatrixInvalidBallast3 } },
                { PathEnum.invalidGraphName, new List<string>() { readerPathGraphAdjacencyMatrixInvalidGraphName } },
                { PathEnum.invalidCountVertices, new List<string>() { readerPathGraphAdjacencyMatrixInvalidCountVertices1, readerPathGraphAdjacencyMatrixInvalidCountVertices2, readerPathGraphAdjacencyMatrixInvalidCountVertices3, readerPathGraphAdjacencyMatrixInvalidCountVertices4 } },
                { PathEnum.invalidGraph, new List<string>() { readerPathGraphAdjacencyMatrixInvalidGraph } },
                { PathEnum.invalidCoreData, new List<string>() { readerPathGraphAdjacencyMatrixInvalidCoreData1, readerPathGraphAdjacencyMatrixInvalidCoreData2, readerPathGraphAdjacencyMatrixInvalidCoreData3, readerPathGraphAdjacencyMatrixInvalidCoreData4 } },
                { PathEnum.invalidColoredGraph, new List<string>() { readerPathGraphAdjacencyMatrixInvalidColoredGraph } },
                { PathEnum.invalidNumberColors, new List<string>() { readerPathGraphAdjacencyMatrixInvalidNumberColors, readerPathGraphAdjacencyMatrixInvalidChromaticNumber } },
                { PathEnum.invalidUsedAlgorithm, new List<string>() { readerPathGraphAdjacencyMatrixInvalidUsedAlgorithm } }
            };

            // Fill testsGraphAdjacencyListDictionary
            testsGraphAdjacencyListDictionary = new Dictionary<PathEnum, List<string>>
            {
                { PathEnum.valid, new List<string>() { readerPathGraphAdjacencyListValid1, readerPathGraphAdjacencyListValid2, readerPathGraphAdjacencyListValidVertexName } },
                { PathEnum.invalidHeader, new List<string>() { readerPathGraphAdjacencyListInvalidHeader1, readerPathGraphAdjacencyListInvalidHeader2 } },
                { PathEnum.invalidBallast, new List<string>() { readerPathGraphAdjacencyListInvalidBallast1, readerPathGraphAdjacencyListInvalidBallast2, readerPathGraphAdjacencyListInvalidBallast3 } },
                { PathEnum.invalidGraphName, new List<string>() { readerPathGraphAdjacencyListInvalidGraphName } },
                { PathEnum.invalidCountVertices, new List<string>() { readerPathGraphAdjacencyListInvalidCountVertices1, readerPathGraphAdjacencyListInvalidCountVertices2, readerPathGraphAdjacencyListInvalidCountVertices3, readerPathGraphAdjacencyListInvalidCountVertices4 } },
                { PathEnum.invalidGraph, new List<string>() { readerPathGraphAdjacencyListInvalidGraph } },
                { PathEnum.invalidCoreData, new List<string>() { readerPathGraphAdjacencyListInvalidCoreData1, readerPathGraphAdjacencyListInvalidCoreData2 } },
                { PathEnum.invalidColoredGraph, new List<string>() { readerPathGraphAdjacencyListInvalidColoredGraph } },
                { PathEnum.invalidNumberColors, new List<string>() { readerPathGraphAdjacencyListInvalidNumberColors, readerPathGraphAdjacencyListInvalidChromaticNumber } },
                { PathEnum.invalidUsedAlgorithm, new List<string>() { readerPathGraphAdjacencyListInvalidUsedAlgorithm } }
            };

            // Fill testsDictionary
            testsDictionary = new Dictionary<Graph.Graph.GraphRepresentationEnum, Dictionary<PathEnum, List<string>>>
            {
                { Graph.Graph.GraphRepresentationEnum.adjacencyList, testsGraphAdjacencyListDictionary },
                { Graph.Graph.GraphRepresentationEnum.adjacencyMatrix, testsGraphAdjacencyMatrixDictionary },
                { Graph.Graph.GraphRepresentationEnum.edgeList, testsGraphEdgeListDictionary }
            };
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

            foreach (KeyValuePair<Graph.Graph.GraphRepresentationEnum, Dictionary<PathEnum, List<string>>> record in testsDictionary)
            {
                stringBuilder.AppendLine(record.Key.ToString());
                foreach (PathEnum pathEnum in record.Value.Keys)
                {
                    stringBuilder.AppendLine(pathEnum.ToString());
                    Testing(record.Key, pathEnum);
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
                List<string> pathList = new List<string>();
                switch (graphEnum)
                {
                    case Graph.Graph.GraphRepresentationEnum.adjacencyList:
                        pathList = testsGraphAdjacencyListDictionary[pathEnum];
                        break;
                    case Graph.Graph.GraphRepresentationEnum.adjacencyMatrix:
                        pathList = testsGraphAdjacencyMatrixDictionary[pathEnum];
                        break;
                    case Graph.Graph.GraphRepresentationEnum.edgeList:
                        pathList = testsGraphEdgeListDictionary[pathEnum];
                        break;
                    default:
                        throw new MyException.TestsMissingTestException(graphEnum.ToString());
                }
                foreach (string path in pathList)
                {
                    reader = new Reader(path);
                    Testing();
                }
            }
            catch (KeyNotFoundException)
            {
                throw new MyException.TestsMissingTestException(pathEnum.ToString());
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

        // Property
        #region
        public string GetPath()
        {
            return testPathReader;
        }
        #endregion
    }
}
