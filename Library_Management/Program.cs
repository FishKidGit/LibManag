using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library_Management
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
            //   //   Application.Run(new add_student_info());// view_student_info());
            //    Application.Run(new Issue_Books());
            //   Application.Run(new add_books());
      //   Application.Run(new Books_Stock());
        }
    }
}
