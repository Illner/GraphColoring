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
        /// Check if potentional Perfect elimination ordering (perfectEliminationOrderingList) is correct.
        /// Return true if PEO is correct (it means the graph is chordal), otherwise return false (the graph is not chordal)
        /// isChordal
        /// </summary>
        private void IsPerfectEliminationOrderingParallel() 
        {
            object locker = new object();
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
                        lock (locker)
                        {
                            isChordal = false;
                        }
                    }
                }
            };
        }

        /// <summary>
        /// Return indexes of neighbors of vertex (with the index) on the left from the vertex in PEO
        /// </summary>
        /// <param name="index">The index</param>
        /// <returns>List of indexes</returns>
        private List<int> GetLeftNeighborhood(int index)
        {
            // Variable
            IVertexInterface vertex;
            List<int> righNeighborhoodList = new List<int>();
            List<IVertexInterface> neighborsList;

            vertex = perfectEliminationOrderingList[index];
            neighborsList = graph.Neighbours(vertex);

            for (int i = 0; i < index; i++)
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
