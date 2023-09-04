using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SistemaEscolar.Permisos;
using SistemaEscolar.Services;
using SistemaEscolar.Models;
using SistemaEscolar.ViewModels;
using SistemaEscolar.Helpers;

namespace SistemaEscolar.Controllers
{
    [ValidarSesionAtributte]
    public class EstudianteController : Controller
    {
        //instanciamos la clase que se encarga de realizar el CRUD o la logíca 
        private ClsEstudianteMantenimiento objestudiante;

        //instanciamos la clase que se encarga de realizar el CRUD o la logíca de los Tipos de Usuarios
        private ClsTipoUsuarioMantenimiento objtpusuario;

        //instanciamos la clase que se encarga de realizar el CRUD o la logíca de las materias
        private ClsMateriaMantenimiento objmateria;

        //Método constructor
        public EstudianteController()
        {
            //inicializamos las dependencias
            objtpusuario = new ClsTipoUsuarioMantenimiento();
            objestudiante = new ClsEstudianteMantenimiento();
            objmateria = new ClsMateriaMantenimiento();
        }

        // GET: lista que se mostrara el estudiante
        public ActionResult Index(int estudiantetId)
        {
            var estudiante = objestudiante.buscarEstudiantesID(estudiantetId);

            return PartialView("_BusquedastudianteID", estudiante);
        }

        //Controlador para listar todos los alumnos
        public ActionResult ListadoEstudiantes()
        {
            var registros = objestudiante.ListaEstudiantes();

            if (registros != null)
            {
                return View(registros);
            }

            ViewBag.Mensaje = "No se encontraron registros de estudiantes.";
            return View();
        }
        
        public ActionResult BuscarEstudiantesPorID(int estudiantetId)
        {
            var registro = objestudiante.buscarEstudiantesID(estudiantetId);

            return PartialView("_BusquedastudianteID", registro);
        }

        //Controlador para realizar filtro de estudiantes
        public ActionResult FiltrarEstudiante(string searchTerm)
        {
            var filtro=LimpiarCampos.EliminarCaracteresEspeciales(searchTerm);

            var registro = objestudiante.FiltrarEstudiante(filtro);

            // Devuelve una vista parcial con los resultados
            return PartialView("_data", registro);
        }

        //Controlador para editar el estudiante GET
        public ActionResult EditarEstudiante(int estudiantetId)
        {
            var registro = objestudiante.buscarEstudiantesID(estudiantetId);

            return PartialView("_EditarEstudianteID", registro);
        }
        
        //Controlador para editar el estudiante mediante POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActualizarEstudiante(List<EstudianteViewModel> estudiante)
        {
            // Aquí aplicas las modificaciones al modelo original basado en los datos recibidos
            var registro = objestudiante.ActualizarEstudianteID(estudiante);

            if (registro>0)
            {
                ViewBag.Message = "Datos Actualizados correctamente";
                return RedirectToAction("ListadoEstudiantes");
            }
            ViewBag.Message = "Error al Actualizar los Datos";

            return RedirectToAction("Index");
        }

        //Controlador para Añadir un estudiante GET
        [HttpGet]
        public ActionResult AgregarEstudiante()
        {
            var listamaterias = objmateria.ObtenerMaterias();
            var listatiposdeUsuarios =objtpusuario.ObtenerTiposUsuario();

            ViewBag.TiposDeUsuarios = listatiposdeUsuarios;
            ViewBag.Materias = listamaterias;

            return PartialView("_AgregarEstudiante");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //Controlador para Añadir un estudiante POST
        public ActionResult AgregarEstudiante(EstudianteViewModel inscribirEstudiante)
        {
            return PartialView("_Mensaje");
        }


        //cerrramos sesión
        public ActionResult CerrarSesion()
        {
            Session["TipoUsuario"] = null;
            Session["Nombreusuario"] = null;
            Session["idusuario"] = null;

            return RedirectToAction("index", "Login");
        }
    }
}