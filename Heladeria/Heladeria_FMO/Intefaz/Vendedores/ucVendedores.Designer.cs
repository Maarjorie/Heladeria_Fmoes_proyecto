namespace Heladeria_FMO.Intefaz.Vendedores
{
    partial class ucVendedores
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
            tlp = new TableLayoutPanel();
            pnlFichas = new Guna.UI2.WinForms.Guna2Panel();
            dgvSalidas = new Guna.UI2.WinForms.Guna2DataGridView();
            lblFichasTit = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlLiq = new Guna.UI2.WinForms.Guna2Panel();
            btnValidar = new Guna.UI2.WinForms.Guna2Button();
            dgvLiquidaciones = new Guna.UI2.WinForms.Guna2DataGridView();
            lblLiqTit = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlHeader = new Guna.UI2.WinForms.Guna2Panel();
            btnNuevaFicha = new Guna.UI2.WinForms.Guna2Button();
            btnRefrescar = new Guna.UI2.WinForms.Guna2Button();
            lblTitulo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            tlp.SuspendLayout();
            pnlFichas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSalidas).BeginInit();
            pnlLiq.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLiquidaciones).BeginInit();
            pnlHeader.SuspendLayout();
            SuspendLayout();
            //
            // tlp
            //
            tlp.ColumnCount = 2;
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlp.Controls.Add(pnlFichas, 0, 0);
            tlp.Controls.Add(pnlLiq, 1, 0);
            tlp.Dock = DockStyle.Fill;
            tlp.Location = new Point(20, 84);
            tlp.Name = "tlp";
            tlp.RowCount = 1;
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlp.Size = new Size(1080, 526);
            tlp.TabIndex = 1;
            //
            // pnlFichas
            //
            pnlFichas.Controls.Add(dgvSalidas);
            pnlFichas.Controls.Add(lblFichasTit);
            pnlFichas.Dock = DockStyle.Fill;
            pnlFichas.Location = new Point(3, 3);
            pnlFichas.Margin = new Padding(3, 3, 12, 3);
            pnlFichas.Name = "pnlFichas";
            pnlFichas.Padding = new Padding(16, 56, 16, 16);
            pnlFichas.Size = new Size(525, 520);
            pnlFichas.TabIndex = 0;
            //
            // dgvSalidas
            //
            dgvSalidas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSalidas.Dock = DockStyle.Fill;
            dgvSalidas.Location = new Point(16, 56);
            dgvSalidas.Name = "dgvSalidas";
            dgvSalidas.RowHeadersVisible = false;
            dgvSalidas.Size = new Size(493, 448);
            dgvSalidas.TabIndex = 1;
            //
            // lblFichasTit
            //
            lblFichasTit.BackColor = Color.Transparent;
            lblFichasTit.Location = new Point(16, 16);
            lblFichasTit.Name = "lblFichasTit";
            lblFichasTit.Size = new Size(300, 28);
            lblFichasTit.TabIndex = 0;
            lblFichasTit.Text = "Fichas de salida";
            //
            // pnlLiq
            //
            pnlLiq.Controls.Add(dgvLiquidaciones);
            pnlLiq.Controls.Add(btnValidar);
            pnlLiq.Controls.Add(lblLiqTit);
            pnlLiq.Dock = DockStyle.Fill;
            pnlLiq.Location = new Point(552, 3);
            pnlLiq.Margin = new Padding(12, 3, 3, 3);
            pnlLiq.Name = "pnlLiq";
            pnlLiq.Padding = new Padding(16, 56, 16, 16);
            pnlLiq.Size = new Size(525, 520);
            pnlLiq.TabIndex = 1;
            //
            // dgvLiquidaciones
            //
            dgvLiquidaciones.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLiquidaciones.Dock = DockStyle.Fill;
            dgvLiquidaciones.Location = new Point(16, 56);
            dgvLiquidaciones.Name = "dgvLiquidaciones";
            dgvLiquidaciones.RowHeadersVisible = false;
            dgvLiquidaciones.Size = new Size(493, 400);
            dgvLiquidaciones.TabIndex = 1;
            //
            // btnValidar
            //
            btnValidar.Dock = DockStyle.Bottom;
            btnValidar.Location = new Point(16, 456);
            btnValidar.Name = "btnValidar";
            btnValidar.Size = new Size(493, 48);
            btnValidar.TabIndex = 2;
            btnValidar.Text = "Validar liquidación seleccionada";
            btnValidar.Click += btnValidar_Click;
            //
            // lblLiqTit
            //
            lblLiqTit.BackColor = Color.Transparent;
            lblLiqTit.Location = new Point(16, 16);
            lblLiqTit.Name = "lblLiqTit";
            lblLiqTit.Size = new Size(320, 28);
            lblLiqTit.TabIndex = 0;
            lblLiqTit.Text = "Liquidaciones pendientes";
            //
            // pnlHeader
            //
            pnlHeader.Controls.Add(btnNuevaFicha);
            pnlHeader.Controls.Add(btnRefrescar);
            pnlHeader.Controls.Add(lblTitulo);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(20, 20);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1080, 64);
            pnlHeader.TabIndex = 0;
            //
            // btnNuevaFicha
            //
            btnNuevaFicha.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnNuevaFicha.Location = new Point(860, 12);
            btnNuevaFicha.Name = "btnNuevaFicha";
            btnNuevaFicha.Size = new Size(220, 42);
            btnNuevaFicha.TabIndex = 2;
            btnNuevaFicha.Text = "+ Nueva ficha de salida";
            btnNuevaFicha.Click += btnNuevaFicha_Click;
            //
            // btnRefrescar
            //
            btnRefrescar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRefrescar.Location = new Point(740, 12);
            btnRefrescar.Name = "btnRefrescar";
            btnRefrescar.Size = new Size(110, 42);
            btnRefrescar.TabIndex = 1;
            btnRefrescar.Text = "Refrescar";
            btnRefrescar.Click += btnRefrescar_Click;
            //
            // lblTitulo
            //
            lblTitulo.BackColor = Color.Transparent;
            lblTitulo.Location = new Point(4, 16);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(400, 30);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Vendedores ambulantes";
            //
            // ucVendedores
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tlp);
            Controls.Add(pnlHeader);
            Padding = new Padding(20);
            Name = "ucVendedores";
            Size = new Size(1120, 630);
            tlp.ResumeLayout(false);
            pnlFichas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvSalidas).EndInit();
            pnlLiq.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvLiquidaciones).EndInit();
            pnlHeader.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlHeader;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitulo;
        private Guna.UI2.WinForms.Guna2Button btnRefrescar;
        private Guna.UI2.WinForms.Guna2Button btnNuevaFicha;
        private TableLayoutPanel tlp;
        private Guna.UI2.WinForms.Guna2Panel pnlFichas;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblFichasTit;
        private Guna.UI2.WinForms.Guna2DataGridView dgvSalidas;
        private Guna.UI2.WinForms.Guna2Panel pnlLiq;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblLiqTit;
        private Guna.UI2.WinForms.Guna2DataGridView dgvLiquidaciones;
        private Guna.UI2.WinForms.Guna2Button btnValidar;
    }
}
