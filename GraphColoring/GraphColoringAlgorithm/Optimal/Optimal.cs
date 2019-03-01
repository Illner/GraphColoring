using System;
using System.Linq;
using System.Collections.Generic;

namespace GraphColoring.GraphColoringAlgorithm.Optimal
{
    public sealed class Optimal : GraphColoringAlgorithm
    {
        // Variable
        #region
        private List<Graph.IVertexInterface> optimalVertexList;
        private int optimalCountColors = int.MaxValue;
        #endregion

        // Constructor
        #region
        public Optimal(Graph.IGraphInterface graph) : base(graph)
        {
            name = "Optimal algorithm";
            timeComplexity = TimeComplexityEnum.factorial;
        }
        #endregion

        // Method
        #region
        /// <summary>
        /// Color a graph
        /// Time complexity: O(n! + m)
        /// </summary>
        override
        public void Color()
        {
            // Variable
            int countUsedColors;
            Graph.IColoredGraphInterface coloredGraph = graph.GetColoredGraph();
            optimalVertexList = new List<Graph.IVertexInterface>();
            
            coloredGraph.ResetColors();

            // If the graph is chordal => use PEO for coloring
            if (graph.GetGraphProperty().GetIsChordal())
            {
                optimalVertexList = graph.GetGraphProperty().GetPerfectEliminationOrdering();
            }
            // The graph is not chordal => try all permutation of vertices
            else
            {
                foreach (var vertexList in MyMath.MyMath.GeneratePermutations(graph.AllVertices()))
                {
                    coloredGraph.GreedyColoring(vertexList.ToList());
                    coloredGraph.InitializeColoredGraph();
                    countUsedColors = coloredGraph.GetCountUsedColors();

                    if (optimalCountColors > countUsedColors)
                    {
                        optimalCountColors = countUsedColors;
                        optimalVertexList = vertexList.ToList();
                    }

                    coloredGraph.DeinitializationColoredGraph();
                }
            }

            coloredGraph.GreedyColoring(optimalVertexList);
            bool isColored = coloredGraph.InitializeColoredGraph();

            if (!isColored)
                throw new MyException.GraphColoringAlgorithmException.AlgorithmGraphIsNotColored();
        }
        #endregion
    }
}
