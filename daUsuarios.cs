using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using EntidadesNegocio;

namespace CapaDatos
{
    public class daUsuarios
    {
        public bool insertaUsuario(beUsuario usuario)
        {
            bool res = false;

            SqlConnection conexion = Conexion.ConexionSql;

            SqlCommand cmd = conexion.CreateCommand();
            cmd.CommandText = "usp_InsertaUsuario";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter par1 = cmd.Parameters.Add("@pNombres", SqlDbType.VarChar, 50);
            par1.Direction = ParameterDirection.Input;
            par1.Value = usuario.Nombres;

            SqlParameter par2 = cmd.Parameters.Add("@pUsuario", SqlDbType.VarChar, 15);
            par2.Direction = ParameterDirection.Input;
            par2.Value = usuario.Usuario;

            SqlParameter par3 = cmd.Parameters.Add("@pClave", SqlDbType.VarChar, 15);
            par3.Direction = ParameterDirection.Input;
            par3.Value = usuario.Clave;

            SqlParameter par4 = cmd.Parameters.Add("@pEmail", SqlDbType.VarChar, 50);
            par4.Direction = ParameterDirection.Input;
            par4.Value = usuario.Email;

            SqlParameter par5 = cmd.Parameters.Add("@pSexo", SqlDbType.Char);
            par5.Direction = ParameterDirection.Input;
            par5.Value = usuario.Sexo;

            conexion.Open();

            //Asignamos a la variable el valor de filas afectadas en la BD
            int resultado = cmd.ExecuteNonQuery();

            //Si llego insertar correctamente:
            if (resultado > 0)
                res = true;

            return res;
        }
    }
}
