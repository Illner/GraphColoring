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
            // Graph.Tests.GraphTest graphTest = new Graph.Tests.GraphTest(Graph.Graph.GraphRepresentationEnum.adjacencyMatrix);
            // ReaderWriter.Tests.ReaderWriterTest readerWriterTest = new ReaderWriter.Tests.ReaderWriterTest(ReaderWriter.Tests.ReaderWriterTest.PathEnum.Valid);

            Console.WriteLine("---------------------");
            Console.WriteLine("----AdjacencyList----");
            Console.WriteLine("---------------------");
            ReaderWriter.Tests.ReaderTest readerTestAdjacencyList = new ReaderWriter.Tests.ReaderTest(ReaderWriter.Tests.ReaderTest.GraphEnum.AdjacencyList, ReaderWriter.Tests.ReaderTest.PathEnum.InvalidCoreData);
            
            Console.WriteLine("-----------------------");
            Console.WriteLine("----AdjacencyMatrix----");
            Console.WriteLine("-----------------------");
            ReaderWriter.Tests.ReaderTest readerTestAdjacencyMatrix = new ReaderWriter.Tests.ReaderTest(ReaderWriter.Tests.ReaderTest.GraphEnum.AdjacencyMatrix, ReaderWriter.Tests.ReaderTest.PathEnum.InvalidCoreData);

            Console.WriteLine("----------------");
            Console.WriteLine("----EdgeList----");
            Console.WriteLine("----------------");
            ReaderWriter.Tests.ReaderTest readerTestEdgeList = new ReaderWriter.Tests.ReaderTest(ReaderWriter.Tests.ReaderTest.GraphEnum.EdgeList, ReaderWriter.Tests.ReaderTest.PathEnum.InvalidCoreData);
        }
    }
}
