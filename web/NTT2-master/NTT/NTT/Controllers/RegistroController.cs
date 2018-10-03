using MySql.Data.MySqlClient;
using NTT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace NTT.Controllers
{
    public class RegistroController : Controller
    {

        Registro_Model modelo  = new Registro_Model();  

        [HttpGet]
        public ActionResult Registro()
        {
            MySqlDataReader r = modelo.Consulta("select * from informacion");
            while (r.Read())
            {
                ViewBag.NombrePrincipal = r.GetString("nombresoft");
                ViewBag.Eslogan = r.GetString("eslogan");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Registro(Registro_Model mod)
        {
            string cadena = "";
            string cadena2 = "";
            if (mod.userc==null)
            {
                MySqlDataReader res =modelo.Consulta("select usuarionombre from usuario");
                while (res.Read())
                {
                    if (res.GetString("usuarionombre").Equals(mod.user))
                    {
                        ViewBag.mensaje = "Usuario ya existente no se pudo completar el registro";
                        return View();
                    }
                }
                EnviarCorreo(mod.emailtienda, mod.nombretienda, "tienda");
                cadena2 = "insert into usuario(fecharegistro,usuarionombre,contraseña,idrol) values(curdate(),'" +mod.user+ "',MD5('" +mod.password+ "'), " +1+ ")";
                modelo.RegistrarUser(cadena2,mod.user, mod.password); 
                cadena = "insert into tienda(idtienda,nombretienda,telefono,email,idusuario) values(" + mod.nit + ",'" + mod.nombretienda + "', '" + mod.telefonotienda + "', '" + mod.emailtienda + "',"+modelo.iduser+")"; 
            }
            else  
            {
                MySqlDataReader res = modelo.Consulta("select usuarionombre from usuario");
                while (res.Read())
                {
                    if (res.GetString("usuarionombre").Equals(mod.userc))
                    {
                        ViewBag.mensaje = "Usuario ya existente no se pudo completar el registro";
                        return View();
                    }
                }
                EnviarCorreo(mod.email, mod.primernombre + " " + mod.segundonombre, "cliente");
                cadena2 = "insert into usuario(fecharegistro,usuarionombre,contraseña,idrol) values(curdate(),'" + mod.userc + "',MD5('" + mod.passwordc + "'), " +2+ ")";
                modelo.RegistrarUser(cadena2,mod.userc, mod.passwordc);
                cadena = "insert into cliente(idcliente,primernombre, segundonombre, primerapellido, segundoapellido, telefono,email, idusuario) values(" + mod.idcliente + ", '" + mod.primernombre + "','" + mod.segundonombre + "','" + mod.primerapellido + "','" + mod.segundoapellido + "','" + mod.telefono + "','" + mod.email + "'," + modelo.iduser + ")"; 
            }
            modelo.Registrar(cadena);
            return RedirectToAction("Index", "Inicio");
        }

 
        [HttpPost]
        public ActionResult RegistrarAdmin(Registro_Model mod)
        {
            string cadena = "";
            string cadena2 = "";
            if (mod.userc == null)
            {
                MySqlDataReader res = modelo.Consulta("select usuarionombre from usuario");
                while (res.Read())
                {
                    if (res.GetString("usuarionombre").Equals(mod.user))
                    {
                        ViewBag.mensaje = "Usuario ya existente no se pudo completar el registro";
                        return View();
                    }
                }
                EnviarCorreo(mod.emailtienda, mod.nombretienda,"tienda");
                cadena2 = "insert into usuario(fecharegistro,usuarionombre,contraseña,idrol) values(curdate(),'" + mod.user+ "',MD5('" + mod.password + "'), " + 1 + ")";
                modelo.RegistrarUser(cadena2,mod.user, mod.password);
                cadena = "insert into tienda(idtienda,nombretienda,telefono,email,idusuario) values(" + mod.nit + ",'" + mod.nombretienda + "', '" + mod.telefonotienda + "', '" + mod.emailtienda + "'," + modelo.iduser + ")";
            }
            else
            {
                MySqlDataReader res = modelo.Consulta("select usuarionombre from usuario");
                while (res.Read())
                {
                    if (res.GetString("usuarionombre").Equals(mod.userc))
                    {
                        ViewBag.mensaje = "Usuario ya existente no se pudo completar el registro";
                        return View();
                    }
                }
                EnviarCorreo(mod.email, mod.primernombre+" "+mod.segundonombre, "cliente");
                cadena2 = "insert into usuario(fecharegistro,usuarionombre,contraseña,idrol) values(curdate(),'" + mod.userc + "',MD5('" + mod.passwordc + "'), " + 2 + ")";
                modelo.RegistrarUser(cadena2,mod.userc, mod.passwordc );
                cadena = "insert into cliente(idcliente,primernombre, segundonombre, primerapellido, segundoapellido, telefono,email, idusuario) values(" + mod.idcliente + ", '" + mod.primernombre + "','" + mod.segundonombre + "','" + mod.primerapellido + "','" + mod.segundoapellido + "','" + mod.telefono + "','" + mod.email + "'," + modelo.iduser + ")";
            }
            modelo.Registrar(cadena);
            return RedirectToAction("PerfilAdmin","Administrador");
        }

        public void EnviarCorreo(string destino, string nombre, string rol) {
            var mensaje = new MailMessage();
            mensaje.Subject = "[NEW TAILOR] !Bienvenido!";
            mensaje.Body = "Hola " + nombre + " gracias por crear una cuenta como "+rol+" en NEWTAILOR.<br>";
            mensaje.To.Add(destino);
            mensaje.IsBodyHtml = true;
            var smtp = new SmtpClient();
            smtp.Send(mensaje);
        }

    }
}