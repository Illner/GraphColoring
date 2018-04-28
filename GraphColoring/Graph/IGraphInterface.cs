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
        int GetRealCountVertices();
        string GetName();
        void SetName(string name);
        #endregion
    }
}
