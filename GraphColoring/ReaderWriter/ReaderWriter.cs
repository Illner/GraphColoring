using System;
using System.IO;
using System.Text.RegularExpressions;

namespace GraphColoring.ReaderWriter
{
    abstract class ReaderWriter : IReaderWriterInterface
    {
        // Variable
        #region
        /// <summary>
        /// newLine - znak pro odřádkování
        /// </summary>
        private string path;
        private Boolean isPathValid;
        private static char newLine = '\n';
        #endregion

        // Constructor
        #region
        /// <summary>
        /// Inicializuje ReaderWriter
        /// Nastaví cestu k souboru
        /// </summary>
        /// <param name="path">cesta k souboru</param>
        ReaderWriter(string path)
        {
            SetPath(path);
            CheckPath();
        }
        #endregion

        // Method
        #region
        /// <summary>
        /// Kontroluje, zda je path správná cesta. Výsledek ukládá do proměnné isPathValid.
        /// </summary>
        private void CheckPath()
        {
            Regex regex = new Regex(@"^[a-zA-Z]:\\$");
            if (!regex.IsMatch(GetPath().Substring(0, 3)))
            {
                isPathValid = false;
                return;
            }

            string strTheseAreInvalidFileNameChars = new string(Path.GetInvalidPathChars());
            strTheseAreInvalidFileNameChars += @":/?*" + "\"";
            Regex containsABadCharacterRegex = new Regex("[" + Regex.Escape(strTheseAreInvalidFileNameChars) + "]");
            if (containsABadCharacterRegex.IsMatch(GetPath().Substring(3, GetPath().Length - 3)))
            {
                isPathValid = false;
                return;
            }

            isPathValid = true;
        }

        /// <summary>
        /// Odstraní daný soubor
        /// Pokud je neplatná cesta, tak vrátí vyjímku ReaderWriterInavalidPathException
        /// Pokud není oprávnění na modifikaci se souborem, tak vrátí vyjímku ReaderWriterNoAuthorizationException
        /// </summary>
        public void DeleteFile()
        {
            if (ExistsFile())
                File.Delete(GetPath());
        }

        /// <summary>
        /// Vytvoří nový soubor
        /// Pokud je neplatná cesta, tak vrátí vyjímku ReaderWriterInavalidPathException
        /// Pokud není oprávnění na modifikaci se souborem, tak vrátí vyjímku ReaderWriterNoAuthorizationException
        /// </summary>
        public void CreateFile()
        {
            DirectoryInfo directory = new DirectoryInfo(Path.GetFullPath(GetPath()));
            if (!directory.Exists)
                directory.Create();
        }

        /// <summary>
        /// Odstraní obsah v daném souboru
        /// Pokud soubor neexistuje, tak vytvoří nový soubor
        /// Pokud je neplatná cesta, tak vrátí vyjímku ReaderWriterInavalidPathException
        /// Pokud není oprávnění na modifikaci se souborem, tak vrátí vyjímku ReaderWriterNoAuthorizationException
        /// </summary>
        public void ClearFile()
        {

        }

        /// <summary>
        /// Kontroluje, zda existuje daný soubor
        /// Pokud je neplatná cesta, tak vrátí vyjímku ReaderWriterInavalidPathException
        /// Pokud není oprávnění na modifikaci se souborem, tak vrátí vyjímku ReaderWriterNoAuthorizationException
        /// </summary>
        /// <returns>true pokud soubor existuje, jinak vrátí false</returns>
        public Boolean ExistsFile()
        {
            return File.Exists(GetPath());
        }
        
        #endregion

        // Property
        #region
        /// <summary>
        /// Vrací znak pro odřádkování
        /// </summary>
        /// <returns>znak pro odřádkování</returns>
        public static char GetNewLine()
        {
            return newLine;
        }

        /// <summary>
        /// Nastaví cestu k danému souboru
        /// </summary>
        /// <param name="path">cesta k souboru</param>
        private void SetPath(string path)
        {
            this.path = path;
        }

        /// <summary>
        /// Vrátí cestu k danému souboru
        /// </summary>
        /// <returns>cesta k souboru</returns>
        public string GetPath()
        {
            return path;
        }
        #endregion
    }
}
