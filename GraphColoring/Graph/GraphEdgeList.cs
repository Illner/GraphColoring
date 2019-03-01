using System;
using System.Collections.Generic;

namespace GraphColoring.Graph
{
    class GraphEdgeList : Graph, IGraphEdgeListInterface
    {
        // Variable
        #region
        /// <summary>
        /// mapping - The map from userName to identifier
        /// </summary>
        private Dictionary<string, int> mapping;
        #endregion

        // Constructor
        #region
        /// <summary>
        /// Initialize graph
        /// </summary>
        /// <param name="countVertices">Count of vertices</param>
        public GraphEdgeList(int countVertices) : base(countVertices)
        {
            mapping = new Dictionary<string, int>();
        }
        #endregion

        // Method
        #region
        /// <summary>
        /// Add an edge (userNameVertex1, userNameVertex2) to the graph
        /// </summary>
        /// <param name="userNameVertex1">first vertex</param>
        /// <param name="userNameVertex2">second vertex</param> 
        public void AddEdge(string userNameVertex1, string userNameVertex2)
        {
            // Variable
            int identifierVertex1, identifierVertex2;
            IVertexInterface vertex1, vertex2;

            identifierVertex1 = GetIdentifier(userNameVertex1);
            identifierVertex2 = GetIdentifier(userNameVertex2);

            vertex1 = GetVertex(identifierVertex1);
            vertex2 = GetVertex(identifierVertex2);

            AddEdgeToAdjacencyList(new Edge(vertex1, vertex2));
        }

        /// <summary>
        /// Return an identifier of vertex which user name is userNameVertex
        /// If the vertex with the identifier does not exist construct one
        /// </summary>
        /// <param name="userNameVertex">user name of vertex</param>
        /// <returns>identifier of vertex</returns>
        private int GetIdentifier(string userNameVertex)
        {
            if (mapping.TryGetValue(userNameVertex, out int identifierVertex))
            {
                return identifierVertex;
            }

            VertexExtended vertexExtended = new VertexExtended(userNameVertex);
            AddVertexToAdjacencyList(vertexExtended);
            mapping.Add(userNameVertex, vertexExtended.GetIdentifier());

            return vertexExtended.GetIdentifier();
        }

        /// <summary>
        /// Create a vertex with the user name
        /// This use only for vertices which have zero degree (manual creating)
        /// </summary>
        /// <param name="userName">user name of vertex</param>
        public void AddVertex(string userName)
        {
            GetIdentifier(userName);
        }
        #endregion
    }
}
