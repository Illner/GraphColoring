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
        private IGraphInterface graph;
        private String testPath;
        private ReaderWriter.IReaderGraphInterface reader;
        private StringBuilder stringBuilder;
        private Dictionary<ModificationEnum, string> testsDictionary;

        // Paths
        private string testPathGraphModification = @"Testing\Test\GraphModification.txt";
        private string graphModification1 = ModificationResource.graphModification;
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
                { ModificationEnum.valid, graphModification1 },
                { ModificationEnum.invalid, graphModification1 }
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
                testPath = GraphColoring.Tests.Tests.CreateTestFile(testsDictionary[modificationEnum]);
                reader = new ReaderWriter.ReaderGraph(testPath, false);
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
                        throw new MyException.TestsException.TestsMissingTestException(modificationEnum.ToString());
                }

                stringBuilder.AppendLine("Graph modified.");
                stringBuilder.AppendLine(graph.ToString());
            }
            catch (KeyNotFoundException)
            {
                throw new MyException.TestsException.TestsMissingTestException(modificationEnum.ToString());
            }
        }

        private void Valid(IGraphInterface graph)
        {
            // Variable
            List<IVertexInterface> vertexList = graph.AllVertices();

            graph.VertexContraction(vertexList.First());
            vertexList = graph.AllVertices();

            graph.VertexSuppression(vertexList.First());
            vertexList = graph.AllVertices();

            graph.VertexExpansion(vertexList.First());
            vertexList = graph.AllVertices();

            graph.VertexSuppression(vertexList.Last());
            vertexList = graph.AllVertices();

            graph.VertexDelete(vertexList.First());
            vertexList = graph.AllVertices();

            graph.VertexSuppression(vertexList.Last());
            vertexList = graph.AllVertices();

            IVertexInterface v1 = new Vertex("V1");
            graph.VertexAdd(v1);
            vertexList = graph.AllVertices();

            graph.EdgeAdd(new Edge(vertexList.First(), v1));
            vertexList = graph.AllVertices();

            graph.VertexContraction(v1);
            vertexList = graph.AllVertices();

            graph.EdgeSubdivision(new Edge(vertexList.First(), vertexList.Skip(1).First()));
            vertexList = graph.AllVertices();

            graph.EdgeAdd(new Edge(vertexList.First(), vertexList.Skip(1).First()));
            vertexList = graph.AllVertices();

            graph.EdgeContraction(new Edge(vertexList.First(), vertexList.Skip(1).First()));
            vertexList = graph.AllVertices();

            graph.EdgeDelete(new Edge(vertexList.First(), vertexList.Skip(1).First()));
            vertexList = graph.AllVertices();

            graph.VertexDelete(vertexList.Skip(1).First());
            vertexList = graph.AllVertices();

            graph.VertexSuppression(vertexList.First());
            vertexList = graph.AllVertices();

            graph.VertexDelete(vertexList.First());
            vertexList = graph.AllVertices();

            graph.VertexExpansion(vertexList.First());
            vertexList = graph.AllVertices();

            graph.EdgeContraction(new Edge(vertexList.First(), vertexList.Skip(1).First()));
            vertexList = graph.AllVertices();

            IVertexInterface v2 = new Vertex("V2");
            graph.VertexAdd(v2);
            vertexList = graph.AllVertices();

            graph.VertexDelete(vertexList.Skip(1).First());

            graph.VertexAdd(new Vertex("V3"));
        }

        private void Invalid(IGraphInterface graph)
        {
            // Variable
            List<IVertexInterface> vertexList = graph.AllVertices();
            List<IVertexInterface> vertexNot2DegreeList = new List<IVertexInterface>();
            vertexNot2DegreeList = vertexList.Where(v => graph.CountNeighbours(v) != 2).ToList();
            IEdgeInterface edge = new Edge(vertexList.First(), graph.Neighbours(vertexList.First()).First());

            // Vertex add
            stringBuilder.AppendLine("Vertex add");
            stringBuilder.AppendLine("Vertex exists");
            try { graph.VertexAdd(vertexList.First()); }
            catch(MyException.GraphException.GraphException e) { stringBuilder.AppendLine(e.Message); }

            // Vertex delete
            stringBuilder.AppendLine("Vertex delete");
            stringBuilder.AppendLine("Vertex doesn't exist");
            try { graph.VertexDelete(new Vertex()); }
            catch (MyException.GraphException.GraphException e) { stringBuilder.AppendLine(e.Message); }

            // Vertex contract
            stringBuilder.AppendLine("Vertex contract");
            stringBuilder.AppendLine("Vertex doesn't exist");
            try { graph.VertexContraction(new Vertex()); }
            catch (MyException.GraphException.GraphException e) { stringBuilder.AppendLine(e.Message); }

            // Vertex suppression
            stringBuilder.AppendLine("Vertex suppression");
            stringBuilder.AppendLine("Vertex doesn't exist");
            try { graph.VertexSuppression(new Vertex()); }
            catch (MyException.GraphException.GraphException e) { stringBuilder.AppendLine(e.Message); }
            stringBuilder.AppendLine("Invalid vertex degree");
            try { graph.VertexSuppression(vertexNot2DegreeList.First()); }
            catch (MyException.GraphException.GraphException e) { stringBuilder.AppendLine(e.Message); }

            // Vertex expansion
            stringBuilder.AppendLine("Vertex expansion");
            stringBuilder.AppendLine("Vertex doesn't exist");
            try { graph.VertexExpansion(new Vertex()); }
            catch (MyException.GraphException.GraphException e) { stringBuilder.AppendLine(e.Message); }

            // Edge add
            stringBuilder.AppendLine("Edge add");
            stringBuilder.AppendLine("Edge exists");
            try { graph.EdgeAdd(edge); }
            catch (MyException.GraphException.GraphException e) { stringBuilder.AppendLine(e.Message); }

            // Edge delete
            stringBuilder.AppendLine("Edge delete");
            stringBuilder.AppendLine("Edge doesn't exist");
            try { graph.EdgeDelete(new Edge(new Vertex(), new Vertex())); }
            catch (MyException.GraphException.GraphException e) { stringBuilder.AppendLine(e.Message); }

            // Edge contract 
            stringBuilder.AppendLine("Edge contract");
            stringBuilder.AppendLine("Edge doesn't exist");
            try { graph.EdgeContraction(new Edge(new Vertex(), new Vertex())); }
            catch (MyException.GraphException.GraphException e) { stringBuilder.AppendLine(e.Message); }

            // Edge subdivision
            stringBuilder.AppendLine("Edge subdivision");
            stringBuilder.AppendLine("Edge doesn't exist");
            try { graph.EdgeSubdivision(new Edge(new Vertex(), new Vertex())); }
            catch (MyException.GraphException.GraphException e) { stringBuilder.AppendLine(e.Message); }
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
