using System;

namespace GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.RandomSequence
{
    public sealed class RandomSequence : GraphColoringSequenceAlgorithm
    {
        // Constructor
        #region
        public RandomSequence(Graph.IGraphInterface graph, GraphColoringAlgorithInterchangeEnum interchangeEnum = GraphColoringAlgorithInterchangeEnum.none) : base(graph)
        {
            // Interchange
            this.interchangeEnum = interchangeEnum;

            switch (interchangeEnum)
            {
                case GraphColoringAlgorithInterchangeEnum.none:
                    name = "Random sequence algorithm";
                    timeComplexity = TimeComplexityEnum.linear;
                    break;
                case GraphColoringAlgorithInterchangeEnum.interchange:
                    name = "Random sequence interchange algorithm";
                    timeComplexity = TimeComplexityEnum.multiply;
                    break;
                case GraphColoringAlgorithInterchangeEnum.interchangeExtended:
                    name = "Random sequence interchange extended algorithm";
                    break;
                case GraphColoringAlgorithInterchangeEnum.interchangeExtendedK3:
                    name = "Random sequence interchange extended with K3 algorithm";
                    break;
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
