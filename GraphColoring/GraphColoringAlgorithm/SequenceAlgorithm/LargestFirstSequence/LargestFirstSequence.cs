using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence
{
    sealed class LargestFirstSequence : GraphColoringSequenceAlgorithm
    {
        // Variable
        #region
        private List<Graph.Vertex> degreeSequenceVertexReverse;
        #endregion

        // Constructor
        #region
        public LargestFirstSequence(Graph.IGraphInterface graph) : base(graph)
        { }
        #endregion

        // Method
        #region
        /// <summary>
        /// Vytvoří posloupnost vrcholů
        /// </summary>
        override
        protected void CreateVertexSequence()
        {
            degreeSequenceVertexReverse = graph.GetGraphProperty().GetDegreeSequenceVertex(true);
            degreeSequenceVertexReverse.Reverse();
            vertexSequenceList = degreeSequenceVertexReverse;
        }
        #endregion
    }
}
