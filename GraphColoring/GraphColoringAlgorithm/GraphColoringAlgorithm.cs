using System;

namespace GraphColoring.GraphColoringAlgorithm
{
    public abstract partial class GraphColoringAlgorithm : IGraphColoringAlgorithmInterface
    {
        // Variable
        #region
        /// <summary>
        /// name - algorithm name (default: Algorithm)
        /// graph - particular graph which we want to color
        /// coloredGraph - colored graph
        /// </summary>
        protected string name = "Algorithm";
        protected TimeComplexityEnum timeComplexity = TimeComplexityEnum.undefined;
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
        /// Color a graph
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
        /// Return a graph
        /// </summary>
        /// <returns>graph</returns>
        public Graph.IGraphInterface GetGraph()
        {
            return graph;
        }

        /// <summary>
        /// Return a colored graph
        /// </summary>
        /// <returns>colored graph</returns>
        private Graph.IColoredGraphInterface GetColoredGraph()
        {
            return coloredGraph;
        }

        /// <summary>
        /// Return time complexity of algorithm
        /// </summary>
        /// <returns>time complexity</returns>
        public TimeComplexityEnum GetTimeComplexity()
        {
            return timeComplexity;
        }
        #endregion
    }
}
