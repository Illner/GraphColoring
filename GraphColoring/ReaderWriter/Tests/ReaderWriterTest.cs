using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace GraphColoring.ReaderWriter.Tests
{
    class ReaderWriterTest : GraphColoring.Tests.ITestInterface
    {
        // Variable
        #region
        private StringBuilder stringBuilder;
        private string readerWriterText = "Lorem impsum.";
        private Dictionary<PathEnum, string> testsDictionary;

        // Paths
        private string testPathReaderWriter = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Test\ReaderWriter.txt";
        private string readerWriterPathValid = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\ReaderWriter\Example.graph";
        private string readerWriterPathInvalid = @"Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\ReaderWriter/Example.graph";
        private string readerWriterPathInvalidType = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\ReaderWriter\Example.txt";
        private string readerWriterPathFolder = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\ReaderWriter\";
        private string readerWriterPathAuthorization = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\ReaderWriter\ExampleAuthorization.graph";
        private string readerWriterPathEmpty = @"";

        // Instance
        private ReaderWriter readerWriter;
        #endregion

        // Enum
        #region
        public enum PathEnum
        {
            valid,
            invalid,
            invalidType,
            folder,
            authorization,
            empty
        }

        public enum OperationEnum
        {
            deleteFile,
            createFile,
            clearFile,
            existFile,
            writeFile,
            readFile
        }
        #endregion

        // Constructor
        #region
        public ReaderWriterTest()
        {
            stringBuilder = new StringBuilder();

            // Fill testsDictionary
            testsDictionary = new Dictionary<PathEnum, string>
            {
                { PathEnum.valid, readerWriterPathValid },
                { PathEnum.invalid, readerWriterPathInvalid },
                { PathEnum.invalidType, readerWriterPathInvalidType },
                { PathEnum.folder, readerWriterPathFolder },
                { PathEnum.authorization, readerWriterPathAuthorization },
                { PathEnum.empty, readerWriterPathEmpty }
            };
        }
        #endregion

        // Method
        #region
        /// <summary>
        /// Otestuje všechny typy souborů (PathEnum)
        /// </summary>
        /// <returns>Vrátí report</returns>
        public StringBuilder Test()
        {
            stringBuilder.Clear();

            foreach (PathEnum pathEnum in testsDictionary.Keys)
            {
                Testing(pathEnum);
            }

            return stringBuilder;
        }

        /// <summary>
        /// Otestuje daný typ souboru (PathEnum)
        /// </summary>
        /// <param name="pathEnum">Typ souboru, který chceme otestovat</param>
        /// <returns>Vrátí report</returns>
        public StringBuilder Test(PathEnum pathEnum)
        {
            stringBuilder.Clear();

            Testing(pathEnum);

            return stringBuilder;
        }

        /// <summary>
        /// Otestuje daný typ operace (OperationEnum) na daný typ souboru (pathEnum)
        /// </summary>
        /// <param name="operationEnum"></param>
        /// <param name="pathEnum"></param>
        /// <returns></returns>
        public StringBuilder Test(OperationEnum operationEnum, PathEnum pathEnum)
        {
            stringBuilder.Clear();

            string path = testsDictionary[pathEnum];
            readerWriter = new Reader(path);

            switch (operationEnum)
            {
                case OperationEnum.clearFile:
                    ClearFile();
                    break;
                case OperationEnum.createFile:
                    CreateFile();
                    break;
                case OperationEnum.deleteFile:
                    DeleteFile();
                    break;
                case OperationEnum.existFile:
                    ExistFile();
                    break;
                case OperationEnum.readFile:
                    ReadFile();
                    break;
                case OperationEnum.writeFile:
                    WriteFile();
                    break;
                default:
                    throw new MyException.TestsMissingTestException(operationEnum.ToString());
            }

            return stringBuilder;
        }

        private void Testing(PathEnum pathEnum)
        {
            try
            {
                string path = testsDictionary[pathEnum];
                readerWriter = new Reader(path);
                Testing();
            }
            catch (KeyNotFoundException)
            {
                throw new MyException.TestsMissingTestException(pathEnum.ToString());
            }
            catch (MyException.ReaderWriterException e)
            {
                stringBuilder.AppendLine(e.Message);
            }
        }

        /// <summary>
        /// Na daný soubor provede řadu operací
        /// </summary>
        private void Testing()
        {
            try
            {
                stringBuilder.AppendLine("Path: " + readerWriter.GetPath());

                // First iteration
                stringBuilder.AppendLine("First iteration");

                ExistFile();
                DeleteFile();
                ExistFile();
                CreateFile();
                ExistFile();
                WriteFile();
                ReadFile();
                ClearFile();
                ExistFile();
                ReadFile();
                ExistFile();

                // Second iteration
                stringBuilder.AppendLine("Second iteration");

                ExistFile();
                DeleteFile();
                WriteFile();
                ReadFile();
                ClearFile();
                ExistFile();

                // Third iteration
                stringBuilder.AppendLine("Third iteration");

                ExistFile();
                DeleteFile();
                ExistFile();
                CreateFile();
                ExistFile();
                WriteFile();
                ReadFile();
                ClearFile();
                ExistFile();
                ReadFile();
                ExistFile();
            }
            catch (MyException.ReaderWriterException e)
            {
                stringBuilder.AppendLine(e.Message);
            }
        }

        /// <summary>
        /// Odstraní soubor
        /// </summary>
        private void DeleteFile()
        {
            stringBuilder.AppendLine("Deleting file");

            readerWriter.DeleteFile();

            stringBuilder.AppendLine("File was deleted");
        }

        /// <summary>
        /// Vytvoří soubor
        /// </summary>
        private void CreateFile()
        {
            stringBuilder.AppendLine("Creating file");

            readerWriter.CreateFile();

            stringBuilder.AppendLine("File was created");
        }

        /// <summary>
        /// Odstraní obsah souboru
        /// </summary>
        private void ClearFile()
        {
            stringBuilder.AppendLine("Clearing file");

            readerWriter.ClearFile();

            stringBuilder.AppendLine("File was cleared");
        }

        /// <summary>
        /// Zjistí, zda soubor existuje
        /// </summary>
        private void ExistFile()
        {
            Boolean exists = readerWriter.ExistsFile();
            
            if (exists)
                stringBuilder.AppendLine("File exists");
            else
                stringBuilder.AppendLine("File doesn't exist");
        }

        /// <summary>
        /// Zapíše do souboru text
        /// </summary>
        private void WriteFile()
        {
            if (!readerWriter.ExistsFile())
                return;

            stringBuilder.AppendLine("Writing to file");

            using (StreamWriter streamWriter = new StreamWriter(readerWriter.GetPath()))
            {
                streamWriter.WriteLine(readerWriterText);
            }

            stringBuilder.AppendLine("Text (" + readerWriterText + ") was written to file");
        }

        /// <summary>
        /// Zobrazí obsah souboru
        /// </summary>
        private void ReadFile()
        {
            if (!readerWriter.ExistsFile())
                return;

            stringBuilder.AppendLine("Reading file");

            string content = "";
            using (StreamReader streamReader = new StreamReader(readerWriter.GetPath()))
            {
                content = streamReader.ReadLine();
            }

            stringBuilder.AppendLine("File contents: " + content);
        }
        #endregion

        // Property
        #region
        public string GetPath()
        {
            return testPathReaderWriter;
        }
        #endregion
    }
}