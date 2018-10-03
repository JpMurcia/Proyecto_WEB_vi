using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using NTT.Models;

namespace NTT.Controllers
{
    public class SubirPrendaController : Controller
    {
        Prenda_Model model = new Prenda_Model();
        Modelo modelo = new Modelo();

        [HttpGet]
        public ActionResult SubirPrenda(Prenda_Model mod)
        {
            if (Session["name"] != null)
            {

                ViewBag.showSuccessAlert = false;

                mod.temp= model.DataConsulta("select * from talla");
                mod.talla = new string[mod.temp.Tables[0].Rows.Count]; 
                mod.temp2 = model.DataConsulta("select * from color");
                mod.color = new string[mod.temp2.Tables[0].Rows.Count];
                mod.cantidad = new int[mod.temp.Tables[0].Rows.Count * mod.temp2.Tables[0].Rows.Count];
                return View(mod);

            }
            else
            {
                return RedirectToAction("Login", "Login");

            }

        }
 

        [ComVisibleAttribute(false)]
        public virtual int ReadTimeout { get; set; }


        [HttpPost]
        public ActionResult SubirPrenda(Prenda_Model mod,string x, HttpPostedFileBase file   )
        {
            if (Session["name"] != null)
            {
                var filename = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Image/"), filename);
                file.SaveAs(path);    
                ViewBag.showSuccessAlert = true;  
                mod.idempresa = model.consultaidemp((int)Session["id"]);
                bool ver = model.Registrar("insert into prenda(nombreprenda,precio,genero,descripcion,idtienda,foto) values('" + mod.nombreprenda + "'," + mod.precio + ",'" + mod.genero + "','" + mod.descripcion + "',"  + mod.idempresa +",'"+filename+"')");
                MySqlDataReader res = model.consulta("select idprenda from prenda where idtienda="+ mod.idempresa +" and nombreprenda='"+mod.nombreprenda+"'");
                while (res.Read())
                {
                    int t = 1;
                    int c = 1;
                    for (int i = 0; i < mod.cantidad.Length; i++)
                    { 
                        bool vr = model.Registrar("insert into tallacolor(idtalla,idcolor,cantidad, idprenda) values(" + t + "," + c + "," + mod.cantidad[i] + "," + res.GetInt32("idprenda") + ")");
                        c++;
                        if (t==6)
                        {
                            t = 1;
                        }
                        if (c==12)
                        {
                            c = 1;
                            t++;
                        }
                    }  
                }
                if (ver)
                {
                    Session["prenda"] = "1";
                    return RedirectToAction("MiTienda", "TiendaLogeado");
                }
                else {
                    return View();
                }   
            }
            else
            {
                return RedirectToAction("Login", "Login");

            }
        }
        public ActionResult Confirmacion() {
            if (Session["name"] != null)
            {
                ViewBag.showSuccessAlert = true;
                return View();
            }
            else {
                return RedirectToAction("Login", "Login");
            }

        }
        public ActionResult editarprenda(Prenda_Model mod,int idprenda)
        {
            if (Session["name"] != null)
            {
                Session["idprenda"] = idprenda;
                MySqlDataReader rs = modelo.Consulta("select nombreprenda,descripcion,precio from prenda where idprenda="+idprenda ); 
                while (rs.Read())
                {
                    ViewBag.nombreprenda = rs.GetString("nombreprenda");
                    ViewData["descripcion"] = rs.GetString("descripcion");
                    ViewBag.precio = rs.GetInt32("precio");
                }
                mod.temp = model.DataConsulta("select * from talla");
                mod.talla = new string[mod.temp.Tables[0].Rows.Count];
                mod.temp2 = model.DataConsulta("select * from color");
                mod.color = new string[mod.temp2.Tables[0].Rows.Count];
                mod.cantidad = new int[mod.temp.Tables[0].Rows.Count * mod.temp2.Tables[0].Rows.Count];
                rs = modelo.Consulta("select cantidad from tallacolor where idprenda=" + idprenda);
                int i = 0;
                while (rs.Read())
                {
                    mod.cantidad[i] = rs.GetInt32("cantidad");
                    i++;
                }
                return View(mod); 
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        [HttpPost]
        public ActionResult editarprenda(Prenda_Model m)
        {
            if (Session["name"] != null)
            {
                if (m.descripcion==null)
                {
                    MySqlDataReader rs = modelo.Consulta("select descripcion from prenda where idprenda=" + Session["idprenda"]);
                    while (rs.Read())
                    {
                        m.descripcion = rs.GetString("descripcion");
                    }
                }
                bool x = modelo.Inserccion("update prenda set nombreprenda='" + m.nombreprenda + "', descripcion='" + m.descripcion + "', precio=" + m.precio + " WHERE idprenda=" + Session["idprenda"]);

                int t = 1;
                int c = 1;
                for (int i = 0; i < m.cantidad.Length; i++)
                {
                    bool vr = model.Registrar("update tallacolor set cantidad=" + m.cantidad[i] + " where idtalla=" +t +" and idcolor=" +c +" and idprenda=" + Session["idprenda"]);
                    c++;
                    if (t == 6)
                    {
                        t = 1;
                    }
                    if (c == 12)
                    {
                        c = 1;
                        t++;
                    }
                }
                if (x)
                { 
                    return RedirectToAction("MiTienda", "TiendaLogeado");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
    }
}