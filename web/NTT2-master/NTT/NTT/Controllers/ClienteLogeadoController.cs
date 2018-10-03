using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NTT.Models;
using MySql.Data.MySqlClient;
using NTT;
using System.Data;

namespace NewTailor123.Controllers
{
    public class ClienteLogeadoController : Controller
    {   
        Mensaje_Model modelo = new Mensaje_Model();
        Modelo mod = new Modelo();
        // GET: ClienteLogeado

        public ActionResult MisCompras(Modelo m) {
            if (Session["name"] != null)
            {
                m.temp = mod.DataConsulta("SELECT distinct PEDIDO.idpedido, PEDIDO.fechapedido, (select sum(cantidad) from detallepedido where detallepedido.idpedido=pedido.idpedido) as CantidadProductos, PEDIDO.valortotal  FROM detallepedido inner join PEDIDO on detallepedido.idpedido=pedido.idpedido INNER JOIN CLIENTE ON PEDIDO.IDCLIENTE=CLIENTE.IDCLIENTE where pedido.idcliente=" + Session["key"].ToString() + " and pedido.estado='Pendiente' or pedido.estado='Cumplido'");
                return View(m); 
            }
            else
            {
                return RedirectToAction("Login", "Login"); 
            } 
        }

        [HttpGet]
        public ActionResult MiCarrito(Modelo m) {
            if (Session["name"] != null)
            {
                MySqlDataReader res = mod.Consulta("Select count(*) from detallepedido where idpedido=" + Session["carro"]);
                while (res.Read())
                {
                    Session["carrito"] = res.GetInt32("count(*)"); 
                }
                m.temp = mod.DataConsulta("SELECT  prenda.idprenda,nombreprenda ,prenda.foto,nombretienda,precio, detallepedido.cantidad, prenda.cantidad as 'canti',(select nombrecolor from color where color.idcolor=tallacolor.idcolor) as Colorsito,(select nombretalla from talla where talla.idtalla=tallacolor.idtalla) as tallita, subtotal,valortotal FROM pedido inner join detallepedido on detallepedido.idpedido=pedido.idpedido INNER JOIN tallacolor on detallepedido.idtc=tallacolor.idtc inner join prenda on tallacolor.idprenda=prenda.idprenda INNER JOIN tienda on prenda.idtienda=tienda.idtienda WHERE detallepedido.idpedido=" + Session["carro"] );
                return View(m); 
            }
            else
            {
                return RedirectToAction("Login", "Login"); 
            }
        }

        [HttpPost]
        public  ActionResult  añadirprenda(Tienda_Model m ,int cantidad, string nombre, string descripcion, string precio, string cantidadt, string fot)
        {
            if (Session["name"] != null)
            {
                MySqlDataReader res = mod.Consulta("select idtc,estado,cantidad from tallacolor where idtalla=" +m.idtalla +" and idcolor=" +m.idcolor +" and idprenda=" + Session["idprenda"]);
                while (res.Read())
                {
                    if (res.GetString("estado") == "N")
                    {
                        ViewBag.Message = "No Disponible";
                    } else if (res.GetInt32("cantidad")<cantidad) {
                        ViewBag.Message = "Excede la cantidad de productos existentes";
                    }
                    else {
                        bool x = mod.ConsultaBool("select * from detallepedido where idpedido=" + Session["carro"].ToString() + " and idtc=" + res.GetInt32("idtc"));
                        if (x)
                        {
                            bool y = mod.Inserccion("update detallepedido set cantidad=cantidad+" + cantidad + " where idpedido=" + Session["carro"].ToString() + " and idtc=" + res.GetInt32("idtc"));
                        }
                        else {
                            bool y = mod.Inserccion("insert into detallepedido(idtc,idpedido,cantidad) values (" + res.GetInt32("idtc") + "," + Session["carro"].ToString() + "," + cantidad + ")");
                        }
                    }
                }
                res = mod.Consulta("select cantidad from prenda where idprenda=" + Session["idprenda"]);
                while (res.Read())
                {
                    cantidadt = res.GetInt32("cantidad").ToString();
                } 
                return RedirectToAction("productpage","TiendaLogeado", new { nombre = nombre, descripcion = descripcion , precio = precio, cantidad = cantidadt, fot = fot });
            }
            else {
                return RedirectToAction("Login", "Login");
            }            
        }
 
 
        [HttpGet]
        public ActionResult ActualizarInformacion()
        {    
            if (Session["name"] != null)
            { 
                MySqlDataReader res = mod.Consulta("select * from cliente where idusuario=" + Session["id"]);
                while (res.Read())
                {
                    ViewBag.nombre1 = res.GetString("primernombre");
                    ViewBag.nombre2 = res.GetString("segundonombre");
                    ViewBag.apellido1 = res.GetString("primerapellido");
                    ViewBag.apellido2 = res.GetString("segundoapellido");
                    ViewBag.telefono = res.GetString("telefono");
                    ViewBag.email = res.GetString("email");
                    ViewBag.id = res.GetString("idcliente"); 
                }
                ViewBag.user = Session["name"];
                return View(); 
            }
            else
            {
                return RedirectToAction("Login", "Login"); 
            }
        }
        
        [HttpPost]
        public ActionResult ActualizarInformacion(Registro_Model m)
        {
            bool x = mod.Inserccion("update cliente set primernombre='" + m.primernombre + "', segundonombre='" + m.segundonombre + "', primerapellido='" + m.primerapellido + "', segundoapellido= '" + m.segundoapellido + "', telefono=" + m.telefono+ ", email='" + m.email + "' WHERE idcliente=" + Session["key"]); 
            return RedirectToAction("ActualizarInformacion","ClienteLogeado"); 
        }
  
        public ActionResult eliminarprendacarrito(Tienda_Model m, string idprenda) {
          mod.Inserccion("delete from detallepedido where idprenda=" +idprenda +"and idpedido=" + Session["carro"]); 
          return  RedirectToAction("MiCarrito", "ClienteLogeado");
        }

        public ActionResult HacerCompra()
        {
            mod.Inserccion("update pedido set estado='Pendiente' where idpedido=" + Session["carro"]);
            mod.Inserccion("INSERT INTO pedido( fechapedido , idcliente, estado) VALUES(curdate()," + Session["key"] + ", 'Temporal')");
            MySqlDataReader rs = mod.Consulta("select idpedido from pedido where estado='Temporal' and idcliente=" + Session["key"]);
            while (rs.Read())
            {
                Session["carro"] = rs.GetInt32("idpedido");
            }
            return RedirectToAction("Index", "Inicio");
        }

        [HttpGet]
        public ActionResult informar()
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

        [HttpPost]
        public ActionResult informar(Mensaje_Model m)
        {
            string cadena = "";
            if (Session["name"] != null)
            {
                cadena = "insert into mensaje (descripcion,asunto,idusuario,estado ) values('" + m.descripcion + "','" + m.asunto + "', '" + Session["id"] + "', '"+"No Leido"+"' )";
                modelo.Registrar(cadena);
                return RedirectToAction("Index","Inicio"); 
            }
            else
            {
                return RedirectToAction("Login", "Login"); 
            }

        }

    }
}