using System;
using System.Collections.Generic;

namespace GraphColoring.Graph.GraphProperty
{
    partial class GraphProperty
    {
        #region Variable
        /// <summary>
        /// isEulerian - EulerianGraphEnum
        /// graphClass - GraphClassEnum
        /// </summary>
        private Boolean? isConnected;
        private Boolean? isRegular;
        private Boolean? isCyclic;
        private Boolean? isChordal;
        private EulerianGraphEnum isEulerian = EulerianGraphEnum.undefined;
        private GraphClass.GraphClass.GraphClassEnum graphClass = GraphClass.GraphClass.GraphClassEnum.undefined;
        #endregion

        #region Method
        /// <summary>
        /// Determine if the graph is connected
        /// Change: countComponents, componentsList
        /// Time complexity: O(V + E)
        /// Sace complexity: O(V + E) + new graphs
        /// </summary>
        private void IsConnected()
        {
            if (GetCountComponents() != 1)
                isConnected = false;
            else
                isConnected = true;
            
        }

        /// <summary>
        /// Determine if the graph is regular
        /// Change: isRegular, degreeSequence, minimumVertexDegree, maximumVertexDegree
        /// Time complexity: O(V + E)
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
        /// Determine if the graph is eulerian
        /// Change: isEulerian, degreeSequence
        /// Time complexity: O(V + E)
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
        /// Determine if the graph is chordal
        /// Change: isChordal, perfectEliminationOrderingList
        /// Time complexity: O(V * (V + Delta(G)^2))
        /// Space complexivity: O(V * Delta(G))
        /// </summary>
        private void IsChordal()
        {
            PerfectEliminationOrdering();
            IsPerfectEliminationOrdering();

            // The graph is not chordal => need to delete perfectEliminationOrderingList and righNeighborhoodDictionary
            if (!(bool)isChordal)
            {
                perfectEliminationOrderingList = new List<IVertexInterface>();
                righNeighborhoodDictionary = new Dictionary<int, List<int>>();
            }
        }
        #endregion
        
        #region Property
        /// <summary>
        /// Return true if the graph is connected, otherwise false
        /// </summary>
        /// <returns>true if the graph is connected</returns>
        public bool GetIsConnected()
        {
            if (!isConnected.HasValue)
                IsConnected();

            return (bool)isConnected;
        }
 
        /// <summary>
        /// Return true if the graph is regular, otherwise false
        /// </summary>
        /// <returns>true if the graph is regular</returns>
        public bool GetIsRegular()
        {
            if (!isRegular.HasValue)
                IsRegular();

            return (bool)isRegular;
        }

        /// <summary>
        /// Return true if the graph is cyclic, otherwise false
        /// </summary>
        /// <returns>true if the graph is cyclic</returns>
        public bool GetIsCyclic()
        {
            if (!isCyclic.HasValue)
                CycleIsCyclic();

            return (bool)isCyclic;
        }

        /// <summary>
        /// Return true if the graph is chordal, otherwise false
        /// </summary>
        /// <returns>true if the graph is chordal</returns>
        public bool GetIsChordal()
        {
            if (!isChordal.HasValue)
                IsChordal();

            return (bool)isChordal;
        }

        /// <summary>
        /// Return:
        /// EulerianGraphEnum.eulerian if the graph contains eulerian cycle,
        /// EulerianGraphEnum.semi-eulerian if the graph contains eulerian trail, otherwise
        /// EulerianGraphEnum.notEulerian
        /// </summary>
        /// <returns>EulerianGraphEnum</returns>
        public EulerianGraphEnum GetIsEulerian()
        {
            if (isEulerian == EulerianGraphEnum.undefined)
                IsEulerian();

            return isEulerian;
        }

        /// <summary>
        /// Return graph class - GraphClassEnum
        /// </summary>
        /// <returns>graph calss - GraphClassEnum</returns>
        public GraphClass.GraphClass.GraphClassEnum GetGraphClass()
        {
            if (graphClass == GraphClass.GraphClass.GraphClassEnum.undefined)
                graphClass = GraphClass.GraphClass.GetGraphClass(graph);

            return graphClass;
        }
        #endregion
    }
}
