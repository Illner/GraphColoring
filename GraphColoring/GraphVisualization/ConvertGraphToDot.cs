using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace GraphColoring.GraphVisualization
{
    class ConvertGraphToDot : IConvertGraphToDotInterface
    {
        // Variable
        private List<Graph.IGraphInterface> graphList;
        private const int MAXCOLORS = 15;
        private StringBuilder stringBuilder;

        private bool isSchedule;
        private bool showSpanningTree;
        private bool showSimplicialVertex;
        private bool showCutVerticesAndBridges;
        private bool showMaximumAndMinimumDegreeVertices;

        private string fillColorDefault = "white";
        private string colorDefault = "black";
        private string colorBridges = "red";
        private string colorCutVertex = "red";
        private double widthBridges = 1.0;
        private double widthSpanningTree = 4.0;
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
        public ConvertGraphToDot(List<Graph.IGraphInterface> graphList, bool isSchedule, bool showSpanningTree, bool showSimplicialVertex,
            bool showCutVerticesAndBridges, bool showMaximumAndMinimumDegreeVertices)
        {
            this.graphList = graphList;

            this.isSchedule = isSchedule;
            this.showSpanningTree = showSpanningTree;
            this.showSimplicialVertex = showSimplicialVertex;
            this.showCutVerticesAndBridges = showCutVerticesAndBridges;
            this.showMaximumAndMinimumDegreeVertices = showMaximumAndMinimumDegreeVertices;
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
            // Variable
            int maximumDegree;
            int minimumDegree;
            List<Graph.IVertexInterface> cutVerticesList;
            List<Graph.IEdgeInterface> bridgesList;
            List<Graph.IVertexInterface> verticesList;
            List<Graph.IEdgeInterface> spanningTreeList;
            Graph.IVertexInterface simplicialVertex = null;

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

                if (graph.GetColoredGraph().GetIsInitializedColoredGraph())
                    useColor = graph.GetColoredGraph().GetCountUsedColors() < MAXCOLORS ? true : false;

                // Graph properties
                if (graph.GetGraphProperty().GetIsChordal())
                    simplicialVertex = graph.GetGraphProperty().GetPerfectEliminationOrdering().Last();
                verticesList = graph.AllVertices();
                cutVerticesList = graph.GetGraphProperty().GetCutVertices();
                bridgesList = graph.GetGraphProperty().GetBridges();
                spanningTreeList = graph.GetGraphProperty().GetSpanningTree();
                minimumDegree = graph.GetGraphProperty().GetMinimumVertexDegree();
                maximumDegree = graph.GetGraphProperty().GetMaximumVertexDegree();

                // Vertices
                stringBuilder.AppendLine("node[style = filled shape = circle fillcolor = " + fillColorDefault + " color = " + colorDefault + " penwidth = " + widthDefault + "]");
                foreach (Graph.IVertexInterface vertex in verticesList)
                {
                    text = "\"";
                    vertexDegree = graph.CountNeighbours(vertex);

                    text += vertex.GetUserName() + "\" [";

                    if (showMaximumAndMinimumDegreeVertices && vertexDegree == minimumDegree)
                        text += "shape = doublecircle ";

                    if (showMaximumAndMinimumDegreeVertices && vertexDegree == maximumDegree)
                        text += "shape = doubleoctagon ";

                    if (showSimplicialVertex && simplicialVertex == vertex)
                        text += "shape = square ";

                    if (showCutVerticesAndBridges && cutVerticesList.Contains(vertex))
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
                        // Neighbour has been written
                        if (visitedVerticesHashSet.Contains(neighbour))
                            continue;

                        // Neighbour has not been written
                        stringBuilder.AppendLine("\"" + vertex.GetUserName() + "\" --  \"" + neighbour.GetUserName() + "\"");

                        if (showCutVerticesAndBridges && bridgesList.Any(e => ((e.GetVertex1().Equals(vertex) && e.GetVertex2().Equals(neighbour)) ||
                                                                               (e.GetVertex1().Equals(neighbour) && e.GetVertex2().Equals(vertex)))))
                            stringBuilder.AppendLine(" [color = " + colorBridges + ", penwidth = " + widthBridges + "]");
                        else
                            stringBuilder.AppendLine();

                        if (showSpanningTree && spanningTreeList.Any(e => ((e.GetVertex1().Equals(vertex) && e.GetVertex2().Equals(neighbour)) ||
                                                       (e.GetVertex1().Equals(neighbour) && e.GetVertex2().Equals(vertex)))))
                            stringBuilder.AppendLine(" [penwidth = " + widthSpanningTree + "]");
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
