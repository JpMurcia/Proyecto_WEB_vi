using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace SpiderPeticion.Models.Conexion
{
    public class Conexion
    {
        public string schema = "bdmedico";
        public string Connection()
        {
            return ConfigurationManager.ConnectionStrings["giecom"].ConnectionString;
        }
        public DataTable ExecuteProcedure(String procedure,List<Parametro> parametros)
        {
            MySqlConnection conn = new MySqlConnection(this.Connection());
            conn.Open();
            MySqlCommand comando = new MySqlCommand(procedure, conn);
            comando.CommandType = CommandType.StoredProcedure;
            DataTable datos = new DataTable();
            if (parametros!=null)
            {
                foreach (Parametro p in parametros)
                {
                    comando.Parameters.Add(new MySqlParameter(p.nombre, p.tipo));
                    comando.Parameters[p.nombre].Direction = p.direccion;
                    if (p.direccion == ParameterDirection.Input)
                    {
                        comando.Parameters[p.nombre].Value = p.valor;
                    }
                }
            }
            try
            {
                datos.Load(comando.ExecuteReader());
            }
            catch (Exception ex)
            {
                datos = this.ManejadorDeError(ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }

            return datos;
        }
        public DataTable ManejadorDeError(string ef)
        {
            DataTable datos = new DataTable();
            datos.Columns.Add("TIPO");
            datos.Columns.Add("MENSAJE");
            datos.Columns.Add("VALORES");
            //dt.Rows.Add("Ha ocurrido un error, por favor verifique e intente nuevamente", "D");
            datos.Rows.Add("D", ef, "");
            return datos;
        }
    }
}