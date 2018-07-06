using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring.ColoredGraph
{
    class ColoredGraph : Graph.Graph
    {
        Dictionary<int, string> mapping2 = new Dictionary<int,object>();

        public ColoredGraph(int countVertices)
        {
            graphProperty = new Graph.GraphProperty.GraphProperty(this, countVertices);

            adjacencyList = new Dictionary<ColoredVertex, List<ColoredVertex>>();
            mapping = new Dictionary<int, ColoredVertex>();

            SetName("My graph");
            newLine = ReaderWriter.ReaderWriter.GetNewLine();
        }

        public Graph.Vertex getVertex()
        {
            return new ColoredVertex();
        }
    }
}
