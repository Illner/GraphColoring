﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring.ReaderWriter.Tests
{
    class WriterTest : GraphColoring.Tests.ITestInterface
    {
        // Variable
        #region
        private Reader reader;
        private Writer writer;
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
        /// Otestuje všechny typy souborů (PathEnum)
        /// </summary>
        /// <returns>Vrátí report</returns>
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
        /// Otestuje daný typ souboru (PathEnum)
        /// </summary>
        /// <param name="pathEnum">Typ souboru, který chceme otestovat</param>
        /// <returns>Vrátí report</returns>
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
                testPath = GraphColoring.Tests.Tests.CreateTestFile(testsDictionary[pathEnum]);
                reader = new Reader(testPath, false);
                writer = new Writer(testPath, false);

                stringBuilder.AppendLine(pathEnum.ToString());
                try
                {
                    switch (pathEnum)
                    {
                        case PathEnum.write:
                            graph = reader.ReadFile();
                            randomSequence = new GraphColoringAlgorithm.SequenceAlgorithm.RandomSequence.RandomSequence(graph);
                            randomSequence.Color();
                            writer.WriteFile(graph, GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequence, false);
                            reader.ReadFile();
                            stringBuilder.AppendLine("OK");
                            break;
                        case PathEnum.recordExists:
                            graph = reader.ReadFile();
                            largestFirstSequence = new GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence(graph);
                            largestFirstSequence.Color();
                            if (!writer.WriteFile(graph, GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequence, false))
                                stringBuilder.AppendLine("OK");
                            else
                                stringBuilder.AppendLine("NOK");
                            reader.ReadFile();
                            break;
                        case PathEnum.first:
                            graph = reader.ReadFile();
                            randomSequence = new GraphColoringAlgorithm.SequenceAlgorithm.RandomSequence.RandomSequence(graph);
                            randomSequence.Color();
                            writer.WriteFile(graph, GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequence, false);
                            reader.ReadFile();
                            stringBuilder.AppendLine("OK");
                            break;
                        default:
                            stringBuilder.AppendLine("This pathEnum isn't implemented!");
                            break;
                    }
                }
                catch (MyException.ReaderWriterException e)
                {
                    stringBuilder.AppendLine(e.Message);
                }
                catch (MyException.GraphException e)
                {
                    stringBuilder.AppendLine(e.Message);
                }
            }
            catch (KeyNotFoundException)
            {
                throw new MyException.TestsMissingTestException(pathEnum.ToString());
            }
            catch (MyException.ReaderWriterException e)
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
