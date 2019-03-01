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
        /// path - file path
        /// newLine - char for a new line (\n)
        /// SEPARATOR - separator in a file
        /// FILENAME - supported file name extension - ".graph"
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
        /// Initialize ReaderWriter
        /// Set file path
        /// </summary>
        /// <param name="path">file path</param>
        protected ReaderWriter(string path)
        {
            SetPath(path);
            CheckPath();
        }

        /// <summary>
        /// Initialize ReaderWriter
        /// Set file path
        /// </summary>
        /// <param name="path">file path</param>
        /// <param name="checkPath">check if the path is valid</param>
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
        /// Check if a path is valid - absolute path
        /// If the path is not valid throws ReaderWriterInavalidPathException("Invalid path")
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
        /// Delete a file
        /// If the path is not valid throws ReaderWriterInavalidPathException
        /// If we do not have a permission for modification throws ReaderWriterNoAuthorizationException
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
        /// Create a file
        /// If the path is not valid throws ReaderWriterInavalidPathException
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
        /// Delete a content in a file
        /// If the file does not exist then create one
        /// If the path is not valid throws ReaderWriterInavalidPathException
        /// If we do not have a permission for modification throws ReaderWriterNoAuthorizationException
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
        /// Check if a file exists
        /// If the path is not valid throws ReaderWriterInavalidPathException
        /// If we do not have a permission for modification throws ReaderWriterNoAuthorizationException
        /// </summary>
        /// <returns>true if the file exists, otherwise false</returns>
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
        /// Create a file with a content
        /// </summary>
        /// <param name="content">file's content</param>
        /// <returns>name of file</returns>
        public static string CreateTestFile(string content)
        {
            File.WriteAllText(fileName, content);

            //File.SetAttributes(fileName, FileAttributes.Hidden);

            return fileName;
        }
        
        /// <summary>
        /// Delete a file
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
        /// Return a char for a new line (\n)
        /// </summary>
        /// <returns>char for a new line</returns>
        public static char GetNewLine()
        {
            return newLine;
        }

        /// <summary>
        /// Set file path - setter
        /// </summary>
        /// <param name="path">file path</param>
        private void SetPath(string path)
        {
            this.path = path;
        }

        /// <summary>
        /// Return file path - getter
        /// </summary>
        /// <returns>file path</returns>
        public string GetPath()
        {
            return path;
        }
        #endregion
    }
}
