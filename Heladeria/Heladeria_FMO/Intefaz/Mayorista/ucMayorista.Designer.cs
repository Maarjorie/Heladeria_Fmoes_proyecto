namespace Heladeria_FMO.Intefaz.Mayorista
{
    partial class ucMayorista
    {
        /// <summary> Variable del diseñador necesaria. </summary>
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        private void InitializeComponent()
        {
            pnlCard = new Guna.UI2.WinForms.Guna2Panel();
            dgvPedidos = new Guna.UI2.WinForms.Guna2DataGridView();
            pnlHeader = new Guna.UI2.WinForms.Guna2Panel();
            btnNuevoPedido = new Guna.UI2.WinForms.Guna2Button();
            btnEntregar = new Guna.UI2.WinForms.Guna2Button();
            txtCodigoEntrega = new Guna.UI2.WinForms.Guna2TextBox();
            btnConfirmar = new Guna.UI2.WinForms.Guna2Button();
            btnRefrescar = new Guna.UI2.WinForms.Guna2Button();
            btnClientes = new Guna.UI2.WinForms.Guna2Button();
            lblTitulo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPedidos).BeginInit();
            pnlHeader.SuspendLayout();
            SuspendLayout();
            //
            // pnlCard
            //
            pnlCard.Controls.Add(dgvPedidos);
            pnlCard.Dock = DockStyle.Fill;
            pnlCard.Location = new Point(20, 84);
            pnlCard.Name = "pnlCard";
            pnlCard.Padding = new Padding(2);
            pnlCard.Size = new Size(1080, 526);
            pnlCard.TabIndex = 1;
            //
            // dgvPedidos
            //
            dgvPedidos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPedidos.Dock = DockStyle.Fill;
            dgvPedidos.Location = new Point(2, 2);
            dgvPedidos.Name = "dgvPedidos";
            dgvPedidos.RowHeadersVisible = false;
            dgvPedidos.Size = new Size(1076, 522);
            dgvPedidos.TabIndex = 0;
            dgvPedidos.SelectionChanged += dgvPedidos_SelectionChanged;
            //
            // pnlHeader
            //
            pnlHeader.Controls.Add(btnNuevoPedido);
            pnlHeader.Controls.Add(btnEntregar);
            pnlHeader.Controls.Add(txtCodigoEntrega);
            pnlHeader.Controls.Add(btnConfirmar);
            pnlHeader.Controls.Add(btnRefrescar);
            pnlHeader.Controls.Add(btnClientes);
            pnlHeader.Controls.Add(lblTitulo);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(20, 20);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1080, 64);
            pnlHeader.TabIndex = 0;
            //
            // btnNuevoPedido
            //
            btnNuevoPedido.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnNuevoPedido.Location = new Point(900, 12);
            btnNuevoPedido.Name = "btnNuevoPedido";
            btnNuevoPedido.Size = new Size(180, 42);
            btnNuevoPedido.TabIndex = 4;
            btnNuevoPedido.Text = "+ Nuevo pedido";
            btnNuevoPedido.Click += btnNuevoPedido_Click;
            //
            // btnEntregar
            //
            btnEntregar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnEntregar.Location = new Point(770, 12);
            btnEntregar.Name = "btnEntregar";
            btnEntregar.Size = new Size(120, 42);
            btnEntregar.TabIndex = 3;
            btnEntregar.Text = "Entregar";
            btnEntregar.Click += btnEntregar_Click;
            //
            // txtCodigoEntrega
            //
            txtCodigoEntrega.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtCodigoEntrega.Location = new Point(590, 12);
            txtCodigoEntrega.Name = "txtCodigoEntrega";
            txtCodigoEntrega.PlaceholderText = "Código de retiro…";
            txtCodigoEntrega.Size = new Size(170, 42);
            txtCodigoEntrega.TabIndex = 5;
            //
            // btnConfirmar
            //
            btnConfirmar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnConfirmar.Location = new Point(460, 12);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Size = new Size(120, 42);
            btnConfirmar.TabIndex = 2;
            btnConfirmar.Text = "Confirmar";
            btnConfirmar.Click += btnConfirmar_Click;
            //
            // btnRefrescar
            //
            btnRefrescar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRefrescar.Location = new Point(350, 12);
            btnRefrescar.Name = "btnRefrescar";
            btnRefrescar.Size = new Size(100, 42);
            btnRefrescar.TabIndex = 1;
            btnRefrescar.Text = "Refrescar";
            btnRefrescar.Click += btnRefrescar_Click;
            //
            // btnClientes
            //
            btnClientes.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClientes.Location = new Point(220, 12);
            btnClientes.Name = "btnClientes";
            btnClientes.Size = new Size(120, 42);
            btnClientes.TabIndex = 6;
            btnClientes.Text = "Clientes";
            btnClientes.Click += btnClientes_Click;
            //
            // lblTitulo
            //
            lblTitulo.BackColor = Color.Transparent;
            lblTitulo.Location = new Point(4, 16);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(200, 30);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Pedidos mayoristas";
            //
            // ucMayorista
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlCard);
            Controls.Add(pnlHeader);
            Padding = new Padding(20);
            Name = "ucMayorista";
            Size = new Size(1120, 630);
            pnlCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvPedidos).EndInit();
            pnlHeader.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlHeader;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitulo;
        private Guna.UI2.WinForms.Guna2Button btnRefrescar;
        private Guna.UI2.WinForms.Guna2Button btnConfirmar;
        private Guna.UI2.WinForms.Guna2Button btnEntregar;
        private Guna.UI2.WinForms.Guna2Button btnNuevoPedido;
        private Guna.UI2.WinForms.Guna2TextBox txtCodigoEntrega;
        private Guna.UI2.WinForms.Guna2Button btnClientes;
        private Guna.UI2.WinForms.Guna2Panel pnlCard;
        private Guna.UI2.WinForms.Guna2DataGridView dgvPedidos;
    }
}
