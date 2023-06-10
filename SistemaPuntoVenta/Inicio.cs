using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CapaEntidad;

namespace SistemaPuntoVenta
{
    public partial class Inicio : Form
    {
        //Objeto estatico del tipo de usuario que se utilizara para navegar entre forms con los permisos del Usuario
        private static Usuario usuarioActual;
        public Inicio(Usuario objUsuario)
        {
            usuarioActual = objUsuario;
            InitializeComponent();
        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            //se cambia el texto del label con el nombre del usuario con el se ha iniciado sesion 
            UsuarioLabel.Text = usuarioActual.NombreCompleto.ToString();
        }
    }
}
