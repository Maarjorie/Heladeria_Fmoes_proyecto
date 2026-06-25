namespace Heladeria_FMO.Intefaz.Inventario
{
    partial class FrmPromociones
    {
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
            marco = new Guna.UI2.WinForms.Guna2Panel();
            titulo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            btnCerrar = new Guna.UI2.WinForms.Guna2Button();
            dgv = new Guna.UI2.WinForms.Guna2DataGridView();
            lblNombre = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtNombre = new Guna.UI2.WinForms.Guna2TextBox();
            lblAplicar = new Guna.UI2.WinForms.Guna2HtmlLabel();
            cboAplicar = new Guna.UI2.WinForms.Guna2ComboBox();
            lblTipo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            cboTipo = new Guna.UI2.WinForms.Guna2ComboBox();
            lblElemento = new Guna.UI2.WinForms.Guna2HtmlLabel();
            cboElemento = new Guna.UI2.WinForms.Guna2ComboBox();
            lblValor = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtValor = new Guna.UI2.WinForms.Guna2TextBox();
            lblDesde = new Guna.UI2.WinForms.Guna2HtmlLabel();
            dtpDesde = new Guna.UI2.WinForms.Guna2DateTimePicker();
            lblHasta = new Guna.UI2.WinForms.Guna2HtmlLabel();
            dtpHasta = new Guna.UI2.WinForms.Guna2DateTimePicker();
            lblDesc = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtDescripcion = new Guna.UI2.WinForms.Guna2TextBox();
            info = new Guna.UI2.WinForms.Guna2HtmlLabel();
            btnGuardar = new Guna.UI2.WinForms.Guna2Button();
            marco.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgv).BeginInit();
            SuspendLayout();
            //
            // marco
            //
            marco.Controls.Add(titulo);
            marco.Controls.Add(btnCerrar);
            marco.Controls.Add(dgv);
            marco.Controls.Add(lblNombre);
            marco.Controls.Add(txtNombre);
            marco.Controls.Add(lblAplicar);
            marco.Controls.Add(cboAplicar);
            marco.Controls.Add(lblTipo);
            marco.Controls.Add(cboTipo);
            marco.Controls.Add(lblElemento);
            marco.Controls.Add(cboElemento);
            marco.Controls.Add(lblValor);
            marco.Controls.Add(txtValor);
            marco.Controls.Add(lblDesde);
            marco.Controls.Add(dtpDesde);
            marco.Controls.Add(lblHasta);
            marco.Controls.Add(dtpHasta);
            marco.Controls.Add(lblDesc);
            marco.Controls.Add(txtDescripcion);
            marco.Controls.Add(info);
            marco.Controls.Add(btnGuardar);
            marco.Dock = System.Windows.Forms.DockStyle.Fill;
            marco.Location = new System.Drawing.Point(0, 0);
            marco.Name = "marco";
            marco.Size = new System.Drawing.Size(900, 548);
            marco.TabIndex = 0;
            //
            // titulo
            //
            titulo.BackColor = System.Drawing.Color.Transparent;
            titulo.Location = new System.Drawing.Point(24, 18);
            titulo.Name = "titulo";
            titulo.Size = new System.Drawing.Size(300, 30);
            titulo.TabIndex = 0;
            titulo.Text = "Promociones";
            //
            // btnCerrar
            //
            btnCerrar.Location = new System.Drawing.Point(844, 14);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new System.Drawing.Size(32, 32);
            btnCerrar.TabIndex = 1;
            btnCerrar.Text = "✕";
            btnCerrar.Click += BtnCerrar_Click;
            //
            // dgv
            //
            dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv.Location = new System.Drawing.Point(24, 60);
            dgv.Name = "dgv";
            dgv.RowHeadersVisible = false;
            dgv.Size = new System.Drawing.Size(430, 460);
            dgv.TabIndex = 2;
            //
            // lblNombre
            //
            lblNombre.BackColor = System.Drawing.Color.Transparent;
            lblNombre.Location = new System.Drawing.Point(482, 60);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new System.Drawing.Size(190, 20);
            lblNombre.TabIndex = 3;
            lblNombre.Text = "Nombre";
            //
            // txtNombre
            //
            txtNombre.Location = new System.Drawing.Point(482, 82);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new System.Drawing.Size(388, 34);
            txtNombre.TabIndex = 4;
            //
            // lblAplicar
            //
            lblAplicar.BackColor = System.Drawing.Color.Transparent;
            lblAplicar.Location = new System.Drawing.Point(482, 126);
            lblAplicar.Name = "lblAplicar";
            lblAplicar.Size = new System.Drawing.Size(190, 20);
            lblAplicar.TabIndex = 5;
            lblAplicar.Text = "Aplicar a";
            //
            // cboAplicar
            //
            cboAplicar.Location = new System.Drawing.Point(482, 148);
            cboAplicar.Name = "cboAplicar";
            cboAplicar.Size = new System.Drawing.Size(185, 34);
            cboAplicar.TabIndex = 6;
            cboAplicar.Items.AddRange(new object[] { "Producto", "Categoría" });
            cboAplicar.SelectedIndexChanged += cboAplicar_SelectedIndexChanged;
            //
            // lblTipo
            //
            lblTipo.BackColor = System.Drawing.Color.Transparent;
            lblTipo.Location = new System.Drawing.Point(685, 126);
            lblTipo.Name = "lblTipo";
            lblTipo.Size = new System.Drawing.Size(190, 20);
            lblTipo.TabIndex = 7;
            lblTipo.Text = "Tipo de descuento";
            //
            // cboTipo
            //
            cboTipo.Location = new System.Drawing.Point(685, 148);
            cboTipo.Name = "cboTipo";
            cboTipo.Size = new System.Drawing.Size(185, 34);
            cboTipo.TabIndex = 8;
            cboTipo.Items.AddRange(new object[] { "Porcentaje (%)", "Monto fijo ($)" });
            //
            // lblElemento
            //
            lblElemento.BackColor = System.Drawing.Color.Transparent;
            lblElemento.Location = new System.Drawing.Point(482, 192);
            lblElemento.Name = "lblElemento";
            lblElemento.Size = new System.Drawing.Size(190, 20);
            lblElemento.TabIndex = 9;
            lblElemento.Text = "Producto / categoría";
            //
            // cboElemento
            //
            cboElemento.Location = new System.Drawing.Point(482, 214);
            cboElemento.Name = "cboElemento";
            cboElemento.Size = new System.Drawing.Size(388, 34);
            cboElemento.TabIndex = 10;
            //
            // lblValor
            //
            lblValor.BackColor = System.Drawing.Color.Transparent;
            lblValor.Location = new System.Drawing.Point(482, 258);
            lblValor.Name = "lblValor";
            lblValor.Size = new System.Drawing.Size(190, 20);
            lblValor.TabIndex = 11;
            lblValor.Text = "Valor del descuento";
            //
            // txtValor
            //
            txtValor.Location = new System.Drawing.Point(482, 280);
            txtValor.Name = "txtValor";
            txtValor.PlaceholderText = "10";
            txtValor.Size = new System.Drawing.Size(185, 34);
            txtValor.TabIndex = 12;
            //
            // lblDesde
            //
            lblDesde.BackColor = System.Drawing.Color.Transparent;
            lblDesde.Location = new System.Drawing.Point(482, 324);
            lblDesde.Name = "lblDesde";
            lblDesde.Size = new System.Drawing.Size(190, 20);
            lblDesde.TabIndex = 13;
            lblDesde.Text = "Desde";
            //
            // dtpDesde
            //
            dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            dtpDesde.Location = new System.Drawing.Point(482, 346);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new System.Drawing.Size(185, 34);
            dtpDesde.TabIndex = 14;
            //
            // lblHasta
            //
            lblHasta.BackColor = System.Drawing.Color.Transparent;
            lblHasta.Location = new System.Drawing.Point(685, 324);
            lblHasta.Name = "lblHasta";
            lblHasta.Size = new System.Drawing.Size(190, 20);
            lblHasta.TabIndex = 15;
            lblHasta.Text = "Hasta";
            //
            // dtpHasta
            //
            dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            dtpHasta.Location = new System.Drawing.Point(685, 346);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new System.Drawing.Size(185, 34);
            dtpHasta.TabIndex = 16;
            //
            // lblDesc
            //
            lblDesc.BackColor = System.Drawing.Color.Transparent;
            lblDesc.Location = new System.Drawing.Point(482, 390);
            lblDesc.Name = "lblDesc";
            lblDesc.Size = new System.Drawing.Size(190, 20);
            lblDesc.TabIndex = 17;
            lblDesc.Text = "Descripción";
            //
            // txtDescripcion
            //
            txtDescripcion.Location = new System.Drawing.Point(482, 412);
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new System.Drawing.Size(388, 34);
            txtDescripcion.TabIndex = 18;
            //
            // info
            //
            info.BackColor = System.Drawing.Color.Transparent;
            info.Location = new System.Drawing.Point(482, 452);
            info.Name = "info";
            info.Size = new System.Drawing.Size(388, 18);
            info.TabIndex = 19;
            info.Text = "Las promociones nuevas quedan pendientes de aprobación.";
            //
            // btnGuardar
            //
            btnGuardar.Location = new System.Drawing.Point(482, 478);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new System.Drawing.Size(388, 42);
            btnGuardar.TabIndex = 20;
            btnGuardar.Text = "Crear promoción";
            btnGuardar.Click += btnGuardar_Click;
            //
            // FrmPromociones
            //
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(900, 548);
            Controls.Add(marco);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "FrmPromociones";
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            marco.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgv).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel marco;
        private Guna.UI2.WinForms.Guna2HtmlLabel titulo;
        private Guna.UI2.WinForms.Guna2Button btnCerrar;
        private Guna.UI2.WinForms.Guna2DataGridView dgv;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblNombre;
        private Guna.UI2.WinForms.Guna2TextBox txtNombre;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblAplicar;
        private Guna.UI2.WinForms.Guna2ComboBox cboAplicar;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTipo;
        private Guna.UI2.WinForms.Guna2ComboBox cboTipo;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblElemento;
        private Guna.UI2.WinForms.Guna2ComboBox cboElemento;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblValor;
        private Guna.UI2.WinForms.Guna2TextBox txtValor;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblDesde;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpDesde;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblHasta;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpHasta;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblDesc;
        private Guna.UI2.WinForms.Guna2TextBox txtDescripcion;
        private Guna.UI2.WinForms.Guna2HtmlLabel info;
        private Guna.UI2.WinForms.Guna2Button btnGuardar;
    }
}
