using System;
using System.Collections.Generic;

namespace GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.ConnectedSequential
{
    public sealed class ConnectedSequential : GraphColoringSequenceAlgorithm
    {
        #region Constructor
        public ConnectedSequential(Graph.IGraphInterface graph) : base(graph)
        {
            name = "Connected sequenctial algorithm";
            timeComplexity = TimeComplexityEnum.linear;
            interchangeEnum = GraphColoringAlgorithInterchangeEnum.none;
        }
        #endregion
        
        #region Method
        /// <summary>
        /// Create a sequence of vertices
        /// Time complexity: O(V + E) + O(V + E)
        /// </summary>
        override
        protected void CreateVertexSequence()
        {
            // Variable
            List<Graph.IEdgeInterface> spanningTreeBFS;
            List<Graph.IVertexInterface> VertexList = new List<Graph.IVertexInterface>();
            
            // Graph is not connected
            if (!graph.GetGraphProperty().GetIsConnected())
                throw new MyException.GraphException.GraphIsNotConnected(graph.ToString());

            if (graph.GetRealCountVertices() == 1)
            {
                VertexList.Add(graph.GetFirstVertex());
            }
            else
            {
                if (graph.GetRealCountVertices() > 1)
                {
                    spanningTreeBFS = graph.GetGraphProperty().GetSpanningTree();

                    foreach (Graph.IEdgeInterface edge in spanningTreeBFS)
                    {
                        if (!VertexList.Contains(edge.GetVertex1()))
                            VertexList.Add(edge.GetVertex1());

                        if (!VertexList.Contains(edge.GetVertex2()))
                            VertexList.Add(edge.GetVertex2());
                    }
                }
            }

            vertexSequenceList = VertexList;
        }
        #endregion
    }
}
