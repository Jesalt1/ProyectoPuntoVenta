using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Capa_Datos;
using Capa_Negocio;
using CapaEntidad;
using FontAwesome.Sharp;
using SistemaPuntoVenta.Compras;
using SistemaPuntoVenta.Productos;
using SistemaPuntoVenta.Usuarios;
using SistemaPuntoVenta.Ventas;

namespace SistemaPuntoVenta
{
    public partial class Inicio : Form
    {
        //Objeto estatico del tipo de usuario que se utilizara para navegar entre forms con los permisos del Usuario
        private static Usuario usuarioActual;
        //Nos permitira verificar que MenuItem esta activo
        private static IconMenuItem menuActivo = null;
        //este objeto guardara el formulario que estara abierto
        private static Form formularioActivo = null;
        public Inicio(Usuario objUsuario)
        {
            usuarioActual = objUsuario;

            InitializeComponent();
        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            //atributos para establecer el tamaño maximo de la ventana independientemente del monitor y resolucion que se use
            this.MaximumSize = SystemInformation.PrimaryMonitorMaximizedWindowSize;
            this.WindowState = FormWindowState.Maximized;

            //se cambia el texto del label con el nombre del usuario con el se ha iniciado sesion 
            toolStripTextBoxUser.Text = usuarioActual.NombreCompleto.ToString();
            
            PermisosDeUsuario();
        }

        //Metodo que se utilizara para cambiar el estado visual de las opciones en las que se encuentra el usuario
        //asi mismo se encargara de abrir los formularios a los que se estan llamando
        private void AbrirFormularios(IconMenuItem menu, Form formulario)
        {
            //Sombrear el boton y opcion en la que se encuentra el usuario
            if (menuActivo != null)
            {
                menuActivo.BackColor = Color.White;
            }

            menu.BackColor = Color.Silver;
            menuActivo = menu;

            //esta expresion de navegacion segura se encarga de cerrar el formulario activo en caso de cambiar de formulario
            formularioActivo?.Close();

            //abre el formulario con los parametros de configuracion necesarios
            formularioActivo = formulario;
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            formulario.BackColor = Color.SteelBlue;

            panelChildForm.Controls.Add(formulario);
            formulario.Show();
        }

        //metodo que se usara para habilitar/deshabilitar las diferentes opciones del software segun 
        //los permisos que este tenga
        private void PermisosDeUsuario()
        {
            List<Permiso> permisos = new PermisoNeg().Listar(usuarioActual.IdUsuario);
            foreach (IconMenuItem iconMenuItem in menuStrip.Items)
            {
                bool habilitarPermisos = permisos.Any(m => m.NombreMenu == iconMenuItem.Name);

                if(habilitarPermisos==false)
                {
                    iconMenuItem.Visible = false;
                }
            }
        }

        //eventos de click en los diferentes botones en la cual los usuarios podran abrir los formularios
        private void iconMenuItemUsuarios_Click(object sender, EventArgs e)
        {
            AbrirFormularios((IconMenuItem)sender, new frmUsuarios());
        }

        private void iconMenuItemCategoria_Click(object sender, EventArgs e)
        {
            AbrirFormularios((IconMenuItem)sender, new frmCategoria());
        }

        private void iconMenuItemProductos_Click(object sender, EventArgs e)
        {
            AbrirFormularios((IconMenuItem)sender, new frmProductos());
        }

        private void iconMenuItemRegistrarVentas_Click(object sender, EventArgs e)
        {
            AbrirFormularios((IconMenuItem)sender, new frmRegistrarVentas());
        }

        private void iconMenuItemDetallesVenta_Click(object sender, EventArgs e)
        {
            AbrirFormularios((IconMenuItem)sender, new frmDetallesVentas());
        }

        private void iconMenuItemRegistrarCompras_Click(object sender, EventArgs e)
        {
            AbrirFormularios((IconMenuItem)sender, new frmRegistrarCompras());
        }

        private void iconMenuItemDetallesCompras_Click(object sender, EventArgs e)
        {
            AbrirFormularios((IconMenuItem)sender, new frmDetallesCompras());
        }


    }
}
