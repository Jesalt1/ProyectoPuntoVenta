using CapaEntidad;
using Capa_Negocio;
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

namespace CapaPresentacion
{
    public partial class frmReporteCompras : Form
    {
        public frmReporteCompras()
        {
            InitializeComponent();
        }

        private void frmReporteCompras_Load(object sender, EventArgs e)
        {
            List<Proveedores> lista = new ProveedorNeg().Listar();

            cboproveedor.Items.Add(new ComboBoxOption() { value = 0, text = "TODOS" });
            foreach (Proveedores item in lista)
            {
                cboproveedor.Items.Add(new ComboBoxOption() { value = item.IdProveedor, text = item.RazonSocial });
            }
            cboproveedor.DisplayMember = "text";
            cboproveedor.ValueMember = "value";
            cboproveedor.SelectedIndex = 0;


            foreach (DataGridViewColumn columna in dgvdata.Columns)
            {
                cbobusqueda.Items.Add(new ComboBoxOption() { value = columna.Name, text = columna.HeaderText });
            }
            cbobusqueda.DisplayMember = "text";
            cbobusqueda.ValueMember = "value";
            cbobusqueda.SelectedIndex = 0;


        }

        private void btnbuscarresultado_Click(object sender, EventArgs e)
        {
            int idproveedor = Convert.ToInt32(((ComboBoxOption)cboproveedor.SelectedItem).value.ToString());

            List<ReporteCompra> lista = new List<ReporteCompra>();

            lista = new ReportesNeg().Compra(
                txtfechainicio.Value.ToString(),
                txtfechafin.Value.ToString(),
                idproveedor
                );


            dgvdata.Rows.Clear();

            foreach (ReporteCompra rc in lista) {
                dgvdata.Rows.Add(new object[] {
                    rc.FechaRegistro,
                    rc.TipoDocumento,
                    rc.NumeroDocumento,
                    rc.MontoTotal,
                    rc.UsuarioRegistro,
                    rc.DocumentoProveedor,
                    rc.RazonSocial,
                    rc.CodigoProducto,
                    rc.NombreProducto,
                    rc.Categoria,
                    rc.PrecioCompra,
                    rc.PrecioVenta,
                    rc.Cantidad,
                    rc.SubTotal
                });

            }

            

        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((ComboBoxOption)cbobusqueda.SelectedItem).value.ToString();

            if (dgvdata.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvdata.Rows)
                {

                    if (row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper().Contains(txtbusqueda.Text.Trim().ToUpper()))
                        row.Visible = true;
                    else
                        row.Visible = false;
                }
            }
        }

        private void btnlimpiarbuscador_Click(object sender, EventArgs e)
        {
            txtbusqueda.Text = "";
            foreach (DataGridViewRow row in dgvdata.Rows)
            {
                row.Visible = true;
            }
        }
    }
}
