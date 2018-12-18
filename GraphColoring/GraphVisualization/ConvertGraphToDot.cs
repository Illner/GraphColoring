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
        private bool isSchedule;
        private List<Graph.IGraphInterface> graphList;
        private List<Graph.IVertexInterface> cutVerticesList;
        private List<Graph.IEdgeInterface> bridgesList;
        private List<Graph.IVertexInterface> verticesList;
        private const int MAXCOLORS = 15;
        private int maximumDegree;
        private int minimumDegree;
        private StringBuilder stringBuilder;
        
        private string fillColorDefault = "white";
        private string colorDefault = "black";
        private string colorBridges = "red";
        private string colorCutVertex = "red";
        private double widthBridges = 2.0;
        private double widthCutVertex = 2.0;
        private double widthDefault = 1.0;

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
        public ConvertGraphToDot(List<Graph.IGraphInterface> graphList, bool isSchedule = false)
        {
            this.graphList = graphList;

            this.isSchedule = isSchedule;
        }

        // Method
        public string Convert()
        {
            if (isSchedule)
                return ConvertSchedule();
            else
                return ConvertGraph();
        }

        // For standard graph visualization
        private string ConvertGraph()
        {
            stringBuilder = new StringBuilder();

            // Sceleton
            if (graphList.Count == 0)
                stringBuilder.AppendLine("graph");
            else
                stringBuilder.AppendLine("graph \"" + graphList[0].GetName() + "\"");
            stringBuilder.AppendLine("{");

            foreach (Graph.IGraphInterface graph in graphList)
            {
                if (graph.GetGraphProperty().GetCountVertices() == 0)
                    continue;

                // Vertices
                string text;
                int vertexDegree;
                bool useColor = false;

                if (graph.GetColoredGraph().GetIsInicializedColoredGraph())
                    useColor = graph.GetColoredGraph().GetCountUsedColors() < MAXCOLORS ? true : false;

                // Graph properties
                verticesList = graph.AllVertices();
                cutVerticesList = graph.GetGraphProperty().GetCutVertices();
                bridgesList = graph.GetGraphProperty().GetBridges();
                minimumDegree = graph.GetGraphProperty().GetMinimumVertexDegree();
                maximumDegree = graph.GetGraphProperty().GetMaximumVertexDegree();

                // Vertices
                stringBuilder.AppendLine("node[style = filled shape = circle fillcolor = " + fillColorDefault + " color = " + colorDefault + " penwidth = " + widthDefault + "]");
                foreach (Graph.IVertexInterface vertex in verticesList)
                {
                    text = "\"";
                    vertexDegree = graph.CountNeighbours(vertex);

                    text += vertex.GetUserName() + "\" [";

                    if (vertexDegree == minimumDegree)
                        text += "shape = doublecircle ";

                    if (vertexDegree == maximumDegree)
                        text += "shape = doubleoctagon ";

                    if (cutVerticesList.Contains(vertex))
                        text += "color = " + colorCutVertex + " , penwidth = " + widthCutVertex + " ";

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
                        stringBuilder.AppendLine("\"" + vertex.GetUserName() + "\" --  \"" + neighbour.GetUserName() + "\"");
                        //Console.WriteLine(bridgesList.Count);

                        if (bridgesList.Any(e => ((e.GetVertex1().Equals(vertex) && e.GetVertex2().Equals(neighbour)) ||
                                                  (e.GetVertex1().Equals(neighbour) && e.GetVertex2().Equals(vertex)))))
                            stringBuilder.AppendLine(" [color = " + colorBridges + ", penwidth = " + widthBridges + "]");
                        else
                            stringBuilder.AppendLine();
                    }

                    visitedVerticesHashSet.Add(vertex);
                }
            }
            stringBuilder.AppendLine("}");

            return stringBuilder.ToString();
        }

        // For schedule visualization
        private string ConvertSchedule()
        {
            stringBuilder = new StringBuilder();

            // Sceleton
            if (graphList.Count == 0)
                stringBuilder.AppendLine("graph");
            else
                stringBuilder.AppendLine("graph \"" + graphList[0].GetName() + "\"");
            stringBuilder.AppendLine("{");

            // Get max count of used colors
            int maxUsedColors = 0;
            foreach (Graph.IGraphInterface graph in graphList)
            {
                int usedColors = graph.GetColoredGraph().GetCountUsedColors();
                if (usedColors > maxUsedColors)
                    maxUsedColors = usedColors;
            }

            stringBuilder.AppendLine("node [shape=Mrecord style=filled];");

            // For all colors
            for (int i = 1; i <= maxUsedColors; i++)
            {
                bool first = true;
                stringBuilder.Append("Color" + i + " [label = \"{");

                // For all graphs
                foreach (Graph.IGraphInterface graph in graphList)
                {
                    if (graph.GetGraphProperty().GetCountVertices() == 0)
                        continue;

                    // For all vertices with the color i
                    foreach (Graph.IVertexInterface vertex in graph.GetColoredGraph().ColoredVertices(i))
                    {
                        if (first)
                        {
                            stringBuilder.Append(vertex.GetUserName());
                            first = false;
                        }
                        else
                            stringBuilder.Append(" | " + vertex.GetUserName());
                    }
                }

                stringBuilder.AppendLine("}\" fillcolor = " + colorsDictionary[i] + "];");
            }
            stringBuilder.AppendLine("}");

            return stringBuilder.ToString();
        }

        // Property
        #region
        public bool GetIsSchedule()
        {
            return isSchedule;
        }

        public void SetIsSchedule(bool isSchedule)
        {
            this.isSchedule = isSchedule;
        }
        #endregion
    }
}
