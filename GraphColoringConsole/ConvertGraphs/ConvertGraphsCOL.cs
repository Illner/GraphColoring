using System;
using System.IO;
using System.Text;

namespace GraphColoringConsole.ConvertGraphs
{
    class ConvertGraphsCOL
    {
        #region Variable
        private bool writer;
        private static string fileNameExtension = "col";
        private static string pathFolder = @"Data\";
        private GraphColoring.Graph.IGraphEdgeListInterface graph = null;
        #endregion
        
        #region Constructor
        public ConvertGraphsCOL(bool writer = true)
        {
            this.writer = writer;
        }
        #endregion
        
        #region Method
        public int Convert()
        {
            // Variable
            int error = 0;

            if (!Directory.Exists(pathFolder))
            {
                Directory.CreateDirectory(pathFolder);
                return 0;
            }

            foreach (string filePath in Directory.EnumerateFiles(pathFolder, "*." + fileNameExtension))
            {
                if (writer)
                    Console.WriteLine("Reading file: " + filePath);

                try
                {
                    string line = "";
                    string firstEdge = "";
                    string secondEdge = "";
                    string name = "";
                    int countOfVertices = 0, countOfEdges = 0;

                    using (FileStream fileStream = File.OpenRead(filePath))
                    using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8))
                    {
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            string[] splitLine = line.Split(' ');

                            if (splitLine.Length == 1)
                                continue;

                            // Header
                            if (splitLine[0] == "c" && (splitLine[1] == "FILE:" || splitLine[1] == "File:"))
                            {
                                splitLine[2] = splitLine[2].Replace(".col", "");

                                name = splitLine[2];
                            }

                            // Count of vertices and count of edges
                            if (splitLine[0] == "p")
                            {
                                countOfVertices = int.Parse(splitLine[2]);
                                countOfEdges = int.Parse(splitLine[3]);
                                graph = new GraphColoring.Graph.GraphEdgeList(countOfVertices);
                                graph.SetName(name);
                            }

                            // Add edges
                            if (splitLine[0] == "e")
                            {
                                firstEdge = splitLine[1];
                                secondEdge = splitLine[2];

                                graph.AddEdge(firstEdge, secondEdge);
                            }
                        }
                    }

                    graph.InitializeGraph();

                    if ((2 * graph.GetGraphProperty().GetCountEdges() != countOfEdges) && (graph.GetGraphProperty().GetCountEdges() != countOfEdges))
                        throw new GraphColoring.MyException.GraphException.GraphInvalidCountVerticesException();

                    GraphColoring.ReaderWriter.WriterGraph writerGraph = new GraphColoring.ReaderWriter.WriterGraph(pathFolder + name + ".graph", false);
                    writerGraph.WriteFile(graph);

                    if (writer)
                        Console.WriteLine("Graph added: " + name + ".graph");
                }
                catch (GraphColoring.MyException.GraphException.GraphInvalidCountVerticesException)
                {
                    Console.WriteLine("Error (" + filePath + "): Invalid count of vertices!");
                    error = 1;
                }
                catch (IOException ex)
                {
                    Console.WriteLine("Something wrong (" + filePath + "): " + ex.GetType() + "!");
                    error = 1;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Something wrong (" + filePath + "): " + ex.GetType() + "!");
                    error = 1;
                }
            }

            return error;
        }
        #endregion
        
        #region Property
        /// <summary>
        /// Return a path folder
        /// </summary>
        /// <returns>path folder</returns>
        public static string GetPathFolder()
        {
            return pathFolder;
        }

        /// <summary>
        /// Return a file name extension
        /// </summary>
        /// <returns>file name extension</returns>
        public static string GetFileNameExtension()
        {
            return fileNameExtension;
        }
        #endregion
    }
}
