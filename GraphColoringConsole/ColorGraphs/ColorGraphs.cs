using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoringConsole.ColorGraphs
{
    public class ColorGraphs
    {
        // Variable
        #region
        protected const int COUNTITERATIONSPROBABILITY = 10;
        #endregion

        // Constructor
        #region
        public ColorGraphs() { }
        #endregion

        // Method
        #region
        /// <summary>
        /// Color a graph with an algorithm
        /// </summary>
        /// <param name="algorithm">algorithm</param>
        /// <param name="probability">probability algorithm?</param>
        /// <returns>(minColors, maxColors), for non-probability algorithm minColors = maxColors</returns>
        public Tuple<int, int> ColorGraph(GraphColoring.GraphColoringAlgorithm.IGraphColoringAlgorithmInterface algorithm, bool probability = false)
        {
            GraphColoring.Graph.IGraphInterface graph = algorithm.GetGraph();

            if (graph.GetColoredGraph().GetIsInitializedColoredGraph())
                graph.GetColoredGraph().DeinitializationColoredGraph();

            // Variable
            int countIterations = 1;
            int colors, minColors = int.MaxValue, maxColors = int.MinValue;

            if (probability)
                countIterations = COUNTITERATIONSPROBABILITY;

            for (int i = 0; i < countIterations; i++)
            {
                algorithm.Color();
                colors = graph.GetColoredGraph().GetCountUsedColors();

                if (colors < minColors)
                    minColors = colors;

                if (colors > maxColors)
                    maxColors = colors;

                graph.GetColoredGraph().DeinitializationColoredGraph();
            }

            return new Tuple<int, int>(minColors, maxColors);
        }
        #endregion

        // Property
        #region
        public int GetCountIterationsProbability()
        {
            return COUNTITERATIONSPROBABILITY;
        }
        #endregion
    }
}
