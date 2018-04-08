using System;

namespace GraphColoring.Graph
{
    interface IGraphInterface
    {
        // Method
        #region
        void InitializeGraph();
        #endregion

        // Property
        #region
        int GetCountVertices();
        int GetRealCountVertices();
        int GetCountEdges();
        string GetName();
        void SetName(string name);
        #endregion
    }
}
