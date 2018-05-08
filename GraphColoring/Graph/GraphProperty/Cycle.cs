using System;
using System.Collections.Generic;
using System.IO;
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
        /// cycleDisjointSetDictionary (CycleIsCyclic) - struktura pro získání reprezentanta vrcholu
        /// </summary>
        private Dictionary<Vertex, Vertex> cycleDisjointSetDictionary;
        #endregion

        /// <summary>
        /// Používá CycleGirth
        /// </summary>
        class CycleNode
        #region
        {
            // Variable
            private int depth;
            private Vertex vertex;

            // Constructor
            public CycleNode(Vertex vertex, int depth)
            {
                this.vertex = vertex;
                this.depth = depth;
            }

            // Property
            /// <summary>
            /// Vrátí hloubku daného uzlu
            /// </summary>
            /// <returns></returns>
            public int GetDepth()
            {
                return depth;
            }

            /// <summary>
            /// Vrátí vrchol
            /// </summary>
            /// <returns>vrchol</returns>
            public Vertex GetVertex()
            {
                return vertex;
            }
        }
        #endregion

        // Method
        #region
        /// <summary>
        /// Zjistí délku nejkratšího cyklu
        /// girth, isCyclic
        /// BFS Parallel
        /// Time complexity: O(V^2)
        /// Space complexity: O(V + E)
        /// </summary>
        private void CycleGirth()
        {
            // Variable
            List<Vertex> allVerticesList;
            int actualBestGirth = int.MaxValue;
            object myLock = new object();

            allVerticesList = graph.AllVertices();

            Parallel.ForEach(allVerticesList, vertex =>
            {
                int bestGirth = CycleGirthBFSParallel(vertex);
                
                if (actualBestGirth > bestGirth)
                {
                    lock (myLock)
                    {
                        if (actualBestGirth > bestGirth)
                            actualBestGirth = bestGirth;
                    }
                }
            });

            if (actualBestGirth == int.MaxValue)
            {
                girth = 0;
                isCyclic = false;
            }
            else
            {
                girth = actualBestGirth;
                isCyclic = true;
            }
        }
        
        /// <summary>
        /// Pro daný vrchol zjistí nejmenší cyklus pomocí BFS
        /// Pokud cyklus neexistuje, tak vrátí int.MaxValue
        /// </summary>
        /// <param name="root">Vrchol, který bude kořenem v BFS</param>
        /// <returns>délku cyklu</returns>
        private int CycleGirthBFSParallel(Vertex root)
        {
            // Variable
            int bestGirth = int.MaxValue;
            Queue<CycleNode> nodeBFSQueue = new Queue<CycleNode>();
            Dictionary<Vertex, int> nodeDictionary = new Dictionary<Vertex, int>();
            List<Vertex> vertexNeighboursList = new List<Vertex>();

            CycleNode rootNode = new CycleNode(root, 0);
            nodeBFSQueue.Enqueue(rootNode);
            nodeDictionary.Add(rootNode.GetVertex(), rootNode.GetDepth());

            while (nodeBFSQueue.Count != 0)
            {
                CycleNode node = nodeBFSQueue.Dequeue();
                int depth = node.GetDepth() + 1;

                vertexNeighboursList = graph.Neighbours(node.GetVertex());

                foreach (Vertex vertexNeighbour in vertexNeighboursList)
                {
                    // Tento vrchol jsme doposud neviděli
                    if (!nodeDictionary.ContainsKey(vertexNeighbour))
                    {
                        nodeBFSQueue.Enqueue(new CycleNode(vertexNeighbour, depth));
                        nodeDictionary.Add(vertexNeighbour, depth);
                    }
                    else
                    {
                        // Cyklus liché délky
                        if (nodeDictionary[vertexNeighbour] == depth - 1)
                        {
                            if (depth * 2 - 1 < bestGirth)
                            {
                                bestGirth = depth * 2 - 1;
                            }
                        }
                        else
                        {
                            // Cyklus sudé délky
                            if (nodeDictionary[vertexNeighbour] == depth)
                            {
                                if (depth * 2 < bestGirth)
                                {
                                    bestGirth = depth * 2;
                                }
                            }
                        }
                    }
                }
            }

            return bestGirth;
        }

        /// <summary>
        /// Zjistí zda je graf cyklický
        /// isCyclic
        /// Union-Find
        /// Time complexity: O(V * E)
        /// Space complexity: O(V + E)
        /// </summary>
        private void CycleIsCyclic()
        {
            // Variable
            List<Vertex> allVerticesList;
            List<Vertex> vertexNeighboursList;
            HashSet<Vertex> expandedVerticesHashSet;

            cycleDisjointSetDictionary = new Dictionary<Vertex, Vertex>(GetCountVertices());
            expandedVerticesHashSet = new HashSet<Vertex>();
            allVerticesList = graph.AllVertices();

            // Create disjoint set
            foreach(Vertex vertex in allVerticesList)
            {
                cycleDisjointSetDictionary.Add(vertex, vertex);
            }

            // Core algorithm
            isCyclic = false;

            foreach(Vertex vertex in allVerticesList)
            {
                vertexNeighboursList = graph.Neighbours(vertex);

                foreach(Vertex neighbourVertex in vertexNeighboursList)
                {
                    if (expandedVerticesHashSet.Contains(neighbourVertex))
                        continue;

                    Vertex vertex1Representative = CycleFind(vertex);
                    Vertex vertex2Representative = CycleFind(neighbourVertex);

                    if (vertex1Representative == vertex2Representative)
                    {
                        isCyclic = true;
                        return;
                    }

                    CycleUnion(vertex, neighbourVertex);
                }

                expandedVerticesHashSet.Add(vertex);
            }
        }

        /// <summary>
        /// Vrátí reprezentanta k danému vrcholu
        /// Time complexity: O(1)
        /// </summary>
        /// <param name="vertex">vrchol, ke kterému hledáme reprezentanta</param>
        /// <returns>reprezentant vrcholu</returns>
        private Vertex CycleFind(Vertex vertex)
        {
            return cycleDisjointSetDictionary[vertex];
        }

        /// <summary>
        /// Spojí dvě komponenty obsahující dané vrcholy
        /// Time complexity: O(V)
        /// </summary>
        /// <param name="vertex1">vrchol z první komponenty</param>
        /// <param name="vertex2">vrchol z druhé komponenty</param>
        private void CycleUnion(Vertex vertex1, Vertex vertex2)
        {
            Vertex vertex1Representative = CycleFind(vertex1);
            Vertex vertex2Representative = CycleFind(vertex2);
            
            for (int i = 0; i < cycleDisjointSetDictionary.Count; i++)
            {
                Vertex vertex = cycleDisjointSetDictionary.ElementAt(i).Key;
                if (cycleDisjointSetDictionary[vertex] == vertex2Representative)
                    cycleDisjointSetDictionary[vertex] = vertex1Representative;
            }
        }
        #endregion
    }
}