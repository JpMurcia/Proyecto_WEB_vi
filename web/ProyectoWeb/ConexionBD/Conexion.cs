using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace ConexionBD
{
    public class Conexion
    {
        public class conexion
        {
            public MySqlConnection ConexionMySql()
            {
                MySqlConnection Connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["conexion_mysql"].ConnectionString);
                try
                {
                    Connection.Open();
                }
                catch (Exception e)
                {
                    throw new Exception("No se puedo realizar la conexion " + e.Message);
                }
                return Connection;
            }

            public void Cerrar(MySqlConnection Connection)
            {
                Connection.Close();
            }

        }
    }
}
