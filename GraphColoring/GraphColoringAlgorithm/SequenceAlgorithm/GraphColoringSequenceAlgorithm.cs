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
        protected List<Graph.Vertex> vertextexSequenceList;
        #endregion

        // Constructor
        #region
        public GraphColoringSequenceAlgorithm(Graph.Graph graph) : base(graph)
        { }
        #endregion

        // Method
        #region
        /// <summary>
        /// Obarví daný graf
        /// Pokud nesedí sekvence vrcholů, vrátí AlgorithmInvalidVertexSequence
        /// Něco se nepovedlo - AlgorithmGraphIsNotColored
        /// </summary>
        override
        public void Color()
        {
            CreateVertexSequence();

            if (vertextexSequenceList.Count != graph.GetRealCountVertices())
                throw new MyException.AlgorithmInvalidVertexSequence();

            // Unique vertices in list
            if (vertextexSequenceList.Distinct().Count() != vertextexSequenceList.Count())
                throw new MyException.AlgorithmInvalidVertexSequence();

            coloredGraph.GreedyColoring(vertextexSequenceList);

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
            return vertextexSequenceList;
        }
        #endregion
    }
}
