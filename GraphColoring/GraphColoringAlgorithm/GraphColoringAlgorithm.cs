using System;
using System.Collections.Generic;

namespace GraphColoring.GraphColoringAlgorithm
{
    abstract partial class GraphColoringAlgorithm : IGraphColoringAlgorithmInterface
    {
        // Variable
        #region
        /// <summary>
        /// graph - daný graf
        /// coloredGraph - pointer na coloredGraf v grafu
        /// </summary>
        private Graph.Graph graph;
        private Graph.IColoredGraphInterface coloredGraph;
        #endregion

        // Constructor
        #region
        public GraphColoringAlgorithm(Graph.Graph graph)
        {
            this.graph = graph;
            coloredGraph = graph.GetColoredGraph();
        }
        #endregion

        // Method
        #region
        // Obarví daný graf
        public abstract void Color();
        #endregion

        // Properly
        #region
        /// <summary>
        /// vrátí graf
        /// </summary>
        /// <returns>daný graf</returns>
        public Graph.Graph GetGraph()
        {
            return graph;
        }

        /// <summary>
        /// Vrátí coloredGraph z daného grafu
        /// </summary>
        /// <returns>coloredGraph</returns>
        private Graph.IColoredGraphInterface GetColoredGraph()
        {
            return coloredGraph;
        }
        #endregion
    }
}
