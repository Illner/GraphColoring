using System;

namespace GraphColoring.Graph
{
    class Arc
    {
        // Variable
        #region
        /// <summary>
        /// vertexFrom - první složka z uspořádané dvojice vrcholů orientované hrany
        /// vertexTo - druhá složka z uspořádané dvojice vrcholů orientované hrany
        /// vertexFrom -> vertexTo
        /// </summary>
        private Vertex vertexFrom;
        private Vertex vertexTo;
        #endregion

        // Constructor
        #region
        public Arc(Vertex vertexFrom, Vertex vertexTo)
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
        public Vertex GetVertexFrom()
        {
            return vertexFrom;
        }

        /// <summary>
        /// Vrátí druhou složku z uspořádané dvojice vrcholů orientované hrany
        /// </summary>
        /// <returns>druhou složku z uspořádané dvojice vrcholů orientované hrany</returns>
        public Vertex GetVertexTo()
        {
            return vertexTo;
        }
        #endregion
    }
}