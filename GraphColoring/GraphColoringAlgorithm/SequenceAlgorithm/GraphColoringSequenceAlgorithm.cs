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
        protected List<Graph.IVertexInterface> vertexSequenceList;
        protected bool interchange;
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
                throw new MyException.GraphException.ColoredGraphAlreadyInitializedException();

            CreateVertexSequence();

            if (vertexSequenceList.Count != graph.GetRealCountVertices())
                throw new MyException.GraphColoringAlgorithmException.AlgorithmInvalidVertexSequence();

            // Unique vertices in list
            if (vertexSequenceList.Distinct().Count() != vertexSequenceList.Count())
                throw new MyException.GraphColoringAlgorithmException.AlgorithmInvalidVertexSequence();

            coloredGraph.GreedyColoring(vertexSequenceList, interchange);
            bool isColored = coloredGraph.InicializeColoredGraph();

            if (!isColored)
                throw new MyException.GraphColoringAlgorithmException.AlgorithmGraphIsNotColored();
        }

        /// <summary>
        /// Vytvoří posloupnost vrcholů
        /// </summary>
        protected abstract void CreateVertexSequence();
        #endregion

        // Property
        #region
        public List<Graph.IVertexInterface> GetVertextexSequenceList()
        {
            return vertexSequenceList;
        }
        #endregion
    }
}
