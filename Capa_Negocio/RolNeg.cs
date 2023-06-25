using Capa_Datos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio
{
    public class RolNeg
    {
            private CD_Roles objcd_Rol = new CD_Roles();

            public List<Rol> Listar()
            {
                return objcd_Rol.Listar();
            }

    }
}
