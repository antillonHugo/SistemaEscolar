using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaEscolar.ViewModels
{
    public class EstudianteViewModel
    {
        public int IdEstudiante { get; set; }
        public string NombreEstudiante { get; set; }
        public string ApellidoEstudiante { get; set; }

        public int IdUsuario { get; set; }
        public string CorreoUsuario { get; set; }
        public string ContrasenaUsuario { get; set; }

        public int IdTipoUsuario { get; set; }
        public string TipoUsuario { get; set; }

        public int IdMateria { get; set; }
        public string NombreMateria { get; set; }

        public int IdNota { get; set; }
        
        public decimal Evaluacion1 { get; set; }
        public decimal Evaluacion2 { get; set; }
        public decimal Evaluacion3 { get; set; }
        public decimal Evaluacion4 { get; set; }
        public decimal Evaluacion5 { get; set; }
        public decimal Promedio { get; set; }
    }
}