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
using System.Net.NetworkInformation;
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
        private int idTmp;
        public frmUsuarios()
        {
            InitializeComponent();
        }

        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            ComboBoxFill();
            LoadDGV();
            FilterSearch();
        }

        //Evento para seleccionar toda una fila al momento de hacer click en ella
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

            //rellenar combobox con los rooles disponibles
            List<Rol> listaRol = new RolNeg().Listar();

            foreach (Rol item in listaRol)
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

                dgvdata.Rows.Add(new object[] {item.IdUsuario,item.Documento,item.NombreCompleto,item.Correo,item.Clave,
                    item.oRol.IdRol,
                    item.oRol.Descripcion,
                    item.Estado == true ? 1 : 0 ,
                    item.Estado == true ? "Activo" : "Inactivo"
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
                    cbobusqueda.Items.Add(new ComboBoxOption() { value =columna.Name, text = columna.HeaderText });
                }
            }
            cbobusqueda.DisplayMember = "text";
            cbobusqueda.ValueMember = "value";
            cbobusqueda.SelectedIndex = 0;
        }


        #endregion

        //region botones
        #region
        private void btnguardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            Usuario objusuario = new Usuario()
            {
                IdUsuario = idTmp,
                Documento = txtdocumento.Text,
                NombreCompleto = txtnombrecompleto.Text,
                Correo = txtcorreo.Text,
                Clave = txtclave.Text,
                oRol = new Rol() { IdRol = Convert.ToInt32(((ComboBoxOption)cborol.SelectedItem).value) },
                Estado = Convert.ToInt32(((ComboBoxOption)cboestado.SelectedItem).value) == 1 ? true : false
            };

            if (objusuario.IdUsuario == 0)//si el id es 0 se abriran los procesos para generar un nuevo usuario
            {
                int idusuariogenerado = new UsuarioNeg().Create(objusuario, out mensaje);

                if (idusuariogenerado != 0)
                {

                    dgvdata.Rows.Add(new object[] {idusuariogenerado,txtdocumento.Text,txtnombrecompleto.Text,txtcorreo.Text,txtclave.Text,
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
            else//si existe el id del usuario se ejecuta los metodos para editar el usuario en cuestion
            {
                bool resultado = new UsuarioNeg().Update(objusuario, out mensaje);

                if (resultado == true)
                {

                    DataGridViewRow row = dgvdata.Rows[indice];
                    row.Cells["Id"].Value = idTmp;
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

        private void btneliminar_Click(object sender, EventArgs e)
        {
            //se obtiene el Id de la fila seleccionada y se envia como parametro para hacer una consulta a la base de datos
            //y al encontrar coincidencia se procede a eliminar el usuario
            //se utiliza un dialogresult para validar el borrado y proceder 
            //al terminar e eliminar devolvera un valor booleano que confirmara el exito de la operacion
            if (idTmp != 0)
            {
                if (MessageBox.Show("¿Desea eliminar el usuario", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    string mensaje = string.Empty;
                    Usuario objusuario = new Usuario()
                    {
                        IdUsuario = idTmp
                    };

                    bool respuesta = new UsuarioNeg().Delete(objusuario, out mensaje);

                    if (respuesta)
                    {
                        dgvdata.Rows.RemoveAt(Convert.ToInt32(indice));
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
            string columnaFiltroEstado = ((ComboBoxOption)cbobusqueda.Items[4]).value.ToString();

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

        //Metodos internos
        #region
        //metodo para rellenar los textbox y ComboBox segun lo que el usuario selecciones en el DGV
        //eso con la finalidad de preparar el terreno para realizar las modificaciones necesarias

        private void FillTextBox(DataGridViewCellEventArgs e)
        {


            idTmp = Convert.ToInt32(dgvdata.Rows[e.RowIndex].Cells["Id"].Value);
            txtdocumento.Text = dgvdata.Rows[e.RowIndex].Cells["Documento"].Value.ToString();
            txtnombrecompleto.Text = dgvdata.Rows[e.RowIndex].Cells["NombreCompleto"].Value.ToString();
            txtcorreo.Text = dgvdata.Rows[e.RowIndex].Cells["Correo"].Value.ToString();
            txtclave.Text = dgvdata.Rows[e.RowIndex].Cells["Clave"].Value.ToString();
            txtconfirmarclave.Text = dgvdata.Rows[e.RowIndex].Cells["Clave"].Value.ToString();


            foreach (ComboBoxOption oc in cborol.Items)
            {
                Console.WriteLine(Convert.ToInt32(oc.value) == Convert.ToInt32(dgvdata.Rows[e.RowIndex].Cells["IdRol"].Value));


                if (Convert.ToInt32(oc.value) == Convert.ToInt32(dgvdata.Rows[e.RowIndex].Cells["IdRol"].Value))
                {
                    int indice_combo = cborol.Items.IndexOf(oc);
                    cborol.SelectedIndex = indice_combo;
                    break;
                }
            }


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
        private void Clean()
        {

            txtdocumento.Text = "";
            txtnombrecompleto.Text = "";
            txtcorreo.Text = "";
            txtclave.Text = "";
            txtconfirmarclave.Text = "";
            cborol.SelectedIndex = 0;
            cboestado.SelectedIndex = 0;
            idTmp = 0;
            txtdocumento.Select();

        }



        #endregion

       
    }
}
