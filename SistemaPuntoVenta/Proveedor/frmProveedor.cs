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
using System.Windows.Documents;
using System.Windows.Forms;

namespace SistemaPuntoVenta.Proveedor
{
    public partial class frmProveedor : Form
    {
        int indice;
        int idTmp;
        public frmProveedor()
        {
            InitializeComponent();
        }

        private void frmProveedor_Load(object sender, EventArgs e)
        {
            ComboBoxFill();
            LoadDGV();
            FilterSearch();
        }

        private void dgvdata_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvdata.Rows.Count)
            {

                dgvdata.Rows[e.RowIndex].Selected = true;


                indice = e.RowIndex;

                FillTextBox(e);
            }
        }

        //region de cargado de datos en el form
        #region

        //metodo encargado de rellenar el combo box de estado del frm de usuarios
        private void ComboBoxFill()
        {
            cboestado.Items.Add(new ComboBoxOption() { value = 1, text = "Activo" });
            cboestado.Items.Add(new ComboBoxOption() { value = 0, text = "Inactivo" });
            cboestado.DisplayMember = "Text";
            cboestado.ValueMember = "value";
            cboestado.SelectedIndex = 0;

        }

        //Cargar la listas DataGrifView al momento de cargar el frm con los usuarios ya registrados en la base de datos
        private void LoadDGV()
        {

            List<Proveedores> lista = new ProveedorNeg().Listar();

            foreach (Proveedores item in lista)
            {
                dgvdata.Rows.Add(new object[] {item.IdProveedor,item.Documento,item.RazonSocial,item.Correo,item.Telefono,
                    item.Estado == true ? 1 : 0 ,
                    item.Estado == true ? "Activo" : "No Activo"
                });
            }

        }

        //llenar el combobox del filtrado de busqueda con los nombres de las columnas del dataGird
        private void FilterSearch()
        {
            foreach (DataGridViewColumn columna in dgvdata.Columns)
            {
                if (columna.Visible)
                {
                    cbobusqueda.Items.Add(new ComboBoxOption() { value = columna.Name, text = columna.HeaderText });
                }
            }
            cbobusqueda.DisplayMember = "text";
            cbobusqueda.ValueMember = "value";
            cbobusqueda.SelectedIndex = 0;
        }


        #endregion

        //Metodos botones
        #region
        private void btnguardar_Click_1(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            Proveedores obj = new Proveedores()
            {
                IdProveedor = idTmp,
                Documento = txtdocumento.Text,
                RazonSocial = txtRazonSocial.Text,
                Correo = txtcorreo.Text,
                Telefono = txtTelefono.Text,
                Estado = Convert.ToInt32(((ComboBoxOption)cboestado.SelectedItem).value) == 1 ? true : false
            };

            if (obj.IdProveedor == 0)
            {
                int idgenerado = new ProveedorNeg().Create(obj, out mensaje);

                if (idgenerado != 0)
                {

                    dgvdata.Rows.Add(new object[] {idgenerado,txtdocumento.Text,txtRazonSocial.Text,txtcorreo.Text,txtTelefono.Text,
                        ((ComboBoxOption)cboestado.SelectedItem).value.ToString(),
                        ((ComboBoxOption)cboestado.SelectedItem).text.ToString()
                    });

                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }


            }
            else
            {
                bool resultado = new ProveedorNeg().Update(obj, out mensaje);

                if (resultado)
                {
                    DataGridViewRow row = dgvdata.Rows[indice];
                    row.Cells["Id"].Value = idTmp;
                    row.Cells["Documento"].Value = txtdocumento.Text;
                    row.Cells["RazonSocial"].Value = txtRazonSocial.Text;
                    row.Cells["Correo"].Value = txtcorreo.Text;
                    row.Cells["Telefono"].Value = txtTelefono.Text;
                    row.Cells["EstadoValor"].Value = ((ComboBoxOption)cboestado.SelectedItem).value.ToString();
                    row.Cells["Estado"].Value = ((ComboBoxOption)cboestado.SelectedItem).text.ToString();
                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }
            }
        }

        private void btnlimpiar_Click_1(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btneliminar_Click_1(object sender, EventArgs e)
        {
            if (idTmp != 0)
            {
                if (MessageBox.Show("¿Desea eliminar el cliente", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    string mensaje = string.Empty;
                    Proveedores obj = new Proveedores()
                    {
                        IdProveedor = idTmp
                    };

                    bool respuesta = new ProveedorNeg().Delete(obj, out mensaje);

                    if (respuesta)
                    {
                        dgvdata.Rows.RemoveAt(indice);
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                }
            }
        }

        private void btnbuscar_Click_1(object sender, EventArgs e)
        {
            string columnaFiltro = ((ComboBoxOption)cbobusqueda.SelectedItem).value.ToString();

            if (dgvdata.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvdata.Rows)
                {
                    row.Visible = row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper().Contains(txtbusqueda.Text.Trim().ToUpper()) ? true : false;


                }
            }
        }

        private void btnlimpiarbuscador_Click_1(object sender, EventArgs e)
        {
            //limpia la caja de texto de busca filtrada y restablece todos los valores del dataGrid
            txtbusqueda.Text = string.Empty;
            foreach (DataGridViewRow row in dgvdata.Rows)
            {
                row.Visible = true;
            }
        }
        #endregion

        //Metodos internos
        #region
        //metodo para rellenar los textbox y ComboBox segun lo que el usuario selecciones en el DGV
        //eso con la finalidad de preparar el terreno para realizar las modificaciones necesarias

        private void FillTextBox(DataGridViewCellEventArgs e)
        {


            idTmp = Convert.ToInt32(dgvdata.Rows[indice].Cells["Id"].Value);
            txtdocumento.Text = dgvdata.Rows[e.RowIndex].Cells["Documento"].Value.ToString();
            txtRazonSocial.Text = dgvdata.Rows[e.RowIndex].Cells["RazonSocial"].Value.ToString();
            txtcorreo.Text = dgvdata.Rows[e.RowIndex].Cells["Correo"].Value.ToString();
            txtTelefono.Text = dgvdata.Rows[e.RowIndex].Cells["Telefono"].Value.ToString();

            foreach (ComboBoxOption oc in cboestado.Items)
            {
                try
                {
                    if (Convert.ToInt32(oc.value) == (Convert.ToInt32(dgvdata.Rows[e.RowIndex].Cells["EstadoValor"].Value.ToString())))
                    {
                        int indice_combo = cboestado.Items.IndexOf(oc);
                        cboestado.SelectedIndex = indice_combo;
                        break;
                    }
                }
                catch (Exception ex)
                {

                }

            }
        }

        //metodo para limpiar las cajas de texto para nuevos registro/ediciones
        private void Limpiar()
        {

            txtdocumento.Text = "";
            txtRazonSocial.Text = "";
            txtcorreo.Text = "";
            txtTelefono.Text = "";
            cboestado.SelectedIndex = 0;
            idTmp = 0;
            txtdocumento.Select();

        }


        #endregion

      
    }
}
