using System;
using System.Linq;
using System.Collections.Generic;

namespace GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence
{
    public class SmallestLastSequence : GraphColoringSequenceAlgorithm
    {
        // Constructor
        #region
        public SmallestLastSequence(Graph.IGraphInterface graph, bool interchange = false) : base(graph)
        {
            name = "Smallest last sequence algorithm";
            timeComplexity = TimeComplexityEnum.quadraticPlusMultiply;

            // Interchange
            this.interchange = interchange;
            if (interchange)
            {
                name = "Smallest last sequence interchange algorithm";
                timeComplexity = TimeComplexityEnum.quadraticPlusMultiply;
            }
        }
        #endregion

        // Method
        #region
        /// <summary>
        /// Create a sequence of vertices
        /// Time complexity: O(n^2 + nm) + 0
        /// </summary>
        override
        protected void CreateVertexSequence()
        {
            // Variable
            Graph.IVertexInterface vertex = null;
            List<Graph.IVertexInterface> VertexList = new List<Graph.IVertexInterface>();
            Graph.IGraphInterface copyGraph;

            copyGraph = Graph.GraphOperation.GraphOperation.CopyGraph(graph);
            while (copyGraph.GetRealCountVertices() != 0)
            {
                // Because delete
                int minDegree = int.MaxValue;
                foreach(Graph.IVertexInterface myVertex in copyGraph.GetGraphProperty().GetDegreeSequenceVertex(false))
                {
                    if (minDegree > copyGraph.CountNeighbours(myVertex))
                    {
                        vertex = myVertex;
                        minDegree = copyGraph.CountNeighbours(myVertex);
                    }
                }
                VertexList.Add(graph.GetVertexByUserName(vertex.GetUserName()));
                copyGraph.VertexDelete(vertex);
            }

            VertexList.Reverse();

            vertexSequenceList = VertexList;
        }
        #endregion
    }
}
