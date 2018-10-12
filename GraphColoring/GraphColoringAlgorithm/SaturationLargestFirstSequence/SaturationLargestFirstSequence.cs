using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring.GraphColoringAlgorithm.SaturationLargestFirstSequence
{
    sealed class SaturationLargestFirstSequence : GraphColoringAlgorithm, IGraphColoringAlgorithmStepInterface
    {
        // Variable
        #region
        bool staurationOnStep;
        #endregion

        // Constructor
        #region
        public SaturationLargestFirstSequence(Graph.IGraphInterface graph) : base(graph)
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

        /// <summary>
        /// Vrátí nejnasytěnější vrchol, který není obarvený.
        /// Pokud jsou všechny obarveny, tak vrátí null
        /// </summary>
        /// <returns>vrchol</returns>
        public Graph.Vertex Step()
        {
            if (!staurationOnStep)
            {
                coloredGraph.SetSaturation(true);
                staurationOnStep = true;
            }

            return coloredGraph.GetSaturationDegreeSequence();
        }
        #endregion
    }
}
