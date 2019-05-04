using System;
using System.Collections.Generic;

namespace GraphColoring.Graph
{
    public interface IColoredGraphInterface
    {
        // Method
        #region
        void ColorVertex(IVertexInterface vertex, int color);
        int GetColorVertex(IVertexInterface vertex);
        bool IsVertexColored(IVertexInterface vertex);
        void ResetColorVertex(IVertexInterface vertex);

        bool CheckValidColor(IVertexInterface vertex);
        List<IVertexInterface> CheckValidColor();
        int GreedyColoring(List<IVertexInterface> vertexList, GraphColoringAlgorithm.SequenceAlgorithm.GraphColoringSequenceAlgorithm.GraphColoringAlgorithInterchangeEnum interchangeEnum = GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.none);
        int GreedyColoring(IVertexInterface vertex);
        bool IsValidColored();
        bool InitializeColoredGraph();
        void DeinitializationColoredGraph();
        void ResetColors();
        List<int> UsedColors();
        List<IVertexInterface> ColoredVertices(int color);
        List<int> ColorsNeighbours(IVertexInterface vertex);
        List<IVertexInterface> ColoredNeighbours(int color, IVertexInterface vertex);
        void SetSaturation(bool saturation);
        IVertexInterface GetSaturationDegreeSequence();
        bool AreAllVerticesColored();
        bool TryChangeColoring(IVertexInterface mainVertex, int color);
        bool TryChangeColoringExtended(IVertexInterface mainVertex, int color, bool canUseK3);
        int GetMostUsedColorNeighborsNeighbor(IVertexInterface vertex, List<int> availableColorList);
        #endregion

        // Property
        #region
        int GetCountUsedColors();
        List<IVertexInterface> GetColoredVertexList();
        List<IVertexInterface> GetUnColoredVertexList();
        bool GetIsInitializedColoredGraph();
        #endregion
    }
}
