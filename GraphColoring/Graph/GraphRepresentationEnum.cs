using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
