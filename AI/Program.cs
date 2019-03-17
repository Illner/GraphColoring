using Microsoft.ML.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace AI
{
    class Program
    {
        static void Main(string[] args)
        {
            
            // Variable
            int minCount, maxCount;
            bool clear, writer;
            string reader;
            
            Console.WriteLine("MinCount");
            reader = Console.ReadLine();
            int.TryParse(reader, out minCount);

            Console.WriteLine("MaxCount");
            reader = Console.ReadLine();
            int.TryParse(reader, out maxCount);

            Console.WriteLine("Writer");
            reader = Console.ReadLine();
            bool.TryParse(reader, out writer);

            Console.WriteLine("Clear");
            reader = Console.ReadLine();
            bool.TryParse(reader, out clear);
            
            GenerateGraphs.GenerateGraphs generateGraphs = new GenerateGraphs.GenerateGraphsDatabase(writer, clear);
            generateGraphs.Generate(minCount, maxCount);
            
            /*
            GenerateGraphs.GenerateGraphsDatabase generateGraphsDatabase = new GenerateGraphs.GenerateGraphsDatabase();
            generateGraphsDatabase.SaveDataFromFileToDB();
            */
            /*
            for (int j = 1; j <= 5; j++)
            {
                ML.CreateAI createAI = new ML.CreateAI(ML.CreateAI.AIEnum.sdca);
                GenerateGraphs.GenerateGraphs generateGraphs = new GenerateGraphs.GenerateGraphs();
                generateGraphs.GetDatabase().SaveDataFromDatabaseToFile(createAI.GetPathData(), j);
                for (int i = 1; i <= 8; i++)
                {
                    createAI.CreateModel(i);
                }
            }
            */
            /*
            foreach (GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum algorithm in (GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum[])Enum.GetValues(typeof(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum)))
            {
                 //foreach (ML.CreateAI.AIEnum aiEnum in (ML.CreateAI.AIEnum[])Enum.GetValues(typeof(ML.CreateAI.AIEnum)))
                 //{
                     if (algorithm == GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.optimal ||
                         algorithm == GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.AI)
                         continue;

                     Console.WriteLine("---------------------------------------------");
                     Console.WriteLine(algorithm.ToString());
                     ML.CreateAI createAI = new ML.CreateAI(ML.CreateAI.AIEnum.fastTree, algorithm);
                     createAI.CreateModel();
                     Console.WriteLine("Accurency: " + createAI.GetAccurancy());
                     Console.WriteLine("LogLoss: " + createAI.GetLogLoss());
                     Console.WriteLine("LogLossReduction: " + createAI.GetLogLossReduction());
                     Console.WriteLine("F1 score: " + createAI.GetF1Score());
                     Console.WriteLine("Auc: " + createAI.GetAuc());
                     Console.WriteLine();
                 //}
                 Console.WriteLine();
             }
             */

            /*
            double max = double.MinValue;
            string model = "";

            for (int i = 0; i < 100; i++)
            {
                createAI.CreateModel();

                if (createAI.GetMicroAccurancy() > max)
                {
                    max = createAI.GetMicroAccurancy();
                    model = createAI.GetPathModel();
                    Console.WriteLine(max);
                }
            }

            Console.WriteLine("Max: " + max);
            Console.WriteLine("Path: " + model);
            */
            /*
            GenerateGraphs.GenerateGraphsFile generateGraphsFile = new GenerateGraphs.GenerateGraphsFile(true, false);
            generateGraphsFile.Pokus();
            */
        }
    }
}
