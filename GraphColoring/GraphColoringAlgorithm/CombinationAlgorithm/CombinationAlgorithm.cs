using System;
using System.Collections.Generic;

namespace GraphColoring.GraphColoringAlgorithm.CombinationAlgorithm
{
    public sealed class CombinationAlgorithm : GraphColoringAlgorithm
    {
        #region Variable
        private SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence largestFirstSequence;
        private SaturationLargestFirstSequence.SaturationLargestFirstSequence saturationLargestFirstSequence;
        private List<IGraphColoringAlgorithmStepInterface> algorithmList;
        #endregion
        
        #region Constructor
        public CombinationAlgorithm(Graph.IGraphInterface graph) : base(graph)
        {
            algorithmList = new List<IGraphColoringAlgorithmStepInterface>();
            name = "Combination algorithm";
            timeComplexity = TimeComplexityEnum.cubic;
        }
        #endregion
        
        #region Method
        /// <summary>
        /// Color a graph
        /// Time complexity: O(V^2) + O(V^3) + O(V + E)
        /// </summary>
        override
        public void Color()
        {
            // Variable
            int helper = 0;
            Graph.IVertexInterface vertex;

            // Initialize
            largestFirstSequence = new SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence(graph);
            saturationLargestFirstSequence = new SaturationLargestFirstSequence.SaturationLargestFirstSequence(graph);

            // Fill algorithmList
            algorithmList.Add(largestFirstSequence);
            algorithmList.Add(saturationLargestFirstSequence);

            // Saturation
            coloredGraph.SetSaturation(true);

            if (coloredGraph.GetIsInitializedColoredGraph())
                throw new MyException.GraphException.ColoredGraphAlreadyInitializedException();

            coloredGraph.ResetColors();

            while (!graph.GetColoredGraph().AreAllVerticesColored())
            {
                vertex = algorithmList[(helper % algorithmList.Count)].Step();
                coloredGraph.ColorVertex(vertex, coloredGraph.GreedyColoring(vertex));
                helper++;
            }

            bool isColored = coloredGraph.InitializeColoredGraph();

            if (!isColored)
                throw new MyException.GraphColoringAlgorithmException.AlgorithmGraphIsNotColored();
        }
        #endregion
    }
}
