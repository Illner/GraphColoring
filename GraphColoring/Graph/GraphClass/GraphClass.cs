﻿using System;
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
        public static GraphClassEnum GetGraphClass(Graph graph)
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
        public static bool IsCompleteGraph(Graph graph)
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
        public static bool IsTreeGraph(Graph graph)
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
        public static bool IsCycleGraph(Graph graph)
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
        /// </summary>
        /// <param name="graph">graf, u kterého chceme zjistit zda je bipartitní</param>
        /// <returns>true pokud je graf bipartitní, jinak vrátí false</returns>
        public static bool IsBipartiteGraph(Graph graph)
        {
            // TODO IsBipartiteGraph

            return false;
        }
        #endregion
    }
}
