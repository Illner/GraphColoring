using System;
using System.Collections.Generic;

namespace GraphColoring.GenerateGraph
{
    partial class ErdosRenyiModel : IGenerateGraphInterface
    {
        // Variable
        /// <summary>
        /// graph - generovaný graf
        /// countVertices - počet vrcholů vygenerovaného grafu
        /// erdosRenyiModelProbabilityEnum - typ pravděpodobnosti
        /// probability - pravděpodobnost vygenerování hrany mezi dvěma vrcholy (*100)
        /// </summary>
        #region
        private Graph.IGraphInterface graph;
        private int countVertices;
        private Random random;
        private ErdosRenyiModelProbabilityEnum erdosRenyiModelProbabilityEnum = ErdosRenyiModelProbabilityEnum.notAssigned;
        public int probability;
        private const string GRAPHNAME = "RandomGraph";
        private HashSet<Graph.Vertex> usedVerticesHashSet;
        #endregion

        // Constructor
        #region
        /// <summary>
        /// Nekladný počet vrcholů vyvolá vyjímku GraphInvalidCountVerticesException
        /// </summary>
        /// <param name="countVertices">počet vrcholů generovaného grafu</param>
        /// <param name="erdosRenyiModelProbabilityEnum">parametr u erdo-renyiho modelu - deafult = notAssigned</param>
        public ErdosRenyiModel(int countVertices, ErdosRenyiModelProbabilityEnum erdosRenyiModelProbabilityEnum = ErdosRenyiModelProbabilityEnum.notAssigned)
        {
            SetCountVertices(countVertices);
            this.erdosRenyiModelProbabilityEnum = erdosRenyiModelProbabilityEnum;
            usedVerticesHashSet = new HashSet<Graph.Vertex>();

            // Create graph
            graph = new Graph.GraphEdgeList(countVertices);
            graph.SetName(GRAPHNAME);
            graph.FullGenerateVertices();
            graph.InitializeGraph();

            // Set random
            SetRandom();
        }
        #endregion

        // Method
        #region
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
                    probability = random.Next(25000, 99999) / countVertices;
                    break;
                case ErdosRenyiModelProbabilityEnum.cDividedByNMoreThanOne:
                    probability = random.Next(100000, 200000) / countVertices;
                    break;
                case ErdosRenyiModelProbabilityEnum.cLogNDividedByN:
                    probability = (int)(random.Next(100000, 200000) * Math.Log(countVertices) / countVertices);
                    break;
                default:
                    throw new MyException.ErdosReneiModelProbabilityEnumMissing(erdosRenyiModelProbabilityEnum.ToString());
            }
        }

        public Graph.IGraphInterface GenerateGraph()
        {
            // Variable
            int probabilityEdge;

            foreach (Graph.Vertex firstVertex in graph.AllVertices())
            {
                foreach (Graph.Vertex secondVertex in graph.AllVertices())
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

        // Property
        #region
        /// <summary>
        /// Vrátí počet vrcholů generovaného grafu
        /// </summary>
        /// <returns>počet vrcholů generovaného grafu</returns>
        public int GetCountVertices()
        {
            return countVertices;
        }

        private void SetCountVertices(int countVertices)
        {
            if (countVertices <= 0)
                throw new MyException.GraphInvalidCountVerticesException();

            this.countVertices = countVertices;
        }

        /// <summary>
        /// Vráti parametr u erdo-renyiho modelu
        /// </summary>
        /// <returns>parametr u erdo-renyiho modelu</returns>
        public ErdosRenyiModelProbabilityEnum GetErdosRenyiModelProbabilityEnum()
        {
            return erdosRenyiModelProbabilityEnum;
        }
        #endregion
    }
}
