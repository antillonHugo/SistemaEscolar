using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SistemaEscolar.Services
{
    public class Conexion
    {
        public SqlConnection conexion;

        public void Conectar()
        {
            string constr = ConfigurationManager.ConnectionStrings["ConexionBD"].ToString();
            conexion = new SqlConnection(constr);
        }
    }
}