using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring.Graph.GraphProperty
{
    partial class GraphProperty
    {
        // Variable
        #region
        /// <summary>
        /// degreeSequence - skóre grafu
        /// isDegreeSequenceSorted - je setříděné skóre grafu
        /// spanningTree - kostra grafu
        /// matching - maximální párování grafu
        /// cutVertices - artikulace grafu
        /// bridges - mosty grafu
        /// eulerianPath - eulerovský cyklus, nebo eulerovský tah v grafu
        /// perfectEliminationOrderList - perfect elimination ordering (chordal)
        /// </summary>
        private List<KeyValuePair<IVertexInterface, int>> degreeSequence;
        private bool isDegreeSequenceSorted = false;
        private List<IVertexInterface> degreeSequenceVertex;
        private List<int> degreeSequenceInt;
        private List<IEdgeInterface> spanningTreeBFS;
        private List<IEdgeInterface> matching;
        private List<IVertexInterface> cutVertices;
        private List<IEdgeInterface> bridges;
        private int timeBridgesCutVertices = 0;
        private List<IVertexInterface> perfectEliminationOrderingList;
        private List<IVertexInterface> firstPartite;
        private List<IVertexInterface> secondPartite;
        // private List<arc> eulerianPath;
        #endregion

        // Method
        #region
        /// <summary>
        /// Získá skóre grafu
        /// degreeSequenceInt, degreeSequenceVertex
        /// Time complexity: O(V^2) / O(V)
        /// Space complexity: O(V)
        /// </summary>
        /// <param name="sorted">setřídit skóre</param>
        private void DegreeSequence(bool sorted)
        {
            // Variable
            List<IVertexInterface> allVerticesList;
            Dictionary<IVertexInterface, int> degreeSequenceDictionary;

            degreeSequenceDictionary = new Dictionary<IVertexInterface, int>(GetCountVertices());
            allVerticesList = graph.AllVertices();

            foreach (IVertexInterface vertex in allVerticesList)
            {
                degreeSequenceDictionary.Add(vertex, graph.CountNeighbours(vertex));
            }

            if (sorted)
                degreeSequence = (from record in degreeSequenceDictionary orderby record.Value select record).ToList();
            else
                degreeSequence = (from record in degreeSequenceDictionary select record).ToList();

            isDegreeSequenceSorted = sorted;

            degreeSequenceInt = (from record in degreeSequence select record.Value).ToList();
            degreeSequenceVertex = (from record in degreeSequence select record.Key).ToList();
        }

        /// <summary>
        /// Získá kostru grafu pomocí BFS
        /// spanningTreeBFS
        /// BFS
        /// Time complexity: O(V + E)
        /// Space complexity: O(V + E)
        /// </summary>
        private void SpanningTreeBFS()
        {
            // Variable
            Queue<IVertexInterface> vertexBFSQueue = new Queue<IVertexInterface>();
            HashSet<IVertexInterface> visitedVertexHashSet = new HashSet<IVertexInterface>();
            List<IVertexInterface> vertexNeighboursList;
            int countVertex = graph.GetGraphProperty().GetCountVertices();
            IVertexInterface root;

            spanningTreeBFS = new List<IEdgeInterface>();

            root = graph.GetFirstVertex();
            vertexBFSQueue.Enqueue(root);
            visitedVertexHashSet.Add(root);

            while (vertexBFSQueue.Count != 0)
            {
                IVertexInterface vertex = vertexBFSQueue.Dequeue();

                vertexNeighboursList = graph.Neighbours(vertex);

                foreach(IVertexInterface vertexNeighbour in vertexNeighboursList)
                {
                    if (visitedVertexHashSet.Contains(vertexNeighbour))
                        continue;

                    vertexBFSQueue.Enqueue(vertexNeighbour);
                    spanningTreeBFS.Add(new Edge(vertex, vertexNeighbour));
                    visitedVertexHashSet.Add(vertexNeighbour);

                    if (spanningTreeBFS.Count == countVertex - 1)
                        return;
                }
            }
        }

        /// <summary>
        /// Získá maximální párování grafu
        /// matching
        /// </summary>
        private void Matching()
        {
            // TODO Matching - R1809
        }

        /// <summary>
        /// Get all bridges and cut vertices
        /// bridges, cutVertices
        /// Must be connected!
        /// DFS
        /// Time complexity: O(V + E)
        /// Space complexity: O(V)
        /// </summary>
        private void BridgesCutVertices()
        {
            // Variable
            timeBridgesCutVertices = 0;
            List<IVertexInterface> allVertices;
            cutVertices = new List<IVertexInterface>();
            bridges = new List<IEdgeInterface>();
            HashSet<IVertexInterface> visitedVertexHashSet = new HashSet<IVertexInterface>();
            Dictionary<IVertexInterface, int> discoveryTimesDictionary = new Dictionary<IVertexInterface, int>();
            Dictionary<IVertexInterface, int> lowDictionary = new Dictionary<IVertexInterface, int>();
            Dictionary<IVertexInterface, IVertexInterface> parentDictionary = new Dictionary<IVertexInterface, IVertexInterface>();

            if (!graph.GetGraphProperty().GetIsConnected())
                throw new MyException.GraphException.GraphIsNotConnected();

            allVertices = graph.AllVertices();

            foreach(IVertexInterface vertex in allVertices)
            {
                if (!visitedVertexHashSet.Contains(vertex))
                    BridgesCutVerticesRecursion(vertex, visitedVertexHashSet, discoveryTimesDictionary, lowDictionary, parentDictionary);
            }
        }

        private void BridgesCutVerticesRecursion(IVertexInterface vertex, HashSet<IVertexInterface> visitedVertexHashSet, Dictionary<IVertexInterface, int> discoveryTimesDictionary, Dictionary<IVertexInterface, int> lowDictionary, Dictionary<IVertexInterface, IVertexInterface> parentDictionary)
        {
            // Variable
            int children = 0;
            List<IVertexInterface> neighboursList = graph.Neighbours(vertex);

            visitedVertexHashSet.Add(vertex);

            if (!discoveryTimesDictionary.ContainsKey(vertex))
                discoveryTimesDictionary.Add(vertex, 0);

            if (!lowDictionary.ContainsKey(vertex))
                lowDictionary.Add(vertex, 0);

            discoveryTimesDictionary[vertex] = lowDictionary[vertex] = ++timeBridgesCutVertices;

            foreach(IVertexInterface neighbour in neighboursList)
            {
                if (!visitedVertexHashSet.Contains(neighbour))
                {
                    children++;

                    if (!parentDictionary.ContainsKey(neighbour))
                        parentDictionary.Add(neighbour, vertex);
                    else
                        parentDictionary[neighbour] = vertex;

                    BridgesCutVerticesRecursion(neighbour, visitedVertexHashSet, discoveryTimesDictionary, lowDictionary, parentDictionary);

                    if (!lowDictionary.ContainsKey(neighbour))
                        lowDictionary.Add(neighbour, 0);

                    lowDictionary[vertex] = Math.Min(lowDictionary[vertex], lowDictionary[neighbour]);

                    if (!cutVertices.Contains(vertex) && !parentDictionary.ContainsKey(vertex) && children > 1)
                        cutVertices.Add(vertex);

                    if (!cutVertices.Contains(vertex) && parentDictionary.ContainsKey(vertex) && lowDictionary[neighbour] >= discoveryTimesDictionary[vertex])
                        cutVertices.Add(vertex);

                    if (!bridges.Contains(new Edge(vertex, neighbour)) && lowDictionary[neighbour] > discoveryTimesDictionary[vertex])
                        bridges.Add(new Edge(vertex, neighbour));
                }
                else
                {
                    if (!parentDictionary.ContainsKey(vertex) || parentDictionary[vertex] != neighbour)
                        lowDictionary[vertex] = Math.Min(lowDictionary[vertex], discoveryTimesDictionary[neighbour]);
                }
            }
        }
        #endregion

        // Property
        #region

        /// <summary>
        /// Vrátí skóre grafu
        /// </summary>
        /// <param name="sorted">má se setřídit</param>
        /// <returns>skóre grafu jako list Vertexů</returns>
        public List<IVertexInterface> GetDegreeSequenceVertex(bool sorted)
        {
            if (degreeSequence == null)
                DegreeSequence(sorted);

            if (!isDegreeSequenceSorted && sorted)
                DegreeSequence(sorted);

            return degreeSequenceVertex;
        }

        /// <summary>
        /// Vrátí skóre grafu
        /// </summary>
        /// <param name="sorted">má se setřídit</param>
        /// <returns>skóre grafu jako list intů</returns>
        public List<int> GetDegreeSequenceInt(bool sorted)
        {
            if (degreeSequence == null)
                DegreeSequence(sorted);

            if (!isDegreeSequenceSorted && sorted)
                DegreeSequence(sorted);

            return degreeSequenceInt;
        }
        
        /// <summary>
        /// Return degree sequence of the graph
        /// </summary>
        /// <param name="sorted">sorted degree sequence?</param>
        /// <returns>List of tuples (vertex, int - degree)</returns>
        public List<KeyValuePair<IVertexInterface, int>> GetDegreeSequence(bool sorted)
        {
            if (degreeSequence == null)
                DegreeSequence(sorted);

            if (!isDegreeSequenceSorted && sorted)
                DegreeSequence(sorted);

            return degreeSequence;
        }

        /// <summary>
        /// Vrátí kostru grafu
        /// Pokud graf není souvislý, tak vrátí prázdnou kostru
        /// </summary>
        /// <returns>kostru grafu jako list</returns>
        public List<IEdgeInterface> GetSpanningTree()
        {
            if (spanningTreeBFS == null)
            {
                if (GetIsConnected())
                    SpanningTreeBFS();
                else
                    spanningTreeBFS = new List<IEdgeInterface>();
            }

            return spanningTreeBFS;
        }

        /// <summary>
        /// Vrátí párování grafu
        /// </summary>
        /// <returns>párování grafu jako list hran</returns>
        public List<IEdgeInterface> GetMatching()
        {
            if (matching == null)
                Matching();

            return matching;
        }
        
        /// <summary>
        /// Vrátí artikulace grafu
        /// </summary>
        /// <returns>artikulace grafu jako list vrcholů</returns>
        public List<IVertexInterface> GetCutVertices()
        {
            if (cutVertices == null)
                BridgesCutVertices();

            return cutVertices;
        }
        
        /// <summary>
        /// Vrátí mosty grafu
        /// </summary>
        /// <returns>mosty grafu jako list hran</returns>
        public List<IEdgeInterface> GetBridges()
        {
            if (bridges == null)
                BridgesCutVertices();

            return bridges;
        }

        /// <summary>
        /// Return perfect elimination ordering if graph is chordal, otherwise return List with size 0
        /// Simplicial vertex is the last vertex in the sequence!
        /// </summary>
        /// <returns>Perfect elimination ordering if graph is chordal</returns>
        public List<IVertexInterface> GetPerfectEliminationOrdering()
        {
            if (perfectEliminationOrderingList == null)
                IsChordal();

            return perfectEliminationOrderingList;
        }

        /// <summary>
        /// Return the first partite and the second partite
        /// If the graph is not a bipartite then return null
        /// </summary>
        /// <returns>firstPartite, secondPartite</returns>
        public Tuple<List<IVertexInterface>, List<IVertexInterface>> GetPartites()
        {
            if (firstPartite == null || secondPartite == null)
                return null;

            return new Tuple<List<IVertexInterface>, List<IVertexInterface>>(firstPartite, secondPartite);
        }

        /// <summary>
        /// Set the first partite and the second partite
        /// If the graphClass is not set to undefined ot bipartite throws GraphIsNotBipartiteOrUndefined
        /// Only for GetGraphClass(IGraphInterface) or IsBipartiteGraph(IGraphInterface)
        /// firstPartite, secondPartite
        /// </summary>
        /// <param name="firstPartite">first partite</param>
        /// <param name="secondPartite">second partite</param>
        public void SetPartites(List<IVertexInterface> firstPartite, List<IVertexInterface> secondPartite)
        {
            if (graphClass != GraphClass.GraphClass.GraphClassEnum.undefined && graphClass != GraphClass.GraphClass.GraphClassEnum.bipartiteGraph)
                throw new MyException.GraphException.GraphIsNotBipartiteOrUndefined(graphClass.ToString());

            this.firstPartite = firstPartite;
            this.secondPartite = secondPartite;
        }
        #endregion
    }
}
