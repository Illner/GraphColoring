using System;

namespace GraphColoring.ReaderWriter
{
    public interface IWriterGraphInterface
    {
        bool WriteFile(Graph.IGraphInterface graph);
        bool WriteFileColor(Graph.IGraphInterface graph, GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum graphColoringAlgorithmEnum, bool isOptimal);
    }
}
