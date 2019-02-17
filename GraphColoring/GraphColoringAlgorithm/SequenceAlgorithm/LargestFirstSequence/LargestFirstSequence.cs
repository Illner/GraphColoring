using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence
{
    public sealed class LargestFirstSequence : GraphColoringSequenceAlgorithm, IGraphColoringAlgorithmStepInterface
    {
        // Variable
        #region
        private List<Graph.IVertexInterface> degreeSequenceVertexReverse;
        #endregion

        // Constructor
        #region
        public LargestFirstSequence(Graph.IGraphInterface graph, bool interchange = false) : base(graph)
        {
            name = "Largest first sequence algorithm";

            // Interchange
            this.interchange = interchange;
            if (interchange)
                name = "Largest first sequence interchange algorithm";
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
            degreeSequenceVertexReverse = graph.GetGraphProperty().GetDegreeSequenceVertex(true).ToList();
            degreeSequenceVertexReverse.Reverse();
            vertexSequenceList = degreeSequenceVertexReverse;
        }

        /// <summary>
        /// Vrátí vrchol s největším stupněm, který ještě není obarven
        /// Pokud jsou všechny vrcholy obarveny, tak vrátí null
        /// </summary>
        /// <returns>vrchol</returns>
        public Graph.IVertexInterface Step()
        {
            if (degreeSequenceVertexReverse == null)
            {
                degreeSequenceVertexReverse = graph.GetGraphProperty().GetDegreeSequenceVertex(true);
                degreeSequenceVertexReverse.Reverse();
            }

            foreach (Graph.IVertexInterface vertex in degreeSequenceVertexReverse)
            {
                if (vertex.GetColor() == Graph.VertexExtended.GetDefaultColor())
                    return vertex;
            }

            return null;
        }
        #endregion
    }
}
