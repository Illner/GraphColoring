using System;
using System.Linq;
using System.Collections.Generic;

namespace GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence
{
    public sealed class LargestFirstSequence : GraphColoringSequenceAlgorithm, IGraphColoringAlgorithmStepInterface
    {
        #region Variable
        private List<Graph.IVertexInterface> degreeSequenceVertexReverse;
        #endregion
        
        #region Constructor
        public LargestFirstSequence(Graph.IGraphInterface graph, GraphColoringAlgorithInterchangeEnum interchangeEnum = GraphColoringAlgorithInterchangeEnum.none) : base(graph)
        {
            // Interchange
            this.interchangeEnum = interchangeEnum;

            switch (interchangeEnum)
            {
                case GraphColoringAlgorithInterchangeEnum.none:
                    name = "Largest first sequence algorithm";
                    timeComplexity = TimeComplexityEnum.quadratic;
                    break;
                case GraphColoringAlgorithInterchangeEnum.interchange:
                    name = "Largest first sequence interchange algorithm";
                    timeComplexity = TimeComplexityEnum.cubic;
                    break;
                case GraphColoringAlgorithInterchangeEnum.interchangeExtended:
                    name = "Largest first sequence interchange extended algorithm";
                    timeComplexity = TimeComplexityEnum.quartic;
                    break;
                case GraphColoringAlgorithInterchangeEnum.interchangeExtendedK3:
                    name = "Largest first sequence interchange extended with K3 algorithm";
                    timeComplexity = TimeComplexityEnum.quintic;
                    break;
            }
        }
        #endregion
        
        #region Method
        /// <summary>
        /// Create a sequence of vertices
        /// Time complexity: O(V) + O(V^2)
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
