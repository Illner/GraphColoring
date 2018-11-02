using System;

namespace GraphColoring.GraphColoringAlgorithm
{
    abstract partial class GraphColoringAlgorithm : IGraphColoringAlgorithmInterface
    {
        public enum GraphColoringAlgorithmEnum
        {
            optimal,
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
            geneticAlgortihm,
            probabilityAlgorithm,
            XXX // Test
        }
    }
}
