using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaEscolar.Controllers
{
    public class InscripcionesController : Controller
    {
        // GET: Inscripciones
        public ActionResult Index()
        {
            return View();
        }
    }
}