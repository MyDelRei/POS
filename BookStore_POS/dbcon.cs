using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace BookStore_POS
{
    internal class dbcon
    {
        public static string GetConnection()
        {
            return ConfigurationManager.ConnectionStrings["db"].ConnectionString;
        }
    }
}
