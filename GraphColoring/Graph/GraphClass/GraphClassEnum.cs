using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace GraphColoring.Graph.GraphClass
{
    public static partial class GraphClass
    {
        public enum GraphClassEnum
        {
            undefined,              // Default value
            [Description("Complete graph (Kn)")]
            completeGraph,          // Kn
            [Description("Tree graph (Tn)")]
            treeGraph,              // Tn
            [Description("Cycle graph (Cn)")]
            cycleGraph,             // Cn
            [Description("Bipartite graph")]
            bipartiteGraph,         // Kn,m
            [Description("Complete bipartite graph")]
            completeBipartiteGraph, // Kn,n
            [Description("None")]
            none
        }

        public static Dictionary<GraphClassEnum, string> WCMClassGraphDictionary = new Dictionary<GraphClassEnum, string>()
        {
            { GraphClassEnum.completeGraph, "Complete graph (Kn)" },
            { GraphClassEnum.treeGraph, "Tree graph (Tn)" },
            { GraphClassEnum.cycleGraph, "Cycle graph (Cn)" },
            { GraphClassEnum.bipartiteGraph, "Bipartite graph" },
            { GraphClassEnum.completeBipartiteGraph, "Complete bipartite graph" },
            { GraphClassEnum.none, "None" },
        };
    }
}
