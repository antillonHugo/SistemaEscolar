using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaEscolar.Controllers
{
    public class ProfesorController : Controller
    {
        // GET: Profesor
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListadoProfesores()
        {
            return View();
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