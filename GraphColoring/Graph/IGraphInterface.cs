using System;
using System.Collections.Generic;

namespace GraphColoring.Graph
{
    interface IGraphInterface
    {
        // Method
        #region
        IVertexInterface GetVertex(string userName);
        void InitializeGraph();
        String ToString();
        List<IVertexInterface> Neighbours(IVertexInterface vertex);
        int CountNeighbours(IVertexInterface vertex);
        List<IVertexInterface> AllVertices();
        IVertexInterface GetFirstVertex();
        void FullGenerateVertices();
        bool ExistsVertex(IVertexInterface vertex);
        bool ExistsEdge(IEdgeInterface edge);

        void VertexAdd(IVertexInterface vertex);
        void VertexDelete(IVertexInterface vertex);
        void VertexContract(IVertexInterface vertex);
        void VertexSuppression(IVertexInterface vertex);
        void VertexExpansion(IVertexInterface vertex);
        void EdgeAdd(IEdgeInterface edge);
        void EdgeDelete(IEdgeInterface edge);
        void EdgeContract(IEdgeInterface edge);
        void EdgeSubdivision(IEdgeInterface edge);
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
