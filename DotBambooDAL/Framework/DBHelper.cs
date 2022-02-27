using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotBambooDAL.Framework
{
    public class DBHelper
    {
        private const string DOTBAMBOO_CONNSTRING_KEY = "DotBambooConnectionString";

        public static string GetDotBambooConnectionString()
        {
            return ConfigurationManager.ConnectionStrings[DOTBAMBOO_CONNSTRING_KEY].ConnectionString;
        }

    }
}
