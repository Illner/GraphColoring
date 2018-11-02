﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace GraphColoring
{
    class Program
    {
        static void Main(string[] args) 
        {
            
            Tests.Tests tests = new Tests.Tests(true);
            tests.Test();
            

            /*
            Graph.ColoredGraph.Tests.ColoredGraphTest coloredGraphTest = new Graph.ColoredGraph.Tests.ColoredGraphTest();
            Console.WriteLine(coloredGraphTest.Test(Graph.ColoredGraph.Tests.ColoredGraphTest.ColoredGraphEnum.valid));
            */
            //Tests.Tests tests = new Tests.Tests(false);
            //tests.Test(Tests.Tests.TestEnum.geneticAlgorithm);
            /*
            ReaderWriter.Reader reader = new ReaderWriter.Reader(Tests.Tests.CreateTestFile(GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.Tests.SmallestLastSequenceResource.smallestLastSequence5), false);
            ReaderWriter.Writer writer = new ReaderWriter.Writer(Tests.Tests.CreateTestFile(GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.Tests.SmallestLastSequenceResource.smallestLastSequence5), false);
            */
            
            /*
            GenerateGraph.ErdosRenyiModel.ErdosRenyiModel erdosRenyiModel = new GenerateGraph.ErdosRenyiModel.ErdosRenyiModel(60, GenerateGraph.ErdosRenyiModel.ErdosRenyiModel.ErdosRenyiModelProbabilityEnum.cLogNDividedByN);
            Graph.IGraphInterface graph = erdosRenyiModel.GenerateGraph();

            GraphColoringAlgorithm.GeneticAlgorithm.GeneticAlgorithm algorithm = new GraphColoringAlgorithm.GeneticAlgorithm.GeneticAlgorithm(graph, 100);

            algorithm.Color();

            //writer.WriteFile(graph, GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.Optimal, true);
            Console.WriteLine(graph.GetColoredGraph());
            Console.WriteLine("Colors: " + graph.GetColoredGraph().GetCountUsedColors());
            */
        }

        public static void TestGraph(int countVerticies)
        {
            // Variable
            Graph.IGraphInterface generatedGraph;
            Graph.GraphProperty.GraphProperty graphProperty;
            GenerateGraph.ErdosRenyiModel.ErdosRenyiModel erdosRenyiModel;
            //
            bool isConnected;
            bool isReguelar;
            bool isCyclic;


            // Generate graph
            erdosRenyiModel = new GenerateGraph.ErdosRenyiModel.ErdosRenyiModel(countVerticies);
            generatedGraph = erdosRenyiModel.GenerateGraph();
            graphProperty = generatedGraph.GetGraphProperty();

            // Graph property
        }
    }
}
