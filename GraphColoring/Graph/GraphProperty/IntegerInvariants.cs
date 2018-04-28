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
        /// components - List of connected components
        /// circuitRank - a linear combination of the numbers of edges, vertices, and components
        /// girth - the length of the shortest cycle
        /// vertexConnectivity - the smallest number of vertices whose removal disconnects the graph
        /// edgeConnectivity - the smallest number of edges whose removal disconnects the graph
        /// </summary>
        private long order;
        private long size = 0;
        private long? countComponents;
        private List<Graph> componentsList;
        private long? circuitRank;
        private long? girth;
        private long? vertexConnectivity;
        private long? edgeConnectivity;
        private long? minimumVertexDegree;
        private long? maximumVertexDegree;
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
        /// Rozloží graf na komponenty souvislosti
        /// countComponents, componentsList
        /// </summary>
        private void Components()
        {
            // TODO countComponents / componentsList
        }

        /// <summary>
        /// Zjistí circuit rank
        /// circuitRank
        /// </summary>
        private void CircuitRank()
        {
            // TODO circuit rank
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
        #endregion

        // Property
        #region
        /// <summary>
        /// Vrátí počet vrcholů
        /// </summary>
        /// <returns>počet vrcholů</returns>
        public long GetCountVertices()
        {
            return order;
        }

        /// <summary>
        /// Nastaví počet vrcholů
        /// </summary>
        /// <param name="countVertices">počet vrcholů</param>
        private void SetCountVertices(long countVertices)
        {
            order = countVertices;
        }
        
        /// <summary>
        /// Vrátí počet hran
        /// </summary>
        /// <returns>počet hran</returns>
        public long GetCountEdges()
        {
            return size;
        }
        
        /// <summary>
        /// Vrátí počet komponent souvislosti
        /// </summary>
        /// <returns>počet komponent souvislosti</returns>
        public long GetCountComponents()
        {
            if (!countComponents.HasValue)
                Components();

            return (long)countComponents;
        }
        
        /// <summary>
        /// Vrátí list komponent souvislosti
        /// Lze přepisovat!!!
        /// </summary>
        /// <returns>list komponent souvislosti</returns>
        public List<Graph> GetComponents()
        {
            if (componentsList == null)
                Components();

            return componentsList;
        }
        
        /// <summary>
        /// Vrátí circuit rank
        /// </summary>
        /// <returns>circuit rank</returns>
        public long GetCircuitRank()
        {
            if (!countComponents.HasValue)
                Components();

            CircuitRank();

            return (long)circuitRank;
        }
        
        /// <summary>
        /// Vrátí girth
        /// </summary>
        /// <returns>girth</returns>
        public long GetGirth()
        {
            if (!girth.HasValue)
                Girth();

            return (long)girth;
        }

        /// <summary>
        /// Vrátí vrcholovou souvislost
        /// </summary>
        /// <returns>vrcholová souvislost</returns>
        public long GetVertexConnectivity()
        {
            if (!vertexConnectivity.HasValue)
                VertexConnectivity();

            return (long)vertexConnectivity;
        }

        /// <summary>
        /// Vrátí hranovou souvislost
        /// </summary>
        /// <returns>hranová souvislost</returns>
        public long GetEdgeConnectivity()
        {
            if (edgeConnectivity.HasValue)
                EdgeConnectivity();

            return (long)edgeConnectivity;
        }

        /// <summary>
        /// Vrátí minimální stupeň vrcholu
        /// </summary>
        /// <returns>minimální stupeň vrcholu</returns>
        public long GetMinimumVertexDegree()
        {
            if (!minimumVertexDegree.HasValue)
            {
                DegreeSequence();
                minimumVertexDegree = degreeSequence.Min();
            }

            return (long)minimumVertexDegree;
        }
        
        /// <summary>
        /// Vrátí maximální stupeň vrcholu
        /// </summary>
        /// <returns>maximální stupeň vrcholu</returns>
        public long GetMaximumVertexDegree()
        {
            if (!maximumVertexDegree.HasValue)
            {
                DegreeSequence();
                maximumVertexDegree = degreeSequence.Max();
            }

            return (long)maximumVertexDegree;
        }
        #endregion
    }
}
