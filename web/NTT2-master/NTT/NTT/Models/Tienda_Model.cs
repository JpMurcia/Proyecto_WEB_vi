using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using conexiondb;
using MySql.Data.MySqlClient;

namespace NTT.Models
{

    public class Tienda_Model
    {
        public string nombreem { get; set; }
        public string telefonoem { get; set; }
        public string nitemp { get; set; }
        public string emailem { get; set; }
        public string usuario { get; set; }
        public string contraseña { get; set; }
        public string foto { get; set; }
        public string texto{ get; set; }
        private conexion conn = new conexion();
        private MySqlCommand Comman = new MySqlCommand();
        public DataSet temp { get; set; }
        public int idtalla { get; set; }
        public int idcolor { get; set; }

        public void eliminar(int id)
        {
            Comman.CommandText = "update prenda set estado='I' WHERE idprenda=" + id;
            Comman.Connection = conn.ConexionMySql();
            Comman.ExecuteNonQuery();
            conn.Cerrar(Comman.Connection);
        }
 

    }
}