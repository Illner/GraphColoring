using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring.Graph.GraphProperty
{
    partial class GraphProperty
    {
        // Variable
        #region
        private List<int> degreeSequence;
        #endregion

        // Method
        #region
        /// <summary>
        /// Získá skóre grafu
        /// degreeSequence
        /// </summary>
        private void DegreeSequence()
        {

        }
        #endregion

        // Property
        #region
        /// <summary>
        /// Vrátí skóre grafu
        /// </summary>
        /// <returns>skóre grafu jako list intů</returns>
        public List<int> GetDegreeSequence()
        {
            if (degreeSequence == null)
                DegreeSequence();

            return degreeSequence;
        }
        #endregion
    }
}
