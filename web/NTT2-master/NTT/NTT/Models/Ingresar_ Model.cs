using conexiondb;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace NTT
{

    public class Ingresar_Model
    {   [Required (ErrorMessage = " El usuario es obligario") ]
        public string user { get; set; }
        [Required(ErrorMessage = " La contraseña es obligaria")]
        public string password { get; set; } 
        public int idrol { get; set; }
        public int iduser { get; set; }
        public string nombre { get; set; }
        public int key { get; set; }
        private conexion conn = new conexion();
        private MySqlCommand Comman = new MySqlCommand();




        public bool ConsultarUsuario(string cadena)
        {
            Comman.CommandText = cadena;
            Comman.Connection = conn.ConexionMySql();
            MySqlDataReader consulta = Comman.ExecuteReader();
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

        public void Consulta(string cadena, string n) {
            Comman.CommandText =cadena;
            Comman.Connection = conn.ConexionMySql();
            MySqlDataReader consulta = Comman.ExecuteReader();
            try
            {
                while (consulta.Read())
                {
                    if (n=="tienda")
                    {
                        nombre = consulta.GetString("nombretienda");
                        key = consulta.GetInt32("idtienda");
                    }
                    else {
                        nombre = consulta.GetString("primernombre");
                        key = consulta.GetInt32("idcliente");
                    }
                    
                }
            }
            catch (Exception)
            { 

            }
            
        }
      



    }



}
