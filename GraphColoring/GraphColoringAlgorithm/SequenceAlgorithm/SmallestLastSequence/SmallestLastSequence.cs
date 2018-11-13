﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring.GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence
{
    class SmallestLastSequence : GraphColoringSequenceAlgorithm, IGraphColoringAlgorithmStepInterface
    {
        // Constructor
        #region
        public SmallestLastSequence(Graph.IGraphInterface graph) : base(graph)
        {
            name = "Smallest last sequence algorithm";
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
            Graph.IVertexInterface vertex;
            List<Graph.IVertexInterface> VertexList = new List<Graph.IVertexInterface>();
            Graph.IGraphInterface copyGraph;

            copyGraph = Graph.GraphOperation.GraphOperation.CopyGraph(graph);
            while (copyGraph.GetRealCountVertices() != 0)
            {
                vertex = copyGraph.GetGraphProperty().GetDegreeSequenceVertex(true).First();
                VertexList.Add(graph.GetVertex(vertex.GetUserName()));
                copyGraph.VertexDelete(vertex);
            }

            vertexSequenceList = VertexList;
        }

        /// <summary>
        /// Vrátí vrchol s nejmenším stupňem v podgrafu s neobarvenými vrcholy
        /// Pokud je graf obarvený, tak vrátí null
        /// </summary>
        /// <returns>vrchol</returns>
        public Graph.IVertexInterface Step()
        {
            // Variable
            Graph.IVertexInterface copyVertex;
            Graph.IGraphInterface copyGraph;
            Graph.IColoredGraphInterface coloredCopyGraph;

            copyGraph = Graph.GraphOperation.GraphOperation.CopyGraph(graph);
            coloredCopyGraph = copyGraph.GetColoredGraph();

            // Duplicate colors from graph to copyGraph
            foreach (Graph.IVertexInterface vertex in graph.AllVertices())
            {
                if (vertex.GetColor() != Graph.VertexExtended.GetDefaultColor())
                    coloredCopyGraph.ColorVertex(copyGraph.GetVertex(vertex.GetUserName()), vertex.GetColor());
            }

            foreach (Graph.IVertexInterface vertex in copyGraph.GetColoredGraph().GetColoredVertexList())
            {
                copyGraph.VertexDelete(vertex);
            }

            copyVertex = copyGraph.GetGraphProperty().GetDegreeSequenceVertex(true).FirstOrDefault();

            if (copyVertex == null)
                return null;

            return graph.GetVertex(copyVertex.GetUserName());
        }
        #endregion
    }
}
