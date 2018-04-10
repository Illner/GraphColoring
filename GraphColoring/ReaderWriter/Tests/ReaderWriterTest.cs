#define writeOut
using System;
using System.IO;
using System.Diagnostics;

namespace GraphColoring.ReaderWriter.Tests
{
    class ReaderWriterTest
    {
        // Variable
        ReaderWriter readerWriter;
        Stopwatch stopwatch = new Stopwatch();

        string pathValid = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\ReaderWriter\Example.graph";
        string pathEmpty = @"";
        string pathInvalid = @"Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\ReaderWriter/Example.graph";
        string pathInvalidType = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\ReaderWriter\Example.txt";
        string pathFoulder = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\ReaderWriter\";
        string pathAuthorization = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\ReaderWriter\ExampleAuthorization.graph";
        string text = "Lorem impsum.";

        public enum PathEnum
        {
            Valid,
            Invalid,
            InvalidType,
            Foulder,
            Authorization,
            Empty
        }

        /// <summary>
        /// Vytvoří instanci ReaderWriteru
        /// </summary>
        /// <param name="pathEnum">typ path - PathEnum</param>
        public ReaderWriterTest(PathEnum pathEnum)
        {
            switch (pathEnum)
            {
                case PathEnum.Valid:
                    readerWriter = new ReaderWriter(pathValid);
                    stopwatch.Start();
                    Test();
                    stopwatch.Stop();
                    Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
                    break;
                case PathEnum.Invalid:
                    readerWriter = new ReaderWriter(pathInvalid);
                    stopwatch.Start();
                    Test();
                    stopwatch.Stop();
                    Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
                    break;
                case PathEnum.InvalidType:
                    readerWriter = new ReaderWriter(pathInvalidType);
                    stopwatch.Start();
                    Test();
                    stopwatch.Stop();
                    Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
                    break;
                case PathEnum.Foulder:
                    readerWriter = new ReaderWriter(pathFoulder);
                    stopwatch.Start();
                    Test();
                    stopwatch.Stop();
                    Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
                    break;
                case PathEnum.Authorization:
                    readerWriter = new ReaderWriter(pathAuthorization);
                    stopwatch.Start();
                    Test();
                    stopwatch.Stop();
                    Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
                    break;
                case PathEnum.Empty:
                    readerWriter = new ReaderWriter(pathEmpty);
                    stopwatch.Start();
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
            Console.WriteLine("Writing out");
            #endif

            using (StreamWriter streamWriter = new StreamWriter(readerWriter.GetPath()))
            {
                streamWriter.WriteLine(text);
            }

            #if writeOut
            Console.WriteLine("Text ({0}) was written to file", text);
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
    }
}