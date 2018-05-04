using System;
using System.IO;
using System.Text;

namespace GraphColoring.Tests
{
    class Tests
    {
        // Variable
        #region
        private Boolean consolePrint;
        private StringBuilder stringBuilder;

        // Path
        private string testPath;
        private string testPathGraph = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Test\Graph.txt";
        private string testPathReaderWriter = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Test\ReaderWriter.txt";
        private string testPathReader = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Test\Reader.txt";
        private string testPathGraphComponent = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Test\GraphComponent.txt";
        private string testPathGraphDegreeSequence = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Test\GraphDegreeSequence.txt";
        #endregion

        // Enum
        #region
        private enum TestEnum
        {
            graph,
            readerWriter,
            reader,
            graphComponent,
            graphDegreeSequence
        }
        #endregion

        // Constructor
        #region
        /// <summary>
        /// consolePrint = true => výpis reportu na obrazovku
        /// consolePrint = false => výpis reportu do souboru(ů)
        /// </summary>
        public Tests(Boolean consolePrint)
        {
            this.consolePrint = consolePrint;
            stringBuilder = new StringBuilder();
        }
        #endregion

        // Method
        #region
        public void Test()
        {
            foreach (TestEnum testEnum in Enum.GetValues(typeof(TestEnum)))
            {
                Testing(testEnum);
            }
        }

        private void Testing (TestEnum testEnum)
        {
            stringBuilder.Clear();
            
            switch (testEnum)
            {
                case TestEnum.graph:
                    Graph.Tests.GraphTest graphTest = new Graph.Tests.GraphTest();
                    stringBuilder = graphTest.Test();

                    testPath = testPathGraph;
                    break;
                case TestEnum.graphComponent:
                    Graph.GraphProperty.Tests.ComponentTest graphComponentTest = new Graph.GraphProperty.Tests.ComponentTest();
                    stringBuilder = graphComponentTest.Test();

                    testPath = testPathGraphComponent;
                    break;
                case TestEnum.graphDegreeSequence:
                    Graph.GraphProperty.Tests.DegreeSequenceTest degreeSequenceTest = new Graph.GraphProperty.Tests.DegreeSequenceTest();
                    stringBuilder = degreeSequenceTest.Test();

                    testPath = testPathGraphDegreeSequence;
                    break;
                case TestEnum.reader:
                    ReaderWriter.Tests.ReaderTest readerTest = new ReaderWriter.Tests.ReaderTest();
                    stringBuilder = readerTest.Test();

                    testPath = testPathReader;
                    break;
                case TestEnum.readerWriter:
                    ReaderWriter.Tests.ReaderWriterTest readerWriterTest = new ReaderWriter.Tests.ReaderWriterTest();
                    stringBuilder = readerWriterTest.Test();

                    testPath = testPathReaderWriter;
                    break;
                default:
                    stringBuilder.AppendLine("This isn't implemented!");
                    break;
            }

            if (consolePrint)
                Console.WriteLine(stringBuilder);
            else
            {
                StreamWriter streamWriter = new StreamWriter(testPath);
                streamWriter.WriteLine(stringBuilder.ToString());
                streamWriter.Flush();
            }
        }
        #endregion
    }
}
