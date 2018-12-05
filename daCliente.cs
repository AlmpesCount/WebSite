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
    public class daCliente
    {
        public DataTable cargaCliente()
        {
            SqlConnection conexion = Conexion.ConexionSql;
            SqlCommand cmd = conexion.CreateCommand();
            cmd.CommandText = "usp_CargaClientesTodos";
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();

            SqlDataReader clienteReader = cmd.ExecuteReader();

            DataTable dClientes = new DataTable();
            using (clienteReader)
            {
                dClientes.Load(clienteReader);
            }
            return dClientes;
        }

        public bool insertaCliente(beCliente cliente)
        {
            bool res = false;
            try
            {
                SqlConnection conexion = Conexion.ConexionSql;
                SqlCommand cmd = conexion.CreateCommand();
                cmd.CommandText = "usp_InsertaCliente";
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 50);
                par1.Direction = ParameterDirection.Input;
                par1.Value = cliente.NombreCliente;

                SqlParameter par2 = cmd.Parameters.Add("@DNI", SqlDbType.VarChar, 50);
                par2.Direction = ParameterDirection.Input;
                par2.Value = cliente.DNI;

                SqlParameter par3 = cmd.Parameters.Add("@Telef", SqlDbType.VarChar, 50);
                par3.Direction = ParameterDirection.Input;
                par3.Value = cliente.Telefono;

                SqlParameter par4 = cmd.Parameters.Add("@Correo", SqlDbType.VarChar, 50);
                par4.Direction = ParameterDirection.Input;
                par4.Value = cliente.Correo;

                SqlParameter par5 = cmd.Parameters.Add("@LibroId", SqlDbType.Int);
                par5.Direction = ParameterDirection.Input;
                par5.Value = cliente.LibroId;

                SqlParameter par6 = cmd.Parameters.Add("@FecEntr", SqlDbType.VarChar, 50);
                par6.Direction = ParameterDirection.Input;
                par6.Value = cliente.FecEntrega;

                SqlParameter par7 = cmd.Parameters.Add("@FecDev", SqlDbType.VarChar, 50);
                par7.Direction = ParameterDirection.Input;
                par7.Value = cliente.FecDevolucion;

                conexion.Open();
                if (cmd.ExecuteNonQuery() > 0) //Asignamos a la variable el valor de filas afectadas en la BD
                    res = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }

        public void EliminarCliente(int Cat)
        {
            SqlConnection conexion = Conexion.ConexionSql;
            SqlCommand cmd = conexion.CreateCommand();
            cmd.CommandText = "usp_EliminarCliente";
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();
            cmd.Parameters.AddWithValue("@ClienteId", Cat);
            cmd.ExecuteNonQuery();
            conexion.Close();
        }

        public beCliente MostrarClientes(String valor)
        {
            beCliente cliente = null;
            int ID = Convert.ToInt32(valor);
            SqlConnection conexion = Conexion.ConexionSql;
            SqlCommand cmd = conexion.CreateCommand();
            cmd.CommandText = "usp_MostrarCliente";
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();
            cmd.Parameters.AddWithValue("@ClienteID", ID);
            SqlDataReader categoriasReader = cmd.ExecuteReader();
            if (categoriasReader.Read())
            {
                cliente = new beCliente();
                cliente.NombreCliente = categoriasReader["NombreCliente"].ToString();
                cliente.DNI = categoriasReader["DNI"].ToString();
                cliente.Telefono = categoriasReader["Telefono"].ToString();
                cliente.Correo = categoriasReader["Correo"].ToString();
                cliente.LibroId = categoriasReader["LibroId"].ToString();
                cliente.FecEntrega = categoriasReader["FecEntrega"].ToString();
                cliente.FecDevolucion = categoriasReader["FecDevolucion"].ToString();
            }
            return cliente;   //retornamos la lista
        }

        public bool ActualizarCliente(beCliente cliente)
        {
            bool res = false;
            try
            {
                SqlConnection conexion = Conexion.ConexionSql;
                SqlCommand cmd = conexion.CreateCommand();
                cmd.CommandText = "usp_ActualizarCliente";
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.AddWithValue("@ClienteId", cliente.ClienteId);
                SqlParameter par1 = cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 50);
                par1.Direction = ParameterDirection.Input;
                par1.Value = cliente.NombreCliente;

                SqlParameter par2 = cmd.Parameters.Add("@DNI", SqlDbType.VarChar, 50);
                par2.Direction = ParameterDirection.Input;
                par2.Value = cliente.DNI;

                SqlParameter par3 = cmd.Parameters.Add("@Telef", SqlDbType.VarChar, 50);
                par3.Direction = ParameterDirection.Input;
                par3.Value = cliente.Telefono;

                SqlParameter par4 = cmd.Parameters.Add("@Correo", SqlDbType.VarChar, 50);
                par4.Direction = ParameterDirection.Input;
                par4.Value = cliente.Correo;

                SqlParameter par5 = cmd.Parameters.Add("@LibroId", SqlDbType.Int);
                par5.Direction = ParameterDirection.Input;
                par5.Value = cliente.LibroId;

                SqlParameter par6 = cmd.Parameters.Add("@FecEntr", SqlDbType.VarChar, 50);
                par6.Direction = ParameterDirection.Input;
                par6.Value = cliente.FecEntrega;

                SqlParameter par7 = cmd.Parameters.Add("@FecDev", SqlDbType.VarChar, 50);
                par7.Direction = ParameterDirection.Input;
                par7.Value = cliente.FecDevolucion;

                if (cmd.ExecuteNonQuery() > 0)
                    conexion.Close();
                res = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return res;
        }
    }
}
