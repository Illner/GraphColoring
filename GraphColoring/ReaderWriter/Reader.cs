using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GraphColoring.ReaderWriter
{
    class Reader : ReaderWriter
    {
        // Variable
        #region
        /// <summary>
        /// graphName - jméno daného grafu
        /// countVertices - Počet vrcholů grafu
        /// </summary>
        private string graphName;
        private int countVertices;
        #endregion

        // Constructor
        #region
        public Reader(string path) : base(path) { }
        #endregion

        // Method
        #region
        /// <summary>
        /// Přečte soubor s grafem a vytvoří daný graf.
        /// </summary>
        /// <returns>graf ze souboru</returns>
        public Graph.Graph ReadFile()
        {
            // Variable
            string header = "";
            Graph.Graph graph;
            ReaderWriterHeaderEnum graphRepresentation = ReaderWriterHeaderEnum.undefined;

            using (StreamReader streamReader = new StreamReader(GetPath()))
            {
                header = streamReader.ReadLine();
            }

            if (!header.StartsWith(READERWRITERHEADER))
                throw new MyException.ReaderWriterInvalidHeaderException(GetPath());

            ReaderWriterHeaderEnum representation = (ReaderWriterHeaderEnum)Enum.Parse(typeof(ReaderWriterHeaderEnum), header.Split(SEPARATOR).Last());

            switch (graphRepresentation)
            {
                case ReaderWriterHeaderEnum.adjacencyList:
                    graph = ReadFileAdjacencyList();
                    break;
                case ReaderWriterHeaderEnum.adjacencyMatrix:
                    graph = ReadFileAdjacencyMatrix();
                    break;
                case ReaderWriterHeaderEnum.edgeList:
                    graph = ReadFileEdgeList();
                    break;
                case ReaderWriterHeaderEnum.undefined:
                default:
                    throw new MyException.ReaderWriterInvalidHeaderException(GetPath());
            }

            return graph;
        }

        /// <summary>
        /// Přečte soubor s grafem, který je reprezentován pomocí seznamu sousedů a daný graf vytvoří.
        /// </summary>
        /// <returns>graf ze souboru</returns>
        private Graph.Graph ReadFileAdjacencyList()
        {
            // Variable
            Graph.GraphEdgeList graph;
            string firstVertex = "", secondVertex;
            string LEFTSEPARATOR = "(";
            string RIGHTSEPARATOR = ")";

            using (FileStream fileStream = File.OpenRead(GetPath()))
            using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                try
                {
                    // Header
                    streamReader.ReadLine();
                    // Empty row
                    streamReader.ReadLine();
                    // Graph name
                    graphName = streamReader.ReadLine();
                    // Count of vertices
                    countVertices = Int32.Parse(streamReader.ReadLine());

                    // Create graph
                    graph = new Graph.GraphEdgeList(countVertices);
                    graph.SetName(graphName);

                    String line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        if (!line.StartsWith(LEFTSEPARATOR) && !line.EndsWith(RIGHTSEPARATOR))
                        {
                            firstVertex = line;
                            continue;
                        }

                        if (line.StartsWith(LEFTSEPARATOR) && line.EndsWith(RIGHTSEPARATOR))
                        {
                            secondVertex = line.Substring(1, line.Length - 2);
                            graph.AddEdge(firstVertex, secondVertex);
                            continue;
                        }

                        throw new MyException.ReaderWriterInvalidDataException(GetPath());    
                            
                    }

                    graph.InitializeGraph();
                }
                catch (Exception e)
                {
                    if (e is IOException)
                        throw new MyException.ReaderWriterInvalidDataException(GetPath());

                    throw;
                }
            }

            return graph;
        }

        /// <summary>
        /// Přečte soubor s grafem, který je reprezentován pomocí matice sousednosti a daný graf vytvoří.
        /// </summary>
        /// <returns>graf ze souboru</returns>
        private Graph.Graph ReadFileAdjacencyMatrix()
        {
            // Variable
            Graph.GraphAdjacencyMatrix graph;

            using (FileStream fileStream = File.OpenRead(GetPath()))
            using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                try
                {
                    // Header
                    streamReader.ReadLine();
                    // Empty row
                    streamReader.ReadLine();
                    // Graph name
                    graphName = streamReader.ReadLine();
                    // Count of vertices
                    countVertices = Int32.Parse(streamReader.ReadLine());

                    // Create graph
                    graph = new Graph.GraphAdjacencyMatrix(countVertices);
                    graph.SetName(graphName);

                    String line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        List<string> rowString = line.Split(new char[] {SEPARATOR}).ToList();
                        List<bool> rowBoolean = new List<bool>(rowString.Capacity);

                        foreach(String neighbour in rowString)
                        {
                            switch (neighbour)
                            {
                                case "0":
                                    rowBoolean.Add(false);
                                    break;
                                case "1":
                                    rowBoolean.Add(true);
                                    break;
                                default:
                                    throw new MyException.ReaderWriterInvalidDataException(GetPath());
                                    break;

                            }
                        }

                        graph.SetOfNeighborsOfVertex(rowBoolean);
                    }

                    graph.InitializeGraph();
                }
                catch (Exception e)
                {
                    if (e is IOException)
                        throw new MyException.ReaderWriterInvalidDataException(GetPath());

                    throw;
                }
            }

            return graph;
        }

        /// <summary>
        /// Přečte soubor s grafem, který je reprezentován pomocí seznamu hran a daný graf vytvoří.
        /// </summary>
        /// <returns>graf ze souboru</returns>
        private Graph.Graph ReadFileEdgeList()
        {
            // Variable
            Graph.GraphEdgeList graph;

            using (FileStream fileStream = File.OpenRead(GetPath()))
            using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                try
                {
                    // Header
                    streamReader.ReadLine();
                    // Empty row
                    streamReader.ReadLine();
                    // Graph name
                    graphName = streamReader.ReadLine();
                    // Count of vertices
                    countVertices = Int32.Parse(streamReader.ReadLine());

                    // Create graph
                    graph = new Graph.GraphEdgeList(countVertices);
                    graph.SetName(graphName);

                    String line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        string[] edge = line.Split(SEPARATOR);
                        graph.AddEdge(edge[0], edge[1]);
                    }

                    graph.InitializeGraph();
                }
                catch (Exception e)
                {
                    if (e is IOException || e is FormatException || e is ArgumentNullException)
                        throw new MyException.ReaderWriterInvalidDataException(GetPath());

                    throw;
                }
            }

            return graph;
        }
        #endregion
    }
}