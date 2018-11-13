﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphColoring.Graph.GraphOperation
{
    static class GraphOperation
    {
        // Method
        #region
        /// <summary>
        /// Vrátí komplementární graf
        /// </summary>
        /// <param name="graph">graf, ze kterého chceme vytvořit komplementární graf</param>
        /// <returns>komplementární graf</returns>
        public static IGraphInterface ComplementGraph(IGraphInterface graph)
        {
            // Variable
            IGraphEdgeListInterface complementGraph;
            List<IVertexInterface> vertexList;
            List<IVertexInterface> neighboursList;
            List<IVertexInterface> intersectionVertexAndNeighboursList;

            complementGraph = new GraphEdgeList(graph.GetRealCountVertices());
            complementGraph.SetName("Complement graph - " + graph.GetName());
            vertexList = graph.AllVertices();
            
            // Add edges
            foreach(IVertexInterface vertex in vertexList)
            {
                neighboursList = graph.Neighbours(vertex);
                neighboursList.Add(vertex);

                intersectionVertexAndNeighboursList = vertexList.FindAll(v => !neighboursList.Contains(v)).ToList();
                
                if (intersectionVertexAndNeighboursList.Count == 0)
                {
                    complementGraph.AddVertex(vertex.GetUserName());
                    continue;
                }
                
                foreach (IVertexInterface neighbour in intersectionVertexAndNeighboursList)
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
        public static IGraphInterface LineGraph(IGraphInterface graph)
        {
            // Variable
            int idVertex1, idVertex2;
            string idNewVertex, userNameNewVertex;
            string userNameVertex1, userNameVertex2;
            IGraphEdgeListInterface lineGraph;
            List<IVertexInterface> vertexList;
            List<IVertexInterface> neighboursList;
            Dictionary<string, IVertexInterface> vertexMap;
            List<IVertexInterface> neighboursNewList;

            lineGraph = new GraphEdgeList(graph.GetGraphProperty().GetCountEdges());
            lineGraph.SetName("Line graph - " + graph.GetName());

            vertexMap = new Dictionary<string, IVertexInterface>();
            vertexList = graph.AllVertices();

            foreach (IVertexInterface vertex in vertexList)
            {
                idVertex1 = vertex.GetIdentifier();
                userNameVertex1 = vertex.GetUserName();
                neighboursList = graph.Neighbours(vertex);

                neighboursNewList = new List<IVertexInterface>();

                foreach (IVertexInterface neighbour in neighboursList)
                {
                    idVertex2 = neighbour.GetIdentifier();
                    userNameVertex2 = neighbour.GetUserName();

                    if (idVertex1 < idVertex2)
                    {
                        idNewVertex = idVertex1.ToString() + idVertex2.ToString();
                        userNameNewVertex = userNameVertex1 + userNameVertex2;
                    }
                    else
                    {
                        idNewVertex = idVertex2.ToString() + idVertex1.ToString();
                        userNameNewVertex = userNameVertex2 + userNameVertex1;
                    }

                    if (!vertexMap.TryGetValue(idNewVertex, out IVertexInterface newVertex))
                    {
                        newVertex = new Vertex(userNameNewVertex);
                        vertexMap.Add(idNewVertex, newVertex);
                    }

                    neighboursNewList.Add(newVertex);
                }
                
                if (neighboursList.Count == 1)
                {
                    lineGraph.AddVertex(neighboursNewList.First().GetUserName());
                    continue;
                }

                for (int i = 0; i < neighboursNewList.Count - 1; i++)
                {
                    for (int j = i + 1; j < neighboursNewList.Count; j++)
                    {
                        lineGraph.AddEdge(neighboursNewList[i].GetUserName(), neighboursNewList[j].GetUserName());
                    }
                }
            }

            lineGraph.InitializeGraph();

            return lineGraph;
        }

        /// <summary>
        /// Vrátí podgraf grafu
        /// </summary>
        /// <param name="graph">graf, ze kterého chceme vytvořit podgraf<</param>
        /// <param name="vertexList">vrcholy, které má graf podgraf obsahovat</param>
        /// <returns>podgraf</returns>
        public static IGraphInterface SubGraph(IGraphInterface graph, List<IVertexInterface> vertexList)
        {
            // Variable
            IGraphEdgeListInterface subGraph;
            List<IVertexInterface> neighboursList;

            subGraph = new GraphEdgeList(vertexList.Count);
            subGraph.SetName("Subgraph - " + graph.GetName());

            foreach(IVertexInterface vertex in vertexList)
            {
                neighboursList = graph.Neighbours(vertex).Intersect(vertexList).ToList();

                if (neighboursList.Count == 0)
                {
                    subGraph.AddVertex(vertex.GetUserName());
                    continue;
                }

                foreach(IVertexInterface neighbour in neighboursList)
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
        public static IGraphInterface CopyGraph(IGraphInterface graph)
        {
            if (!graph.GetIsInitialized())
                throw new MyException.GraphException.GraphInitializationException();

            // Variable
            IGraphEdgeListInterface graphCopy;
            List<IVertexInterface> neighboursVertexList;
            List<IVertexInterface> allVerticesList = graph.AllVertices();

            graphCopy = new GraphEdgeList(graph.GetGraphProperty().GetCountVertices());

            graphCopy.SetName(graph.GetName());

            foreach (IVertexInterface vertex1 in allVerticesList)
            {
                neighboursVertexList = graph.Neighbours(vertex1);

                if (neighboursVertexList.Count == 0)
                {
                    graphCopy.AddVertex(vertex1.GetUserName());
                    continue;
                }

                foreach (IVertexInterface vertex2 in neighboursVertexList)
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
