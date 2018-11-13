using System;
using System.Collections.Generic;

namespace GraphColoring.GraphColoringAlgorithm
{
    abstract partial class GraphColoringAlgorithm : IGraphColoringAlgorithmInterface
    {
        // Variable
        #region
        /// <summary>
        /// name - algorithm name (default: Algorithm)
        /// graph - daný graf
        /// coloredGraph - pointer na coloredGraf v grafu
        /// </summary>
        protected string name = "Algorithm";
        protected Graph.IGraphInterface graph;
        protected Graph.IColoredGraphInterface coloredGraph;
        #endregion

        // Constructor
        #region
        public GraphColoringAlgorithm(Graph.IGraphInterface graph)
        {
            this.graph = graph;
            coloredGraph = graph.GetColoredGraph();
        }
        #endregion

        // Method
        #region
        /// <summary>
        /// Obarví graf
        /// Exceptions: AlgorithmInvalidVertexSequence, AlgorithmGraphIsNotColored
        /// </summary>
        public abstract void Color();

        override
        public string ToString()
        {
            return name;
        }
        #endregion

        // Properly
        #region
        /// <summary>
        /// vrátí graf
        /// </summary>
        /// <returns>daný graf</returns>
        public Graph.IGraphInterface GetGraph()
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
