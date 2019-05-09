using System;
using System.Collections.Generic;

namespace GraphColoring.Graph
{
    class GraphAdjacencyMatrix : Graph, IGraphAdjacencyMatrixInterface
    {
        #region Variable
        /// <summary>
        /// mapping - The map from position in adjacency matrix to identifier
        /// actualVertex - actual row (vertex) in the adjacency matrix
        /// </summary>
        private Dictionary<int, int> mapping;
        private int actualVertex = 0;
        #endregion
        
        #region Constructor
        /// <summary>
        /// Initialize graph
        /// </summary>
        /// <param name="countVertices">Count of vertices = size of an adjacency matrix</param>
        public GraphAdjacencyMatrix(int countVertices) : base(countVertices)
        {
            // Variable
            VertexExtended vertexExtended;

            mapping = new Dictionary<int, int>();

            // Create vertices
            for (int i = 0; i < countVertices; i++)
            {
                vertexExtended = new VertexExtended();
                AddVertexToAdjacencyList(vertexExtended);
                mapping.Add(i, vertexExtended.GetIdentifier());
            }
        }
        #endregion
        
        #region Method
        /// <summary>
        /// Create edges for one row in adjacency matrix
        /// </summary>
        /// <param name="rowAdjacencyMatrix">row in adjacency matrix</param>
        public void SetOfNeighborsOfVertex(List<bool> rowAdjacencyMatrix)
        {
            // Variable
            IVertexInterface vertex1, vertex2;

            mapping.TryGetValue(actualVertex, out int identifierVertex1);
            vertex1 = GetVertex(identifierVertex1);
            
            for (int i = 0; i < rowAdjacencyMatrix.Count; i++)
            {
                if (rowAdjacencyMatrix[i])
                {
                    mapping.TryGetValue(i, out int identifierVertex2);
                    vertex2 = GetVertex(identifierVertex2);

                    AddEdgeToAdjacencyList(new Edge(vertex1, vertex2));
                }
            }

            actualVertex++;
        }
        #endregion
    }
}