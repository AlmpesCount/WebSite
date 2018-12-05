using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class daSeguridad
    {
        public string ValidaUsuario(string usuario, string password)
        {
            //bool esValido = false;
            string resultado = "";

            try
            {
                SqlConnection conexion = Conexion.ConexionSql;
                SqlCommand cmd = new SqlCommand("usp_ValidaUsuario", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = cmd.Parameters.Add("@usuario", SqlDbType.VarChar, 15);
                par1.Direction = ParameterDirection.Input;
                par1.Value = usuario;

                SqlParameter par2 = cmd.Parameters.Add("@password", SqlDbType.VarChar, 15);
                par2.Direction = ParameterDirection.Input;
                par2.Value = password;

                conexion.Open();

                //Creamos una variable para cargar el resultado de nuestro Procedure
                var resul = cmd.ExecuteScalar();

                if (resul != null)  //Asegurandonos que está trayendo un resultado.
                {
                    //if (resul.ToString() != "0")  //Verificamos el valor devuelto.
                    resultado = resul.ToString();   //Sì es un usuario valido.
                }
                cmd.Dispose();
            }

            catch (Exception ex)
            {
                return ex.Message;
            }

            return resultado;
        }
    }
}
