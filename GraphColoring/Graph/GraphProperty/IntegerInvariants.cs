using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring.Graph.GraphProperty
{
    partial class GraphProperty
    {
        // Variable
        #region
        /// <summary>
        /// order - the number of vertices
        /// size - the number of edges
        /// countComponents - Number of connected components
        /// circuitRank - a linear combination of the numbers of edges, vertices, and components
        /// girth - the length of the shortest cycle
        /// vertexConnectivity - the smallest number of vertices whose removal disconnects the graph
        /// edgeConnectivity - the smallest number of edges whose removal disconnects the graph
        /// number of spanning trees
        /// </summary>
        private int order;
        private int size = 0;
        private int? countComponents;
        private int? circuitRank;
        private int? girth;
        private int? vertexConnectivity;
        private int? edgeConnectivity;
        private int? minimumVertexDegree;
        private int? maximumVertexDegree;
        private double? cayleysFormula;
        #endregion

        // Method
        #region
        /// <summary>
        /// Inkrementuje počet hran o jedna
        /// </summary>
        public void IncrementCountEdges()
        {
            size++;
        }

        /// <summary>
        /// Zjistí circuit rank
        /// circuitRank
        /// </summary>
        private void CircuitRank()
        {
            if (!countComponents.HasValue)
                Components();

            circuitRank = GetCountEdges() - GetCountVertices() + GetCountComponents();
        }

        /// <summary>
        /// Zjistí délku nejkratšího cyklu
        /// girth
        /// </summary>
        private void Girth()
        {
            // TODO girth
        }

        /// <summary>
        /// Zjistí vrcvholovou souvislost
        /// vertexConnectivity
        /// </summary>
        private void VertexConnectivity()
        {
            // TODO vertexConnectivity
        }

        /// <summary>
        /// Zjistí hranovou souvislost
        /// edgeConnectivity
        /// </summary>
        private void EdgeConnectivity()
        {
            // TODO edgeConnectivity
        }

        /// <summary>
        /// Zjistí celkový počet koster
        /// cayleysFormula
        /// </summary>
        private void CayleysFormula()
        {
            cayleysFormula = Math.Pow(GetCountVertices(), (GetCountVertices() - 2));
        }
        #endregion

        // Property
        #region
        /// <summary>
        /// Vrátí počet vrcholů
        /// </summary>
        /// <returns>počet vrcholů</returns>
        public int GetCountVertices()
        {
            return order;
        }

        /// <summary>
        /// Nastaví počet vrcholů
        /// </summary>
        /// <param name="countVertices">počet vrcholů</param>
        private void SetCountVertices(int countVertices)
        {
            order = countVertices;
        }
        
        /// <summary>
        /// Vrátí počet hran
        /// </summary>
        /// <returns>počet hran</returns>
        public int GetCountEdges()
        {
            return size;
        }
        
        /// <summary>
        /// Vrátí počet komponent souvislosti
        /// </summary>
        /// <returns>počet komponent souvislosti</returns>
        public int GetCountComponents()
        {
            if (!countComponents.HasValue)
                Components();

            return (int)countComponents;
        }
        
        /// <summary>
        /// Vrátí circuit rank
        /// </summary>
        /// <returns>circuit rank</returns>
        public int GetCircuitRank()
        {
            if (!circuitRank.HasValue)
                CircuitRank();

            return (int)circuitRank;
        }
        
        /// <summary>
        /// Vrátí girth
        /// </summary>
        /// <returns>girth</returns>
        public int GetGirth()
        {
            if (!girth.HasValue)
                Girth();

            return (int)girth;
        }

        /// <summary>
        /// Vrátí vrcholovou souvislost
        /// </summary>
        /// <returns>vrcholová souvislost</returns>
        public int GetVertexConnectivity()
        {
            if (!vertexConnectivity.HasValue)
                VertexConnectivity();

            return (int)vertexConnectivity;
        }

        /// <summary>
        /// Vrátí hranovou souvislost
        /// </summary>
        /// <returns>hranová souvislost</returns>
        public int GetEdgeConnectivity()
        {
            if (edgeConnectivity.HasValue)
                EdgeConnectivity();

            return (int)edgeConnectivity;
        }

        /// <summary>
        /// Vrátí minimální stupeň vrcholu
        /// </summary>
        /// <returns>minimální stupeň vrcholu</returns>
        public int GetMinimumVertexDegree()
        {
            if (!minimumVertexDegree.HasValue)
            {
                DegreeSequence();
                minimumVertexDegree = degreeSequence.First();
            }

            return (int)minimumVertexDegree;
        }
        
        /// <summary>
        /// Vrátí maximální stupeň vrcholu
        /// </summary>
        /// <returns>maximální stupeň vrcholu</returns>
        public int GetMaximumVertexDegree()
        {
            if (!maximumVertexDegree.HasValue)
            {
                DegreeSequence();
                maximumVertexDegree = degreeSequence.Last();
            }

            return (int)maximumVertexDegree;
        }

        /// <summary>
        /// Vrátí celkový počet koster grafu
        /// </summary>
        /// <returns>celkový počet koster grafu</returns>
        public double GetCayleysFormula()
        {
            if (!cayleysFormula.HasValue)
                CayleysFormula();

            return (double)cayleysFormula;
        }
        #endregion
    }
}
