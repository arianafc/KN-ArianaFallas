using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KProyecto.Models;

namespace KProyecto.Controllers
{
    public class HomeController : Controller
    {

        //todas las acciones van en pares, es decir, dos index usualmente. 1 es un http get y el otro http post

        [HttpGet] //A nivel de web, se usan para abrir las vistas
        public ActionResult Index() 
        {
            //var autenticacion = new Autenticacion();
            //autenticacion.NombreUsuario = "Hola";

            // return View(autenticacion);

            return View();
        }

        [HttpPost] //Ejecutan acciones de las vistas con botones de tipo submit
        public ActionResult Index(Autenticacion autenticacion)
        {

            ViewBag.Mensaje = "No se pudo validar su información"; //mandar algo a la vista sin que forme parte del controlador

            return View();

            //return RedirectToAction("Principal", "Home"); //redireccionamiento
        }

        [HttpGet]

        public ActionResult Registro()
        {
            return View();

        }















        [HttpGet]
        public ActionResult Principal()
        {
            return View();
        
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}