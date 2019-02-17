using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring.Graph
{
    public interface IEdgeInterface
    {
        string ToString();
        IVertexInterface GetVertex1();
        IVertexInterface GetVertex2();
    }
}
