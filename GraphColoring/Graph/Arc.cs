using System;

namespace GraphColoring.Graph
{
    class Arc : IArcInterface
    {
        // Variable
        #region
        /// <summary>
        /// vertexFrom - první složka z uspořádané dvojice vrcholů orientované hrany
        /// vertexTo - druhá složka z uspořádané dvojice vrcholů orientované hrany
        /// vertexFrom -> vertexTo
        /// </summary>
        private IVertexInterface vertexFrom;
        private IVertexInterface vertexTo;
        #endregion

        // Constructor
        #region
        public Arc(IVertexInterface vertexFrom, IVertexInterface vertexTo)
        {
            this.vertexFrom = vertexFrom;
            this.vertexTo = vertexTo;
        }
        #endregion

        // Property
        #region
        /// <summary>
        /// Vráí první složku z uspořádané dvojice vrcholů orientované hrany
        /// </summary>
        /// <returns>první složku z uspořádané dvojice vrcholů orientované hrany</returns>
        public IVertexInterface GetVertexFrom()
        {
            return vertexFrom;
        }

        /// <summary>
        /// Vrátí druhou složku z uspořádané dvojice vrcholů orientované hrany
        /// </summary>
        /// <returns>druhou složku z uspořádané dvojice vrcholů orientované hrany</returns>
        public IVertexInterface GetVertexTo()
        {
            return vertexTo;
        }
        #endregion
    }
}