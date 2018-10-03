using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace pruebaV4.Models
{
    public class login_modelo
    {
        [Required(ErrorMessage = "El usuario obligatorio")]
        public string usuario { get; set; }
        [Required(ErrorMessage = "La contraseña es obligatorio")]
        public string password { get; set; }
        public int idrol { get; set; }
        public int iduser { get; set; }
        private Conexion conn2 = new Conexion();
        private MySqlCommand comman1 = new MySqlCommand();


        public bool ConsultarUsuario(string cadena)
        {
            comman1.CommandText = cadena;
            comman1.Connection = conn2.ConexionMySql();
            MySqlDataReader consulta = conn2.ExecuteReader();
            try
            {
                while (consulta.Read())
                {
                    idrol = consulta.GetInt16("idrol");
                    iduser = consulta.GetInt32("idusuario");
                }

            }
            catch (Exception)
            {

            }
            return (consulta.HasRows) ? true : false;
        }






    }
   
}