using System;
using System.Text;
using System.Collections.Generic;

namespace GraphColoring.Graph.GraphProperty.Tests
{
    class DegreeSequenceTest : GraphColoring.Tests.ITestInterface
    {
        #region Variable
        private IGraphInterface graph;
        private String testPath;
        private ReaderWriter.IReaderGraphInterface reader;
        private StringBuilder stringBuilder;
        private Dictionary<DegreeSequenceEnum, string> testsDictionary;

        // Paths
        private string testPathGraphDegreeSequence = @"Testing\Test\GraphDegreeSequence.txt";
        private string graphDegreeSequence1 = DegreeSequenceResource.graphDegreeSequence1;
        private string graphDegreeSequence2 = DegreeSequenceResource.graphDegreeSequence2;
        private string graphDegreeSequence3 = DegreeSequenceResource.graphDegreeSequence3;
        private string graphDegreeSequence4 = DegreeSequenceResource.graphDegreeSequence4;
        #endregion
        
        #region Enum
        public enum DegreeSequenceEnum
        {
            graphDegreeSequence1,
            graphDegreeSequence2,
            graphDegreeSequence3,
            graphDegreeSequence4
        }
        #endregion
        
        #region Constructor
        public DegreeSequenceTest()
        {
            stringBuilder = new StringBuilder();

            // Fill testsDictionary
            testsDictionary = new Dictionary<DegreeSequenceEnum, string>
            {
                { DegreeSequenceEnum.graphDegreeSequence1, graphDegreeSequence1 },
                { DegreeSequenceEnum.graphDegreeSequence2, graphDegreeSequence2 },
                { DegreeSequenceEnum.graphDegreeSequence3, graphDegreeSequence3 },
                { DegreeSequenceEnum.graphDegreeSequence4, graphDegreeSequence4 }
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

            foreach (DegreeSequenceEnum degreeSequenceEnum in testsDictionary.Keys)
            {
                Testing(degreeSequenceEnum);
            }

            return stringBuilder;
        }

        /// <summary>
        /// Test a particular enum (file)
        /// </summary>
        /// <param name="degreeSequenceEnum">enum (file)</param>
        /// <returns>report</returns>
        public StringBuilder Test(DegreeSequenceEnum degreeSequenceEnum)
        {
            stringBuilder.Clear();

            Testing(degreeSequenceEnum);

            return stringBuilder;
        }

        private void Testing(DegreeSequenceEnum degreeSequenceEnum)
        {
            try
            {
                testPath = ReaderWriter.ReaderWriter.CreateTestFile(testsDictionary[degreeSequenceEnum]);

                reader = new ReaderWriter.ReaderGraph(testPath, false);
                graph = reader.ReadFile();

                stringBuilder.AppendLine(degreeSequenceEnum.ToString());
                stringBuilder.AppendLine("Graph created.");
                stringBuilder.AppendLine(graph.ToString());

                // Sorted
                List<int> degreeSequenceList = graph.GetGraphProperty().GetDegreeSequenceInt(true);
                
                stringBuilder.AppendLine("Degree sequence");
                foreach (int degree in degreeSequenceList)
                {
                    stringBuilder.Append(degree + " ");
                }

                stringBuilder.AppendLine("");
                stringBuilder.AppendLine("Minimum vertex degree: " + graph.GetGraphProperty().GetMinimumVertexDegree());
                stringBuilder.AppendLine("Maximum vertex degree: " + graph.GetGraphProperty().GetMaximumVertexDegree());
                stringBuilder.AppendLine("Average vertex degree: " + graph.GetGraphProperty().GetAverageVertexDegree());
                stringBuilder.AppendLine("Is graph regular: " + graph.GetGraphProperty().GetIsRegular());

                graph.GetGraphProperty().Reset();

                // Unsorted
                degreeSequenceList = graph.GetGraphProperty().GetDegreeSequenceInt(false);

                stringBuilder.AppendLine("Degree sequence");
                foreach (int degree in degreeSequenceList)
                {
                    stringBuilder.Append(degree + " ");
                }

                stringBuilder.AppendLine("");
                stringBuilder.AppendLine("Minimum vertex degree: " + graph.GetGraphProperty().GetMinimumVertexDegree());
                stringBuilder.AppendLine("Maximum vertex degree: " + graph.GetGraphProperty().GetMaximumVertexDegree());
                stringBuilder.AppendLine("Average vertex degree: " + graph.GetGraphProperty().GetAverageVertexDegree());
                stringBuilder.AppendLine("Is graph regular: " + graph.GetGraphProperty().GetIsRegular());
            }
            catch (KeyNotFoundException)
            {
                throw new MyException.TestsException.TestsMissingTestException(degreeSequenceEnum.ToString());
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
            return testPathGraphDegreeSequence;
        }
        #endregion
    }
}
