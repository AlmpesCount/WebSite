using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using EntidadesNegocio;

namespace CapaDatos
{
    public class daAutor
    {
        public bool InsertarAutor(beAutor autor)
        {
            bool res = false;
            try
            {
                SqlConnection conexion = Conexion.ConexionSql;
                SqlCommand cmd = conexion.CreateCommand();
                cmd.CommandText = "usp_InsertaAutor";
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter par1 = cmd.Parameters.Add("@pNombre", SqlDbType.VarChar, 50);
                par1.Direction = ParameterDirection.Input;
                par1.Value = autor.NombreAutor;
                SqlParameter par2 = cmd.Parameters.Add("@pPais", SqlDbType.VarChar, 50);
                par2.Direction = ParameterDirection.Input;
                par2.Value = autor.Pais;
                conexion.Open();
                //Si llego insertar correctamente:
                if (cmd.ExecuteNonQuery() > 0) //Asignamos a la variable el valor de filas afectadas en la BD
                    res = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return res;
        }

        public static List<beAutor> cargaAutor()
        {
            //Usamos el metodo ConexionSql de la clase Conexion para crear rapidamente la conexion
            SqlConnection conexion = Conexion.ConexionSql;

            //Creamos el objeto comando
            SqlCommand cmd = conexion.CreateCommand();
            cmd.CommandText = "usp_CargarAutor";
            cmd.CommandType = CommandType.StoredProcedure;

            //Abrimos la conexion:
            conexion.Open();

            //Creamos el reader y lo ejecutamos desde el comando
            SqlDataReader categoriasReader = cmd.ExecuteReader(/*CommandBehavior.SingleResult*/);

            //Creamos una lista de entidades del tipo Categoria
            List<beAutor> lbeAutores = new List<beAutor>();

            //Valida que el lector ha traido datos
            if (categoriasReader.HasRows == true)
            {
                //Recorremos el reader:
                while (categoriasReader.Read())
                {
                    //Creamos un objeto del tipo entidad Categoria 
                    beAutor objAutor = new beAutor();
                    objAutor.AutorId = categoriasReader.GetInt32(0);  //ubicando el valor por posición                    
                    objAutor.NombreAutor = categoriasReader["NombreAutor"].ToString();  //ubicando el valor por nombre columna.
                    objAutor.Pais = categoriasReader["PaisAutor"].ToString();
                    //Agregamos el objeto creado a nuestra lista de entidades
                    lbeAutores.Add(objAutor);
                }
            }
            categoriasReader.Close();  //cerramos el reader
            cmd.Dispose();   //Liberamos recursos del comando
            conexion.Close();  //cerramos la conexion
            return lbeAutores;   //retornamos la lista
        }

        public beAutor MostrarAutor(String valor)
        {
            beAutor Autor = null;
            int ID = Convert.ToInt32(valor);
            SqlConnection conexion = Conexion.ConexionSql;
            SqlCommand cmd = conexion.CreateCommand();
            cmd.CommandText = "usp_MostrarAutor";
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();
            cmd.Parameters.AddWithValue("@AutorId", ID);
            SqlDataReader categoriasReader = cmd.ExecuteReader();
            if (categoriasReader.Read())
            {
                Autor = new beAutor();
                Autor.NombreAutor = categoriasReader["NombreAutor"].ToString();
                Autor.Pais = categoriasReader["PaisAutor"].ToString();
            }
            return Autor;
        }

        public bool ActualizarAutor(beAutor Autor)
        {
            bool res = false;
            try
            {
                SqlConnection conexion = Conexion.ConexionSql;
                SqlCommand cmd = conexion.CreateCommand();
                cmd.CommandText = "usp_ActualizarAutor";
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.AddWithValue("@AutorId", Autor.AutorId);

                SqlParameter par1 = cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 50);
                par1.Direction = ParameterDirection.Input;
                par1.Value = Autor.NombreAutor;

                SqlParameter par2 = cmd.Parameters.Add("@Pais", SqlDbType.VarChar, 50);
                par2.Direction = ParameterDirection.Input;
                par2.Value = Autor.Pais;

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

        public void EliminarAutor(int Cat)
        {
            SqlConnection conexion = Conexion.ConexionSql;
            SqlCommand cmd = conexion.CreateCommand();
            cmd.CommandText = "usp_EliminarAutor";
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();
            cmd.Parameters.AddWithValue("@AutorId", Cat);
            cmd.ExecuteNonQuery();
            conexion.Close();
        }
    }
}
