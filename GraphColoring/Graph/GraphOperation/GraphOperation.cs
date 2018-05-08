using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            // TODO ComplementGraph

            return null;
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
        /// <param name="vertexList">vrcholy, které má graf podgraf obsahovat</param>
        /// <returns>podgraf</returns>
        public static Graph SubGraph(List<Vertex> vertexList)
        {
            // TODO SubGraph

            return null;
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

            graphCopy = new GraphEdgeList(graph.GetCountVertices());

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
