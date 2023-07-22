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
 
            Usuario usuario = new UsuarioNeg().Listar().Where(u => u.Documento.Equals(textBoxUser.Text) && u.Clave.Equals(textBoxPassword.Text)).FirstOrDefault();

            if (usuario != null)
            {
                if (usuario.Estado.Equals(true))
                {
                    //se envia el objeto del tipo usuario que se ha obtenido de la consulta a la base de datos
                    //
                    Inicio inicio = new Inicio(usuario);
                    inicio.Show();
                    this.Hide();


                    inicio.FormClosing += FrmClosing;//envento de cerrado y retorno al fomrulario login
                }
                else
                {
                    MessageBox.Show("Usuario inactivo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
               
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

        private void Login_Load(object sender, EventArgs e)
        {
            textBoxUser.Text= "101010";
            textBoxPassword.Text= "123";
        }
    }
}
