using System;

namespace GraphColoring.GraphColoringAlgorithm
{
    public abstract partial class GraphColoringAlgorithm : IGraphColoringAlgorithmInterface
    {
        #region Variable
        /// <summary>
        /// name - algorithm name (default: Algorithm)
        /// graph - particular graph which we want to color
        /// coloredGraph - colored graph
        /// </summary>
        protected string name = "Algorithm";
        protected TimeComplexityEnum timeComplexity = TimeComplexityEnum.undefined;
        protected Graph.IGraphInterface graph;
        protected Graph.IColoredGraphInterface coloredGraph;
        private int countVertices;
        protected int countInterchangeCalls;
        #endregion
        
        #region Constructor
        public GraphColoringAlgorithm(Graph.IGraphInterface graph)
        {
            this.graph = graph;
            countInterchangeCalls = 0;
            coloredGraph = graph.GetColoredGraph();
            countVertices = graph.GetRealCountVertices();
        }
        #endregion
        
        #region Method
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

        public double ColoringProgress()
        {
            return (double)coloredGraph.GetColoredVertexList().Count / countVertices;
        }
        #endregion
        
        #region Property
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

        /// <summary>
        /// Return count of interchange calls
        /// </summary>
        /// <returns>count of interchange calls</returns>
        public int GetCountInterchangeCalls()
        {
            return countInterchangeCalls;
        }
        #endregion
    }
}
