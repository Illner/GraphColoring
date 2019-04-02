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
        private bool consolePrint;
        private StringBuilder stringBuilder;
        private Dictionary<TestEnum, Tuple<ITestInterface, string>> testsDictionary;

        // Path
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
        GraphColoringAlgorithm.GeneticAlgorithm.Tests.GeneticAlgorithmTest geneticAlgorithmTest = new GraphColoringAlgorithm.GeneticAlgorithm.Tests.GeneticAlgorithmTest();
        Graph.GraphProperty.Tests.BridgesCutVerticesTest bridgesCutVerticesTest = new Graph.GraphProperty.Tests.BridgesCutVerticesTest();
        GraphVisualization.Tests.ConvertGraphToDotTest convertGraphToDotTest = new GraphVisualization.Tests.ConvertGraphToDotTest();
        Graph.GraphProperty.Tests.ChordalTest chordalTest = new Graph.GraphProperty.Tests.ChordalTest();
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
            combinationAlgorithm,
            graphBridgesCutVertices,
            convertGraphToDot,
            chordalTest,
            geneticAlgorithm    // Must be last
        }
        #endregion

        // Constructor
        #region
        /// <summary>
        /// ConsolePrint = true => print a report on the screen
        /// ConsolePrint = false => Print a report to the files
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
                { TestEnum.combinationAlgorithm, new Tuple<ITestInterface, string>(combinationAlgorithmTest, TestResource.CombinationStandard) },
                { TestEnum.graphBridgesCutVertices, new Tuple<ITestInterface, string>(bridgesCutVerticesTest, TestResource.GraphBridgesCutVerticesStandard) },
                { TestEnum.convertGraphToDot, new Tuple<ITestInterface, string>(convertGraphToDotTest, TestResource.ConverterGraphToDotStandard) },
                { TestEnum.chordalTest, new Tuple<ITestInterface, string>(chordalTest, TestResource.GraphChordalStandard) },
                { TestEnum.geneticAlgorithm, new Tuple<ITestInterface, string>(geneticAlgorithmTest, TestResource.GeneticAlgorithmStandard) }   // Must be last
        };
        }
        #endregion

        // Method
        #region
        public bool Test()
        {
            // Variable
            bool result = true;

            foreach (TestEnum testEnum in testsDictionary.Keys)
            {
                if (!Testing(testEnum))
                    result = false;
            }
            
            return result;
        }

        public bool Test(TestEnum testEnum)
        {
            return Testing(testEnum);
        }

        private bool Testing (TestEnum testEnum)
        {
            // Variable
            bool result = true;

            stringBuilder.Clear();

            ITestInterface test = testsDictionary[testEnum].Item1;

            stringBuilder = test.Test();

            // :( - Don't know why it is necessary
            if (consolePrint)
                stringBuilder.AppendLine();

            if (consolePrint)
            {
                string template = testsDictionary[testEnum].Item2;
                string testString = stringBuilder.ToString();

                // Because Windows and Unix
                template = template.Replace("\r\n", "\n");
                testString = testString.Replace("\r\n", "\n");

                if (template.Equals(testString))
                {
                    Console.WriteLine("OK   " + testEnum.ToString());
                    result = true;
                }
                else
                {
                    Console.WriteLine("NOK  " + testEnum.ToString());
                    result = false;
                }
            }
            else
            {
                Directory.CreateDirectory(Path.GetDirectoryName(test.GetPath()));

                testPath = test.GetPath();
                StreamWriter streamWriter = new StreamWriter(testPath);
                streamWriter.WriteLine(stringBuilder.ToString());
                streamWriter.Flush();
            }

            ReaderWriter.ReaderWriter.DeleteTestFile();

            return result;
        }
        #endregion
    }
}
