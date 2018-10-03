using MySql.Data.MySqlClient;
using NTT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTT.Controllers
{
    public class AcercadeController : Controller
    {
        Modelo model = new Modelo();
 
        public ActionResult Acercade()
        {
            MySqlDataReader r = model.Consulta("select * from informacion");
            while (r.Read())
            {
                ViewBag.NombrePrincipal = r.GetString("nombresoft");
                ViewBag.Eslogan = r.GetString("eslogan");
            }
            return View();
        }
    }
}