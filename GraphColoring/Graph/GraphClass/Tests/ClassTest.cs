﻿using System;
using System.Text;
using System.Collections.Generic;
namespace GraphColoring.Graph.GraphClass.Tests
{
    class ClassTest : GraphColoring.Tests.ITestInterface
    {
        #region Variable
        private IGraphInterface graph;
        private String testPath;
        private ReaderWriter.IReaderGraphInterface reader;
        private StringBuilder stringBuilder;
        private Dictionary<ClassEnum, string> testsDictionary;

        // Paths
        private string testPathGraphClass = @"Testing\Test\GraphClass.txt";
        private string graphClass1 = ClassResource.graphClass1;
        private string graphClass2 = ClassResource.graphClass2;
        private string graphClass3 = ClassResource.graphClass3;
        private string graphClass4 = ClassResource.graphClass4;
        private string graphClass5 = ClassResource.graphClass5;
        private string graphClass6 = ClassResource.graphClass6;
        private string graphClass7 = ClassResource.graphClass7;
        private string graphClass8 = ClassResource.graphClass8;
        #endregion
        
        #region Enum
        public enum ClassEnum
        {
            graphClass1,
            graphClass2,
            graphClass3,
            graphClass4,
            graphClass5,
            graphClass6,
            graphClass7,
            graphClass8
        }
        #endregion
        
        #region Constructor
        public ClassTest()
        {
            stringBuilder = new StringBuilder();

            // Fill testsDictionary
            testsDictionary = new Dictionary<ClassEnum, string>
            {
                { ClassEnum.graphClass1, graphClass1 },
                { ClassEnum.graphClass2, graphClass2 },
                { ClassEnum.graphClass3, graphClass3 },
                { ClassEnum.graphClass4, graphClass4 },
                { ClassEnum.graphClass5, graphClass5 },
                { ClassEnum.graphClass6, graphClass6 },
                { ClassEnum.graphClass7, graphClass7 },
                { ClassEnum.graphClass8, graphClass8 }
            };
        }
        #endregion
        
        #region Method
        /// <summary>
        /// Test all values of enum (files)
        /// </summary>
        /// <returns>report</returns>
        public StringBuilder Test()
        {
            stringBuilder.Clear();

            foreach (ClassEnum classEnum in testsDictionary.Keys)
            {
                Testing(classEnum);
            }

            return stringBuilder;
        }

        /// <summary>
        /// Test a particular enum (file)
        /// </summary>
        /// <param name="graphEnum">enum (file)</param>
        /// <returns>report</returns>
        public StringBuilder Test(ClassEnum classEnum)
        {
            stringBuilder.Clear();

            Testing(classEnum);

            return stringBuilder;
        }

        private void Testing(ClassEnum classEnum)
        {
            try
            {
                testPath = ReaderWriter.ReaderWriter.CreateTestFile(testsDictionary[classEnum]);

                reader = new ReaderWriter.ReaderGraph(testPath, false);
                graph = reader.ReadFile();

                stringBuilder.AppendLine(classEnum.ToString());
                stringBuilder.AppendLine("Graph created.");
                stringBuilder.AppendLine(graph.ToString()); 

                GraphClass.GraphClassEnum graphClassEnum = GraphClass.GetGraphClass(graph);

                stringBuilder.AppendLine("Graph class: " + graphClassEnum.ToString());
            }
            catch (KeyNotFoundException)
            {
                throw new MyException.TestsException.TestsMissingTestException(classEnum.ToString());
            }
            catch (MyException.ReaderWriterException.ReaderWriterException e)
            {
                stringBuilder.AppendLine(e.Message);
            }
        }
        #endregion
        
        #region Property
        public string GetPath()
        {
            return testPathGraphClass;
        }
        #endregion
    }
}