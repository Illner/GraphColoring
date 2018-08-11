﻿using System;
using System.Collections.Generic;

namespace GraphColoring.Graph
{
    interface IColoredGraphInterface
    {
        // Method
        #region
        void ColorVertex(Vertex vertex, int color);
        int GetColorVertex(Vertex vertex);
        bool IsVertexColored(Vertex vertex);
        void ResetColorVertex(Vertex vertex);

        bool CheckValidColor(Vertex vertex);
        List<Vertex> CheckValidColor();
        void GreedyColoring(List<Vertex> vertexList);
        int GreedyColoring(Vertex vertex);
        bool InicializeColoredGraph();
        void DeinicializationColoredGraph();
        void ResetColors();
        List<int> UsedColors();
        List<Vertex> ColoredVertices(int color);
        #endregion

        // Property
        #region
        int GetCountUsedColors();
        List<Vertex> GetColoredVertexList();
        List<Vertex> GetUnColoredVertexList();
        bool GetIsInicializedColoredGraph();
        #endregion
    }
}