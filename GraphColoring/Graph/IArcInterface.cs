using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring.Graph
{
    interface IArcInterface
    {
        IVertexInterface GetVertexFrom();
        IVertexInterface GetVertexTo();
    }
}
