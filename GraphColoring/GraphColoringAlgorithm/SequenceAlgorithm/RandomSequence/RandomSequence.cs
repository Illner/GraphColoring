using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.RandomSequence
{
    class RandomSequence : GraphColoringSequenceAlgorithm
    {
        // Variable
        #region

        #endregion

        // Constructor
        #region
        public RandomSequence(Graph.Graph graph) : base(graph)
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
            // TODO CreateVertexSequence (RandomSequence) - R1807
        }
        #endregion
    }
}
