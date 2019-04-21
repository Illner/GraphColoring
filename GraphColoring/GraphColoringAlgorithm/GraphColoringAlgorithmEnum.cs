using System;

namespace GraphColoring.GraphColoringAlgorithm
{
    public abstract partial class GraphColoringAlgorithm : IGraphColoringAlgorithmInterface
    {
        public enum GraphColoringAlgorithmEnum
        {
            optimal,
            AI,
            connectedLargestFirst,
            connectedLargestFirstInterchange,
            connectedLargestFirstInterchangeExtended,
            connectedLargestFirstInterchangeExtendedK3,
            randomSequence,
            randomSequenceInterchange,
            randomSequenceInterchangeExtended,
            randomSequenceInterchangeExtendedK3,
            largestFirstSequence,
            largestFirstSequenceInterchange,
            largestFirstSequenceInterchangeExtended,
            largestFirstSequenceInterchangeExtendedK3,
            smallestLastSequence,
            smallestLastSequenceInterchange,
            smallestLastSequenceInterchangeExtended,
            smallestLastSequenceInterchangeExtendedK3,
            connectedSequential,
	        saturationLargestFirstSequence,
	        greedyIndependentSet,
            combinationAlgorithm,
            geneticAlgorithm,
            geneticAlgorithm2
        }
    }
}
