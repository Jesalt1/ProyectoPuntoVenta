using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Librerias necesarias para la logica
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;

namespace Capa_Datos
{
    public class UsuarioDT:CRUD<Usuario>
    {
        

        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();

            using (SqlConnection conexion = new SqlConnection(ConexionBD.cadena))//abrimos la conexion con la base de datos desde la configuaracion inicial
            {
                try
                {
                    //Comando SQL con la cual llamaremos los datos de usuario y se guardaran en una lista del tipo usuario

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select u.IdUsuario,u.Documento,u.NombreCompleto,u.Correo,u.Clave,u.Estado,r.IdRol,r.Descripcion from usuario u");
                    query.AppendLine("inner join rol r on r.IdRol = u.IdRol");


                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.CommandType = CommandType.Text;

                    conexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {

                            lista.Add(new Usuario()
                            {
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                Documento = dr["Documento"].ToString(),
                                NombreCompleto = dr["NombreCompleto"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                Clave = dr["Clave"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"]),
                                oRol = new Rol() { IdRol = Convert.ToInt32(dr["IdRol"]),
                                Descripcion = dr["Descripcion"].ToString() }
                            });

                        }

                    }
                }
                catch (Exception ex)
                {

                    lista = new List<Usuario>();
                }
            }

            return lista;

        }

        public int Create(Usuario item)
        {
            int idusuariogenerado = 0;
            //Mensaje = string.Empty;


            try
            {

                using (SqlConnection oconexion = new SqlConnection(ConexionBD.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_REGISTRARUSUARIO", oconexion);
                    cmd.Parameters.AddWithValue("Documento", item.Documento);
                    cmd.Parameters.AddWithValue("NombreCompleto", item.NombreCompleto);
                    cmd.Parameters.AddWithValue("Correo", item.Correo);
                    cmd.Parameters.AddWithValue("Clave", item.Clave);
                    cmd.Parameters.AddWithValue("IdRol", item.oRol.IdRol);
                    cmd.Parameters.AddWithValue("Estado", item.Estado);
                    cmd.Parameters.Add("IdUsuarioResultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    idusuariogenerado = Convert.ToInt32(cmd.Parameters["IdUsuarioResultado"].Value);
                   // Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }

            }
            catch (Exception ex)
            {
                idusuariogenerado = 0;
               //Mensaje = ex.Message;
            }



            return idusuariogenerado;
        }

        public void Delete(Usuario item)
        {
            throw new NotImplementedException();
        }

        public bool Update(Usuario item)
        {
            bool respuesta = false;
           // Mensaje = string.Empty;


            try
            {

                using (SqlConnection oconexion = new SqlConnection(ConexionBD.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_EDITARUSUARIO", oconexion);
                    cmd.Parameters.AddWithValue("IdUsuario", item.IdUsuario);
                    cmd.Parameters.AddWithValue("Documento", item.Documento);
                    cmd.Parameters.AddWithValue("NombreCompleto", item.NombreCompleto);
                    cmd.Parameters.AddWithValue("Correo", item.Correo);
                    cmd.Parameters.AddWithValue("Clave", item.Clave);
                    cmd.Parameters.AddWithValue("IdRol", item.oRol.IdRol);
                    cmd.Parameters.AddWithValue("Estado", item.Estado);
                    cmd.Parameters.Add("Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Respuesta"].Value);
                    //Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }

            }
            catch (Exception ex)
            {
                respuesta = false;
                //Mensaje = ex.Message;
            }



            return respuesta;
        }
    }
}

