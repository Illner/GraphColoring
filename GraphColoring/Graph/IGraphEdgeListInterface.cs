using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring.Graph
{
    public interface IGraphEdgeListInterface : IGraphInterface
    {
        void AddEdge(string userNameVertex1, string userNameVertex2);
        void AddVertex(string userName);
    }
}
