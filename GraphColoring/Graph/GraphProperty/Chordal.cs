﻿using System;
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

                perfectEliminationOrderingList.Add(null);
            }

            // Add vertices to fibonacci heap
            foreach (IVertexInterface vertex in graph.AllVertices())
            {
                priorityQueue.Insert(mappingVertexDictionary[vertex], countVertices);
            }
            
            for (int i = countVertices - 1; i >= 0; i--)
            {
                IVertexInterface selectedVertex = mappingVertexArray[priorityQueue.ExtractMin().GetIdentifier()];

                foreach (IVertexInterface neighbour in graph.Neighbours(selectedVertex))
                {
                    int neighbourFibonacciIdentifier = mappingVertexDictionary[neighbour];
                    if (priorityQueue.ElementExists(neighbourFibonacciIdentifier))
                        priorityQueue.Decrease(neighbourFibonacciIdentifier, priorityQueue.GetValue(neighbourFibonacciIdentifier) - 1);
                }

                perfectEliminationOrderingList[i] = (selectedVertex);
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
