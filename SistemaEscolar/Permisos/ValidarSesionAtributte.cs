using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaEscolar.Permisos
{
    public class ValidarSesionAtributte : ActionFilterAttribute
    {
        //OnActionExecuting evitara que podamos ingresar por medio de la url a nuestro sistema 
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //validamos que se cree una variable se sesión llamada Session["idusuario"] de lo contrario nos redigira al login 
            if (HttpContext.Current.Session["idusuario"] == null)
            {
                filterContext.Result = new RedirectResult("~/Login/Index");
            }

            base.OnActionExecuting(filterContext);
        }
    }
}