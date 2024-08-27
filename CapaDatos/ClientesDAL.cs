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
            using (var cadena = ContextoBD.ObtenerCadena())
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

                using (SqlCommand comando = new SqlCommand(consulta, cadena))
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

        public Clientes ObtenerClientesID(int id)
        {
            using (var cadena = ContextoBD.ObtenerCadena())
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
                consulta = consulta + "  FROM [dbo].[Clientes]" + "\n";
                consulta = consulta + " WHERE [Identificacion] = @identificacion";

                using (SqlCommand comando = new SqlCommand(consulta, cadena))
                {
                    comando.Parameters.AddWithValue("@identificacion", id);
                    SqlDataReader reader = comando.ExecuteReader();
                    Clientes Personas = null;

                    while (reader.Read())
                    {
                        Personas = LeerDelDataReader(reader);
                    }

                    return Personas;
                }
            }
        }
        public int InsertarCliente(Clientes cliente)
        {
            // Validar los datos del cliente antes de intentar insertarlos
            if (cliente == null)
            {
                throw new ArgumentNullException(nameof(cliente), "El cliente no puede ser nulo.");
            }

            string consulta = @"
                INSERT INTO [dbo].[Clientes] 
                ([Nombres], [Apellidos], [Telefono], [Direccion], [Correo], [EstadoCivil], [Activo])
                VALUES 
                (@Nombres, @Apellidos, @Telefono, @Direccion, @Correo, @EstadoCivil, @Activo)";

            using (var conexion = ContextoBD.ObtenerCadena())
            {
                try
                {
                    using (SqlCommand comando = new SqlCommand(consulta, conexion))
                    {
                        // Configurar los parámetros del comando
                        comando.Parameters.AddWithValue("@Nombres", cliente.Nombres);
                        comando.Parameters.AddWithValue("@Apellidos", cliente.Apellidos);
                        comando.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                        comando.Parameters.AddWithValue("@Direccion", cliente.Direccion);
                        comando.Parameters.AddWithValue("@Correo", cliente.Correo);
                        comando.Parameters.AddWithValue("@EstadoCivil", cliente.EstadoCivil);
                        comando.Parameters.AddWithValue("@Activo", cliente.Activo);

                        // Ejecutar el comando
                        int filasAfectadas = comando.ExecuteNonQuery();
                        return filasAfectadas;
                    }
                }
                catch (SqlException ex)
                {
                    // Manejo de excepciones: Registrar el error y/o lanzar una excepción personalizada
                    throw new InvalidOperationException("Error al insertar el cliente en la base de datos.", ex);
                }
            }
        }

        public int EditarCliente(Clientes cliente)
        {
            using (var conexion = ContextoBD.ObtenerCadena())
            {
                string consulta = @"
                UPDATE [dbo].[Clientes]
                SET [Nombres] = @Nombres,
                    [Apellidos] = @Apellidos,
                    [Telefono] = @Telefono,
                    [Direccion] = @Direccion,
                    [Correo] = @Correo,
                    [EstadoCivil] = @EstadoCivil,
                    [Activo] = @Activo
                WHERE [Identificacion] = @Identificacion";

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    // Configurar los parámetros del comando
                    comando.Parameters.AddWithValue("@Nombres", cliente.Nombres);
                    comando.Parameters.AddWithValue("@Apellidos", cliente.Apellidos);
                    comando.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                    comando.Parameters.AddWithValue("@Direccion", cliente.Direccion);
                    comando.Parameters.AddWithValue("@Correo", cliente.Correo);
                    comando.Parameters.AddWithValue("@EstadoCivil", cliente.EstadoCivil);
                    comando.Parameters.AddWithValue("@Activo", cliente.Activo);
                    comando.Parameters.AddWithValue("@Identificacion", cliente.Identificacion);

                    // Ejecutar el comando
                    int filasAfectadas = comando.ExecuteNonQuery();
                    return filasAfectadas;
                }
            }
        }
    }
}
