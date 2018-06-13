using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring
{
    class Program
    {
        static void Main(string[] args)
        {
             Tests.Tests tests = new Tests.Tests(false);
             tests.Test();

            //Tests.Tests tests = new Tests.Tests(true);
            //tests.Test(Tests.Tests.TestEnum.graphSpanningTree);
        }
    }
}
