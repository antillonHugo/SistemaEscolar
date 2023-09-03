using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistemaEscolar.Models;

namespace SistemaEscolar.ViewModels
{
    public class ListaMateriasTPUsuarioViewModel
    {

        public List<TipoUsuario> TiposUsuario { get; set; }
        public List<Materia> Materias { get; set; }


    }
}