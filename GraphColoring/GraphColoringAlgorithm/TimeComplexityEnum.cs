using System;

namespace GraphColoring.GraphColoringAlgorithm
{
    public abstract partial class GraphColoringAlgorithm : IGraphColoringAlgorithmInterface
    {
        public enum TimeComplexityEnum
        {
            constant,               // O(c)
            logarithmic,
            linear,                 // O(n + m)
            quadratic,              // O(n^2 + m)
            multiply,               // O(nm)
            quadraticPlusMultiply,  // O(n^2 + nm)
            cubic,                  // O(n^3)
            cubicPlusQuadratic,     // O(n^3 + mn^2)
            quartic,                // O(n^4)
            exponential,
            factorial,              // O(n! + m)
            undefined
        }
    }
}
