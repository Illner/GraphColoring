using System;

namespace GraphColoring.GraphColoringAlgorithm
{
    interface IGraphColoringAlgorithmInterface
    {
        void Color();
        Graph.IGraphInterface GetGraph();
    }
}
