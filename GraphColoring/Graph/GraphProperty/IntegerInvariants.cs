using System;
using System.Linq;

namespace GraphColoring.Graph.GraphProperty
{
    partial class GraphProperty
    {
        #region Variable
        /// <summary>
        /// order - number of vertices
        /// size - number of edges
        /// countComponents - number of connected components
        /// circuitRank - a linear combination of the numbers of edges, vertices and components
        /// girth - the length of the shortest cycle
        /// cayleysFormula - number of spanning trees
        /// </summary>
        private int order;
        private int size = 0;
        private int? countComponents;
        private int? circuitRank;
        private int? girth;
        private int? minimumVertexDegree;
        private int? maximumVertexDegree;
        private double? averageVertexDegree;
        private int? medianVertexDegree;
        private double? cayleysFormula;
        #endregion

        #region Method
        /// <summary>
        /// Increment number of edges
        /// If canDeIncreaseCountEdges is false, throws GraphPermissionDeIncrementVerticesEdges
        /// Change: size
        /// Time complexity: O(1)
        /// </summary>
        public void IncrementCountEdges()
        {
            if (!graph.GetCanDeIncreaseCountEdges())
                throw new MyException.GraphException.GraphPermissionDeIncrementVerticesEdges("Edge");

            size++;
        }

        /// <summary>
        /// Decrement number of edges
        /// If canDeIncreaseCountEdges is false, throws GraphPermissionDeIncrementVerticesEdges
        /// If count of edges is equal to 0, throws GraphInvalidDecrementCountEdges
        /// Change: size
        /// Time complexity: O(1)
        /// </summary>
        public void DecrementCountEdges()
        {
            if (!graph.GetCanDeIncreaseCountEdges())
                throw new MyException.GraphException.GraphPermissionDeIncrementVerticesEdges("Edge");

            if (size == 0)
                throw new MyException.GraphException.GraphInvalidDecrementCountEdges();

            size--;
        }

        /// <summary>
        /// Decrement number of edges
        /// If canDeIncreaseCountEdges is false, throws GraphPermissionDeIncrementVerticesEdges
        /// If the new value is less than 0, throws GraphInvalidDecrementCountEdges
        /// Change: size
        /// Time complexity: O(1)
        /// </summary>
        /// <param name="count">number of edges to decrease</param>
        public void DecrementCountEdges(int count)
        {
            if (!graph.GetCanDeIncreaseCountEdges())
                throw new MyException.GraphException.GraphPermissionDeIncrementVerticesEdges("Edge");

            if (size - count < 0)
                throw new MyException.GraphException.GraphInvalidDecrementCountEdges();
            
            size -= count;
        }

        /// <summary>
        /// Increment number of vertices
        /// If canDeIncreaseCountVertices is false, throws GraphPermissionDeIncrementVerticesEdges
        /// Change: order
        /// Time complexity: O(1)
        /// </summary>
        public void IncrementCountVertices()
        {
            if (!graph.GetCanDeIncreaseCountVertices())
                throw new MyException.GraphException.GraphPermissionDeIncrementVerticesEdges("Vertex");

            order++;
        }

        /// <summary>
        /// Decrement number of vertices
        /// If canDeIncreaseCountVertices is false, throws GraphPermissionDeIncrementVerticesEdges
        /// If count of vertices is equal to 0, throws GraphInvalidDecrementCountVertices
        /// Change: order
        /// Time complexity: O(1)
        /// </summary>
        public void DecrementCountVertices()
        {
            if (!graph.GetCanDeIncreaseCountVertices())
                throw new MyException.GraphException.GraphPermissionDeIncrementVerticesEdges("Vertex");

            if (order == 0)
                throw new MyException.GraphException.GraphInvalidDecrementCountVertices();

            order--;
        }

        /// <summary>
        /// Decrement number of vertices
        /// If canDeIncreaseCountVertices is false, throws GraphPermissionDeIncrementVerticesEdges
        /// If the new value is less than 0, throws GraphInvalidDecrementCountVertices
        /// Change: order
        /// Time complexity: O(1)
        /// </summary>
        /// <param name="count">number of vertices to decrease</param>
        public void DecrementCountVertices(int count)
        {
            if (!graph.GetCanDeIncreaseCountVertices())
                throw new MyException.GraphException.GraphPermissionDeIncrementVerticesEdges("Vertex");

            if (order - count < 0)
                throw new MyException.GraphException.GraphInvalidDecrementCountVertices();

            order -= count;
        }

        /// <summary>
        /// Determine circuit rank
        /// Change: circuitRank, countComponents, componentsList
        /// Time complexity: O(V + E)
        /// Sace complexity: O(V + E) + new graphs
        /// </summary>
        private void CircuitRank()
        {
            if (!countComponents.HasValue)
                Components();

            circuitRank = GetCountEdges() - GetCountVertices() + GetCountComponents();
        }

        /// <summary>
        /// Compute number of spanning trees
        /// Change: cayleysFormula
        /// Time complexity: O(1)
        /// Space complexity: O(1)
        /// </summary>
        private void CayleysFormula()
        {
            cayleysFormula = Math.Pow(GetCountVertices(), (GetCountVertices() - 2));
        }
        #endregion
        
        #region Property
        /// <summary>
        /// Return number of vertices (order)
        /// </summary>
        /// <returns>number of vertices</returns>
        public int GetCountVertices()
        {
            return order;
        }

        /// <summary>
        /// Set number of vertices
        /// </summary>
        /// <param name="countVertices">new number of vertices</param>
        private void SetCountVertices(int countVertices)
        {
            order = countVertices;
        }
        
        /// <summary>
        /// Return number of edges (size)
        /// </summary>
        /// <returns>number of edges</returns>
        public int GetCountEdges()
        {
            return size;
        }
        
        /// <summary>
        /// Return number of components
        /// </summary>
        /// <returns>number of components</returns>
        public int GetCountComponents()
        {
            if (!countComponents.HasValue)
                Components();

            return (int)countComponents;
        }
        
        /// <summary>
        /// Return circuit rank
        /// </summary>
        /// <returns>circuit rank</returns>
        public int GetCircuitRank()
        {
            if (!circuitRank.HasValue)
                CircuitRank();

            return (int)circuitRank;
        }
        
        /// <summary>
        /// Return girth
        /// </summary>
        /// <returns>girth</returns>
        public int GetGirth()
        {
            if (!girth.HasValue)
                CycleGirthParallel();

            return (int)girth;
        }

        /// <summary>
        /// Return minimal vertex degree
        /// </summary>
        /// <returns>minimal vertex degree</returns>
        public int GetMinimumVertexDegree()
        {
            if (!minimumVertexDegree.HasValue)
            {
                GetDegreeSequenceInt(false);

                if (isDegreeSequenceSorted)
                {
                    minimumVertexDegree = degreeSequenceInt.First();
                }
                else
                {
                    minimumVertexDegree = degreeSequenceInt.Min();
                }
            }

            return (int)minimumVertexDegree;
        }

        /// <summary>
        /// Return vertex with the degree
        /// If the vertex does not exist returns null
        /// </summary>
        /// <param name="degree">degree</param>
        /// <returns>vertex with the degree</returns>
        public IVertexInterface GetVertexWithDegree(int degree)
        {
            if (degreeSequence == null)
                GetDegreeSequenceInt(false);

            return (from record in degreeSequence where record.Value == degree select record.Key).First();
        }

        /// <summary>
        /// Return maximal vertex degree
        /// </summary>
        /// <returns>maximal vertex degree</returns>
        public int GetMaximumVertexDegree()
        {
            if (!maximumVertexDegree.HasValue)
            {
                GetDegreeSequenceInt(false);

                if (isDegreeSequenceSorted)
                    maximumVertexDegree = degreeSequenceInt.Last();
                else
                    maximumVertexDegree = degreeSequenceInt.Max();
            }

            return (int)maximumVertexDegree;
        }

        /// <summary>
        /// Return average vertex degree
        /// </summary>
        /// <returns>average vertex degree</returns>
        public double GetAverageVertexDegree()
        {
            if (!averageVertexDegree.HasValue)
            {
                GetDegreeSequenceInt(false);

                averageVertexDegree = degreeSequenceInt.Average();
            }

            return (double)averageVertexDegree;
        }

        /// <summary>
        /// Return median of the vertex degree list
        /// </summary>
        /// <returns>median of the vertex degree list</returns>
        public int GetMedianVertexDegree()
        {
            if (!medianVertexDegree.HasValue)
            {
                GetDegreeSequenceInt(true);

                int numberCount = degreeSequenceInt.Count;
                int halfIndex = degreeSequenceInt.Count / 2;
                if ((numberCount % 2) == 0)
                {
                    medianVertexDegree = ((degreeSequenceInt.ElementAt(halfIndex) + degreeSequenceInt.ElementAt(halfIndex - 1)) / 2);
                }
                else
                {
                    medianVertexDegree = degreeSequenceInt.ElementAt(halfIndex);
                }
            }

            return (int)medianVertexDegree;
        }

        /// <summary>
        /// Return number of spanning trees
        /// </summary>
        /// <returns>number of spanning trees</returns>
        public double GetCayleysFormula()
        {
            if (!cayleysFormula.HasValue)
                CayleysFormula();

            return (double)cayleysFormula;
        }
        #endregion
    }
}
