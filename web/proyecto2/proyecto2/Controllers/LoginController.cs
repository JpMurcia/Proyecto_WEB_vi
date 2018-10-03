using proyecto2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proyecto2.Controllers
{
    public class LoginController : Controller
    {

        ModeloLogin modelo;
        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(int x) {
            return View();
        }

    }
}