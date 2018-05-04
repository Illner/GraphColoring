using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring.Graph.GraphClass
{
    static partial class GraphClass
    {
        public enum GraphClassEnum
        {
            undefined,      // Default value
            completeGraph,  // Kn
            treeGraph,      // Tn
            cycleGraph,     // Cn
            bipartiteGraph, // Kn,m
            none    
        }
    }
}
