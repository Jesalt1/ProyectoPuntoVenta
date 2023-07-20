using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capa_Datos
{
    public class CD_Clientes : CRUD<Cliente>
    {
        public int Create(Cliente item, out string mensaje)
        {
            int idClientegenerado = 0;
            mensaje = string.Empty;
            try
            {

                using (SqlConnection oconexion = new SqlConnection(ConexionBD.cadena))
                {

                    SqlCommand cmd = new SqlCommand("sp_RegistrarCliente", oconexion);
                    cmd.Parameters.AddWithValue("Documento", item.Documento);
                    cmd.Parameters.AddWithValue("NombreCompleto", item.NombreCompleto);
                    cmd.Parameters.AddWithValue("Correo", item.Correo);
                    cmd.Parameters.AddWithValue("Telefono", item.Telefono);
                    cmd.Parameters.AddWithValue("Estado", item.Estado);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    idClientegenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }

            }
            catch (Exception ex)
            {
                idClientegenerado = 0;
                mensaje = ex.Message;
            }



            return idClientegenerado;
        }

        public bool Delete(Cliente item, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(ConexionBD.cadena))
                {

                    SqlCommand cmd = new SqlCommand("delete from cliente where IdCliente = @id", oconexion);
                    cmd.Parameters.AddWithValue("@id", item.IdCliente);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    //si existen filas afectadas la respuesta sera verdadera, si el valor de filas aefctadas es 0 la respuesta es falso
                    respuesta = cmd.ExecuteNonQuery() > 0 ? true : false;
                }

            }
            catch (Exception ex)
            {
                respuesta = false;
                mensaje = ex.Message;
            }

            return respuesta;
        }

        public List<Cliente> Listar()
        {
            List<Cliente> lista = new List<Cliente>();

            using (SqlConnection oconexion = new SqlConnection(ConexionBD.cadena))
            {

                try
                {

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select IdCliente,Documento,NombreCompleto,Correo,Telefono,Estado from CLIENTE");
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Cliente()
                            {
                                IdCliente = Convert.ToInt32(dr["IdCliente"]),
                                Documento = dr["Documento"].ToString(),
                                NombreCompleto = dr["NombreCompleto"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                Telefono = dr["Telefono"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"]),
                            });

                        }

                    }


                }
                catch (Exception ex)
                {

                    lista = new List<Cliente>();
                }
            }

            return lista;
        }

        public bool Update(Cliente item, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;


            try
            {

                using (SqlConnection oconexion = new SqlConnection(ConexionBD.cadena))
                {

                    SqlCommand cmd = new SqlCommand("sp_ModificarCliente", oconexion);
                    cmd.Parameters.AddWithValue("IdCliente", item.IdCliente);
                    cmd.Parameters.AddWithValue("Documento", item.Documento);
                    cmd.Parameters.AddWithValue("NombreCompleto", item.NombreCompleto);
                    cmd.Parameters.AddWithValue("Correo", item.Correo);
                    cmd.Parameters.AddWithValue("Telefono", item.Telefono);
                    cmd.Parameters.AddWithValue("Estado", item.Estado);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }

            }
            catch (Exception ex)
            {
                respuesta = false;
                mensaje = ex.Message;
            }



            return respuesta;
        }
    }
}
