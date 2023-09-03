using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

using SistemaEscolar.Models;

namespace SistemaEscolar.Services
{
    public class ClsUsuarioMantenimiento
    {
        //clase que posee la conexion a sql
        Conexion objConexion = new Conexion();

        //variables para almacenar el nombre del usuario logeado
        string nombreUsuario;

        //variable para los querys sql
        string query;

        //método para verificar las cerdenciales del usuario y retorna una lista con algunos datos del usuario
        public List<string> ValidarCredenciales(string correo, string contrasena)
        {
            try
            {
                // Crear una lista de strings
                List<string> usuarioInfo = new List<string>();

                objConexion.Conectar();

                query = "select IdUsuario,IdTipoUsuario from Usuario where CorreoUsuario = @correo and ContrasenaUsuario = @contrasena";

                using (SqlCommand comando = new SqlCommand(query, objConexion.conexion))
                {
                    comando.Parameters.AddWithValue("@correo", correo);
                    comando.Parameters.AddWithValue("@contrasena", contrasena);

                    objConexion.conexion.Open();

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            //Creamos una nueva instancia del objeto Usuario
                            Usuario usuario = new Usuario
                            {
                                //Asigna los valores a las instancias
                                IdUsuario = (int)reader["IdUsuario"],
                                IdTipoUsuario = (int)reader["IdTipoUsuario"]
                            };

                            reader.Close();

                            // Ahora que tenemos el IdUsuario y el IdTipoUsuario, podemos obtener el nombre del estudiante
                            string nombreUsuario = ObtenerNombreUsuario(usuario);

                            string idusuario = usuario.IdUsuario.ToString();
                            string idtipo = usuario.IdTipoUsuario.ToString();

                            //agregamos los datos a la lista
                            usuarioInfo.Add(idusuario);
                            usuarioInfo.Add(idtipo);
                            usuarioInfo.Add(nombreUsuario);

                            //retorna la lista  con la informacíón del usuario
                            return usuarioInfo;

                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al validar credenciales. Por favor, inténtalo de nuevo más tarde." + ex);
            }
            finally
            {
                if (objConexion.conexion.State != ConnectionState.Closed)
                {
                    // Cerrar la conexión en el bloque finally para asegurar su cierre incluso en caso de excepciones.
                    objConexion.conexion.Close();
                }
            }

            return null;

        }

        //método para extraer el nombre del usuario ya sea alumno o profesor
        public string ObtenerNombreUsuario(Usuario data)
        {
            int idtipo = Convert.ToInt32(data.IdTipoUsuario);

            if (idtipo == 1)
            {

                query = "select NombreEstudiante from Estudiante where IdUsuario = @idUsuario";

            }
            else
            {
                query = "select NombreProfesor from Profesor where IdUsuario = @idUsuario";

            }

            SqlCommand comando = new SqlCommand(query, objConexion.conexion);
            comando.Parameters.AddWithValue("@idUsuario", data.IdUsuario);

            using (SqlDataReader reader = comando.ExecuteReader())
            {
                if (reader.Read())
                {
                    // Si hay una fila de resultados, puedes obtener los valores de las columnas
                    nombreUsuario = reader.GetString(0);
                    reader.Close();

                }
            }

            return nombreUsuario;
        }
    }
}