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
        /// isEulerian - je graf eulerovský - EulerianGraphEnum
        /// </summary>
        private Boolean? isConnected;
        private Boolean? isRegular;
        private Boolean? isCyclic;
        private EulerianGraphEnum isEulerian = EulerianGraphEnum.undefined;
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

            if (degreeSequence == null)
                GetDegreeSequence();
            
            firstDegree = degreeSequenceInt.First();
            lastDegree = degreeSequenceInt.Last();
            
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

            if (degreeSequence == null)
                DegreeSequence();

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
        #endregion
    }
}
