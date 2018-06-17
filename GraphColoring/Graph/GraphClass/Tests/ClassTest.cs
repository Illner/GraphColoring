using System;
using System.Text;
using System.Collections.Generic;
namespace GraphColoring.Graph.GraphClass.Tests
{
    class ClassTest : GraphColoring.Tests.ITestInterface
    {
        // Variable
        #region
        private Graph graph;
        private String testPath;
        private ReaderWriter.Reader reader;
        private StringBuilder stringBuilder;
        private Dictionary<ClassEnum, string> testsDictionary;

        // Paths
        private string testPathGraphClass = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Test\GraphClass.txt";
        private string graphClass1Path = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Class\graphClass1.graph";
        private string graphClass2Path = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Class\graphClass2.graph";
        private string graphClass3Path = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Class\graphClass3.graph";
        private string graphClass4Path = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Class\graphClass4.graph";
        private string graphClass5Path = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Class\graphClass5.graph";
        private string graphClass6Path = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Class\graphClass6.graph";
        private string graphClass7Path = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Class\graphClass7.graph";
        private string graphClass8Path = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Class\graphClass8.graph";
        #endregion

        // Enum
        #region
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

        // Constructor
        #region
        public ClassTest()
        {
            stringBuilder = new StringBuilder();

            // Fill testsDictionary
            testsDictionary = new Dictionary<ClassEnum, string>
            {
                { ClassEnum.graphClass1, graphClass1Path },
                { ClassEnum.graphClass2, graphClass2Path },
                { ClassEnum.graphClass3, graphClass3Path },
                { ClassEnum.graphClass4, graphClass4Path },
                { ClassEnum.graphClass5, graphClass5Path },
                { ClassEnum.graphClass6, graphClass6Path },
                { ClassEnum.graphClass7, graphClass7Path },
                { ClassEnum.graphClass8, graphClass8Path }
            };
        }
        #endregion

        // Method
        #region
        /// <summary>
        /// Otestuje všechny typy grafů
        /// </summary>
        /// <returns>Vrátí report</returns>
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
        /// Otestuje daný typ grafu
        /// </summary>
        /// <param name="graphEnum">daný typ grafu</param>
        /// <returns>Vrátí report</returns>
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
                testPath = testsDictionary[classEnum];
                
                reader = new ReaderWriter.Reader(testPath);
                graph = reader.ReadFile();

                stringBuilder.AppendLine(classEnum.ToString());
                stringBuilder.AppendLine("Graph created.");
                stringBuilder.AppendLine(graph.ToString()); 

                GraphClass.GraphClassEnum graphClassEnum = GraphClass.GetGraphClass(graph);

                stringBuilder.AppendLine("Graph class: " + graphClassEnum.ToString());
            }
            catch (KeyNotFoundException)
            {
                throw new MyException.TestsMissingTestException(classEnum.ToString());
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
            return testPathGraphClass;
        }
        #endregion
    }
}