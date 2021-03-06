﻿using System;

namespace GraphColoring.GraphColoringAlgorithm
{
    public interface IGraphColoringAlgorithmInterface
    {
        void Color();
        Graph.IGraphInterface GetGraph();
        GraphColoringAlgorithm.TimeComplexityEnum GetTimeComplexity();
        double ColoringProgress();
        int GetCountInterchangeCalls();
    }
}
