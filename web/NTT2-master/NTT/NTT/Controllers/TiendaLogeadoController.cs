using NTT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using System.Collections;
using NTT;
using System.IO;

namespace NewTailor123.Controllers
{
    public class TiendaLogeadoController : Controller
    {
        Prenda_Model model = new Prenda_Model();
        Modelo modelo = new Modelo(); 
        MySqlDataReader anterior;
        // GET: TiendaLogeado

 

        [HttpGet]
        public ActionResult MiTienda(Modelo  m)
        {
            if (Session["name"] != null)
            {
                MySqlDataReader r  = modelo.Consulta("select idtienda from tienda where idusuario=" + Session["id"]);
                while (r.Read()) {
                    Session["idtienda"] = r.GetInt32("idtienda");
                }
                MySqlDataReader res = modelo.Consulta("select count(*) from prenda where idtienda="+ Session["idtienda"] + " and estado='A'");
                while (res.Read())
                {
                    Session["numeroprendas"] = res.GetInt32("count(*)");
                }
                res = modelo.Consulta("select * from tienda where idtienda=" + Session["idtienda"]);
                 while (res.Read())
                {           
                        ViewBag.foto = res.GetString("foto");   
                        Session["portada"] = res.GetString("portada");
                 }
                res = modelo.Consulta("select count(*) from prenda inner join tallacolor on tallacolor.idprenda = prenda.idprenda inner join detallepedido on detallepedido.idtc = tallacolor.idtc where prenda.idtienda = " + Session["key"].ToString() );
                while (res.Read()) {
                    ViewBag.ventas = res.GetInt32("count(*)");
                } 
                m.temp = modelo.DataConsulta("select idprenda, prenda.nombreprenda, precio,  genero, descripcion,cantidad, idtienda,foto from prenda WHERE idtienda=" + Session["key"] + " and prenda.estado='A'"); 
                return View(m);
            }
            else
            {
                return RedirectToAction("Login", "Login"); 
            } 
        }

        public ActionResult RePost(string id) {
            MySqlDataReader c = model.consulta("select distinct talla.idtalla, nombretalla from talla inner join tallacolor on tallacolor.idtalla=talla.idtalla inner join prenda on tallacolor.idprenda=prenda.idprenda where tallacolor.idprenda=" + id + " and tallacolor.estado='D'");
            List<SelectListItem> talla = new List<SelectListItem>();
            while (c.Read())
            {
                talla.Add(new SelectListItem() { Text = c.GetString("nombretalla"), Value = "" + c.GetInt32("idtalla") + "" });
            } 
            ViewBag.Talla = new SelectList(talla, "Value", "Text");
            return PartialView("productpage");
        }

        public ActionResult productpage(Tienda_Model m,string id, string nombre, string descripcion, string precio, string cantidad,string fot,string empresa) {
            if (Session["name"] != null)
            {
                if (id!= null )
                {
                    Session["idprenda"] = id;
                }
                if (empresa!=null)
                {
                    Session["empresaid"] = empresa;
                }
                MySqlDataReader c = model.consulta("select distinct talla.idtalla, nombretalla from talla inner join tallacolor on tallacolor.idtalla=talla.idtalla inner join prenda on tallacolor.idprenda=prenda.idprenda where tallacolor.idprenda=" + Session["idprenda"] + " and tallacolor.estado='D'");
                List<SelectListItem> talla = new List<SelectListItem>();
                while (c.Read())
                {
                    talla.Add(new SelectListItem() { Text = c.GetString("nombretalla"), Value = "" + c.GetInt32("idtalla") + "" });
                }
                ViewBag.Talla = new SelectList(talla, "Value", "Text");
                c = model.consulta("select distinct  color.idcolor, nombrecolor from color inner join tallacolor on tallacolor.idcolor=color.idcolor inner join prenda on tallacolor.idprenda=prenda.idprenda where tallacolor.idprenda=" + Session["idprenda"] + " and tallacolor.estado='D'");
                List<SelectListItem> color = new List<SelectListItem>();
                while (c.Read())
                {
                    color.Add(new SelectListItem() { Text = c.GetString("nombrecolor"), Value = "" + c.GetInt32("idcolor") + "" });
                }
                ViewBag.Color = new SelectList(color, "Value", "Text");
                ViewBag.nombreprenda = nombre;
                ViewBag.descripcionprenda = descripcion;
                ViewBag.precioprenda = precio;
                ViewBag.cantidad = cantidad;
                ViewBag.foto = fot;
                c = model.consulta("select nombretienda from tienda where idtienda=" + Session["empresaid"]);
                while (c.Read())
                {
                    ViewBag.empresa = c.GetString("nombretienda");
                }

                ViewBag.id = Session["idprenda"];

                m.temp = model.consultaprenda();

                return View(m);
            }
            else
            {
                return RedirectToAction("Login", "Login");

            }
                     
         }

        public ActionResult MisVentas(Modelo m)
        {
            if (Session["name"] != null)
            {
                m.temp = modelo.DataConsulta("select prenda.idprenda, nombreprenda, detallepedido.cantidad,subtotal from prenda inner join tallacolor on tallacolor.idprenda=prenda.idprenda inner join detallepedido on detallepedido.idtc=tallacolor.idtc where prenda.idtienda=" + Session["key"].ToString());
                return View(m);
            }
            else
            {
                return RedirectToAction("Login", "Login");

            }
        }


        public ActionResult MisPrendas(Modelo m) {
            if (Session["name"] != null)
            {
                m.temp = modelo.DataConsulta("select idprenda, prenda.nombreprenda, precio, cantidad  from prenda WHERE idtienda=" + Session["key"].ToString() + " and prenda.estado='A'"); 
                return View(m);
            }
            else
            {
                return RedirectToAction("Login", "Login");

            }
        }
        public ActionResult eliminar(Tienda_Model m, int id)
        {
             m.eliminar(id);
             return RedirectToAction("MisPrendas","TiendaLogeado"); 
        }

        

        [HttpGet]
        public ActionResult Actualizarinformacion()
        {             
            if (Session["name"] != null)
            {
                anterior = modelo.Consulta("select idtienda from tienda where idusuario=" + (int)Session["id"] ) ;
                while (anterior.Read())
                {
                    Session["idtienda"] = anterior.GetInt32("idtienda");
                }
                
                anterior = model.consulta("select * from tienda WHERE idtienda="+Session["idtienda"]);
                while (anterior.Read())
                {
                    ViewBag.nombreem = anterior.GetString("nombretienda");
                    ViewBag.telefonoem = anterior.GetString("telefono");
                    ViewBag.emailem = anterior.GetString("email");
                    ViewBag.id = anterior.GetInt32("idtienda");
                    Session["fotoem"] = anterior.GetString("foto");
                    Session["portada"] = anterior.GetString("portada"); 
                }
                return View(); 
            }
            else
            {
                return RedirectToAction("Login", "Login"); 
            }
        }
        [HttpPost]
        public ActionResult Actualizarinformacion(Registro_Model mod, HttpPostedFileBase file, HttpPostedFileBase portada)
        {
            var filename = "";
            var filename2 = "";
            if (file != null)
            {
                 filename = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Image/"), filename);
                file.SaveAs(path);
                ViewBag.showSuccessAlert = true;
            }
            else {
                filename = Session["fotoem"].ToString();
            }
            if (portada != null)
            {
                filename2 = Path.GetFileName(portada.FileName);
                var path = Path.Combine(Server.MapPath("~/Image/"), filename2);
                portada.SaveAs(path);
                ViewBag.showSuccessAlert = true;
            }
            else
            {
                filename2 = Session["portada"].ToString();
            } 
            bool x = modelo.Inserccion("update tienda set nombretienda='" + mod.nombretienda + "', telefono='" + mod.telefonotienda +"', email='"+mod.emailtienda + "', idtienda= " +mod.nit +", foto ='" +filename + "', portada ='" + filename2 + "' WHERE idtienda=" + Session["idtienda"]);
            if (x)
            {
                Session["ver"] = mod.nombretienda;
                //Session["name"] = mod.usuario; 
                return RedirectToAction("ActualizarInformacion", "TiendaLogeado" );
            }
            else
            {
                return View();
            }
        }
   
    }
}