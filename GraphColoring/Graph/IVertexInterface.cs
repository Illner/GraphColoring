using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring.Graph
{
    interface IVertexInterface
    {
        bool Equals(IVertexInterface vertex);
        string ToString();
        int GetColor();
        int GetIdentifier();
        string GetUserName();
    }
}
