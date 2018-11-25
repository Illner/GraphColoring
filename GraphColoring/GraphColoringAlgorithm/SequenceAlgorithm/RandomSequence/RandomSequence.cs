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
        public RandomSequence(Graph.IGraphInterface graph, bool interchange = false) : base(graph)
        {
            name = "Random sequence algorithm";

            // Interchange
            this.interchange = interchange;
            if (interchange)
                name = "Random sequence interchange algorithm";
        }
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
