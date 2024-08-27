using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class ContextoBD
    {
        private static string ConnectionString { get
            {
                string cadenaConexion = ConfigurationManager
                    .ConnectionStrings["CRUDCadena"]
                    .ConnectionString;

                SqlConnectionStringBuilder connectionBuilder = 
                    new SqlConnectionStringBuilder(cadenaConexion);

                connectionBuilder.ApplicationName = 
                    ApplicationName ?? connectionBuilder.ApplicationName;

                connectionBuilder.ConnectTimeout = (ConnectionTimeout > 0) 
                    ? ConnectionTimeout : connectionBuilder.ConnectTimeout;

                return connectionBuilder.ToString();
            } 
        }

        public static string ApplicationName { get; set; }
        public static int ConnectionTimeout { get; set; }

        public static SqlConnection ObtenerCadena()
        {
            var cadena = new SqlConnection(ConnectionString);
            cadena.Open();
            return cadena;
        }
    }
}
