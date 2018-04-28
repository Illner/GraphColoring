using System;
using System.Text;
using System.Collections.Generic;

namespace GraphColoring.Graph
{
    abstract partial class Graph : IGraphInterface
    {
        // Variable
        #region
        /// <summary>
        /// name - Jméno daného grafu (implicitně My graph)
        /// isInitialized - informace zda je graf inicializován, tj. byly do něj vloženy hrany
        /// realCountVertices - skutečný počet naalokovaných vrcholů, nikoliv předpokládaný počet vrcholů (parametr konstruktoru)
        /// adjacencyList - seznam sousedů grafu
        /// mapping - slouží pro snadné nalezení vrcholu na základě identifikátoru
        /// graphProperty - vlastnosti grafu
        /// </summary>
        private string name;
        private char newLine;
        private bool isInitialized;
        private int realCountVertices;
        private Dictionary<Vertex, List<Vertex>> adjacencyList;
        private Dictionary<int, Vertex> mapping;
        private GraphProperty.GraphProperty graphProperty;
        #endregion

        // Constructor
        #region
        /// <summary>
        /// Inicializuje graf
        /// </summary>
        /// <param name="countVertices">Počet vrcholů grafu</param>
        public Graph(int countVertices)
        {
            graphProperty = new GraphProperty.GraphProperty(this, countVertices);
            adjacencyList = new Dictionary<Vertex, List<Vertex>>();
            mapping = new Dictionary<int, Vertex>();

            SetName("My graph");
            newLine = ReaderWriter.ReaderWriter.GetNewLine();
        }
        #endregion

        // Method
        #region
        /// <summary>
        /// Zvýší proměnnou countEdges o jedna
        /// </summary>
        protected void IncrementCountEdges()
        {
            graphProperty.IncrementCountEdges();
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
            if (graphProperty.GetCountVertices() < GetRealCountVertices())
                throw new MyException.GraphInvalidCountVerticesException();

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
                    throw new MyException.GraphVertexDoesntExistException();

                if (adjacencyListVertex.Contains(vertex2))
                    return;
                    // throw new MyException.GraphDupliciteEdge();

                adjacencyListVertex.Add(vertex2);

                // Swap variables
                vertex = vertex1;
                vertex1 = vertex2;
                vertex2 = vertex;
            }

            graphProperty.IncrementCountEdges();
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
                throw new MyException.GraphVertexDoesntExistException();

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


        /// <summary>
        /// Vrátí list sousedů vrcholu vertex
        /// </summary>
        /// <param name="vertex">vrchol pro který vracíme list sousedů</param>
        /// <returns>list sousedů</returns>
        public List<Vertex> Neighbours(Vertex vertex)
        {
            return adjacencyList[vertex];
        }

        /// <summary>
        /// Vrátí list všech vrcholů v grafu
        /// </summary>
        /// <returns>lsit všech vrcholů v grafu</returns>
        public List<Vertex> AllVertices()
        {
            return new List<Vertex>(adjacencyList.Keys);
        }

        override
        public String ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("Name of graph: " + GetName() + newLine);
            stringBuilder.Append("Count of vertices: " + graphProperty.GetCountVertices() + newLine);
            stringBuilder.Append("Real count of vertices: " + GetRealCountVertices() + newLine);
            stringBuilder.Append("Count of edges: " + graphProperty.GetCountEdges() + newLine);

            stringBuilder.Append("Vertices: " + newLine);
            foreach (Vertex vertex in adjacencyList.Keys)
            {
                stringBuilder.Append("-- Identifier: " + vertex.GetIdentifier() + ", userName: " + vertex.GetUserName() + newLine);
            }

            stringBuilder.Append("Edges: " + newLine);
            foreach (KeyValuePair<Vertex, List<Vertex>> record in adjacencyList)
            {
                stringBuilder.Append("-- Vertex: " + record.Key.GetUserName() + " (" + record.Key.GetIdentifier() + ")" + newLine);
                foreach (Vertex vertex in record.Value)
                {
                    stringBuilder.Append("---- " + vertex.GetUserName() + " (" + vertex.GetIdentifier() + ") " + newLine);
                }
            }

            return stringBuilder.ToString();
        }
        #endregion

        // Property
        #region
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
        /// Vrátí název grafu
        /// </summary>
        /// <returns>název grafu</returns>
        public string GetName()
        {
            return name;
        }

        /// <summary>
        /// Nastaví název daného grafu
        /// </summary>
        /// <param name="name">název grafu</param>
        public void SetName(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// Vrátí referenci na GraphProperty
        /// Pokud graf není inicializování, vyvolá se vyjímka GraphWasNotInitializedException
        /// </summary>
        /// <returns>referenci na GraphProperty</returns>
        public GraphProperty.GraphProperty GetGraphProperty()
        {
            if (isInitialized)
                return graphProperty;

            throw new MyException.GraphWasNotInitializedException();
        }
        #endregion
    }
}
