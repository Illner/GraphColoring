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
        /// spanningTree - kostra grafu
        /// matching - maximální párování grafu
        /// cutVertices - artikulace grafu
        /// bridges - mosty grafu
        /// eulerianPath - eulerovský cyklus, nebo eulerovský tah v grafu
        /// </summary>
        private List<int> degreeSequence;
        private List<Edge> spanningTreeBFS;
        private List<Edge> matching;
        private List<Vertex> cutVertices;
        private List<Edge> bridges;
        // private List<arc> eulerianPath;
        #endregion

        // Method
        #region
        /// <summary>
        /// Získá skóre grafu
        /// degreeSequence
        /// Time complexity: O(V)
        /// Space complexity: O(V)
        /// </summary>
        private void DegreeSequence()
        {
            // Variable
            List<Vertex> allVerticesList;

            degreeSequence = new List<int>(GetCountVertices());
            allVerticesList = graph.AllVertices();

            foreach (Vertex vertex in allVerticesList)
            {
                degreeSequence.Add(graph.CountNeighbours(vertex));
            }

            degreeSequence.Sort();
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
        /// Získá všechny artikulace grafu
        /// cutVertices
        /// </summary>
        private void CutVertices()
        {
            // TODO CutVertices - R1809
        }

        /// <summary>
        /// Získá všechny mosty grafu
        /// bridges
        /// </summary>
        private void Bridges()
        {
            // TODO Bridges - R1809
        }
        #endregion

        // Property
        #region
        /// <summary>
        /// Vrátí skóre grafu
        /// </summary>
        /// <returns>skóre grafu jako list intů</returns>
        public List<int> GetDegreeSequence()
        {
            if (degreeSequence == null)
                DegreeSequence();

            return degreeSequence;
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
                CutVertices();

            return cutVertices;
        }
        
        /// <summary>
        /// Vrátí mosty grafu
        /// </summary>
        /// <returns>mosty grafu jako list hran</returns>
        public List<Edge> GetBridges()
        {
            if (bridges == null)
                Bridges();

            return bridges;
        }
        #endregion
    }
}
