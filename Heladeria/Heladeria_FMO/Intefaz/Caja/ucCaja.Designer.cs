namespace Heladeria_FMO.Intefaz.Caja
{
    partial class ucCaja
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
            flpCajas = new FlowLayoutPanel();
            pnlAbrir = new Guna.UI2.WinForms.Guna2Panel();
            lblEstado = new Guna.UI2.WinForms.Guna2HtmlLabel();
            btnAbrir = new Guna.UI2.WinForms.Guna2Button();
            txtFondo = new Guna.UI2.WinForms.Guna2TextBox();
            lblFondoCap = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtCajero = new Guna.UI2.WinForms.Guna2TextBox();
            lblCajeroCap = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblAbrirTitulo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlArqueo = new Guna.UI2.WinForms.Guna2Panel();
            btnArqueo = new Guna.UI2.WinForms.Guna2Button();
            btnCerrar = new Guna.UI2.WinForms.Guna2Button();
            lblDiferenciaVal = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblDiferenciaCap = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtContado = new Guna.UI2.WinForms.Guna2TextBox();
            lblContadoCap = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblEsperadoVal = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblEsperadoCap = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblArqueoTitulo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlMovimientos = new Guna.UI2.WinForms.Guna2Panel();
            lblMovTitulo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            btnNuevoMovimiento = new Guna.UI2.WinForms.Guna2Button();
            dgvMovimientos = new Guna.UI2.WinForms.Guna2DataGridView();
            flpCajas.SuspendLayout();
            pnlAbrir.SuspendLayout();
            pnlArqueo.SuspendLayout();
            pnlMovimientos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMovimientos).BeginInit();
            SuspendLayout();
            //
            // flpCajas
            //
            flpCajas.Controls.Add(pnlAbrir);
            flpCajas.Controls.Add(pnlArqueo);
            flpCajas.Controls.Add(pnlMovimientos);
            flpCajas.Dock = DockStyle.Fill;
            flpCajas.Location = new Point(0, 0);
            flpCajas.Name = "flpCajas";
            flpCajas.Padding = new Padding(24);
            flpCajas.Size = new Size(900, 560);
            flpCajas.TabIndex = 0;
            //
            // pnlAbrir
            //
            pnlAbrir.Controls.Add(lblEstado);
            pnlAbrir.Controls.Add(btnAbrir);
            pnlAbrir.Controls.Add(txtFondo);
            pnlAbrir.Controls.Add(lblFondoCap);
            pnlAbrir.Controls.Add(txtCajero);
            pnlAbrir.Controls.Add(lblCajeroCap);
            pnlAbrir.Controls.Add(lblAbrirTitulo);
            pnlAbrir.Location = new Point(27, 27);
            pnlAbrir.Margin = new Padding(3, 3, 24, 3);
            pnlAbrir.Name = "pnlAbrir";
            pnlAbrir.Size = new Size(400, 330);
            pnlAbrir.TabIndex = 0;
            //
            // lblEstado
            //
            lblEstado.BackColor = Color.Transparent;
            lblEstado.Location = new Point(20, 282);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(360, 24);
            lblEstado.TabIndex = 6;
            lblEstado.Text = "Sin caja abierta";
            //
            // btnAbrir
            //
            btnAbrir.Location = new Point(20, 222);
            btnAbrir.Name = "btnAbrir";
            btnAbrir.Size = new Size(360, 46);
            btnAbrir.TabIndex = 5;
            btnAbrir.Text = "Abrir caja";
            btnAbrir.Click += btnAbrir_Click;
            //
            // txtFondo
            //
            txtFondo.Location = new Point(20, 168);
            txtFondo.Name = "txtFondo";
            txtFondo.PlaceholderText = "100.00";
            txtFondo.Size = new Size(360, 36);
            txtFondo.TabIndex = 4;
            //
            // lblFondoCap
            //
            lblFondoCap.BackColor = Color.Transparent;
            lblFondoCap.Location = new Point(20, 142);
            lblFondoCap.Name = "lblFondoCap";
            lblFondoCap.Size = new Size(200, 22);
            lblFondoCap.TabIndex = 3;
            lblFondoCap.Text = "Fondo inicial ($)";
            //
            // txtCajero
            //
            txtCajero.Location = new Point(20, 90);
            txtCajero.Name = "txtCajero";
            txtCajero.ReadOnly = true;
            txtCajero.Size = new Size(360, 36);
            txtCajero.TabIndex = 2;
            //
            // lblCajeroCap
            //
            lblCajeroCap.BackColor = Color.Transparent;
            lblCajeroCap.Location = new Point(20, 64);
            lblCajeroCap.Name = "lblCajeroCap";
            lblCajeroCap.Size = new Size(200, 22);
            lblCajeroCap.TabIndex = 1;
            lblCajeroCap.Text = "Cajero";
            //
            // lblAbrirTitulo
            //
            lblAbrirTitulo.BackColor = Color.Transparent;
            lblAbrirTitulo.Location = new Point(20, 20);
            lblAbrirTitulo.Name = "lblAbrirTitulo";
            lblAbrirTitulo.Size = new Size(250, 30);
            lblAbrirTitulo.TabIndex = 0;
            lblAbrirTitulo.Text = "Abrir caja";
            //
            // pnlArqueo
            //
            pnlArqueo.Controls.Add(btnArqueo);
            pnlArqueo.Controls.Add(btnCerrar);
            pnlArqueo.Controls.Add(lblDiferenciaVal);
            pnlArqueo.Controls.Add(lblDiferenciaCap);
            pnlArqueo.Controls.Add(txtContado);
            pnlArqueo.Controls.Add(lblContadoCap);
            pnlArqueo.Controls.Add(lblEsperadoVal);
            pnlArqueo.Controls.Add(lblEsperadoCap);
            pnlArqueo.Controls.Add(lblArqueoTitulo);
            pnlArqueo.Location = new Point(451, 27);
            pnlArqueo.Name = "pnlArqueo";
            pnlArqueo.Size = new Size(400, 330);
            pnlArqueo.TabIndex = 1;
            //
            // btnArqueo
            //
            btnArqueo.Location = new Point(20, 274);
            btnArqueo.Name = "btnArqueo";
            btnArqueo.Size = new Size(360, 40);
            btnArqueo.TabIndex = 8;
            btnArqueo.Text = "Registrar arqueo";
            btnArqueo.Click += btnArqueo_Click;
            //
            // btnCerrar
            //
            btnCerrar.Location = new Point(20, 220);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(360, 46);
            btnCerrar.TabIndex = 7;
            btnCerrar.Text = "Cerrar caja";
            btnCerrar.Click += btnCerrar_Click;
            //
            // lblDiferenciaVal
            //
            lblDiferenciaVal.BackColor = Color.Transparent;
            lblDiferenciaVal.Location = new Point(220, 158);
            lblDiferenciaVal.Name = "lblDiferenciaVal";
            lblDiferenciaVal.Size = new Size(160, 24);
            lblDiferenciaVal.TabIndex = 6;
            lblDiferenciaVal.Text = "$0.00";
            lblDiferenciaVal.TextAlignment = ContentAlignment.MiddleRight;
            //
            // lblDiferenciaCap
            //
            lblDiferenciaCap.BackColor = Color.Transparent;
            lblDiferenciaCap.Location = new Point(20, 159);
            lblDiferenciaCap.Name = "lblDiferenciaCap";
            lblDiferenciaCap.Size = new Size(160, 22);
            lblDiferenciaCap.TabIndex = 5;
            lblDiferenciaCap.Text = "Diferencia";
            //
            // txtContado
            //
            txtContado.Location = new Point(220, 106);
            txtContado.Name = "txtContado";
            txtContado.PlaceholderText = "0.00";
            txtContado.Size = new Size(160, 36);
            txtContado.TabIndex = 4;
            txtContado.TextChanged += txtContado_TextChanged;
            //
            // lblContadoCap
            //
            lblContadoCap.BackColor = Color.Transparent;
            lblContadoCap.Location = new Point(20, 114);
            lblContadoCap.Name = "lblContadoCap";
            lblContadoCap.Size = new Size(180, 22);
            lblContadoCap.TabIndex = 3;
            lblContadoCap.Text = "Contado ($)";
            //
            // lblEsperadoVal
            //
            lblEsperadoVal.BackColor = Color.Transparent;
            lblEsperadoVal.Location = new Point(220, 66);
            lblEsperadoVal.Name = "lblEsperadoVal";
            lblEsperadoVal.Size = new Size(160, 24);
            lblEsperadoVal.TabIndex = 2;
            lblEsperadoVal.Text = "$0.00";
            lblEsperadoVal.TextAlignment = ContentAlignment.MiddleRight;
            //
            // lblEsperadoCap
            //
            lblEsperadoCap.BackColor = Color.Transparent;
            lblEsperadoCap.Location = new Point(20, 67);
            lblEsperadoCap.Name = "lblEsperadoCap";
            lblEsperadoCap.Size = new Size(160, 22);
            lblEsperadoCap.TabIndex = 1;
            lblEsperadoCap.Text = "Esperado";
            //
            // lblArqueoTitulo
            //
            lblArqueoTitulo.BackColor = Color.Transparent;
            lblArqueoTitulo.Location = new Point(20, 20);
            lblArqueoTitulo.Name = "lblArqueoTitulo";
            lblArqueoTitulo.Size = new Size(250, 30);
            lblArqueoTitulo.TabIndex = 0;
            lblArqueoTitulo.Text = "Arqueo de caja";
            //
            // pnlMovimientos
            //
            pnlMovimientos.Controls.Add(dgvMovimientos);
            pnlMovimientos.Controls.Add(btnNuevoMovimiento);
            pnlMovimientos.Controls.Add(lblMovTitulo);
            pnlMovimientos.Location = new Point(27, 363);
            pnlMovimientos.Margin = new Padding(3, 3, 3, 3);
            pnlMovimientos.Name = "pnlMovimientos";
            pnlMovimientos.Size = new Size(824, 260);
            pnlMovimientos.TabIndex = 2;
            //
            // lblMovTitulo
            //
            lblMovTitulo.BackColor = Color.Transparent;
            lblMovTitulo.Location = new Point(20, 18);
            lblMovTitulo.Name = "lblMovTitulo";
            lblMovTitulo.Size = new Size(300, 30);
            lblMovTitulo.TabIndex = 0;
            lblMovTitulo.Text = "Movimientos de caja";
            //
            // btnNuevoMovimiento
            //
            btnNuevoMovimiento.Location = new Point(574, 14);
            btnNuevoMovimiento.Name = "btnNuevoMovimiento";
            btnNuevoMovimiento.Size = new Size(230, 40);
            btnNuevoMovimiento.TabIndex = 1;
            btnNuevoMovimiento.Text = "+ Registrar gasto / ingreso";
            btnNuevoMovimiento.Click += btnNuevoMovimiento_Click;
            //
            // dgvMovimientos
            //
            dgvMovimientos.AllowUserToAddRows = false;
            dgvMovimientos.AllowUserToDeleteRows = false;
            dgvMovimientos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMovimientos.Location = new Point(20, 64);
            dgvMovimientos.Name = "dgvMovimientos";
            dgvMovimientos.ReadOnly = true;
            dgvMovimientos.RowHeadersVisible = false;
            dgvMovimientos.RowHeadersWidth = 51;
            dgvMovimientos.Size = new Size(784, 180);
            dgvMovimientos.TabIndex = 2;
            //
            // ucCaja
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(flpCajas);
            Name = "ucCaja";
            Size = new Size(900, 560);
            flpCajas.ResumeLayout(false);
            pnlAbrir.ResumeLayout(false);
            pnlArqueo.ResumeLayout(false);
            pnlMovimientos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvMovimientos).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flpCajas;
        private Guna.UI2.WinForms.Guna2Panel pnlAbrir;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblAbrirTitulo;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblCajeroCap;
        private Guna.UI2.WinForms.Guna2TextBox txtCajero;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblFondoCap;
        private Guna.UI2.WinForms.Guna2TextBox txtFondo;
        private Guna.UI2.WinForms.Guna2Button btnAbrir;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblEstado;
        private Guna.UI2.WinForms.Guna2Panel pnlArqueo;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblArqueoTitulo;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblEsperadoCap;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblEsperadoVal;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblContadoCap;
        private Guna.UI2.WinForms.Guna2TextBox txtContado;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblDiferenciaCap;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblDiferenciaVal;
        private Guna.UI2.WinForms.Guna2Button btnCerrar;
        private Guna.UI2.WinForms.Guna2Button btnArqueo;
        private Guna.UI2.WinForms.Guna2Panel pnlMovimientos;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblMovTitulo;
        private Guna.UI2.WinForms.Guna2Button btnNuevoMovimiento;
        private Guna.UI2.WinForms.Guna2DataGridView dgvMovimientos;
    }
}
