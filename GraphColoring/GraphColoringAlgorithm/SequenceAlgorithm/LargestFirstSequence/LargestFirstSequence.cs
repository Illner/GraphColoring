using System;
using System.Linq;
using System.Collections.Generic;

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
        public LargestFirstSequence(Graph.IGraphInterface graph, GraphColoringAlgorithInterchangeEnum interchangeEnum = GraphColoringAlgorithInterchangeEnum.none) : base(graph)
        {
            // Interchange
            this.interchangeEnum = interchangeEnum;

            switch (interchangeEnum)
            {
                case GraphColoringAlgorithInterchangeEnum.none:
                    name = "Largest first sequence algorithm";
                    timeComplexity = TimeComplexityEnum.linear;
                    break;
                case GraphColoringAlgorithInterchangeEnum.interchange:
                    name = "Largest first sequence interchange algorithm";
                    timeComplexity = TimeComplexityEnum.multiply;
                    break;
                case GraphColoringAlgorithInterchangeEnum.interchangeExtended:
                    name = "Largest first sequence interchange extended algorithm";

                    break;
            }
        }
        #endregion

        // Method
        #region
        /// <summary>
        /// Create a sequence of vertices
        /// Time complexity: O(n) + O(n^2)
        /// </summary>
        override
        protected void CreateVertexSequence()
        {
            degreeSequenceVertexReverse = graph.GetGraphProperty().GetDegreeSequenceVertex(true);
            degreeSequenceVertexReverse.Reverse();
            vertexSequenceList = degreeSequenceVertexReverse;
        }

        /// <summary>
        /// Return a vertex with the largest degree which has not been colored yet
        /// If a graph is colored return null
        /// </summary>
        /// <returns>vertex</returns>
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
