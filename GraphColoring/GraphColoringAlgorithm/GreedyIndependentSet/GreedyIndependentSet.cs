using System;
using System.Collections.Generic;

namespace GraphColoring.GraphColoringAlgorithm.GreedyIndependentSet
{
    public sealed class GreedyIndependentSet : GraphColoringAlgorithm
    {
        #region Variable
        private int color = 1;
        private Graph.IGraphInterface copyGraph;
        #endregion
        
        #region Constructor
        public GreedyIndependentSet(Graph.IGraphInterface graph) : base(graph)
        {
            name = "Greedy independent set algorithm";
            timeComplexity = TimeComplexityEnum.undefined;
        }
        #endregion
        
        #region Method
        /// <summary>
        /// Color a graph
        /// </summary>
        override
        public void Color()
        {
            // Variable
            Graph.IVertexInterface vertex;
            List<Graph.IVertexInterface> neighboursVertexList;

            if (coloredGraph.GetIsInitializedColoredGraph())
                throw new MyException.GraphException.ColoredGraphAlreadyInitializedException();

            coloredGraph.ResetColors();

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

            bool isColored = coloredGraph.InitializeColoredGraph();

            if (!isColored)
                throw new MyException.GraphColoringAlgorithmException.AlgorithmGraphIsNotColored();
        }
        #endregion
    }
}
