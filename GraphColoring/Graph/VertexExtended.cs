using System;

namespace GraphColoring.Graph
{
    class VertexExtended : Vertex
    {
        // Variable
        #region
        #endregion

        // Construcotr
        #region
        #endregion

        // Method
        #region
        /// <summary>
        /// Resetuje barvu vrcholu
        /// Ńastaví color na null
        /// </summary>
        public void ResetColor()
        {
            color = null;
        }
        #endregion

        // Property
        #region
        /// <summary>
        /// Nastaví barvu vrcholu
        /// </summary>
        /// <param name="color">barva vrcholu</param>
        public void SetColor(int color)
        {
            this.color = color;
        }
        #endregion
    }
}
