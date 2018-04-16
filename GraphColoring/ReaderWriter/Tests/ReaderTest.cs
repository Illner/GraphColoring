#define writeOut
using System;

namespace GraphColoring.ReaderWriter.Tests
{
    class ReaderTest
    {
        // Variable
        #region
        Reader reader;
        Graph.Graph graph;

        // GraphEdgeList
        string readerPathGraphEdgeListValid1 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListValidNumberColors.graph";
        string readerPathGraphEdgeListValid2 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListValidCHromaticNumber.graph";
        string readerPathGraphEdgeListValidVertexName = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListValidVertexName.graph";

        string readerPathGraphEdgeListInvalidHeader1 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListInvalidHeader1.graph";
        string readerPathGraphEdgeListInvalidHeader2 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListInvalidHeader2.graph";

        string readerPathGraphEdgeListInvalidBallast1 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListInvalidBallast1.graph";
        string readerPathGraphEdgeListInvalidBallast2 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListInvalidBallast2.graph";
        string readerPathGraphEdgeListInvalidBallast3 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListInvalidBallast3.graph";

        string readerPathGraphEdgeListInvalidGraphName = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListInvalidGraphName.graph";

        string readerPathGraphEdgeListInvalidCountVertices1 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListInvalidCountVertices1.graph";
        string readerPathGraphEdgeListInvalidCountVertices2 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListInvalidCountVertices2.graph";
        string readerPathGraphEdgeListInvalidCountVertices3 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListInvalidCountVertices3.graph";
        string readerPathGraphEdgeListInvalidCountVertices4 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListInvalidCountVertices4.graph";

        string readerPathGraphEdgeListInvalidGraph = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListInvalidGraph.graph";

        string readerPathGraphEdgeListInvalidCoreData1 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListInvalidCoreData1.graph";
        string readerPathGraphEdgeListInvalidCoreData2 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListInvalidCoreData2.graph";
        string readerPathGraphEdgeListInvalidCoreData3 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListInvalidCoreData3.graph";
        string readerPathGraphEdgeListInvalidCoreData4 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListInvalidCoreData4.graph";

        string readerPathGraphEdgeListInvalidColoredGraph = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListInvalidColoredGraph.graph";

        string readerPathGraphEdgeListInvalidNumberColors = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListInvalidNumberColors.graph";
        string readerPathGraphEdgeListInvalidChromaticNumber = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListInvalidChromaticNumber.graph";

        // GraphAdjacencyMatrix
        string readerPathGraphAdjacencyMatrixValid1 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyMatrix\GraphAdjacencyMatrixValidNumberColors.graph";
        string readerPathGraphAdjacencyMatrixValid2 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyMatrix\GraphAdjacencyMatrixValidChromaticNumber.graph";

        string readerPathGraphAdjacencyMatrixInvalidHeader1 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyMatrix\GraphAdjacencyMatrixInvalidHeader1.graph";
        string readerPathGraphAdjacencyMatrixInvalidHeader2 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyMatrix\GraphAdjacencyMatrixInvalidHeader2.graph";

        string readerPathGraphAdjacencyMatrixInvalidBallast1 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyMatrix\GraphAdjacencyMatrixInvalidBallast1.graph";
        string readerPathGraphAdjacencyMatrixInvalidBallast2 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyMatrix\GraphAdjacencyMatrixInvalidBallast2.graph";
        string readerPathGraphAdjacencyMatrixInvalidBallast3 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyMatrix\GraphAdjacencyMatrixInvalidBallast3.graph";

        string readerPathGraphAdjacencyMatrixInvalidGraphName = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyMatrix\GraphAdjacencyMatrixInvalidGraphName.graph";

        string readerPathGraphAdjacencyMatrixInvalidCountVertices1 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyMatrix\GraphAdjacencyMatrixInvalidCountVertices1.graph";
        string readerPathGraphAdjacencyMatrixInvalidCountVertices2 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyMatrix\GraphAdjacencyMatrixInvalidCountVertices2.graph";
        string readerPathGraphAdjacencyMatrixInvalidCountVertices3 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyMatrix\GraphAdjacencyMatrixInvalidCountVertices3.graph";
        string readerPathGraphAdjacencyMatrixInvalidCountVertices4 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyMatrix\GraphAdjacencyMatrixInvalidCountVertices4.graph";

        string readerPathGraphAdjacencyMatrixInvalidGraph = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyMatrix\GraphAdjacencyMatrixInvalidGraph.graph";

        string readerPathGraphAdjacencyMatrixInvalidCoreData1 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyMatrix\GraphAdjacencyMatrixInvalidCoreData1.graph";
        string readerPathGraphAdjacencyMatrixInvalidCoreData2 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyMatrix\GraphAdjacencyMatrixInvalidCoreData2.graph";
        string readerPathGraphAdjacencyMatrixInvalidCoreData3 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyMatrix\GraphAdjacencyMatrixInvalidCoreData3.graph";
        string readerPathGraphAdjacencyMatrixInvalidCoreData4 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyMatrix\GraphAdjacencyMatrixInvalidCoreData4.graph";

        string readerPathGraphAdjacencyMatrixInvalidColoredGraph = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyMatrix\GraphAdjacencyMatrixInvalidColoredGraph.graph";

        string readerPathGraphAdjacencyMatrixInvalidNumberColors = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyMatrix\GraphAdjacencyMatrixInvalidNumberColors.graph";
        string readerPathGraphAdjacencyMatrixInvalidChromaticNumber = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyMatrix\GraphAdjacencyMatrixInvalidChromaticNumber.graph";
        
        // GraphAdjacencyList
        string readerPathGraphAdjacencyListValid1 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyList\GraphAdjacencyListValidNumberColors.graph";
        string readerPathGraphAdjacencyListValid2 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyList\GraphAdjacencyListValidChromaticNumber.graph";
        string readerPathGraphAdjacencyListValidVertexName = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyList\GraphAdjacencyListValidVertexName.graph";

        string readerPathGraphAdjacencyListInvalidHeader1 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyList\GraphAdjacencyListInvalidHeader1.graph";
        string readerPathGraphAdjacencyListInvalidHeader2 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyList\GraphAdjacencyListInvalidHeader2.graph";

        string readerPathGraphAdjacencyListInvalidBallast1 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyList\GraphAdjacencyListInvalidBallast1.graph";
        string readerPathGraphAdjacencyListInvalidBallast2 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyList\GraphAdjacencyListInvalidBallast2.graph";
        string readerPathGraphAdjacencyListInvalidBallast3 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyList\GraphAdjacencyListInvalidBallast3.graph";

        string readerPathGraphAdjacencyListInvalidGraphName = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyList\GraphAdjacencyListInvalidGraphName.graph";

        string readerPathGraphAdjacencyListInvalidCountVertices1 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyList\GraphAdjacencyListInvalidCountVertices1.graph";
        string readerPathGraphAdjacencyListInvalidCountVertices2 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyList\GraphAdjacencyListInvalidCountVertices2.graph";
        string readerPathGraphAdjacencyListInvalidCountVertices3 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyList\GraphAdjacencyListInvalidCountVertices3.graph";
        string readerPathGraphAdjacencyListInvalidCountVertices4 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyList\GraphAdjacencyListInvalidCountVertices4.graph";

        string readerPathGraphAdjacencyListInvalidGraph = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyList\GraphAdjacencyListInvalidGraph.graph";

        string readerPathGraphAdjacencyListInvalidCoreData1 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyList\GraphAdjacencyListInvalidCoreData1.graph";
        string readerPathGraphAdjacencyListInvalidCoreData2 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyList\GraphAdjacencyListInvalidCoreData2.graph";

        string readerPathGraphAdjacencyListInvalidColoredGraph = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyList\GraphAdjacencyListInvalidColoredGraph.graph";

        string readerPathGraphAdjacencyListInvalidNumberColors = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyList\GraphAdjacencyListInvalidNumberColors.graph";
        string readerPathGraphAdjacencyListInvalidChromaticNumber = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyList\GraphAdjacencyListInvalidChromaticNumber.graph";
        #endregion

        // Enum
        #region
        public enum PathEnum
        {
            Valid,
            InvalidHeader,
            InvalidBallast,
            InvalidGraphName,
            InvalidCountVertices,
            InvalidGraph,
            InvalidCoreData,
            InvalidColoredGraph,
            InvalidNumberColors
        }
        public enum GraphEnum
        {
            EdgeList,
            AdjacencyMatrix,
            AdjacencyList
        }
        #endregion

        // Constructor
        #region
        public ReaderTest()
        {
            foreach (PathEnum pathEnum in Enum.GetValues(typeof(PathEnum)))
            {
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine(pathEnum);
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine("---------------------------------------------------------------");

                foreach (GraphEnum graphEnum in Enum.GetValues(typeof(GraphEnum)))
                {
                    Console.WriteLine("-------------------------------------");
                    Console.WriteLine(graphEnum);
                    Console.WriteLine("-------------------------------------");

                    Test(graphEnum, pathEnum);
                }
            }
        }

        public ReaderTest(GraphEnum graphEnum, PathEnum pathEnum)
        {
            Test(graphEnum, pathEnum);
        }
        #endregion

        // Method
        #region
        private void Test(GraphEnum graphEnum, PathEnum pathEnum)
        {
            switch (graphEnum)
            {
                case GraphEnum.AdjacencyList:
                    #region
                    switch (pathEnum)
                    {
                        case PathEnum.Valid:
                            reader = new Reader(readerPathGraphAdjacencyListValid1);
                            Test();
                            reader = new Reader(readerPathGraphAdjacencyListValid2);
                            Test();
                            reader = new Reader(readerPathGraphAdjacencyListValidVertexName);
                            Test();
                            break;
                        case PathEnum.InvalidHeader:
                            reader = new Reader(readerPathGraphAdjacencyListInvalidHeader1);
                            Test();
                            reader = new Reader(readerPathGraphAdjacencyListInvalidHeader2);
                            Test();
                            break;
                        case PathEnum.InvalidBallast:
                            reader = new Reader(readerPathGraphAdjacencyListInvalidBallast1);
                            Test();
                            reader = new Reader(readerPathGraphAdjacencyListInvalidBallast2);
                            Test();
                            reader = new Reader(readerPathGraphAdjacencyListInvalidBallast3);
                            Test();
                            break;
                        case PathEnum.InvalidGraphName:
                            reader = new Reader(readerPathGraphAdjacencyListInvalidGraphName);
                            Test();
                            break;
                        case PathEnum.InvalidCountVertices:
                            reader = new Reader(readerPathGraphAdjacencyListInvalidCountVertices1);
                            Test();
                            reader = new Reader(readerPathGraphAdjacencyListInvalidCountVertices2);
                            Test();
                            reader = new Reader(readerPathGraphAdjacencyListInvalidCountVertices3);
                            Test();
                            reader = new Reader(readerPathGraphAdjacencyListInvalidCountVertices4);
                            Test();
                            break;
                        case PathEnum.InvalidGraph:
                            reader = new Reader(readerPathGraphAdjacencyListInvalidGraph);
                            Test();
                            break;
                        case PathEnum.InvalidCoreData:
                            reader = new Reader(readerPathGraphAdjacencyListInvalidCoreData1);
                            Test();
                            reader = new Reader(readerPathGraphAdjacencyListInvalidCoreData2);
                            Test();
                            break;
                        case PathEnum.InvalidNumberColors:
                            reader = new Reader(readerPathGraphAdjacencyListInvalidColoredGraph);
                            Test();
                            reader = new Reader(readerPathGraphAdjacencyListInvalidNumberColors);
                            Test();
                            reader = new Reader(readerPathGraphAdjacencyListInvalidChromaticNumber);
                            Test();
                            break;
                        default:
                            reader = null;
                            Console.WriteLine("Isn't implemented");
                            break;
                    }
                    #endregion
                    break;
                case GraphEnum.AdjacencyMatrix:
                    #region
                    switch (pathEnum)
                    {
                        case PathEnum.Valid:
                            reader = new Reader(readerPathGraphAdjacencyMatrixValid1);
                            Test();
                            reader = new Reader(readerPathGraphAdjacencyMatrixValid2);
                            Test();
                            break;
                        case PathEnum.InvalidHeader:
                            reader = new Reader(readerPathGraphAdjacencyMatrixInvalidHeader1);
                            Test();
                            reader = new Reader(readerPathGraphAdjacencyMatrixInvalidHeader2);
                            Test();
                            break;
                        case PathEnum.InvalidBallast:
                            reader = new Reader(readerPathGraphAdjacencyMatrixInvalidBallast1);
                            Test();
                            reader = new Reader(readerPathGraphAdjacencyMatrixInvalidBallast2);
                            Test();
                            reader = new Reader(readerPathGraphAdjacencyMatrixInvalidBallast3);
                            Test();
                            break;
                        case PathEnum.InvalidGraphName:
                            reader = new Reader(readerPathGraphAdjacencyMatrixInvalidGraphName);
                            Test();
                            break;
                        case PathEnum.InvalidCountVertices:
                            reader = new Reader(readerPathGraphAdjacencyMatrixInvalidCountVertices1);
                            Test();
                            reader = new Reader(readerPathGraphAdjacencyMatrixInvalidCountVertices2);
                            Test();
                            reader = new Reader(readerPathGraphAdjacencyMatrixInvalidCountVertices3);
                            Test();
                            reader = new Reader(readerPathGraphAdjacencyMatrixInvalidCountVertices4);
                            Test();
                            break;
                        case PathEnum.InvalidGraph:
                            reader = new Reader(readerPathGraphAdjacencyMatrixInvalidGraph);
                            Test();
                            break;
                        case PathEnum.InvalidCoreData:
                            reader = new Reader(readerPathGraphAdjacencyMatrixInvalidCoreData1);
                            Test();
                            reader = new Reader(readerPathGraphAdjacencyMatrixInvalidCoreData2);
                            Test();
                            reader = new Reader(readerPathGraphAdjacencyMatrixInvalidCoreData3);
                            Test();
                            reader = new Reader(readerPathGraphAdjacencyMatrixInvalidCoreData4);
                            Test();
                            break;
                        case PathEnum.InvalidNumberColors:
                            reader = new Reader(readerPathGraphAdjacencyMatrixInvalidColoredGraph);
                            Test();
                            reader = new Reader(readerPathGraphAdjacencyMatrixInvalidNumberColors);
                            Test();
                            reader = new Reader(readerPathGraphAdjacencyMatrixInvalidChromaticNumber);
                            Test();
                            break;
                        default:
                            reader = null;
                            Console.WriteLine("Isn't implemented");
                            break;
                    }
                    #endregion
                    break;
                case GraphEnum.EdgeList:
                    #region
                    switch (pathEnum)
                    {
                        case PathEnum.Valid:
                            reader = new Reader(readerPathGraphEdgeListValid1);
                            Test();
                            reader = new Reader(readerPathGraphEdgeListValid2);
                            Test();
                            reader = new Reader(readerPathGraphEdgeListValidVertexName);
                            Test();
                            break;
                        case PathEnum.InvalidHeader:
                            reader = new Reader(readerPathGraphEdgeListInvalidHeader1);
                            Test();
                            reader = new Reader(readerPathGraphEdgeListInvalidHeader2);
                            Test();
                            break;
                        case PathEnum.InvalidBallast:
                            reader = new Reader(readerPathGraphEdgeListInvalidBallast1);
                            Test();
                            reader = new Reader(readerPathGraphEdgeListInvalidBallast2);
                            Test();
                            reader = new Reader(readerPathGraphEdgeListInvalidBallast3);
                            Test();
                            break;
                        case PathEnum.InvalidGraphName:
                            reader = new Reader(readerPathGraphEdgeListInvalidGraphName);
                            Test();
                            break;
                        case PathEnum.InvalidCountVertices:
                            reader = new Reader(readerPathGraphEdgeListInvalidCountVertices1);
                            Test();
                            reader = new Reader(readerPathGraphEdgeListInvalidCountVertices2);
                            Test();
                            reader = new Reader(readerPathGraphEdgeListInvalidCountVertices3);
                            Test();
                            reader = new Reader(readerPathGraphEdgeListInvalidCountVertices4);
                            Test();
                            break;
                        case PathEnum.InvalidGraph:
                            reader = new Reader(readerPathGraphEdgeListInvalidGraph);
                            Test();
                            break;
                        case PathEnum.InvalidCoreData:
                            reader = new Reader(readerPathGraphEdgeListInvalidCoreData1);
                            Test();
                            reader = new Reader(readerPathGraphEdgeListInvalidCoreData2);
                            Test();
                            reader = new Reader(readerPathGraphEdgeListInvalidCoreData3);
                            Test();
                            reader = new Reader(readerPathGraphEdgeListInvalidCoreData4);
                            Test();
                            break;
                        case PathEnum.InvalidNumberColors:
                            reader = new Reader(readerPathGraphEdgeListInvalidColoredGraph);
                            Test();
                            reader = new Reader(readerPathGraphEdgeListInvalidNumberColors);
                            Test();
                            reader = new Reader(readerPathGraphEdgeListInvalidChromaticNumber);
                            Test();
                            break;
                        default:
                            reader = null;
                            Console.WriteLine("Isn't implemented");
                            break;
                    }
                    #endregion
                    break;
                default:
                    reader = null;
                    Console.WriteLine("Isn't implemented");
                    break;
            }
        }

        private void Test()
        {
            try
            {
                #if writeOut
                Console.WriteLine("---------------------------------");
                Console.WriteLine("Reading file: " + reader.GetPath());
                graph = reader.ReadFile();
                Console.WriteLine("Graph: \n" + graph.ToString());
                #endif
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        #endregion
    }
}
