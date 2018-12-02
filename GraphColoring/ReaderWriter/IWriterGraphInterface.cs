using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring.ReaderWriter
{
    interface IWriterGraphInterface
    {
        bool WriteFile(Graph.IGraphInterface graph);
        bool WriteFileColor(Graph.IGraphInterface graph, GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum graphColoringAlgorithmEnum, bool isOptimal);
    }
}
