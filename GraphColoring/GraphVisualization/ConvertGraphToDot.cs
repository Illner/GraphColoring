using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GraphColoring.GraphVisualization
{
    class ConvertGraphToDot : IConvertGraphToDotInterface
    {
        // Variable
        private List<Graph.IGraphInterface> graphList;
        private List<Graph.IVertexInterface> cutVerticesList;
        private List<Graph.IEdgeInterface> bridgesList;
        private List<Graph.IVertexInterface> verticesList;
        private const int MAXCOLORS = 15;
        private int maximumDegree;
        private int minimumDegree;
        private StringBuilder stringBuilder;
        
        private string fillColorDefault = "white";
        private string colorBridges = "blue";

        // Initialize Colors
        private static Dictionary<int, String> colorsDictionary = new Dictionary<int, string>()
        {
            {1, "blue" },
            {2, "chartreuse" },
            {3, "cadetblue" },
            {4, "coral" },
            {5, "cyan" },
            {6, "darkorchid" },
            {7, "deeppink" },
            {8, "firebrick" },
            {9, "gold" },
            {10, "green" },
            {11, "brown" },
            {12, "yellow" },
            {13, "aquamarine" },
            {14, "darkslategray" },
            {15, "white" }
        };
        
        // Constructor
        public ConvertGraphToDot(List<Graph.IGraphInterface> graphList)
        {
            this.graphList = graphList;
        }

        // Method
        public string Convert()
        {
            stringBuilder = new StringBuilder();
            
            // Sceleton
            if (graphList.Count == 0)
                stringBuilder.AppendLine("graph");
            else
                stringBuilder.AppendLine("graph " + graphList[0].GetName());
            stringBuilder.AppendLine("{");

            foreach (Graph.IGraphInterface graph in graphList)
            {
                // Vertices
                string text;
                int vertexDegree;
                bool useColor = graph.GetColoredGraph().GetCountUsedColors() < MAXCOLORS ? true : false;

                // Graph properties
                verticesList = graph.AllVertices();
                cutVerticesList = graph.GetGraphProperty().GetCutVertices();
                bridgesList = graph.GetGraphProperty().GetBridges();
                minimumDegree = graph.GetGraphProperty().GetMinimumVertexDegree();
                maximumDegree = graph.GetGraphProperty().GetMaximumVertexDegree();

                stringBuilder.AppendLine("node[style = filled shape = circle fillcolor = " + fillColorDefault + "]");
                foreach (Graph.IVertexInterface vertex in verticesList)
                {
                    text = "";
                    vertexDegree = graph.CountNeighbours(vertex);

                    text += vertex.GetUserName() + " [";
                    // Minimum vertex
                    if (vertexDegree == minimumDegree)
                        text += "shape = doublecircle ";

                    if (vertexDegree == maximumDegree)
                        text += "shape = doubleoctagon ";

                    if (cutVerticesList.Contains(vertex))
                        text += "shape = square ";
                    
                    if (useColor && vertex.GetColor() != Graph.VertexExtended.GetDefaultColor())
                        text += "fillcolor = " + colorsDictionary[vertex.GetColor()] + " ";

                    text += "]";

                    stringBuilder.AppendLine(text);
                }

                // Edges
                HashSet<Graph.IVertexInterface> visitedVerticesHashSet = new HashSet<Graph.IVertexInterface>();

                foreach (Graph.IVertexInterface vertex in verticesList)
                {
                    List<Graph.IVertexInterface> neighboursList = graph.Neighbours(vertex);

                    foreach (Graph.IVertexInterface neighbour in neighboursList)
                    {
                        // Neighbour has been writed
                        if (visitedVerticesHashSet.Contains(neighbour))
                            continue;

                        // Neighbour has not been writed
                        stringBuilder.AppendLine(vertex.GetUserName() + " --  " + neighbour.GetUserName());
                        //Console.WriteLine(bridgesList.Count);

                        if (bridgesList.Any(e => ((e.GetVertex1().Equals(vertex) && e.GetVertex2().Equals(neighbour)) ||
                                                  (e.GetVertex1().Equals(neighbour) && e.GetVertex2().Equals(vertex)))))
                            stringBuilder.AppendLine(" [color = " + colorBridges + "]");
                        else
                            stringBuilder.AppendLine();
                    }

                    visitedVerticesHashSet.Add(vertex);
                }
            }
            stringBuilder.AppendLine("}");

            return stringBuilder.ToString();
        }
    }
}
