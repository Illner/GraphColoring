using System;

namespace GraphColoring.Graph
{
    class Vertex
    {
        // Variable
        #region
        /// <summary>
        /// color - barva vrcholu, pokud vrchol nemá přiřazenou barvu, tak color = null
        /// identifier - číselný identifikátor vrcholu
        /// userName - označení vrcholu uživatelem -> např. A, Vertex1 apod.
        /// counter - určuje identifikátor pro nový vrchol
        /// </summary>
        protected int? color;
        private int identifier;
        private string userName;
        private static int counter = 1;
        #endregion

        // Constructor
        #region
        /// <summary>
        /// Vytvoří vrchol
        /// Jméno vrcholu bude stejné jako jeho identifikátor
        /// Barva vrcholu bude nastavena na null
        /// </summary>
        public Vertex()
        {
            SetIdentifier(counter++);
            SetUserName(identifier.ToString());
            color = null;
        }

        /// <summary>
        /// Vytvoří vrchol
        /// Barva vrcholu bude nastavena na null
        /// </summary>
        /// <param name="userName">Jméno vrcholu, které určil uživatel</param>
        public Vertex(String userName)
        {
            SetIdentifier(counter++);
            SetUserName(userName);
            color = null;
        }
        #endregion

        // Method
        #region
        public bool Equals(Vertex vertex)
        {
            if (identifier == vertex.GetIdentifier() && userName == vertex.GetUserName())
                return true;

            return false;
        }
        #endregion

        // Property
        #region
        /// <summary>
        /// Vrátí identifikátor vrcholu
        /// </summary>
        /// <returns>číselný identifikátor</returns>
        public int GetIdentifier()
        {
            return identifier;
        }

        /// <summary>
        /// Nastaví identifikátor vrcholu
        /// </summary>
        /// <param name="identifier">nový identifikátor vrcholu</param>
        private void SetIdentifier(int identifier)
        {
            this.identifier = identifier;
        }

        /// <summary>
        /// Vrátí jméno vrcholu, které určil uživatel
        /// </summary>
        /// <returns>řetězcové jménbo vrcholu</returns>
        public string GetUserName()
        {
            return userName;
        }

        /// <summary>
        /// Nastaví jméno vrcholu, které určil uživatel
        /// </summary>
        /// <param name="userName">nový jméno vrcholu</param>
        private void SetUserName(string userName)
        {
            this.userName = userName;
        }

        /// <summary>
        /// Vrátí barvu vrcholu
        /// Pokud vrchol nemá přiřazenou barvu, tak vrátí null
        /// </summary>
        /// <returns>barva vrcholu</returns>
        public int? GetColor()
        {
            return color;
        }
        #endregion
    }
}
