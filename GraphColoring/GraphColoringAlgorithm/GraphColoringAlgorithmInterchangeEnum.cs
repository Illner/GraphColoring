using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring.GraphColoringAlgorithm
{
    public abstract partial class GraphColoringAlgorithm : IGraphColoringAlgorithmInterface
    {
        public enum GraphColoringAlgorithInterchangeEnum
        {
            none,
            interchange,
            interchangeExtended,
            interchangeExtendedK3
        }
    }
}
