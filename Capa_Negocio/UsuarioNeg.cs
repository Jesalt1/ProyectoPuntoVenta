using Capa_Datos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio
{
    public class UsuarioNeg
    {
        private UsuarioDT objDt_Usuario = new UsuarioDT();

        public List<Usuario> Listar()
        {
            return objDt_Usuario.Listar();
        }
    }
}
