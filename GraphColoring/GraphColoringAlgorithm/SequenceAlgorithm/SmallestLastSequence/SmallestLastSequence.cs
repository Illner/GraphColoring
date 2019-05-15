using System;
using System.Linq;
using System.Collections.Generic;

namespace GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence
{
    public class SmallestLastSequence : GraphColoringSequenceAlgorithm
    {
        #region Constructor
        public SmallestLastSequence(Graph.IGraphInterface graph, GraphColoringAlgorithInterchangeEnum interchangeEnum = GraphColoringAlgorithInterchangeEnum.none) : base(graph)
        {
            // Interchange
            this.interchangeEnum = interchangeEnum;

            switch (interchangeEnum)
            {
                case GraphColoringAlgorithInterchangeEnum.none:
                    name = "Smallest last sequence algorithm";
                    timeComplexity = TimeComplexityEnum.quadraticPlusMultiply;
                    break;
                case GraphColoringAlgorithInterchangeEnum.interchange:
                    name = "Smallest last sequence interchange algorithm";
                    timeComplexity = TimeComplexityEnum.cubic;
                    break;
                case GraphColoringAlgorithInterchangeEnum.interchangeExtended:
                    name = "Smallest last sequence interchange extended algorithm";
                    timeComplexity = TimeComplexityEnum.quartic;
                    break;
                case GraphColoringAlgorithInterchangeEnum.interchangeExtendedK3:
                    name = "Smallest last sequence interchange extended with K3 algorithm";
                    timeComplexity = TimeComplexityEnum.quintic;
                    break;
            }
        }
        #endregion
        
        #region Method
        /// <summary>
        /// Create a sequence of vertices
        /// Time complexity: O(V^2 + V * E) + 0
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
                int minDegree = int.MaxValue;
                foreach (Graph.IVertexInterface myVertex in copyGraph.GetGraphProperty().GetDegreeSequenceVertex(false))
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
