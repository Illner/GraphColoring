using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace GraphColoring.Graph
{
    public abstract partial class Graph : IGraphInterface
    {
        // Variable
        #region
        /// <summary>
        /// name - Jméno daného grafu (implicitně My graph)
        /// isInitialized - informace zda je graf inicializován, tj. byly do něj vloženy hrany
        /// realCountVertices - skutečný počet naalokovaných vrcholů, nikoliv předpokládaný počet vrcholů (parametr konstruktoru)
        /// coloredGraph - obarvení grafu
        /// mapping - slouží pro snadné nalezení vrcholu na základě identifikátoru
        /// mappingUserName - slouží pro snadné nalezení vrcholu na základě userName
        /// graphProperty - vlastnosti grafu
        /// adjacencyList - seznam sousedů grafu
        /// canDeIncreaseCountVertices - určuje, zda se může zavolat metoda IncrementCountVertices / DecrementCountVertices, true - OK, false - vyvolá se výjimka
        /// canDeIncreaseCountEdges - určuje, zda se může zavolat metoda IncrementCountEdges / DecrementCountEdges, true - OK, false - vyvolá se výjimka
        /// </summary>
        private string name;
        private bool isInitialized;
        private int realCountVertices;
        private ColoredGgraph coloredGraph;
        private Dictionary<int, VertexExtended> mapping;
        private Dictionary<string, VertexExtended> mappingUserName;
        private GraphProperty.GraphProperty graphProperty;
        private Dictionary<VertexExtended, HashSet<VertexExtended>> adjacencyList;
        private bool canDeIncreaseCountVertices, canDeIncreaseCountEdges;
        private const string DEFAULTGRAPHNAME = "My graph";
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

            adjacencyList = new Dictionary<VertexExtended, HashSet<VertexExtended>>();
            mapping = new Dictionary<int, VertexExtended>();
            mappingUserName = new Dictionary<string, VertexExtended>();

            SetName(DEFAULTGRAPHNAME);
        }
        #endregion

        // Method
        #region
        /// <summary>
        /// Přidá do AdjacencyList nový vrchol s prázdným listem hran
        /// Pokud countVertices je menší než realCountVertices, tak vrátí vyjímku GraphInvalidCountVertices
        /// </summary>
        /// <param name="vertexExtended">vrchol, který chceme přidat</param>
        protected void AddVertexToAdjacencyList(VertexExtended vertexExtended)
        {
            try
            {
                adjacencyList.Add(vertexExtended, new HashSet<VertexExtended>());
                mapping.Add(vertexExtended.GetIdentifier(), vertexExtended);
                mappingUserName.Add(vertexExtended.GetUserName(), vertexExtended);

                IncrementRealCountVertices();
            }
            catch (ArgumentException)
            {
                throw new MyException.GraphException.GraphVertexAlreadyExistsException();
            }

            if (graphProperty.GetCountVertices() < GetRealCountVertices())
                throw new MyException.GraphException.GraphInvalidCountVerticesException();
        }

        /// <summary>
        /// VLoží hranu mezi vrchol vertex1 a vrchol vertex2
        /// Pokud jeden z vrcholů neexistuje, tak vrátí vyjímku GraphVertexDoesntExist
        /// if vertex1 is equal to vertex2, throws GraphInvalidVertexException
        /// Pokud hrana již existuje, tak vrátí vyjímku GraphDupliciteEdge
        /// </summary>
        /// <param name="vertex1">1. vrchol</param>
        /// <param name="vertex2">2. vrchol</param>
        protected void AddEdgeToAdjacencyList(IEdgeInterface edge)
        {
            // Variable
            IVertexInterface vertex;
            IVertexInterface vertex1 = edge.GetVertex1();
            IVertexInterface vertex2 = edge.GetVertex2();
            
            // If vertex1 is vertex2
            if (vertex1 == vertex2)
                throw new MyException.GraphException.GraphInvalidVertexException();

            // Symmetry
            for (int i = 0; i < 2; i++)
            {
                if (!adjacencyList.TryGetValue(ConvertVertexToVertexExtended(vertex1), out HashSet<VertexExtended> adjacencyHashSetVertexExtended))
                    throw new MyException.GraphException.GraphVertexDoesntExistException();

                if (adjacencyHashSetVertexExtended.Contains(ConvertVertexToVertexExtended(vertex2)))
                    return;
                // throw new MyException.GraphDupliciteEdge();

                adjacencyHashSetVertexExtended.Add(ConvertVertexToVertexExtended(vertex2));

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
        protected VertexExtended GetVertex(int identifier)
        {
            if (!mapping.TryGetValue(identifier, out VertexExtended vertexExtended))
                throw new MyException.GraphException.GraphVertexDoesntExistException();

            return vertexExtended;
        }

        /// <summary>
        /// Vrátí Vertex s daným userName
        /// Pokud daný vrchol neexistuje, vyvolá výjimku GraphVertexDoesntExistException
        /// </summary>
        /// <param name="userName">jméno vrcholu</param>
        /// <returns>vrchol s daným userName</returns>
        public IVertexInterface GetVertexByUserName(string userName)
        {
            if (!mappingUserName.TryGetValue(userName, out VertexExtended vertexExtended))
                throw new MyException.GraphException.GraphVertexDoesntExistException(userName);

            return vertexExtended;
        }

        /// <summary>
        /// Return vertex which has the identifier
        /// If vertex doesn't exist throw GraphVertexDoesntExistException
        /// </summary>
        /// <param name="identifier">Identifier of the vertex</param>
        /// <returns>The vertex with the identifier</returns>
        public IVertexInterface GetVertexByIdentifier(int identifier)
        {
            return GetVertex(identifier);
        }

        /// <summary>
        /// Inicializuje graf. Pokud už graf byl inicializovaný, tak vrátí vyjímku GraphAlreadyInitializedException
        /// </summary>
        public void InitializeGraph()
        {
            if (isInitialized)
                throw new MyException.GraphException.GraphAlreadyInitializedException();

            if (GetRealCountVertices() != graphProperty.GetCountVertices())
                throw new MyException.GraphException.GraphInvalidCountVerticesException("Real count of vertices: " + realCountVertices + ", count of vertices: " + graphProperty.GetCountVertices());

            isInitialized = true;

            realCountVertices = GetGraphProperty().GetCountVertices();
            coloredGraph = new ColoredGgraph(this);
        }


        /// <summary>
        /// Vrátí list sousedů vrcholu vertex
        /// Pokud graf není inicializovaný, tak vrátí vyjímku GraphInitializationException
        /// </summary>
        /// <param name="vertex">vrchol pro který vracíme list sousedů</param>
        /// <returns>list sousedů</returns>
        public List<IVertexInterface> Neighbours(IVertexInterface vertex)
        {
            if (!isInitialized)
                throw new MyException.GraphException.GraphNotInitializationException();
            
            return new List<IVertexInterface> (adjacencyList[ConvertVertexToVertexExtended(vertex)]);
        }

        /// <summary>
        /// Vrátí počet sousedu vrcholu vertex
        /// </summary>
        /// <param name="vertex">vrchol, pro který chceme zjistit počet sousedů</param>
        /// <returns>počet sousedů</returns>
        public int CountNeighbours(IVertexInterface vertex)
        {
            if (!isInitialized)
                throw new MyException.GraphException.GraphNotInitializationException();
            
            return Neighbours(vertex).Count;
        }

        /// <summary>
        /// Vrátí list všech vrcholů v grafu
        /// </summary>
        /// <returns>lsit všech vrcholů v grafu</returns>
        public List<IVertexInterface> AllVertices()
        {
            return new List<IVertexInterface>(adjacencyList.Keys);
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
        public IVertexInterface GetFirstVertex()
        {
            if (adjacencyList.Count == 0)
                throw new MyException.GraphException.GraphDoesntHaveAnyVertices();

            var firstRecord = mapping.First();
            return firstRecord.Value;
        }

        /// <summary>
        /// Doinicializuje zbývající vrcholy (do realCountVertex)
        /// </summary>
        public void FullGenerateVertices()
        {
            if (GetIsInitialized())
                throw new MyException.GraphException.GraphInitializationException();

            while (GetRealCountVertices() != graphProperty.GetCountVertices())
            {
                VertexExtended vertexExtended = new VertexExtended();
                AddVertexToAdjacencyList(vertexExtended);
            }
        }

        /// <summary>
        /// Vrátí true, pokud daný vrchol v grafu existuje, jinak vrátí false
        /// Time complexity: O(1)
        /// </summary>
        /// <param name="vertex">daný vrchol</param>
        /// <returns>true, pokud vrchol existuje, jinak false</returns>
        public bool ExistsVertex(IVertexInterface vertex)
        {
            try
            {
                return adjacencyList.ContainsKey(ConvertVertexToVertexExtended(vertex));
            }
            catch (MyException.GraphException.GraphVertexDoesntExistException)
            {
                return false;
            }
        }

        /// <summary>
        /// Return true, if vertex with userName exist, otherwise return false
        /// </summary>
        /// <param name="userName">The user name</param>
        /// <returns>return true, if vertex exists, otherwise return false</returns>
        public bool ExistsUserName(string userName)
        {
            if (!mappingUserName.TryGetValue(userName, out VertexExtended vertexExtended))
                return false;

            return true;
        }

        /// <summary>
        /// Vrátí true, pokud daná hrana existuje v grafu, jinak vrátí false
        /// Time complexity: O(1)
        /// </summary>
        /// <param name="edge">daná hrana</param>
        /// <returns>true, pokud hrana existuje, jinak false</returns>
        public bool ExistsEdge(IEdgeInterface edge)
        {
            if (!ExistsVertex(edge.GetVertex1()) || !ExistsVertex(edge.GetVertex2()))
                return false;

            adjacencyList.TryGetValue(ConvertVertexToVertexExtended(edge.GetVertex1()), out HashSet<VertexExtended> neighboursList);

            if (neighboursList == null)
                return false;

            return neighboursList.Contains(ConvertVertexToVertexExtended(edge.GetVertex2()));
        }

        /// <summary>
        /// Konvertuje Vertex na VertexExtended
        /// Pokud daný vrchol neexistuje v grafu, tak vrátí výjimku GraphVertexDoesntExistException
        /// </summary>
        /// <param name="vertex">daný Vertex</param>
        /// <returns>daný VertexExtended</returns>
        protected VertexExtended ConvertVertexToVertexExtended(IVertexInterface vertex)
        {
            try
            {
                return mapping[vertex.GetIdentifier()];
            }
            catch (KeyNotFoundException)
            {
                throw new MyException.GraphException.GraphVertexDoesntExistException();
            }
        }

        /// <summary>
        /// Change the vertex user name
        /// If the vertex doesn't exist, throw GraphVertexDoesntExistException
        /// </summary>
        /// <param name="vertex">the name</param>
        /// <param name="newUserName">the user name</param>
        public void RenameVertexUserName(IVertexInterface vertex, string newUserName)
        {
            // Variable
            string oldUserName;
            VertexExtended vertexExtended = ConvertVertexToVertexExtended(vertex);

            if (ExistsUserName(newUserName))
                throw new MyException.GraphException.GraphVertexUserNameAlreadyExistsException();

            oldUserName = vertex.GetUserName();
            mappingUserName.Remove(vertex.GetUserName());
            vertexExtended.SetUserName(newUserName);
            mappingUserName.Add(newUserName, vertexExtended);

            // Change the name in components
            // The graph has more than one component => need to change vertex name in one of them.
            if (GetGraphProperty().GetIsInitializedComponent() && GetGraphProperty().GetCountComponents() > 1)
            {
                foreach (IGraphInterface componentGraph in GetGraphProperty().GetComponents())
                {
                    if (componentGraph.ExistsUserName(oldUserName))
                    {
                        componentGraph.RenameVertexUserName(componentGraph.GetVertexByUserName(oldUserName), newUserName);
                        break;
                    }
                }
            }
        }

        override
        public String ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("Name of graph: " + GetName());
            stringBuilder.AppendLine("Count of vertices: " + graphProperty.GetCountVertices());
            stringBuilder.AppendLine("Real count of vertices: " + GetRealCountVertices());
            stringBuilder.AppendLine("Count of edges: " + graphProperty.GetCountEdges());

            stringBuilder.AppendLine("Vertices: ");
            foreach (VertexExtended vertexExtended in adjacencyList.Keys)
            {
                stringBuilder.AppendLine("-- Identifier: " + vertexExtended.GetIdentifier() + ", userName: " + vertexExtended.GetUserName());
            }

            stringBuilder.AppendLine("Edges: ");
            foreach (KeyValuePair<VertexExtended, HashSet<VertexExtended>> record in adjacencyList)
            {
                stringBuilder.AppendLine("-- Vertex: " + record.Key.GetIdentifier() + " (" + record.Key.GetUserName() + ")");
                foreach (VertexExtended vertexExtended in record.Value)
                {
                    stringBuilder.AppendLine("---- " + vertexExtended.GetIdentifier() + " (" + vertexExtended.GetUserName() + ") ");
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

            throw new MyException.GraphException.GraphNotInitializationException();
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

        /// <summary>
        /// Vrátí referenci na ColoredGraph
        /// Pokud graf není inicializování, vyvolá se vyjímka GraphWasNotInitializedException
        /// </summary>
        /// <returns>referenci na ColoredGraph</returns>
        public IColoredGraphInterface GetColoredGraph()
        {
            if (isInitialized)
                return coloredGraph;

            throw new MyException.GraphException.GraphNotInitializationException();
        }
        #endregion
    }
}
