using Capa_Datos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio
{
    public class CategoriaNeg : CRUD<Categoria>
    {


        private CD_Categorias objcd_Categoria = new CD_Categorias();

        public List<Categoria> Listar()
        {
            return objcd_Categoria.Listar();
        }

      
        public int Create(Categoria item, out string mensaje)
        {
            mensaje = string.Empty;


            if (item.Descripcion == "")
            {
                mensaje += "Es necesario la descripcion de la Categoria\n";
            }

            if (mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objcd_Categoria.Create(item, out mensaje);
            };
        }

        public bool Update(Categoria item, out string mensaje)
        {
            mensaje = string.Empty;

            if (item.Descripcion == "")
            {
                mensaje += "Es necesario la descripcion de la Categoria\n";
            }

            if (mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objcd_Categoria.Update(item, out mensaje);
            }
        }

        public bool Delete(Categoria item, out string mensaje)
        {
           return objcd_Categoria.Delete(item, out mensaje);
        }
    }
}
