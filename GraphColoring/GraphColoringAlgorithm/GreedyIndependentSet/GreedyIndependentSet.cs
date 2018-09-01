using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring.GraphColoringAlgorithm.GreedyIndependentSet
{
    sealed class GreedyIndependentSet : GraphColoringAlgorithm
    {
        // Variable
        #region
        int color = 1;
        Graph.Graph copyGraph;
        #endregion

        // Constructor
        #region
        public GreedyIndependentSet(Graph.Graph graph) : base(graph)
        { }
        #endregion

        // Method
        #region
        /// <summary>
        /// Obarví daný graf
        /// </summary>
        override
        public void Color()
        {
            // Variable
            Graph.Vertex vertex;
            List<Graph.Vertex> neighboursVertexList;

            while (!graph.GetColoredGraph().AreAllVerticesColored())
            {
                copyGraph = Graph.GraphOperation.GraphOperation.SubGraph(graph, graph.GetColoredGraph().GetUnColoredVertexList());

                while (copyGraph.GetRealCountVertices() != 0)
                {
                    vertex = copyGraph.GetGraphProperty().GetVertexWithDegree(copyGraph.GetGraphProperty().GetMinimumVertexDegree());
                    neighboursVertexList = copyGraph.Neighbours(vertex);

                    copyGraph.VertexDelete(vertex);
                    foreach (Graph.Vertex neighbour in neighboursVertexList)
                    {
                        copyGraph.VertexDelete(neighbour);
                    }
                    
                    graph.GetColoredGraph().ColorVertex(graph.GetVertex(vertex.GetUserName()), color);
                }

                color++;
            }
        }
        #endregion
    }
}
