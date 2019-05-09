using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace GraphColoring.Graph.ColoredGraph.Tests
{
    class ColoredGraphTest : GraphColoring.Tests.ITestInterface
    {
        #region Variable
        private IGraphInterface graph;
        private String testPath;
        private ReaderWriter.IReaderGraphInterface reader;
        private StringBuilder stringBuilder;
        private Dictionary<ColoredGraphEnum, string> testsDictionary;

        // Paths
        private string testPathColoredGraph = @"Testing\Test\ColoredGraph.txt";
        private string coloredGraph1 = ColoredGraphResource.coloredGraph1;
        private string coloredGraph2 = ColoredGraphResource.coloredGraph2;
        private string coloredGraph3 = ColoredGraphResource.coloredGraph3;
        private string coloredGraph4 = ColoredGraphResource.coloredGraph4;
        private string coloredGraph5 = ColoredGraphResource.coloredGraph5;
        private string coloredGraph6 = ColoredGraphResource.coloredGraph6;
        #endregion
        
        #region Enum
        public enum ColoredGraphEnum
        {
            valid,
            invalid,
            interchange1,
            interchange2,
            interchange3,
            interchange4,
            interchange5
        }
        #endregion
        
        #region Constructor
        public ColoredGraphTest()
        {
            stringBuilder = new StringBuilder();

            // Fill testsDictionary
            testsDictionary = new Dictionary<ColoredGraphEnum, string>
            {
                { ColoredGraphEnum.valid, coloredGraph1 },
                { ColoredGraphEnum.invalid, coloredGraph1 },
                { ColoredGraphEnum.interchange1, coloredGraph2 },
                { ColoredGraphEnum.interchange2, coloredGraph3 },
                { ColoredGraphEnum.interchange3, coloredGraph4 },
                { ColoredGraphEnum.interchange4, coloredGraph5 },
                { ColoredGraphEnum.interchange5, coloredGraph6 }
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

            foreach (ColoredGraphEnum coloredGraphEnum in testsDictionary.Keys)
            {
                Testing(coloredGraphEnum);
            }

            return stringBuilder;
        }

        /// <summary>
        /// Test a particular enum (file)
        /// </summary>
        /// <param name="coloredGraphEnum">enum (file)</param>
        /// <returns>report</returns>
        public StringBuilder Test(ColoredGraphEnum coloredGraphEnum)
        {
            stringBuilder.Clear();

            Testing(coloredGraphEnum);

            return stringBuilder;
        }

        private void Testing(ColoredGraphEnum coloredGraphEnum)
        {
            try
            {
                testPath = ReaderWriter.ReaderWriter.CreateTestFile(testsDictionary[coloredGraphEnum]);
                reader = new ReaderWriter.ReaderGraph(testPath, false);
                graph = reader.ReadFile();

                stringBuilder.AppendLine(coloredGraphEnum.ToString());
                stringBuilder.AppendLine("Graph created.");
                stringBuilder.AppendLine(graph.ToString());

                switch (coloredGraphEnum)
                {
                    case ColoredGraphEnum.valid:
                        Valid(graph);
                        stringBuilder.AppendLine("Graph modified.");
                        break;
                    case ColoredGraphEnum.invalid:
                        Invalid(graph);
                        stringBuilder.AppendLine("Graph modified.");
                        break;
                    case ColoredGraphEnum.interchange1:
                    case ColoredGraphEnum.interchange2:
                    case ColoredGraphEnum.interchange3:
                    case ColoredGraphEnum.interchange4:
                    case ColoredGraphEnum.interchange5:
                        graph.GetColoredGraph().GreedyColoring(graph.AllVertices(), GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchange);
                        stringBuilder.AppendLine("Graph colored.");
                        break;
                    default:
                        throw new MyException.TestsException.TestsMissingTestException(coloredGraphEnum.ToString());
                }

                stringBuilder.AppendLine(graph.GetColoredGraph().ToString());
            }
            catch (KeyNotFoundException)
            {
                throw new MyException.TestsException.TestsMissingTestException(coloredGraphEnum.ToString());
            }
            catch (MyException.GraphException.GraphException e)
            {
                stringBuilder.AppendLine(e.ToString());
            }
        }

        private void Valid(IGraphInterface graph)
        {
            // Variable
            List<IVertexInterface> vertexList = graph.AllVertices();
            IColoredGraphInterface coloredGraph = graph.GetColoredGraph();
            
            if (coloredGraph.InitializeColoredGraph())
                throw new MyException.TestsException.SomethingWrongTestException();

            coloredGraph.ColorVertex(vertexList.First(), coloredGraph.GreedyColoring(vertexList.First()));

            if (coloredGraph.InitializeColoredGraph())
                throw new MyException.TestsException.SomethingWrongTestException();

            coloredGraph.ResetColorVertex(vertexList.First());

            coloredGraph.GreedyColoring(vertexList);

            if (!coloredGraph.InitializeColoredGraph())
                throw new MyException.TestsException.SomethingWrongTestException();

            coloredGraph.DeinitializationColoredGraph();
            if (!coloredGraph.InitializeColoredGraph())
                throw new MyException.TestsException.SomethingWrongTestException();
            coloredGraph.DeinitializationColoredGraph();

            coloredGraph.ResetColors();
            coloredGraph.ResetColors();

            if (coloredGraph.InitializeColoredGraph())
                throw new MyException.TestsException.SomethingWrongTestException();

            coloredGraph.ColorVertex(vertexList.First(), 1);
            coloredGraph.ColorVertex(vertexList.ElementAt(1), 1);

            if (coloredGraph.CheckValidColor().Count == 0)
                throw new MyException.TestsException.SomethingWrongTestException();

            coloredGraph.ColorVertex(vertexList.First(), 2);
            
            if (!coloredGraph.IsVertexColored(vertexList.First()))
                throw new MyException.TestsException.SomethingWrongTestException();

            if (coloredGraph.IsVertexColored(vertexList.ElementAt(2)))
                throw new MyException.TestsException.SomethingWrongTestException();

            coloredGraph.ResetColorVertex(vertexList.First());
            
            if (coloredGraph.IsVertexColored(vertexList.First()))
                throw new MyException.TestsException.SomethingWrongTestException();

            coloredGraph.ResetColorVertex(vertexList.ElementAt(2));

            if (coloredGraph.IsVertexColored(vertexList.ElementAt(2)))
                throw new MyException.TestsException.SomethingWrongTestException();

            coloredGraph.ColorVertex(vertexList.First(), 1);
            
            if (coloredGraph.CheckValidColor(vertexList.First()))
                throw new MyException.TestsException.SomethingWrongTestException();
            
            if (!coloredGraph.CheckValidColor(vertexList.ElementAt(2)))
                throw new MyException.TestsException.SomethingWrongTestException();

            if (coloredGraph.GreedyColoring(vertexList.First()) != 2)
                throw new MyException.TestsException.SomethingWrongTestException();

            if (coloredGraph.GetUnColoredVertexList().Count != 4)
                throw new MyException.TestsException.SomethingWrongTestException();

            if (coloredGraph.GetColoredVertexList().Count != 2)
                throw new MyException.TestsException.SomethingWrongTestException();
            
            if (coloredGraph.GetCountUsedColors() != 1)
                throw new MyException.TestsException.SomethingWrongTestException();

            coloredGraph.GreedyColoring(vertexList);
            
            if (coloredGraph.GetCountUsedColors() != 2)
                throw new MyException.TestsException.SomethingWrongTestException();

            coloredGraph.ColorVertex(vertexList.First(), VertexExtended.GetDefaultColor());
            
            if (coloredGraph.CheckValidColor().Count != 0)
                throw new MyException.TestsException.SomethingWrongTestException();

            coloredGraph.GreedyColoring(vertexList);
            coloredGraph.InitializeColoredGraph();

            if (!coloredGraph.GetIsInitializedColoredGraph())
                throw new MyException.TestsException.SomethingWrongTestException();

            graph.VertexAdd(new Vertex());

            if (coloredGraph.GetIsInitializedColoredGraph())
                throw new MyException.TestsException.SomethingWrongTestException();

            vertexList = graph.AllVertices();

            if (vertexList.Last().GetColor() != VertexExtended.GetDefaultColor())
                throw new MyException.TestsException.SomethingWrongTestException();

            coloredGraph.ColorVertex(vertexList.Last(), 2);
            coloredGraph.ColorVertex(vertexList.First(), 2);

            if (coloredGraph.InitializeColoredGraph())
                throw new MyException.TestsException.SomethingWrongTestException();

            coloredGraph.GreedyColoring(vertexList);

            if (!coloredGraph.InitializeColoredGraph())
                throw new MyException.TestsException.SomethingWrongTestException();

            graph.VertexDelete(vertexList.Last());
            graph.VertexAdd(new Vertex());
            vertexList = graph.AllVertices();

            if (coloredGraph.GetColoredVertexList().Count != 6)
                throw new MyException.TestsException.SomethingWrongTestException();

            if (coloredGraph.GetUnColoredVertexList().Count != 1)
                throw new MyException.TestsException.SomethingWrongTestException();

            coloredGraph.ColorVertex(vertexList.Last(), 3);

            if (coloredGraph.GetCountUsedColors() != 3)
                throw new MyException.TestsException.SomethingWrongTestException();

            graph.VertexDelete(vertexList.Last());

            if (coloredGraph.GetColoredVertexList().Count != 6)
                throw new MyException.TestsException.SomethingWrongTestException();

            if (coloredGraph.GetUnColoredVertexList().Count != 0)
                throw new MyException.TestsException.SomethingWrongTestException();

            if (coloredGraph.GetCountUsedColors() != 2)
                throw new MyException.TestsException.SomethingWrongTestException();
        }

        private void Invalid(IGraphInterface graph)
        {
            // Variable
            List<IVertexInterface> vertexList = graph.AllVertices();
            IColoredGraphInterface coloredGraph = graph.GetColoredGraph();

            coloredGraph.GreedyColoring(vertexList);
            coloredGraph.InitializeColoredGraph();

            // VertexColor - initialization
            stringBuilder.AppendLine("VertexColor - initialization");
            try { coloredGraph.ColorVertex(vertexList.First(), 2); }
            catch (MyException.GraphException.GraphException e) { stringBuilder.AppendLine(e.Message); }

            // GreedingColoring - initialization
            stringBuilder.AppendLine("GreedyColoring - initialization");
            try { coloredGraph.GreedyColoring(vertexList); }
            catch (MyException.GraphException.GraphException e) { stringBuilder.AppendLine(e.Message); }

            // ResetColorVertex - initialization
            stringBuilder.AppendLine("Reset - initialization");
            try { coloredGraph.ResetColorVertex(vertexList.First()); }
            catch (MyException.GraphException.GraphException e) { stringBuilder.AppendLine(e.Message); }

            // Reset - initialization
            stringBuilder.AppendLine("Reset - initialization");
            try { coloredGraph.ResetColors(); }
            catch (MyException.GraphException.GraphException e) { stringBuilder.AppendLine(e.Message); }

            // Initialization - initialization
            stringBuilder.AppendLine("Initialization - initialization");
            try { coloredGraph.InitializeColoredGraph(); }
            catch (MyException.GraphException.GraphException e) { stringBuilder.AppendLine(e.Message); }

            coloredGraph.DeinitializationColoredGraph();

            // Deinitialization - initialization
            stringBuilder.AppendLine("Deinitialization - initialization");
            try { coloredGraph.DeinitializationColoredGraph(); }
            catch (MyException.GraphException.GraphException e) { stringBuilder.AppendLine(e.Message); }

            // ColorVertex - Doesn't exist
            stringBuilder.AppendLine("VertexColor - Doesn't exist");
            try { coloredGraph.ColorVertex(new Vertex(), 3); }
            catch (MyException.GraphException.GraphException e) { stringBuilder.AppendLine(e.Message); }

            // GreedyColoring - Doesn't exist
            stringBuilder.AppendLine("GreedyColoring - Doesn't exist");
            try { coloredGraph.GreedyColoring(new Vertex()); }
            catch (MyException.GraphException.GraphException e) { stringBuilder.AppendLine(e.Message); }

            // CheckValidColor - Doesn't exist
            stringBuilder.AppendLine("CheckValidColor - Doesn't exist");
            try { coloredGraph.CheckValidColor(new Vertex()); }
            catch (MyException.GraphException.GraphException e) { stringBuilder.AppendLine(e.Message); }

            // IsVertexColored - Doesn't exist
            stringBuilder.AppendLine("IsVertexColored - Doesn't exist");
            try { coloredGraph.IsVertexColored(new Vertex()); }
            catch (MyException.GraphException.GraphException e) { stringBuilder.AppendLine(e.Message); }

            // GetColorVertex - Doesn't exist
            stringBuilder.AppendLine("GetColorVertex - Doesn't exist");
            try { coloredGraph.GetColorVertex(new Vertex()); }
            catch (MyException.GraphException.GraphException e) { stringBuilder.AppendLine(e.Message); }
        }
        #endregion
        
        #region Property
        public string GetPath()
        {
            return testPathColoredGraph;
        }
        #endregion
    }
}
