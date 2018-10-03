using NTT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace NTT.Controllers
{
    public class cerrarsessionController : Controller
    {
        Modelo m = new Modelo(); 
        // GET: cerrarsession
        public void cerrarsession()
        {

            if (Session["rol"].ToString()=="2")
            {
                m.Inserccion("DELETE FROM detallepedido WHERE idpedido=" + Session["carro"].ToString());
                m.Inserccion("DELETE FROM pedido WHERE idpedido=" + Session["carro"].ToString());
            }
            FormsAuthentication.SignOut();
            Session.Abandon();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Redirect("../Login/Login");
 
        }
    }
}