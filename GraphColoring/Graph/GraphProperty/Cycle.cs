using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GraphColoring.Graph.GraphProperty
{
    partial class GraphProperty
    {
        #region Variable
        /// <summary>
        /// cycleDisjointSetDictionary - union-find structure
        /// </summary>
        private Dictionary<IVertexInterface, IVertexInterface> cycleDisjointSetDictionary;
        #endregion
        
        #region Help class - CycleNode
        /// <summary>
        /// Help class used by CycleGirth
        /// </summary>
        class CycleNode
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

        #region Method
        /// <summary>
        /// Determine the shortest cycle
        /// Change: girth, isCyclic
        /// Time complexity: O(V * (V + E))
        /// Space complexity: O(V + E)
        /// </summary>
        private void CycleGirthParallel()
        {
            // Variable
            List<IVertexInterface> allVerticesList;
            int actualBestGirth = int.MaxValue;
            object myLock = new object();

            allVerticesList = graph.AllVertices();

            Parallel.ForEach(allVerticesList, vertex =>
            {
                int bestGirth = CycleGirthBFS(vertex);
                
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
        /// Determine the shortest cycle which contains the vertex
        /// If the cycle does not exist returns int.MaxValue
        /// </summary>
        /// <param name="root">vertex</param>
        /// <returns>the shortest cycle which contains the vertex</returns>
        private int CycleGirthBFS(IVertexInterface root)
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
                    // We have not seen this vertex yet
                    if (!nodeDictionary.ContainsKey(vertexNeighbour))
                    {
                        nodeBFSQueue.Enqueue(new CycleNode(vertexNeighbour, depth));
                        nodeDictionary.Add(vertexNeighbour, depth);
                    }
                    else
                    {
                        // Odd cycle
                        if (nodeDictionary[vertexNeighbour] == depth - 1)
                        {
                            if (depth * 2 - 1 < bestGirth)
                            {
                                bestGirth = depth * 2 - 1;
                            }
                        }
                        else
                        {
                            // Even cycle
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
        /// Determine if the graph is cyclic
        /// Change: isCyclic
        /// Time complexity: O(V + E)
        /// Space complexity: O(V + E)
        /// </summary>
        protected void CycleIsCyclic()
        {
            // Variable
            Queue<IVertexInterface> verticesQueue;
            HashSet<IVertexInterface> visitedVerticesHashSet;
            Dictionary<IVertexInterface, IVertexInterface> parentsDictionary;

            verticesQueue = new Queue<IVertexInterface>();
            visitedVerticesHashSet = new HashSet<IVertexInterface>();
            parentsDictionary = new Dictionary<IVertexInterface, IVertexInterface>();

            verticesQueue.Enqueue(graph.GetFirstVertex());
            visitedVerticesHashSet.Add(graph.GetFirstVertex());

            isCyclic = false;

            // Initialize parentsDictionary
            foreach (IVertexInterface vertex in graph.AllVertices())
            {
                parentsDictionary.Add(vertex, null);
            }

            while (verticesQueue.Count != 0)
            {
                IVertexInterface vertex = verticesQueue.Dequeue();

                foreach (IVertexInterface neighbor in graph.Neighbours(vertex))
                {
                    if (!visitedVerticesHashSet.Contains(neighbor))
                    {
                        visitedVerticesHashSet.Add(neighbor);
                        verticesQueue.Enqueue(neighbor);
                        parentsDictionary[neighbor] = vertex;
                    }
                    else
                    {
                        if (parentsDictionary[vertex] != neighbor)
                        {
                            isCyclic = true;
                            return;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Determine if the graph is cyclic
        /// Change: isCyclic
        /// Using: Union-Find
        /// Time complexity: O(V^2 + E)
        /// Space complexity: O(V + E)
        /// </summary>
        private void CycleIsCyclicUnionFind()
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
        /// Find a representant
        /// Time complexity: O(1)
        /// </summary>
        /// <param name="vertex">vertex</param>
        /// <returns>representant of the vertex</returns>
        private IVertexInterface CycleFind(IVertexInterface vertex)
        {
            return cycleDisjointSetDictionary[vertex];
        }

        /// <summary>
        /// Union two components
        /// Time complexity: O(V)
        /// </summary>
        /// <param name="vertex1">first vertex</param>
        /// <param name="vertex2">second vertex</param>
        private void CycleUnion(IVertexInterface vertex1, IVertexInterface vertex2)
        {
            IVertexInterface vertex1Representative = CycleFind(vertex1);
            IVertexInterface vertex2Representative = CycleFind(vertex2);

            if (vertex1Representative == vertex2Representative)
                return;

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