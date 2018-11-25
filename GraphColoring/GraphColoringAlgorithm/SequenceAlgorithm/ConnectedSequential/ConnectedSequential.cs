using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.ConnectedSequential
{
    sealed class ConnectedSequential : GraphColoringSequenceAlgorithm
    {
        // Constructor
        #region
        public ConnectedSequential(Graph.IGraphInterface graph) : base(graph)
        {
            name = "Connected sequenctial algorithm";
            interchange = false;
        }
        #endregion

        // Method
        #region
        /// <summary>
        /// Vytvoří posloupnost vrcholů
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
