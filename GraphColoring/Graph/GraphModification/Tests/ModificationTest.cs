using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GraphColoring.Graph.GraphModification.Tests
{
    class ModificationTest : GraphColoring.Tests.ITestInterface
    {
        // Variable
        #region
        private Graph graph;
        private String testPath;
        private ReaderWriter.Reader reader;
        private StringBuilder stringBuilder;
        private Dictionary<ModificationEnum, string> testsDictionary;

        // Paths
        private string testPathGraphModification = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Test\GraphModification.txt";
        private string graphModification1Path = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\Modification\graphModification.graph";
        #endregion

        // Enum
        #region
        public enum ModificationEnum
        {
            valid,
            invalid
        }
        #endregion

        // Constructor
        #region
        public ModificationTest()
        {
            stringBuilder = new StringBuilder();

            // Fill testsDictionary
            testsDictionary = new Dictionary<ModificationEnum, string>
            {
                { ModificationEnum.valid, graphModification1Path },
                { ModificationEnum.invalid, graphModification1Path }
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

            foreach (ModificationEnum modificationEnum in testsDictionary.Keys)
            {
                Testing(modificationEnum);
            }

            return stringBuilder;
        }

        /// <summary>
        /// Otestuje daný typ grafu
        /// </summary>
        /// <param name="modificationEnum">daný typ grafu</param>
        /// <returns>Vrátí report</returns>
        public StringBuilder Test(ModificationEnum modificationEnum)
        {
            stringBuilder.Clear();

            Testing(modificationEnum);

            return stringBuilder;
        }

        private void Testing(ModificationEnum modificationEnum)
        { 
            try
            {
                testPath = testsDictionary[modificationEnum];

                reader = new ReaderWriter.Reader(testPath);
                graph = reader.ReadFile();

                stringBuilder.AppendLine(modificationEnum.ToString());
                stringBuilder.AppendLine("Graph created.");
                stringBuilder.AppendLine(graph.ToString());

                switch(modificationEnum)
                {
                    case ModificationEnum.valid:
                        Valid(graph);
                        break;
                    case ModificationEnum.invalid:
                        Invalid(graph);
                        break;
                    default:
                        throw new MyException.TestsMissingTestException(modificationEnum.ToString());
                }

                stringBuilder.AppendLine("Graph modified.");
                stringBuilder.AppendLine(graph.ToString());
            }
            catch (KeyNotFoundException)
            {
                throw new MyException.TestsMissingTestException(modificationEnum.ToString());
            }
        }

        private void Valid(Graph graph)
        {
            // Variable
            Vertex vertex1 = new Vertex("Vrchol 1");
            Vertex vertex2 = new Vertex("Vrchol 2");
            Vertex vertex3 = new Vertex("Vrchol 3");
            List<Vertex> vertexList = graph.AllVertices();
            Vertex lastVertex = vertexList.Last();
            List<Vertex> vertex2DegreeList = new List<Vertex>();

            graph.VertexAdd(vertex1);
            graph.VertexAdd(vertex2);
            graph.VertexAdd(vertex3);
            
            graph.EdgeAdd(new Edge(vertex1, vertex2));
            graph.EdgeAdd(new Edge(vertex2, vertex3));
            graph.EdgeAdd(new Edge(vertex1, vertexList.First()));
            graph.EdgeAdd(new Edge(vertex3, vertexList.First()));
            graph.EdgeAdd(new Edge(vertex1, vertexList.Last()));
            graph.EdgeAdd(new Edge(vertex3, vertexList.Last()));

            graph.VertexSuppression(vertex2);
            
            graph.EdgeContract(new Edge(vertex1, vertex3));

            graph.VertexContract(lastVertex);
            vertexList = graph.AllVertices();

            graph.EdgeContract(new Edge(vertexList.First(), vertexList.Last()));
            vertexList = graph.AllVertices();

            graph.VertexDelete(vertexList.First());
            
            vertexList = graph.AllVertices();
            graph.EdgeDelete(new Edge(vertexList.First(), vertexList.Last()));

            graph.VertexContract(vertexList.First());
            graph.VertexDelete(vertexList.Last());
            vertexList = graph.AllVertices();

            graph.EdgeSubdivision(new Edge(vertexList.First(), vertexList.Last()));
            vertexList = graph.AllVertices();

            graph.VertexContract(vertexList.First());
            vertexList = graph.AllVertices();

            graph.EdgeContract(new Edge(vertexList.First(), vertexList.Last()));
            vertexList = graph.AllVertices();

            graph.VertexDelete(vertexList.First());
            vertexList = graph.AllVertices();

            graph.VertexAdd(new Vertex());
            vertexList = graph.AllVertices();

            graph.VertexContract(vertexList.First());
            graph.VertexAdd(new Vertex());
            vertexList = graph.AllVertices();

            graph.EdgeAdd(new Edge(vertexList.First(), vertexList.Last()));
            graph.EdgeDelete(new Edge(vertexList.First(), vertexList.Last()));
            graph.EdgeAdd(new Edge(vertexList.First(), vertexList.Last()));

            graph.EdgeContract(new Edge(vertexList.First(), vertexList.Last()));
        }

        private void Invalid(Graph graph)
        {
            List<Vertex> vertexList = graph.AllVertices();
            List<Vertex> vertexNot2DegreeList = new List<Vertex>();
            vertexNot2DegreeList = vertexList.Where(v => graph.CountNeighbours(v) != 2).ToList();
            Edge edge = new Edge(vertexList.First(), graph.Neighbours(vertexList.First()).First());

            // Vertex add
            stringBuilder.AppendLine("Vertex add");
            stringBuilder.AppendLine("Vertex exists");
            try { graph.VertexAdd(vertexList.First()); }
            catch(MyException.GraphException e) { stringBuilder.AppendLine(e.Message); }

            // Vertex delete
            stringBuilder.AppendLine("Vertex delete");
            stringBuilder.AppendLine("Vertex doesn't exist");
            try { graph.VertexDelete(new Vertex()); }
            catch (MyException.GraphException e) { stringBuilder.AppendLine(e.Message); }

            // Vertex contract
            stringBuilder.AppendLine("Vertex contract");
            stringBuilder.AppendLine("Vertex doesn't exist");
            try { graph.VertexContract(new Vertex()); }
            catch (MyException.GraphException e) { stringBuilder.AppendLine(e.Message); }

            // Vertex suppression
            stringBuilder.AppendLine("Vertex suppression");
            stringBuilder.AppendLine("Vertex doesn't exist");
            try { graph.VertexSuppression(new Vertex()); }
            catch (MyException.GraphException e) { stringBuilder.AppendLine(e.Message); }
            stringBuilder.AppendLine("Invalid vertex degree");
            try { graph.VertexSuppression(vertexNot2DegreeList.First()); }
            catch (MyException.GraphException e) { stringBuilder.AppendLine(e.Message); }

            // Edge add
            stringBuilder.AppendLine("Edge add");
            stringBuilder.AppendLine("Edge exists");
            try { graph.EdgeAdd(edge); }
            catch (MyException.GraphException e) { stringBuilder.AppendLine(e.Message); }

            // Edge delete
            stringBuilder.AppendLine("Edge delete");
            stringBuilder.AppendLine("Edge doesn't exist");
            try { graph.EdgeDelete(new Edge(new Vertex(), new Vertex())); }
            catch (MyException.GraphException e) { stringBuilder.AppendLine(e.Message); }

            // Edge contract 
            stringBuilder.AppendLine("Edge contract");
            stringBuilder.AppendLine("Edge doesn't exist");
            try { graph.EdgeContract(new Edge(new Vertex(), new Vertex())); }
            catch (MyException.GraphException e) { stringBuilder.AppendLine(e.Message); }

            // Edge subdivision
            stringBuilder.AppendLine("Edge subdivision");
            stringBuilder.AppendLine("Edge doesn't exist");
            try { graph.EdgeSubdivision(new Edge(new Vertex(), new Vertex())); }
            catch (MyException.GraphException e) { stringBuilder.AppendLine(e.Message); }
        }
        #endregion

        // Property
        #region
        public string GetPath()
        {
            return testPathGraphModification;
        }
        #endregion
    }
}
