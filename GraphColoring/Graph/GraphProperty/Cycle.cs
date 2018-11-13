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
        private Dictionary<IVertexInterface, IVertexInterface> cycleDisjointSetDictionary;
        #endregion

        /// <summary>
        /// Používá CycleGirth
        /// </summary>
        class CycleNode
        #region
        {
            // Variable
            private int depth;
            private IVertexInterface vertex;

            // Constructor
            public CycleNode(IVertexInterface vertex, int depth)
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
            public IVertexInterface GetVertex()
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
            List<IVertexInterface> allVerticesList;
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
        private int CycleGirthBFSParallel(IVertexInterface root)
        {
            // Variable
            int bestGirth = int.MaxValue;
            Queue<CycleNode> nodeBFSQueue = new Queue<CycleNode>();
            Dictionary<IVertexInterface, int> nodeDictionary = new Dictionary<IVertexInterface, int>();
            List<IVertexInterface> vertexNeighboursList = new List<IVertexInterface>();

            CycleNode rootNode = new CycleNode(root, 0);
            nodeBFSQueue.Enqueue(rootNode);
            nodeDictionary.Add(rootNode.GetVertex(), rootNode.GetDepth());

            while (nodeBFSQueue.Count != 0)
            {
                CycleNode node = nodeBFSQueue.Dequeue();
                int depth = node.GetDepth() + 1;

                vertexNeighboursList = graph.Neighbours(node.GetVertex());

                foreach (IVertexInterface vertexNeighbour in vertexNeighboursList)
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
            List<IVertexInterface> allVerticesList;
            List<IVertexInterface> vertexNeighboursList;
            HashSet<IVertexInterface> expandedVerticesHashSet;

            cycleDisjointSetDictionary = new Dictionary<IVertexInterface, IVertexInterface>(GetCountVertices());
            expandedVerticesHashSet = new HashSet<IVertexInterface>();
            allVerticesList = graph.AllVertices();

            // Create disjoint set
            foreach(IVertexInterface vertex in allVerticesList)
            {
                cycleDisjointSetDictionary.Add(vertex, vertex);
            }

            // Core algorithm
            isCyclic = false;

            foreach(IVertexInterface vertex in allVerticesList)
            {
                vertexNeighboursList = graph.Neighbours(vertex);

                foreach(IVertexInterface neighbourVertex in vertexNeighboursList)
                {
                    if (expandedVerticesHashSet.Contains(neighbourVertex))
                        continue;

                    IVertexInterface vertex1Representative = CycleFind(vertex);
                    IVertexInterface vertex2Representative = CycleFind(neighbourVertex);

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
        private IVertexInterface CycleFind(IVertexInterface vertex)
        {
            return cycleDisjointSetDictionary[vertex];
        }

        /// <summary>
        /// Spojí dvě komponenty obsahující dané vrcholy
        /// Time complexity: O(V)
        /// </summary>
        /// <param name="vertex1">vrchol z první komponenty</param>
        /// <param name="vertex2">vrchol z druhé komponenty</param>
        private void CycleUnion(IVertexInterface vertex1, IVertexInterface vertex2)
        {
            IVertexInterface vertex1Representative = CycleFind(vertex1);
            IVertexInterface vertex2Representative = CycleFind(vertex2);
            
            for (int i = 0; i < cycleDisjointSetDictionary.Count; i++)
            {
                IVertexInterface vertex = cycleDisjointSetDictionary.ElementAt(i).Key;
                if (cycleDisjointSetDictionary[vertex] == vertex2Representative)
                    cycleDisjointSetDictionary[vertex] = vertex1Representative;
            }
        }
        #endregion
    }
}