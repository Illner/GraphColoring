using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring.Graph
{
    abstract partial class Graph
    {
        private enum GraphRepresentationEnum
        {
            adjacencyList,      // Adjacency list
            adjacencyMatrix,    // Adjacency matrix
            edgeList            // Edge list
        }
    }
}
