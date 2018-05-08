using System;

namespace GraphColoring.Graph.GraphProperty
{
    partial class GraphProperty
    {
        // Variable
        #region
        private Graph graph;
        #endregion

        // Constructor
        #region
        public GraphProperty (Graph graph, int countVertices)
        {
            this.graph = graph;
            SetCountVertices(countVertices);
        }
        #endregion
    }
}