using System;
using System.Linq;

namespace GraphColoring.GraphColoringAlgorithm
{
    public abstract partial class GraphColoringAlgorithm : IGraphColoringAlgorithmInterface
    {
        public enum TimeComplexityEnum
        {
            linear,                 // O(V + E)
            quadratic,              // O(V^2)
            multiply,               // O(V * E)
            cubic,                  // O(V^3)
            quadraticPlusMultiply,  // O(V^2 + V * E)
            quartic,                // O(V^4)
            cubicPlusQuadratic,     // O(V^3 + V^2 * E)
            quintic,                // O(V^5)
            factorial,              // O(V!)
            undefined
        }

        /// <summary>
        /// Get enum order
        /// </summary>
        /// <param name="State">enum</param>
        /// <returns>order</returns>
        public static int GetOrder(TimeComplexityEnum state)
        {
            return Enum.GetValues(typeof(TimeComplexityEnum)).Cast<TimeComplexityEnum>().Select((x, i) => new { item = x, index = i }).Single(x => x.item == state).index;
        }
    }
}
