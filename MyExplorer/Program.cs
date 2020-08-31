using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyExplorer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            args = new string[] { @"D:\" };
            //
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length < 1) Application.Run(new MyExplorer(@"D:\"));
            else Application.Run(new MyExplorer(string.Join(" ", args)));
        }
    }
}
