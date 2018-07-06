using System;
using System.Linq;
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
        /// mapping - slouží pro snadné nalezení vrcholu na základě identifikátoru
        /// graphProperty - vlastnosti grafu
        /// adjacencyList - seznam sousedů grafu
        /// canDeIncreaseCountVertices - určuje, zda se může zavolat metoda IncrementCountVertices / DecrementCountVertices, true - OK, false - vyvolá se výjimka
        /// canDeIncreaseCountEdges - určuje, zda se může zavolat metoda IncrementCountEdges / DecrementCountEdges, true - OK, false - vyvolá se výjimka
        /// graphClass - Třída grafu - GraphClassEnum
        /// </summary>
        private string name;
        protected char newLine;
        private bool isInitialized;
        private int realCountVertices;
        protected Dictionary<int, Vertex> mapping;
        protected GraphProperty.GraphProperty graphProperty;
        protected Dictionary<Vertex, List<Vertex>> adjacencyList;
        private bool canDeIncreaseCountVertices, canDeIncreaseCountEdges;
        private GraphClass.GraphClass.GraphClassEnum graphClass = GraphClass.GraphClass.GraphClassEnum.undefined;
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
        /// Přidá do AdjacencyList nový vrchol s prázdným listem hran
        /// Pokud countVertices je menší než realCountVertices, tak vrátí vyjímku GraphInvalidCountVertices
        /// </summary>
        /// <param name="vertex">vrchol, který chceme přidat</param>
        protected void AddVertexToAdjacencyList(Vertex vertex)
        {
            adjacencyList.Add(vertex, new List<Vertex>());
            mapping.Add(vertex.GetIdentifier(), vertex);

            IncrementRealCountVertices();

            if (graphProperty.GetCountVertices() < GetRealCountVertices())
                throw new MyException.GraphInvalidCountVerticesException();
        }

        /// <summary>
        /// VLoží hranu mezi vrchol vertex1 a vrchol vertex2
        /// Pokud jeden z vrcholů neexistuje, tak vrátí vyjímku GraphVertexDoesntExist
        /// Pokud hrana již existuje, tak vrátí vyjímku GraphDupliciteEdge
        /// </summary>
        /// <param name="vertex1">1. vrchol</param>
        /// <param name="vertex2">2. vrchol</param>
        protected void AddEdgeToAdjacencyList(Edge edge)
        {
            // Variable
            List<Vertex> adjacencyListVertex;
            Vertex vertex;
            Vertex vertex1 = edge.GetVertex1();
            Vertex vertex2 = edge.GetVertex2();

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

            SetCanDeIncreaseCountEdges(true);
            graphProperty.IncrementCountEdges();
            SetCanDeIncreaseCountEdges(false);
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
        /// Inicializuje graf. Pokud už graf byl inicializovaný, tak vrátí vyjímku GraphAlreadyInitializedException
        /// </summary>
        public void InitializeGraph()
        {
            if (isInitialized)
                throw new MyException.GraphAlreadyInitializedException();

            if (GetRealCountVertices() != graphProperty.GetCountVertices())
                throw new MyException.GraphInvalidCountVerticesException();

            isInitialized = true;

            realCountVertices = GetGraphProperty().GetCountVertices();
        }


        /// <summary>
        /// Vrátí list sousedů vrcholu vertex
        /// Pokud graf není inicializovaný, tak vrátí vyjímku GraphInitializationException
        /// </summary>
        /// <param name="vertex">vrchol pro který vracíme list sousedů</param>
        /// <returns>list sousedů</returns>
        public List<Vertex> Neighbours(Vertex vertex)
        {
            if (!isInitialized)
                throw new MyException.GraphNotInitializationException();

            return adjacencyList[vertex];
        }

        /// <summary>
        /// Vrátí počet sousedu vrcholu vertex
        /// </summary>
        /// <param name="vertex">vrchol, pro který chceme zjistit počet sousedů</param>
        /// <returns>počet sousedů</returns>
        public int CountNeighbours(Vertex vertex)
        {
            if (!isInitialized)
                throw new MyException.GraphNotInitializationException();
            
            return Neighbours(vertex).Count;
        }

        /// <summary>
        /// Vrátí list všech vrcholů v grafu
        /// </summary>
        /// <returns>lsit všech vrcholů v grafu</returns>
        public List<Vertex> AllVertices()
        {
            return new List<Vertex>(adjacencyList.Keys);
        }

        /// <summary>
        /// Inkrementuje počet naalokovaných vrcholů (realCountVertices)
        /// </summary>
        private void IncrementRealCountVertices()
        {
            realCountVertices++;
        }

        /// <summary>
        /// Dekrementuje počet naalokovaných vrcholů (realCountVertices)
        /// </summary>
        private void DecrementRealCountVertices()
        {
            realCountVertices--;
        }

        /// <summary>
        /// Vrátí první vrchol grafu, tj vrchol, který byl jako první inicializovaný
        /// Pokud graf nemá žádný vrchol, vrátí výjimku GraphDoesntHaveAnyVertices
        /// </summary>
        /// <returns>první vrchol</returns>
        public Vertex GetFirstVertex()
        {
            if (adjacencyList.Count == 0)
                throw new MyException.GraphDoesntHaveAnyVertices();

            var firstRecord = mapping.First();
            return firstRecord.Value;
        }

        /// <summary>
        /// Doinicializuje zbývající vrcholy (do realCountVertex)
        /// </summary>
        public void FullGenerateVertices()
        {
            if (GetIsInitialized())
                throw new MyException.GraphInitializationException();

            while (GetRealCountVertices() != graphProperty.GetCountVertices())
            {
                Vertex vertex = new Vertex();
                AddVertexToAdjacencyList(vertex);
            }
        }

        /// <summary>
        /// Vrátí true, pokud daný vrchol v grafu existuje, jinak vrátí false
        /// Time complexity: O(1)
        /// </summary>
        /// <param name="vertex">daný vrchol</param>
        /// <returns>true, pokud vrchol existuje, jinak false</returns>
        public bool ExistsVertex(Vertex vertex)
        {
            return adjacencyList.ContainsKey(vertex);
        }

        /// <summary>
        /// Vrátí true, pokud daná hrana existuje v grafu, jinak vrátí false
        /// Time complexity: O(1)
        /// </summary>
        /// <param name="edge">daná hrana</param>
        /// <returns>true, pokud hrana existuje, jinak false</returns>
        public bool ExistsEdge(Edge edge)
        {
            // Variable
            List<Vertex> neighboursList;

            adjacencyList.TryGetValue(edge.GetVertex1(), out neighboursList);

            if (neighboursList == null)
                return false;

            return neighboursList.Contains(edge.GetVertex2());
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

            throw new MyException.GraphNotInitializationException();
        }

        /// <summary>
        /// Vrátí třídu grafu - GraphClassEnum
        /// </summary>
        /// <returns>třídu grafu</returns>
        public GraphClass.GraphClass.GraphClassEnum GetGraphClass()
        {
            if (graphClass == GraphClass.GraphClass.GraphClassEnum.undefined)
                graphClass = GraphClass.GraphClass.GetGraphClass(this);

            return graphClass;
        }

        /// <summary>
        /// Vrátí informaci zda je graf inicializovaný
        /// </summary>
        /// <returns>true pokud je graf inicializovaný, jinak vrátí false</returns>
        public bool GetIsInitialized()
        {
            return isInitialized;
        }

        /// <summary>
        /// Vráti informaci zda je mozné dekrementovat / inkrementovat pocet vrcholu
        /// Kvuli omezeni volani funkce IncrementCountVertices a DecrementCountVertices v GraphProperty mimo Graph
        /// </summary>
        /// <returns>true pokud je mozno menit hodnotu, jinak vrátí false</returns>
        public bool GetCanDeIncreaseCountVertices()
        {
            return canDeIncreaseCountVertices;
        }

        /// <summary>
        /// Nastaví hodnotu canDeIncreaseCountVertices
        /// </summary>
        /// <param name="value">hodnota, která se má nastavit</param>
        private void SetCanDeIncreaseCountVertices(bool value)
        {
            canDeIncreaseCountVertices = value;
        }

        /// <summary>
        /// Vráti informaci zda je mozné dekrementovat / inkrementovat pocet hran
        /// Kvuli omezeni volani funkce IncrementCountEdges a DecrementCountEdges v GraphProperty mimo Graph
        /// </summary>
        /// <returns>true pokud je mozno menit hodnotu, jinak vrátí false</returns>
        public bool GetCanDeIncreaseCountEdges()
        {
            return canDeIncreaseCountEdges;
        }

        /// <summary>
        /// Nastaví hodnotu canDeIncreaseCountVertices
        /// </summary>
        /// <param name="value">hodnota, která se má nastavit</param>
        private void SetCanDeIncreaseCountEdges(bool value)
        {
            canDeIncreaseCountEdges = value;
        }
        #endregion
    }
}
