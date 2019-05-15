using System;

namespace GraphColoring.GraphColoringAlgorithm.SaturationLargestFirstSequence
{
    public sealed class SaturationLargestFirstSequence : GraphColoringAlgorithm, IGraphColoringAlgorithmStepInterface
    {
        #region Variable
        bool staurationOnStep;
        #endregion
        
        #region Constructor
        public SaturationLargestFirstSequence(Graph.IGraphInterface graph) : base(graph)
        {
            name = "Saturation largest first sequence algorithm";
            timeComplexity = TimeComplexityEnum.cubic;
        }
        #endregion
        
        #region Method
        /// <summary>
        /// Color a graph
        /// If the colored graph is initialized throws ColoredGraphAlreadyInitializedException
        /// Something wrong - AlgorithmGraphIsNotColored
        /// Time complexity: O(V^3) + 0
        /// </summary>
        override
        public void Color()
        {
            if (coloredGraph.GetIsInitializedColoredGraph())
                throw new MyException.GraphException.ColoredGraphAlreadyInitializedException();

            coloredGraph.ResetColors();

            coloredGraph.SetSaturation(true);

            Graph.IVertexInterface vertex = coloredGraph.GetSaturationDegreeSequence();

            while (vertex != null)
            {
                coloredGraph.ColorVertex(vertex, coloredGraph.GreedyColoring(vertex));
                vertex = coloredGraph.GetSaturationDegreeSequence();
            }

            bool isColored = coloredGraph.InitializeColoredGraph();

            if (!isColored)
                throw new MyException.GraphColoringAlgorithmException.AlgorithmGraphIsNotColored();
        }

        /// <summary>
        /// Return the most saturated vertex which is not colored
        /// If a graph is colored return null
        /// </summary>
        /// <returns>vertex</returns>
        public Graph.IVertexInterface Step()
        {
            if (!staurationOnStep)
            {
                coloredGraph.SetSaturation(true);
                staurationOnStep = true;
            }

            return coloredGraph.GetSaturationDegreeSequence();
        }
        #endregion
    }
}
