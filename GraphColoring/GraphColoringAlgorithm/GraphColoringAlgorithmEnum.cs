using System;

namespace GraphColoring.GraphColoringAlgorithm
{
    public abstract partial class GraphColoringAlgorithm : IGraphColoringAlgorithmInterface
    {
        public enum GraphColoringAlgorithmEnum
        {
            optimal,
            AI,
            connectedLargestFirstInterchangeExtended,
            randomSequence,
            randomSequenceInterchange,
            randomSequenceInterchangeExtended,
            largestFirstSequence,
            largestFirstSequenceInterchange,
            largestFirstSequenceInterchangeExtended,
            smallestLastSequence,
            smallestLastSequenceInterchange,
            smallestLastSequenceInterchangeExtended,
            connectedSequential,
	        saturationLargestFirstSequence,
	        greedyIndependentSet,
            combinationAlgorithm,
            geneticAlgorithm,
            geneticAlgorithm2
        }
    }
}
