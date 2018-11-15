﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GraphColoring.ReaderWriter
{
    class WriterGraph : ReaderWriter, IWriterGraphInterface
    {
        // Variable
        #region

        #endregion

        // Constructor
        #region
        public WriterGraph(string path) : base(path) { }
        public WriterGraph(string path, bool checkPath) : base(path, checkPath) { }
        #endregion

        // Method
        #region
        /// <summary>
        /// Do daného souboru zapíše obarvený graf
        /// Pokud coloredGraph není inicializovaný, tak vrátí výjimku ColoredGraphNotInitializationException
        /// </summary>
        /// <param name="graph">Obarvený graf</param>
        /// <param name="graphColoringAlgorithm">Algoritmus, který byl využit při obarvení grafu</param>
        public bool WriteFile(Graph.IGraphInterface graph, GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum graphColoringAlgorithm, bool isOptimal)
        {
            // Variable
            Graph.IColoredGraphInterface coloredGraph;

            coloredGraph = graph.GetColoredGraph();

            if (!coloredGraph.GetIsInicializedColoredGraph())
                throw new MyException.GraphException.ColoredGraphNotInitializationException();

            if (CheckIfRecordExists(graphColoringAlgorithm))
                return false;

            using (StreamWriter streamWriter = File.AppendText(GetPath()))
            {
                streamWriter.WriteLine();

                // Number of colors
                if (isOptimal)
                    streamWriter.WriteLine(READERWRITERCHROMATICNUMBER + coloredGraph.GetCountUsedColors());
                else
                    streamWriter.WriteLine(READERWRITERNUMBEROFCOLORS + coloredGraph.GetCountUsedColors());

                // Used algorithm
                streamWriter.WriteLine(READERWRITERUSEDALGORITHM + graphColoringAlgorithm);

                // Colored graph
                List<int> colorList = coloredGraph.UsedColors();

                foreach(int color in colorList)
                {
                    List<Graph.IVertexInterface> vertexList = coloredGraph.ColoredVertices(color);

                    streamWriter.WriteLine("- " + color);
                    vertexList.ForEach(vertex => { streamWriter.WriteLine("-- " + vertex.GetUserName() + " (" + vertex.GetIdentifier() + ") "); });
                }
            }

            return true;
        }

        private bool CheckIfRecordExists(GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum graphColoringAlgorithm)
        {
            // Variable
            string line;
            
            if (graphColoringAlgorithm == GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequence)
                return false;
                
            using (FileStream fileStream = File.OpenRead(GetPath()))
            using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                while (!streamReader.EndOfStream)
                {
                    line = streamReader.ReadLine();
                    
                    while (!line.StartsWith(READERWRITERUSEDALGORITHM))
                    {
                        // No colored
                        if (streamReader.EndOfStream)
                            return false;

                        line = streamReader.ReadLine();
                    }

                    line = line.Substring(READERWRITERUSEDALGORITHM.Length);

                    if (line == graphColoringAlgorithm.ToString())
                        return true;
                }
            }
            
            return false;
        }
        #endregion
    }
}