﻿using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

// No active // 

namespace GraphColoring.ReaderWriter.Tests
{
    class ReaderWriterTest : GraphColoring.Tests.ITestInterface
    {
        #region Variable
        private StringBuilder stringBuilder;
        private string readerWriterText = "Lorem impsum.";
        private Dictionary<PathEnum, string> testsDictionary;

        // Paths
        // EDIT: Invalid paths
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
        
        #region Enum
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
        
        #region Constructor
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
        
        #region Method
        /// <summary>
        /// Test all value of enums (files)
        /// </summary>
        /// <returns>report</returns>
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
        /// Test a particular enum (file)
        /// </summary>
        /// <param name="pathEnum">enum (file) which we want to test</param>
        /// <returns>report</returns>
        public StringBuilder Test(PathEnum pathEnum)
        {
            stringBuilder.Clear();

            Testing(pathEnum);

            return stringBuilder;
        }

        /// <summary>
        /// Test a operation (OperationEnum) on a file (pathEnum)
        /// </summary>
        /// <param name="operationEnum">type of operation (enum)</param>
        /// <param name="pathEnum">file path (enum)</param>
        /// <returns>report</returns>
        public StringBuilder Test(OperationEnum operationEnum, PathEnum pathEnum)
        {
            stringBuilder.Clear();

            string path = testsDictionary[pathEnum];
            readerWriter = new ReaderGraph(path);

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
                    throw new MyException.TestsException.TestsMissingTestException(operationEnum.ToString());
            }

            return stringBuilder;
        }

        private void Testing(PathEnum pathEnum)
        {
            try
            {
                string path = testsDictionary[pathEnum];
                readerWriter = new ReaderGraph(path);
                Testing();
            }
            catch (KeyNotFoundException)
            {
                throw new MyException.TestsException.TestsMissingTestException(pathEnum.ToString());
            }
            catch (MyException.ReaderWriterException.ReaderWriterException e)
            {
                stringBuilder.AppendLine(e.Message);
            }
        }

        /// <summary>
        /// Apply some operations on a file
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
            catch (MyException.ReaderWriterException.ReaderWriterException e)
            {
                stringBuilder.AppendLine(e.Message);
            }
        }

        /// <summary>
        /// Remove a file
        /// </summary>
        private void DeleteFile()
        {
            stringBuilder.AppendLine("Deleting file");

            readerWriter.DeleteFile();

            stringBuilder.AppendLine("File was deleted");
        }

        /// <summary>
        /// Create a file
        /// </summary>
        private void CreateFile()
        {
            stringBuilder.AppendLine("Creating file");

            readerWriter.CreateFile();

            stringBuilder.AppendLine("File was created");
        }

        /// <summary>
        /// Remove a content of a file
        /// </summary>
        private void ClearFile()
        {
            stringBuilder.AppendLine("Clearing file");

            readerWriter.ClearFile();

            stringBuilder.AppendLine("File was cleared");
        }

        /// <summary>
        /// Check if a file exists
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
        /// Write a text to a file
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
        /// Show a content of a file
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
        
        #region Property
        public string GetPath()
        {
            return testPathReaderWriter;
        }
        #endregion
    }
}