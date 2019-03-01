using System;

namespace GraphColoring.Graph
{
    public interface IGraphEdgeListInterface : IGraphInterface
    {
        void AddEdge(string userNameVertex1, string userNameVertex2);
        void AddVertex(string userName);
    }
}
