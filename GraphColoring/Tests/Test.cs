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
        private static string fileName = "TestFile.temp";
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
        GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence.Tests.LargestFirstSequenceTest largestFirstSequenceTest = new GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence.Tests.LargestFirstSequenceTest();
        GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.Tests.SmallestLastSequenceTest smallestLastSequenceTest = new GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.Tests.SmallestLastSequenceTest();
        GraphColoringAlgorithm.Optimal.Tests.OptimalTest optimalTest = new GraphColoringAlgorithm.Optimal.Tests.OptimalTest();
        GraphColoringAlgorithm.SaturationLargestFirstSequence.Tests.SaturationLargestFirstSequenceTest saturationLargestFirstSequenceTest = new GraphColoringAlgorithm.SaturationLargestFirstSequence.Tests.SaturationLargestFirstSequenceTest();
        GraphColoringAlgorithm.SequenceAlgorithm.ConnectedSequential.Tests.ConnectedSequentialTest connectedSequentialTest = new GraphColoringAlgorithm.SequenceAlgorithm.ConnectedSequential.Tests.ConnectedSequentialTest();
        GraphColoringAlgorithm.GreedyIndependentSet.Tests.GreedyIndependentSetTest greedyIndependentSetTest = new GraphColoringAlgorithm.GreedyIndependentSet.Tests.GreedyIndependentSetTest();
        GenerateGraph.ErdosRenyiModel.Tests.ErdosRenyiModelTest generateGraphErdosRenyiModelTest = new GenerateGraph.ErdosRenyiModel.Tests.ErdosRenyiModelTest();
        GraphColoringAlgorithm.CombinationAlgorithm.Tests.CombinationAlgorithmTest combinationAlgorithmTest = new GraphColoringAlgorithm.CombinationAlgorithm.Tests.CombinationAlgorithmTest();
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
            writer,
            largestFirstSequence,
            smallestLastSequence,
            optimal,
            saturationLargestFirstSequence,
            connectedSequential,
            greedyIndependentSet,
            generateGraphErdosRanyiModel,
            combinationAlgorithm
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
                { TestEnum.writer, new Tuple<ITestInterface, string>(writerTest, TestResource.WriterStandard) },
                { TestEnum.largestFirstSequence, new Tuple<ITestInterface, string>(largestFirstSequenceTest, TestResource.LargestFirstSequenceStandard) },
                { TestEnum.smallestLastSequence, new Tuple<ITestInterface, string>(smallestLastSequenceTest, TestResource.SmallestLastSequenceStandard) },
                { TestEnum.optimal, new Tuple<ITestInterface, string>(optimalTest, TestResource.OptimalStandard) },
                { TestEnum.saturationLargestFirstSequence, new Tuple<ITestInterface, string>(saturationLargestFirstSequenceTest, TestResource.SaturationLargestFirstSequenceStandard) },
                { TestEnum.connectedSequential, new Tuple<ITestInterface, string>(connectedSequentialTest, TestResource.ConnectedSequentialStandard) },
                { TestEnum.greedyIndependentSet, new Tuple<ITestInterface, string>(greedyIndependentSetTest, TestResource.GreedyIndependentSetStandard) },
                { TestEnum.generateGraphErdosRanyiModel, new Tuple<ITestInterface, string>(generateGraphErdosRenyiModelTest, TestResource.GenerateGraphErdosRenyiModelStandard) },
                { TestEnum.combinationAlgorithm, new Tuple<ITestInterface, string>(combinationAlgorithmTest, TestResource.CombinationStandard) }
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

            // :(
            stringBuilder.AppendLine();
            stringBuilder.AppendLine();
            stringBuilder.AppendLine();

            if (consolePrint)
            {
                String template = testsDictionary[testEnum].Item2;

                if (template.Equals(stringBuilder.ToString()))
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

            DeleteTestFile();
        }

        /// <summary>
        /// Vytvoří soubor s daným obsahem
        /// </summary>
        /// <param name="content">obsah souboru</param>
        /// <returns>název souboru</returns>
        public static string CreateTestFile(string content)
        {
            File.WriteAllText(fileName, content);

            //File.SetAttributes(fileName, FileAttributes.Hidden);

            return fileName;
        }

        /// <summary>
        /// Odstraní soubor
        /// </summary>
        public static void DeleteTestFile()
        {
            ReaderWriter.ReaderWriter reader = new ReaderWriter.Reader(fileName, false);
            reader.DeleteFile();
        }
        #endregion
    }
}
