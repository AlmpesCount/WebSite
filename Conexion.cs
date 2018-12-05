using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class Conexion
    {
        //Funcion que devuelve la cadena de conexion
        public static string CadenaConexion
        {
            get
            {
                string cadenaConexion = null;
                cadenaConexion = ConfigurationManager.ConnectionStrings["conexionBiblioteca"].ToString();
                return cadenaConexion;
            }
        }

        //Funcion que devuelve la clase conexión de SQL
        public static SqlConnection ConexionSql
        {
            get
            {
                SqlConnection conexion = null;
                try
                {
                    string cadena = ConfigurationManager.ConnectionStrings["conexionBiblioteca"].ToString();
                    conexion = new SqlConnection(cadena);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return conexion;
            }
        }
    }
}
