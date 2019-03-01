using System;
using System.Text;
using System.Collections.Generic;

namespace GraphColoring.ReaderWriter.Tests
{
    class WriterTest : GraphColoring.Tests.ITestInterface
    {
        // Variable
        #region
        private IReaderGraphInterface reader;
        private WriterGraph writer;
        private String testPath;
        private StringBuilder stringBuilder;
        private Dictionary<PathEnum, string> testsDictionary;

        // Paths
        private string testPathWriter = @"Testing\Test\Writer.txt";
        private string writerGraph1 = WriterResource.graph1;
        private string writerGraph2 = WriterResource.graph2;

        #endregion

        // Enum
        #region
        public enum PathEnum
        {
            write,
            recordExists,
            first
        }
        #endregion

        // Constructor
        #region
        public WriterTest()
        {
            stringBuilder = new StringBuilder();

            // Fill testsDictionary
            testsDictionary = new Dictionary<PathEnum, string>
            {
                { PathEnum.write, writerGraph1 },
                { PathEnum.recordExists, writerGraph1 },
                { PathEnum.first, writerGraph2 }
            };
        }
        #endregion

        // Method
        #region
        /// <summary>
        /// Test all values of enum (files)
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

        private void Testing(PathEnum pathEnum)
        {
            try
            {
                Graph.IGraphInterface graph;
                GraphColoringAlgorithm.SequenceAlgorithm.RandomSequence.RandomSequence randomSequence;
                GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence largestFirstSequence;
                testPath = ReaderWriter.CreateTestFile(testsDictionary[pathEnum]);
                reader = new ReaderGraph(testPath, false);
                writer = new WriterGraph(testPath, false);

                stringBuilder.AppendLine(pathEnum.ToString());
                try
                {
                    switch (pathEnum)
                    {
                        case PathEnum.write:
                            graph = reader.ReadFile();
                            randomSequence = new GraphColoringAlgorithm.SequenceAlgorithm.RandomSequence.RandomSequence(graph);
                            randomSequence.Color();
                            writer.WriteFileColor(graph, GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequence, false);
                            reader.ReadFile();
                            stringBuilder.AppendLine("OK");
                            break;
                        case PathEnum.recordExists:
                            graph = reader.ReadFile();
                            largestFirstSequence = new GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence(graph);
                            largestFirstSequence.Color();
                            if (!writer.WriteFileColor(graph, GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequence, false))
                                stringBuilder.AppendLine("OK");
                            else
                                stringBuilder.AppendLine("NOK");
                            reader.ReadFile();
                            break;
                        case PathEnum.first:
                            graph = reader.ReadFile();
                            randomSequence = new GraphColoringAlgorithm.SequenceAlgorithm.RandomSequence.RandomSequence(graph);
                            randomSequence.Color();
                            writer.WriteFileColor(graph, GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequence, false);
                            reader.ReadFile();
                            stringBuilder.AppendLine("OK");
                            break;
                        default:
                            stringBuilder.AppendLine("This pathEnum isn't implemented!");
                            break;
                    }
                }
                catch (MyException.ReaderWriterException.ReaderWriterException e)
                {
                    stringBuilder.AppendLine(e.Message);
                }
                catch (MyException.GraphException.GraphException e)
                {
                    stringBuilder.AppendLine(e.Message);
                }
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
        #endregion

        // Property
        #region
        public string GetPath()
        {
            return testPathWriter;
        }
        #endregion
    }
}
