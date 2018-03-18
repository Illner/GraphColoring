#define writeOut
using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;

namespace GraphColoring.Graph.Tests
{
    class GraphTest
    {
        // Variable
        string pathGraphAdjacencyMatrix = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\GraphColoring\GraphColoring\Graph\Tests\Graphs\GraphAdjacencyMatrix.txt";
        string pathGraphEdgeList = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\GraphColoring\GraphColoring\Graph\Tests\Graphs\GraphEdgeList.txt";
        GraphAdjacencyMatrix graphAdjacencyMatrix;
        GraphEdgeList graphEdgeList;
        Stopwatch stopwatch = new Stopwatch();

        /// <summary>
        /// Vytvoří graf, který je zadaný danou reprezentací
        /// </summary>
        /// <param name="graphRepresentation">jakou representaci grafu chci otestovat</param>
        public GraphTest(Graph.GraphRepresentationEnum graphRepresentation)
        {
            switch (graphRepresentation)
            {
                case Graph.GraphRepresentationEnum.edgeList:
                    stopwatch.Start();
                    GraphEdgeList();
                    stopwatch.Stop();
                    Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
                    break;
                case Graph.GraphRepresentationEnum.adjacencyMatrix:
                    stopwatch.Start();
                    GraphAdjacencyMatrix();
                    stopwatch.Stop();
                    Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
                    break;
                default:
                    Console.WriteLine("This graph representation isn't implemented");
                    break;
            }
        }
        
        /// <summary>
        /// Testuje graf, který je zadán pomocí seznamu hran
        /// </summary>
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
                Console.WriteLine(e);
            }

            #if writeOut
            Console.WriteLine("Graph created");
            graphEdgeList.WriteOutGraph();
            #endif
        }

        /// <summary>
        /// Testuje graf, který je zadán pomocí matice sousednosti
        /// </summary>
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
                Console.WriteLine(e);
            }

            #if writeOut
            Console.WriteLine("Graph created");
            graphAdjacencyMatrix.WriteOutGraph();
            #endif
        }
    }
}
