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
        /// </summary>
        private List<KeyValuePair<Vertex, int>> degreeSequence;
        private bool isDegreeSequenceSorted = false;
        private List<Vertex> degreeSequenceVertex;
        private List<int> degreeSequenceInt;
        private List<Edge> spanningTreeBFS;
        private List<Edge> matching;
        private List<Vertex> cutVertices;
        private List<Edge> bridges;
        private int timeBridgesCutVertices = 0;
        // private List<arc> eulerianPath;
        #endregion

        // Method
        #region
        /// <summary>
        /// Získá skóre grafu
        /// degreeSequence
        /// Time complexity: O(V^2) / O(V)
        /// Space complexity: O(V)
        /// </summary>
        /// <param name="sorted">setřídit skóre</param>
        private void DegreeSequence(bool sorted)
        {
            // Variable
            List<Vertex> allVerticesList;
            Dictionary<Vertex, int> degreeSequenceDictionary;

            degreeSequenceDictionary = new Dictionary<Vertex, int>(GetCountVertices());
            allVerticesList = graph.AllVertices();

            foreach (Vertex vertex in allVerticesList)
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
            Queue<Vertex> vertexBFSQueue = new Queue<Vertex>();
            HashSet<Vertex> visitedVertexHashSet = new HashSet<Vertex>();
            List<Vertex> vertexNeighboursList;
            int countVertex = graph.GetGraphProperty().GetCountVertices();
            Vertex root;

            spanningTreeBFS = new List<Edge>();

            root = graph.GetFirstVertex();
            vertexBFSQueue.Enqueue(root);
            visitedVertexHashSet.Add(root);

            while (vertexBFSQueue.Count != 0)
            {
                Vertex vertex = vertexBFSQueue.Dequeue();

                vertexNeighboursList = graph.Neighbours(vertex);

                foreach(Vertex vertexNeighbour in vertexNeighboursList)
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
        /// DFS
        /// Time complexity: O(V + E)
        /// Space complexity: O(V)
        /// </summary>
        private void BridgesCutVertices()
        {
            // Variable
            List<Vertex> allVertices;
            HashSet<Vertex> visitedVertexHashSet = new HashSet<Vertex>();
            Dictionary<Vertex, int> discoveryTimesDictionary = new Dictionary<Vertex, int>();
            Dictionary<Vertex, int> lowDictionary = new Dictionary<Vertex, int>();
            Dictionary<Vertex, Vertex> parentDictionary = new Dictionary<Vertex, Vertex>();

            allVertices = graph.AllVertices();

            foreach(Vertex vertex in allVertices)
            {
                if (!visitedVertexHashSet.Contains(vertex))
                    BridgesCutVerticesRecursion(vertex, visitedVertexHashSet, discoveryTimesDictionary, lowDictionary, parentDictionary);
            }
        }

        private void BridgesCutVerticesRecursion(Vertex vertex, HashSet<Vertex> visitedVertexHashSet, Dictionary<Vertex, int> discoveryTimesDictionary, Dictionary<Vertex, int> lowDictionary, Dictionary<Vertex, Vertex> parentDictionary)
        {
            // Variable
            int children = 0;
            cutVertices = new List<Vertex>();
            bridges = new List<Edge>();
            List<Vertex> neighboursList = graph.Neighbours(vertex);

            visitedVertexHashSet.Add(vertex);

            if (!discoveryTimesDictionary.ContainsKey(vertex))
                discoveryTimesDictionary.Add(vertex, 0);

            if (!lowDictionary.ContainsKey(vertex))
                lowDictionary.Add(vertex, 0);

            discoveryTimesDictionary[vertex] = lowDictionary[vertex] = ++timeBridgesCutVertices;

            foreach(Vertex neighbour in neighboursList)
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
                    
                    if (!parentDictionary.ContainsKey(vertex) && children > 1)
                        cutVertices.Add(vertex);

                    if (parentDictionary.ContainsKey(vertex) && lowDictionary[neighbour] >= discoveryTimesDictionary[vertex])
                        cutVertices.Add(vertex);

                    if (lowDictionary[neighbour] > discoveryTimesDictionary[vertex])
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
        public List<Vertex> GetDegreeSequenceVertex(bool sorted)
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
        public List<int> GetDegreeSequence(bool sorted)
        {
            if (degreeSequence == null)
                DegreeSequence(sorted);

            if (!isDegreeSequenceSorted && sorted)
                DegreeSequence(sorted);

            return degreeSequenceInt;
        }

        /// <summary>
        /// Vrátí kostru grafu
        /// Pokud graf není souvislý, tak vrátí prázdnou kostru
        /// </summary>
        /// <returns>kostru grafu jako list</returns>
        public List<Edge> GetSpanningTree()
        {
            if (spanningTreeBFS == null)
            {
                if (GetIsConnected())
                    SpanningTreeBFS();
                else
                    spanningTreeBFS = new List<Edge>();
            }

            return spanningTreeBFS;
        }

        /// <summary>
        /// Vrátí párování grafu
        /// </summary>
        /// <returns>párování grafu jako list hran</returns>
        public List<Edge> GetMatching()
        {
            if (matching == null)
                Matching();

            return matching;
        }
        
        /// <summary>
        /// Vrátí artikulace grafu
        /// </summary>
        /// <returns>artikulace grafu jako list vrcholů</returns>
        public List<Vertex> GetCutVertices()
        {
            if (cutVertices == null)
                BridgesCutVertices();

            return cutVertices;
        }
        
        /// <summary>
        /// Vrátí mosty grafu
        /// </summary>
        /// <returns>mosty grafu jako list hran</returns>
        public List<Edge> GetBridges()
        {
            if (bridges == null)
                BridgesCutVertices();

            return bridges;
        }
        #endregion
    }
}
