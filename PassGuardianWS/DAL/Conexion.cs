using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PassGuardianWS.DAL
{
    public class Conexion
    {
        public SqlConnection DapperConexion;
        private const string SERVER = @"LAPTOP-3038J8U3\SQLEXPRESS";
        private const string DATABASE = "PassGuardian";
        private const string USER = "Johan";
        private const string PWD = "732398";

        public Conexion()
        {
            if (DapperConexion == null)
            {
                string strcon = $"Data Source={SERVER};Initial Catalog={DATABASE};User ID={USER};Password={PWD}";
                DapperConexion = new SqlConnection(strcon);
            }
        }

        
    }
}
