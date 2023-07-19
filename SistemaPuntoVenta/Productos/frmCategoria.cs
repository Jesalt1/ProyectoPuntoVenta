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
using System.Windows.Forms;

namespace SistemaPuntoVenta.Productos
{
    public partial class frmCategoria : Form
    {
        private int indice;
        private int idTmp;

        public frmCategoria()
        {
            InitializeComponent();
        }

        private void frmCategoria_Load(object sender, EventArgs e)
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



        //metodos de carga de datos
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

        //Cargar la listas DataGrifView al momento de cargar el frm con las categorias  ya registrados en la base de datos
        private void LoadDGV()
        {
            List<Categoria> lista = new CategoriaNeg().Listar();

            foreach (Categoria item in lista)
            {

                dgvdata.Rows.Add(new object[] {item.IdCategoria,
                    item.Descripcion,
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

        //metodos botones
        #region

        private void btnguardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            Categoria obj = new Categoria()
            {
                IdCategoria = idTmp,
                Descripcion = txtdescripcion.Text,
                Estado = Convert.ToInt32(((ComboBoxOption)cboestado.SelectedItem).value) == 1 ? true : false
            };

            if (obj.IdCategoria == 0)
            {
                int idgenerado = new CategoriaNeg().Create(obj, out mensaje);

                if (idgenerado != 0)
                {

                    dgvdata.Rows.Add(new object[] {idgenerado,txtdescripcion.Text,
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
                bool resultado = new CategoriaNeg().Update(obj, out mensaje);

                if (resultado)
                {
                    
                    DataGridViewRow row = dgvdata.Rows[indice];
                    row.Cells["Id"].Value = idTmp;
                    row.Cells["Descripcion"].Value = txtdescripcion.Text;
                    row.Cells["Estado"].Value = ((ComboBoxOption)cboestado.SelectedItem).value.ToString();
                    row.Cells["EstadoValor"].Value = ((ComboBoxOption)cboestado.SelectedItem).text.ToString();
                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }
            }
        }

        private void btnlimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            if (idTmp != 0)
            {
                if (MessageBox.Show("¿Desea eliminar la categoria", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    string mensaje = string.Empty;
                    Categoria obj = new Categoria()
                    {
                        IdCategoria = idTmp
                    };

                    bool respuesta = new CategoriaNeg().Delete(obj, out mensaje);

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

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((ComboBoxOption)cbobusqueda.SelectedItem).value.ToString();
            string columnaFiltroEstado = ((ComboBoxOption)cbobusqueda.Items[1]).value.ToString();

            //filtrara si lls caracteres ingresados estan dentro de los valores de las columnas
            if ((dgvdata.Rows.Count > 0) && !(columnaFiltro.Equals(columnaFiltroEstado)))
            {
                foreach (DataGridViewRow row in dgvdata.Rows)
                {
                    row.Visible = row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper().Contains(txtbusqueda.Text.Trim().ToUpper()) ? true : false;
                }
            }
            //filtrara solo y solo si la palabra estado (activo/inactivo) son iguales a los valores de las columnas
            else if ((dgvdata.Rows.Count > 0) && (columnaFiltro.Equals(columnaFiltroEstado)))
            {
                foreach (DataGridViewRow row in dgvdata.Rows)
                {
                    row.Visible = row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper().Equals(txtbusqueda.Text.Trim().ToUpper()) ? true : false;
                }
            }
        }

        private void btnlimpiarbuscador_Click(object sender, EventArgs e)
        {
            //limpia la caja de texto de busca filtrada y restablece todos los valores del dataGrid
            txtbusqueda.Text = string.Empty;
            foreach (DataGridViewRow row in dgvdata.Rows)
            {
                row.Visible = true;
            }
        }


        #endregion

        //metodos extras
        #region
        private void Limpiar()
        {

            txtdescripcion.Text = "";
            idTmp = 0;
            cboestado.SelectedIndex = 0;
            txtdescripcion.Select();
        }

        private void FillTextBox(DataGridViewCellEventArgs e)
        {
            txtdescripcion.Text = dgvdata.Rows[indice].Cells["Descripcion"].Value.ToString();


            idTmp = Convert.ToInt32(dgvdata.Rows[e.RowIndex].Cells["Id"].Value);

            foreach (ComboBoxOption oc in cboestado.Items)
            {
                try
                {
                    if (Convert.ToInt32(oc.value) == (Convert.ToInt32(dgvdata.Rows[e.RowIndex].Cells["Estado"].Value.ToString())))
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



        #endregion

        
    }
}
