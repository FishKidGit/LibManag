using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace Library_Management
{
    class Sucks1
    {
        static string conn = ConfigurationManager.ConnectionStrings["ConnectionLibrary"].ConnectionString;
        SqlConnection con = new SqlConnection(conn);

        public static void Method1()
        {
            Console.WriteLine("SUUUUUUUCKS!");
        }

    }
}
