﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        }
        #endregion

        // Method
        #region
        /// <summary>
        /// Obarví graf
        /// </summary>
        override
        public void Color()
        {
            // Variable
            int countUsedColors;
            Graph.IColoredGraphInterface coloredGraph = graph.GetColoredGraph();
            optimalVertexList = new List<Graph.IVertexInterface>();

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
                    coloredGraph.InicializeColoredGraph();
                    countUsedColors = coloredGraph.GetCountUsedColors();

                    if (optimalCountColors > countUsedColors)
                    {
                        optimalCountColors = countUsedColors;
                        optimalVertexList = vertexList.ToList();
                    }

                    coloredGraph.DeinicializationColoredGraph();
                }
            }

            coloredGraph.GreedyColoring(optimalVertexList);
            coloredGraph.InicializeColoredGraph();
        }
        #endregion
    }
}
