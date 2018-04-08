using System;
using System.Collections.Generic;

namespace GraphColoring.Graph
{
    abstract partial class Graph : IGraphInterface
    {
        // Variable
        #region
        /// <summary>
        /// isInitialized - Informace zda je graf inicializován, tj. byly do něj vloženy hrany
        /// realCountVertices - skutečný počet naalokovaných vrcholů, nikoliv předpokládaný počet vrcholů (parametr konstruktoru)
        /// countVertices - počet vrcholů grafu
        /// countEdges - počet hran grafu
        /// adjacencyList - seznam sousedů grafu
        /// mapping - slouží pro snadné nalezení vrcholu na základě identifikátoru
        /// </summary>
        private bool isInitialized;
        private int realCountVertices;
        private int countVertices, countEdges;
        private Dictionary<Vertex, List<Vertex>> adjacencyList;
        private Dictionary<int, Vertex> mapping;
        #endregion

        // Constructor
        #region
        /// <summary>
        /// Inicializuje graf
        /// </summary>
        /// <param name="countVertices">Počet vrcholů grafu</param>
        public Graph(int countVertices)
        {
            SetCountVertices(countVertices);
            adjacencyList = new Dictionary<Vertex, List<Vertex>>();
            mapping = new Dictionary<int, Vertex>();
        }
        #endregion

        // Method
        #region
        /// <summary>
        /// Zvýší proměnnou countEdges o jedna
        /// </summary>
        protected void IncrementCountEdges()
        {
            countEdges++;
        }

        /// <summary>
        /// Přidá do AdjacencyList nový vrchol s prázdným listem hran
        /// Pokud countVertices je menší než realCountVertices, tak vrátí vyjímku GraphInvalidCountVertices
        /// </summary>
        /// <param name="vertex">vrchol, který chceme přidat</param>
        protected void AddVertexToAdjacencyList(Vertex vertex)
        {
            adjacencyList.Add(vertex, new List<Vertex>());
            mapping.Add(vertex.GetIdentifier(), vertex);

            SetRealCountVertices(++realCountVertices);
            if (GetCountVertices() < GetRealCountVertices())
                throw new MyException.GraphInvalidCountVertices();

            SetRealCountVertices(realCountVertices);
        }

        /// <summary>
        /// VLoží hranu mezi vrchol vertex1 a vrchol vertex2
        /// Pokud jeden z vrcholů neexistuje, tak vrátí vyjímku GraphVertexDoesntExist
        /// Pokud hrana již existuje, tak vrátí vyjímku GraphDupliciteEdge
        /// </summary>
        /// <param name="vertex1">1. vrchol</param>
        /// <param name="vertex2">2. vrchol</param>
        protected void AddEdgeToAdjacencyList(Vertex vertex1, Vertex vertex2)
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
                    return;
                    // throw new MyException.GraphDupliciteEdge();

                adjacencyListVertex.Add(vertex2);

                // Swap variables
                vertex = vertex1;
                vertex1 = vertex2;
                vertex2 = vertex;
            }

            SetCountEdges(++countEdges);
        }

        /// <summary>
        /// Vrátí vrchol na základě identifikátoru
        /// </summary>
        /// <param name="identifier">identifikátor vrcholu</param>
        /// <returns>vrchol s daným identifikátorem</returns>
        protected Vertex GetVertex(int identifier)
        {
            // Variable
            Vertex vertex;

            if (!mapping.TryGetValue(identifier, out vertex))
                throw new MyException.GraphVertexDoesntExist();

            return vertex;
        }

        /// <summary>
        /// Inicializuje graf. Pokud už graf byl inicializovaný, tak vrátí vyjímku 
        /// </summary>
        public void InitializeGraph()
        {
            if (isInitialized)
                throw new MyException.GraphAlreadyInitializedException();

            isInitialized = true;
        }
        #endregion

        // Testing
        public void WriteOutGraph()
        {
            Console.WriteLine("Write out graph");
            Console.WriteLine("Count of vertices: " + GetCountVertices());
            Console.WriteLine("Count of real vertices: " + GetRealCountVertices());
            Console.WriteLine("Count of edges: " + GetCountEdges());
            Console.WriteLine("----------");
            Console.WriteLine("Vertices: ");
            Console.WriteLine("----------");
            foreach (Vertex vertex in adjacencyList.Keys)
            {
                Console.WriteLine("Identifier: {0}, userName: {1}",vertex.GetIdentifier(), vertex.GetUserName());
            }

            Console.WriteLine("-------");
            Console.WriteLine("Edges: ");
            Console.WriteLine("-------");
            foreach (KeyValuePair<Vertex, List<Vertex>> record in adjacencyList)
            {
                Console.WriteLine("Vertex: {0} ({1})", record.Key.GetUserName(), record.Key.GetIdentifier());
                foreach (Vertex vertex in record.Value)
                {
                    Console.WriteLine("{0} ({1}) ", vertex.GetUserName(), vertex.GetIdentifier());
                }
            }
        } 

        // Property
        #region
        /// <summary>
        /// Vrátí počet vrcholů grafu
        /// </summary>
        /// <returns>počet vrcholů</returns>
        public int GetCountVertices()
        {
            return countVertices;
        }

        /// <summary>
        /// Nastaví počet vrcholů grafu 
        /// </summary>
        /// <param name="countVertices">počet vrcholů</param>
        private void SetCountVertices(int countVertices)
        {
            this.countVertices = countVertices;
        }

        /// <summary>
        /// Vrátí počet naalokovaných vrcholů grafu
        /// </summary>
        /// <returns>počet naalokovaných vrcholů</returns>
        public int GetRealCountVertices()
        {
            return realCountVertices;
        }

        /// <summary>
        /// Nastaví počet doposuď naalokovaných vrcholů
        /// </summary>
        /// <param name="countVertices">počet vrcholů</param>
        private void SetRealCountVertices(int countVertices)
        {
            this.realCountVertices = countVertices;
        }

        /// <summary>
        /// Vrátí počet hran grafu
        /// Pokud graf nebyl inicializován, tak vrátí vyjímku GraphInitializationException
        /// </summary>
        /// <returns>počet hran</returns>
        public int GetCountEdges()
        {
            if (isInitialized)
                return countEdges;

            throw new MyException.GraphInitializationException();
        }

        /// <summary>
        /// Nastaví počet hran grafu
        /// </summary>
        /// <param name="countEdges">počet hran</param>
        private void SetCountEdges(int countEdges)
        {
            this.countEdges = countEdges;
        }
        #endregion
    }
}
