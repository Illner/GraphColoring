using System;
using System.Text;
using System.Collections.Generic;

namespace GraphColoring.Graph.GraphOperation.Tests
{
    class ComplementTest : GraphColoring.Tests.ITestInterface
    {
        #region Variable
        private IGraphInterface graph;
        private String testPath;
        private ReaderWriter.IReaderGraphInterface reader;
        private StringBuilder stringBuilder;
        private Dictionary<ComplementEnum, string> testsDictionary;

        // Paths
        private string testPathGraphComplement = @"Testing\Test\GraphComplement.txt";
        private string graphComplement1 = ComplementResource.graphComplement1;
        private string graphComplement2 = ComplementResource.graphComplement2;
        private string graphComplement3 = ComplementResource.graphComplement3;
        private string graphComplement4 = ComplementResource.graphComplement4;
        #endregion
        
        #region Enum
        public enum ComplementEnum
        {
            graphComplement1,
            graphComplement2,
            graphComplement3,
            graphComplement4
        }
        #endregion
        
        #region Constructor
        public ComplementTest()
        {
            stringBuilder = new StringBuilder();

            // Fill testsDictionary
            testsDictionary = new Dictionary<ComplementEnum, string>
            {
                { ComplementEnum.graphComplement1, graphComplement1 },
                { ComplementEnum.graphComplement2, graphComplement2 },
                { ComplementEnum.graphComplement3, graphComplement3 },
                { ComplementEnum.graphComplement4, graphComplement4 }
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

            foreach (ComplementEnum complementEnum in testsDictionary.Keys)
            {
                Testing(complementEnum);
            }

            return stringBuilder;
        }

        /// <summary>
        /// Test a particular enum (file)
        /// </summary>
        /// <param name="complementEnum">enum (file)</param>
        /// <returns>report</returns>
        public StringBuilder Test(ComplementEnum complementEnum)
        {
            stringBuilder.Clear();

            Testing(complementEnum);

            return stringBuilder;
        }

        private void Testing(ComplementEnum complementEnum)
        {
            try
            {
                testPath = ReaderWriter.ReaderWriter.CreateTestFile(testsDictionary[complementEnum]);

                reader = new ReaderWriter.ReaderGraph(testPath, false);
                graph = reader.ReadFile();

                stringBuilder.AppendLine(complementEnum.ToString());
                stringBuilder.AppendLine("Graph created.");
                stringBuilder.AppendLine(graph.ToString());

                IGraphInterface graphComplement = GraphOperation.ComplementGraph(graph);
                
                stringBuilder.AppendLine("Complement graph.");
                stringBuilder.AppendLine(graphComplement.ToString());
            }
            catch (KeyNotFoundException)
            {
                throw new MyException.TestsException.TestsMissingTestException(complementEnum.ToString());
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
            return testPathGraphComplement;
        }
        #endregion
    }
}
