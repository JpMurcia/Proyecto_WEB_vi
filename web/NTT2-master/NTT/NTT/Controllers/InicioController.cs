using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using NTT.Models;

namespace NTT.Controllers
{
    public class InicioController : Controller
    {
        Modelo model = new Modelo();
 
        [HttpGet]
        public ActionResult Index(Modelo m)
        {
          
            MySqlDataReader res = model.Consulta("Select count(*) from prenda");
            while (res.Read())
            {
                ViewBag.numeroprendas = res.GetInt32("count(*)");
            }
            if (Session["texto"] != null)
            {
                m.temp = model.DataConsulta("select idprenda, nombreprenda, precio , genero, descripcion,cantidad, idtienda,foto from prenda where nombreprenda like '%" + Session["texto"] + "%' and estado ='A'");
                Session["texto"] = null;
            }
            else {
                m.temp = model.DataConsulta("select idprenda, nombreprenda, precio, genero, descripcion,cantidad, idtienda,foto from prenda where estado='A' limit 10 ");
            }

            MySqlDataReader r = model.Consulta("select * from informacion");
            while (r.Read())
            {
                ViewBag.NombrePrincipal = r.GetString("nombresoft");
                ViewBag.Eslogan = r.GetString("eslogan");
            } 
             if (Session["rol"]!=null) {
                if (Session["rol"].ToString() == "2") {        
                    MySqlDataReader ss = model.Consulta("Select count(*) from detallepedido where idpedido=" + Session["carro"]);
                    while (ss.Read())
                    {
                        Session["carrito"] = ss.GetInt32("count(*)");
                    }
                }
            }  
            return View(m);          
        }

        [HttpPost]
        public ActionResult Index(Modelo m, string x)
        {
            Session["texto"] = m.texto;
            return RedirectToAction("Index", "Inicio");

        }
 
        public ActionResult Error()
        {
            return View();
         }
        
        public ActionResult terminosycondiciones()
        {
            return View();
        }
 
    }
    
}
 