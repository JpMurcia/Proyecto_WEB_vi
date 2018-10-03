using conexiondb;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NTT.Models
{
    public class Modelo
    {
        private conexion conn = new conexion();
        private MySqlCommand Comman = new MySqlCommand();
        public string texto { get; set; } 
        public DataSet temp { get; set; }

        public bool ConsultaBool(string cadena)
        {
            Comman.CommandText = cadena;
            Comman.Connection = conn.ConexionMySql();
            MySqlDataReader consulta = Comman.ExecuteReader(); 
            return (consulta.HasRows) ? true : false;
        }

        public MySqlDataReader Consulta(string cadena)
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
    }
}