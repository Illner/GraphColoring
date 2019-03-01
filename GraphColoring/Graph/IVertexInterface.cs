using System;

namespace GraphColoring.Graph
{
    public interface IVertexInterface
    {
        bool Equals(IVertexInterface vertex);
        string ToString();
        int GetColor();
        int GetIdentifier();
        string GetUserName();
    }
}
