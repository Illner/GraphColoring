using System;
using System.Threading;

namespace AI.GenerateGraphs
{
    abstract class GenerateGraphs
    {
        // Variable
        #region
        protected bool writer;
        protected const int COUNTITERATIONSPROBABILITY = 10;
        
        protected GraphColoring.Graph.IGraphInterface graph;
        protected GraphColoring.GenerateGraph.ErdosRenyiModel.ErdosRenyiModel erdosRenyiModel;
        #endregion

        // Constructor
        #region
        /// <summary>
        /// Initialize GenerateGraphs
        /// </summary>
        /// <param name="writer">write generated graphs on the screen</param>
        public GenerateGraphs(bool writer = true)
        {
            this.writer = writer;
        }
        #endregion

        // Method
        #region
        /// <summary>
        /// Generate graphs with count of vertices greater than or equal to minCount and less than or equal to maxCount
        /// For invalid minCount and maxCount returns GenerateGraphsInvalidArgumentsMinCountMaxCountException
        /// </summary>
        /// <param name="minCount">the lowest count of vertices</param>
        /// <param name="maxCount">the highest count of vertices</param>
        public abstract void Generate(int minCount, int maxCount);

        protected int GetCountIterations(int countVertices)
        {
            switch (countVertices)
            {
                case 1:
                    return 1;
                case 2:
                    return 1;
                case 3:
                    return 2;
                case 4:
                    return 4;
                case 5:
                    return 11;
                case 6:
                    return 34;
                default:
                    return 2 * (int)Math.Pow(countVertices, 1);
            }
        }
        
        /// <summary>
        /// Color a graph with an algorithm
        /// </summary>
        /// <param name="algorithm">algorithm</param>
        /// <param name="probability">probability algorithm?</param>
        /// <returns>(minColors, maxColors), for non-probability algorithm minColors = maxColors</returns>
        protected Tuple<int, int> ColorGraph(GraphColoring.GraphColoringAlgorithm.IGraphColoringAlgorithmInterface algorithm, bool probability = false)
        {
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
    }
}
