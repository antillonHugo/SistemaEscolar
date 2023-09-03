using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

using SistemaEscolar.Models;

namespace SistemaEscolar.Services
{
    public class ClsMateriaMantenimiento
    {
        //instancia a la clase que posee la cadena de conexion a sql server
        Conexion miConexion = new Conexion();

        //lista para guardar a las materias
        List<Materia> ListaMaterias = new List<Materia>();

        Materia materias = new Materia();

        //Método para extraer el listado de los tipos de usuarios existentes
        public List<Materia> ObtenerMaterias()
        {

            try
            {
                miConexion.Conectar();

                var sql = "select IdMateria,NombreMateria from Materia";

                using (SqlCommand comando = new SqlCommand(sql, miConexion.conexion))
                {
                    miConexion.conexion.Open();

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            materias = new Materia()
                            {
                                IdMateria = (int)reader["IdMateria"],
                                NombreMateria = (string)reader["NombreMateria"]
                            };

                            // Agregar las materis  a la lista
                            ListaMaterias.Add(materias);
                        }
                    }
                }

                return ListaMaterias;
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error al buscar las materias. Por favor, inténtalo de nuevo más tarde." + ex.Message);
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