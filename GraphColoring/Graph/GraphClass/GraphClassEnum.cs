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
            completeGraph,  // Kn
            pathGraph,      // Pn
            cycleGraph,     // Cn
            treeGraph,      // Tn
            kPartiteGraph,  // Kn1,n2, ... ,nn
            none
        }
    }
}
