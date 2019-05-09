using System;
using System.Linq;
using System.Collections.Generic;

namespace GraphColoring.GraphColoringAlgorithm.Optimal
{
    public sealed class Optimal : GraphColoringAlgorithm
    {
        #region Variable
        private List<Graph.IVertexInterface> optimalVertexList;
        private int optimalCountColors = int.MaxValue;
        #endregion
        
        #region Constructor
        public Optimal(Graph.IGraphInterface graph) : base(graph)
        {
            name = "Optimal algorithm";
            timeComplexity = TimeComplexityEnum.factorial;
        }
        #endregion
        
        #region Method
        /// <summary>
        /// Color a graph
        /// Time complexity: O(n! + m)
        /// Time complexity - tree, cycle, complete graph, bipartite graph: O(n + m)
        /// Time complexity - complete bipartite graph: O(n^2 + nm)
        /// </summary>
        override
        public void Color()
        {
            // Variable
            int countUsedColors;
            Graph.IColoredGraphInterface coloredGraph = graph.GetColoredGraph();
            optimalVertexList = new List<Graph.IVertexInterface>();
            
            coloredGraph.ResetColors();

            // Try optimal color
            if (TryOptimalColorInPolynomalTime(graph))
            {
                return;
            }

            // The graph can't be optimal colored => try all permutations
            else
            {
                foreach (var vertexList in MyMath.MyMath.GeneratePermutations(graph.AllVertices()))
                {
                    coloredGraph.GreedyColoring(vertexList.ToList());
                    coloredGraph.InitializeColoredGraph();
                    countUsedColors = coloredGraph.GetCountUsedColors();

                    if (optimalCountColors > countUsedColors)
                    {
                        optimalCountColors = countUsedColors;
                        optimalVertexList = vertexList.ToList();
                    }

                    coloredGraph.DeinitializationColoredGraph();
                }
            }

            coloredGraph.GreedyColoring(optimalVertexList);
            bool isColored = coloredGraph.InitializeColoredGraph();

            if (!isColored)
                throw new MyException.GraphColoringAlgorithmException.AlgorithmGraphIsNotColored();
        }

        /// <summary>
        /// Return true if a graph has been colored in polynomal time, otherwise false
        /// </summary>
        /// <param name="graph">graph</param>
        /// <returns>true if graph has been colored in polynomal time, otherwise false</returns>
        public static bool TryOptimalColorInPolynomalTime(Graph.IGraphInterface graph)
        {
            bool isColored;
            IGraphColoringAlgorithmInterface algorithm;
            List<Graph.IVertexInterface> optimalVertexList;
            Graph.IColoredGraphInterface coloredGraph = graph.GetColoredGraph();

            coloredGraph.ResetColors();

            // If the graph is chordal => use PEO for coloring
            if (graph.GetGraphProperty().GetIsChordal())
            {
                optimalVertexList = graph.GetGraphProperty().GetPerfectEliminationOrdering();

                coloredGraph.GreedyColoring(optimalVertexList);
                isColored = coloredGraph.InitializeColoredGraph();

                if (!isColored)
                    throw new MyException.GraphColoringAlgorithmException.AlgorithmGraphIsNotColored();

                return true;
            }

            switch (graph.GetGraphProperty().GetGraphClass())
            {
                case Graph.GraphClass.GraphClass.GraphClassEnum.bipartiteGraph:
                    Tuple<List<Graph.IVertexInterface>, List<Graph.IVertexInterface>> partites = graph.GetGraphProperty().GetPartites();

                    coloredGraph.GreedyColoring(partites.Item1.Concat(partites.Item2).ToList());
                    isColored = coloredGraph.InitializeColoredGraph();

                    if (!isColored)
                        throw new MyException.GraphColoringAlgorithmException.AlgorithmGraphIsNotColored();

                    return true;
                case Graph.GraphClass.GraphClass.GraphClassEnum.completeBipartiteGraph:
                    algorithm = new SequenceAlgorithm.SmallestLastSequence.SmallestLastSequence(graph);

                    algorithm.Color();

                    return true;
                case Graph.GraphClass.GraphClass.GraphClassEnum.completeGraph:
                    coloredGraph.GreedyColoring(graph.AllVertices());
                    isColored = coloredGraph.InitializeColoredGraph();

                    if (!isColored)
                        throw new MyException.GraphColoringAlgorithmException.AlgorithmGraphIsNotColored();

                    return true;
                case Graph.GraphClass.GraphClass.GraphClassEnum.cycleGraph:
                case Graph.GraphClass.GraphClass.GraphClassEnum.treeGraph:
                    algorithm = new SequenceAlgorithm.ConnectedSequential.ConnectedSequential(graph);

                    algorithm.Color();

                    return true;
            }

            return false;
        }
        #endregion
    }
}
