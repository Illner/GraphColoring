using System;
using System.IO;
using System.Text.RegularExpressions;

namespace GraphColoring.ReaderWriter
{
    class ReaderWriter : IReaderWriterInterface
    {
        // Variable
        #region
        /// <summary>
        /// path - cesta k souboru
        /// newLine - znak pro odřádkování
        /// FILETYPE - přípona podporovaných souboru = .graph
        /// </summary>
        private string path;
        private static char newLine = '\n';
        private const string FILETYPE = ".graph";
        #endregion

        // Constructor
        #region
        /// <summary>
        /// Inicializuje ReaderWriter
        /// Nastaví cestu k souboru
        /// </summary>
        /// <param name="path">cesta k souboru</param>
        public ReaderWriter(string path)
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
            if (GetPath() == "")
                throw new MyException.ReaderWriterInavalidPathException(GetPath());

            Regex regex = new Regex(@"^[a-zA-Z]:\\$");
            if (!regex.IsMatch(GetPath().Substring(0, 3)))
            {
                throw new MyException.ReaderWriterInavalidPathException(GetPath());
            }

            string strTheseAreInvalidFileNameChars = new string(Path.GetInvalidPathChars());
            strTheseAreInvalidFileNameChars += @":/?*" + "\"";
            Regex containsABadCharacterRegex = new Regex("[" + Regex.Escape(strTheseAreInvalidFileNameChars) + "]");
            if (containsABadCharacterRegex.IsMatch(GetPath().Substring(3, GetPath().Length - 3)))
            {
                throw new MyException.ReaderWriterInavalidPathException(GetPath());
            }

            if (!GetPath().EndsWith(FILETYPE))
                throw new MyException.ReaderWriterInvalidFileTypeException(GetPath());
        }

        /// <summary>
        /// Odstraní daný soubor
        /// Pokud je neplatná cesta, tak vrátí vyjímku ReaderWriterInavalidPathException
        /// Pokud není oprávnění na modifikaci se souborem, tak vrátí vyjímku ReaderWriterNoAuthorizationException
        /// </summary>
        public void DeleteFile()
        {
            try
            {
                if (ExistsFile())
                    File.Delete(GetPath());
            }
            catch (UnauthorizedAccessException)
            {
                throw new MyException.ReaderWriterNoAuthorizationException(GetPath());
            }

        }

        /// <summary>
        /// Vytvoří nový soubor
        /// Pokud je neplatná cesta, tak vrátí vyjímku ReaderWriterInavalidPathException
        /// Pokud není oprávnění na modifikaci se souborem, tak vrátí vyjímku ReaderWriterNoAuthorizationException
        /// </summary>
        public void CreateFile()
        {
            try
            {
                if (!ExistsFile())
                    using (File.Create(GetPath())) { }
            }
            catch (UnauthorizedAccessException)
            {
                throw new MyException.ReaderWriterNoAuthorizationException(GetPath());
            }
        }

        /// <summary>
        /// Odstraní obsah v daném souboru
        /// Pokud soubor neexistuje, tak vytvoří nový soubor
        /// Pokud je neplatná cesta, tak vrátí vyjímku ReaderWriterInavalidPathException
        /// Pokud není oprávnění na modifikaci se souborem, tak vrátí vyjímku ReaderWriterNoAuthorizationException
        /// </summary>
        public void ClearFile()
        {
            try
            {
                File.WriteAllText(GetPath(), string.Empty);
            }
            catch (UnauthorizedAccessException)
            {
                throw new MyException.ReaderWriterNoAuthorizationException(GetPath());
            }
        }

        /// <summary>
        /// Kontroluje, zda existuje daný soubor
        /// Pokud je neplatná cesta, tak vrátí vyjímku ReaderWriterInavalidPathException
        /// Pokud není oprávnění na modifikaci se souborem, tak vrátí vyjímku ReaderWriterNoAuthorizationException
        /// </summary>
        /// <returns>true pokud soubor existuje, jinak vrátí false</returns>
        public Boolean ExistsFile()
        {
            try
            {
                return File.Exists(GetPath());
            }
            catch (UnauthorizedAccessException)
            {
                throw new MyException.ReaderWriterNoAuthorizationException(GetPath());
            }
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
