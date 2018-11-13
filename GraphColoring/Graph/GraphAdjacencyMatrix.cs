using System;
using System.Collections.Generic;

namespace GraphColoring.Graph
{
    class GraphAdjacencyMatrix : Graph, IGraphAdjacencyMatrixInterface
    {
        // Variable
        #region
        /// <summary>
        /// mapping - slouží pro snadné nalezení identifikátoru vrcholu na základě pořadí vrcholu
        /// actualVertex - slouží jako idendifikátor, na jakém řádku matice jsme a jakému vrcholu nastavujeme sousedy
        /// </summary>
        private new Dictionary<int, int> mapping;
        private int actualVertex = 0;
        #endregion

        // Constructor
        #region
        /// <summary>
        /// Inicializuje graf
        /// </summary>
        /// <param name="countVertices">Počet vrcholů grafu</param>
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

        // Method
        #region
        /// <summary>
        /// Vytvoří hrany pro jeden řádek matice sousednosti
        /// </summary>
        /// <param name="rowAdjacencyMatrix">řádek matice sousednosti</param>
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