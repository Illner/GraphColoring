using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring.Graph
{
    public interface IArcInterface
    {
        IVertexInterface GetVertexFrom();
        IVertexInterface GetVertexTo();
    }
}
