using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoWeb.Models
{
    public class grupo_invest
    {
        public string infor_contacto_grupo_email;
        public string infor_contacto_grupo_tele;
        public string infor_contacto_grupo_direcc;
        public string infor_contacto_grupo_color;


        public grupo_invest() {

        }

        public grupo_invest(string infor_contacto_grupo_email, string infor_contacto_grupo_tele, 
                string infor_contacto_grupo_direcc, string infor_contacto_grupo_color) {
            this.infor_contacto_grupo_email = infor_contacto_grupo_email;
            this.infor_contacto_grupo_tele = infor_contacto_grupo_tele;
            this.infor_contacto_grupo_direcc = infor_contacto_grupo_direcc;
            this.infor_contacto_grupo_color = infor_contacto_grupo_color;




        }




        

        
    }
}