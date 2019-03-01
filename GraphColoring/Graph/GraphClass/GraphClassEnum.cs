using System;

namespace GraphColoring.Graph.GraphClass
{
    public static partial class GraphClass
    {
        public enum GraphClassEnum
        {
            undefined,              // Default value
            completeGraph,          // Kn
            treeGraph,              // Tn
            cycleGraph,             // Cn
            bipartiteGraph,         // Kn,m
            completeBipartiteGraph, // Kn,n
            none
        }
    }
}
