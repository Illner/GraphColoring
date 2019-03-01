using System;

namespace GraphColoring.ReaderWriter
{
    public interface IReaderGraphInterface
    {
        Graph.IGraphInterface ReadFile();
    }
}
