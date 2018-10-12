using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring.GraphColoringAlgorithm
{
    interface IGraphColoringAlgorithmStepInterface
    {
        Graph.Vertex Step();
    }
}
