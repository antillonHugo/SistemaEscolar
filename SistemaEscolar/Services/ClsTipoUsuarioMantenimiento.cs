using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

using SistemaEscolar.Models;

namespace SistemaEscolar.Services
{
    public class ClsTipoUsuarioMantenimiento
    {
        Conexion miConexion = new Conexion();

        List<TipoUsuario> ListaTPousuarios = new List<TipoUsuario>();

        TipoUsuario tipousuarios = new TipoUsuario();

        //Método para extraer el listado de los tipos de usuarios existentes
        public List<TipoUsuario> ObtenerTiposUsuario()
        {

            try
            {
                miConexion.Conectar();

                var sql = "select IdTipoUsuario,TipoUsuario from TipoUsuario";

                using (SqlCommand comando = new SqlCommand(sql, miConexion.conexion))
                {
                    miConexion.conexion.Open();

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tipousuarios = new TipoUsuario()
                            {
                                IdTipoUsuario = (int)reader["IdTipoUsuario"],
                                TipoUsuarios = (string)reader["TipoUsuario"]
                            };

                            // Agregar el estudiante a la lista
                            ListaTPousuarios.Add(tipousuarios);
                        }
                    }
                }

                return ListaTPousuarios;
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al buscar el estudiante. Por favor, inténtalo de nuevo más tarde." + ex.Message);
            }
            finally
            {
                if (miConexion.conexion.State != ConnectionState.Closed)
                {
                    // Cerrar la conexión en el bloque finally para asegurar su cierre incluso en caso de excepciones.
                    miConexion.conexion.Close();
                }
            }
        }

        //Funciñon para extraer los tipos de usuarios por ID
        public List<TipoUsuario> obtenerTipoUsuarioDiferenteID(int idtipousuario)
        {

            try
            {
                // Antes de realizar una nueva consulta, limpiar la lista o crear una nueva instancia
                ListaTPousuarios.Clear();

                miConexion.Conectar();

                var sql = "select IdTipoUsuario,TipoUsuario from TipoUsuario where IdTipoUsuario !=@idtipousuario";

                using (SqlCommand comando = new SqlCommand(sql, miConexion.conexion))
                {
                    comando.Parameters.AddWithValue("@idtipousuario", idtipousuario);

                    miConexion.conexion.Open();

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tipousuarios = new TipoUsuario()
                            {
                                IdTipoUsuario = (int)reader["IdTipoUsuario"],
                                TipoUsuarios = (string)reader["TipoUsuario"]
                            };

                            // Agregar el estudiante a la lista
                            ListaTPousuarios.Add(tipousuarios);
                        }
                    }
                }

                return ListaTPousuarios;
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al buscar el tipo de Usuario. Por favor, inténtalo de nuevo más tarde." + ex.Message);
            }
            finally
            {
                if (miConexion.conexion.State != ConnectionState.Closed)
                {
                    // Cerrar la conexión en el bloque finally para asegurar su cierre incluso en caso de excepciones.
                    miConexion.conexion.Close();
                }
            }

        }
    }
}