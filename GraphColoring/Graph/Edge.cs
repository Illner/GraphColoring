using System;

namespace GraphColoring.Graph
{
    class Edge
    {
        // Variable
        #region
        /// <summary>
        /// vertex1 - První vrchol hrany
        /// vertex2 - Druhý vrchol hrany
        /// </summary>
        private Vertex vertex1;
        private Vertex vertex2;
        #endregion

        // Constructor
        #region
        public Edge(Vertex vertex1, Vertex vertex2)
        {
            this.vertex1 = vertex1;
            this.vertex2 = vertex2;
        }
        #endregion

        // Property
        #region
        /// <summary>
        /// Vrátí první vrchol hrany
        /// </summary>
        /// <returns>první vrchol hrany</returns>
        public Vertex GetVertex1()
        {
            return vertex1;
        }

        /// <summary>
        /// Vrátí druhý vrchol hrany
        /// </summary>
        /// <returns>druhý vrchol hrany</returns>
        public Vertex GetVertex2()
        {
            return vertex2;
        }
        
        override
        public String ToString()
        {
            return "(" + vertex1.GetUserName() + ", " + vertex2.GetUserName() + ")";
        }
        #endregion
    }
}
