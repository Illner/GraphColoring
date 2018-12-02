using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GraphColoring.ReaderWriter
{
    class ReaderGraph : ReaderWriter, IReaderGraphInterface
    {
        // Constructor
        #region
        public ReaderGraph(string path) : base(path) { }
        public ReaderGraph(string path, bool checkPath) : base(path, checkPath) { }
        #endregion

        // Method
        #region
        /// <summary>
        /// Přečte soubor s grafem a vytvoří daný graf.
        /// </summary>
        /// <returns>graf ze souboru</returns>
        public Graph.IGraphInterface ReadFile()
        {
            // Variable
            Graph.IGraphInterface graph = null;
            Graph.IGraphEdgeListInterface graphEdgeList;
            Graph.IGraphAdjacencyMatrixInterface graphAdjacencyMatrix;

            string header = "";
            int countVertices, numberColors;
            string line, graphName, numberColorsString;
            ReaderWriterHeaderEnum graphRepresentationEnum;

            using (FileStream fileStream = File.OpenRead(GetPath()))
            using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                try
                {
                    // Header
                    header = streamReader.ReadLine();

                    /// Invalid header
                    if (!header.StartsWith(READERWRITERHEADER))
                        throw new MyException.ReaderWriterException.ReaderWriterInvalidHeaderException("Invalid header");

                    graphRepresentationEnum = (ReaderWriterHeaderEnum)Enum.Parse(typeof(ReaderWriterHeaderEnum), header.Split(SEPARATOR).Last());

                    // Ballast
                    line = streamReader.ReadLine();
                    if (line != READERWRITERBALLAST)
                        throw new MyException.ReaderWriterException.ReaderWriterInvalidFormatException("Missing empty line");

                    // Graph name
                    line = streamReader.ReadLine();
                    if (!line.StartsWith(READERWRITERNAME))
                        throw new MyException.ReaderWriterException.ReaderWriterInvalidFormatException("Invalid name of graph (header)");
                    graphName = line.Substring(READERWRITERNAME.Length);

                    // Count of vertices
                    line = streamReader.ReadLine();
                    if (!line.StartsWith(READERWRITERCOUNTVERTICES))
                        throw new MyException.ReaderWriterException.ReaderWriterInvalidFormatException("Invalid vertex count (header)");
                    countVertices = Int32.Parse(line.Substring(READERWRITERCOUNTVERTICES.Length));
                    if (countVertices < 0)
                        throw new MyException.ReaderWriterException.ReaderWriterInvalidFormatException("Invalid vertex count (not a number)");
                    if (countVertices == 0)
                        throw new MyException.ReaderWriterException.ReaderWriterInvalidFormatException("Invalid vertex count (0)");

                    // Ballast
                    line = streamReader.ReadLine();
                    if (line != READERWRITERBALLAST)
                        throw new MyException.ReaderWriterException.ReaderWriterInvalidFormatException("Missing empty line");

                    // Graph
                    line = streamReader.ReadLine();
                    if (line != READERWRITERGRAPH)
                        throw new MyException.ReaderWriterException.ReaderWriterInvalidFormatException("Invalid graph (header)");
                    
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
                            throw new MyException.ReaderWriterException.ReaderWriterInvalidHeaderException("Unknown header");
                    }

                    // Colored graph
                    line = streamReader.ReadLine();
                    if (line != READERWRITERCOLOREDGRAPH)
                        throw new MyException.ReaderWriterException.ReaderWriterInvalidFormatException("Invalid colored graph (header)");

                    while (!streamReader.EndOfStream)
                    {
                        // Number of colors
                        line = streamReader.ReadLine();
                        if (!line.StartsWith(READERWRITERNUMBEROFCOLORS) && !line.StartsWith(READERWRITERCHROMATICNUMBER))
                            throw new MyException.ReaderWriterException.ReaderWriterInvalidFormatException("Invalid number of colors / chromatic number (header)");
                        if (line.StartsWith(READERWRITERNUMBEROFCOLORS))
                            numberColorsString = line.Substring(READERWRITERNUMBEROFCOLORS.Length);
                        else
                            numberColorsString = line.Substring(READERWRITERCHROMATICNUMBER.Length);
                        numberColors = Int32.Parse(numberColorsString);

                        // Used algorithm
                        line = streamReader.ReadLine();
                        if (!line.StartsWith(READERWRITERUSEDALGORITHM))
                            throw new MyException.ReaderWriterException.ReaderWriterInvalidHeaderException("Invalid used algorithm (header)");

                        line = line.Substring(READERWRITERUSEDALGORITHM.Length);

                        if (!Enum.GetNames(typeof(GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum)).Contains(line))
                            throw new MyException.ReaderWriterException.ReaderWriterInvalidFormatException("Unknown algorithm");

                        // Colored graph
                        line = streamReader.ReadLine();
                        while (line != null && line != "")
                        {
                            line = streamReader.ReadLine();
                        }
                    }
                }
                catch (ArgumentException)
                {
                    throw new MyException.ReaderWriterException.ReaderWriterInvalidHeaderException("Invalid header");

                }
                catch (FormatException)
                {
                    throw new MyException.ReaderWriterException.ReaderWriterInvalidFormatException("Invalid vertex count (not a number)");
                    // throw new MyException.ReaderWriterInvalidFormatException("Invalid chromatic number (not a number)");
                }
                catch (IOException)
                {
                    throw new MyException.ReaderWriterException.ReaderWriterInvalidDataException("Something went wrong with a file");
                }
            }

            return graph;
        }

        /// <summary>
        /// Přečte soubor s grafem, který je reprezentován pomocí seznamu sousedů a daný graf vytvoří.
        /// </summary>
        private void ReadFileAdjacencyList(Graph.IGraphEdgeListInterface graph, StreamReader streamReader)
        {
            // Variable
            string line;
            bool isK1 = false; // Vrchol, který nemá žádnou hranu
            string firstVertex = "", secondVertex;

            // Read graph
            while ((line = streamReader.ReadLine()) != "")
            {
                if (!line.StartsWith(LEFTSEPARATORADJACENCYLIST) && !line.EndsWith(RIGHTSEPARATORADJACENCYLIST))
                {
                    if (isK1)
                        graph.AddVertex(firstVertex);

                    firstVertex = line;
                    isK1 = true;
                    continue;
                }

                if (line.StartsWith(LEFTSEPARATORADJACENCYLIST) && line.EndsWith(RIGHTSEPARATORADJACENCYLIST))
                {
                    secondVertex = line.Substring(1, line.Length - 2);
                    graph.AddEdge(firstVertex, secondVertex);
                    isK1 = false;
                    continue;
                }

                throw new MyException.ReaderWriterException.ReaderWriterInvalidDataException("Invalid graph");
            }

            // Last vertex doesn't have any edge
            if (isK1)
                graph.AddVertex(firstVertex);
            
            graph.InitializeGraph();
        }

        /// <summary>
        /// Přečte soubor s grafem, který je reprezentován pomocí matice sousednosti a daný graf vytvoří.
        /// </summary>
        private void ReadFileAdjacencyMatrix(Graph.IGraphAdjacencyMatrixInterface graph, StreamReader streamReader, int countVertices)
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
                            throw new MyException.ReaderWriterException.ReaderWriterInvalidDataException("Invalid graph");
                    }
                }

                if (rowBoolean.Capacity != countVertices)
                    throw new MyException.ReaderWriterException.ReaderWriterInvalidDataException("Invalid graph");

                if (++countRows > countVertices)
                    throw new MyException.ReaderWriterException.ReaderWriterInvalidDataException("Invalid graph");
                
                graph.SetOfNeighborsOfVertex(rowBoolean);
            }

            graph.InitializeGraph();
        }

        /// <summary>
        /// Přečte soubor s grafem, který je reprezentován pomocí seznamu hran a daný graf vytvoří.
        /// </summary>
        private void ReadFileEdgeList(Graph.IGraphEdgeListInterface graph, StreamReader streamReader)
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
                            throw new MyException.ReaderWriterException.ReaderWriterInvalidDataException("Invalid graph");
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
                                throw new MyException.ReaderWriterException.ReaderWriterInvalidDataException("Invalid graph");
                        }
                        // (Vertex name) (Vertex name)
                        else
                        {
                            startVertex1Index = 1;
                            endVertex1Index = line.IndexOf(RIGHTSEPARATOR);
                            startVertex2Index = line.LastIndexOf(LEFTSEPARATOR) + 1;
                            endVertex2Index = line.Length - 1;

                            if (startVertex1Index == startVertex2Index && endVertex1Index == endVertex2Index)
                                throw new MyException.ReaderWriterException.ReaderWriterInvalidDataException("Invalid graph");
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
                    throw new MyException.ReaderWriterException.ReaderWriterInvalidDataException("Invalid graph");

                graph.AddEdge(edge[0], edge[1]);
            }

            graph.FullGenerateVertices();
            
            graph.InitializeGraph();
        }
        #endregion
    }
}