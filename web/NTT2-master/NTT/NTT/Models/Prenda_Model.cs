using conexiondb;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace NTT.Models
{
    public class Prenda_Model
    {
        private conexion conn = new conexion();
        private MySqlCommand Comman = new MySqlCommand();
        public int idprenda { get; set; }
        [Required]
        public string nombreprenda { get; set; }
        [Required]
        public float precio { get; set; }
        [Required]
        public string[] talla { get; set; }
        [Required]
        public string[] color { get; set; }
        [Required]
        public string genero { get; set; } 
        public string descripcion { get; set; }  
        public int idempresa { get; set; }
        public string foto { get; set; }
         public DataSet temp { get; set; }
        public DataSet temp2 { get; set; }
        public int[] cantidad { get; set; }

        public bool Registrar(string cadena)
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

        public int consultaidemp(int iduser2)
        {
            int x = 0;
            Comman.CommandText = "select idtienda from tienda where idusuario=" + iduser2;
            Comman.Connection = conn.ConexionMySql();
            MySqlDataReader consulta = Comman.ExecuteReader();
            while (consulta.Read())
            {
                x= consulta.GetInt32("idtienda");
            }
            return x;
        }

        public MySqlDataReader consulta(string cadena)
        {
            Comman.CommandText = cadena;
            Comman.Connection = conn.ConexionMySql();
             MySqlDataReader consulta = Comman.ExecuteReader();
            return consulta;
        }

        public DataSet DataConsulta(string cadena)
        {
            DataSet ds = new DataSet();
            Comman.CommandText = cadena;
            Comman.Connection = conn.ConexionMySql();
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter(Comman);
                da.Fill(ds);
                conn.Cerrar(Comman.Connection);
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public DataSet consultaprenda()
        {
            DataSet ds = new DataSet();
            Comman.CommandText = "select idprenda, nombreprenda, precio,genero, descripcion,cantidad, idtienda,foto from prenda limit 4";
            Comman.Connection = conn.ConexionMySql();
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter(Comman);
                da.Fill(ds);
                conn.Cerrar(Comman.Connection);
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        } 
    }
}