using Capa_Datos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio
{
    public class ClientesNeg : CRUD<Cliente>
    {

        private CD_Clientes objcd_Cliente = new CD_Clientes();
        public int Create(Cliente item, out string mensaje)
        {
            //validacion de que todos los campos esten llenos
            mensaje = string.Empty;

            mensaje += string.IsNullOrWhiteSpace(item.Documento) ? "Es necesario el documento del Cliente\n" : "";
            mensaje += string.IsNullOrWhiteSpace(item.NombreCompleto) ? "Es necesario el nombre completo del Cliente\n" : "";
            mensaje += string.IsNullOrWhiteSpace(item.Correo) ? "Es necesario el correo del Cliente\n" : "";

            return string.IsNullOrWhiteSpace(mensaje) ? objcd_Cliente.Create(item, out mensaje) : 0;
        }

        public bool Delete(Cliente item, out string mensaje)
        {
            return objcd_Cliente.Delete(item, out mensaje);
        }

        public List<Cliente> Listar()
        {
            return objcd_Cliente.Listar();
        }

        public bool Update(Cliente item, out string mensaje)
        {
            mensaje = string.Empty;

            mensaje += string.IsNullOrWhiteSpace(item.Documento) ? "Es necesario el documento del Cliente\n" : "";
            mensaje += string.IsNullOrWhiteSpace(item.NombreCompleto) ? "Es necesario el nombre completo del Cliente\n" : "";
            mensaje += string.IsNullOrWhiteSpace(item.Correo) ? "Es necesario el correo del Cliente\n" : "";

            return string.IsNullOrEmpty(mensaje) ? objcd_Cliente.Update(item, out mensaje) : false;

        }
    }
}
