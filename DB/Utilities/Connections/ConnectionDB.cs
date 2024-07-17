using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Repository.Utilities.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Utilities.Connections
{
    public class ConnectionDB
    {
        public static SqlConnection Connection()
        {
            string? strConnection = ConfigManager.AppSetting.GetConnectionString("strConnection");
            SqlConnection sqlConnection = new SqlConnection(strConnection);
            return sqlConnection;
        }
    }
}
