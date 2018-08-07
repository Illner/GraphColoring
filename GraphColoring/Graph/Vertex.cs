﻿using System;
using System.Text;

namespace GraphColoring.Graph
{
    class Vertex
    {
        // Variable
        #region
        /// <summary>
        /// color - barva vrcholu, pokud vrchol nemá přiřazenou barvu, tak color = 0
        /// identifier - číselný identifikátor vrcholu
        /// userName - označení vrcholu uživatelem -> např. A, Vertex1 apod.
        /// counter - určuje identifikátor pro nový vrchol
        /// </summary>
        protected int color;
        protected int identifier;
        private string userName;
        private static int counter = 1;
        #endregion

        // Constructor
        #region
        /// <summary>
        /// Vytvoří vrchol
        /// Jméno vrcholu bude stejné jako jeho identifikátor
        /// Barva vrcholu bude nastavena na 0
        /// </summary>
        public Vertex()
        {
            SetIdentifier(counter++);
            SetUserName(identifier.ToString());
            color = 0;
        }

        /// <summary>
        /// Vytvoří vrchol
        /// Barva vrcholu bude nastavena na 0
        /// </summary>
        /// <param name="userName">Jméno vrcholu, které určil uživatel</param>
        public Vertex(String userName)
        {
            SetIdentifier(counter++);
            SetUserName(userName);
            color = 0;
        }

        /// <summary>
        /// Nepoužívat!!!!!!!!!!!!
        /// Slouží pouze pro VertexExtended(int), aby se neikrementoval counter
        /// </summary>
        /// <param name="useless">nic</param>
        protected Vertex(Boolean useless) { }
        #endregion

        // Method
        #region
        public bool Equals(Vertex vertex)
        {
            if (identifier == vertex.GetIdentifier() && userName == vertex.GetUserName() && GetColor() == vertex.GetColor())
                return true;

            return false;
        }

        override
        public String ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("Vertex (" + GetIdentifier() + "): " + GetUserName());

            return stringBuilder.ToString();
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
        protected void SetIdentifier(int identifier)
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
        protected void SetUserName(string userName)
        {
            this.userName = userName;
        }

        /// <summary>
        /// Vrátí barvu vrcholu
        /// Pokud vrchol nemá přiřazenou barvu, tak vrátí 0
        /// </summary>
        /// <returns>barva vrcholu</returns>
        public int GetColor()
        {
            return color;
        }
        #endregion
    }
}
