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
    public class daCategorias
    {
        public bool InsertarCategoria(beCategoria categ)
        {
            bool res = false;
            try
            {
                SqlConnection conexion = Conexion.ConexionSql;
                SqlCommand cmd = conexion.CreateCommand();
                cmd.CommandText = "usp_InsertaCategoria";
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter par1 = cmd.Parameters.Add("@pNombre", SqlDbType.VarChar, 50);
                par1.Direction = ParameterDirection.Input;
                par1.Value = categ.NombreCategoria;
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

        public static List<beCategoria> cargaCategorias()
        {
            //Usamos el metodo ConexionSql de la clase Conexion para crear rapidamente la conexion
            SqlConnection conexion = Conexion.ConexionSql;

            //Creamos el objeto comando
            SqlCommand cmd = conexion.CreateCommand();
            cmd.CommandText = "usp_CargaCategoriasTodas";
            cmd.CommandType = CommandType.StoredProcedure;

            //Abrimos la conexion:
            conexion.Open();

            //Creamos el reader y lo ejecutamos desde el comando
            SqlDataReader categoriasReader = cmd.ExecuteReader(/*CommandBehavior.SingleResult*/);
            
            //Creamos una lista de entidades del tipo Categoria
            List<beCategoria>  lbeCategorias = new List<beCategoria>();

            //Valida que el lector ha traido datos
            if (categoriasReader.HasRows == true)
            {
                //Recorremos el reader:
                while (categoriasReader.Read())
                {
                    //Creamos un objeto del tipo entidad Categoria 
                    beCategoria obeCategorias = new beCategoria();
                    obeCategorias.CategoriaId = categoriasReader.GetInt32(0);  //ubicando el valor por posición                    
                    obeCategorias.NombreCategoria = categoriasReader["NombreCategoria"].ToString();  //ubicando el valor por nombre columna.

                    //Agregamos el objeto creado a nuestra lista de entidades
                    lbeCategorias.Add(obeCategorias);
                }
            }
            categoriasReader.Close();  //cerramos el reader
            cmd.Dispose();   //Liberamos recursos del comando
            conexion.Close();  //cerramos la conexion
            return lbeCategorias;   //retornamos la lista
        }

        public beCategoria MostrarCategoria(String valor)
        {
            beCategoria Categoria = null;
            int ID = Convert.ToInt32(valor);
            SqlConnection conexion = Conexion.ConexionSql;
            SqlCommand cmd = conexion.CreateCommand();
            cmd.CommandText = "usp_MostrarCategoria";
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();
            cmd.Parameters.AddWithValue("@CategoriaId", ID);
            SqlDataReader categoriasReader = cmd.ExecuteReader();
            if (categoriasReader.Read())
            {
                Categoria = new beCategoria();
                Categoria.NombreCategoria = categoriasReader["NombreCategoria"].ToString();
            }
            return Categoria;   //retornamos la lista
        }

        public bool ActualizarCategoria(beCategoria Categoria)
        {
            bool res = false;
            try
            {
                SqlConnection conexion = Conexion.ConexionSql;
                SqlCommand cmd = conexion.CreateCommand();
                cmd.CommandText = "usp_ActualizarCategoria";
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                cmd.Parameters.AddWithValue("@CategoriaId", Categoria.CategoriaId);

                SqlParameter par1 = cmd.Parameters.Add("@NomCat", SqlDbType.VarChar, 50);
                par1.Direction = ParameterDirection.Input;
                par1.Value = Categoria.NombreCategoria;

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

        public void EliminarCategoria(int Cat)
        {
            SqlConnection conexion = Conexion.ConexionSql;
            SqlCommand cmd = conexion.CreateCommand();
            cmd.CommandText = "usp_EliminarCategoria";
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();
            cmd.Parameters.AddWithValue("@CategoriaId", Cat);
            cmd.ExecuteNonQuery();
            conexion.Close();
        }
    }    
}
