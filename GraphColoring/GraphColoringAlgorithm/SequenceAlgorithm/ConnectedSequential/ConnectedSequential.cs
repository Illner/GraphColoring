using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.ConnectedSequential
{
    class ConnectedSequential : GraphColoringSequenceAlgorithm
    {
        // Variable
        #region

        #endregion

        // Constructor
        #region
        public ConnectedSequential(Graph.Graph graph) : base(graph)
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
            // TODO CreateVertexSequence (ConnectedSequential) - R1807
        }
        #endregion
    }
}
