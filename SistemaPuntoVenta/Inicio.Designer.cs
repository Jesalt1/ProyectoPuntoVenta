namespace SistemaPuntoVenta
{
    partial class Inicio
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.iconMenuItemUsuarios = new FontAwesome.Sharp.IconMenuItem();
            this.iconMenuItemMantenedor = new FontAwesome.Sharp.IconMenuItem();
            this.iconMenuItemVentas = new FontAwesome.Sharp.IconMenuItem();
            this.iconMenuItemCompras = new FontAwesome.Sharp.IconMenuItem();
            this.iconMenuItemClientes = new FontAwesome.Sharp.IconMenuItem();
            this.iconMenuItemProveedor = new FontAwesome.Sharp.IconMenuItem();
            this.iconMenuItemReportes = new FontAwesome.Sharp.IconMenuItem();
            this.menuStripTitle = new System.Windows.Forms.MenuStrip();
            this.label1 = new System.Windows.Forms.Label();
            this.panelChildForm = new System.Windows.Forms.Panel();
            this.UsuarioLabel = new System.Windows.Forms.Label();
            this.iconMenuItemCategoria = new FontAwesome.Sharp.IconMenuItem();
            this.iconMenuItemProductos = new FontAwesome.Sharp.IconMenuItem();
            this.iconMenuItemRegistrarVentas = new FontAwesome.Sharp.IconMenuItem();
            this.iconMenuItemDetallesVenta = new FontAwesome.Sharp.IconMenuItem();
            this.iconMenuItemRegistrarCompras = new FontAwesome.Sharp.IconMenuItem();
            this.iconMenuItemDetallesCompras = new FontAwesome.Sharp.IconMenuItem();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iconMenuItemUsuarios,
            this.iconMenuItemMantenedor,
            this.iconMenuItemVentas,
            this.iconMenuItemCompras,
            this.iconMenuItemClientes,
            this.iconMenuItemProveedor,
            this.iconMenuItemReportes});
            this.menuStrip.Location = new System.Drawing.Point(0, 59);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1032, 84);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // iconMenuItemUsuarios
            // 
            this.iconMenuItemUsuarios.AutoSize = false;
            this.iconMenuItemUsuarios.IconChar = FontAwesome.Sharp.IconChar.UserGear;
            this.iconMenuItemUsuarios.IconColor = System.Drawing.Color.Black;
            this.iconMenuItemUsuarios.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconMenuItemUsuarios.IconSize = 50;
            this.iconMenuItemUsuarios.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.iconMenuItemUsuarios.Name = "iconMenuItemUsuarios";
            this.iconMenuItemUsuarios.Size = new System.Drawing.Size(122, 80);
            this.iconMenuItemUsuarios.Text = "Usuarios";
            this.iconMenuItemUsuarios.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.iconMenuItemUsuarios.Click += new System.EventHandler(this.iconMenuItemUsuarios_Click);
            // 
            // iconMenuItemMantenedor
            // 
            this.iconMenuItemMantenedor.AutoSize = false;
            this.iconMenuItemMantenedor.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iconMenuItemCategoria,
            this.iconMenuItemProductos});
            this.iconMenuItemMantenedor.IconChar = FontAwesome.Sharp.IconChar.Tools;
            this.iconMenuItemMantenedor.IconColor = System.Drawing.Color.Black;
            this.iconMenuItemMantenedor.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconMenuItemMantenedor.IconSize = 50;
            this.iconMenuItemMantenedor.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.iconMenuItemMantenedor.Name = "iconMenuItemMantenedor";
            this.iconMenuItemMantenedor.Size = new System.Drawing.Size(122, 80);
            this.iconMenuItemMantenedor.Text = "Mantenedor";
            this.iconMenuItemMantenedor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // iconMenuItemVentas
            // 
            this.iconMenuItemVentas.AutoSize = false;
            this.iconMenuItemVentas.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iconMenuItemRegistrarVentas,
            this.iconMenuItemDetallesVenta});
            this.iconMenuItemVentas.IconChar = FontAwesome.Sharp.IconChar.Tags;
            this.iconMenuItemVentas.IconColor = System.Drawing.Color.Black;
            this.iconMenuItemVentas.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconMenuItemVentas.IconSize = 50;
            this.iconMenuItemVentas.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.iconMenuItemVentas.Name = "iconMenuItemVentas";
            this.iconMenuItemVentas.Size = new System.Drawing.Size(122, 80);
            this.iconMenuItemVentas.Text = "Ventas";
            this.iconMenuItemVentas.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // iconMenuItemCompras
            // 
            this.iconMenuItemCompras.AutoSize = false;
            this.iconMenuItemCompras.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iconMenuItemRegistrarCompras,
            this.iconMenuItemDetallesCompras});
            this.iconMenuItemCompras.IconChar = FontAwesome.Sharp.IconChar.DollyFlatbed;
            this.iconMenuItemCompras.IconColor = System.Drawing.Color.Black;
            this.iconMenuItemCompras.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconMenuItemCompras.IconSize = 50;
            this.iconMenuItemCompras.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.iconMenuItemCompras.Name = "iconMenuItemCompras";
            this.iconMenuItemCompras.Size = new System.Drawing.Size(122, 80);
            this.iconMenuItemCompras.Text = "Compras";
            this.iconMenuItemCompras.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // iconMenuItemClientes
            // 
            this.iconMenuItemClientes.AutoSize = false;
            this.iconMenuItemClientes.IconChar = FontAwesome.Sharp.IconChar.UserGroup;
            this.iconMenuItemClientes.IconColor = System.Drawing.Color.Black;
            this.iconMenuItemClientes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconMenuItemClientes.IconSize = 50;
            this.iconMenuItemClientes.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.iconMenuItemClientes.Name = "iconMenuItemClientes";
            this.iconMenuItemClientes.Size = new System.Drawing.Size(122, 80);
            this.iconMenuItemClientes.Text = "Clientes";
            this.iconMenuItemClientes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // iconMenuItemProveedor
            // 
            this.iconMenuItemProveedor.AutoSize = false;
            this.iconMenuItemProveedor.IconChar = FontAwesome.Sharp.IconChar.AddressCard;
            this.iconMenuItemProveedor.IconColor = System.Drawing.Color.Black;
            this.iconMenuItemProveedor.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconMenuItemProveedor.IconSize = 50;
            this.iconMenuItemProveedor.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.iconMenuItemProveedor.Name = "iconMenuItemProveedor";
            this.iconMenuItemProveedor.Size = new System.Drawing.Size(122, 80);
            this.iconMenuItemProveedor.Text = "Proveedor";
            this.iconMenuItemProveedor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // iconMenuItemReportes
            // 
            this.iconMenuItemReportes.AutoSize = false;
            this.iconMenuItemReportes.IconChar = FontAwesome.Sharp.IconChar.BarChart;
            this.iconMenuItemReportes.IconColor = System.Drawing.Color.Black;
            this.iconMenuItemReportes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconMenuItemReportes.IconSize = 50;
            this.iconMenuItemReportes.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.iconMenuItemReportes.Name = "iconMenuItemReportes";
            this.iconMenuItemReportes.Size = new System.Drawing.Size(122, 80);
            this.iconMenuItemReportes.Text = "Reportes";
            this.iconMenuItemReportes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // menuStripTitle
            // 
            this.menuStripTitle.AutoSize = false;
            this.menuStripTitle.BackColor = System.Drawing.Color.SteelBlue;
            this.menuStripTitle.Location = new System.Drawing.Point(0, 0);
            this.menuStripTitle.Name = "menuStripTitle";
            this.menuStripTitle.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.menuStripTitle.Size = new System.Drawing.Size(1032, 59);
            this.menuStripTitle.TabIndex = 1;
            this.menuStripTitle.Text = "menuStrip2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.SteelBlue;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(263, 31);
            this.label1.TabIndex = 2;
            this.label1.Text = "SISTEMA VENTAS";
            // 
            // panelChildForm
            // 
            this.panelChildForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChildForm.Location = new System.Drawing.Point(0, 143);
            this.panelChildForm.Name = "panelChildForm";
            this.panelChildForm.Size = new System.Drawing.Size(1032, 307);
            this.panelChildForm.TabIndex = 3;
            // 
            // UsuarioLabel
            // 
            this.UsuarioLabel.AutoSize = true;
            this.UsuarioLabel.BackColor = System.Drawing.Color.SteelBlue;
            this.UsuarioLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UsuarioLabel.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.UsuarioLabel.Location = new System.Drawing.Point(822, 23);
            this.UsuarioLabel.Name = "UsuarioLabel";
            this.UsuarioLabel.Size = new System.Drawing.Size(59, 20);
            this.UsuarioLabel.TabIndex = 4;
            this.UsuarioLabel.Text = "aaaaa";
            // 
            // iconMenuItemCategoria
            // 
            this.iconMenuItemCategoria.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconMenuItemCategoria.IconColor = System.Drawing.Color.Black;
            this.iconMenuItemCategoria.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconMenuItemCategoria.Name = "iconMenuItemCategoria";
            this.iconMenuItemCategoria.Size = new System.Drawing.Size(180, 22);
            this.iconMenuItemCategoria.Text = "Categoria";
            this.iconMenuItemCategoria.Click += new System.EventHandler(this.iconMenuItemCategoria_Click);
            // 
            // iconMenuItemProductos
            // 
            this.iconMenuItemProductos.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconMenuItemProductos.IconColor = System.Drawing.Color.Black;
            this.iconMenuItemProductos.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconMenuItemProductos.Name = "iconMenuItemProductos";
            this.iconMenuItemProductos.Size = new System.Drawing.Size(180, 22);
            this.iconMenuItemProductos.Text = "Productos";
            this.iconMenuItemProductos.Click += new System.EventHandler(this.iconMenuItemProductos_Click);
            // 
            // iconMenuItemRegistrarVentas
            // 
            this.iconMenuItemRegistrarVentas.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconMenuItemRegistrarVentas.IconColor = System.Drawing.Color.Black;
            this.iconMenuItemRegistrarVentas.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconMenuItemRegistrarVentas.Name = "iconMenuItemRegistrarVentas";
            this.iconMenuItemRegistrarVentas.Size = new System.Drawing.Size(180, 22);
            this.iconMenuItemRegistrarVentas.Text = "Registrar";
            this.iconMenuItemRegistrarVentas.Click += new System.EventHandler(this.iconMenuItemRegistrarVentas_Click);
            // 
            // iconMenuItemDetallesVenta
            // 
            this.iconMenuItemDetallesVenta.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconMenuItemDetallesVenta.IconColor = System.Drawing.Color.Black;
            this.iconMenuItemDetallesVenta.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconMenuItemDetallesVenta.Name = "iconMenuItemDetallesVenta";
            this.iconMenuItemDetallesVenta.Size = new System.Drawing.Size(180, 22);
            this.iconMenuItemDetallesVenta.Text = "Detalles";
            this.iconMenuItemDetallesVenta.Click += new System.EventHandler(this.iconMenuItemDetallesVenta_Click);
            // 
            // iconMenuItemRegistrarCompras
            // 
            this.iconMenuItemRegistrarCompras.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconMenuItemRegistrarCompras.IconColor = System.Drawing.Color.Black;
            this.iconMenuItemRegistrarCompras.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconMenuItemRegistrarCompras.Name = "iconMenuItemRegistrarCompras";
            this.iconMenuItemRegistrarCompras.Size = new System.Drawing.Size(180, 22);
            this.iconMenuItemRegistrarCompras.Text = "Registrar";
            this.iconMenuItemRegistrarCompras.Click += new System.EventHandler(this.iconMenuItemRegistrarCompras_Click);
            // 
            // iconMenuItemDetallesCompras
            // 
            this.iconMenuItemDetallesCompras.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconMenuItemDetallesCompras.IconColor = System.Drawing.Color.Black;
            this.iconMenuItemDetallesCompras.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconMenuItemDetallesCompras.Name = "iconMenuItemDetallesCompras";
            this.iconMenuItemDetallesCompras.Size = new System.Drawing.Size(180, 22);
            this.iconMenuItemDetallesCompras.Text = "Detalles";
            this.iconMenuItemDetallesCompras.Click += new System.EventHandler(this.iconMenuItemDetallesCompras_Click);
            // 
            // Inicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1032, 450);
            this.Controls.Add(this.UsuarioLabel);
            this.Controls.Add(this.panelChildForm);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.menuStripTitle);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "Inicio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Inicio_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.MenuStrip menuStripTitle;
        private FontAwesome.Sharp.IconMenuItem iconMenuItemUsuarios;
        private FontAwesome.Sharp.IconMenuItem iconMenuItemMantenedor;
        private FontAwesome.Sharp.IconMenuItem iconMenuItemVentas;
        private FontAwesome.Sharp.IconMenuItem iconMenuItemCompras;
        private FontAwesome.Sharp.IconMenuItem iconMenuItemClientes;
        private FontAwesome.Sharp.IconMenuItem iconMenuItemProveedor;
        private FontAwesome.Sharp.IconMenuItem iconMenuItemReportes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelChildForm;
        private System.Windows.Forms.Label UsuarioLabel;
        private FontAwesome.Sharp.IconMenuItem iconMenuItemCategoria;
        private FontAwesome.Sharp.IconMenuItem iconMenuItemProductos;
        private FontAwesome.Sharp.IconMenuItem iconMenuItemRegistrarVentas;
        private FontAwesome.Sharp.IconMenuItem iconMenuItemDetallesVenta;
        private FontAwesome.Sharp.IconMenuItem iconMenuItemRegistrarCompras;
        private FontAwesome.Sharp.IconMenuItem iconMenuItemDetallesCompras;
    }
}
