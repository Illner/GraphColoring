using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace GraphColoring.Tests
{
    class Tests
    {
        // Variable
        #region
        private Boolean consolePrint;
        private StringBuilder stringBuilder;
        private Dictionary<TestEnum, Tuple<ITestInterface, string>> testsDictionary;

        // Paths
        private string testPath;

        // Instance test
        ReaderWriter.Tests.ReaderWriterTest readerWriterTest = new ReaderWriter.Tests.ReaderWriterTest();
        ReaderWriter.Tests.ReaderTest readerTest = new ReaderWriter.Tests.ReaderTest();
        Graph.GraphProperty.Tests.ComponentTest graphComponentTest = new Graph.GraphProperty.Tests.ComponentTest();
        Graph.GraphProperty.Tests.DegreeSequenceTest graphDegreeSequenceTest = new Graph.GraphProperty.Tests.DegreeSequenceTest();
        Graph.GraphProperty.Tests.CycleTest graphCycleTest = new Graph.GraphProperty.Tests.CycleTest();
        Graph.GraphClass.Tests.ClassTest graphClassTest = new Graph.GraphClass.Tests.ClassTest();
        Graph.GraphProperty.Tests.SpanningTreeTest graphSpanningTreeTest = new Graph.GraphProperty.Tests.SpanningTreeTest();
        Graph.GraphModification.Tests.ModificationTest graphModificationTest = new Graph.GraphModification.Tests.ModificationTest();
        Graph.GraphOperation.Tests.ComplementTest graphComplementTest = new Graph.GraphOperation.Tests.ComplementTest();
        Graph.GraphOperation.Tests.CopyTest graphCopyTest = new Graph.GraphOperation.Tests.CopyTest();
        Graph.GraphOperation.Tests.SubGraphTest subGraphTest = new Graph.GraphOperation.Tests.SubGraphTest();
        Graph.GraphOperation.Tests.LineGraphTest lineGraphTest = new Graph.GraphOperation.Tests.LineGraphTest();
        Graph.ColoredGraph.Tests.ColoredGraphTest coloredGraphTest = new Graph.ColoredGraph.Tests.ColoredGraphTest();
        ReaderWriter.Tests.WriterTest writerTest = new ReaderWriter.Tests.WriterTest();
        #endregion

        // Enum
        #region
        public enum TestEnum
        {
            readerWriter,
            reader,
            graphComponent,
            graphDegreeSequence,
            graphCycle,
            graphClass,
            graphSpanningTree,
            graphModification,
            graphComplement,
            graphCopy,
            graphSubGraph,
            graphLineGraph,
            coloredGraph,
            writer
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

            // Fill testsDictionary
            testsDictionary = new Dictionary<TestEnum, Tuple<ITestInterface, string>>
            {
                { TestEnum.reader, new Tuple<ITestInterface, string>(readerTest, TestResource.ReaderStandard) },
                { TestEnum.graphComponent, new Tuple<ITestInterface, string>(graphComponentTest, TestResource.GraphComponentStandard) },
                { TestEnum.graphDegreeSequence, new Tuple<ITestInterface, string>(graphDegreeSequenceTest, TestResource.GraphDegreeSequenceStandard) },
                { TestEnum.graphCycle, new Tuple<ITestInterface, string>(graphCycleTest, TestResource.GraphCycleStandard) },
                { TestEnum.graphClass, new Tuple<ITestInterface, string>(graphClassTest, TestResource.GraphClassStandard) },
                { TestEnum.graphSpanningTree, new Tuple<ITestInterface, string>(graphSpanningTreeTest, TestResource.GraphSpanningTreeStandard) },
                { TestEnum.graphModification, new Tuple<ITestInterface, string>(graphModificationTest, TestResource.GraphModificationStandard) },
                { TestEnum.graphComplement, new Tuple<ITestInterface, string>(graphComplementTest, TestResource.GraphComplementStandard) },
                { TestEnum.graphCopy, new Tuple<ITestInterface, string>(graphCopyTest, TestResource.GraphCopyStandard) },
                { TestEnum.graphSubGraph, new Tuple<ITestInterface, string>(subGraphTest, TestResource.GraphSubGraphStandard) },
                { TestEnum.graphLineGraph, new Tuple<ITestInterface, string>(lineGraphTest, TestResource.GraphLineGraphStandard) },
                { TestEnum.coloredGraph, new Tuple<ITestInterface, string>(coloredGraphTest, TestResource.ColoredGraphStandard) },
                { TestEnum.writer, new Tuple<ITestInterface, string>(writerTest, TestResource.WriterStandard) }
            };
        }
        #endregion

        // Method
        #region
        public void Test()
        {
            foreach (TestEnum testEnum in testsDictionary.Keys)
            {
                Testing(testEnum);
            }
        }

        public void Test(TestEnum testEnum)
        {
            Testing(testEnum);
        }

        private void Testing (TestEnum testEnum)
        {
            stringBuilder.Clear();

            ITestInterface test = testsDictionary[testEnum].Item1;

            stringBuilder = test.Test();
            stringBuilder.AppendLine();

            if (consolePrint)
            {
                String template = testsDictionary[testEnum].Item2;

                if (String.Compare(template, stringBuilder.ToString()) == 0)
                    Console.WriteLine("OK   " + testEnum.ToString());
                else
                    Console.WriteLine("NOK  " + testEnum.ToString());
            }
            else
            {
                testPath = test.GetPath();
                StreamWriter streamWriter = new StreamWriter(testPath);
                streamWriter.WriteLine(stringBuilder.ToString());
                streamWriter.Flush();
            }
        }

        /// <summary>
        /// Vytvoří soubor s daným obsahem
        /// </summary>
        /// <param name="content">obsah souboru</param>
        /// <returns>název souboru</returns>
        public static string CreateTestFile(string content)
        {
            // Variable
            string fileName = "TestFile.temp";

            File.WriteAllText(fileName, content);

            //File.SetAttributes(fileName, FileAttributes.Hidden);

            return fileName;
        }
        #endregion
    }
}
