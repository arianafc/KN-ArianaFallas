using KProyecto.EF;
using KProyecto.Models;
using KProyecto.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.EnterpriseServices;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace KProyecto.Controllers
{

    
    public class HomeController : Controller

    {
        Utilitarios service = new Utilitarios(); //Instancia del servicio de utilitarios, para usar sus métodos
        #region Index

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
            using (var dbContext = new KNDatabaseEntities2())
            {
                var result = dbContext.TUsuario.FirstOrDefault(u => u.Correo == autenticacion.Correo

                && u.Contrasenna == autenticacion.Contrasenna);


                if (result != null)
                {
                    //GUARDAR INFO EN VARIABLES DE SESSION
                    Session["Nombre"] = result.Nombre; //No son accesibles por el usuario, son del servidor
                    Session["IdUsuario"] = result.IdUsuario; //ID del usuario autenticado
                    return RedirectToAction("Principal", "Home");


                }
                ViewBag.Mensaje = "No se pudo validar su información"; //mandar algo a la vista sin que forme parte del controlador

                return View();
            }


            //return RedirectToAction("Principal", "Home"); //redireccionamiento
        }
        #endregion


        #region RecuperarContrasenna

        [HttpGet]
        public ActionResult RecuperarContrasenna()
        {
            //abre vista
            return View();
        }

        [HttpPost]
        public ActionResult RecuperarContrasenna(Autenticacion a)
        {
            //recupera contraseña

            //BUSCAR EL REGISTRO EN LA BASE DE DATOS

            using (var dbContext = new KNDatabaseEntities2())
            {

                var result = dbContext.TUsuario.FirstOrDefault(u => u.Correo == a.Correo);

                if (result != null)
                {
                    StringBuilder mensaje = new StringBuilder();

                    mensaje.Append("Estimado " + result.Nombre + "<br/>");
                    mensaje.Append("Se ha generado una solicitud de recuperación de contraseña a su nombre<br/>");
                    mensaje.Append("Su contraseña temporal es: " + result.Contrasenna + "\n\n");

                    mensaje.Append("Procure realizar el cambio de su contraseña en cuanto ingrese al sistema\n");
                    mensaje.Append("Muchas gracias\n");

                    if (service.EnviarCorreo(result.Correo, mensaje.ToString(), "Solicitud de acceso"))
                        return RedirectToAction("Index", "Home");

                    ViewBag.Mensaje = "No se pudo realizar la notificación de su acceso al sistema";
                    return View();
                }

                ViewBag.Mensaje = "No se pudo recuperar su contraseña"; //mandar algo a la vista sin que forme parte del controlador
                return View(); //retorna a la vista de recuperar contraseña

            }
            
        }

        #endregion RecuperarContrasenna


        #region Registro
        [HttpGet]

        public ActionResult Registro()
        {
            return View();

        }

        [HttpPost]

        public ActionResult Registro(Autenticacion autenticacion)
        {
            using (var dbContext = new KNDatabaseEntities2())
            {
                dbContext.TUsuario.Add(new TUsuario
                {
                    Identificacion = autenticacion.Identificacion,
                    Nombre = autenticacion.Nombre,
                    Correo = autenticacion.Correo,
                    Contrasenna = autenticacion.Contrasenna
                });

                var result = dbContext.SaveChanges();
                //var result2 = dbContext.RegistroUsuario(autenticacion.Nombre, autenticacion.Correo, autenticacion.Contrasenna, autenticacion.NombreUsuario);

                if (result > 0)
                {
                    return RedirectToAction("Index", "Home"); //SI TENEMOS MAS DE UNA LINEA EN EL IF, NO DEBEMOS PONER LLAVES
                }
                ViewBag.Mensaje = "No se pudo registrar su información";
                return View();
            }




        }

        #endregion


        [HttpGet]
        public ActionResult Principal()
        {

            return View();

        }


       
            

        }
    }





    