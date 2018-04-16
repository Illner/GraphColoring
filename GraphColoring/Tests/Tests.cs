using System;
using System.IO;

namespace GraphColoring.Tests
{
    class Tests
    {
        // Variable
        #region
        TextWriter textWriter;
        FileStream fileStream;
        StreamWriter streamWriter;

        string testsPathGraph = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph.txt";
        string testsPathReader = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Reader.txt";
        string testsPathReaderWriter = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\ReaderWriter.txt";
        #endregion

        // Enum
        #region
        public enum TestEnum
        {
            Graph,
            Reader,
            ReaderWriter
        }
        #endregion

        // Constructor
        #region
        public Tests(TestEnum testEnum)
        {
            Test(testEnum);
        }

        public Tests()
        {
            foreach (TestEnum testEnum in Enum.GetValues(typeof(TestEnum)))
            {
                Test(testEnum);
            }
        }
        #endregion

        // Method
        #region
        private void Test(TestEnum testEnum)
        {
            Console.Clear();

            switch (testEnum)
            {
                case TestEnum.Graph:
                    textWriter = Console.Out;
                    fileStream = new FileStream(testsPathGraph, FileMode.OpenOrCreate);
                    streamWriter = new StreamWriter(fileStream);
                    fileStream.Flush();
                    Console.SetOut(streamWriter);

                    Graph.Tests.GraphTest graphTest = new Graph.Tests.GraphTest();

                    Console.SetOut(textWriter);
                    streamWriter.Close();
                    fileStream.Close();
                    break;
                case TestEnum.Reader:
                    textWriter = Console.Out;
                    fileStream = new FileStream(testsPathReader, FileMode.OpenOrCreate);
                    streamWriter = new StreamWriter(fileStream);
                    fileStream.Flush();
                    Console.SetOut(streamWriter);

                    ReaderWriter.Tests.ReaderTest readerTest = new ReaderWriter.Tests.ReaderTest();

                    Console.SetOut(textWriter);
                    streamWriter.Close();
                    fileStream.Close();
                    break;
                case TestEnum.ReaderWriter:
                    textWriter = Console.Out;
                    fileStream = new FileStream(testsPathReaderWriter, FileMode.OpenOrCreate);
                    streamWriter = new StreamWriter(fileStream);
                    fileStream.Flush();
                    Console.SetOut(streamWriter);

                    ReaderWriter.Tests.ReaderWriterTest readerWriterTest = new ReaderWriter.Tests.ReaderWriterTest();

                    Console.SetOut(textWriter);
                    streamWriter.Close();
                    fileStream.Close();
                    break;
                default:
                    Console.WriteLine("Isn't implemented");
                    break;
            }
        }
        #endregion
    }
}
