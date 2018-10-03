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

    public class Registro_Model
    {
        /*Datos de cliente*/
        [Required]
        public string primernombre { get; set; } 
        public string segundonombre { get; set; }
        [Required]
        public string primerapellido { get; set; } 
        public string segundoapellido { get; set; }
        [Required] 
        public string idcliente { get; set; } 
        [Required]
        public string email { get; set; } 
        public string telefono { get; set; }
        [Required]
        public string userc { get; set; }
        [Required]
        public string passwordc { get; set; } 
        /*Datos de tienda*/
        [Required]
        public string nombretienda { get; set; } 
        [Required]
        public string nit { get; set; }
        [Required]
        public string emailtienda { get; set; }
        public string  telefonotienda { get; set; } 
        /*Datos de usuario*/
        [Required]
        public string user { get; set; } 
        [Required]
        public string password { get; set; }  
        public Int32 iduser{ get; set; }
         
        private conexion conn = new conexion();
        private MySqlCommand Comman = new MySqlCommand();

        public bool RegistrarUser(string cadena, string user, string pass)
        {
            try
            {
                Comman.CommandText = cadena;
                Comman.Connection = conn.ConexionMySql();
                Comman.ExecuteNonQuery();
                MySqlDataReader con = Consulta("select idusuario from usuario where usuarionombre='" + user + "' and contraseña=MD5('" + pass + "')");
                try
                {
                    while (con.Read())
                    {
                        iduser = con.GetInt32("idusuario");
                    }
                }
                catch (Exception)
                {
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally {
                conn.Cerrar(Comman.Connection);
            }
        }

        public MySqlDataReader Consulta(string cadena) {
            Comman.CommandText = cadena;
            Comman.Connection = conn.ConexionMySql();
            MySqlDataReader consulta = Comman.ExecuteReader();
            return consulta;         
        }

        public bool Registrar(string  cadena )
        {
            try
            {
                Comman.CommandText = cadena;
                Comman.Connection = conn.ConexionMySql();
                Comman.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            finally {
                conn.Cerrar(Comman.Connection);
            }
        }
         
 
    }



}
