using System;
using System.Collections.Generic;

namespace GraphColoring.Graph.GraphClass
{
    public static partial class GraphClass
    {
        // Method
        #region
        public static GraphClassEnum GetGraphClass(IGraphInterface graph)
        {
            if (IsCompleteGraph(graph))
                return GraphClassEnum.completeGraph;

            if (IsTreeGraph(graph))
                return GraphClassEnum.treeGraph;

            if (IsCycleGraph(graph))
                return GraphClassEnum.cycleGraph;

            Tuple<bool, bool> bipartiteResult = IsBipartiteGraph(graph);

            if (bipartiteResult.Item1)
            {
                if (bipartiteResult.Item2)
                    return GraphClassEnum.completeBipartiteGraph;

                return GraphClassEnum.bipartiteGraph;
            }

            return GraphClassEnum.none;
        }

        /// <summary>
        /// Return true if the graph is complete graph
        /// Time complexity: O(1)
        /// </summary>
        /// <param name="graph">graph</param>
        /// <returns>true if the graph is complete graph, otherwise false</returns>
        public static bool IsCompleteGraph(IGraphInterface graph)
        {
            // |E| = (|V|)C(2)
            if (graph.GetGraphProperty().GetCountEdges() == MyMath.MyMath.nCr(graph.GetGraphProperty().GetCountVertices(), 2))
                return true;

            return false;
        }

        /// <summary>
        /// Return true if the graph is tree
        /// Time complexity: O(V + E)
        /// </summary>
        /// <param name="graph">graph</param>
        /// <returns>true if the graph is tree, otherwise false</returns>
        public static bool IsTreeGraph(IGraphInterface graph)
        {
            // Graph is connected and |E| = |V| - 1 // Euler's formula
            if ((graph.GetGraphProperty().GetIsConnected()) &&
                (graph.GetGraphProperty().GetCountEdges() == graph.GetGraphProperty().GetCountVertices() - 1))
                return true;

            return false;
        }

        /// <summary>
        /// Return true if the graph is cycle
        /// Time complexity: O(V + E)
        /// </summary>
        /// <param name="graph">graph</param>
        /// <returns>true if the graph is cycle, otherwise false</returns>
        public static bool IsCycleGraph(IGraphInterface graph)
        {
            // Graph is connected and 2-regular
            if ((graph.GetGraphProperty().GetIsConnected()) &&
                (graph.GetGraphProperty().GetIsRegular()) &&
                (graph.GetGraphProperty().GetMaximumVertexDegree() == 2))
                return true;

            return false;
        }

        /// <summary>
        /// Return true if the graph is (complete) bipartite
        /// Time complexity: O(V + E)
        /// </summary>
        /// <param name="graph">graph</param>
        /// <returns>(bool, bool) - the first item stands for bipartite and the second item stands for complete
        /// (true, true) - complete bipartite graph
        /// (true, false) - bipartite graph
        /// (false, - ) - the graph is not (complete) bipartite</returns>
        public static Tuple<bool, bool> IsBipartiteGraph(IGraphInterface graph)
        {
            // Variable
            IVertexInterface vertex;
            List<IVertexInterface> neighboursVertexList;
            bool isFirstPartite, isBipartite = true;

            HashSet<IVertexInterface> firstPartite = new HashSet<IVertexInterface>();
            HashSet<IVertexInterface> secondPartite = new HashSet<IVertexInterface>();
            Queue<IVertexInterface> vertexQueue = new Queue<IVertexInterface>();

            vertex = graph.GetFirstVertex();
            vertexQueue.Enqueue(vertex);
            firstPartite.Add(vertex);

            while(vertexQueue.Count != 0)
            {
                vertex = vertexQueue.Dequeue();
                neighboursVertexList = graph.Neighbours(vertex);

                if (firstPartite.Contains(vertex))
                    isFirstPartite = true;
                else
                    isFirstPartite = false;

                foreach (IVertexInterface neighbourVertex in neighboursVertexList)
                {
                    if (!firstPartite.Contains(neighbourVertex) && !secondPartite.Contains(neighbourVertex))
                    {
                        if (isFirstPartite)
                            secondPartite.Add(neighbourVertex);
                        else
                            firstPartite.Add(neighbourVertex);

                        vertexQueue.Enqueue(neighbourVertex);
                    }
                    else
                    {
                        if ((firstPartite.Contains(neighbourVertex) && isFirstPartite) || (secondPartite.Contains(neighbourVertex) && !isFirstPartite))
                        {
                            isBipartite = false;
                        }
                    }
                }

            }

            if (!isBipartite)
                return new Tuple<bool, bool>(false, false);

            // Variable
            int countFirstPartiteVertex = firstPartite.Count;
            int countSecondPartiteVertex = secondPartite.Count;

            // Is the graph a complete bipartite graph
            foreach(Vertex firstPartiteVertex in firstPartite)
            {
                if (graph.CountNeighbours(firstPartiteVertex) != countSecondPartiteVertex)
                    return new Tuple<bool, bool>(true, false);
            }
            foreach (Vertex secondPartiteVertex in secondPartite)
            {
                if (graph.CountNeighbours(secondPartiteVertex) != countFirstPartiteVertex)
                    return new Tuple<bool, bool>(true, false);
            }

            return new Tuple<bool, bool>(true, true);
        }
        #endregion
    }
}
