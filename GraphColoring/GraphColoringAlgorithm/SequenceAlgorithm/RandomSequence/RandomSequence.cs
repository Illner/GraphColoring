using System;

namespace GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.RandomSequence
{
    public sealed class RandomSequence : GraphColoringSequenceAlgorithm
    {
        #region Constructor
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
                    timeComplexity = TimeComplexityEnum.cubic;
                    break;
                case GraphColoringAlgorithInterchangeEnum.interchangeExtended:
                    name = "Random sequence interchange extended algorithm";
                    timeComplexity = TimeComplexityEnum.quartic;
                    break;
                case GraphColoringAlgorithInterchangeEnum.interchangeExtendedK3:
                    name = "Random sequence interchange extended with K3 algorithm";
                    timeComplexity = TimeComplexityEnum.quintic;
                    break;
            }
        }
        #endregion
        
        #region Method
        /// <summary>
        /// Create a sequence of vertices
        /// Time complexity: O(V)
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
