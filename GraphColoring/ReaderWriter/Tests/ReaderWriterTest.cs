#define writeOut
using System;
using System.IO;
using System.Diagnostics;

namespace GraphColoring.ReaderWriter.Tests
{
    class ReaderWriterTest
    {
        // Variable
        #region
        ReaderWriter readerWriter;
        Stopwatch stopwatch = new Stopwatch();

        string readerWriterPathValid = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\ReaderWriter\Example.graph";
        string readerWriterPathEmpty = @"";
        string readerWriterPathInvalid = @"Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\ReaderWriter/Example.graph";
        string readerWriterPathInvalidType = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\ReaderWriter\Example.txt";
        string readerWriterPathFoulder = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\ReaderWriter\";
        string readerWriterPathAuthorization = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\ReaderWriter\ExampleAuthorization.graph";
        string readerWriterText = "Lorem impsum.";
        #endregion

        // Enum
        #region
        public enum PathEnum
        {
            Valid,
            Invalid,
            InvalidType,
            Foulder,
            Authorization,
            Empty
        }
        #endregion

        // Constructor
        #region
        /// <summary>
        /// Vytvoří instanci ReaderWriteru
        /// </summary>
        /// <param name="pathEnum">typ path - PathEnum</param>
        public ReaderWriterTest(PathEnum pathEnum)
        {
            switch (pathEnum)
            {
                case PathEnum.Valid:
                    stopwatch.Start();
                    readerWriter = new Reader(readerWriterPathValid);
                    Test();
                    stopwatch.Stop();
                    Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
                    break;
                case PathEnum.Invalid:
                    stopwatch.Start();
                    readerWriter = new Reader(readerWriterPathInvalid);
                    Test();
                    stopwatch.Stop();
                    Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
                    break;
                case PathEnum.InvalidType:
                    stopwatch.Start();
                    readerWriter = new Reader(readerWriterPathInvalidType);
                    Test();
                    stopwatch.Stop();
                    Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
                    break;
                case PathEnum.Foulder:
                    stopwatch.Start();
                    readerWriter = new Reader(readerWriterPathFoulder);
                    Test();
                    stopwatch.Stop();
                    Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
                    break;
                case PathEnum.Authorization:
                    stopwatch.Start();
                    readerWriter = new Reader(readerWriterPathAuthorization);
                    Test();
                    stopwatch.Stop();
                    Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
                    break;
                case PathEnum.Empty:
                    stopwatch.Start();
                    readerWriter = new Reader(readerWriterPathEmpty);
                    Test();
                    stopwatch.Stop();
                    Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
                    break;
                default:
                    readerWriter = null;
                    Console.WriteLine("Isn't implemented");
                    break;
            }
        }
        #endregion

        // Method
        #region
        private void Test()
        {
            #if writeOut
            Console.WriteLine("Path: " + readerWriter.GetPath());
            #endif

            // First iteration
            #if writeOut
            Console.WriteLine("------------------------------");
            Console.WriteLine("---------------First iteration");
            Console.WriteLine("------------------------------");
            #endif

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
            #if writeOut
            Console.WriteLine("------------------------------");
            Console.WriteLine("--------------Second iteration");
            Console.WriteLine("------------------------------");
            #endif

            ExistFile();
            DeleteFile();
            WriteFile();
            ReadFile();
            ClearFile();
            ExistFile();

            // Third iteration
            #if writeOut
            Console.WriteLine("------------------------------");
            Console.WriteLine("---------------Third iteration");
            Console.WriteLine("------------------------------");
            #endif

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

        /// <summary>
        /// Odstraní soubor
        /// </summary>
        private void DeleteFile()
        {
            #if writeOut
            Console.WriteLine("-------------");
            Console.WriteLine("Deleting file");
            #endif

            readerWriter.DeleteFile();

            #if writeOut
            Console.WriteLine("File was deleted");
            #endif
        }

        /// <summary>
        /// Vytvoří soubor
        /// </summary>
        private void CreateFile()
        {
            #if writeOut
            Console.WriteLine("-------------");
            Console.WriteLine("Creating file");
            #endif

            readerWriter.CreateFile();

            #if writeOut
            Console.WriteLine("File was created");
            #endif
        }

        /// <summary>
        /// Odstraní obsah souboru
        /// </summary>
        private void ClearFile()
        {
            #if writeOut
            Console.WriteLine("-------------");
            Console.WriteLine("Clearing file");
            #endif
            readerWriter.ClearFile();

            #if writeOut
            Console.WriteLine("File was cleared");
            #endif
        }

        /// <summary>
        /// Zjistí, zda soubor existuje
        /// </summary>
        private void ExistFile()
        {
            Boolean exists = readerWriter.ExistsFile();

            #if writeOut
            Console.WriteLine("-------------");
            if (exists)
                Console.WriteLine("File exists");
            else
                Console.WriteLine("File doesn't exist");
            #endif
        }

        /// <summary>
        /// Zapíše do souboru text
        /// </summary>
        private void WriteFile()
        {
            if (!readerWriter.ExistsFile())
                return;

            #if writeOut
            Console.WriteLine("-------------");
            Console.WriteLine("Writing");
            #endif

            using (StreamWriter streamWriter = new StreamWriter(readerWriter.GetPath()))
            {
                streamWriter.WriteLine(readerWriterText);
            }

            #if writeOut
            Console.WriteLine("Text ({0}) was written to file", readerWriterText);
            #endif
        }

        /// <summary>
        /// Zobrazí obsah souboru
        /// </summary>
        private void ReadFile()
        {
            if (!readerWriter.ExistsFile())
                return;

            #if writeOut
            Console.WriteLine("-------------");
            Console.WriteLine("Reading file");
            #endif

            string content = "";
            using (StreamReader streamReader = new StreamReader(readerWriter.GetPath()))
            {
                content = streamReader.ReadLine();
            }

            #if writeOut
            Console.WriteLine("File contents: " + content);
            #endif
        }
        #endregion
    }
}