using System;
using System.IO;

using System.Data.SqlClient;


namespace AI
{
    class Program
    {
        static void Main(string[] args)
        {
            Start();
            
            /*
            // Variable
            int minCount, maxCount;
            bool clear = true, writer = true;
            string reader;
            int constant;

            
            Console.WriteLine("MinCount");
            reader = Console.ReadLine();
            int.TryParse(reader, out minCount);

            Console.WriteLine("MaxCount");
            reader = Console.ReadLine();
            int.TryParse(reader, out maxCount);

            Console.WriteLine("Constant");
            reader = Console.ReadLine();
            int.TryParse(reader, out constant);

            Console.WriteLine("Writer");
            reader = Console.ReadLine();
            bool.TryParse(reader, out writer);

            Console.WriteLine("Clear");
            reader = Console.ReadLine();
            bool.TryParse(reader, out clear);
            
            GenerateGraphs.GenerateGraphs generateGraphs = new GenerateGraphs.GenerateGraphsFile(constant, 1, writer, clear);
            generateGraphs.Generate(minCount, maxCount);
            */

            /*
            Console.WriteLine("Writer");
            reader = Console.ReadLine();
            bool.TryParse(reader, out writer);

            Console.WriteLine("Clear");
            reader = Console.ReadLine();
            bool.TryParse(reader, out clear);

            GenerateGraphs.GenerateGraphsDatabase generateGraphsDatabase = new GenerateGraphs.GenerateGraphsDatabase(writer, clear);
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
                     ML.CreateAI createAI = new ML.CreateAI("127.0.0.1", "GraphColoring", "GraphColoring", "GraphColoring.", ML.CreateAI.AIEnum.stochasticDualCoordinateAscent, algorithm);
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

        private static string dataSource;
        private static string database;
        private static string userName;
        private static string password;

        public static void Start()
        {
            // Variable
            string reader;
            int exitCode = 0;
            bool quit = false;

            while (true)
            {
                Console.Clear();

                ShowMenu();

                reader = Console.ReadLine();
                
                switch (reader)
                {
                    case "1":
                        exitCode = GenerateGraphsToFile();
                        break;
                    case "2":
                        exitCode = GenerateGraphsToDatabase();
                        break;
                    case "3":
                        exitCode = InsertGraphsFromFilesToDatabase();
                        break;
                    case "4":
                        exitCode = CreateMls();
                        break;
                    case "5":
                        exitCode = 0;
                        quit = true;
                        break;
                    default:
                        exitCode = 2;
                        break;
                }

                if (exitCode == 0 || exitCode == 1)
                {
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                }

                if (quit)
                    break;
            }
        }

        public static void ShowMenu()
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("----------- GraphColoring - AI console -----------");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("1) Generate graphs to file");
            Console.WriteLine("2) Generate graphs to database");
            Console.WriteLine("3) Insert graphs from files to database");
            Console.WriteLine("4) Create MLs");
            Console.WriteLine("5) Exit console");
        }

        private static void GetDatabaseInformations()
        {
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("We need some information about your database.");
            Console.WriteLine("---------------------------------------------");

            Console.Write("Database location [string]: ");
            dataSource = Console.ReadLine();

            Console.Write("Name of database [string]: ");
            database = Console.ReadLine();

            Console.Write("User name [string]: ");
            userName = Console.ReadLine();

            Console.Write("Password [string]: ");
            password = Console.ReadLine();
        }

        private static int InsertGraphsFromFilesToDatabase()
        {
            // Variable
            string reader;
            bool writer, clearFile;

            Console.Clear();
            Console.WriteLine("Insert graphs from files to database");

            Console.WriteLine("Info: the files will be found in " + GenerateGraphs.GenerateGraphsFile.GetPathFolder() + "*." + GenerateGraphs.GenerateGraphsFile.GetFileNameExtension());

            Console.Write("Write report to console [true | false]: ");
            reader = Console.ReadLine();
            bool.TryParse(reader, out writer);
            
            Console.Write("Clear database [true | false]: ");
            reader = Console.ReadLine();
            bool.TryParse(reader, out clearFile);

            if (dataSource == null)
                GetDatabaseInformations();

            try
            {
                GenerateGraphs.GenerateGraphsDatabase generateGraphsDatabase = new GenerateGraphs.GenerateGraphsDatabase(dataSource, database, userName, password, writer, clearFile);
                
                Console.WriteLine();
                Console.WriteLine("Start inserting... ");
                Console.WriteLine();

                generateGraphsDatabase.SaveDataFromFileToDB();

                Console.WriteLine();
                Console.WriteLine("Graphs have been inserted.");
                Console.WriteLine();
            }
            catch (MyException.DatabaseException.GenerateGraphsDatabaseNotOpenException)
            {
                dataSource = null;
                Console.WriteLine("Something wrong with database connection!");
                return 1;
            }
            catch (SqlException ex)
            {
                dataSource = null;
                Console.WriteLine("Something wrong: " + ex.GetType());
                return 1;
            }
            catch (IOException ex)
            {
                Console.WriteLine("Something wrong: " + ex.GetType());
                return 1;
            }

            return 0;
        }

        private static int GenerateGraphsToFile()
        {
            // Variable
            string reader;
            bool writer, clearFile;
            int constant = 0, exponent = 0;
            int minCount = 0, maxCount = 0;
        
            Console.Clear();
            Console.WriteLine("Generate graphs to file");

            Console.WriteLine("Info: the file will be saved in " + GenerateGraphs.GenerateGraphsFile.GetPathFile());

            Console.Write("Write report to console [true | false]: ");
            reader = Console.ReadLine();
            bool.TryParse(reader, out writer);

            Console.Write("Clear file [true | false]: ");
            reader = Console.ReadLine();
            bool.TryParse(reader, out clearFile);

            do
            {
                Console.Write("Minimum number of vertices [positive int]: ");
                reader = Console.ReadLine();
                int.TryParse(reader, out minCount);
            }
            while (minCount <= 0);

            do
            {
                Console.Write("Maximum number of vertices [positive int]: ");
                reader = Console.ReadLine();
                int.TryParse(reader, out maxCount);
            }
            while (maxCount <= 0);

            do
            {
                Console.Write("Number of generated graphs - constant (constant * (number of vertices)^(exponent)) [positive int]: ");
                reader = Console.ReadLine();
                int.TryParse(reader, out constant);
            }
            while (constant <= 0);

            do
            {
                Console.Write("Number of generated graphs - exponent (constant * (number of vertices)^(exponent)) [positive int]: ");
                reader = Console.ReadLine();
                int.TryParse(reader, out exponent);
            }
            while (exponent <= 0);

            try
            {
                GenerateGraphs.GenerateGraphs generateGraphs = new GenerateGraphs.GenerateGraphsFile(constant, exponent, writer, clearFile);

                Console.WriteLine();
                Console.WriteLine("Start generating... ");
                Console.WriteLine();

                generateGraphs.Generate(minCount, maxCount);

                Console.WriteLine();
                Console.WriteLine("Graphs have been generated and saved.");
                Console.WriteLine();
            }
            catch (GraphColoring.MyException.ReaderWriterException.ReaderWriterException ex)
            {
                Console.WriteLine("Something wrong: " + ex.GetType());
                return 1;
            }
            catch (IOException ex)
            {
                Console.WriteLine("Something wrong: " + ex.GetType());
                return 1;
            }

            return 0;
        }

        private static int GenerateGraphsToDatabase()
        {
            // Variable
            string reader;
            bool writer, clearDatabase;
            int constant = 0, exponent = 0;
            int minCount = 0, maxCount = 0;

            Console.Clear();
            Console.WriteLine("Generate graphs to database");

            Console.Write("Write report to console [true | false]: ");
            reader = Console.ReadLine();
            bool.TryParse(reader, out writer);

            Console.Write("Clear database [true | false]: ");
            reader = Console.ReadLine();
            bool.TryParse(reader, out clearDatabase);

            do
            {
                Console.Write("Minimum number of vertices [positive int]: ");
                reader = Console.ReadLine();
                int.TryParse(reader, out minCount);
            }
            while (minCount <= 0);

            do
            {
                Console.Write("Maximum number of vertices [positive int]: ");
                reader = Console.ReadLine();
                int.TryParse(reader, out maxCount);
            }
            while (maxCount <= 0);

            do
            {
                Console.Write("Number of generated graphs - constant (constant * (number of vertices)^(exponent)) [positive int]: ");
                reader = Console.ReadLine();
                int.TryParse(reader, out constant);
            }
            while (constant <= 0);

            do
            {
                Console.Write("Number of generated graphs - exponent (constant * (number of vertices)^(exponent)) [positive int]: ");
                reader = Console.ReadLine();
                int.TryParse(reader, out exponent);
            }
            while (exponent <= 0);

            if (dataSource == null)
                GetDatabaseInformations();

            try
            {
                GenerateGraphs.GenerateGraphs generateGraphs = new GenerateGraphs.GenerateGraphsDatabase(dataSource, database, userName, password, writer, clearDatabase, constant, exponent);
                
                Console.WriteLine();
                Console.WriteLine("Start generating... ");
                Console.WriteLine();

                generateGraphs.Generate(minCount, maxCount);
                
                Console.WriteLine();
                Console.WriteLine("Graphs have been generated and saved.");
                Console.WriteLine();
            }
            catch (MyException.DatabaseException.GenerateGraphsDatabaseNotOpenException)
            {
                dataSource = null;
                Console.WriteLine("Something wrong with database connection!");
                return 1;
            }
            catch (SqlException ex)
            {
                dataSource = null;
                Console.WriteLine("Something wrong: " + ex.GetType());
                return 1;
            }

            return 0;
        }

        private static int CreateMls()
        {
            Console.Clear();
            Console.WriteLine("Create MLs");

            Console.WriteLine("Info: models will be saved in " + ML.CreateAI.GetPathFolder());

            if (dataSource == null)
                GetDatabaseInformations();

            foreach (GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum algorithm in (GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum[])Enum.GetValues(typeof(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum)))
            {
                if (algorithm == GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.optimal ||
                    algorithm == GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.AI)
                    continue;

                try
                {

                    Console.WriteLine();
                    Console.WriteLine("Model is creating ({0})... ", algorithm);
                    Console.WriteLine();

                    ML.CreateAI createAI = new ML.CreateAI(dataSource, database, userName, password, ML.CreateAI.AIEnum.fastTree, algorithm);
                    createAI.CreateModel();

                    Console.WriteLine("Accurency: " + createAI.GetAccurancy());
                    Console.WriteLine("LogLoss: " + createAI.GetLogLoss());
                    Console.WriteLine("LogLossReduction: " + createAI.GetLogLossReduction());
                    Console.WriteLine("F1 score: " + createAI.GetF1Score());
                    Console.WriteLine("Auc: " + createAI.GetAuc());
                    Console.WriteLine("Model has been saved: " + createAI.GetPathModel());
                    Console.WriteLine();
                }
                catch (MyException.DatabaseException.GenerateGraphsDatabaseNotOpenException)
                {
                    dataSource = null;
                    Console.WriteLine("Something wrong with database connection!");
                    return 1;
                }
                catch (MyException.AIException.AIException ex)
                {
                    Console.WriteLine("Something wrong: " + ex.GetType());
                }
                catch (SqlException ex)
                {
                    dataSource = null;
                    Console.WriteLine("Something wrong: " + ex.GetType());
                    return 1;
                }
                catch (AggregateException ex)
                {
                    Console.WriteLine("Something wrong: " + ex.GetType());
                    return 1;
                }
            }

            return 0;
        }
    }
}
