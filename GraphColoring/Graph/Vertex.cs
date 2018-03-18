using System;

namespace GraphColoring.Graph
{
    class Vertex
    {
        // Variable
        #region
        /// <summary>
        /// identifier - číselný identifikátor vrcholu
        /// userName - označení vrcholu uživatelem -> např. A, Vertex1 apod.
        /// counter - určuje identifikátor pro nový vrchol
        /// </summary>
        private int identifier;
        private string userName;
        private static int counter = 0;
        #endregion

        // Constructor
        #region
        /// <summary>
        /// Vytvoří vrchol
        /// Jméno vrcholu bude stejné jako jeho identifikátor
        /// </summary>
        public Vertex()
        {
            setIdentifier(counter++);
            setUserName(identifier.ToString());
        }

        /// <summary>
        /// Vytvoří vrchol
        /// </summary>
        /// <param name="userName">Jméno vrcholu, které určil uživatel</param>
        public Vertex(String userName)
        {
            setIdentifier(counter++);
            setUserName(userName);
        }
        #endregion

        // Property
        #region
        /// <summary>
        /// Vrátí identifikátor vrcholu
        /// </summary>
        /// <returns>číselný identifikátor</returns>
        public int getIdentifier()
        {
            return identifier;
        }

        /// <summary>
        /// Nastaví identifikátor vrcholu
        /// </summary>
        /// <param name="identifier">nový identifikátor vrcholu</param>
        private void setIdentifier(int identifier)
        {
            this.identifier = identifier;
        }

        /// <summary>
        /// Vrátí jméno vrcholu, které určil uživatel
        /// </summary>
        /// <returns>řetězcové jménbo vrcholu</returns>
        public string getUserName()
        {
            return userName;
        }

        /// <summary>
        /// Nastaví jméno vrcholu, které určil uživatel
        /// </summary>
        /// <param name="userName">nový jméno vrcholu</param>
        private void setUserName(string userName)
        {
            this.userName = userName;
        }
        #endregion
    }
}
