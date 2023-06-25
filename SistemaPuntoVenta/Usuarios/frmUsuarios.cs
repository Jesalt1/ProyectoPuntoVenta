using Capa_Datos;
using Capa_Negocio;
using CapaEntidad;
using SistemaPuntoVenta.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;

namespace SistemaPuntoVenta.Usuarios
{
    public partial class frmUsuarios : Form
    {
        private int indice;
        public frmUsuarios()
        {
            InitializeComponent();
        }

        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            ComboBoxFill();
            LoadDGV();
        }

        //Evento para seleccionar toda una fila al momento de hacer click en ella
        private void dgvdata_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvdata.Rows.Count)
            {
                dgvdata.Rows[e.RowIndex].Selected = true;
                txtindice.Text = e.RowIndex.ToString();
                indice = e.RowIndex;
                Console.WriteLine(indice);
                Console.WriteLine(txtindice.Text);
                FillTextBox(e);
            }
        }



        //metodo encargado de rellenar el combo box de estado del frm de usuarios
        private void ComboBoxFill()
        {
            cboestado.Items.Add(new ComboBoxOption() { value = true, text = "Activo" });
            cboestado.Items.Add(new ComboBoxOption() { value = true, text = "Inactivo" });
            cboestado.DisplayMember = "Text";
            cboestado.ValueMember = "value";
            cboestado.SelectedIndex = 0;

            //rellenar combobox con los rooles disponibles
            List<Rol> listaRol = new RolNeg().Listar();

            foreach(Rol item in listaRol)
            {
                cborol.Items.Add(new ComboBoxOption() { value = item.IdRol, text = item.Descripcion });
            }
            cborol.DisplayMember = "Text";
            cborol.ValueMember = "value";
            cborol.SelectedIndex = 0;

        }


        //Cargar la listas DataGrifView al momento de cargar el frm con los usuarios ya registrados en la base de datos
        private void LoadDGV()
        {
            List<Usuario> listaUsuario = new UsuarioDT().Listar();

            foreach (Usuario item in listaUsuario)
            {

                dgvdata.Rows.Add(new object[] {"",item.IdUsuario,item.Documento,item.NombreCompleto,item.Correo,item.Clave,
                    item.oRol.IdRol,
                    item.oRol.Descripcion,
                    item.Estado == true ? 1 : 0 ,
                    item.Estado == true ? "Activo" : "No Activo"
                });
            }

        }

       

        //metodo para rellenar los textbox y ComboBox segun lo que el usuario selecciones en el DGV
        //eso con la finalidad de preparar el terreno para realizar las modificaciones necesarias

        private void FillTextBox(DataGridViewCellEventArgs e)
        {

            txtindice.Text = e.ToString();
            txtid.Text = dgvdata.Rows[e.RowIndex].Cells["Id"].Value.ToString();
            txtdocumento.Text = dgvdata.Rows[e.RowIndex].Cells["Documento"].Value.ToString();
            txtnombrecompleto.Text = dgvdata.Rows[e.RowIndex].Cells["NombreCompleto"].Value.ToString();
            txtcorreo.Text = dgvdata.Rows[e.RowIndex].Cells["Correo"].Value.ToString();
            txtclave.Text = dgvdata.Rows[e.RowIndex].Cells["Clave"].Value.ToString();
            txtconfirmarclave.Text = dgvdata.Rows[e.RowIndex].Cells["Clave"].Value.ToString();


            foreach (ComboBoxOption oc in cborol.Items)
            {

                if (Convert.ToInt32(oc.value) == Convert.ToInt32(dgvdata.Rows[e.RowIndex].Cells["IdRol"].Value))
                {
                    int indice_combo = cborol.Items.IndexOf(oc);
                    cborol.SelectedIndex = indice_combo;
                    break;
                }
            }


            foreach (ComboBoxOption oc in cboestado.Items)
            {
                if (Convert.ToInt32(oc.value) == Convert.ToInt32(dgvdata.Rows[e.RowIndex].Cells["EstadoValor"].Value))
                {
                    int indice_combo = cboestado.Items.IndexOf(oc);
                    cboestado.SelectedIndex = indice_combo;
                    break;
                }
            }
        }

        
        //metodo para limpiar las cajas de texto para nuevos registro/ediciones
        private void Clean()
        {

            txtindice.Text = "-1";
            txtid.Text = "0";
            txtdocumento.Text = "";
            txtnombrecompleto.Text = "";
            txtcorreo.Text = "";
            txtclave.Text = "";
            txtconfirmarclave.Text = "";
            cborol.SelectedIndex = 0;
            cboestado.SelectedIndex = 0;

            txtdocumento.Select();

        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            Usuario objusuario = new Usuario()
            {
                IdUsuario = Convert.ToInt32(txtid.Text),
                Documento = txtdocumento.Text,
                NombreCompleto = txtnombrecompleto.Text,
                Correo = txtcorreo.Text,
                Clave = txtclave.Text,
                oRol = new Rol() { IdRol = Convert.ToInt32(((ComboBoxOption)cborol.SelectedItem).value) },
                Estado = Convert.ToInt32(((ComboBoxOption)cboestado.SelectedItem).value) == 1 ? true : false
            };

            if (objusuario.IdUsuario == 0)
            {
                int idusuariogenerado = new UsuarioDT().Create(objusuario);

                if (idusuariogenerado != 0)
                {

                    dgvdata.Rows.Add(new object[] {"",idusuariogenerado,txtdocumento.Text,txtnombrecompleto.Text,txtcorreo.Text,txtclave.Text,
                ((ComboBoxOption)cborol.SelectedItem).value.ToString(),
                ((ComboBoxOption)cborol.SelectedItem).text.ToString(),
                ((ComboBoxOption)cboestado.SelectedItem).value.ToString(),
                ((ComboBoxOption)cboestado.SelectedItem).text.ToString()
                });

                    Clean();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }


            }
            else
            {
                bool resultado = new UsuarioDT().Update(objusuario);

                if (true)
                {
                    Console.WriteLine(txtindice.Text);
                    Console.WriteLine(indice);
                    DataGridViewRow row = dgvdata.Rows[indice];
                    row.Cells["Id"].Value = txtid.Text;
                    row.Cells["Documento"].Value = txtdocumento.Text;
                    row.Cells["NombreCompleto"].Value = txtnombrecompleto.Text;
                    row.Cells["Correo"].Value = txtcorreo.Text;
                    row.Cells["Clave"].Value = txtclave.Text;
                    row.Cells["IdRol"].Value = ((ComboBoxOption)cborol.SelectedItem).value.ToString();
                    row.Cells["Rol"].Value = ((ComboBoxOption)cborol.SelectedItem).text.ToString();
                    row.Cells["EstadoValor"].Value = ((ComboBoxOption)cboestado.SelectedItem).value.ToString();
                    row.Cells["Estado"].Value = ((ComboBoxOption)cboestado.SelectedItem).text.ToString();

                    Clean();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }
            }
        }

        private void btnlimpiar_Click(object sender, EventArgs e)
        {
            Clean();
        }
    }
}
