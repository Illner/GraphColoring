using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

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
        private int exponentCountOfIteration;
        #endregion

        // Constructor
        #region
        /// <summary>
        /// If the populationSize is less than 1, throw exception AlgorithmGraphGeneticAlgorithmInvalidPopulationCount
        /// If the exponentCountOfIteration is less than 1 or greater than 10 throws AlgorithmGraphGeneticAlgorithmInvalidExponentCountOfIteration
        /// </summary>
        /// <param name="graph">Graph</param>
        /// <param name="populationSize">Size of population</param>
        /// <param name="exponentCountOfIteration">Exponent of count of iteration = (populationSize)^(exponentCountOfIteration)</param>
        public GeneticAlgorithm(Graph.IGraphInterface graph, int exponentCountOfIteration, int populationSize = 10) : base(graph)
        {
            if (populationSize < 1)
                throw new MyException.GraphColoringAlgorithmException.AlgorithmGraphGeneticAlgorithmInvalidPopulationCount();

            if (exponentCountOfIteration < 1 || exponentCountOfIteration > 10)
                throw new MyException.GraphColoringAlgorithmException.AlgorithmGraphGeneticAlgorithmInvalidExponentCountOfIteration(exponentCountOfIteration.ToString());

            random = new Random();

            // Even property
            if (populationSize % 2 == 1)
                populationSize++;

            this.populationSize = populationSize;
            this.exponentCountOfIteration = exponentCountOfIteration;
            name = "Genetic algorithm (exponent: " + exponentCountOfIteration + ")";
            //timeComplexity = TimeComplexityEnum.cubicPlusQuadratic;
        }
        #endregion

        // Method
        #region
        /// <summary>
        /// Color a graph
        /// Time complexity: O(k * p * (p * n + m)) + 0
        /// </summary>
        override
        public void Color()
        {
            // Variable
            double randomDouble;
            int indexFirstParent, indexSecondParent;
            
            stateSize = graph.GetRealCountVertices();
            countOfIteration = (int)Math.Pow(stateSize, exponentCountOfIteration);
            coloredGraph = graph.GetColoredGraph();

            coloredGraph.ResetColors();

            CreatePopulation();

            for (int i = 0; i < countOfIteration; i++)
            {
                // new iteration
                FillFfitnessFunctionPopulationCumulativeDistributionfunctionList();
                newPopulationList = new List<List<Graph.IVertexInterface>>();
                parentPopulationList = new List<Tuple<List<Graph.IVertexInterface>, List<Graph.IVertexInterface>>>();

                // Select parents - parallel
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
            bool isColored = coloredGraph.InitializeColoredGraph();

            if (!isColored)
                throw new MyException.GraphColoringAlgorithmException.AlgorithmGraphIsNotColored();
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
        /// <param name="state">state (list of vertices)</param>
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
        /// Time complexity: O(n), where n is the size of population
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
        /// <param name="state">state</param>
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
        /// <param name="state">state</param>/param>
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
        /// Select 2 random points in the encoding and invert the order of the genes. 
        /// </summary>
        /// <param name="state">state</param>
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
        /// Return a size of population
        /// </summary>
        /// <returns>size of population</returns>
        public int GetPopulationSize()
        {
            return populationSize;
        }
        #endregion
    }
}
