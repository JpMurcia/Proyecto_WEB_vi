using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using NTT;
using NTT.Models;

namespace NewTailor123.Controllers
{
    public class AdministradorController : Controller
    {
    
        AdminModel model = new AdminModel();
         [HttpGet]
        public ActionResult PerfilAdmin()
        {
            if (Session["name"] != null)
            {
                MySqlDataReader res = model.consulta("select count(*) from prenda");
                while (res.Read())
                {
                    ViewBag.producto = res.GetInt32("count(*)");
                }       
                res = model.consulta("select count(*) from cliente");
                while (res.Read())
                {
                    ViewBag.clientes = res.GetInt32("count(*)");
                }
                res = model.consulta("select count(*) from tienda");
                while (res.Read())
                {
                    ViewBag.tiendas = res.GetInt32("count(*)");
                }
                res = model.consulta("select count(*) from mensaje where estado='No Leido'");
                while (res.Read())
                {
                    Session["mensajes"] = res.GetInt32("count(*)");
                }
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login"); 
            }
        }
        public ActionResult Permisos() {
            if (Session["name"] != null)
            {
                return View(); 
            }
            else
            {
                return RedirectToAction("Login", "Login");

            }
        }


        //CREAR MENUS
        public IHtmlString crearmenu() {
            string finalpermisos="<i>";
             IHtmlString response = new HtmlString(finalpermisos);
 
                        return response;
         }


        public ActionResult CrearCliente() {
            if (Session["name"] != null)
            {
                return View(); 
            }
            else
            {
                return RedirectToAction("Login", "Login");

            }
        }
        public ActionResult CrearTienda()
        {
            if (Session["name"] != null)
            {
                return View(); 
            }
            else
            {
                return RedirectToAction("Login", "Login");

            }
        }

        [HttpGet]
        public ActionResult TablaRoles(AdminModel m, string n)
        {
            if (Session["name"] != null)
            {
                ViewBag.seleccion = n;
                if (n=="cliente")
                {
                    m.temp = model.DataConsulta("select idcliente,primernombre,segundonombre,primerapellido,segundoapellido,email,telefono from cliente");
                }
                else if (n=="tienda")
                {
                    m.temp = model.DataConsulta("select idtienda,nombretienda,telefono,email from tienda");
                }
                else if (n=="administrador")
                {
                    m.temp = model.DataConsulta("select idusuario,usuarionombre from usuario where idrol=3");
                }        
                return View(m); 
            }
            else
            {
                return RedirectToAction("Login", "Login");

            }
        }
  
        public ActionResult CrearAdministrador()
        {
            if (Session["name"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");

            }
        }
        [HttpGet]
        public ActionResult Tabla(AdminModel m, string n) {
            if (Session["name"] != null)
            {
                ViewBag.seleccion = n;
                m.temp = model.DataConsulta("select * from "+n);      
                return View(m);
            }
            else
            {
                return RedirectToAction("Login", "Login");

            }
        }

        public ActionResult Bandejademensajes(AdminModel m) {

            if (Session["name"] != null)
            {
                MySqlDataReader res = model.consulta("select count(*) from mensaje where estado='No Leido'");
                while (res.Read())
                {
                    Session["mensajes"] = res.GetInt32("count(*)");
                }
                m.temp = model.DataConsulta("select usuarionombre,asunto,idmensaje,descripcion,estado,usuario.idusuario,idrol from mensaje inner join usuario on mensaje.idusuario=usuario.idusuario");
                return View(m);
            }
            else
            {
                return RedirectToAction("Login", "Login");

            }
        }
        [HttpGet]
        public ActionResult responder(string usua, string id, string men, string email, string Rol, string idmensaje)
        {
            Session["mensajebandeja"] = men;
            Session["usuariobandeja"] = usua;
            MySqlDataReader res;
            
            if (Rol == "1") {
                res= model.consulta(" select email from tienda where idsuario=" +id);
            }
            else
            {
               res = model.consulta("select email from cliente where idusuario=" + id);
            }
            while (res.Read())
            {
                Session["emailbandeja"] = res.GetString("email");
            }
            res = model.consulta("update mensaje set estado=" +"'Leido'"+"WHERE idmensaje="+idmensaje);
 
            return View(); 
        }

        [HttpPost]
        public ActionResult responder(AdminModel m)
        {
            model.EnviarCorreo(Session["emailbandeja"].ToString(), Session["usuariobandeja"].ToString(), m.mensaje); 
           return  RedirectToAction("Bandejademensajes", ("Administrador"));

        }



        [HttpPost]
        public ActionResult Tabla(AdminModel m,string x, string h)
        {
            if (Session["name"] != null)
            {
                m.seleccion = x;
                model.Inserccion("insert into "+m.seleccion+"(nombre) values('"+m.nombre+"')");
                m.temp = model.DataConsulta("select * from " +m.seleccion);
                return RedirectToAction("PerfilAdmin","Administrador");
            }
            else
            {
                return RedirectToAction("Login", "Login");

            }
        }

        [HttpGet]
        public ActionResult Informaciongeneral() {
            MySqlDataReader r = model.consulta("select * from informacion");
            while (r.Read())
            {
                ViewBag.Nombre = r.GetString("nombresoft");
                ViewBag.Eslogan = r.GetString("eslogan");
            }
            return View();
         }

        [HttpPost]
        public ActionResult Informaciongeneral(AdminModel mod)
        {
            bool x = model.Inserccion("update informacion set nombresoft='"+mod.nombreprincipal+"', eslogan='"+mod.eslogan+"'");
            if (x)
            {
                return RedirectToAction("PerfilAdmin", "Administrador");
            }
            else
            {
                return View();
            }
        }

        /* 
        public void eliminar(AdminModel mod,int id,string nombre)
        {
            mod.eliminar(nombre,id);
            Response.Redirect("../PerfilAdmin/Administrador");
        }*/



    }
}