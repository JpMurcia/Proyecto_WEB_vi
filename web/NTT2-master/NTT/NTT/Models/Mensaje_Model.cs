using conexiondb;
using MySql.Data.MySqlClient;
using System;
using System.ComponentModel.DataAnnotations;

namespace NTT
{

    public class Mensaje_Model
    {
        [Required]
        public string descripcion { get; set; }
        public bool estado { get; set; }
        [Required] 
        public string asunto { get; set; }

        private conexion conn = new conexion();
        private MySqlCommand Comman = new MySqlCommand();

        public bool Registrar(string cadena)
        {
            try
            {
                Comman.CommandText = cadena;
                Comman.Connection = conn.ConexionMySql();
                Comman.ExecuteNonQuery();
                return true;
            }
            catch (Exception )
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