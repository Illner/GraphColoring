#define writeOut
using System;
using System.Diagnostics;

namespace GraphColoring.ReaderWriter.Tests
{
    class ReaderTest
    {
        // Variable
        #region
        Reader reader;
        Graph.Graph graph;
        Stopwatch stopwatch = new Stopwatch();

        // GraphEdgeList
        string readerPathGraphEdgeList = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeList.graph";
        string readerPathGraphEdgeListInvalidHeader1 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListInvalidHeader1.graph";
        string readerPathGraphEdgeListInvalidHeader2 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListInvalidHeader2.graph";
        string readerPathGraphEdgeListInvalidBallast = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListInvalidBallast.graph";
        string readerPathGraphEdgeListInvalidCountVertices1 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListInvalidCountVertices1.graph";
        string readerPathGraphEdgeListInvalidCountVertices2 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListInvalidCountVertices2.graph";
        string readerPathGraphEdgeListInvalidCoreData1 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListInvalidCoreData1.graph";
        string readerPathGraphEdgeListInvalidCoreData2 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListInvalidCoreData2.graph";
        string readerPathGraphEdgeListInvalidCoreData3 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphEdgeList\GraphEdgeListInvalidCoreData3.graph";

        // GraphAdjacencyMatrix
        string readerPathGraphAdjacencyMatrix = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyMatrix\GraphAdjacencyMatrix.graph";
        string readerPathGraphAdjacencyMatrixInvalidHeader1 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyMatrix\GraphAdjacencyMatrixInvalidHeader1.graph";
        string readerPathGraphAdjacencyMatrixInvalidHeader2 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyMatrix\GraphAdjacencyMatrixInvalidHeader2.graph";
        string readerPathGraphAdjacencyMatrixInvalidBallast = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyMatrix\GraphAdjacencyMatrixInvalidBallast.graph";
        string readerPathGraphAdjacencyMatrixInvalidCountVertices1 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyMatrix\GraphAdjacencyMatrixInvalidCountVertices1.graph";
        string readerPathGraphAdjacencyMatrixInvalidCountVertices2 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyMatrix\GraphAdjacencyMatrixInvalidCountVertices2.graph";
        string readerPathGraphAdjacencyMatrixInvalidCoreData1 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyMatrix\GraphAdjacencyMatrixInvalidCoreData1.graph";
        string readerPathGraphAdjacencyMatrixInvalidCoreData2 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyMatrix\GraphAdjacencyMatrixInvalidCoreData2.graph";
        string readerPathGraphAdjacencyMatrixInvalidCoreData3 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyMatrix\GraphAdjacencyMatrixInvalidCoreData3.graph";

        // GraphAdjacencyList
        string readerPathGraphAdjacencyList = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyList\GraphAdjacencyList.graph";
        string readerPathGraphAdjacencyListInvalidHeader1 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyList\GraphAdjacencyListInvalidHeader1.graph";
        string readerPathGraphAdjacencyListInvalidHeader2 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyList\GraphAdjacencyListInvalidHeader2.graph";
        string readerPathGraphAdjacencyListInvalidBallast = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyList\GraphAdjacencyListInvalidBallast.graph";
        string readerPathGraphAdjacencyListInvalidCountVertices1 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyList\GraphAdjacencyListInvalidCountVertices1.graph";
        string readerPathGraphAdjacencyListInvalidCountVertices2 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyList\GraphAdjacencyListInvalidCountVertices2.graph";
        string readerPathGraphAdjacencyListInvalidCoreData1 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyList\GraphAdjacencyListInvalidCoreData1.graph";
        string readerPathGraphAdjacencyListInvalidCoreData2 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyList\GraphAdjacencyListInvalidCoreData2.graph";
        string readerPathGraphAdjacencyListInvalidCoreData3 = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader\GraphAdjacencyList\GraphAdjacencyListInvalidCoreData3.graph";
        #endregion

        // Enum
        #region
        public enum PathEnum
        {
            Valid,
            InvalidHeader,
            InvalidBallast,
            InvalidCountVertices,
            InvalidCoreData
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
        public ReaderTest(GraphEnum graphEnum, PathEnum pathEnum)
        {
            switch (graphEnum)
            {
                case GraphEnum.AdjacencyList:
                    #region
                switch (pathEnum)
                {
                    case PathEnum.Valid:
                        stopwatch.Start();
                        reader = new Reader(readerPathGraphAdjacencyList);
                        Test();
                        stopwatch.Stop();
                        Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
                        break;
                    case PathEnum.InvalidHeader:
                        stopwatch.Start();
                        reader = new Reader(readerPathGraphAdjacencyListInvalidHeader1);
                        Test();
                        reader = new Reader(readerPathGraphAdjacencyListInvalidHeader2);
                        Test();
                        stopwatch.Stop();
                        Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
                        break;
                    case PathEnum.InvalidBallast:
                        stopwatch.Start();
                        reader = new Reader(readerPathGraphAdjacencyListInvalidBallast);
                        Test();
                        stopwatch.Stop();
                        Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
                        break;
                    case PathEnum.InvalidCountVertices:
                        stopwatch.Start();
                        reader = new Reader(readerPathGraphAdjacencyListInvalidCountVertices1);
                        Test();
                        reader = new Reader(readerPathGraphAdjacencyListInvalidCountVertices2);
                        Test();
                        stopwatch.Stop();
                        Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
                        break;
                    case PathEnum.InvalidCoreData:
                        stopwatch.Start();
                        reader = new Reader(readerPathGraphAdjacencyListInvalidCoreData1);
                        Test();
                        reader = new Reader(readerPathGraphAdjacencyListInvalidCoreData2);
                        Test();
                        reader = new Reader(readerPathGraphAdjacencyListInvalidCoreData3);
                        Test();
                        stopwatch.Stop();
                        Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
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
                            stopwatch.Start();
                            reader = new Reader(readerPathGraphAdjacencyMatrix);
                            Test();
                            stopwatch.Stop();
                            Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
                            break;
                        case PathEnum.InvalidHeader:
                            stopwatch.Start();
                            reader = new Reader(readerPathGraphAdjacencyMatrixInvalidHeader1);
                            Test();
                            reader = new Reader(readerPathGraphAdjacencyMatrixInvalidHeader2);
                            Test();
                            stopwatch.Stop();
                            Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
                            break;
                        case PathEnum.InvalidBallast:
                            stopwatch.Start();
                            reader = new Reader(readerPathGraphAdjacencyMatrixInvalidBallast);
                            Test();
                            stopwatch.Stop();
                            Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
                            break;
                        case PathEnum.InvalidCountVertices:
                            stopwatch.Start();
                            reader = new Reader(readerPathGraphAdjacencyMatrixInvalidCountVertices1);
                            Test();
                            reader = new Reader(readerPathGraphAdjacencyMatrixInvalidCountVertices2);
                            Test();
                            stopwatch.Stop();
                            Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
                            break;
                        case PathEnum.InvalidCoreData:
                            stopwatch.Start();
                            reader = new Reader(readerPathGraphAdjacencyMatrixInvalidCoreData1);
                            Test();
                            reader = new Reader(readerPathGraphAdjacencyMatrixInvalidCoreData2);
                            Test();
                            reader = new Reader(readerPathGraphAdjacencyMatrixInvalidCoreData3);
                            Test();
                            stopwatch.Stop();
                            Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
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
                            stopwatch.Start();
                            reader = new Reader(readerPathGraphEdgeList);
                            Test();
                            stopwatch.Stop();
                            Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
                            break;
                        case PathEnum.InvalidHeader:
                            stopwatch.Start();
                            reader = new Reader(readerPathGraphEdgeListInvalidHeader1);
                            Test();
                            reader = new Reader(readerPathGraphEdgeListInvalidHeader2);
                            Test();
                            stopwatch.Stop();
                            Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
                            break;
                        case PathEnum.InvalidBallast:
                            stopwatch.Start();
                            reader = new Reader(readerPathGraphEdgeListInvalidBallast);
                            Test();
                            stopwatch.Stop();
                            Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
                            break;
                        case PathEnum.InvalidCountVertices:
                            stopwatch.Start();
                            reader = new Reader(readerPathGraphEdgeListInvalidCountVertices1);
                            Test();
                            reader = new Reader(readerPathGraphEdgeListInvalidCountVertices2);
                            Test();
                            stopwatch.Stop();
                            Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
                            break;
                        case PathEnum.InvalidCoreData:
                            stopwatch.Start();
                            reader = new Reader(readerPathGraphEdgeListInvalidCoreData1);
                            Test();
                            reader = new Reader(readerPathGraphEdgeListInvalidCoreData2);
                            Test();
                            reader = new Reader(readerPathGraphEdgeListInvalidCoreData3);
                            Test();
                            stopwatch.Stop();
                            Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
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
        #endregion

        // Method
        #region
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
