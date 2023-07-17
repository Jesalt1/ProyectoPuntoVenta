using Capa_Datos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio
{
    public class UsuarioNeg: CRUD<Usuario>
    {
        private UsuarioDT objDt_Usuario = new UsuarioDT();

        public int Create(Usuario item, out string mensaje)
        {
            //validacion de que todos los campos esten llenos
            mensaje = string.Empty;

            mensaje = (item.Documento == "") ? "Es necesario el documento del usuario\n" : string.Empty;
            mensaje += (item.NombreCompleto == "") ? "Es necesario el nombre completo del usuario\n" : string.Empty;
            mensaje += (item.Clave == "") ? "Es necesario la clave del usuario\n" : string.Empty;

            return string.IsNullOrEmpty(mensaje) ? objDt_Usuario.Create(item, out mensaje) : 0;


        }

        public bool Delete(Usuario item, out string mensaje)
        {
           return objDt_Usuario.Delete(item, out mensaje);
        }

        public  List<Usuario> Listar()
        {
            return objDt_Usuario.Listar();
        }

        public bool Update(Usuario item, out string mensaje)
        {
            //validacion de que todos los campos esten llenos
            mensaje = string.Empty;

            mensaje = (item.Documento == "") ? "Es necesario el documento del usuario\n" : string.Empty;
            mensaje += (item.NombreCompleto == "") ? "Es necesario el nombre completo del usuario\n" : string.Empty;
            mensaje += (item.Clave == "") ? "Es necesario la clave del usuario\n" : string.Empty;

            return string.IsNullOrEmpty(mensaje) && objDt_Usuario.Update(item, out mensaje);
        }
    }
}
