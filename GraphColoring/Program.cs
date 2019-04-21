using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

namespace GraphColoring
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            // Variable
            bool result;

            Tests.Tests tests = new Tests.Tests(true);
            result = tests.Test();

            Console.WriteLine();

            if (!result)
            {
                Console.WriteLine("Testing failed. Some features may not be working correctly");
                Console.WriteLine("Press any key to start application.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Testing passed.");
            }

            Console.WriteLine();
            Console.WriteLine("The application is starting ...");
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GUI.GraphColoringForm());

            Console.WriteLine("The application is closing ...");
        }
    }
}
