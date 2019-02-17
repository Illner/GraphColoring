using System;
using System.Collections.Generic;

namespace GraphColoring.Graph
{
    public interface IGraphAdjacencyMatrixInterface : IGraphInterface
    {
        void SetOfNeighborsOfVertex(List<bool> rowAdjacencyMatrix);
    }
}
