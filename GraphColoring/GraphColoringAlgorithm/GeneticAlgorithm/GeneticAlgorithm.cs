using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring.GraphColoringAlgorithm.GeneticAlgorithm
{
    public class GeneticAlgorithm : GraphColoringAlgorithm
    {
        // Variable
        #region
        private List<List<Graph.IVertexInterface>> populationList;
        private List<List<Graph.IVertexInterface>> newPopulationList;
        private List<Tuple<List<Graph.IVertexInterface>, List<Graph.IVertexInterface>>> parentPopulationList;
        private List<double> fitnessFunctionPopulationCumulativeDistributionfunctionList;
        private int populationSize;
        private Random random;
        private int countOfIteration;
        private int stateSize;
        private int bestColorUsed = Int32.MaxValue;
        private List<Graph.IVertexInterface> bestState;
        #endregion

        // Constructor
        #region
        /// <summary>
        /// If populationSize is less than 1, throw exception AlgorithmGraphGeneticAlgorithmInvalidPopulationCount
        /// </summary>
        /// <param name="graph">Graph</param>
        /// <param name="populationSize">Size of population</param>
        public GeneticAlgorithm(Graph.IGraphInterface graph, int populationSize = 10) : base(graph)
        {
            if (populationSize < 1)
                throw new MyException.GraphColoringAlgorithmException.AlgorithmGraphGeneticAlgorithmInvalidPopulationCount();

            random = new Random();

            // Even property
            if (populationSize % 2 == 1)
                populationSize++;

            this.populationSize = populationSize;
            name = "Genetic algorithm";
        }
        #endregion

        // Method
        #region
        /// <summary>
        /// Color graph
        /// </summary>
        override
        public void Color()
        {
            // Variable
            double randomDouble;
            int indexFirstParent, indexSecondParent;
            
            stateSize = graph.GetRealCountVertices();
            countOfIteration = stateSize * stateSize;
            coloredGraph = graph.GetColoredGraph();

            CreatePopulation();

            for (int i = 0; i < countOfIteration; i++)
            {
                // new iteration
                FillFfitnessFunctionPopulationCumulativeDistributionfunctionList();
                newPopulationList = new List<List<Graph.IVertexInterface>>();
                parentPopulationList = new List<Tuple<List<Graph.IVertexInterface>, List<Graph.IVertexInterface>>>();

                // Set parents - parallel
                Parallel.For(0, populationSize / 2, index  => {
                    // First parent
                    randomDouble = random.NextDouble();
                    indexFirstParent = GetIndexFfitnessFunctionPopulationCumulativeDistributionfunctionList(randomDouble);

                    // Second parent
                    randomDouble = random.NextDouble();
                    indexSecondParent = GetIndexFfitnessFunctionPopulationCumulativeDistributionfunctionList(randomDouble);

                    lock (parentPopulationList)
                    {
                        parentPopulationList.Add(new Tuple<List<Graph.IVertexInterface>, List<Graph.IVertexInterface>>(populationList.ElementAt(indexFirstParent), populationList.ElementAt(indexSecondParent)));
                    }
                });

                // Core - parallel
                Parallel.ForEach(parentPopulationList, parents => 
                {
                    List<Graph.IVertexInterface> firstState, firstStateCrossOver;
                    List<Graph.IVertexInterface> secondState, secondStateCrossOver;

                    firstState = parents.Item1;
                    secondState = parents.Item2;

                    firstStateCrossOver = firstState.ToList();
                    secondStateCrossOver = secondState.ToList();

                    Crossover(ref firstStateCrossOver, ref secondStateCrossOver);

                    // Mutation
                    AdjacentSwapMutation(ref firstStateCrossOver);
                    RandomSwapMutation(ref firstStateCrossOver);
                    InvertedExchangeMutation(ref firstStateCrossOver);

                    AdjacentSwapMutation(ref secondStateCrossOver);
                    RandomSwapMutation(ref secondStateCrossOver);
                    InvertedExchangeMutation(ref secondStateCrossOver);

                    lock (newPopulationList)
                    {
                        newPopulationList.Add(firstStateCrossOver);
                        newPopulationList.Add(secondStateCrossOver);
                    }
                });

                populationList = newPopulationList;
            }

            coloredGraph.GreedyColoring(bestState);
            coloredGraph.InicializeColoredGraph();
        }

        private List<Graph.IVertexInterface> GetRandomVerticesList()
        {
            // Varibale
            List<Graph.IVertexInterface> state;

            state = graph.AllVertices();
            MyMath.MyMath.FisherYatesShuffle(state);
            
            return state;
        }

        private void CreatePopulation()
        {
            populationList = new List<List<Graph.IVertexInterface>>();

            for (int i = 0; i < populationSize; i++)
            {
                populationList.Add(GetRandomVerticesList());
            }
        }

        /// <summary>
        /// 1 / color number
        /// </summary>
        /// <param name="state">List of vertices</param>
        /// <returns>1 / color number</returns>
        private double FitnessFunction(List<Graph.IVertexInterface> state)
        {
            coloredGraph.ResetColors();
            coloredGraph.GreedyColoring(state);

            if (!coloredGraph.IsValidColored())
                throw new MyException.GraphColoringAlgorithmException.AlgorithmGraphIsNotColored("Genetic algorithm - fitness function");

            int usedColors = coloredGraph.GetCountUsedColors();

            if (bestColorUsed > usedColors)
            {
                bestColorUsed = usedColors;
                bestState = state;
            }

            return (double)1 / usedColors;
        }

        /// <summary>
        /// Fill fitnessFunctionPopulationCumulativeDistributionfunctionList (normalized)
        /// </summary>
        private void FillFfitnessFunctionPopulationCumulativeDistributionfunctionList()
        {
            // Variable
            double sum = 0;
            fitnessFunctionPopulationCumulativeDistributionfunctionList = new List<double>();

            foreach(List<Graph.IVertexInterface> state in populationList)
            {
                sum += FitnessFunction(state);
                fitnessFunctionPopulationCumulativeDistributionfunctionList.Add(sum);
            }

            // Normalizing
            for (int i = 0; i < fitnessFunctionPopulationCumulativeDistributionfunctionList.Count; i++)
            {
                double temp = fitnessFunctionPopulationCumulativeDistributionfunctionList[i];
                fitnessFunctionPopulationCumulativeDistributionfunctionList[i] = temp / sum;
            }
        }

        /// <summary>
        /// Time complexity: O(n), where n is size population
        /// </summary>
        private int GetIndexFfitnessFunctionPopulationCumulativeDistributionfunctionList(double number)
        {
            // Variable
            int index = 0;
            double sum = 0;

            while (number >= sum)
            {
                sum = fitnessFunctionPopulationCumulativeDistributionfunctionList.ElementAt(index);
                index++;
            }

            return --index;
        }

        private void Crossover(ref List<Graph.IVertexInterface> firstState, ref List<Graph.IVertexInterface> secondState)
        {
            // Variable
            int tempIndex;
            int crossover = random.Next(stateSize);
            Graph.IVertexInterface firstVertex, secondVertex;
            List<Graph.IVertexInterface> firstTemp, secondTemp;

            firstTemp = firstState.ToList();
            secondTemp = secondState.ToList();

            // First crossover
            for (int i = 0; i <= crossover; i++)
            {
                firstVertex = firstState.ElementAt(i);
                secondVertex = secondState.ElementAt(i);

                if (firstVertex == secondVertex)
                    continue;

                tempIndex = firstState.FindIndex(x => x == secondVertex);

                if (tempIndex > stateSize)
                    throw new MyException.GraphColoringAlgorithmException.AlgorithmGraphGeneticAlgorithmRandomNumberOutRange("First crossover");

                firstState[i] = secondVertex;
                firstState[tempIndex] = firstVertex;
            }
            
            secondTemp = firstState;
            firstState = firstTemp;

            // Second crossover
            for (int i = 0; i <= crossover; i++)
            {
                secondVertex = secondState.ElementAt(i);
                firstVertex = firstState.ElementAt(i);

                if (firstVertex == secondVertex)
                    continue;

                tempIndex = secondState.FindIndex(x => x == firstVertex);

                if (tempIndex > stateSize)
                    throw new MyException.GraphColoringAlgorithmException.AlgorithmGraphGeneticAlgorithmRandomNumberOutRange("Second crossover");

                secondState[i] = firstVertex;
                secondState[tempIndex] = secondVertex;
            }

            firstState = secondTemp;
        }

        /// <summary>
        /// Choose 2 random points in the encoding and swap between them.
        /// </summary>
        /// <param name="state">State</param>
	    private void RandomSwapMutation (ref List<Graph.IVertexInterface> state)
        {
            // Variable
            int firstPoint;
            int secondPoint;
            Graph.IVertexInterface vertex;

            if (state.Count == 1)
                return;

            firstPoint = random.Next(0, stateSize);
            secondPoint = random.Next(0, stateSize);

            vertex = state[firstPoint];
            state[firstPoint] = state[secondPoint];
            state[secondPoint] = vertex;
        }

        /// <summary>
        /// Choose 1 random point in the encoding and swap it with the geneto its right.
        /// </summary>
        /// <param name="state"></param>
	    private void AdjacentSwapMutation(ref List<Graph.IVertexInterface> state)
        {
            // Variable
            int point = 0;
            Graph.IVertexInterface vertex;

            if (state.Count == 1)
                return;

            point = random.Next(0, stateSize - 1);

            vertex = state[point];
            state[point] = state[point + 1];
            state[point + 1] = vertex;
        }

        /// <summary>
        /// Select 2 random points in the encoding and invert the order ofthe genes. 
        /// </summary>
        /// <param name="state">State</param>
	    private void InvertedExchangeMutation(ref List<Graph.IVertexInterface> state)
        {
            // Variable
            int firstPoint, secondPoint;

            if (state.Count == 1)
                return;

            firstPoint = random.Next(0, stateSize + 1);
            secondPoint = random.Next(0, stateSize + 1);
            
            // Guarantee firstPoint <= secondPoint
            if (firstPoint > secondPoint)
            {
                int temp = firstPoint;
                firstPoint = secondPoint;
                secondPoint = temp;
            }

            state.Reverse(firstPoint, secondPoint-firstPoint);
        }

        private void ReverseMutation(ref List<Graph.IVertexInterface> state)
        {
            state.Reverse();
        }
        #endregion

        // Property
        #region
        /// <summary>
        /// Return size of population
        /// </summary>
        /// <returns>size of population</returns>
        public int GetPopulationSize()
        {
            return populationSize;
        }
        #endregion
    }
}
