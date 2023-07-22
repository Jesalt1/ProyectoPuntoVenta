using Capa_Datos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio
{
    public class ProveedorNeg : CRUD<Proveedores>
    {
        private CD_Proveedor objcd_Proveedor = new CD_Proveedor();
        public int Create(Proveedores item, out string mensaje)
        {
            //validacion de que todos los campos esten llenos
            mensaje = string.Empty;

            mensaje += string.IsNullOrWhiteSpace(item.Documento) ? "Es necesario el documento del Proveedor\n" : "";
            mensaje += string.IsNullOrWhiteSpace(item.RazonSocial) ? "Es necesario la Razon Social\n" : "";
            mensaje += string.IsNullOrWhiteSpace(item.Correo) ? "Es necesario el correo del Proveedor\n" : "";

            return string.IsNullOrWhiteSpace(mensaje) ? objcd_Proveedor.Create(item, out mensaje) : 0;
        }

        public bool Delete(Proveedores item, out string mensaje)
        {
            return objcd_Proveedor.Delete(item, out mensaje);
        }

        public List<Proveedores> Listar()
        {
            return objcd_Proveedor.Listar();
        }

        public bool Update(Proveedores item, out string mensaje)
        {
            mensaje = string.Empty;

            mensaje += string.IsNullOrWhiteSpace(item.Documento) ? "Es necesario el documento del Proveedor\n" : "";
            mensaje += string.IsNullOrWhiteSpace(item.RazonSocial) ? "Es necesario la Razon Social\n" : "";
            mensaje += string.IsNullOrWhiteSpace(item.Correo) ? "Es necesario el correo del Proveedor\n" : "";

            return string.IsNullOrEmpty(mensaje) ? objcd_Proveedor.Update(item, out mensaje) : false;
        }
    }
}
