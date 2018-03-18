using System;
using System.Collections.Generic;

namespace GraphColoring.Graph
{
    class GraphEdgeList : Graph
    {
        // Variable
        #region
        /// <summary>
        /// mapping - slouží pro snadné nalezení identifikátoru vrcholu na základě userName vrcholu
        /// </summary>
        private Dictionary<string, int> mapping;
        #endregion

        // Constructor
        #region
        /// <summary>
        /// Inicializuje graf
        /// </summary>
        /// <param name="countVertices">Počet vrcholů grafu</param>
        public GraphEdgeList(int countVertices) : base(countVertices)
        {
            mapping = new Dictionary<string, int>();
        }
        #endregion

        // Method
        #region
        /// <summary>
        /// Do grafu vloží příslušnou hranu mezi vrcholem userNameVertex1 a vrcholem userNameVertex2
        /// </summary>
        /// <param name="userNameVertex1">1. vrchol</param>
        /// <param name="userNameVertex2">2. vrchol</param>
        public void AddEdge(string userNameVertex1, string userNameVertex2)
        {
            // Variable
            int identifierVertex1, identifierVertex2;
            Vertex vertex1, vertex2;

            identifierVertex1 = GetIdentifier(userNameVertex1);
            identifierVertex2 = GetIdentifier(userNameVertex2);

            vertex1 = GetVertex(identifierVertex1);
            vertex2 = GetVertex(identifierVertex2);

            AddEdgeToAdjacencyList(vertex1, vertex2);
        }

        /// <summary>
        /// Vrátí identifikátor na zákledě jména, které mu zadal uživatel
        /// Pokud vrchol neexistuje, tak se vytvoří
        /// </summary>
        /// <param name="userNameVertex">Jméno vrcholu, které určil uživatel</param>
        /// <returns></returns>
        private int GetIdentifier(string userNameVertex)
        {
            int identifierVertex;

            if (mapping.TryGetValue(userNameVertex, out identifierVertex))
            {
                return identifierVertex;
            }

            Vertex vertex = new Vertex(userNameVertex);
            AddVertexToAdjacencyList(vertex);
            mapping.Add(userNameVertex, vertex.GetIdentifier());

            return vertex.GetIdentifier();
        }
        #endregion

        // Property
        #region

        #endregion
    }
}
