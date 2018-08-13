using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence
{
    class SmallestLastSequence : GraphColoringSequenceAlgorithm
    {
        // Constructor
        #region
        public SmallestLastSequence(Graph.Graph graph) : base(graph)
        { }
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
            Graph.Vertex vertex;
            List<Graph.Vertex> VertexList = new List<Graph.Vertex>();
            Graph.Graph copyGraph;

            copyGraph = Graph.GraphOperation.GraphOperation.CopyGraph(graph);
            while (copyGraph.GetRealCountVertices() != 0)
            {
                vertex = copyGraph.GetGraphProperty().GetDegreeSequenceVertex().First();
                VertexList.Add(graph.GetVertex(vertex.GetUserName()));
                copyGraph.VertexDelete(vertex);
            }

            vertexSequenceList = VertexList;
        }
        #endregion
    }
}
