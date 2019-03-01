using System;

namespace GraphColoring.Graph
{
    class Edge : IEdgeInterface
    {
        // Variable
        #region
        /// <summary>
        /// vertex1 - First vertex
        /// vertex2 - Second vertex
        /// </summary>
        private IVertexInterface vertex1;
        private IVertexInterface vertex2;
        #endregion

        // Constructor
        #region
        public Edge(IVertexInterface vertex1, IVertexInterface vertex2)
        {
            this.vertex1 = vertex1;
            this.vertex2 = vertex2;
        }
        #endregion

        // Method
        public bool Equals(IEdgeInterface edge)
        {
            if ((edge.GetVertex1().Equals(vertex1) && edge.GetVertex2().Equals(vertex2)) ||
                (edge.GetVertex1().Equals(vertex2) && edge.GetVertex2().Equals(vertex1)))
                return true;

            return false;
        }

        // Property
        #region
        /// <summary>
        /// Return the first vertex of the edge
        /// </summary>
        /// <returns>first vertex</returns>
        public IVertexInterface GetVertex1()
        {
            return vertex1;
        }

        /// <summary>
        /// Return the second vertex of the edge
        /// </summary>
        /// <returns>second vertex</returns>
        public IVertexInterface GetVertex2()
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
