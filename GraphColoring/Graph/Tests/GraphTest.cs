using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace GraphColoring.Graph.Tests
{
    class GraphTest : GraphColoring.Tests.ITestInterface
    {
        // Variable
        #region
        private StringBuilder stringBuilder;

        // Path
        private string testPath = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Test\Graph.txt";
        private string graphAdjacencyMatrixPath = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\GraphColoring\GraphColoring\Graph\Tests\Graphs\GraphAdjacencyMatrix.txt";
        private string graphEdgeListPath = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\GraphColoring\GraphColoring\Graph\Tests\Graphs\GraphEdgeList.txt";

        // Instance test
        private GraphEdgeList graphEdgeList;
        private GraphAdjacencyMatrix graphAdjacencyMatrix;
        #endregion

        // Constructor
        #region
        public GraphTest()
        {
            stringBuilder = new StringBuilder();
        }
        #endregion

        // Method
        #region
        /// <summary>
        /// Otestuje všechny reprezentace grafu (Graph.GraphRepresentationEnum)
        /// </summary>
        /// <returns>Vrátí report</returns>
        public StringBuilder Test()
        {
            stringBuilder.Clear();

            foreach (Graph.GraphRepresentationEnum graphRepresentationEnum in Enum.GetValues(typeof(Graph.GraphRepresentationEnum)))
            {
                Testing(graphRepresentationEnum);
            }

            return stringBuilder;
        }

        /// <summary>
        /// Otestuje danou reprezentaci grafu (Graph.GraphReprezentationEnum)
        /// </summary>
        /// <param name="graphRepresentationEnum">Reprezentace grafu, kterou chceme otestovat</param>
        /// <returns>Vrátí report</returns>
        public StringBuilder Test(Graph.GraphRepresentationEnum graphRepresentationEnum)
        {
            stringBuilder.Clear();

            Testing(graphRepresentationEnum);

            return stringBuilder;
        }

        private void Testing(Graph.GraphRepresentationEnum graphRepresentationEnum)
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
                    stringBuilder.AppendLine("This graph representation isn't implemented!");
                    break;
            }
        }
        
        /// <summary>
        /// Ze souboru přečte graf (edgeList reprezentace) a daný graf vytvoří
        /// </summary>
        private void GraphEdgeList()
        {
            stringBuilder.AppendLine("Testing: Graph edge list");

            try
            {
                using (StreamReader sr = new StreamReader(graphEdgeListPath))
                {
                    String line = sr.ReadLine();

                    stringBuilder.AppendLine("Read line: " + line);

                    graphEdgeList = new GraphEdgeList(int.Parse(line));

                    while ((line = sr.ReadLine()) != null)
                    {
                        stringBuilder.AppendLine("Read line: " + line);

                        string[] myArray = line.Split(' ');
                        graphEdgeList.AddEdge(myArray[0], myArray[1]);

                        stringBuilder.AppendLine("Split line. " + line);
                        stringBuilder.AppendLine("First edge: " + myArray[0]);
                        stringBuilder.AppendLine("Second edge: " + myArray[1]);
                    }
                }
            }
            catch (Exception e)
            {
                stringBuilder.AppendLine("The file could not be read!");
                stringBuilder.AppendLine(e.Message);
            }

            graphEdgeList.InitializeGraph();
            
            stringBuilder.AppendLine("Graph created.");
            stringBuilder.AppendLine(graphEdgeList.ToString());
        }
        
        /// <summary>
        /// Ze souboru přečte graf (adjacencyMatrix reprezentace) a daný graf vytvoří
        /// </summary>
        private void GraphAdjacencyMatrix()
        {
            stringBuilder.AppendLine("Testing: Graph adjacency matrix");

            try
            {
                using (StreamReader sr = new StreamReader(graphAdjacencyMatrixPath))
                {
                    String line = sr.ReadLine();

                    stringBuilder.AppendLine("Read line: " + line);

                    graphAdjacencyMatrix = new GraphAdjacencyMatrix(int.Parse(line));

                    while ((line = sr.ReadLine()) != null)
                    {
                        stringBuilder.AppendLine("Read line: " + line);

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
                stringBuilder.AppendLine("The file could not be read!");
                stringBuilder.AppendLine(e.Message);
            }

            graphAdjacencyMatrix.InitializeGraph();

            stringBuilder.AppendLine("Graph created.");
            stringBuilder.AppendLine(graphAdjacencyMatrix.ToString());
        }
        #endregion

        // Property
        #region
        public string GetPath()
        {
            return testPath;
        }
        #endregion
    }
}