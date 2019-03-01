using System;

namespace GraphColoring.Graph
{
    public abstract partial class Graph
    {
        public enum GraphRepresentationEnum
        {
            adjacencyList,      // Adjacency list
            adjacencyMatrix,    // Adjacency matrix
            edgeList            // Edge list
        }
    }
}
