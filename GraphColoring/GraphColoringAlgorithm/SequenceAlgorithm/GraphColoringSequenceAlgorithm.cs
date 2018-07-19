using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm
{
    abstract class GraphColoringSequenceAlgorithm : GraphColoringAlgorithm
    {
        // Variable
        #region
        private List<Graph.Vertex> vertextexSequenceList;
        #endregion

        // Constructor
        #region
        public GraphColoringSequenceAlgorithm(Graph.Graph graph) : base(graph)
        { }
        #endregion

        // Method
        #region
        /// <summary>
        /// Obarví daný graf
        /// </summary>
        override
        public void Color()
        {
            // TODO Color (GraphColoringSequenceAlgorithm) - R1807
        }

        /// <summary>
        /// Vytvoří posloupnost vrcholů
        /// </summary>
        protected abstract void CreateVertexSequence();
        #endregion
    }
}
