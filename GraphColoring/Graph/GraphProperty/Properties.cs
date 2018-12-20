using System;
using System.Linq;
using System.Collections.Generic;

namespace GraphColoring.Graph.GraphProperty
{
    partial class GraphProperty
    {
        // Variable
        #region
        /// <summary>
        /// isConnected - je graf souvislý
        /// isRegular - je graf regulární
        /// isCyclic - je graf cyklický
        /// isChordal - graph is chordal
        /// isEulerian - je graf eulerovský - EulerianGraphEnum
        /// graphClass - Třída grafu - GraphClassEnum
        /// </summary>
        private Boolean? isConnected;
        private Boolean? isRegular;
        private Boolean? isCyclic;
        private Boolean? isChordal;
        private EulerianGraphEnum isEulerian = EulerianGraphEnum.undefined;
        private GraphClass.GraphClass.GraphClassEnum graphClass = GraphClass.GraphClass.GraphClassEnum.undefined;
        #endregion

        // Method
        #region
        /// <summary>
        /// Zjistí zda je graf souvislý
        /// countComponents, componentsList
        /// BFS
        /// Time complexity: O(V + E)
        /// Sace complexity: O(V + E) + vytvořené grafy
        /// </summary>
        private void IsConnected()
        {
            if (GetCountComponents() != 1)
                isConnected = false;
            else
                isConnected = true;
            
        }

        /// <summary>
        /// Zjistí zda je graf regulární
        /// isRegular, degreeSequence
        /// Time complexity: O(V)
        /// Space complexity: O(V)
        /// </summary>
        private void IsRegular()
        {
            // Variable
            int firstDegree, lastDegree;

            firstDegree = GetMinimumVertexDegree();
            lastDegree = GetMaximumVertexDegree();
            
            if (firstDegree == lastDegree)
                isRegular = true;
            else
                isRegular = false;
        }

        /// <summary>
        /// Zjistí zda je graf eulerovský
        /// isEulerian, degreeSequence
        /// Time complexity: O(V)
        /// Space complexity: O(V)
        /// </summary>
        private void IsEulerian()
        {
            // Variable
            int evenDegrees = 0;
            int oddDegrees = 0;

            if (!GetIsConnected())
                isEulerian = EulerianGraphEnum.notEulerian;

            if (degreeSequence == null)
                DegreeSequence(false);

            foreach (int degree in degreeSequenceInt)
            {
                if (degree % 2 == 0)
                    evenDegrees++;
                else
                    oddDegrees++;
            }

            isEulerian = EulerianGraphEnum.notEulerian;

            if (oddDegrees <= 2)
                isEulerian = EulerianGraphEnum.semiEulerian;

            if (oddDegrees == 0)
                isEulerian = EulerianGraphEnum.eulerian;
        }

        /// <summary>
        /// Determine if graph is chordal
        /// isChordal, perfectEliminationOrderingList
        /// Time complexivity: O(V + E)
        /// Space complexivity: O(V)
        /// </summary>
        private void IsChordal()
        {
            PerfectEliminationOrdering();
            IsPerfectEliminationOrderingParallel();

            // The graph is not chordal => need to delete perfectEliminationOrderingList
            if (!(bool)isChordal)
            {
                perfectEliminationOrderingList = new List<IVertexInterface>();
            }
        }
        #endregion

        // Property 
        #region
        /// <summary>
        /// Vrátí true pokud je graf souvislý, jinak vrátí false
        /// </summary>
        /// <returns>true pokud je graf souvislý, jinak vrátí false</returns>
        public bool GetIsConnected()
        {
            if (!isConnected.HasValue)
                IsConnected();

            return (bool)isConnected;
        }
 
        /// <summary>
        /// Vrátí true pokud je graf regulární, jinak vrátí false
        /// </summary>
        /// <returns>true pokud je graf regulární, jinak vrátí false</returns>
        public bool GetIsRegular()
        {
            if (!isRegular.HasValue)
                IsRegular();

            return (bool)isRegular;
        }

        /// <summary>
        /// Vrátí true pokud je graf cyklická, jinak vrátí false
        /// </summary>
        /// <returns>true pokud je graf cyklický, jinak vrátí false</returns>
        public bool GetIsCyclic()
        {
            if (!isCyclic.HasValue)
                CycleIsCyclic();

            return (bool)isCyclic;
        }

        /// <summary>
        /// Return true if graph is chordal, otherwise return false
        /// </summary>
        /// <returns>true if graph is chordal</returns>
        public bool GetIsChordal()
        {
            if (!isChordal.HasValue)
                IsChordal();

            return (bool)isChordal;
        }

        /// <summary>
        /// Vrátí:
        /// eulerian, pokud graf obsahuje eulerovský cyklus
        /// semiEulerian, pokud graf obsahuje eulerovský tah
        /// notEulerian, pokud graf neobsahuje ani eulerovský cyklus, ani eulerovský tah
        /// </summary>
        /// <returns>EulerianGraphEnum</returns>
        public EulerianGraphEnum GetIsEulerian()
        {
            if (isEulerian == EulerianGraphEnum.undefined)
                IsEulerian();

            return isEulerian;
        }
        
        /// <summary>
        /// Vrátí třídu grafu - GraphClassEnum
        /// </summary>
        /// <returns>třídu grafu</returns>
        public GraphClass.GraphClass.GraphClassEnum GetGraphClass()
        {
            if (graphClass == GraphClass.GraphClass.GraphClassEnum.undefined)
                graphClass = GraphClass.GraphClass.GetGraphClass(graph);

            return graphClass;
        }
        #endregion
    }
}
