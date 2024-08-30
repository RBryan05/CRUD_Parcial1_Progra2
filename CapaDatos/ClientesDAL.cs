using CapaModelos;
using System;
using System.Collections;
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
            // Abrimos una conexión a la base de datos utilizando el contexto.
            using (var cadena = ContextoBD.ObtenerCadena())
            {
                // Definimos la consulta SQL para seleccionar todos los campos necesarios de la tabla Clientes.
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
                consulta = consulta + "  WHERE [Activo] = 1"; // Filtrar solo los clientes que están activos.

                // Creamos un objeto SqlCommand para ejecutar la consulta SQL.
                using (SqlCommand comando = new SqlCommand(consulta, cadena))
                {
                    // Ejecutamos la consulta y obtener un SqlDataReader para leer los resultados.
                    SqlDataReader reader = comando.ExecuteReader();
                    List<Clientes> Personas = new List<Clientes>(); // Crear una lista para almacenar los clientes.

                    // Leer cada fila de resultados del SqlDataReader.
                    while (reader.Read())
                    {
                        // Convertimos la fila leída en un objeto Cliente utilizando el método LeerDelDataReader.
                        var clientes = LeerDelDataReader(reader);
                        // Agregamos el objeto Cliente a la lista de clientes.
                        Personas.Add(clientes);
                    }

                    // Retornamos la lista de clientes activos.
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

        public List<Clientes> ObtenerClientesID2(int id)
        {
            // Abrir una conexión a la base de datos utilizando el contexto de conexión.
            using (var cadena = ContextoBD.ObtenerCadena())
            {
                // Construir la consulta SQL para seleccionar clientes según su identificación.
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

                // Crear un comando SQL con la consulta y la conexión.
                using (SqlCommand comando = new SqlCommand(consulta, cadena))
                {
                    // Agregar el parámetro de identificación al comando.
                    comando.Parameters.AddWithValue("@identificacion", id);

                    // Ejecutar el comando y obtener un lector de datos.
                    SqlDataReader reader = comando.ExecuteReader();

                    // Crear una lista para almacenar los clientes obtenidos.
                    List<Clientes> Personas = new List<Clientes>();

                    // Leer cada fila de datos del SqlDataReader.
                    while (reader.Read())
                    {
                        // Convertir la fila leída en un objeto Cliente.
                        var personas = LeerDelDataReader(reader);
                        // Agregar el objeto Cliente a la lista.
                        Personas.Add(personas);
                    }

                    // Retornar la lista de clientes obtenidos.
                    return Personas;
                }
            }
        }

        // Método para obtener un cliente específico basado en su identificación.
        public Clientes ObtenerClientesID(int id)
        {
            // Abrir una conexión a la base de datos utilizando el contexto de conexión.
            using (var cadena = ContextoBD.ObtenerCadena())
            {
                // Construir la consulta SQL para seleccionar un cliente específico según su identificación.
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

                // Crear un comando SQL con la consulta y la conexión.
                using (SqlCommand comando = new SqlCommand(consulta, cadena))
                {
                    // Agregar el parámetro de identificación al comando para evitar inyecciones SQL.
                    comando.Parameters.AddWithValue("@identificacion", id);

                    // Ejecutar el comando y obtener un lector de datos.
                    SqlDataReader reader = comando.ExecuteReader();
                    // Variable para almacenar el cliente obtenido.
                    Clientes Personas = null;

                    // Leer cada fila de datos del SqlDataReader.
                    while (reader.Read())
                    {
                        // Convertir la fila leída en un objeto Cliente.
                        Personas = LeerDelDataReader(reader);
                    }

                    // Retornar el cliente obtenido. Si no se encuentra ningún cliente, retornará null.
                    return Personas;
                }
            }
        }

        // Método para insertar un nuevo cliente en la base de datos.
        public int InsertarCliente(Clientes cliente)
        {
            // Validar los datos del cliente antes de intentar insertarlos
            if (cliente == null)
            {
                // Lanzar una excepción si el objeto cliente es nulo, ya que no se puede insertar un cliente nulo.
                throw new ArgumentNullException(nameof(cliente), "El cliente no puede ser nulo.");
            }

            // Definir la consulta SQL para insertar un nuevo registro en la tabla Clientes.
            string consulta = @"
        INSERT INTO [dbo].[Clientes] 
        ([Nombres], [Apellidos], [Telefono], [Direccion], [Correo], [EstadoCivil], [Activo])
        VALUES 
        (@Nombres, @Apellidos, @Telefono, @Direccion, @Correo, @EstadoCivil, @Activo)";

            // Abrir una conexión a la base de datos utilizando el contexto de conexión.
            using (var conexion = ContextoBD.ObtenerCadena())
            {
                try
                {
                    // Crear un comando SQL con la consulta y la conexión.
                    using (SqlCommand comando = new SqlCommand(consulta, conexion))
                    {
                        // Configurar los parámetros del comando con los valores del objeto cliente.
                        comando.Parameters.AddWithValue("@Nombres", cliente.Nombres);
                        comando.Parameters.AddWithValue("@Apellidos", cliente.Apellidos);
                        comando.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                        comando.Parameters.AddWithValue("@Direccion", cliente.Direccion);
                        comando.Parameters.AddWithValue("@Correo", cliente.Correo);
                        comando.Parameters.AddWithValue("@EstadoCivil", cliente.EstadoCivil);
                        comando.Parameters.AddWithValue("@Activo", cliente.Activo);

                        // Ejecutar el comando y obtener el número de filas afectadas por la inserción.
                        int filasAfectadas = comando.ExecuteNonQuery();

                        // Retornar el número de filas afectadas. Debería ser 1 si la inserción fue exitosa.
                        return filasAfectadas;
                    }
                }
                catch (SqlException ex)
                {
                    // Manejo de excepciones: Lanzar una excepción personalizada si ocurre un error durante la ejecución del comando.
                    throw new InvalidOperationException("Error al insertar el cliente en la base de datos.", ex);
                }
            }
        }


        // Método para actualizar la información de un cliente existente en la base de datos.
        public int EditarCliente(Clientes cliente)
        {
            // Abrir una conexión a la base de datos utilizando el contexto de conexión.
            using (var conexion = ContextoBD.ObtenerCadena())
            {
                // Definir la consulta SQL para actualizar el registro del cliente en la tabla Clientes.
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

                // Crear un comando SQL con la consulta y la conexión.
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    // Configurar los parámetros del comando con los valores del objeto cliente.
                    comando.Parameters.AddWithValue("@Nombres", cliente.Nombres);
                    comando.Parameters.AddWithValue("@Apellidos", cliente.Apellidos);
                    comando.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                    comando.Parameters.AddWithValue("@Direccion", cliente.Direccion);
                    comando.Parameters.AddWithValue("@Correo", cliente.Correo);
                    comando.Parameters.AddWithValue("@EstadoCivil", cliente.EstadoCivil);
                    comando.Parameters.AddWithValue("@Activo", cliente.Activo);
                    comando.Parameters.AddWithValue("@Identificacion", cliente.Identificacion);

                    // Ejecutar el comando y obtener el número de filas afectadas por la actualización.
                    int filasAfectadas = comando.ExecuteNonQuery();

                    // Retornar el número de filas afectadas. Debería ser 1 si la actualización fue exitosa.
                    return filasAfectadas;
                }
            }
        }

        // Método para marcar un cliente como inactivo en la base de datos (lógica de eliminación).
        public int EliminarCliente(int identificacion)
        {
            // Abrir una conexión a la base de datos utilizando el contexto de conexión.
            using (var conexion = ContextoBD.ObtenerCadena())
            {
                // Definir la consulta SQL para actualizar el estado del cliente a inactivo (Activo = 0).
                string consulta = @"
        UPDATE [dbo].[Clientes]
        SET [Activo] = 0
        WHERE [Identificacion] = @Identificacion";

                // Crear un comando SQL con la consulta y la conexión.
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    // Configurar el parámetro del comando con la identificación del cliente a eliminar.
                    comando.Parameters.AddWithValue("@Identificacion", identificacion);

                    // Ejecutar el comando y obtener el número de filas afectadas por la actualización.
                    int filasAfectadas = comando.ExecuteNonQuery();

                    // Retornar el número de filas afectadas. Debería ser 1 si el cliente fue marcado como inactivo correctamente.
                    return filasAfectadas;
                }
            }
        }

        // Metodo para obtener clientes por nombres.
        public List<Clientes> ObtenerPorNombre(string nombre)
        {
            // Obtenemos todos los registros de clientes y los agregamos a una variables
            var datos = ObtenerClientes();
            // Aplicamos un filtro sobre los la variable con los datos y seleccionamos solo el que concida con el nombre brindado.
            var filtro = datos.FindAll(x => x.Nombres.ToUpper().StartsWith(nombre.ToUpper()));
            // Retornamos lista que contenga solo el registro que coincida con el nombre.
            return filtro;
        }

        // Método para obtener una lista de clientes que están marcados como inactivos (Activo = 0).
        public List<Clientes> FiltrarPorEstado()
        {
            // Abrir una conexión a la base de datos utilizando el contexto de conexión.
            using (var cadena = ContextoBD.ObtenerCadena())
            {
                // Definir la consulta SQL para seleccionar todos los clientes que están inactivos.
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
                consulta = consulta + "  WHERE [Activo] = 0";

                // Crear un comando SQL con la consulta y la conexión.
                using (SqlCommand comando = new SqlCommand(consulta, cadena))
                {
                    // Ejecutar el comando y obtener un lector de datos.
                    SqlDataReader reader = comando.ExecuteReader();
                    // Crear una lista para almacenar los clientes que cumplen con el criterio.
                    List<Clientes> Personas = new List<Clientes>();

                    // Leer cada fila de datos del lector.
                    while (reader.Read())
                    {
                        // Convertir los datos de la fila en un objeto Cliente.
                        var clientes = LeerDelDataReader(reader);
                        // Agregar el cliente a la lista.
                        Personas.Add(clientes);
                    }

                    // Retornar la lista de clientes inactivos.
                    return Personas;
                }
            }
        }


        // Método para eliminar un cliente de manera permanente de la base de datos, basándose en su identificación.
        public int EliminarClientePermanente(int identificacion)
        {
            // Abrir una conexión a la base de datos utilizando el contexto de conexión.
            using (var conexion = ContextoBD.ObtenerCadena())
            {
                // Definir la consulta SQL para eliminar un cliente específico de la tabla.
                string consulta = @"
        DELETE FROM [dbo].[Clientes]
        WHERE [Identificacion] = @Identificacion";

                // Crear un comando SQL con la consulta y la conexión.
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    // Configurar el parámetro del comando con la identificación del cliente a eliminar.
                    comando.Parameters.AddWithValue("@Identificacion", identificacion);

                    // Ejecutar el comando y obtener el número de filas afectadas.
                    int filasAfectadas = comando.ExecuteNonQuery();

                    // Verificar si se eliminó algún registro. Si no, lanzar una excepción.
                    if (filasAfectadas == 0)
                    {
                        throw new Exception("No se eliminó ningún registro. Verifica que el cliente exista.");
                    }

                    // Retornar el número de filas afectadas por el comando de eliminación.
                    return filasAfectadas;
                }
            }
        }
    }
}