using CapaModelos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    
    public class ClientesDAL
    {
        public List<Clientes> ObtenerClientes()
        {
            using(var cadena = ContextoBD.ObtenerCadena())
            {
                String consulta = "";
                consulta = consulta + "SELECT [Identificacion] " + "\n";
                consulta = consulta + "      ,[Nombres] " + "\n";
                consulta = consulta + "      ,[Apellidos] " + "\n";
                consulta = consulta + "      ,[Telefono] " + "\n";
                consulta = consulta + "      ,[Direccion] " + "\n";
                consulta = consulta + "      ,[Correo] " + "\n";
                consulta = consulta + "      ,[EstadoCivil] " + "\n";
                consulta = consulta + "      ,[Activo] " + "\n";
                consulta = consulta + "  FROM [dbo].[Clientes]";

                using(SqlCommand comando = new SqlCommand(consulta,cadena))
                {
                    SqlDataReader reader = comando.ExecuteReader();
                    List<Clientes> Personas = new List<Clientes>();

                    while (reader.Read())
                    {
                        var clientes = LeerDelDataReader(reader);
                        Personas.Add(clientes);
                    }

                    return Personas;
                }
            }
        }

        public Clientes LeerDelDataReader(SqlDataReader reader)
        {
            Clientes clientes = new Clientes();
            clientes.Identificacion = reader["Identificacion"] == DBNull.Value ? 0 : (int)reader["Identificacion"];
            clientes.Nombres = reader["Nombres"] == DBNull.Value ? "" : (string)reader["Nombres"];
            clientes.Apellidos = reader["Apellidos"] == DBNull.Value ? "" : (string)reader["Apellidos"];
            clientes.Telefono = reader["Telefono"] == DBNull.Value ? 0 : (int)reader["Telefono"];
            clientes.Direccion = reader["Direccion"] == DBNull.Value ? "" : (string)reader["Direccion"];
            clientes.Correo = reader["Correo"] == DBNull.Value ? "" : (string)reader["correo"];
            clientes.EstadoCivil = reader["EstadoCivil"] == DBNull.Value ? "" : (string)reader["EstadoCivil"];
            clientes.Activo = reader["Activo"] == DBNull.Value ? true : (bool)reader["Activo"];

            return clientes;
        }
    }
}
