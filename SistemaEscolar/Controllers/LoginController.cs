using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SistemaEscolar.Services;
using SistemaEscolar.Models;
using SistemaEscolar.Helpers;

namespace SistemaEscolar.Controllers
{
    public class LoginController : Controller
    {
        //instanciamos la clase que nos permite realizar el CRUD 
        ClsUsuarioMantenimiento objUsuario = new ClsUsuarioMantenimiento();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Usuario usuario)
        {
            //Eliminamos algunos caracteres especiales
           var correo= LimpiarCampos.EliminarCaracteresEspeciales(usuario.CorreoUsuario);
           var contrasena = LimpiarCampos.EliminarCaracteresEspeciales(usuario.ContrasenaUsuario);

            if (!string.IsNullOrWhiteSpace(correo) && !string.IsNullOrWhiteSpace(contrasena))
            {
                //verificamos las credenciales del usuario
                var respuesta = objUsuario.ValidarCredenciales(correo.Trim(), contrasena.Trim());

                if (respuesta !=null)
                {
                    //si existe el usuario
                    if (respuesta.Count > 0)
                    {
                        //convertimos el elemento 1 de nuestra lista a int que es el idTipoUsuario para dedirigirlo a su área de trabajo
                        var idusuario = Convert.ToInt32(respuesta[0]);
                        var idtipo = Convert.ToInt32(respuesta[1]);

                        //creamos varibles de sesión para almacenar la información del suario y hacer accesible en todo el sistema
                        Session["TipoUsuario"] = idtipo;
                        Session["Nombreusuario"] = respuesta[2];
                        Session["idusuario"] = idusuario;

                        //evaluamos si es alumno o profesor
                        if (idtipo == 1)
                        {
                            return RedirectToAction("Index", "Estudiante", new { estudiantetId = idusuario });
                            //return RedirectToAction("Index", "Estudiante");

                        }
                        else
                        {
                            return RedirectToAction("Index", "Profesor");
                        }
                    }
                }

                ViewBag.Message = "Debe ingresar un usuario y contraseña válidos";

                return View();
            }

            ViewBag.Message = "Debe ingresar un usuario y contraseña válidos";
            return View();
        }
    }
}
