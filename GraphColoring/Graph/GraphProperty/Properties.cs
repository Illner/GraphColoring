﻿using System;
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
        /// <summary>
        /// isConnected - je graf souvislý
        /// isPlanar - je graf rovinný
        /// isRegular - je graf regulární
        /// </summary>
        private Boolean? isConnected;
        private Boolean? isPlanar;
        private Boolean? isRegular;
        #endregion

        // Method
        #region
        /// <summary>
        /// Zjistí zda je graf souvislý
        /// isConnected
        /// </summary>
        private void IsConnected()
        {
            if (GetCountComponents() != 1)
                isConnected = false;
            else
                isConnected = true;
            
        }
        
        /// <summary>
        /// Zjistí zda je graf rovinný
        /// </summary>
        private void IsPlanar()
        {
            // TODO isPlannar
        }

        /// <summary>
        /// Zjistí zda je graf regulární
        /// </summary>
        private void IsRegular()
        {
            // TODO isRegular
        }
        #endregion

        // Property
        #region
        /// <summary>
        /// Vrátí true pokud je graf souvislý, jinak vrátí false
        /// </summary>
        /// <returns>true pokud je graf souvislý, jinak vrátí false</returns>
        public bool GetIsConnected()
        {
            if (!isConnected.HasValue)
                IsConnected();

            return (bool)isConnected;
        }

        /// <summary>
        /// Vrátí true pokud je graf rovinný, jinak vrátí false
        /// </summary>
        /// <returns>true pokud je graf rovinný, jinak vrátí false</returns>
        public bool GetIsPlanar()
        {
            if (!isPlanar.HasValue)
                IsPlanar();

            return (bool)isPlanar;
        }

        /// <summary>
        /// Vrátí true pokud je graf regulární, jinak vrátí false
        /// </summary>
        /// <returns>true pokud je graf regulární, jinak vrátí false</returns>
        public bool GetIsRegular()
        {
            if (!isRegular.HasValue)
                IsRegular();

            return (bool)isRegular;
        }
        #endregion
    }
}
