using System;
using System.Collections.Generic;

namespace GraphColoring.Graph
{
    class GraphAdjacencyMatrix : Graph
    {
        // Variable
        #region
        /// <summary>
        /// mapping - slouží pro snadné nalezení identifikátoru vrcholu na základě pořadí vrcholu
        /// actualVertex - slouží jako idendifikátor, na jakém řádku matice jsme a jakému vrcholu nastavujeme sousedy
        /// </summary>
        private Dictionary<int, int> mapping;
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
            Vertex vertex;

            mapping = new Dictionary<int, int>();

            // Create vertices
            for (int i = 0; i < countVertices; i++)
            {
                vertex = new Vertex();
                addVertexToAdjacencyList(vertex);
                mapping.Add(i, vertex.getIdentifier());
            }
        }
        #endregion

        // Method
        #region
        /// <summary>
        /// Vytvoří hrany pro jeden řádek matice sousednosti
        /// </summary>
        /// <param name="rowAdjacencyMatrix">řádek matice sousednosti</param>
        public void setOfNeighborsOfVertex(List<bool> rowAdjacencyMatrix)
        {
            // Variable
            Vertex vertex1, vertex2;
            int identifierVertex1, identifierVertex2;

            mapping.TryGetValue(actualVertex, out identifierVertex1);
            vertex1 = getVertex(identifierVertex1);
            
            for (int i = 0; i < rowAdjacencyMatrix.Count; i++)
            {
                if (rowAdjacencyMatrix[i])
                {
                    mapping.TryGetValue(i, out identifierVertex2);
                    vertex2 = getVertex(identifierVertex2);

                    addEdgeToAdjacencyList(vertex1, vertex2);
                }
            }

            actualVertex++;
        }
        #endregion

        // Property
        #region

        #endregion
    }
}