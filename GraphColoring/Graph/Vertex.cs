using System;
using System.Text;

namespace GraphColoring.Graph
{
    public class Vertex : IVertexInterface
    {
        #region Variable
        protected int color;
        protected int identifier;
        private string userName;
        private static int counter = 1;
        #endregion
        
        #region Constructor
        /// <summary>
        /// Create a vertex
        /// User name will be same as identifier
        /// Color of vertex will be set to default (0)
        /// </summary>
        public Vertex()
        {
            SetIdentifier(counter++);
            SetUserName(identifier.ToString());
            color = 0;
        }

        /// <summary>
        /// Create a vertex
        /// Color of vertex will be set to default (0)
        /// </summary>
        /// <param name="userName">user name of vertex</param>
        public Vertex(string userName)
        {
            SetIdentifier(counter++);
            SetUserName(userName);
            color = 0;
        }

        /// <summary>
        /// Do not use !!!!!!!!!!!
        /// Only for vertexExtended(identifier)
        /// Does not increment the counter
        /// </summary>
        /// <param name="useless">useless</param>
        protected Vertex(bool useless) { }
        #endregion
        
        #region Method
        /// <summary>
        /// Compare two vertices
        /// Return true if vertices are equal, otherwise false
        /// </summary>
        /// <param name="vertex">the second vertex</param>
        /// <returns>true if vertices are equal, otherwise false</returns>
        public bool Equals(IVertexInterface vertex)
        {
            if (identifier == vertex.GetIdentifier() && userName == vertex.GetUserName() && GetColor() == vertex.GetColor())
                return true;

            return false;
        }

        override
        public string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("Vertex (" + GetIdentifier() + "): " + GetUserName());

            return stringBuilder.ToString();
        }
        #endregion
        
        #region Property
        /// <summary>
        /// Return an identifier of vertex
        /// </summary>
        /// <returns>identifier of vertex</returns>
        public int GetIdentifier()
        {
            return identifier;
        }

        /// <summary>
        /// Set an identifier of vertex
        /// </summary>
        /// <param name="identifier">new identifier of vertex</param>
        protected void SetIdentifier(int identifier)
        {
            this.identifier = identifier;
        }

        /// <summary>
        /// Return an user name of vertex
        /// </summary>
        /// <returns>user name of vertex</returns>
        public string GetUserName()
        {
            return userName;
        }

        /// <summary>
        /// Set an user name of vertex
        /// </summary>
        /// <param name="userName">new user name of vertex</param>
        protected void SetUserName(string userName)
        {
            this.userName = userName;
        }

        /// <summary>
        /// Return a color of vertex
        /// </summary>
        /// <returns>color of vertex</returns>
        public int GetColor()
        {
            return color;
        }
        #endregion
    }
}
