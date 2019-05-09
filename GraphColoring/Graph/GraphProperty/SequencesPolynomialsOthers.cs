using System;
using System.Linq;
using System.Collections.Generic;

namespace GraphColoring.Graph.GraphProperty
{
    partial class GraphProperty
    {
        #region Variable
        /// <summary>
        /// degreeSequence - graph score
        /// isDegreeSequenceSorted - is sorted graph score
        /// degreeSequence - skóre grafu
        /// perfectEliminationOrderList - perfect elimination ordering (if graph is chordal)
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
        #endregion

        #region Method
        /// <summary>
        /// Get graph score
        /// Change: degreeSequenceInt, degreeSequenceVertex
        /// Time complexity (unsorted): O(V + E)
        /// Time complexity (sorted): O(V^2)
        /// Space complexity: O(V)
        /// </summary>
        /// <param name="sorted">sorted graph score</param>
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
        /// Get (BFS) spanning tree
        /// Change: spanningTreeBFS
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
        /// Get maximal matching (Edmons algorithm)
        /// Change: matching
        /// Not implemented!
        /// </summary>
        private void Matching()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get all bridges and cut vertices
        /// Change: bridges, cutVertices
        /// Graph must be connected!
        /// Time complexity: O(V + E)
        /// Space complexity: O(V + E)
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

        #region Property
        /// <summary>
        /// Get graph score
        /// </summary>
        /// <param name="sorted">sorted degree sequence</param>
        /// <returns>graph score - only list of vertices</returns>
        public List<IVertexInterface> GetDegreeSequenceVertex(bool sorted)
        {
            if (degreeSequence == null)
                DegreeSequence(sorted);

            if (!isDegreeSequenceSorted && sorted)
                DegreeSequence(sorted);

            return degreeSequenceVertex;
        }

        /// <summary>
        /// Get graph score
        /// </summary>
        /// <param name="sorted">sorted degree sequence</param>
        /// <returns>graph score - only list of degrees (int)</returns>
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
        /// <param name="sorted">sorted degree sequence</param>
        /// <returns>graph score - list of tuples (vertex, int - degree)</returns>
        public List<KeyValuePair<IVertexInterface, int>> GetDegreeSequence(bool sorted)
        {
            if (degreeSequence == null)
                DegreeSequence(sorted);

            if (!isDegreeSequenceSorted && sorted)
                DegreeSequence(sorted);

            return degreeSequence;
        }

        /// <summary>
        /// Get spanning tree
        /// If the graph is not connected returns empty set of edges
        /// </summary>
        /// <returns>spanning tree - list of edges</returns>
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
        /// Get maximal matching
        /// Not implemented!
        /// </summary>
        /// <returns>matching - list of vertices</returns>
        public List<IEdgeInterface> GetMatching()
        {
            if (matching == null)
                Matching();

            return matching;
        }
        
        /// <summary>
        /// Get cut vertices
        /// </summary>
        /// <returns>cut vertices - list of vertices</returns>
        public List<IVertexInterface> GetCutVertices()
        {
            if (cutVertices == null)
                BridgesCutVertices();

            return cutVertices;
        }
        
        /// <summary>
        /// Get bridges
        /// </summary>
        /// <returns>bridges - list of edges</returns>
        public List<IEdgeInterface> GetBridges()
        {
            if (bridges == null)
                BridgesCutVertices();

            return bridges;
        }

        /// <summary>
        /// Return perfect elimination ordering if the graph is chordal, otherwise return List with size 0
        /// Simplicial vertex is the last vertex in the sequence!
        /// </summary>
        /// <returns>Perfect elimination ordering</returns>
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
        /// If the graphClass is not set to undefined or bipartite throws GraphIsNotBipartiteOrUndefined
        /// Only for GetGraphClass(IGraphInterface) or IsBipartiteGraph(IGraphInterface)
        /// Change: firstPartite, secondPartite
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
