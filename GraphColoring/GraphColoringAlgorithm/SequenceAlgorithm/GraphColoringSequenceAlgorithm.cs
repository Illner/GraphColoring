using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm
{
    abstract class GraphColoringSequenceAlgorithm : GraphColoringAlgorithm
    {
        // Variable
        #region
        protected List<Graph.Vertex> vertexSequenceList;
        #endregion

        // Constructor
        #region
        public GraphColoringSequenceAlgorithm(Graph.IGraphInterface graph) : base(graph)
        { }
        #endregion

        // Method
        #region
        /// <summary>
        /// Obarví daný graf
        /// Pokud graf je již inicializovaný, tak vrátí ColoredGraphAlreadyInitializedException
        /// Pokud nesedí sekvence vrcholů, vrátí AlgorithmInvalidVertexSequence
        /// Něco se nepovedlo - AlgorithmGraphIsNotColored
        /// </summary>
        override
        public void Color()
        {
            if (coloredGraph.GetIsInicializedColoredGraph())
                throw new MyException.ColoredGraphAlreadyInitializedException();

            CreateVertexSequence();

            if (vertexSequenceList.Count != graph.GetRealCountVertices())
                throw new MyException.AlgorithmInvalidVertexSequence();

            // Unique vertices in list
            if (vertexSequenceList.Distinct().Count() != vertexSequenceList.Count())
                throw new MyException.AlgorithmInvalidVertexSequence();

            coloredGraph.GreedyColoring(vertexSequenceList);

            bool isColored = coloredGraph.InicializeColoredGraph();

            if (!isColored)
                throw new MyException.AlgorithmGraphIsNotColored();
        }

        /// <summary>
        /// Vytvoří posloupnost vrcholů
        /// </summary>
        protected abstract void CreateVertexSequence();

        /// <summary>
        /// 
        /// </summary>
        protected void TryChangeColoring()
        {
            // TODO TryChangeColoring - R1807

        }
        #endregion

        // Property
        #region
        public List<Graph.Vertex> GetVertextexSequenceList()
        {
            return vertexSequenceList;
        }
        #endregion
    }
}
