using System;

namespace GraphColoring.GraphColoringAlgorithm
{
    interface IGraphColoringAlgorithmStepInterface
    {
        Graph.IVertexInterface Step();
    }
}
