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

    
    public class UserController : Controller

    {

        [HttpGet]
        public ActionResult ConsultarPerfilUsuario()
        {
            using (var dbContext = new KNDatabaseEntities2())
            {
                var idUsuario = long.Parse(Session["IdUsuario"].ToString());
                var result = dbContext.TUsuario.FirstOrDefault(u => u.IdUsuario == idUsuario);

                if (result != null)
                {
                    var datos = new Usuario
                    {
                        Nombre = result.Nombre
                    };

                    return View(datos);
                }

                return View();

            }
        }

    }









    }





    