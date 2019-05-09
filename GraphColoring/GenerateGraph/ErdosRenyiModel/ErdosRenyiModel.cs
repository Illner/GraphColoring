using System;
using System.Collections.Generic;

namespace GraphColoring.GenerateGraph.ErdosRenyiModel
{
    public partial class ErdosRenyiModel : IGenerateGraphInterface
    {
        #region Variable
        /// <summary>
        /// graph - generated graph
        /// countVertices - count vertices of generated graph
        /// erdosRenyiModelProbabilityEnum - probability attribute
        /// probability - probability for creating edge between two vertices (* 100)
        /// </summary>
        private Graph.IGraphInterface graph;
        private int countVertices;
        private Random random;
        private ErdosRenyiModelProbabilityEnum erdosRenyiModelProbabilityEnum = ErdosRenyiModelProbabilityEnum.notAssigned;
        public int probability;
        private const string GRAPHNAME = "RandomGraph";
        private HashSet<Graph.IVertexInterface> usedVerticesHashSet;
        #endregion
        
        #region Constructor
        /// <summary>
        /// If count of vertices is less than 1 throws GraphInvalidCountVerticesException
        /// </summary>
        /// <param name="countVertices">count of vertices</param>
        /// <param name="erdosRenyiModelProbabilityEnum">probability attribute - deafult = notAssigned</param>
        public ErdosRenyiModel(int countVertices, ErdosRenyiModelProbabilityEnum erdosRenyiModelProbabilityEnum = ErdosRenyiModelProbabilityEnum.notAssigned)
        {
            SetCountVertices(countVertices);
            this.erdosRenyiModelProbabilityEnum = erdosRenyiModelProbabilityEnum;
            usedVerticesHashSet = new HashSet<Graph.IVertexInterface>();

            // Create graph
            graph = new Graph.GraphEdgeList(countVertices);
            graph.SetName(GRAPHNAME);
            graph.FullGenerateVertices();
            graph.InitializeGraph();

            // Set random
            SetRandom();
        }
        #endregion
        
        #region Method
        private void SetRandom()
        {
            random = new Random();

            // Choose erdosRenyiModelProbabilityEnum
            if (erdosRenyiModelProbabilityEnum == ErdosRenyiModelProbabilityEnum.notAssigned)
            {
                erdosRenyiModelProbabilityEnum = (ErdosRenyiModelProbabilityEnum)random.Next(Enum.GetNames(typeof(ErdosRenyiModelProbabilityEnum)).Length - 1);
            }

            switch (erdosRenyiModelProbabilityEnum)
            {
                case ErdosRenyiModelProbabilityEnum.cDividedByNLessThanOne:
                    probability = random.Next(10000, 99999) / countVertices;
                    break;
                case ErdosRenyiModelProbabilityEnum.cDividedByNMoreThanOne:
                    probability = random.Next(100000, 300000) / countVertices;
                    break;
                case ErdosRenyiModelProbabilityEnum.cLogNDividedByN:
                    probability = (int)(random.Next(100000, 300000) * Math.Log(countVertices) / countVertices);
                    break;
                default:
                    throw new MyException.GenerateGraphException.ErdosReneiModelProbabilityEnumMissing(erdosRenyiModelProbabilityEnum.ToString());
            }

            if (erdosRenyiModelProbabilityEnum == ErdosRenyiModelProbabilityEnum.cLogNDividedByN)
                probability += (random.Next(0, 30000));
        }

        public Graph.IGraphInterface GenerateGraph()
        {
            // Variable
            int probabilityEdge;

            foreach (Graph.IVertexInterface firstVertex in graph.AllVertices())
            {
                foreach (Graph.IVertexInterface secondVertex in graph.AllVertices())
                {
                    // No duplicates
                    if (usedVerticesHashSet.Contains(secondVertex))
                        continue;

                    if (firstVertex == secondVertex)
                        continue;

                    probabilityEdge = random.Next(0, 100000);

                    if (probabilityEdge <= probability)
                        graph.EdgeAdd(new Graph.Edge(firstVertex, secondVertex));
                }

                usedVerticesHashSet.Add(firstVertex);
            }

            usedVerticesHashSet = null;
            return graph;
        }
        #endregion
        
        #region Property
        /// <summary>
        /// Return count of vertices 
        /// </summary>
        /// <returns>count of vertices</returns>
        public int GetCountVertices()
        {
            return countVertices;
        }

        private void SetCountVertices(int countVertices)
        {
            if (countVertices <= 0)
                throw new MyException.GraphException.GraphInvalidCountVerticesException();

            this.countVertices = countVertices;
        }

        /// <summary>
        /// Return probability attribute
        /// </summary>
        /// <returns>probability attribute</returns>
        public ErdosRenyiModelProbabilityEnum GetErdosRenyiModelProbabilityEnum()
        {
            return erdosRenyiModelProbabilityEnum;
        }
        #endregion
    }
}
