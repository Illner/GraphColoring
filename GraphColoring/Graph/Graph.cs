using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring.Graph
{
    abstract partial class Graph
    {
        // Variable
        #region
        /// <summary>
        /// isInicialized - Informace zda je graf inicializován, tj. byly do něj vloženy hrany
        /// countVertices - počet vrcholů grafu
        /// countEdges - počet hran grafu
        /// adjacencyList - seznam sousedů grafu
        /// mapping - slouží pro snadné nalezení vrcholu na základě identifikátoru
        /// </summary>
        private bool isInicialized;
        private int countVertices, countEdges;
        private Dictionary<Vertex, List<Vertex>> adjacencyList;
        private Dictionary<int, Vertex> mapping;
        #endregion

        //Constructor
        #region
        /// <summary>
        /// Inicializuje graf
        /// </summary>
        /// <param name="countVertices">Počet vrcholů grafu</param>
        public Graph(int countVertices)
        {
            setCountVertices(countVertices);
            mapping = new Dictionary<int, Vertex>();
        }
        #endregion

        // Method
        #region
        /// <summary>
        /// Zvýší proměnnou countEdges o jedna
        /// </summary>
        protected void incrementCountEdges()
        {
            countEdges++;
        }

        /// <summary>
        /// Přidá do AdjacencyList nový vrchol s prázdným listem hran
        /// </summary>
        /// <param name="vertex">vrchol, který chceme přidat</param>
        protected void addVertexToAdjacencyList(Vertex vertex)
        {
            adjacencyList.Add(vertex, new List<Vertex>());
            mapping.Add(vertex.getIdentifier(), vertex);
        }

        /// <summary>
        /// VLoží hranu mezi vrchol vertex1 a vrchol vertex2
        /// Pokud jeden z vrcholů neexistuje, tak vrátí vyjímku GraphVertexDoesntExist
        /// Pokud hrana již existuje, tak vrátí vyjímku GraphDupliciteEdge
        /// </summary>
        /// <param name="vertex1">1. vrchol</param>
        /// <param name="vertex2">2. vrchol</param>
        protected void addEdgeToAdjacencyList(Vertex vertex1, Vertex vertex2)
        {
            // Variable
            List<Vertex> adjacencyListVertex;
            Vertex vertex;

            // Symmetry
            for (int i = 0; i < 2; i++)
            {
                if (!adjacencyList.TryGetValue(vertex1, out adjacencyListVertex))
                    throw new MyException.GraphVertexDoesntExist();

                if (adjacencyListVertex.Contains(vertex2))
                    throw new MyException.GraphDupliciteEdge();

                adjacencyListVertex.Add(vertex2);

                // Swap variables
                vertex = vertex1;
                vertex1 = vertex2;
                vertex2 = vertex;
            }
        }

        /// <summary>
        /// Vrátí vrchol na základě identifikátoru
        /// </summary>
        /// <param name="identifier">identifikátor vrcholu</param>
        /// <returns>vrchol s daným identifikátorem</returns>
        protected Vertex getVertex(int identifier)
        {
            // Variable
            Vertex vertex;

            if (!mapping.TryGetValue(identifier, out vertex))
                throw new MyException.GraphVertexDoesntExist();

            return vertex;
        }
        #endregion

        // Property
        #region
        /// <summary>
        /// Vrátí počet vrcholů grafu
        /// </summary>
        /// <returns>počet vrcholů</returns>
        public int getCountVertices()
        {
            return countVertices;
        }

        /// <summary>
        /// Nastaví počet vrcholů grafu 
        /// </summary>
        /// <param name="countVertices">počet vrcholů</param>
        private void setCountVertices(int countVertices)
        {
            this.countVertices = countVertices;
        }

        /// <summary>
        /// Vrátí počet hran grafu
        /// Pokud graf nebyl inicializován, tak vrátí vyjímku §§
        /// </summary>
        /// <returns>počet hran</returns>
        public int getCountEdges()
        {
            if (isInicialized)
                return countEdges;

            throw new MyException.GraphInitializationException();
        }

        /// <summary>
        /// Nastaví počet hran grafu
        /// </summary>
        /// <param name="countEdges">počet hran</param>
        private void setCountEdges(int countEdges)
        {
            this.countEdges = countEdges;
        }
        #endregion
    }
}
