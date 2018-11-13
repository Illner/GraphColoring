using System;
using System.Collections.Generic;

namespace GraphColoring.Graph
{
    interface IColoredGraphInterface
    {
        // Method
        #region
        void ColorVertex(IVertexInterface vertex, int color);
        int GetColorVertex(IVertexInterface vertex);
        bool IsVertexColored(IVertexInterface vertex);
        void ResetColorVertex(IVertexInterface vertex);

        bool CheckValidColor(IVertexInterface vertex);
        List<IVertexInterface> CheckValidColor();
        void GreedyColoring(List<IVertexInterface> vertexList);
        int GreedyColoring(IVertexInterface vertex);
        bool IsValidColored();
        bool InicializeColoredGraph();
        void DeinicializationColoredGraph();
        void ResetColors();
        List<int> UsedColors();
        List<IVertexInterface> ColoredVertices(int color);
        void SetSaturation(bool saturation);
        IVertexInterface GetSaturationDegreeSequence();
        bool AreAllVerticesColored();
        #endregion

        // Property
        #region
        int GetCountUsedColors();
        List<IVertexInterface> GetColoredVertexList();
        List<IVertexInterface> GetUnColoredVertexList();
        bool GetIsInicializedColoredGraph();
        #endregion
    }
}
