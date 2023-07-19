using Capa_Datos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio
{
    public class ProductoNeg : CRUD<Producto>
    {
        private CD_Productos objcd_Producto = new CD_Productos();

        public int Create(Producto item, out string mensaje)
        {
            //validaciones de que los campos esten completos antes de pasar al proceso de carga a la base de datos
            mensaje = string.Empty;

            if (item.Codigo == "")
            {
                mensaje += "Es necesario el codigo del Producto\n";
            }

            if (item.Nombre == "")
            {
                mensaje += "Es necesario el nombre del Producto\n";
            }

            if (item.Descripcion == "")
            {
                mensaje += "Es necesario la Descripcion del Producto\n";
            }

            if (mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objcd_Producto.Create(item, out mensaje);
            }
        }

        public bool Delete(Producto item, out string mensaje)
        {
            return objcd_Producto.Delete(item, out mensaje);
        }

        public List<Producto> Listar()
        {
            return objcd_Producto.Listar();
        }

        public bool Update(Producto item, out string mensaje)
        {
            //validacion de que los campos esten llenos antes de modificar la base de datos
            mensaje = string.Empty;


            if (item.Codigo == "")
            {
                mensaje += "Es necesario el codigo del Producto\n";
            }

            if (item.Nombre == "")
            {
                mensaje += "Es necesario el nombre del Producto\n";
            }

            if (item.Descripcion == "")
            {
                mensaje += "Es necesario la Descripcion del Producto\n";
            }

            if (mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objcd_Producto.Update(item, out mensaje);
            }
        }
    }
}
