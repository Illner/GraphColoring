using System;
using System.Collections.Generic;

namespace GraphColoring.Graph
{
    interface IGraphAdjacencyMatrixInterface : IGraphInterface
    {
        void SetOfNeighborsOfVertex(List<bool> rowAdjacencyMatrix);
    }
}
