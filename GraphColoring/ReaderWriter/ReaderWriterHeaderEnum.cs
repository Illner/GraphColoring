using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GraphColoring.ReaderWriter
{
    abstract partial class ReaderWriter
    {
        // Variable
        /// <summary>
        /// READERWRITERHEADER - hlavička soborů .graph
        /// </summary>
        protected const string READERWRITERHEADER = "Graph coloring. Graph representation: ";
        protected const string READERWRITERNAME = "Graph name: ";
        protected const string READERWRITERCOUNTVERTICES = "Count of the number of vertices: ";

        protected enum ReaderWriterHeaderEnum
        {
            // Adjacency list
            adjacencyList,   
            // Adjacency matrix
            adjacencyMatrix,   
            // Edge list
            edgeList 
        }

    }
}