using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Capa_Datos
{
    //Clase para obtener configiracion mediante una referencia en System.Configuration
    public class ConexionBD
    {
        //cadena statica que se obtiene de la configuarcion app.config en la que se agrego la siguiente linea
        // <connectionStrings>
        //<add name = "Cadena_Conexion" connectionString="Data Source=(Local);Initial Catalog=punto de venta; Integrated Security=True" providerName="System.Data.SqlClient"  /></connectionStrings>
        public static String cadena = ConfigurationManager.ConnectionStrings["Cadena_Conexion"].ToString();

    }
}
