using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistemaEscolar.Models;

namespace SistemaEscolar.ViewModels
{
    public class ListaMateriasTPUsuarioViewModel
    {

        public EstudianteViewModel Estudiante { get; set; }
        public List<Materia> Materias { get; set; }
        public List<TipoUsuario> TiposDeUsuarios { get; set; }


    }
}