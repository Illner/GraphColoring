using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphColoring.Graph.GraphOperation
{
    class GraphOperation
    {
        // Method
        #region
        /// <summary>
        /// Vrátí komplementární graf
        /// </summary>
        /// <param name="graph">graf, ze kterého chceme vytvořit komplementární graf</param>
        /// <returns>komplementární graf</returns>
        public static Graph ComplementGraph(Graph graph)
        {
            // Variable
            GraphEdgeList complementGraph;
            List<Vertex> vertexList;
            List<Vertex> neighboursList;
            List<Vertex> intersectionVertexAndNeighboursList;

            complementGraph = new GraphEdgeList(graph.GetRealCountVertices());
            complementGraph.SetName("Complement graph - " + graph.GetName());
            vertexList = graph.AllVertices();
            
            // Add edges
            foreach(Vertex vertex in vertexList)
            {
                neighboursList = graph.Neighbours(vertex);
                neighboursList.Add(vertex);

                intersectionVertexAndNeighboursList = vertexList.FindAll(v => !neighboursList.Contains(v)).ToList();
                
                if (intersectionVertexAndNeighboursList.Count == 0)
                {
                    complementGraph.AddVertex(vertex.GetUserName());
                    continue;
                }
                
                foreach (Vertex neighbour in intersectionVertexAndNeighboursList)
                {
                    complementGraph.AddEdge(vertex.GetUserName(), neighbour.GetUserName());
                }
            }
            
            complementGraph.InitializeGraph();

            return complementGraph;
        }

        /// <summary>
        /// Vrátí line graf
        /// </summary>
        /// <param name="graph">graf, ze kterého chceme vytvořit line graf</param>
        /// <returns>line graf</returns>
        public static Graph LineGraph(Graph graph)
        {
            // TODO LineGraph

            return null;
        }

        /// <summary>
        /// Vrátí podgraf grafu
        /// </summary>
        /// <param name="graph">graf, ze kterého chceme vytvořit podgraf<</param>
        /// <param name="vertexList">vrcholy, které má graf podgraf obsahovat</param>
        /// <returns>podgraf</returns>
        public static Graph SubGraph(Graph graph, List<Vertex> vertexList)
        {
            // Variable
            GraphEdgeList subGraph;
            List<Vertex> neighboursList;

            subGraph = new GraphEdgeList(vertexList.Count);
            subGraph.SetName("Subgraph - " + graph.GetName());

            foreach(Vertex vertex in vertexList)
            {
                neighboursList = graph.Neighbours(vertex).Intersect(vertexList).ToList();

                if (neighboursList.Count == 0)
                {
                    subGraph.AddVertex(vertex.GetUserName());
                    continue;
                }

                foreach(Vertex neighbour in neighboursList)
                {
                    subGraph.AddEdge(vertex.GetUserName(), neighbour.GetUserName());
                }
            }

            subGraph.InitializeGraph();

            return subGraph;
        }

        /// <summary>
        /// Vytvoří kopii grafu
        /// Pokud graf není inicializovaný, vyvolá se vyjímka GraphInitializationException
        /// </summary>
        /// <param name="graph">graf, který chceme zkopírovat</param>
        /// <returns>kopie grafu</returns>
        public static Graph CopyGraph(Graph graph)
        {
            if (!graph.GetIsInitialized())
                throw new MyException.GraphInitializationException();

            // Variable
            GraphEdgeList graphCopy;
            List<Vertex> neighboursVertexList;
            List<Vertex> allVerticesList = graph.AllVertices();

            graphCopy = new GraphEdgeList(graph.GetGraphProperty().GetCountVertices());

            graphCopy.SetName(graph.GetName());

            foreach (Vertex vertex1 in allVerticesList)
            {
                neighboursVertexList = graph.Neighbours(vertex1);

                if (neighboursVertexList.Count == 0)
                {
                    graphCopy.AddVertex(vertex1.GetUserName());
                    continue;
                }

                foreach (Vertex vertex2 in neighboursVertexList)
                {
                    graphCopy.AddEdge(vertex1.GetUserName(), vertex2.GetUserName());
                }
            }

            graphCopy.InitializeGraph();

            return graphCopy;
        }
        #endregion
    }
}
