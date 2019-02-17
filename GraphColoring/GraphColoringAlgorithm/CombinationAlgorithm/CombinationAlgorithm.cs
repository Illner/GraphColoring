using System;
using System.Collections.Generic;

namespace GraphColoring.GraphColoringAlgorithm.CombinationAlgorithm
{
    public sealed class CombinationAlgorithm : GraphColoringAlgorithm
    {
        // Variable
        #region
        private SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence largestFirstSequence;
        private SequenceAlgorithm.SmallestLastSequence.SmallestLastSequence smallestLastSequence;
        private SaturationLargestFirstSequence.SaturationLargestFirstSequence saturationLargestFirstSequence;
        private List<IGraphColoringAlgorithmStepInterface> algorithmList;
        #endregion

        // Constructor
        #region
        public CombinationAlgorithm(Graph.IGraphInterface graph) : base(graph)
        {
            algorithmList = new List<IGraphColoringAlgorithmStepInterface>();
            name = "Combination algorithm";
        }
        #endregion

        // Method
        #region
        /// <summary>
        /// Obarví daný graf
        /// </summary>
        override
        public void Color()
        {
            // Variable
            int helper = 0;
            Graph.IVertexInterface vertex;

            // Initialize
            largestFirstSequence = new SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence(graph);
            smallestLastSequence = new SequenceAlgorithm.SmallestLastSequence.SmallestLastSequence(graph);
            saturationLargestFirstSequence = new SaturationLargestFirstSequence.SaturationLargestFirstSequence(graph);

            // Fill algorithmList
            algorithmList.Add(largestFirstSequence);
            algorithmList.Add(smallestLastSequence);
            algorithmList.Add(saturationLargestFirstSequence);

            // Saturation
            coloredGraph.SetSaturation(true);

            while (!graph.GetColoredGraph().AreAllVerticesColored())
            {
                vertex = algorithmList[(helper % algorithmList.Count)].Step();
                coloredGraph.ColorVertex(vertex, coloredGraph.GreedyColoring(vertex));

                helper++;
            }

            coloredGraph.InicializeColoredGraph();
        }
        #endregion
    }
}
