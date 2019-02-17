using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI
{
    class Program
    {
        static void Main(string[] args)
        {
            // Variable
            int minCount, maxCount;
            bool clearDB, writer;
            string reader;
            string path = @"Data\data" + DateTime.Now.ToString("-dd-MM-yyyy-HH-mm-ss") + ".tsv";
            
            Console.WriteLine("MinCount");
            reader = Console.ReadLine();
            int.TryParse(reader, out minCount);

            Console.WriteLine("MaxCount");
            reader = Console.ReadLine();
            int.TryParse(reader, out maxCount);

            Console.WriteLine("Writer");
            reader = Console.ReadLine();
            bool.TryParse(reader, out writer);

            Console.WriteLine("ClearDB");
            reader = Console.ReadLine();
            bool.TryParse(reader, out clearDB);
            
            GenerateGraphs.GenerateGraphs generateGraphs = new GenerateGraphs.GenerateGraphs(writer, clearDB);
            generateGraphs.Generate(minCount, maxCount);
            
            //generateGraphs.GetDatabase().SaveDataFromDatabaseToFile(path);
        }
    }
}
