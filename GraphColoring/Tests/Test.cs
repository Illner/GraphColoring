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
        private Dictionary<TestEnum, ITestInterface> testsDictionary;

        // Paths
        private string testPath;

        // Instance test
        Graph.Tests.GraphTest graphTest = new Graph.Tests.GraphTest();
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
        #endregion

        // Enum
        #region
        public enum TestEnum
        {
            graph,
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
            graphSubGraph
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
            testsDictionary = new Dictionary<TestEnum, ITestInterface>
            {
                { TestEnum.graph, graphTest },
                { TestEnum.readerWriter, readerWriterTest },
                { TestEnum.reader, readerTest },
                { TestEnum.graphComponent, graphComponentTest },
                { TestEnum.graphDegreeSequence, graphDegreeSequenceTest },
                { TestEnum.graphCycle, graphCycleTest },
                { TestEnum.graphClass, graphClassTest },
                { TestEnum.graphSpanningTree, graphSpanningTreeTest },
                { TestEnum.graphModification, graphModificationTest },
                { TestEnum.graphComplement, graphComplementTest },
                { TestEnum.graphCopy, graphCopyTest },
                { TestEnum.graphSubGraph, subGraphTest }
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

            ITestInterface test = testsDictionary[testEnum];

            stringBuilder = test.Test();

            if (consolePrint)
                Console.WriteLine(stringBuilder);
            else
            {
                testPath = test.GetPath();
                StreamWriter streamWriter = new StreamWriter(testPath);
                streamWriter.WriteLine(stringBuilder.ToString());
                streamWriter.Flush();
            }
        }
        #endregion
    }
}
