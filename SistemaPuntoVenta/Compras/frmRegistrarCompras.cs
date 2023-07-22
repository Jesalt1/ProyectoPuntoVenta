using Capa_Negocio;
using CapaEntidad;
using CapaPresentacion.Modales;
using SistemaPuntoVenta.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;

namespace SistemaPuntoVenta.Compras
{
    public partial class frmRegistrarCompras : Form
    {
        private Usuario _usuario;
        int idProveedorTmp;
        int idProductoTmp;
        public frmRegistrarCompras(Usuario usuario = null)
        {
            //se establece que el usuario que ha iniciado quede guardado en los registros
            InitializeComponent();
            _usuario = usuario;
        }

        private void frmRegistrarCompras_Load(object sender, EventArgs e)
        {
            ComboBoxFill();
        }

        private void dgvdata_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvdata.Rows.Count)
            {
                dgvdata.Rows[e.RowIndex].Selected = true;

            }
        }

        private void dgvdata_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvdata.Columns[e.ColumnIndex].Name == "btneliminar")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    dgvdata.Rows.RemoveAt(indice);
                    calcularTotal();
                }
            }
        }

        private void txtcodproducto_KeyDown(object sender, KeyEventArgs e)
        {
            //cada que se presione el la tecla enter/salto de linea se ejecutara este comando
            //esto se hace con la intencion que al usar el lector de codigos este por defecto genera un salto de linea y 
            //asi llamar automaticamente el evento de busqueda
            if (e.KeyData == Keys.Enter)
            {

                Producto oProducto = new ProductoNeg().Listar().Where(p => p.Codigo == txtcodproducto.Text && p.Estado == true).FirstOrDefault();

                if (oProducto != null)
                {
                    txtcodproducto.BackColor = Color.Honeydew;
                    idProductoTmp = oProducto.IdProducto;
                    txtproducto.Text = oProducto.Nombre;
                    txtpreciocompra.Select();
                }
                //en caso de no encontrar coincidencias se resaltara en rojo el textbox
                else
                {
                    txtcodproducto.BackColor = Color.MistyRose;
                    idProductoTmp = 0;
                    txtproducto.Text = "";
                }


            }
        }

        //metodo para rellenar el combobox de tipo de documento y fecha actual en la que se hara el registro
        private void ComboBoxFill()
        {
            cbotipodocumento.Items.Add(new ComboBoxOption() { value = "Boleta", text = "Boleta" });
            cbotipodocumento.Items.Add(new ComboBoxOption() { value = "Factura", text = "Factura" });
            cbotipodocumento.DisplayMember = "Text";
            cbotipodocumento.ValueMember = "value";
            cbotipodocumento.SelectedIndex = 0;

            txtfecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        //botones
        #region
        private void btnbuscarproveedor_Click(object sender, EventArgs e)
        {
            using (var modal = new mdProveedor())
            {
                var result = modal.ShowDialog();
                if (result == DialogResult.OK)
                {
                    idProveedorTmp = modal._Proveedor.IdProveedor;
                    txtdocproveedor.Text = modal._Proveedor.Documento;
                    txtnombreproveedor.Text = modal._Proveedor.RazonSocial;
                }
            };
        }

        private void btnbuscarproducto_Click(object sender, EventArgs e)
        {
            using (var modal = new mdProducto())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {
                    idProductoTmp = modal._Producto.IdProducto;
                    txtcodproducto.Text = modal._Producto.Codigo;
                    txtproducto.Text = modal._Producto.Nombre;
                    txtpreciocompra.Select();
                }
                else
                {
                    txtcodproducto.Select();
                }

            }
        }

        private void btnagregarproducto_Click(object sender, EventArgs e)
        {
            //aqui se realizan las validaciones de que todos los campos se encuentren llenos
            decimal preciocompra = 0;
            decimal precioventa = 0;
            bool producto_existe = false;

            if (idProductoTmp == 0)
            {
                MessageBox.Show("Debe seleccionar un producto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (!decimal.TryParse(txtpreciocompra.Text, out preciocompra))
            {
                MessageBox.Show("Precio Compra - Formato moneda incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtpreciocompra.Select();
                return;
            }

            if (!decimal.TryParse(txtprecioventa.Text, out precioventa))
            {
                MessageBox.Show("Precio Venta - Formato moneda incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtprecioventa.Select();
                return;
            }

            foreach (DataGridViewRow fila in dgvdata.Rows)
            {
                if (fila.Cells["IdProducto"].Value.ToString() == idProductoTmp.ToString())
                {
                    producto_existe = true;
                    break;
                }
            }

            if (!producto_existe)
            {

                dgvdata.Rows.Add(new object[] {
                    idProductoTmp,
                    txtproducto.Text,
                    preciocompra.ToString("0.00"),
                    precioventa.ToString("0.00"),
                    txtcantidad.Value.ToString(),
                    (txtcantidad.Value * preciocompra).ToString("0.00")

                });

                calcularTotal();
                limpiarProducto();
                txtcodproducto.Select();

            }
        }

        private void btnregistrar_Click(object sender, EventArgs e)
        {
            if (idProveedorTmp == 0)
            {
                MessageBox.Show("Debe seleccionar un proveedor", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (dgvdata.Rows.Count < 1)
            {
                MessageBox.Show("Debe ingresar productos en la compra", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            //se genera un dataTable por el procedimiento almacenado en el que se estable que las compras 
            //son tablas puesto que estas se gusrdaran primero en una tabla para pasar a las tablas de la base de datos
            //y tener un mejor orden de transsaccion de datos
            DataTable detalle_compra = new DataTable();

            detalle_compra.Columns.Add("IdProducto", typeof(int));
            detalle_compra.Columns.Add("PrecioCompra", typeof(decimal));
            detalle_compra.Columns.Add("PrecioVenta", typeof(decimal));
            detalle_compra.Columns.Add("Cantidad", typeof(int));
            detalle_compra.Columns.Add("MontoTotal", typeof(decimal));

            foreach (DataGridViewRow row in dgvdata.Rows)
            {
                detalle_compra.Rows.Add(
                    new object[] {
                       Convert.ToInt32(row.Cells["IdProducto"].Value.ToString()),
                       row.Cells["PrecioCompra"].Value.ToString(),
                       row.Cells["PrecioVenta"].Value.ToString(),
                       row.Cells["Cantidad"].Value.ToString(),
                       row.Cells["SubTotal"].Value.ToString()
                    });

            }

            //una vez creada la tabla se empieza a enviar todo a nuestros metodos almacenados y 
            //creamos una formato especifico para el
            int idcorrelativo = new CompraNeg().ObtenerCorrelativo();
            string numerodocumento = string.Format("{0:00000}", idcorrelativo);

            Compra oCompra = new Compra()
            {
                oUsuario = new Usuario() { IdUsuario = _usuario.IdUsuario },
                oProveedor = new Proveedores() { IdProveedor = idProveedorTmp },
                TipoDocumento = ((ComboBoxOption)cbotipodocumento.SelectedItem).text,
                NumeroDocumento = numerodocumento,
                MontoTotal = Convert.ToDecimal(txttotalpagar.Text)
            };

            string mensaje = string.Empty;
            bool respuesta = new CompraNeg().Registrar(oCompra, detalle_compra, out mensaje);

            if (respuesta)
            {
                var result = MessageBox.Show("Numero de compra generada:\n" + numerodocumento + "\n\n¿Desea copiar al portapapeles?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (result == DialogResult.Yes)
                    Clipboard.SetText(numerodocumento);

                idProveedorTmp = 0;
                txtdocproveedor.Text = "";
                txtnombreproveedor.Text = "";
                dgvdata.Rows.Clear();
                calcularTotal();

            }
            else
            {
                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        #endregion

        //metodos extras
        #region
        private void calcularTotal()
        {
            decimal total = 0;
            if (dgvdata.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvdata.Rows)
                    total += Convert.ToDecimal(row.Cells["SubTotal"].Value.ToString());
            }
            txttotalpagar.Text = total.ToString("0.00");
        }

        private void limpiarProducto()
        {
            idProductoTmp = 0;
            txtcodproducto.Text = "";
            txtcodproducto.BackColor = Color.White;
            txtproducto.Text = "";
            txtpreciocompra.Text = "";
            txtprecioventa.Text = "";
            txtcantidad.Value = 1;
        }
        #endregion


       

        
    }
}
