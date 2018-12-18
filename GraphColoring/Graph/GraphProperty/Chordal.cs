using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring.Graph.GraphProperty
{
    partial class GraphProperty
    {
        /// <summary>
        /// Set potrentional Perfect elimination ordering.
        /// It is correct only if graph is chordal!
        /// You can check it via IsPerfectEliminationOrderingParallel
        /// perfectEliminationOrderingList
        /// Use lex-BFS algorithm
        /// </summary>
        public void PerfectEliminationOrdering()
        {
            // Variable
            int shift = graph.GetFirstVertex().GetIdentifier();
            int countVertices = graph.GetRealCountVertices();
            perfectEliminationOrderingList = new List<IVertexInterface>(countVertices);
            MyDataStructure.FibonacciHeap priorityQueue = new MyDataStructure.FibonacciHeap(countVertices);

            // Initilize perfectEliminationOrderingList
            for (int i = 0; i < countVertices; i++)
            {
                perfectEliminationOrderingList.Add(null);
            }

            // Add vertices to fibonacci heap
            foreach (IVertexInterface vertex in graph.AllVertices())
            {
                priorityQueue.Insert(vertex.GetIdentifier() - shift, countVertices);
            }
            
            for (int i = countVertices - 1; i >= 0; i--)
            {
                int vertexIdentifier = priorityQueue.ExtractMin().GetIdentifier() + shift;

                foreach (IVertexInterface neighbour in graph.Neighbours(graph.GetVertexByIdentifier(vertexIdentifier)))
                {
                    int neighbourIdentifier = neighbour.GetIdentifier() - shift;
                    if (priorityQueue.ElementExists(neighbourIdentifier))
                        priorityQueue.Decrease(neighbourIdentifier, priorityQueue.GetValue(neighbourIdentifier) - 1);
                }

                perfectEliminationOrderingList[i] = (graph.GetVertexByIdentifier(vertexIdentifier));
            }
        }

        /// <summary>
        /// Check if potentional Perfect elimination ordering (perfectEliminationOrderingList) is correct.
        /// Return true if PEO is correct (it means the graph is chordal), otherwise return false (the graph is not chordal)
        /// isChordal
        /// </summary>
        public void IsPerfectEliminationOrderingParallel() 
        {
            object locker = new object();
            isChordal = true;

            for (int index = 0; index < perfectEliminationOrderingList.Count - 2; index++)
            {
                // Variable
                int pv;
                List<int> rnvList;
                List<int> rnpvList;

                // Get RNv - {pv}
                rnvList = GetRightNeighborhood(index);

                // If count is equal to 0 or 1, it is trivially true
                if (rnvList.Count > 1)
                {
                    pv = rnvList.First();
                    rnvList.Remove(pv);

                    // Get RNpv
                    rnpvList = GetRightNeighborhood(pv);

                    // RNv - {pv} is included in RNpv
                    if (rnvList.Except(rnpvList).Any())
                    {
                        lock (locker)
                        {
                            isChordal = false;
                        }
                    }
                }
            };
        }

        /// <summary>
        /// Return indexes of neighbors of vertex (with the index) on the right from the vertex in PEO
        /// </summary>
        /// <param name="index">The index</param>
        /// <returns>List of indexes</returns>
        private List<int> GetRightNeighborhood(int index)
        {
            // Variable
            IVertexInterface vertex;
            List<int> righNeighborhoodList = new List<int>();
            List<IVertexInterface> neighborsList;

            vertex = perfectEliminationOrderingList[index];
            neighborsList = graph.Neighbours(vertex);

            for (int i = index + 1; i < graph.GetRealCountVertices(); i++)
            {
                if (neighborsList.Contains(perfectEliminationOrderingList[i]))
                {
                    righNeighborhoodList.Add(i);
                }
            }

            return righNeighborhoodList;
        }
    }
}
