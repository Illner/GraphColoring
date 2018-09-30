using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring.Graph.GraphClass
{
    static partial class GraphClass
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

            if (IsBipartiteGraph(graph))
                return GraphClassEnum.bipartiteGraph;

            return GraphClassEnum.none;
        }

        /// <summary>
        /// Vrátí true, pokud je graf úplný, jinak vrátí false
        /// Time complexity: O(1)
        /// </summary>
        /// <param name="graph">Graf, u kterého chceme zjistit zda je úplný</param>
        /// <returns>true pokud je graf úplny, jinak vrátí false</returns>
        public static bool IsCompleteGraph(IGraphInterface graph)
        {
            // |E| = (|V|)C(2)
            if (graph.GetGraphProperty().GetCountEdges() == MyMath.MyMath.nCr(graph.GetGraphProperty().GetCountVertices(), 2))
                return true;

            return false;
        }

        /// <summary>
        /// Vrátí true, pokud je graf strom, jinak vrátí false
        /// Time complexity: O(V + E)
        /// </summary>
        /// <param name="graph">graf, u kterého chceme zjistit zda je strom</param>
        /// <returns>true pokud je graf strom, jinak vrátí false</returns>
        public static bool IsTreeGraph(IGraphInterface graph)
        {
            // Graph is connected and |E| = |V| - 1 // Euler's formula
            if ((graph.GetGraphProperty().GetIsConnected()) &&
                (graph.GetGraphProperty().GetCountEdges() == graph.GetGraphProperty().GetCountVertices() - 1))
                return true;

            return false;
        }

        /// <summary>
        /// Vrátí true, pokud je graf kružnice, jinak vrátí false
        /// Time complexity: O(V + E)
        /// </summary>
        /// <param name="graph">graf, u kterého chceme zjistit zda je kružnice</param>
        /// <returns>true pokud je graf kružnice, jinak vrátí false</returns>
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
        /// Vrátí true, pokud je graf bipartitní, jinak vrátí false
        /// Time complexity: O(V + E)
        /// </summary>
        /// <param name="graph">graf, u kterého chceme zjistit zda je bipartitní</param>
        /// <returns>true pokud je graf bipartitní, jinak vrátí false</returns>
        public static bool IsBipartiteGraph(IGraphInterface graph)
        {
            // Variable
            Vertex vertex;
            List<Vertex> neighboursVertexList;
            bool isFirstPartite, isBipartite = true;

            HashSet<Vertex> firstPartite = new HashSet<Vertex>();
            HashSet<Vertex> secondPartite = new HashSet<Vertex>();
            Queue<Vertex> vertexQueue = new Queue<Vertex>();

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

                foreach (Vertex neighbourVertex in neighboursVertexList)
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

            return isBipartite;
        }
        #endregion
    }
}
