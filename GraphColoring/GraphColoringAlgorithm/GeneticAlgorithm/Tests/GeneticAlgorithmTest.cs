using System;
using System.Text;
using System.Collections.Generic;

namespace GraphColoring.GraphColoringAlgorithm.GeneticAlgorithm.Tests
{
    class GeneticAlgorithmTest : GraphColoring.Tests.ITestInterface
    {
        // Variable
        #region
        private Graph.IGraphInterface graph;
        private StringBuilder stringBuilder;
        private Random random;
        private GenerateGraph.ErdosRenyiModel.ErdosRenyiModel erdosRenyiModel;

        // Paths
        private string testPathAlgorithmGenetic = @"Testing\Test\GeneticAlgorithm.txt";
        #endregion

        // Enum
        #region
        public enum GeneticAlgorithmEnum
        {
            randomGraphs
        }
        #endregion

        // Constructor
        #region
        public GeneticAlgorithmTest()
        {
            stringBuilder = new StringBuilder();
            random = new Random();
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

            foreach (GeneticAlgorithmEnum geneticAlgorithm in (GeneticAlgorithmEnum[])Enum.GetValues(typeof(GeneticAlgorithmEnum)))
            {
                Testing(geneticAlgorithm);
            }

            return stringBuilder;
        }

        /// <summary>
        /// Test a particular enum (file)
        /// </summary>
        /// <returns>report</returns>
        public StringBuilder Test(GeneticAlgorithmEnum geneticAlgorithm)
        {
            stringBuilder.Clear();

            Testing(geneticAlgorithm);

            return stringBuilder;
        }

        private void Testing(GeneticAlgorithmEnum geneticAlgorithm)
        {
            try
            {
                erdosRenyiModel = new GenerateGraph.ErdosRenyiModel.ErdosRenyiModel(random.Next(15, 25));
                graph = erdosRenyiModel.GenerateGraph();

                GeneticAlgorithm algorithm = new GeneticAlgorithm(graph, 2, random.Next(250, 300));
                algorithm.Color();

                stringBuilder.AppendLine(algorithm.ToString());
                stringBuilder.AppendLine("Graph colored.");
                stringBuilder.AppendLine("Is valid colored: " + graph.GetColoredGraph().IsValidColored().ToString());
            }
            catch (KeyNotFoundException)
            {
                throw new MyException.TestsException.TestsMissingTestException(geneticAlgorithm.ToString());
            }
            catch (MyException.ReaderWriterException.ReaderWriterException e)
            {
                stringBuilder.AppendLine(e.Message);
            }
        }
        #endregion

        // Property
        #region
        public string GetPath()
        {
            return testPathAlgorithmGenetic;
        }
        #endregion
    }
}
