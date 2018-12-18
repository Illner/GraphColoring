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
        private int color = 1;
        private Graph.IGraphInterface copyGraph;
        #endregion

        // Constructor
        #region
        public GreedyIndependentSet(Graph.IGraphInterface graph) : base(graph)
        {
            name = "Greedy independent set algorithm";
        }
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
            Graph.IVertexInterface vertex;
            List<Graph.IVertexInterface> neighboursVertexList;

            while (!graph.GetColoredGraph().AreAllVerticesColored())
            {
                copyGraph = Graph.GraphOperation.GraphOperation.SubGraph(graph, graph.GetColoredGraph().GetUnColoredVertexList());

                while (copyGraph.GetRealCountVertices() != 0)
                {
                    vertex = copyGraph.GetGraphProperty().GetVertexWithDegree(copyGraph.GetGraphProperty().GetMinimumVertexDegree());
                    neighboursVertexList = copyGraph.Neighbours(vertex);

                    copyGraph.VertexDelete(vertex);
                    foreach (Graph.IVertexInterface neighbour in neighboursVertexList)
                    {
                        copyGraph.VertexDelete(neighbour);
                    }

                    graph.GetColoredGraph().ColorVertex(graph.GetVertexByUserName(vertex.GetUserName()), color);
                }

                color++;
            }

            coloredGraph.InicializeColoredGraph();
        }
        #endregion
    }
}
