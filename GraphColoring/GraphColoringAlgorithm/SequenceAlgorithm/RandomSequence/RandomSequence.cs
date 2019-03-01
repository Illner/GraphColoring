using System;

namespace GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.RandomSequence
{
    public sealed class RandomSequence : GraphColoringSequenceAlgorithm
    {
        // Constructor
        #region
        public RandomSequence(Graph.IGraphInterface graph, bool interchange = false) : base(graph)
        {
            name = "Random sequence algorithm";
            timeComplexity = TimeComplexityEnum.linear;

            // Interchange
            this.interchange = interchange;
            if (interchange)
            {
                name = "Random sequence interchange algorithm";
                timeComplexity = TimeComplexityEnum.multiply;
            }
        }
        #endregion

        // Method
        #region
        /// <summary>
        /// Create a sequence of vertices
        /// Time complexity: O(n)
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
