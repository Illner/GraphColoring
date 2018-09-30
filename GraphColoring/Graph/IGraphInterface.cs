using System;
using System.Collections.Generic;

namespace GraphColoring.Graph
{
    interface IGraphInterface
    {
        // Method
        #region
        Vertex GetVertex(string userName);
        void InitializeGraph();
        String ToString();
        List<Vertex> Neighbours(Vertex vertex);
        int CountNeighbours(Vertex vertex);
        List<Vertex> AllVertices();
        Vertex GetFirstVertex();
        void FullGenerateVertices();
        bool ExistsVertex(Vertex vertex);
        bool ExistsEdge(Edge edge);

        void VertexAdd(Vertex vertex);
        void VertexDelete(Vertex vertex);
        void VertexContract(Vertex vertex);
        void VertexSuppression(Vertex vertex);
        void VertexExpansion(Vertex vertex);
        void EdgeAdd(Edge edge);
        void EdgeDelete(Edge edge);
        void EdgeContract(Edge edge);
        void EdgeSubdivision(Edge edge);
        #endregion

        // Property
        #region
        int GetRealCountVertices();
        String GetName();
        void SetName(string name);
        GraphClass.GraphClass.GraphClassEnum GetGraphClass();
        GraphProperty.GraphProperty GetGraphProperty();
        bool GetIsInitialized();
        bool GetCanDeIncreaseCountVertices();
        bool GetCanDeIncreaseCountEdges();
        IColoredGraphInterface GetColoredGraph();
        #endregion
    }
}
