using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.RandomSequence
{
    sealed class RandomSequence : GraphColoringSequenceAlgorithm
    {
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
            vertexSequenceList = graph.AllVertices();
            MyMath.MyMath.FisherYatesShuffle(vertexSequenceList);
        }
        #endregion
    }
}
