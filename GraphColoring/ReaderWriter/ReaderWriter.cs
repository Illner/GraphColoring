using System;
using System.IO;
using System.Text.RegularExpressions;

namespace GraphColoring.ReaderWriter
{
    public abstract partial class ReaderWriter : IReaderWriterInterface
    {
        // Variable
        #region
        /// <summary>
        /// path - cesta k souboru
        /// newLine - znak pro odřádkování
        /// DELIMITER - Oddělovač v souboru
        /// FILETYPE - přípona podporovaných souboru = .graph
        /// </summary>
        private string path;
        private static char newLine = '\n';
        protected const char SEPARATOR = ' ';
        private const string FILETYPE = ".graph";
        private static string fileName = "TestFile.temp";
        // Adjacency list
        protected const string LEFTSEPARATORADJACENCYLIST = "(";
        protected const string RIGHTSEPARATORADJACENCYLIST = ")";
        #endregion

        // Constructor
        #region
        /// <summary>
        /// Inicializuje ReaderWriter
        /// Nastaví cestu k souboru
        /// </summary>
        /// <param name="path">cesta k souboru</param>
        protected ReaderWriter(string path)
        {
            SetPath(path);
            CheckPath();
        }

        /// <summary>
        /// Inicializuje ReaderWriter
        /// Nastaví cestu k souboru
        /// </summary>
        /// <param name="path">cesta k souboru</param>
        /// <param name="checkPath">má se provádět kontrola cesty</param>
        protected ReaderWriter(string path, bool checkPath)
        {
            SetPath(path);
            if (checkPath)
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
                throw new MyException.ReaderWriterException.ReaderWriterInavalidPathException("Invalid path");

            Regex regex = new Regex(@"^[a-zA-Z]:\\$");
            if (!regex.IsMatch(GetPath().Substring(0, 3)))
            {
                throw new MyException.ReaderWriterException.ReaderWriterInavalidPathException("Invalid path");
            }

            string strTheseAreInvalidFileNameChars = new string(Path.GetInvalidPathChars());
            strTheseAreInvalidFileNameChars += @":/?*" + "\"";
            Regex containsABadCharacterRegex = new Regex("[" + Regex.Escape(strTheseAreInvalidFileNameChars) + "]");
            if (containsABadCharacterRegex.IsMatch(GetPath().Substring(3, GetPath().Length - 3)))
            {
                throw new MyException.ReaderWriterException.ReaderWriterInavalidPathException("Invalid path");
            }
            
            if (!GetPath().EndsWith(FILETYPE))
                throw new MyException.ReaderWriterException.ReaderWriterInvalidFileTypeException("Invalid file format");
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
                throw new MyException.ReaderWriterException.ReaderWriterNoAuthorizationException("No authorization");
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
                throw new MyException.ReaderWriterException.ReaderWriterNoAuthorizationException("No authorization");
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
                throw new MyException.ReaderWriterException.ReaderWriterNoAuthorizationException("No authorization");
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
                throw new MyException.ReaderWriterException.ReaderWriterNoAuthorizationException("No authorization");
            }
        }

        /// <summary>
        /// Vytvoří soubor s daným obsahem
        /// </summary>
        /// <param name="content">obsah souboru</param>
        /// <returns>název souboru</returns>
        public static string CreateTestFile(string content)
        {
            File.WriteAllText(fileName, content);

            //File.SetAttributes(fileName, FileAttributes.Hidden);

            return fileName;
        }
        
        /// <summary>
        /// Odstraní soubor
        /// </summary>
        public static void DeleteTestFile()
        {
            ReaderWriter reader = new ReaderGraph(fileName, false);
            reader.DeleteFile();
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
