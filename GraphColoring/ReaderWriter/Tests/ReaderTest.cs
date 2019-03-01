using System;
using System.Text;
using System.Collections.Generic;


namespace GraphColoring.ReaderWriter.Tests
{
    class ReaderTest : GraphColoring.Tests.ITestInterface
    {
        // Variable
        #region
        private ReaderGraph reader;
        private Graph.IGraphInterface graph;
        private StringBuilder stringBuilder;
        private Dictionary<Graph.Graph.GraphRepresentationEnum, Dictionary<PathEnum, List<string>>> testsDictionary;
        private Dictionary<PathEnum, List<string>> testsGraphEdgeListDictionary;
        private Dictionary<PathEnum, List<string>> testsGraphAdjacencyMatrixDictionary;
        private Dictionary<PathEnum, List<string>> testsGraphAdjacencyListDictionary;

        // Paths
        private string testPathReader = @"Testing\Test\Reader.txt";
        // GraphEdgeList
        private string readerGraphEdgeListValid1 = ReaderResource.GraphEdgeListValidNumberColors;
        private string readerGraphEdgeListValid2 = ReaderResource.GraphEdgeListValidChromaticNumber;
        private string readerGraphEdgeListValidVertexName = ReaderResource.GraphEdgeListValidVertexName;

        private string readerGraphEdgeListInvalidHeader1 = ReaderResource.GraphEdgeListInvalidHeader1;
        private string readerGraphEdgeListInvalidHeader2 = ReaderResource.GraphEdgeListInvalidHeader2;

        private string readerGraphEdgeListInvalidBallast1 = ReaderResource.GraphEdgeListInvalidBallast1;
        private string readerGraphEdgeListInvalidBallast2 = ReaderResource.GraphEdgeListInvalidBallast2;
        private string readerGraphEdgeListInvalidBallast3 = ReaderResource.GraphEdgeListInvalidBallast3;

        private string readerGraphEdgeListInvalidGraphName = ReaderResource.GraphEdgeListInvalidGraphName;

        private string readerGraphEdgeListInvalidCountVertices1 = ReaderResource.GraphEdgeListInvalidCountVertices1;
        private string readerGraphEdgeListInvalidCountVertices2 = ReaderResource.GraphEdgeListInvalidCountVertices2;
        private string readerGraphEdgeListInvalidCountVertices3 = ReaderResource.GraphEdgeListInvalidCountVertices3;
        private string readerGraphEdgeListInvalidCountVertices4 = ReaderResource.GraphEdgeListInvalidCountVertices4;

        private string readerGraphEdgeListInvalidGraph = ReaderResource.GraphEdgeListInvalidGraph;

        private string readerGraphEdgeListInvalidCoreData1 = ReaderResource.GraphEdgeListInvalidCoreData1;
        private string readerGraphEdgeListInvalidCoreData2 = ReaderResource.GraphEdgeListInvalidCoreData2;
        private string readerGraphEdgeListInvalidCoreData3 = ReaderResource.GraphEdgeListInvalidCoreData3;
        private string readerGraphEdgeListInvalidCoreData4 = ReaderResource.GraphEdgeListInvalidCoreData4;

        private string readerGraphEdgeListInvalidColoredGraph = ReaderResource.GraphEdgeListInvalidColoredGraph;

        private string readerGraphEdgeListInvalidNumberColors = ReaderResource.GraphEdgeListInvalidNumberColors;
        private string readerGraphEdgeListInvalidChromaticNumber = ReaderResource.GraphEdgeListInvalidChromaticNumber;

        private string readerGraphEdgeListInvalidUsedAlgorithm = ReaderResource.GraphEdgeListInvalidUsedAlgorithm;

        // GraphAdjacencyMatrix
        private string readerGraphAdjacencyMatrixValid1 = ReaderResource.GraphAdjacencyMatrixValidNumberColors;
        private string readerGraphAdjacencyMatrixValid2 = ReaderResource.GraphAdjacencyMatrixValidChromaticNumber;

        private string readerGraphAdjacencyMatrixInvalidHeader1 = ReaderResource.GraphAdjacencyMatrixInvalidHeader1;
        private string readerGraphAdjacencyMatrixInvalidHeader2 = ReaderResource.GraphAdjacencyMatrixInvalidHeader2;

        private string readerGraphAdjacencyMatrixInvalidBallast1 = ReaderResource.GraphAdjacencyMatrixInvalidBallast1;
        private string readerGraphAdjacencyMatrixInvalidBallast2 = ReaderResource.GraphAdjacencyMatrixInvalidBallast2;
        private string readerGraphAdjacencyMatrixInvalidBallast3 = ReaderResource.GraphAdjacencyMatrixInvalidBallast3;

        private string readerGraphAdjacencyMatrixInvalidGraphName = ReaderResource.GraphAdjacencyMatrixInvalidGraphName;

        private string readerGraphAdjacencyMatrixInvalidCountVertices1 = ReaderResource.GraphAdjacencyMatrixInvalidCountVertices1;
        private string readerGraphAdjacencyMatrixInvalidCountVertices2 = ReaderResource.GraphAdjacencyMatrixInvalidCountVertices2;
        private string readerGraphAdjacencyMatrixInvalidCountVertices3 = ReaderResource.GraphAdjacencyMatrixInvalidCountVertices3;
        private string readerGraphAdjacencyMatrixInvalidCountVertices4 = ReaderResource.GraphAdjacencyMatrixInvalidCountVertices4;

        private string readerGraphAdjacencyMatrixInvalidGraph = ReaderResource.GraphAdjacencyMatrixInvalidGraph;

        private string readerGraphAdjacencyMatrixInvalidCoreData1 = ReaderResource.GraphAdjacencyMatrixInvalidCoreData1;
        private string readerGraphAdjacencyMatrixInvalidCoreData2 = ReaderResource.GraphAdjacencyMatrixInvalidCoreData2;
        private string readerGraphAdjacencyMatrixInvalidCoreData3 = ReaderResource.GraphAdjacencyMatrixInvalidCoreData3;
        private string readerGraphAdjacencyMatrixInvalidCoreData4 = ReaderResource.GraphAdjacencyMatrixInvalidCoreData4;

        private string readerGraphAdjacencyMatrixInvalidColoredGraph = ReaderResource.GraphAdjacencyMatrixInvalidColoredGraph;

        private string readerGraphAdjacencyMatrixInvalidNumberColors = ReaderResource.GraphAdjacencyMatrixInvalidNumberColors;
        private string readerGraphAdjacencyMatrixInvalidChromaticNumber = ReaderResource.GraphAdjacencyMatrixInvalidChromaticNumber;

        private string readerGraphAdjacencyMatrixInvalidUsedAlgorithm = ReaderResource.GraphAdjacencyMatrixInvalidUsedAlgorithm;

        // GraphAdjacencyList
        private string readerGraphAdjacencyListValid1 = ReaderResource.GraphAdjacencyListValidNumberColors;
        private string readerGraphAdjacencyListValid2 = ReaderResource.GraphAdjacencyListValidChromaticNumber;
        private string readerGraphAdjacencyListValidVertexName = ReaderResource.GraphAdjacencyListValidVertexName;

        private string readerGraphAdjacencyListInvalidHeader1 = ReaderResource.GraphAdjacencyListInvalidHeader1;
        private string readerGraphAdjacencyListInvalidHeader2 = ReaderResource.GraphAdjacencyListInvalidHeader2;

        private string readerGraphAdjacencyListInvalidBallast1 = ReaderResource.GraphAdjacencyListInvalidBallast1;
        private string readerGraphAdjacencyListInvalidBallast2 = ReaderResource.GraphAdjacencyListInvalidBallast2;
        private string readerGraphAdjacencyListInvalidBallast3 = ReaderResource.GraphAdjacencyListInvalidBallast3;

        private string readerGraphAdjacencyListInvalidGraphName = ReaderResource.GraphAdjacencyListInvalidGraphName;

        private string readerGraphAdjacencyListInvalidCountVertices1 = ReaderResource.GraphAdjacencyListInvalidCountVertices1;
        private string readerGraphAdjacencyListInvalidCountVertices2 = ReaderResource.GraphAdjacencyListInvalidCountVertices2;
        private string readerGraphAdjacencyListInvalidCountVertices3 = ReaderResource.GraphAdjacencyListInvalidCountVertices3;
        private string readerGraphAdjacencyListInvalidCountVertices4 = ReaderResource.GraphAdjacencyListInvalidCountVertices4;

        private string readerGraphAdjacencyListInvalidGraph = ReaderResource.GraphAdjacencyListInvalidGraph;

        private string readerGraphAdjacencyListInvalidCoreData1 = ReaderResource.GraphAdjacencyListInvalidCoreData1;
        private string readerGraphAdjacencyListInvalidCoreData2 = ReaderResource.GraphAdjacencyListInvalidCoreData2;

        private string readerGraphAdjacencyListInvalidColoredGraph = ReaderResource.GraphAdjacencyListInvalidColoredGraph;

        private string readerGraphAdjacencyListInvalidNumberColors = ReaderResource.GraphAdjacencyListInvalidNumberColors;
        private string readerGraphAdjacencyListInvalidChromaticNumber = ReaderResource.GraphAdjacencyListInvalidChromaticNumber;

        private string readerGraphAdjacencyListInvalidUsedAlgorithm = ReaderResource.GraphAdjacencyListInvalidUsedAlgorithm;
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
                { PathEnum.valid, new List<string>() { readerGraphEdgeListValid1, readerGraphEdgeListValid2, readerGraphEdgeListValidVertexName } },
                { PathEnum.invalidHeader, new List<string>() { readerGraphEdgeListInvalidHeader1, readerGraphEdgeListInvalidHeader2 } },
                { PathEnum.invalidBallast, new List<string>() { readerGraphEdgeListInvalidBallast1, readerGraphEdgeListInvalidBallast2, readerGraphEdgeListInvalidBallast3 } },
                { PathEnum.invalidGraphName, new List<string>() { readerGraphEdgeListInvalidGraphName } },
                { PathEnum.invalidCountVertices, new List<string>() { readerGraphEdgeListInvalidCountVertices1, readerGraphEdgeListInvalidCountVertices2, readerGraphEdgeListInvalidCountVertices3, readerGraphEdgeListInvalidCountVertices4 } },
                { PathEnum.invalidGraph, new List<string>() { readerGraphEdgeListInvalidGraph } },
                { PathEnum.invalidCoreData, new List<string>() { readerGraphEdgeListInvalidCoreData1, readerGraphEdgeListInvalidCoreData2, readerGraphEdgeListInvalidCoreData3, readerGraphEdgeListInvalidCoreData4 } },
                { PathEnum.invalidColoredGraph, new List<string>() { readerGraphEdgeListInvalidColoredGraph } },
                { PathEnum.invalidNumberColors, new List<string>() { readerGraphEdgeListInvalidNumberColors, readerGraphEdgeListInvalidChromaticNumber } },
                { PathEnum.invalidUsedAlgorithm, new List<string>() { readerGraphEdgeListInvalidUsedAlgorithm } }
            };

            // Fill testsGraphAdjacencyMatrixDictionary
            testsGraphAdjacencyMatrixDictionary = new Dictionary<PathEnum, List<string>>
            {
                { PathEnum.valid, new List<string>() { readerGraphAdjacencyMatrixValid1, readerGraphAdjacencyMatrixValid2 } },
                { PathEnum.invalidHeader, new List<string>() { readerGraphAdjacencyMatrixInvalidHeader1, readerGraphAdjacencyMatrixInvalidHeader2 } },
                { PathEnum.invalidBallast, new List<string>() { readerGraphAdjacencyMatrixInvalidBallast1, readerGraphAdjacencyMatrixInvalidBallast2, readerGraphAdjacencyMatrixInvalidBallast3 } },
                { PathEnum.invalidGraphName, new List<string>() { readerGraphAdjacencyMatrixInvalidGraphName } },
                { PathEnum.invalidCountVertices, new List<string>() { readerGraphAdjacencyMatrixInvalidCountVertices1, readerGraphAdjacencyMatrixInvalidCountVertices2, readerGraphAdjacencyMatrixInvalidCountVertices3, readerGraphAdjacencyMatrixInvalidCountVertices4 } },
                { PathEnum.invalidGraph, new List<string>() { readerGraphAdjacencyMatrixInvalidGraph } },
                { PathEnum.invalidCoreData, new List<string>() { readerGraphAdjacencyMatrixInvalidCoreData1, readerGraphAdjacencyMatrixInvalidCoreData2, readerGraphAdjacencyMatrixInvalidCoreData3, readerGraphAdjacencyMatrixInvalidCoreData4 } },
                { PathEnum.invalidColoredGraph, new List<string>() { readerGraphAdjacencyMatrixInvalidColoredGraph } },
                { PathEnum.invalidNumberColors, new List<string>() { readerGraphAdjacencyMatrixInvalidNumberColors, readerGraphAdjacencyMatrixInvalidChromaticNumber } },
                { PathEnum.invalidUsedAlgorithm, new List<string>() { readerGraphAdjacencyMatrixInvalidUsedAlgorithm } }
            };

            // Fill testsGraphAdjacencyListDictionary
            testsGraphAdjacencyListDictionary = new Dictionary<PathEnum, List<string>>
            {
                { PathEnum.valid, new List<string>() { readerGraphAdjacencyListValid1, readerGraphAdjacencyListValid2, readerGraphAdjacencyListValidVertexName } },
                { PathEnum.invalidHeader, new List<string>() { readerGraphAdjacencyListInvalidHeader1, readerGraphAdjacencyListInvalidHeader2 } },
                { PathEnum.invalidBallast, new List<string>() { readerGraphAdjacencyListInvalidBallast1, readerGraphAdjacencyListInvalidBallast2, readerGraphAdjacencyListInvalidBallast3 } },
                { PathEnum.invalidGraphName, new List<string>() { readerGraphAdjacencyListInvalidGraphName } },
                { PathEnum.invalidCountVertices, new List<string>() { readerGraphAdjacencyListInvalidCountVertices1, readerGraphAdjacencyListInvalidCountVertices2, readerGraphAdjacencyListInvalidCountVertices3, readerGraphAdjacencyListInvalidCountVertices4 } },
                { PathEnum.invalidGraph, new List<string>() { readerGraphAdjacencyListInvalidGraph } },
                { PathEnum.invalidCoreData, new List<string>() { readerGraphAdjacencyListInvalidCoreData1, readerGraphAdjacencyListInvalidCoreData2 } },
                { PathEnum.invalidColoredGraph, new List<string>() { readerGraphAdjacencyListInvalidColoredGraph } },
                { PathEnum.invalidNumberColors, new List<string>() { readerGraphAdjacencyListInvalidNumberColors, readerGraphAdjacencyListInvalidChromaticNumber } },
                { PathEnum.invalidUsedAlgorithm, new List<string>() { readerGraphAdjacencyListInvalidUsedAlgorithm } }
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
        /// Test all types of files (PathEnum) and representations of graphs (Graph.GraphRepresentationEnum)
        /// </summary>
        /// <returns>report</returns>
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
        /// Test a particular type of file with a particular representation of graph
        /// </summary>
        /// <param name="graphRepresentation">representation of graph</param>
        /// <param name="pathEnum">type of file</param>
        /// <returns>report</returns>
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
                List<string> fileList = new List<string>();
                switch (graphEnum)
                {
                    case Graph.Graph.GraphRepresentationEnum.adjacencyList:
                        fileList = testsGraphAdjacencyListDictionary[pathEnum];
                        break;
                    case Graph.Graph.GraphRepresentationEnum.adjacencyMatrix:
                        fileList = testsGraphAdjacencyMatrixDictionary[pathEnum];
                        break;
                    case Graph.Graph.GraphRepresentationEnum.edgeList:
                        fileList = testsGraphEdgeListDictionary[pathEnum];
                        break;
                    default:
                        throw new MyException.TestsException.TestsMissingTestException(graphEnum.ToString());
                }
                foreach (string file in fileList)
                {
                    reader = new ReaderGraph(ReaderWriter.CreateTestFile(file), false);
                    Testing();
                }
            }
            catch (KeyNotFoundException)
            {
                throw new MyException.TestsException.TestsMissingTestException(pathEnum.ToString());
            }
            catch (MyException.ReaderWriterException.ReaderWriterException e)
            {
                stringBuilder.AppendLine(e.Message);
            }
        }

        /// <summary>
        /// Read a file with a graph and construct it
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
