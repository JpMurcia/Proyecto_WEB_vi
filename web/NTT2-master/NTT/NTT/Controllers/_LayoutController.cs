using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using NTT.Models;

namespace NTT.Controllers
{
    public class _LayoutController : Controller
    {
        Prenda_Model model = new Prenda_Model();

        // GET: _Layout
        public ActionResult _Layout(Prenda_Model m)
        {

            if (Session["name"] != null)
            {
                /*Session["idtienda"] = model.consultaidemp((int)Session["id"]);
                MySqlDataReader res = model.consulta("Select count(*) from permisos where id_rol=" + model.consultaidemp((int)Session["rol"]));
                while (res.Read())
                {
                    ViewBag.numeroprendas = res.GetInt32("count(*)");
                }
                */
                MySqlDataReader res = model.consulta("Select count(*) from carro where idcliente=" + Session["key"]);
                while (res.Read())
                {
                    ViewBag.carrito = res.GetInt32("count(*)");

                } 
                m.temp = model.DataConsulta("select idprenda, prenda.nombreprenda, precio, genero, descripcion,cantidad, idtienda,foto from prenda");
                return View(m);
            }
            else
            {
                return RedirectToAction("Login", "Login");

            }
        }
    }
}