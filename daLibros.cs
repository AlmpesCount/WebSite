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
    public class daLibros
    {
        public DataTable cargaLibros()
        {
            SqlConnection conexion = Conexion.ConexionSql;
            SqlCommand cmd = conexion.CreateCommand();
            cmd.CommandText = "usp_CargaLibrosTodos";
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();

            SqlDataReader librosReader = cmd.ExecuteReader();

            DataTable dtLibros = new DataTable();
            using (librosReader)
            {
                dtLibros.Load(librosReader);
            }
            return dtLibros;
        }

        public bool insertaLibro(beLibro libro)
        {
            bool res = false;
            try
            {
                SqlConnection conexion = Conexion.ConexionSql;
                SqlCommand cmd = conexion.CreateCommand();
                cmd.CommandText = "usp_InsertaLibro";
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = cmd.Parameters.Add("@pNombre", SqlDbType.VarChar, 50);
                par1.Direction = ParameterDirection.Input;
                par1.Value = libro.NombreLibro;

                SqlParameter par2 = cmd.Parameters.Add("@pCategoria", SqlDbType.Int);
                par2.Direction = ParameterDirection.Input;
                par2.Value = libro.CategoriaId;

                SqlParameter par3 = cmd.Parameters.Add("@pAutor", SqlDbType.Int);
                par3.Direction = ParameterDirection.Input;
                par3.Value = libro.AutorId;

                SqlParameter par4 = cmd.Parameters.Add("@pAnio", SqlDbType.Int);
                par4.Direction = ParameterDirection.Input;
                par4.Value = libro.Anio;
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

        public void EliminarLibro(int Cat)
        {
            SqlConnection conexion = Conexion.ConexionSql;
            SqlCommand cmd = conexion.CreateCommand();
            cmd.CommandText = "usp_EliminarLibro";
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();
            cmd.Parameters.AddWithValue("@LibroId", Cat);
            cmd.ExecuteNonQuery();
            conexion.Close();
        }

        public beLibro MostrarLibros(String valor)
        {
            beLibro Libros = null;
            int ID = Convert.ToInt32(valor);
            SqlConnection conexion = Conexion.ConexionSql;
            SqlCommand cmd = conexion.CreateCommand();
            cmd.CommandText = "usp_MostrarLibro";
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();
            cmd.Parameters.AddWithValue("@LibroID", ID);
            SqlDataReader categoriasReader = cmd.ExecuteReader();
            if (categoriasReader.Read())
            {
                Libros = new beLibro();
                Libros.NombreLibro = categoriasReader["NombreLibro"].ToString();
                Libros.CategoriaId = categoriasReader["CategoriaId"].ToString();
                Libros.AutorId = categoriasReader["AutorId"].ToString();
                Libros.Anio = categoriasReader["Anio"].ToString();
            }
            return Libros;   //retornamos la lista
        }

        public bool ActualizarLibro(beLibro libro)
        {
            bool res = false;
            try
            {
                SqlConnection conexion = Conexion.ConexionSql;
                SqlCommand cmd = conexion.CreateCommand();
                cmd.CommandText = "usp_ActualizarLibro";
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.AddWithValue("@pLibroId", libro.LibroId);

                SqlParameter par1 = cmd.Parameters.Add("@pNombre", SqlDbType.VarChar, 50);
                par1.Direction = ParameterDirection.Input;
                par1.Value = libro.NombreLibro;

                SqlParameter par2 = cmd.Parameters.Add("@pCategoria", SqlDbType.Int);
                par2.Direction = ParameterDirection.Input;
                par2.Value = libro.CategoriaId;

                SqlParameter par3 = cmd.Parameters.Add("@pAutor", SqlDbType.Int);
                par3.Direction = ParameterDirection.Input;
                par3.Value = libro.AutorId;

                SqlParameter par4 = cmd.Parameters.Add("@pAnio", SqlDbType.Int);
                par4.Direction = ParameterDirection.Input;
                par4.Value = libro.Anio;
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
