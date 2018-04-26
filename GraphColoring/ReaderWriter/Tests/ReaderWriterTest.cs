using System;
using System.IO;
using System.Text;

namespace GraphColoring.ReaderWriter.Tests
{
    class ReaderWriterTest : GraphColoring.Tests.ITestInterface
    {
        // Variable
        #region
        private ReaderWriter readerWriter;
        private StringBuilder stringBuilder;

        private string readerWriterPathValid = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\ReaderWriter\Example.graph";
        private string readerWriterPathEmpty = @"";
        private string readerWriterPathInvalid = @"Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\ReaderWriter/Example.graph";
        private string readerWriterPathInvalidType = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\ReaderWriter\Example.txt";
        private string readerWriterPathFoulder = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\ReaderWriter\";
        private string readerWriterPathAuthorization = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\ReaderWriter\ExampleAuthorization.graph";
        private string readerWriterText = "Lorem impsum.";
        #endregion

        // Enum
        #region
        public enum PathEnum
        {
            valid,
            invalid,
            invalidType,
            foulder,
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

            foreach (PathEnum pathEnum in Enum.GetValues(typeof(PathEnum)))
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

            switch (operationEnum)
            {
                case OperationEnum.clearFile:
                    #region
                    switch (pathEnum)
                    {
                        case PathEnum.valid:
                            readerWriter = new Reader(readerWriterPathValid);
                            ClearFile();
                            break;
                        case PathEnum.invalid:
                            readerWriter = new Reader(readerWriterPathInvalid);
                            ClearFile();
                            break;
                        case PathEnum.invalidType:
                            readerWriter = new Reader(readerWriterPathInvalidType);
                            ClearFile();
                            break;
                        case PathEnum.foulder:
                            readerWriter = new Reader(readerWriterPathFoulder);
                            ClearFile();
                            break;
                        case PathEnum.authorization:
                            readerWriter = new Reader(readerWriterPathAuthorization);
                            ClearFile();
                            break;
                        case PathEnum.empty:
                            readerWriter = new Reader(readerWriterPathEmpty);
                            ClearFile();
                            break;
                        default:
                            stringBuilder.AppendLine("This isn't implemented!");
                            break;
                    }
                    #endregion
                    break;
                case OperationEnum.createFile:
                    #region
                    switch (pathEnum)
                    {
                        case PathEnum.valid:
                            readerWriter = new Reader(readerWriterPathValid);
                            CreateFile();
                            break;
                        case PathEnum.invalid:
                            readerWriter = new Reader(readerWriterPathInvalid);
                            CreateFile();
                            break;
                        case PathEnum.invalidType:
                            readerWriter = new Reader(readerWriterPathInvalidType);
                            CreateFile();
                            break;
                        case PathEnum.foulder:
                            readerWriter = new Reader(readerWriterPathFoulder);
                            CreateFile();
                            break;
                        case PathEnum.authorization:
                            readerWriter = new Reader(readerWriterPathAuthorization);
                            CreateFile();
                            break;
                        case PathEnum.empty:
                            readerWriter = new Reader(readerWriterPathEmpty);
                            CreateFile();
                            break;
                        default:
                            stringBuilder.AppendLine("This isn't implemented!");
                            break;
                    }
                    #endregion
                    break;
                case OperationEnum.deleteFile:
                    #region
                    switch (pathEnum)
                    {
                        case PathEnum.valid:
                            readerWriter = new Reader(readerWriterPathValid);
                            DeleteFile();
                            break;
                        case PathEnum.invalid:
                            readerWriter = new Reader(readerWriterPathInvalid);
                            DeleteFile();
                            break;
                        case PathEnum.invalidType:
                            readerWriter = new Reader(readerWriterPathInvalidType);
                            DeleteFile();
                            break;
                        case PathEnum.foulder:
                            readerWriter = new Reader(readerWriterPathFoulder);
                            DeleteFile();
                            break;
                        case PathEnum.authorization:
                            readerWriter = new Reader(readerWriterPathAuthorization);
                            DeleteFile();
                            break;
                        case PathEnum.empty:
                            readerWriter = new Reader(readerWriterPathEmpty);
                            DeleteFile();
                            break;
                        default:
                            stringBuilder.AppendLine("This isn't implemented!");
                            break;
                    }
                    #endregion
                    break;
                case OperationEnum.existFile:
                    #region
                    switch (pathEnum)
                    {
                        case PathEnum.valid:
                            readerWriter = new Reader(readerWriterPathValid);
                            ExistFile();
                            break;
                        case PathEnum.invalid:
                            readerWriter = new Reader(readerWriterPathInvalid);
                            ExistFile();
                            break;
                        case PathEnum.invalidType:
                            readerWriter = new Reader(readerWriterPathInvalidType);
                            ExistFile();
                            break;
                        case PathEnum.foulder:
                            readerWriter = new Reader(readerWriterPathFoulder);
                            ExistFile();
                            break;
                        case PathEnum.authorization:
                            readerWriter = new Reader(readerWriterPathAuthorization);
                            ExistFile();
                            break;
                        case PathEnum.empty:
                            readerWriter = new Reader(readerWriterPathEmpty);
                            ExistFile();
                            break;
                        default:
                            stringBuilder.AppendLine("This isn't implemented!");
                            break;
                    }
                    #endregion
                    break;
                case OperationEnum.readFile:
                    #region
                    switch (pathEnum)
                    {
                        case PathEnum.valid:
                            readerWriter = new Reader(readerWriterPathValid);
                            ReadFile();
                            break;
                        case PathEnum.invalid:
                            readerWriter = new Reader(readerWriterPathInvalid);
                            ReadFile();
                            break;
                        case PathEnum.invalidType:
                            readerWriter = new Reader(readerWriterPathInvalidType);
                            ReadFile();
                            break;
                        case PathEnum.foulder:
                            readerWriter = new Reader(readerWriterPathFoulder);
                            ReadFile();
                            break;
                        case PathEnum.authorization:
                            readerWriter = new Reader(readerWriterPathAuthorization);
                            ReadFile();
                            break;
                        case PathEnum.empty:
                            readerWriter = new Reader(readerWriterPathEmpty);
                            ReadFile();
                            break;
                        default:
                            stringBuilder.AppendLine("This isn't implemented!");
                            break;
                    }
                    #endregion
                    break;
                case OperationEnum.writeFile:
                    #region
                    switch (pathEnum)
                    {
                        case PathEnum.valid:
                            readerWriter = new Reader(readerWriterPathValid);
                            WriteFile();
                            break;
                        case PathEnum.invalid:
                            readerWriter = new Reader(readerWriterPathInvalid);
                            WriteFile();
                            break;
                        case PathEnum.invalidType:
                            readerWriter = new Reader(readerWriterPathInvalidType);
                            WriteFile();
                            break;
                        case PathEnum.foulder:
                            readerWriter = new Reader(readerWriterPathFoulder);
                            WriteFile();
                            break;
                        case PathEnum.authorization:
                            readerWriter = new Reader(readerWriterPathAuthorization);
                            WriteFile();
                            break;
                        case PathEnum.empty:
                            readerWriter = new Reader(readerWriterPathEmpty);
                            WriteFile();
                            break;
                        default:
                            stringBuilder.AppendLine("This isn't implemented!");
                            break;
                    }
                    #endregion
                    break;
                default:
                    stringBuilder.AppendLine("This operation isn't implemented!");
                    break;
            }

            return stringBuilder;
        }

        private void Testing(PathEnum pathEnum)
        {
            try
            {
                switch (pathEnum)
                {
                    case PathEnum.valid:
                        readerWriter = new Reader(readerWriterPathValid);
                        Testing();
                        break;
                    case PathEnum.invalid:
                        readerWriter = new Reader(readerWriterPathInvalid);
                        Testing();
                        break;
                    case PathEnum.invalidType:
                        readerWriter = new Reader(readerWriterPathInvalidType);
                        Testing();
                        break;
                    case PathEnum.foulder:
                        readerWriter = new Reader(readerWriterPathFoulder);
                        Testing();
                        break;
                    case PathEnum.authorization:
                        readerWriter = new Reader(readerWriterPathAuthorization);
                        Testing();
                        break;
                    case PathEnum.empty:
                        readerWriter = new Reader(readerWriterPathEmpty);
                        Testing();
                        break;
                    default:
                        readerWriter = null;
                        stringBuilder.AppendLine("This isn't implemented!");
                        break;
                }
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
    }
}