using System;

namespace GraphColoring.GraphColoringAlgorithm
{
    public abstract partial class GraphColoringAlgorithm : IGraphColoringAlgorithmInterface
    {
        public enum GraphColoringAlgorithmEnum
        {
            optimal,
            AI,
            illnerAlgorithm,
            randomSequence,
	        largestFirstSequence,
	        smallestLastSequence,
	        randomSequenceInterchange,
	        largestFirstSequenceInterchange,
	        smallestLastSequenceInterchange,
	        connectedSequential,
	        saturationLargestFirstSequence,
	        greedyIndependentSet,
            combinationAlgorithm,
            geneticAlgorithm
        }
    }
}
