using System;
using System.Linq;
using System.Collections.Generic;

namespace GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm
{
    public abstract class GraphColoringSequenceAlgorithm : GraphColoringAlgorithm
    {
        #region Variable
        protected List<Graph.IVertexInterface> vertexSequenceList;
        protected GraphColoringAlgorithInterchangeEnum interchangeEnum;
        #endregion
        
        #region Constructor
        public GraphColoringSequenceAlgorithm(Graph.IGraphInterface graph) : base(graph)
        { }
        #endregion
        
        #region Method
        /// <summary>
        /// Color a graph
        /// If colored graph is initialized throws ColoredGraphAlreadyInitializedException
        /// If sequence of vertices is invalid throws AlgorithmInvalidVertexSequence
        /// Something wrong - AlgorithmGraphIsNotColored
        /// </summary>
        override
        public void Color()
        {
            if (coloredGraph.GetIsInitializedColoredGraph())
                throw new MyException.GraphException.ColoredGraphAlreadyInitializedException();
            
            coloredGraph.ResetColors();

            CreateVertexSequence();

            if (vertexSequenceList.Count != graph.GetRealCountVertices())
                throw new MyException.GraphColoringAlgorithmException.AlgorithmInvalidVertexSequence();

            // Unique vertices in list
            if (vertexSequenceList.Distinct().Count() != vertexSequenceList.Count())
                throw new MyException.GraphColoringAlgorithmException.AlgorithmInvalidVertexSequence();

            countInterchangeCalls = coloredGraph.GreedyColoring(vertexSequenceList, interchangeEnum);
            bool isColored = coloredGraph.InitializeColoredGraph();

            if (!isColored)
                throw new MyException.GraphColoringAlgorithmException.AlgorithmGraphIsNotColored();
        }

        /// <summary>
        /// Create a sequence of vertices
        /// </summary>
        protected abstract void CreateVertexSequence();
        #endregion
        
        #region Property
        public List<Graph.IVertexInterface> GetVertextexSequenceList()
        {
            return vertexSequenceList;
        }
        #endregion
    }
}
