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
    public partial class frmProductos : Form
    {
        int idTmp;
        int indice;
        public frmProductos()
        {
            InitializeComponent();
        }

        private void frmProductos_Load(object sender, EventArgs e)
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
        //metodo encargado de rellenar el combo box de estado y categoria del frm de usuarios
        private void ComboBoxFill()
        {
            cboestado.Items.Add(new ComboBoxOption() { value = 1, text = "Activo" });
            cboestado.Items.Add(new ComboBoxOption() { value = 0, text = "No Activo" });
            cboestado.DisplayMember = "text";
            cboestado.ValueMember = "value";
            cboestado.SelectedIndex = 0;

            //lenando el comboBox con las categorias generadas en el submodulo Categorias
            List<Categoria> listacategoria = new CategoriaNeg().Listar();

            foreach (Categoria item in listacategoria)
            {
                //validacion de que la categoria se encuentre activa, en caso de no serlo no sera cargado en la lista
                if (item.Estado.Equals(true))
                {
                    cbocategoria.Items.Add(new ComboBoxOption() { value = item.IdCategoria, text = item.Descripcion });
                }
            }
            cbocategoria.DisplayMember = "text";
            cbocategoria.ValueMember = "value";
            cbocategoria.SelectedIndex = 0;
        }

        //Cargar la listas DataGrifView al momento de cargar el frm con las categorias  ya registrados en la base de datos
        private void LoadDGV()
        {
            List<Producto> lista = new ProductoNeg().Listar();

            foreach (Producto item in lista)
            {

                dgvdata.Rows.Add(new object[] {
                    item.IdProducto,
                    item.Codigo,
                    item.Nombre,
                    item.Descripcion,
                    item.oCategoria.IdCategoria,
                    item.oCategoria.Descripcion,
                    item.Stock,
                    item.PrecioCompra,
                    item.PrecioVenta,
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

        //Evento de botones
        #region
        private void btnguardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            Producto obj = new Producto()
            {
                IdProducto = idTmp,
                Codigo = txtcodigo.Text,
                Nombre = txtnombre.Text,
                Descripcion = txtdescripcion.Text,
                oCategoria = new Categoria() { IdCategoria = Convert.ToInt32(((ComboBoxOption)cbocategoria.SelectedItem).value) },
                Estado = Convert.ToInt32(((ComboBoxOption)cboestado.SelectedItem).value) == 1 ? true : false
            };

            if (obj.IdProducto == 0)
            {
                int idgenerado = new ProductoNeg().Create(obj, out mensaje);

                if (idgenerado != 0)
                {

                    dgvdata.Rows.Add(new object[] {
                       idgenerado,
                       txtcodigo.Text,
                       txtnombre.Text,
                       txtdescripcion.Text,
                       ((ComboBoxOption)cbocategoria.SelectedItem).value.ToString(),
                       ((ComboBoxOption)cbocategoria.SelectedItem).text.ToString(),
                       "0",
                       "0.00",
                       "0.00",
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
                bool resultado = new ProductoNeg().Update(obj, out mensaje);

                if (resultado)
                {
                    DataGridViewRow row = dgvdata.Rows[Convert.ToInt32(indice)];
                    row.Cells["Id"].Value = idTmp;
                    row.Cells["Codigo"].Value = txtcodigo.Text;
                    row.Cells["Nombre"].Value = txtnombre.Text;
                    row.Cells["Descripcion"].Value = txtdescripcion.Text;
                    row.Cells["IdCategoria"].Value = ((ComboBoxOption)cbocategoria.SelectedItem).value.ToString();
                    row.Cells["Categoria"].Value = ((ComboBoxOption)cbocategoria.SelectedItem).text.ToString();
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

        private void btnlimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            if (idTmp != 0)
            {
                if (MessageBox.Show("¿Desea eliminar el producto", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    string mensaje = string.Empty;
                    Producto obj = new Producto()
                    {
                        IdProducto = Convert.ToInt32(idTmp)
                    };

                    bool respuesta = new ProductoNeg().Delete(obj, out mensaje);

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

        #endregion

        //metodos extras
        #region
        private void Limpiar()
        {
            idTmp = 0;
            txtcodigo.Text = "";
            txtnombre.Text = "";
            txtdescripcion.Text = "";
            cbocategoria.SelectedIndex = 0;
            cboestado.SelectedIndex = 0;


        }

        //llenar las cajas de texto al seleccionar una fila del DataViedGriew
        private void FillTextBox(DataGridViewCellEventArgs e)
        {

            txtcodigo.Text = dgvdata.Rows[indice].Cells["Codigo"].Value.ToString();
            txtnombre.Text = dgvdata.Rows[indice].Cells["Nombre"].Value.ToString();
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
