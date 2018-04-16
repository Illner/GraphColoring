#define writeOut
using System;
using System.IO;
using System.Collections.Generic;

namespace GraphColoring.Graph.Tests
{
    class GraphTest
    {
        // Variable
        #region
        GraphEdgeList graphEdgeList;
        GraphAdjacencyMatrix graphAdjacencyMatrix;

        string pathGraphAdjacencyMatrix = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\GraphColoring\GraphColoring\Graph\Tests\Graphs\GraphAdjacencyMatrix.txt";
        string pathGraphEdgeList = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\GraphColoring\GraphColoring\Graph\Tests\Graphs\GraphEdgeList.txt";
        #endregion

        // Constructor
        #region
        public GraphTest(Graph.GraphRepresentationEnum graphRepresentationEnum)
        {
            Test(graphRepresentationEnum);
        }

        public GraphTest()
        {
            foreach (Graph.GraphRepresentationEnum graphRepresentationEnum in Enum.GetValues(typeof(Graph.GraphRepresentationEnum)))
            {
                Test(graphRepresentationEnum);
            }
        }
        #endregion

        // Method
        #region
        private void Test(Graph.GraphRepresentationEnum graphRepresentationEnum)
        {
            switch (graphRepresentationEnum)
            {
                case Graph.GraphRepresentationEnum.edgeList:
                    GraphEdgeList();
                    break;
                case Graph.GraphRepresentationEnum.adjacencyMatrix:
                    GraphAdjacencyMatrix();
                    break;
                default:
                    Console.WriteLine("This graph representation isn't implemented");
                    break;
            }
        }
        
        private void GraphEdgeList()
        {
            Console.WriteLine("Testing: Graph edge list");

            try
            {
                using (StreamReader sr = new StreamReader(pathGraphEdgeList))
                {
                    String line = sr.ReadLine();

                    #if writeOut
                    Console.WriteLine("Read line: " + line);
                    #endif

                    graphEdgeList = new GraphEdgeList(int.Parse(line));

                    while ((line = sr.ReadLine()) != null)
                    {
                        #if writeOut
                        Console.WriteLine("Read line: " + line);
                        #endif

                        string[] myArray = line.Split(' ');
                        graphEdgeList.AddEdge(myArray[0], myArray[1]);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            graphEdgeList.InitializeGraph();

            #if writeOut
            Console.WriteLine("Graph created");
            Console.WriteLine(graphEdgeList.ToString());
            #endif
        }
        
        private void GraphAdjacencyMatrix()
        {
            Console.WriteLine("Testing: Graph adjacency matrix");

            try
            {
                using (StreamReader sr = new StreamReader(pathGraphAdjacencyMatrix))
                {
                    String line = sr.ReadLine();
                    #if writeOut
                    Console.WriteLine("Read line: " + line);
                    #endif

                    graphAdjacencyMatrix = new GraphAdjacencyMatrix(int.Parse(line));

                    while ((line = sr.ReadLine()) != null)
                    {
                        #if writeOut
                        Console.WriteLine("Read line: " + line);
                        #endif

                        string[] myArray = line.Split(' ');
                        List<bool> myList = new List<bool>();
                        foreach (string record in myArray)
                        {
                            if (record == "0")
                                myList.Add(false);
                            if (record == "1")
                                myList.Add(true);
                        }

                        graphAdjacencyMatrix.SetOfNeighborsOfVertex(myList);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            graphAdjacencyMatrix.InitializeGraph();

            #if writeOut
            Console.WriteLine("Graph created");
            Console.WriteLine(graphAdjacencyMatrix.ToString());
            #endif
        }
        #endregion
    }
}
