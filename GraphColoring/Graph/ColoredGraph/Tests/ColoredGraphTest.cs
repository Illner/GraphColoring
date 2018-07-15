using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphColoring.Graph.ColoredGraph.Tests
{
    class ColoredGraphTest : GraphColoring.Tests.ITestInterface
    {
        // Variable
        #region
        private Graph graph;
        private String testPath;
        private ReaderWriter.Reader reader;
        private StringBuilder stringBuilder;
        private Dictionary<ColoredGraphEnum, string> testsDictionary;

        // Paths
        private string testPathGraphCycle = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Test\ColoredGraph.txt";
        private string coloredGraph1Path = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\ColoredGraph\coloredGraph1.graph";
        private string coloredGraph2Path = @"D:\Storage\OneDrive\Škola\Vysoká škola\UK\Bakalářská práce\Program\Testing\Graph\ColoredGraph\coloredGraph1.graph";
        #endregion

        // Enum
        #region
        public enum ColoredGraphEnum
        {
            valid,
            invalid,
        }
        #endregion

        // Constructor
        #region
        public ColoredGraphTest()
        {
            stringBuilder = new StringBuilder();

            // Fill testsDictionary
            testsDictionary = new Dictionary<ColoredGraphEnum, string>
            {
                { ColoredGraphEnum.valid, coloredGraph1Path },
                { ColoredGraphEnum.invalid, coloredGraph2Path }
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

            foreach (ColoredGraphEnum coloredGraphEnum in testsDictionary.Keys)
            {
                Testing(coloredGraphEnum);
            }

            return stringBuilder;
        }

        /// <summary>
        /// Otestuje daný typ grafu
        /// </summary>
        /// <param name="coloredGraphEnum">daný typ grafu</param>
        /// <returns>Vrátí report</returns>
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
                testPath = testsDictionary[coloredGraphEnum];
                reader = new ReaderWriter.Reader(testPath);
                graph = reader.ReadFile();

                stringBuilder.AppendLine(coloredGraphEnum.ToString());
                stringBuilder.AppendLine("Graph created.");
                stringBuilder.AppendLine(graph.ToString());

                switch (coloredGraphEnum)
                {
                    case ColoredGraphEnum.valid:
                        Valid(graph);
                        break;
                    case ColoredGraphEnum.invalid:
                        Invalid(graph);
                        break;
                    default:
                        throw new MyException.TestsMissingTestException(coloredGraphEnum.ToString());
                }

                stringBuilder.AppendLine("Graph modified.");
                stringBuilder.AppendLine(graph.GetColoredGraph().ToString());
            }
            catch (KeyNotFoundException)
            {
                throw new MyException.TestsMissingTestException(coloredGraphEnum.ToString());
            }
            catch (MyException.GraphException e)
            {
                stringBuilder.AppendLine(e.ToString());
            }
        }

        private void Valid(Graph graph)
        {
            // Variable
            List<Vertex> vertexList = graph.AllVertices();
            IColoredGraphInterface coloredGraph = graph.GetColoredGraph();
            
            if (coloredGraph.InicializeColoredGraph())
                throw new MyException.SomethingWrongTestException();

            coloredGraph.ColorVertex(vertexList.First(), coloredGraph.GreedyColoring(vertexList.First()));

            if (coloredGraph.InicializeColoredGraph())
                throw new MyException.SomethingWrongTestException();

            coloredGraph.ResetColorVertex(vertexList.First());

            coloredGraph.GreedyColoring(vertexList);

            if (!coloredGraph.InicializeColoredGraph())
                throw new MyException.SomethingWrongTestException();

            coloredGraph.DeinicializationColoredGraph();
            if (!coloredGraph.InicializeColoredGraph())
                throw new MyException.SomethingWrongTestException();
            coloredGraph.DeinicializationColoredGraph();

            coloredGraph.ResetColors();
            coloredGraph.ResetColors();

            if (coloredGraph.InicializeColoredGraph())
                throw new MyException.SomethingWrongTestException();

            coloredGraph.ColorVertex(vertexList.First(), 1);
            coloredGraph.ColorVertex(vertexList.ElementAt(1), 1);

            if (coloredGraph.CheckValidColor().Count == 0)
                throw new MyException.SomethingWrongTestException();

            coloredGraph.ColorVertex(vertexList.First(), 2);
            
            if (!coloredGraph.IsVertexColored(vertexList.First()))
                throw new MyException.SomethingWrongTestException();

            if (coloredGraph.IsVertexColored(vertexList.ElementAt(2)))
                throw new MyException.SomethingWrongTestException();

            coloredGraph.ResetColorVertex(vertexList.First());
            
            if (coloredGraph.IsVertexColored(vertexList.First()))
                throw new MyException.SomethingWrongTestException();

            coloredGraph.ResetColorVertex(vertexList.ElementAt(2));

            if (coloredGraph.IsVertexColored(vertexList.ElementAt(2)))
                throw new MyException.SomethingWrongTestException();

            coloredGraph.ColorVertex(vertexList.First(), 1);
            
            if (coloredGraph.CheckValidColor(vertexList.First()))
                throw new MyException.SomethingWrongTestException();
            
            if (!coloredGraph.CheckValidColor(vertexList.ElementAt(2)))
                throw new MyException.SomethingWrongTestException();

            if (coloredGraph.GreedyColoring(vertexList.First()) != 2)
                throw new MyException.SomethingWrongTestException();

            if (coloredGraph.GetUnColoredVertexList().Count != 4)
                throw new MyException.SomethingWrongTestException();

            if (coloredGraph.GetColoredVertexList().Count != 2)
                throw new MyException.SomethingWrongTestException();
            
            if (coloredGraph.GetCountUsedColors() != 1)
                throw new MyException.SomethingWrongTestException();

            coloredGraph.GreedyColoring(vertexList);
            
            if (coloredGraph.GetCountUsedColors() != 2)
                throw new MyException.SomethingWrongTestException();

            coloredGraph.ColorVertex(vertexList.First(), VertexExtended.GetDefaultColor());
            
            if (coloredGraph.CheckValidColor().Count != 0)
                throw new MyException.SomethingWrongTestException();

            coloredGraph.GreedyColoring(vertexList);
            coloredGraph.InicializeColoredGraph();

            if (!coloredGraph.GetIsInicializedColoredGraph())
                throw new MyException.SomethingWrongTestException();

            graph.VertexAdd(new Vertex());

            if (coloredGraph.GetIsInicializedColoredGraph())
                throw new MyException.SomethingWrongTestException();

            vertexList = graph.AllVertices();

            if (vertexList.Last().GetColor() != VertexExtended.GetDefaultColor())
                throw new MyException.SomethingWrongTestException();

            coloredGraph.ColorVertex(vertexList.Last(), 2);
            coloredGraph.ColorVertex(vertexList.First(), 2);

            if (coloredGraph.InicializeColoredGraph())
                throw new MyException.SomethingWrongTestException();

            coloredGraph.GreedyColoring(vertexList);

            if (!coloredGraph.InicializeColoredGraph())
                throw new MyException.SomethingWrongTestException();

            graph.VertexDelete(vertexList.Last());
            graph.VertexAdd(new Vertex());
            vertexList = graph.AllVertices();

            if (coloredGraph.GetColoredVertexList().Count != 6)
                throw new MyException.SomethingWrongTestException();

            if (coloredGraph.GetUnColoredVertexList().Count != 1)
                throw new MyException.SomethingWrongTestException();

            coloredGraph.ColorVertex(vertexList.Last(), 3);

            if (coloredGraph.GetCountUsedColors() != 3)
                throw new MyException.SomethingWrongTestException();

            graph.VertexDelete(vertexList.Last());

            if (coloredGraph.GetColoredVertexList().Count != 6)
                throw new MyException.SomethingWrongTestException();

            if (coloredGraph.GetUnColoredVertexList().Count != 0)
                throw new MyException.SomethingWrongTestException();

            if (coloredGraph.GetCountUsedColors() != 2)
                throw new MyException.SomethingWrongTestException();
        }

        private void Invalid(Graph graph)
        {
            // Variable
            List<Vertex> vertexList = graph.AllVertices();
            IColoredGraphInterface coloredGraph = graph.GetColoredGraph();

            coloredGraph.GreedyColoring(vertexList);
            coloredGraph.InicializeColoredGraph();

            // VertexColor - initialization
            stringBuilder.AppendLine("VertexColor - initialization");
            try { coloredGraph.ColorVertex(vertexList.First(), 2); }
            catch (MyException.GraphException e) { stringBuilder.AppendLine(e.Message); }

            // GreedingColoring - initialization
            stringBuilder.AppendLine("GreedyColoring - initialization");
            try { coloredGraph.GreedyColoring(vertexList); }
            catch (MyException.GraphException e) { stringBuilder.AppendLine(e.Message); }

            // ResetColorVertex - initialization
            stringBuilder.AppendLine("Reset - initialization");
            try { coloredGraph.ResetColorVertex(vertexList.First()); }
            catch (MyException.GraphException e) { stringBuilder.AppendLine(e.Message); }

            // Reset - initialization
            stringBuilder.AppendLine("Reset - initialization");
            try { coloredGraph.ResetColors(); }
            catch (MyException.GraphException e) { stringBuilder.AppendLine(e.Message); }

            // Inicialization - initialization
            stringBuilder.AppendLine("Inicialization - initialization");
            try { coloredGraph.InicializeColoredGraph(); }
            catch (MyException.GraphException e) { stringBuilder.AppendLine(e.Message); }

            coloredGraph.DeinicializationColoredGraph();

            // Deinicialization - initialization
            stringBuilder.AppendLine("Deinicialization - initialization");
            try { coloredGraph.DeinicializationColoredGraph(); }
            catch (MyException.GraphException e) { stringBuilder.AppendLine(e.Message); }

            // ColorVertex - Doesn't exist
            stringBuilder.AppendLine("VertexColor - Doesn't exist");
            try { coloredGraph.ColorVertex(new Vertex(), 3); }
            catch (MyException.GraphException e) { stringBuilder.AppendLine(e.Message); }

            // GreedyColoring - Doesn't exist
            stringBuilder.AppendLine("GreedyColoring - Doesn't exist");
            try { coloredGraph.GreedyColoring(new Vertex()); }
            catch (MyException.GraphException e) { stringBuilder.AppendLine(e.Message); }

            // CheckValidColor - Doesn't exist
            stringBuilder.AppendLine("CheckValidColor - Doesn't exist");
            try { coloredGraph.CheckValidColor(new Vertex()); }
            catch (MyException.GraphException e) { stringBuilder.AppendLine(e.Message); }

            // IsVertexColored - Doesn't exist
            stringBuilder.AppendLine("IsVertexColored - Doesn't exist");
            try { coloredGraph.IsVertexColored(new Vertex()); }
            catch (MyException.GraphException e) { stringBuilder.AppendLine(e.Message); }

            // GetColorVertex - Doesn't exist
            stringBuilder.AppendLine("GetColorVertex - Doesn't exist");
            try { coloredGraph.GetColorVertex(new Vertex()); }
            catch (MyException.GraphException e) { stringBuilder.AppendLine(e.Message); }
        }
        #endregion

        // Property
        #region
        public string GetPath()
        {
            return testPathGraphCycle;
        }
        #endregion
    }
}
