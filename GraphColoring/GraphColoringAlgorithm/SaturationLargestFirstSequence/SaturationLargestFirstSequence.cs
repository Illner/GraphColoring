using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring.GraphColoringAlgorithm.SaturationLargestFirstSequence
{
    sealed class SaturationLargestFirstSequence : GraphColoringAlgorithm
    {
        // Constructor
        #region
        public SaturationLargestFirstSequence(Graph.Graph graph) : base(graph)
        { }
        #endregion

        // Method
        #region
        /// <summary>
        /// Obarví daný graf
        /// Pokud graf je již inicializovaný, tak vrátí ColoredGraphAlreadyInitializedException
        /// Něco se nepovedlo - AlgorithmGraphIsNotColored
        /// </summary>
        override
        public void Color()
        {
            if (coloredGraph.GetIsInicializedColoredGraph())
                throw new MyException.ColoredGraphAlreadyInitializedException();

            coloredGraph.ResetColors();

            coloredGraph.SetSaturation(true);

            Graph.Vertex vertex = coloredGraph.GetSaturationDegreeSequence();

            while (vertex != null)
            {
                coloredGraph.ColorVertex(vertex, coloredGraph.GreedyColoring(vertex));
                vertex = coloredGraph.GetSaturationDegreeSequence();
            }

            bool isColored = coloredGraph.InicializeColoredGraph();

            if (!isColored)
                throw new MyException.AlgorithmGraphIsNotColored();
        }
        #endregion
    }
}
