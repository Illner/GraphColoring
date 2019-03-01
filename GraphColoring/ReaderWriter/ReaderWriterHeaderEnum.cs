using System;

namespace GraphColoring.ReaderWriter
{
    abstract partial class ReaderWriter
    {
        // Variable
        /// <summary>
        /// Headers in .graph files
        /// </summary>
        protected const string READERWRITERHEADER = "Graph coloring. Graph representation: ";
        protected const string READERWRITERBALLAST = "";
        protected const string READERWRITERNAME = "Graph name: ";
        protected const string READERWRITERCOUNTVERTICES = "Number of vertices: ";
        protected const string READERWRITERGRAPH = "GRAPH";
        protected const string READERWRITERCOLOREDGRAPH = "COLORED GRAPH";
        protected const string READERWRITERNUMBEROFCOLORS = "Number of colors: ";
        protected const string READERWRITERCHROMATICNUMBER = "Chromatic number: ";
        protected const string READERWRITERUSEDALGORITHM = "Used algorithm: ";


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