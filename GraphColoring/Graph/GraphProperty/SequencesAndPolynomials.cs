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
        private List<Edge> spanningTree;
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
        /// Získá kostru grafu
        /// spanningTree
        /// </summary>
        private void SpanningTree()
        {
            // TODO SpanningTree
        }

        /// <summary>
        /// Získá maximální párování grafu
        /// matching
        /// </summary>
        private void Matching()
        {
            // TODO Matching
        }

        /// <summary>
        /// Získá všechny artikulace grafu
        /// cutVertices
        /// </summary>
        private void CutVertices()
        {
            // TODO CutVertices
        }

        /// <summary>
        /// Získá všechny mosty grafu
        /// bridges
        /// </summary>
        private void Bridges()
        {
            // TODO Bridges
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
        /// </summary>
        /// <returns>kostru grafu jako list hran</returns>
        public List<Edge> GetSpanningTree()
        {
            if (spanningTree == null)
                SpanningTree();

            return spanningTree;
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
