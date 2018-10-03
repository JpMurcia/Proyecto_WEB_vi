using conexiondb;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace NTT.Models
{ 
    public class AdminModel
    {
        private conexion conn = new conexion();
        private MySqlCommand Comman = new MySqlCommand();
        public DataSet temp { get; set; } 
        public string seleccion { get; set; }
        public string nombre { get; set; }
        public string nombreprincipal { get; set; }
        public string eslogan { get; set; }
        public string mensaje { get; set; }
        public string correor { get; set; }

        public DataSet DataConsulta(string cadena)
        {
            DataSet ds = new DataSet();
            Comman.CommandText = cadena;
            Comman.Connection = conn.ConexionMySql();
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter(Comman); 
                da.Fill(ds);  
            }  
             catch (Exception e)
             {
                throw e;
            }
            conn.Cerrar(Comman.Connection);
            return ds;
        }

  
        public DataSet consultarcarro(string ke)
        {
            DataSet ds = new DataSet();
            Comman.CommandText = "SELECT prenda.foto, precio, prenda.nombre as 'per',id, EMPRESA.nombre, carro.cantidad, prenda.cantidad as 'canti', subtotal FROM CARRO INNER JOIN PRENDA on carro.id_prenda=prenda.idprenda INNER JOIN EMPRESA on prenda.idempresa=empresa.idempresa WHERE ID_CLIENTE=" + ke;
            Comman.Connection = conn.ConexionMySql();
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter(Comman);
                da.Fill(ds);
                
            }
            catch (Exception e)
            {
                throw e;
            }
            conn.Cerrar(Comman.Connection);
            return ds;
        }

        public MySqlDataReader consulta(string cadena)
        {
            Comman.CommandText = cadena;
            Comman.Connection = conn.ConexionMySql();
            MySqlDataReader consulta = Comman.ExecuteReader(); 
            return consulta;
        }

        public bool Inserccion(string cadena)
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
            finally
            {
                conn.Cerrar(Comman.Connection);
            }
        }

        public void EnviarCorreo(string destino, string nombre, string mensaje1)
        {
            var mensaje = new MailMessage();
            mensaje.Subject = "[NEW TAILOR]-RESPUESTA";
            mensaje.Body = "Hola " + nombre + "  " +mensaje1;
            mensaje.To.Add(destino);
            mensaje.IsBodyHtml = true;
            var smtp = new SmtpClient();
            smtp.Send(mensaje);
        }
        public void eliminar(string nombre,int id)
        {
            Comman.CommandText = " DELETE FROM "+ this.nombre+" WHERE id="+id;
            Comman.Connection = conn.ConexionMySql(); 
            Comman.ExecuteNonQuery();
            conn.Cerrar(Comman.Connection);
        }
    }
}