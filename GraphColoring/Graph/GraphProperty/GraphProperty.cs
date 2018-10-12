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

        // Method
        #region
        /// <summary>
        /// Resetuje všechny vlastnosti grafu
        /// </summary>
        public void Reset()
        {
            // SequencesPolynomialsOthers
            degreeSequence = null;
            isDegreeSequenceSorted = false;
            spanningTreeBFS = null;
            matching = null;
            cutVertices = null;
            bridges = null;

            // Properties
            isConnected = null;
            isRegular = null;
            isCyclic = null;
            isEulerian = EulerianGraphEnum.undefined;

            // Component
            componentsList = null;

            // IntegralInvariants
            countComponents = null;
            circuitRank = null;
            girth = null;
            vertexConnectivity = null;
            edgeConnectivity = null;
            minimumVertexDegree = null;
            maximumVertexDegree = null;
            averageVertexDegree = null;
            cayleysFormula = null;
        }
        #endregion
    }
}