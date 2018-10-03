using MySql.Data.MySqlClient;
using NTT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTT.Controllers
{
    public class LoginController : Controller
    {
        Ingresar_Model modelo = new Ingresar_Model();
        Modelo model = new Modelo();
 
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            MySqlDataReader r = model.Consulta("select * from informacion");
            while (r.Read())
            {
                ViewBag.NombrePrincipal = r.GetString("nombresoft");
                ViewBag.Eslogan = r.GetString("eslogan");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
         public ActionResult Login(Ingresar_Model mod)
        {
            bool verificar = modelo.ConsultarUsuario("select idusuario ,idrol from usuario where usuarionombre='" +mod.user+ "' and contraseña=MD5('" +mod.password+ "')");
            if (verificar)
            {
                Session["rol"] = modelo.idrol; 
                if (modelo.idrol==1 )
                {
                    modelo.Consulta("select  nombretienda , idtienda from tienda where idusuario=" +modelo.iduser,"tienda");
                    Session["ver"] = modelo.nombre;
                    Session["name"] = mod.user;
                    Session["id"] = modelo.iduser;
                    Session["key"] = modelo.key;
                    return RedirectToAction("MiTienda","TiendaLogeado");
                   
                }
                else if (modelo.idrol==2)
                {
                    modelo.Consulta("select  primernombre, idcliente from cliente where idusuario=" + modelo.iduser, "cliente");
                    Session["ver"] = modelo.nombre;
                    Session["name"] = mod.user;
                    Session["id"] = modelo.iduser;
                    Session["key"] = modelo.key;
                    model.Inserccion("INSERT INTO pedido( fechapedido , idcliente, estado) VALUES(curdate()," + Session["key"].ToString() + ", 'Temporal')");
                    MySqlDataReader rs = model.Consulta("select idpedido from pedido where estado='Temporal' and idcliente=" + Session["key"]);
                    while (rs.Read())
                    {
                        Session["carro"] = rs.GetInt32("idpedido");
                    }
                    return RedirectToAction("Index", "Inicio");
                }
                else if (modelo.idrol==3)
                {
                    Session["name"] = mod.user;
                    Session["id"] = modelo.iduser;
                    return RedirectToAction("PerfilAdmin", "Administrador");
                   
                }
                else
                {
                    return View();
                }
            }
            else
            {
                 ViewBag.Message = "Usuario y/o Contraseña incorrectas";
                return View();
            }
 
        }
 
    }

}