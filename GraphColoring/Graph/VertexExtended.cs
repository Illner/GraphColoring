using System;

namespace GraphColoring.Graph
{
    class VertexExtended : Vertex
    {
        // Variable
        #region
        #endregion

        // Constructor
        #region
        public VertexExtended() : base() { }
        public VertexExtended(String userName) : base(userName) { }
        /// <summary>
        /// Vytvoří nový vrchol typu VertexExtended s daným identifikátorem
        /// Bez userName (null) a bez color (null)
        /// Pouze pro GraphModification.VertexAdd !!!!!!!!!!!!!!!!!!!!!!!!!
        /// </summary>
        /// <param name="identifier">Identifikátor vrcholu</param>
        public VertexExtended(int identifier) : base(true)
        {
            SetIdentifier(identifier);
        }
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
        public void SetColor(int? color)
        {
            this.color = color;
        }

        /// <summary>
        /// Nastaví jméno vrcholu, které určil uživatel
        /// </summary>
        /// <param name="userName">nový jméno vrcholu</param>
        new public void SetUserName(string userName)
        {
            base.SetUserName(userName);
        }
        #endregion
    }
}
