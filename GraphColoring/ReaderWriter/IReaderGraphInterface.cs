using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring.ReaderWriter
{
    interface IReaderGraphInterface
    {
        Graph.IGraphInterface ReadFile();
    }
}
