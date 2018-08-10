using System;

namespace GraphColoring.GraphColoringAlgorithm
{
    abstract partial class GraphColoringAlgorithm : IGraphColoringAlgorithmInterface
    {
        public enum GraphColoringAlgorithmEnum
        {
            Optimal,
	        RandomSequence,
	        LargestFirstSequence,
	        SmallestLastSequence,
	        RandomSequenceInterchange,
	        LargestFirstSequenceInterchange,
	        SmallestLastSequenceInterchange,
	        ConnectedSequential,
	        SaturationLargestFirstSequence,
	        GreedyIndependentSet,
            XXX // Test
        }
    }
}
