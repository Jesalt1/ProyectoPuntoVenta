using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos
{
    public class CD_Roles
    {
        public List<Rol> Listar()
        {
            List<Rol> lista = new List<Rol>();

            using (SqlConnection conexion = new SqlConnection(ConexionBD.cadena))//abrimos la conexion con la base de datos desde la configuaracion inicial
            {
                try
                {
                    //Comando SQL con la cual llamaremos los datos de los roles distintos que existen en la base de datos
     
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select IdRol, Descripcion from ROL");


                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.CommandType = CommandType.Text;

                    conexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {

                            
                            lista.Add(new Rol()
                            {
                                IdRol = Convert.ToInt32(dr["IdRol"]),

                                Descripcion = dr["Descripcion"].ToString(),
                            });

                        }

                    }
                }
                catch (Exception ex)
                {

                    lista = new List<Rol>();
                }
            }

            return lista;

        }
    }
}
