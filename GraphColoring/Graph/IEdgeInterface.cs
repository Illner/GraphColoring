using System;

namespace GraphColoring.Graph
{
    public interface IEdgeInterface
    {
        string ToString();
        IVertexInterface GetVertex1();
        IVertexInterface GetVertex2();
    }
}
