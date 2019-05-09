using System;
using System.Linq;
using System.Collections.Generic;

namespace GraphColoring.Graph.GraphProperty
{
    partial class GraphProperty
    {
        private Dictionary<int, List<int>> righNeighborhoodDictionary;

        /// <summary>
        /// Set potrentional Perfect elimination ordering.
        /// It is correct only if a graph is chordal!
        /// You can check it via IsPerfectEliminationOrderingParallel
        /// Change: perfectEliminationOrderingList
        /// Use lex-BFS algorithm
        /// Time complexity: O(V * log(V) + E)
        /// Space complexity: O(V)
        /// </summary>
        private void PerfectEliminationOrdering()
        {
            // Variable
            int countVertices = graph.GetRealCountVertices();
            perfectEliminationOrderingList = new List<IVertexInterface>(countVertices);
            MyDataStructure.FibonacciHeap priorityQueue = new MyDataStructure.FibonacciHeap(countVertices);
            Dictionary<IVertexInterface, int> mappingVertexDictionary = new Dictionary<IVertexInterface, int>();
            IVertexInterface[] mappingVertexArray = new IVertexInterface[countVertices];

            // Initilize perfectEliminationOrderingList + mapping
            int help = 0;
            foreach(IVertexInterface vertex in graph.AllVertices())
            {
                // Mapping
                mappingVertexDictionary.Add(vertex, help);
                mappingVertexArray[help] = vertex;
                help++;
            }

            // Add vertices to fibonacci heap
            foreach (IVertexInterface vertex in graph.AllVertices())
            {
                priorityQueue.Insert(mappingVertexDictionary[vertex], countVertices);
            }
            
            for (int i = 0; i < countVertices; i++)
            {
                IVertexInterface selectedVertex = mappingVertexArray[priorityQueue.ExtractMin().GetIdentifier()];

                foreach (IVertexInterface neighbour in graph.Neighbours(selectedVertex))
                {
                    int neighbourFibonacciIdentifier = mappingVertexDictionary[neighbour];
                    if (priorityQueue.ElementExists(neighbourFibonacciIdentifier))
                        priorityQueue.Decrease(neighbourFibonacciIdentifier, priorityQueue.GetValue(neighbourFibonacciIdentifier) - 1);
                }

                perfectEliminationOrderingList.Add(selectedVertex);
            }
        }

        /// <summary>
        /// Check if a potentional Perfect elimination ordering (perfectEliminationOrderingList) is correct.
        /// Return true if the PEO is correct (it means that the graph is chordal), otherwise return false (the graph is not chordal)
        /// Change: isChordal
        /// Time complexity: O(V * (V + Delta(G)^2))
        /// Space complexity: O(V * Delta(G))
        /// </summary>
        private void IsPerfectEliminationOrdering() 
        {
            isChordal = true;

            for (int index = 2; index < perfectEliminationOrderingList.Count; index++)
            {
                // Variable
                int pv;
                List<int> lnvList;
                List<int> lnpvList;

                // Get LNv - {pv}
                lnvList = GetLeftNeighborhood(index);

                // If count is equal to 0 or 1, it is trivially true
                if (lnvList.Count > 1)
                {
                    pv = lnvList.Last();
                    lnvList.Remove(pv);

                    // Get LNpv
                    lnpvList = GetLeftNeighborhood(pv);

                    // LNv - {pv} is included in RNpv
                    if (lnvList.Except(lnpvList).Any())
                    {
                        isChordal = false;
                    }
                }

                if (!(bool)isChordal)
                    return;
            };
        }

        /// <summary>
        /// Return indexes of neighbors of vertex (with the index) on the left from the vertex in a PEO
        /// Time complexity: O(V)
        /// Space complexity: O(V * Delta(G))
        /// </summary>
        /// <param name="index">index</param>
        /// <returns>list of indexes</returns>
        private List<int> GetLeftNeighborhood(int index)
        {
            // Variable
            IVertexInterface vertex;
            List<int> righNeighborhoodList;
            List<IVertexInterface> neighborsList;

            // Initialize righNeighborhoodDictionary
            if (righNeighborhoodDictionary == null)
            {
                righNeighborhoodDictionary = new Dictionary<int, List<int>>();
            }

            if (righNeighborhoodDictionary.TryGetValue(index, out righNeighborhoodList))
            {
                // Record exists
                return new List<int>(righNeighborhoodList);
            }

            // Record does not exist
            righNeighborhoodList = new List<int>();

            vertex = perfectEliminationOrderingList[index];
            neighborsList = graph.Neighbours(vertex);

            for (int i = 0; i < index; i++)
            {
                if (neighborsList.Contains(perfectEliminationOrderingList[i]))
                {
                    righNeighborhoodList.Add(i);
                }
            }

            righNeighborhoodDictionary.Add(index, righNeighborhoodList);

            return new List<int>(righNeighborhoodList);
        }
    }
}
