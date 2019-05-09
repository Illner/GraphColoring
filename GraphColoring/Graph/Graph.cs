using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace GraphColoring.Graph
{
    public abstract partial class Graph : IGraphInterface
    {
        #region Variable
        /// <summary>
        /// name - graph name (default My graph)
        /// realCountVertices - real count of vertices (different from count of vertics in constructor)
        /// coloredGraph - coloredGraph instance
        /// mapping - mapping from identifier to user name
        /// mappingUserName - mapping from user name to identifier
        /// graphProperty - graphProperty instance
        /// adjacencyList - core adjacency list
        /// canDeIncreaseCountVertices - determine if the count of vertices can be changed
        /// canDeIncreaseCountEdges - determine if the count of edges can be cahnged
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
        
        #region Constructor
        public Graph(int countVertices)
        {
            graphProperty = new GraphProperty.GraphProperty(this, countVertices);

            adjacencyList = new Dictionary<VertexExtended, HashSet<VertexExtended>>();
            mapping = new Dictionary<int, VertexExtended>();
            mappingUserName = new Dictionary<string, VertexExtended>();

            SetName(DEFAULTGRAPHNAME);
        }
        #endregion

        #region Method
        /// <summary>
        /// Add a vertex to AdjacencyList (no neighbors)
        /// If countVertices is less than realCountVertices throws GraphInvalidCountVertices
        /// </summary>
        /// <param name="vertexExtended">new vertex</param>
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
        /// Add a new edge
        /// If any vertex does not exist throws GraphVertexDoesntExist
        /// If vertex1 = vertex2 throws GraphInvalidVertexException
        /// If the edge already exists throws GraphDupliciteEdge
        /// </summary>
        /// <param name="vertex1">first vertex</param>
        /// <param name="vertex2">second vertex</param>
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
        /// Return a vertex with the identifier
        /// If the vertex does not exist throws GraphVertexDoesntExistException
        /// </summary>
        /// <param name="identifier">vertex identifier</param>
        /// <returns>vertex with the identifier</returns>
        protected VertexExtended GetVertex(int identifier)
        {
            if (!mapping.TryGetValue(identifier, out VertexExtended vertexExtended))
                throw new MyException.GraphException.GraphVertexDoesntExistException();

            return vertexExtended;
        }

        /// <summary>
        /// Return a vertex with the user name
        /// If the vertex does not exist throws GraphVertexDoesntExistException
        /// </summary>
        /// <param name="userName">vertex user name</param>
        /// <returns>vertex with the user name</returns>
        public IVertexInterface GetVertexByUserName(string userName)
        {
            if (!mappingUserName.TryGetValue(userName, out VertexExtended vertexExtended))
                throw new MyException.GraphException.GraphVertexDoesntExistException(userName);

            return vertexExtended;
        }

        /// <summary>
        /// Return a vertex with the identifier
        /// If the vertex doesn't exist throws GraphVertexDoesntExistException
        /// </summary>
        /// <param name="identifier">vertex identifier</param>
        /// <returns>vertex with the identifier</returns>
        public IVertexInterface GetVertexByIdentifier(int identifier)
        {
            return GetVertex(identifier);
        }

        /// <summary>
        /// Initialize graph
        /// If the graph has been already initialized throws GraphAlreadyInitializedException
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
        /// Return a list of neighbors
        /// If the graph is not initialized throwsGraphInitializationException
        /// </summary>
        /// <param name="vertex">vertex</param>
        /// <returns>list of neighbors</returns>
        public List<IVertexInterface> Neighbours(IVertexInterface vertex)
        {
            if (!isInitialized)
                throw new MyException.GraphException.GraphNotInitializationException();
            
            return new List<IVertexInterface> (adjacencyList[ConvertVertexToVertexExtended(vertex)]);
        }

        /// <summary>
        /// Return count of neighbors
        /// If the graph is not initialized throwsGraphInitializationException
        /// </summary>
        /// <param name="vertex">vertex</param>
        /// <returns>count of neighbors</returns>
        public int CountNeighbours(IVertexInterface vertex)
        {
            if (!isInitialized)
                throw new MyException.GraphException.GraphNotInitializationException();
            
            return Neighbours(vertex).Count;
        }

        /// <summary>
        /// Return a list with all vertices
        /// </summary>
        /// <returns>list with all vertices</returns>
        public List<IVertexInterface> AllVertices()
        {
            return new List<IVertexInterface>(adjacencyList.Keys);
        }

        /// <summary>
        /// Increment number of realCountVertices
        /// </summary>
        private void IncrementRealCountVertices()
        {
            realCountVertices++;
        }

        /// <summary>
        /// Decrement number of realCountVertices
        /// </summary>
        private void DecrementRealCountVertices()
        {
            realCountVertices--;
        }

        /// <summary>
        /// Return some vertex (deterministic - first created vertex)
        /// If the graph has not any vertices throws GraphDoesntHaveAnyVertices
        /// </summary>
        /// <returns>vertex</returns>
        public IVertexInterface GetFirstVertex()
        {
            if (adjacencyList.Count == 0)
                throw new MyException.GraphException.GraphDoesntHaveAnyVertices();

            var firstRecord = mapping.First();
            return firstRecord.Value;
        }

        /// <summary>
        /// Create new vertices (number of vertices: countVertices - realCountVertex)
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
        /// Return true if a vertex exists in the graph, otherwise false
        /// Time complexity: O(1)
        /// </summary>
        /// <param name="vertex">vertex</param>
        /// <returns>true if the vertex exists in the graph, otherwise false</returns>
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
        /// Return true if a vertex (with userName) exists, otherwise false
        /// </summary>
        /// <param name="userName">user name</param>
        /// <returns>return true if the vertex exists, otherwise return false</returns>
        public bool ExistsUserName(string userName)
        {
            if (!mappingUserName.TryGetValue(userName, out VertexExtended vertexExtended))
                return false;

            return true;
        }

        /// <summary>
        /// Return true if an edge exists in the graph, otherwise false
        /// Time complexity: O(1)
        /// </summary>
        /// <param name="edge">edge</param>
        /// <returns>true if the edge exists in the graph, otherwise false</returns>
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
        /// Convertor from Vertex to VertexExtended
        /// If the vertex does not exist in the graph throws GraphVertexDoesntExistException
        /// </summary>
        /// <param name="vertex">vertex</param>
        /// <returns>vertexExtended</returns>
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
        /// Change a vertex user name
        /// If the vertex doesn't exist throws GraphVertexDoesntExistException
        /// </summary>
        /// <param name="vertex">name</param>
        /// <param name="newUserName">user name</param>
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
        
        #region Property
        /// <summary>
        /// Return count of vertices
        /// </summary>
        /// <returns>count of vertices</returns>
        public int GetRealCountVertices()
        {
            return realCountVertices;
        }

        /// <summary>
        /// Return graph name
        /// </summary>
        /// <returns>graph name</returns>
        public string GetName()
        {
            return name;
        }

        /// <summary>
        /// Set graph name
        /// </summary>
        /// <param name="name">new graph name</param>
        public void SetName(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// Return GraphProperty instance
        /// If the graph is not initialized throws GraphWasNotInitializedException
        /// </summary>
        /// <returns>GraphProperty instance</returns>
        public GraphProperty.GraphProperty GetGraphProperty()
        {
            if (isInitialized)
                return graphProperty;

            throw new MyException.GraphException.GraphNotInitializationException();
        }

        /// <summary>
        /// Return true if the graph is initialized, otherwise false
        /// </summary>
        /// <returns>true if the graph is initialized, ottherwise false</returns>
        public bool GetIsInitialized()
        {
            return isInitialized;
        }

        /// <summary>
        /// Return true if count of edges can be changed, otherwise false
        /// </summary>
        /// <returns>true if count of edges can be changed, otherwise false</returns>
        public bool GetCanDeIncreaseCountVertices()
        {
            return canDeIncreaseCountVertices;
        }

        /// <summary>
        /// Set canDeIncreaseCountVertices
        /// </summary>
        /// <param name="value">new value</param>
        private void SetCanDeIncreaseCountVertices(bool value)
        {
            canDeIncreaseCountVertices = value;
        }

        /// <summary>
        /// Return true if count of vertices can be changed, otherwise false
        /// </summary>
        /// <returns>true if count of vertices can be changed, otherwise false</returns>
        public bool GetCanDeIncreaseCountEdges()
        {
            return canDeIncreaseCountEdges;
        }

        /// <summary>
        /// Set canDeIncreaseCountVertices
        /// </summary>
        /// <param name="value">new value</param>
        private void SetCanDeIncreaseCountEdges(bool value)
        {
            canDeIncreaseCountEdges = value;
        }

        /// <summary>
        /// Return ColoredGraph instance
        /// If the graph is not initialized throws GraphWasNotInitializedException
        /// </summary>
        /// <returns>ColoredGraph instance</returns>
        public IColoredGraphInterface GetColoredGraph()
        {
            if (isInitialized)
                return coloredGraph;

            throw new MyException.GraphException.GraphNotInitializationException();
        }
        #endregion
    }
}
