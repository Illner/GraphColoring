using System;
using System.Text;
using System.Collections.Generic;

namespace GraphColoring.Graph.GraphProperty.Tests
{
    class ChordalTest : GraphColoring.Tests.ITestInterface
    {
        // Variable
        #region
        private String testPath;
        private IGraphInterface graph;
        private StringBuilder stringBuilder;
        private ReaderWriter.IReaderGraphInterface reader;
        private Dictionary<ChordalTestEnum, string> testsDictionary;

        // Paths
        private string testPathGraphChordal = @"Testing\Test\GraphChordal.txt";
        private string chordal1 = ChordalResource.graphChordal1;
        private string chordal2 = ChordalResource.graphChordal2;
        private string chordal3 = ChordalResource.graphChordal3;
        private string chordal4 = ChordalResource.graphChordal4;
        private string chordal5 = ChordalResource.graphChordal5;
        private string chordal6 = ChordalResource.graphChordal6;
        private string chordal7 = ChordalResource.graphChordal7;
        private string chordal8 = ChordalResource.graphChordal8;
        private string chordal9 = ChordalResource.graphChordal9;
        #endregion

        // Enum
        #region
        public enum ChordalTestEnum
        {
            chordal1,
            chordal2,
            nonChordal1,
            nonChordal2,
            nonChordal3,
            chordal3,
            chordal4,
            chordal5,
            nonChordal4
        }
        #endregion

        // Constructor
        #region
        public ChordalTest()
        {
            stringBuilder = new StringBuilder();

            // Fill testsDictionary
            testsDictionary = new Dictionary<ChordalTestEnum, string>
            {
                { ChordalTestEnum.chordal1, chordal1 },
                { ChordalTestEnum.chordal2, chordal2 },
                { ChordalTestEnum.chordal3, chordal6 },
                { ChordalTestEnum.chordal4, chordal7 },
                { ChordalTestEnum.chordal5, chordal8 },
                { ChordalTestEnum.nonChordal1, chordal3 },
                { ChordalTestEnum.nonChordal2, chordal4 },
                { ChordalTestEnum.nonChordal3, chordal5 },
                { ChordalTestEnum.nonChordal4, chordal9 }
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

            foreach (ChordalTestEnum chordalEnum in testsDictionary.Keys)
            {
                Testing(chordalEnum);
            }

            return stringBuilder;
        }

        /// <summary>
        /// Test a particular enum (file)
        /// </summary>
        /// <param name="chordalEnum">enum (file)</param>
        /// <returns>report</returns>
        public StringBuilder Test(ChordalTestEnum chordalEnum)
        {
            stringBuilder.Clear();

            Testing(chordalEnum);

            return stringBuilder;
        }

        private void Testing(ChordalTestEnum chordalEnum)
        {
            try
            {
                testPath = ReaderWriter.ReaderWriter.CreateTestFile(testsDictionary[chordalEnum]);

                reader = new ReaderWriter.ReaderGraph(testPath, false);
                graph = reader.ReadFile();

                stringBuilder.AppendLine(chordalEnum.ToString());
                stringBuilder.AppendLine("Graph created.");
                stringBuilder.AppendLine(graph.ToString());

                stringBuilder.AppendLine("isChordal " + graph.GetGraphProperty().GetIsChordal());

                foreach(IVertexInterface vertex in graph.GetGraphProperty().GetPerfectEliminationOrdering())
                {
                    stringBuilder.AppendLine("- " + vertex.GetIdentifier());
                }
            }
            catch (KeyNotFoundException)
            {
                throw new MyException.TestsException.TestsMissingTestException(chordalEnum.ToString());
            }
            catch (MyException.ReaderWriterException.ReaderWriterException e)
            {
                stringBuilder.AppendLine(e.Message);
            }
            catch (MyException.GraphException.GraphIsNotConnected e)
            {
                stringBuilder.AppendLine(e.Message);
            }
        }
        #endregion

        // Property
        #region
        public string GetPath()
        {
            return testPathGraphChordal;
        }
        #endregion
    }
}
