using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SistemaEscolar.ViewModels;

namespace SistemaEscolar.Services
{
    public class ClsEstudianteMantenimiento
    {
        //clase que posee la conexion a sql
        Conexion miConexion = new Conexion();

        protected EstudianteViewModel estudiante = new EstudianteViewModel();

        protected List<EstudianteViewModel> listaestudiante = new List<EstudianteViewModel>();

        //Método para listar todos los alumnos
        public List<EstudianteViewModel> ListaEstudiantes()
        {
            try
            {
                // Antes de realizar una nueva consulta, limpiar la lista o crear una nueva instancia
                listaestudiante.Clear();

                //ejecutamos el método Conectar para conectarnos a sql server
                miConexion.Conectar();

                var sql = "select IdEstudiante,NombreEstudiante,CorreoUsuario from Estudiante E inner join Usuario U on E.IdUsuario = U.IdUsuario";

                using (SqlCommand comando = new SqlCommand(sql, miConexion.conexion))
                {
                    miConexion.conexion.Open();

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            estudiante = new EstudianteViewModel()
                            {
                                IdEstudiante = (int)reader["IdEstudiante"],
                                NombreEstudiante = (string)reader["NombreEstudiante"],
                                CorreoUsuario = (string)reader["CorreoUsuario"]
                            };

                            // Agregar el estudiante a la lista
                            listaestudiante.Add(estudiante);
                        }
                    }
                }

                return listaestudiante;
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al validar credenciales. Por favor, inténtalo de nuevo más tarde." + ex.Message);
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

        //Método para filtrar los estudiantes por nombre
        public List<EstudianteViewModel> FiltrarEstudiante(string filtro)
        {
            try
            {
                // Antes de realizar una nueva consulta, limpiar la lista o crear una nueva instancia
                listaestudiante.Clear();

                miConexion.Conectar();

                var query = "select E.IdEstudiante,NombreEstudiante,ApellidoEstudiante,CorreoUsuario,ContrasenaUsuario,TipoUsuario,NombreMateria,Evaluacion1,Evaluacion2,Evaluacion3,Evaluacion4,Evaluacion5,Promedio from Inscripciones I inner join Materia M on I.IdMateria = M.IdMateria inner join Estudiante E on I.IdEstudiante = E.IdEstudiante inner join Notas N on I.IdNota = N.IdNota inner join Usuario U on E.IdUsuario = U.IdUsuario inner join TipoUsuario TU on U.IdTipoUsuario =TU.IdTipoUsuario";

                if (!string.IsNullOrEmpty(filtro))
                {
                    query += " WHERE NombreEstudiante LIKE '%' + @nombre + '%'";
                }

                using (SqlCommand comando = new SqlCommand(query, miConexion.conexion))
                {
                    if (!string.IsNullOrEmpty(filtro))
                    {
                        comando.Parameters.AddWithValue("@nombre", filtro);
                    }

                    miConexion.conexion.Open();

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //Creamos una nueva instancia a EstudianteViewModel para asignarle los datos de la consulta
                            estudiante = new EstudianteViewModel()
                            {
                                IdEstudiante = (int)reader["IdEstudiante"],
                                NombreEstudiante = (string)reader["NombreEstudiante"],
                                ApellidoEstudiante = (string)reader["ApellidoEstudiante"],
                                CorreoUsuario = (string)reader["CorreoUsuario"],
                                TipoUsuario = (string)reader["TipoUsuario"],
                                NombreMateria = (string)reader["NombreMateria"],
                                Evaluacion1 = (decimal)reader["Evaluacion1"],
                                Evaluacion2 = (decimal)reader["Evaluacion2"],
                                Evaluacion3 = (decimal)reader["Evaluacion3"],
                                Evaluacion4 = (decimal)reader["Evaluacion4"],
                                Evaluacion5 = (decimal)reader["Evaluacion5"],
                                Promedio = (decimal)reader["Promedio"]
                            };

                            // Agregar el estudiante a la lista
                            listaestudiante.Add(estudiante);
                        }
                    }
                }
                return listaestudiante;
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al validar credenciales. Por favor, inténtalo de nuevo más tarde." + ex.Message);
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

        //Método para buscar los estudiantes por código
        public List<EstudianteViewModel> buscarEstudiantesID(int IdEstudiante)
        {
            try
            {
                // Antes de realizar una nueva consulta, limpiar la lista o crear una nueva instancia
                listaestudiante.Clear();

                miConexion.Conectar();

                var query = "select E.IdEstudiante,NombreEstudiante,ApellidoEstudiante,E.IdUsuario,CorreoUsuario,ContrasenaUsuario,U.IdTipoUsuario,TipoUsuario,I.IdMateria,NombreMateria,I.IdNota,Evaluacion1,Evaluacion2,Evaluacion3,Evaluacion4,Evaluacion5,Promedio from Inscripciones I inner join Materia M on I.IdMateria = M.IdMateria inner join Estudiante E on I.IdEstudiante = E.IdEstudiante inner join Notas N on I.IdNota = N.IdNota inner join Usuario U on E.IdUsuario = U.IdUsuario inner join TipoUsuario TU on U.IdTipoUsuario =TU.IdTipoUsuario where E.IdEstudiante = @idEstudiante";

                using (SqlCommand comando = new SqlCommand(query, miConexion.conexion))
                {
                    comando.Parameters.AddWithValue("@idEstudiante", IdEstudiante);
                    
                    miConexion.conexion.Open();
                   
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            estudiante = new EstudianteViewModel()
                            {
                                IdEstudiante = (int)reader["IdEstudiante"],
                                NombreEstudiante = (string)reader["NombreEstudiante"],
                                ApellidoEstudiante = (string)reader["ApellidoEstudiante"],
                                IdUsuario = (int)reader["IdUsuario"],
                                CorreoUsuario = (string)reader["CorreoUsuario"],
                                IdTipoUsuario = (int)reader["IdTipoUsuario"],
                                TipoUsuario = (string)reader["TipoUsuario"],
                                IdMateria = (int)reader["IdMateria"],
                                NombreMateria = (string)reader["NombreMateria"],
                                IdNota = (int)reader["IdNota"],
                                Evaluacion1 = (decimal)reader["Evaluacion1"],
                                Evaluacion2 = (decimal)reader["Evaluacion2"],
                                Evaluacion3 = (decimal)reader["Evaluacion3"],
                                Evaluacion4 = (decimal)reader["Evaluacion4"],
                                Evaluacion5 = (decimal)reader["Evaluacion5"],
                                Promedio = (decimal)reader["Promedio"]
                            };

                            // Agregar el estudiante a la lista
                            listaestudiante.Add(estudiante);
                        }
                    }
                }
                return listaestudiante;
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

        //Método para Actualizar los datos del estudiante por medio de su código
        public int ActualizarEstudianteID(List<EstudianteViewModel> registrosestudiante)
        {
            try
            {
                miConexion.Conectar();

                 miConexion.conexion.Open();

                using (SqlTransaction transacciones = miConexion.conexion.BeginTransaction())
                {
                    try
                    {
                        //Actualizamos los datos de la tabla Estudiante
                        var Actualizarestudiante = "update Estudiante set NombreEstudiante = @NombreEstudiante, ApellidoEstudiante = @ApellidoEstudiante , IdUsuario = @IdUsuario where IdEstudiante = @IdEstudiante";

                        SqlCommand comandoestudiante = new SqlCommand(Actualizarestudiante, miConexion.conexion, transacciones);

                        comandoestudiante.Parameters.Add("@NombreEstudiante", SqlDbType.VarChar);
                        comandoestudiante.Parameters["@NombreEstudiante"].Value = registrosestudiante[0].NombreEstudiante;

                        comandoestudiante.Parameters.Add("@ApellidoEstudiante", SqlDbType.VarChar);
                        comandoestudiante.Parameters["@ApellidoEstudiante"].Value = registrosestudiante[0].ApellidoEstudiante;

                        comandoestudiante.Parameters.Add("@IdUsuario", SqlDbType.Int);
                        comandoestudiante.Parameters["@IdUsuario"].Value = registrosestudiante[0].IdUsuario;

                        comandoestudiante.Parameters.Add("@IdEstudiante", SqlDbType.Int);
                        comandoestudiante.Parameters["@IdEstudiante"].Value = registrosestudiante[0].IdEstudiante;
                        comandoestudiante.ExecuteNonQuery();

                        //Actualizamos los datos de la tabla Usuario
                        var queryusuario = "update Usuario set CorreoUsuario = @CorreoUsuario where IdUsuario = @IdUsuario";

                        SqlCommand comandoUsuario = new SqlCommand(queryusuario, miConexion.conexion, transacciones);
                        comandoUsuario.Parameters.Add("@CorreoUsuario", SqlDbType.VarChar);
                        comandoUsuario.Parameters["@CorreoUsuario"].Value = registrosestudiante[0].CorreoUsuario;

                        comandoUsuario.Parameters.Add("@IdUsuario", SqlDbType.Int);
                        comandoUsuario.Parameters["@IdUsuario"].Value = registrosestudiante[0].IdUsuario;
                        comandoUsuario.ExecuteNonQuery();

                        for (int i = 0; i < registrosestudiante.Count; i++)
                        {
                            //Actualizamos los datos de la tabla Notas
                            var Actualizarnota = "update Notas set Evaluacion1 = @Evaluacion1 , Evaluacion2 = @Evaluacion2 , Evaluacion3 = @Evaluacion3, Evaluacion4 = @Evaluacion4, Evaluacion5 = @Evaluacion5 where IdNota = @IdNota";

                            SqlCommand comandonotas = new SqlCommand(Actualizarnota, miConexion.conexion, transacciones);

                            comandonotas.Parameters.Add("@Evaluacion1", SqlDbType.Decimal);
                            comandonotas.Parameters["@Evaluacion1"].Value = registrosestudiante[i].Evaluacion1;

                            comandonotas.Parameters.Add("@Evaluacion2", SqlDbType.Decimal);
                            comandonotas.Parameters["@Evaluacion2"].Value = registrosestudiante[i].Evaluacion2;

                            comandonotas.Parameters.Add("@Evaluacion3", SqlDbType.Decimal);
                            comandonotas.Parameters["@Evaluacion3"].Value = registrosestudiante[i].Evaluacion3;

                            comandonotas.Parameters.Add("@Evaluacion4", SqlDbType.Decimal);
                            comandonotas.Parameters["@Evaluacion4"].Value = registrosestudiante[i].Evaluacion4;

                            comandonotas.Parameters.Add("@Evaluacion5", SqlDbType.Decimal);
                            comandonotas.Parameters["@Evaluacion5"].Value = registrosestudiante[i].Evaluacion5;

                            comandonotas.Parameters.Add("@IdNota", SqlDbType.Int);
                            comandonotas.Parameters["@IdNota"].Value = registrosestudiante[i].IdNota;

                            comandonotas.ExecuteNonQuery();
                        }

                        // Confirmar la transacción si todo está bien
                        transacciones.Commit();

                        return 1;
                    }
                    catch (Exception)
                    {
                        // Si ocurre un error, revertir la transacción
                        transacciones.Rollback();
                        throw; // Puedes manejar el error según tus necesidades
                    }
                }
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



    }
}