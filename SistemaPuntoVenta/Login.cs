using CapaEntidad;
using Capa_Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaPuntoVenta
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void iconButtonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void iconButtonLogin_Click(object sender, EventArgs e)
        {

            OpenMainMenu();
        }


        private void OpenMainMenu()
        {
            //estoy probando si git actualiza cambios
            Usuario usuario = new UsuarioNeg().Listar().Where(u => u.Documento.Equals(textBoxUser.Text) && u.Clave.Equals(textBoxPassword.Text)).FirstOrDefault();

            if (usuario != null)
            {
                Inicio inicio = new Inicio();
                inicio.Show();
                this.Hide();


                inicio.FormClosing += FrmClosing;//envento de cerrado y retorno al fomrulario login
            }
            else
            {
                MessageBox.Show("no hay usuario registrado","Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
           
        }

        //evento para regresar al formulario de login en caso de cerrar menu principal
        private void FrmClosing(object sender, FormClosingEventArgs e)
        {
            //setteando los texbox a vacios y mostrando formulario login
            textBoxPassword.Text = String.Empty;
            textBoxUser.Text = String.Empty;    
            this.Show();
        }

    }
}
