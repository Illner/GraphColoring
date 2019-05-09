using System;

namespace GraphColoring.Graph
{
    public class VertexExtended : Vertex
    {
        #region Variable
        private static int defaultColor = 0;
        #endregion
        
        #region Constructor
        public VertexExtended() : base() { }

        public VertexExtended(String userName) : base(userName) { }
        /// <summary>
        /// Create a new vertexExtended with a particular identifier
        /// Without userName (null) and without color (0)
        /// Only for GraphEdgeList for vertices with zero degree !!!!!!!!!!!!!!!!!!!!!!!!!!
        /// </summary>
        /// <param name="identifier">identifier of vertex</param>
        public VertexExtended(int identifier) : base(true)
        {
            SetIdentifier(identifier);
        }
        #endregion
        
        #region Method
        /// <summary>
        /// Reset a color of vertex
        /// Set color to default (0)
        /// </summary>
        public void ResetColor()
        {
            color = GetDefaultColor();
        }
        #endregion
        
        #region Property
        /// <summary>
        /// Set a color of vertex
        /// </summary>
        /// <param name="color">new color of vertex</param>
        public void SetColor(int color)
        {
            this.color = color;
        }

        /// <summary>
        /// Set name of vertex which was defined by user
        /// </summary>
        /// <param name="userName">new user name of vertex</param>
        new public void SetUserName(string userName)
        {
            base.SetUserName(userName);
        }

        /// <summary>
        /// Return the default color (0)
        /// </summary>
        /// <returns>default color</returns>
        public static int GetDefaultColor()
        {
            return defaultColor;
        }
        #endregion
    }
}
