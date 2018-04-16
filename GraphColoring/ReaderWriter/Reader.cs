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
            Graph.Graph graph = null;
            Graph.GraphEdgeList graphEdgeList;
            Graph.GraphAdjacencyMatrix graphAdjacencyMatrix;

            string header = "";
            int countVertices, numberColors;
            ReaderWriterHeaderEnum graphRepresentationEnum;
            string line, graphName, numberColorsString = "";

            using (FileStream fileStream = File.OpenRead(GetPath()))
            using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                try
                {
                    // Header
                    header = streamReader.ReadLine();

                    /// Invalid header
                    if (!header.StartsWith(READERWRITERHEADER))
                        throw new MyException.ReaderWriterInvalidHeaderException();

                    graphRepresentationEnum = (ReaderWriterHeaderEnum)Enum.Parse(typeof(ReaderWriterHeaderEnum), header.Split(SEPARATOR).Last());

                    // Ballast
                    line = streamReader.ReadLine();
                    if (line != READERWRITERBALLAST)
                        throw new MyException.ReaderWriterInvalidFormatException("Ballast (1)");

                    // Graph name
                    line = streamReader.ReadLine();
                    if (!line.StartsWith(READERWRITERNAME))
                        throw new MyException.ReaderWriterInvalidFormatException("Name");
                    graphName = line.Substring(READERWRITERNAME.Length);

                    // Count of vertices
                    line = streamReader.ReadLine();
                    if (!line.StartsWith(READERWRITERCOUNTVERTICES))
                        throw new MyException.ReaderWriterInvalidFormatException("Count of vertices");
                    countVertices = Int32.Parse(line.Substring(READERWRITERCOUNTVERTICES.Length));
                    if (countVertices < 0)
                        throw new MyException.ReaderWriterInvalidFormatException("Count of vertices");

                    // Ballast
                    line = streamReader.ReadLine();
                    if (line != READERWRITERBALLAST)
                        throw new MyException.ReaderWriterInvalidFormatException("Ballast (2)");

                    // Graph
                    line = streamReader.ReadLine();
                    if (line != READERWRITERGRAPH)
                        throw new MyException.ReaderWriterInvalidFormatException("Graph");
                    
                    switch (graphRepresentationEnum)
                    {
                        case ReaderWriterHeaderEnum.adjacencyList:
                            graphEdgeList = new Graph.GraphEdgeList(countVertices);
                            graphEdgeList.SetName(graphName);

                            ReadFileAdjacencyList(graphEdgeList, streamReader);
                            graph = graphEdgeList;
                            break;
                        case ReaderWriterHeaderEnum.adjacencyMatrix:
                            graphAdjacencyMatrix = new Graph.GraphAdjacencyMatrix(countVertices);
                            graphAdjacencyMatrix.SetName(graphName);

                            ReadFileAdjacencyMatrix(graphAdjacencyMatrix, streamReader, countVertices);
                            graph = graphAdjacencyMatrix;
                            break;
                        case ReaderWriterHeaderEnum.edgeList:
                            graphEdgeList = new Graph.GraphEdgeList(countVertices);
                            graphEdgeList.SetName(graphName);

                            ReadFileEdgeList(graphEdgeList, streamReader);
                            graph = graphEdgeList;
                            break;
                        default:
                            throw new MyException.ReaderWriterInvalidHeaderException();
                    }

                    // Colored graph
                    line = streamReader.ReadLine();
                    if (line != READERWRITERCOLOREDGRAPH)
                        throw new MyException.ReaderWriterInvalidFormatException("Colored graph");

                    // Number of colors
                    line = streamReader.ReadLine();
                    if (!line.StartsWith(READERWRITERNUMBEROFCOLORS) && !line.StartsWith(READERWRITERCHROMATICNUMBER))
                        throw new MyException.ReaderWriterInvalidFormatException("Number of colors / Chromatic number");
                    if (line.StartsWith(READERWRITERNUMBEROFCOLORS))
                        numberColorsString = line.Substring(READERWRITERNUMBEROFCOLORS.Length);
                    else
                        numberColorsString = line.Substring(READERWRITERCHROMATICNUMBER.Length);

                    if (numberColorsString != "")
                    {
                        numberColors = Int32.Parse(numberColorsString);

                        // Colored graph
                        // TODO
                    }
                }
                catch (ArgumentException)
                {
                    throw new MyException.ReaderWriterInvalidHeaderException();

                }
                catch (FormatException)
                {
                    if (numberColorsString == "")
                        throw new MyException.ReaderWriterInvalidFormatException("Count of vertices (parse)");
                    throw new MyException.ReaderWriterInvalidFormatException("number of colors / Chromatic number (parse)");
                }
                catch (IOException)
                {
                    throw new MyException.ReaderWriterInvalidDataException();
                }
            }

            return graph;
        }

        /// <summary>
        /// Přečte soubor s grafem, který je reprezentován pomocí seznamu sousedů a daný graf vytvoří.
        /// </summary>
        private void ReadFileAdjacencyList(Graph.GraphEdgeList graph, StreamReader streamReader)
        {
            // Variable
            string line;
            string firstVertex = "", secondVertex;
            const string LEFTSEPARATOR = "(";
            const string RIGHTSEPARATOR = ")";

            // Read graph
            while ((line = streamReader.ReadLine()) != "")
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

                throw new MyException.ReaderWriterInvalidDataException();
            }

            graph.InitializeGraph();
        }

        /// <summary>
        /// Přečte soubor s grafem, který je reprezentován pomocí matice sousednosti a daný graf vytvoří.
        /// </summary>
        private void ReadFileAdjacencyMatrix(Graph.GraphAdjacencyMatrix graph, StreamReader streamReader, int countVertices)
        {
            // Variable
            string line;
            int countRows = 0;

            // Read graph
            while ((line = streamReader.ReadLine()) != "")
            {
                List<string> rowString = line.Split(new char[] { SEPARATOR }).ToList();
                List<bool> rowBoolean = new List<bool>(rowString.Capacity);

                foreach (String neighbour in rowString)
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
                            throw new MyException.ReaderWriterInvalidDataException();
                    }
                }

                if (rowBoolean.Capacity != countVertices)
                    throw new MyException.ReaderWriterInvalidDataException();

                if (++countRows > countVertices)
                    throw new MyException.ReaderWriterInvalidDataException();
                
                graph.SetOfNeighborsOfVertex(rowBoolean);
            }

            graph.InitializeGraph();
        }

        /// <summary>
        /// Přečte soubor s grafem, který je reprezentován pomocí seznamu hran a daný graf vytvoří.
        /// </summary>
        private void ReadFileEdgeList(Graph.GraphEdgeList graph, StreamReader streamReader)
        {
            // Variable
            string line;
            string[] edge = null;
            const string LEFTSEPARATOR = "(";
            const string RIGHTSEPARATOR = ")";
            int startVertex1Index = 0, endVertex1Index = 0;
            int startVertex2Index = 0, endVertex2Index = 0;

            // Read graph
            while ((line = streamReader.ReadLine()) != "")
            {
                if (line.StartsWith(LEFTSEPARATOR) || line.EndsWith(RIGHTSEPARATOR))
                {
                    edge = new string[2];

                    // (Vertex name) VertexName
                    if (line.StartsWith(LEFTSEPARATOR) && !line.EndsWith(RIGHTSEPARATOR))
                    {
                        startVertex1Index = 1;
                        endVertex1Index = line.IndexOf(RIGHTSEPARATOR);
                        startVertex2Index = endVertex1Index + 2;
                        endVertex2Index = line.Length;

                        if (endVertex1Index == -1)
                            throw new MyException.ReaderWriterInvalidDataException();
                    }
                    else
                    {
                        // VertexName (Vertex name)
                        if (!line.StartsWith(LEFTSEPARATOR) && line.EndsWith(RIGHTSEPARATOR))
                        {
                            startVertex1Index = 0;
                            endVertex1Index = line.IndexOf(LEFTSEPARATOR) - 1;
                            startVertex2Index = endVertex1Index + 2;
                            endVertex2Index = line.Length - 1;

                            if (endVertex1Index == -2)
                                throw new MyException.ReaderWriterInvalidDataException();
                        }
                        // (Vertex name) (Vertex name)
                        else
                        {
                            startVertex1Index = 1;
                            endVertex1Index = line.IndexOf(RIGHTSEPARATOR);
                            startVertex2Index = line.LastIndexOf(LEFTSEPARATOR) + 1;
                            endVertex2Index = line.Length - 1;

                            if (startVertex1Index == startVertex2Index && endVertex1Index == endVertex2Index)
                                throw new MyException.ReaderWriterInvalidDataException();
                        }
                    }

                    edge[0] = line.Substring(startVertex1Index, endVertex1Index - startVertex1Index);
                    edge[1] = line.Substring(startVertex2Index, endVertex2Index - startVertex2Index);
                }
                else
                {
                    edge = line.Split(SEPARATOR);
                }

                if (edge.Length != 2)
                    throw new MyException.ReaderWriterInvalidDataException();

                graph.AddEdge(edge[0], edge[1]);
            }

            graph.InitializeGraph();
        }
        #endregion
    }
}