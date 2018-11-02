using System;
using System.Collections.Generic;
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
        /// order - the number of vertices
        /// size - the number of edges
        /// countComponents - Number of connected components
        /// circuitRank - a linear combination of the numbers of edges, vertices, and components
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
        private double? cayleysFormula;
        #endregion

        // Method
        #region
        /// <summary>
        /// Inkrementuje počet hran o jedna
        /// Pokud není nastavená proměnná canDeIncreaseCountEdges, vyvolá výjimku GraphPermissionDeIncrementVerticesEdges
        /// </summary>
        public void IncrementCountEdges()
        {
            if (!graph.GetCanDeIncreaseCountEdges())
                throw new MyException.GraphPermissionDeIncrementVerticesEdges("Edge");

            size++;
        }

        /// <summary>
        /// Dekrementuje počet hran o jedna
        /// Pokud není nastavená proměnná canDeIncreaseCountEdges, vyvolá výjimku GraphPermissionDeIncrementVerticesEdges
        /// Pokud je počet hran 0, vyvolá se vyjímka GraphInvalidDecrementCountEdges
        /// </summary>
        public void DecrementCountEdges()
        {
            if (!graph.GetCanDeIncreaseCountEdges())
                throw new MyException.GraphPermissionDeIncrementVerticesEdges("Edge");

            if (size == 0)
                throw new MyException.GraphInvalidDecrementCountEdges();

            size--;
        }

        /// <summary>
        /// Dekrementuje počet hran o count
        /// Pokud není nastavená proměnná canDeIncreaseCountEdges, vyvolá výjimku GraphPermissionDeIncrementVerticesEdges
        /// Pokud po dekrementaci bude min nez 0 hran, vyvolá se vyjímka GraphInvalidDecrementCountEdges
        /// </summary>
        /// <param name="count">Počet hran pro dekrementaci</param>
        public void DecrementCountEdges(int count)
        {
            if (!graph.GetCanDeIncreaseCountEdges())
                throw new MyException.GraphPermissionDeIncrementVerticesEdges("Edge");

            if (size - count < 0)
                throw new MyException.GraphInvalidDecrementCountEdges();
            
            size -= count;
        }

        /// <summary>
        /// Inkrementuje počet vrcholu o jedna
        /// Pokud není nastavená proměnná canDeIncreaseCountVertices, vyvolá výjimku GraphPermissionDeIncrementVerticesEdges
        /// </summary>
        public void IncrementCountVertices()
        {
            if (!graph.GetCanDeIncreaseCountVertices())
                throw new MyException.GraphPermissionDeIncrementVerticesEdges("Vertex");

            order++;
        }

        /// <summary>
        /// Dekrementuje počet vrcholu o jedna
        /// Pokud není nastavená proměnná canDeIncreaseCountVertices, vyvolá výjimku GraphPermissionDeIncrementVerticesEdges
        /// Pokud je počet vrcholů 0, vyvolá se vyjímka GraphInvalidDecrementCountVertices
        /// </summary>
        public void DecrementCountVertices()
        {
            if (!graph.GetCanDeIncreaseCountVertices())
                throw new MyException.GraphPermissionDeIncrementVerticesEdges("Vertex");

            if (order == 0)
                throw new MyException.GraphInvalidDecrementCountVertices();

            order--;
        }

        /// <summary>
        /// Dekrementuje počet vrcholu o coun
        /// Pokud není nastavená proměnná canDeIncreaseCountVertices, vyvolá výjimku GraphPermissionDeIncrementVerticesEdges
        /// Pokud je počet vrcholů 0, vyvolá se vyjímka GraphInvalidDecrementCountVertices
        /// </summary>
        /// <param name="count">Počet vrcholu pro dekrementaci</param>
        public void DecrementCountVertices(int count)
        {
            if (!graph.GetCanDeIncreaseCountVertices())
                throw new MyException.GraphPermissionDeIncrementVerticesEdges("Vertex");

            if (order - count < 0)
                throw new MyException.GraphInvalidDecrementCountVertices();

            order -= count;
        }

        /// <summary>
        /// Zjistí circuit rank
        /// circuitRank, countComponents, componentsList
        /// BFS
        /// Time complexity: O(V + E)
        /// Sace complexity: O(V + E) + vytvořené grafy
        /// </summary>
        private void CircuitRank()
        {
            if (!countComponents.HasValue)
                Components();

            circuitRank = GetCountEdges() - GetCountVertices() + GetCountComponents();
        }

        /// <summary>
        /// Zjistí celkový počet koster
        /// cayleysFormula
        /// Time complexity: O(1)
        /// Space complexity: O(1)
        /// </summary>
        private void CayleysFormula()
        {
            cayleysFormula = Math.Pow(GetCountVertices(), (GetCountVertices() - 2));
        }
        #endregion

        // Property
        #region
        /// <summary>
        /// Vrátí počet vrcholů
        /// </summary>
        /// <returns>počet vrcholů</returns>
        public int GetCountVertices()
        {
            return order;
        }

        /// <summary>
        /// Nastaví počet vrcholů
        /// </summary>
        /// <param name="countVertices">počet vrcholů</param>
        private void SetCountVertices(int countVertices)
        {
            order = countVertices;
        }
        
        /// <summary>
        /// Vrátí počet hran
        /// </summary>
        /// <returns>počet hran</returns>
        public int GetCountEdges()
        {
            return size;
        }
        
        /// <summary>
        /// Vrátí počet komponent souvislosti
        /// </summary>
        /// <returns>počet komponent souvislosti</returns>
        public int GetCountComponents()
        {
            if (!countComponents.HasValue)
                Components();

            return (int)countComponents;
        }
        
        /// <summary>
        /// Vrátí circuit rank
        /// </summary>
        /// <returns>circuit rank</returns>
        public int GetCircuitRank()
        {
            if (!circuitRank.HasValue)
                CircuitRank();

            return (int)circuitRank;
        }
        
        /// <summary>
        /// Vrátí girth
        /// </summary>
        /// <returns>girth</returns>
        public int GetGirth()
        {
            if (!girth.HasValue)
                CycleGirth();

            return (int)girth;
        }

        /// <summary>
        /// Vrátí minimální stupeň vrcholu
        /// </summary>
        /// <returns>minimální stupeň vrcholu</returns>
        public int GetMinimumVertexDegree()
        {
            if (!minimumVertexDegree.HasValue)
            {
                GetDegreeSequence(false);

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
        /// Vrátí první vrchol s daným stupněm.
        /// </summary>
        /// <param name="degree">daný stupěň</param>
        /// <returns>vrchol s daným stupněm</returns>
        public Vertex GetVertexWithDegree(int degree)
        {
            if (degreeSequence == null)
                GetDegreeSequence(false);

            return (from record in degreeSequence where record.Value == degree select record.Key).First();
        }

        /// <summary>
        /// Vrátí maximální stupeň vrcholu
        /// </summary>
        /// <returns>maximální stupeň vrcholu</returns>
        public int GetMaximumVertexDegree()
        {
            if (!maximumVertexDegree.HasValue)
            {
                GetDegreeSequence(false);

                if (isDegreeSequenceSorted)
                    maximumVertexDegree = degreeSequenceInt.Last();
                else
                    maximumVertexDegree = degreeSequenceInt.Max();
            }

            return (int)maximumVertexDegree;
        }

        /// <summary>
        /// Vrátí průměrný stupeň vrcholu
        /// </summary>
        /// <returns>průměrný stupeň vrcholu</returns>
        public double GetAverageVertexDegree()
        {
            if (!averageVertexDegree.HasValue)
            {
                GetDegreeSequence(false);

                averageVertexDegree = degreeSequenceInt.Average();
            }

            return (double)averageVertexDegree;
        }

        /// <summary>
        /// Vrátí celkový počet koster grafu
        /// </summary>
        /// <returns>celkový počet koster grafu</returns>
        public double GetCayleysFormula()
        {
            if (!cayleysFormula.HasValue)
                CayleysFormula();

            return (double)cayleysFormula;
        }
        #endregion
    }
}
