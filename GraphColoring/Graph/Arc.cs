﻿using System;

namespace GraphColoring.Graph
{
    class Arc : IArcInterface
    {
        // Variable
        #region
        /// <summary>
        /// vertexFrom - First vertex
        /// vertexTo - Second vertex
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
        /// Return the first vertex
        /// </summary>
        /// <returns>first vertex of the edge</returns>
        public IVertexInterface GetVertexFrom()
        {
            return vertexFrom;
        }

        /// <summary>
        /// Return the second vertex
        /// </summary>
        /// <returns>second vertex of the edge</returns>
        public IVertexInterface GetVertexTo()
        {
            return vertexTo;
        }
        #endregion
    }
}