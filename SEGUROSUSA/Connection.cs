using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SEGUROSUSA.Properties;
using System.Data.SqlClient;
using System.Configuration;

namespace SEGUROSUSA
{
    class Connection
    {
        public static SqlConnection conn;
        public static String ObtenerString()
        {
            return Settings.Default.SEGUROSUSAConnectionString;
        }

        public static SqlConnection ObtenerConexion()
        {
            conn = new SqlConnection(ObtenerString());
            conn.Open();
            return conn;
        }
    }
}
