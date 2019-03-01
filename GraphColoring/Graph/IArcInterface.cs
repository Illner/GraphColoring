using System;

namespace GraphColoring.Graph
{
    public interface IArcInterface
    {
        IVertexInterface GetVertexFrom();
        IVertexInterface GetVertexTo();
    }
}
