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

            mensaje += string.IsNullOrWhiteSpace(item.Descripcion) ? "Es necesario la descripción de la Categoría\n" : "";

            return string.IsNullOrWhiteSpace(mensaje) ? objcd_Categoria.Create(item, out mensaje) : 0;

        }

        public bool Update(Categoria item, out string mensaje)
        {
           mensaje = string.Empty;

            //este operador ternario esta comprobando que descrippcion no este vacio con espacios en blancos
            mensaje = string.IsNullOrWhiteSpace(item.Descripcion) ? "Es necesario la descripción de la Categoría\n" : string.Empty;

            return string.IsNullOrEmpty(mensaje) && objcd_Categoria.Update(item, out mensaje);

        }

        public bool Delete(Categoria item, out string mensaje)
        {
           return objcd_Categoria.Delete(item, out mensaje);
        }
    }
}
