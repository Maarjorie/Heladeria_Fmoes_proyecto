namespace Heladeria_FMO.Intefaz.Reportes
{
    partial class ucReportes
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        private void InitializeComponent()
        {
            pnlCard = new Guna.UI2.WinForms.Guna2Panel();
            dgvReporte = new Guna.UI2.WinForms.Guna2DataGridView();
            pnlHeader = new Guna.UI2.WinForms.Guna2Panel();
            btnGenerar = new Guna.UI2.WinForms.Guna2Button();
            dtpFin = new Guna.UI2.WinForms.Guna2DateTimePicker();
            dtpInicio = new Guna.UI2.WinForms.Guna2DateTimePicker();
            cboReporte = new Guna.UI2.WinForms.Guna2ComboBox();
            lblHasta = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblDesde = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblTitulo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReporte).BeginInit();
            pnlHeader.SuspendLayout();
            SuspendLayout();
            //
            // pnlCard
            //
            pnlCard.Controls.Add(dgvReporte);
            pnlCard.Dock = DockStyle.Fill;
            pnlCard.Location = new Point(20, 116);
            pnlCard.Name = "pnlCard";
            pnlCard.Padding = new Padding(2);
            pnlCard.Size = new Size(1080, 494);
            pnlCard.TabIndex = 1;
            //
            // dgvReporte
            //
            dgvReporte.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReporte.Dock = DockStyle.Fill;
            dgvReporte.Location = new Point(2, 2);
            dgvReporte.Name = "dgvReporte";
            dgvReporte.RowHeadersVisible = false;
            dgvReporte.Size = new Size(1076, 490);
            dgvReporte.TabIndex = 0;
            //
            // pnlHeader
            //
            pnlHeader.Controls.Add(btnGenerar);
            pnlHeader.Controls.Add(dtpFin);
            pnlHeader.Controls.Add(dtpInicio);
            pnlHeader.Controls.Add(cboReporte);
            pnlHeader.Controls.Add(lblHasta);
            pnlHeader.Controls.Add(lblDesde);
            pnlHeader.Controls.Add(lblTitulo);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(20, 20);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1080, 96);
            pnlHeader.TabIndex = 0;
            //
            // btnGenerar
            //
            btnGenerar.Location = new Point(700, 54);
            btnGenerar.Name = "btnGenerar";
            btnGenerar.Size = new Size(150, 40);
            btnGenerar.TabIndex = 6;
            btnGenerar.Text = "Generar";
            btnGenerar.Click += btnGenerar_Click;
            //
            // dtpFin
            //
            dtpFin.Checked = true;
            dtpFin.Format = DateTimePickerFormat.Short;
            dtpFin.Location = new Point(530, 56);
            dtpFin.Name = "dtpFin";
            dtpFin.Size = new Size(150, 36);
            dtpFin.TabIndex = 5;
            dtpFin.Value = new DateTime(2026, 6, 23, 0, 0, 0, 0);
            //
            // dtpInicio
            //
            dtpInicio.Checked = true;
            dtpInicio.Format = DateTimePickerFormat.Short;
            dtpInicio.Location = new Point(360, 56);
            dtpInicio.Name = "dtpInicio";
            dtpInicio.Size = new Size(150, 36);
            dtpInicio.TabIndex = 4;
            dtpInicio.Value = new DateTime(2026, 6, 1, 0, 0, 0, 0);
            //
            // cboReporte
            //
            cboReporte.BackColor = Color.Transparent;
            cboReporte.DrawMode = DrawMode.OwnerDrawFixed;
            cboReporte.DropDownStyle = ComboBoxStyle.DropDownList;
            cboReporte.FocusedColor = Color.FromArgb(94, 148, 255);
            cboReporte.Font = new Font("Segoe UI", 10F);
            cboReporte.ItemHeight = 30;
            cboReporte.Location = new Point(4, 56);
            cboReporte.Name = "cboReporte";
            cboReporte.Size = new Size(330, 36);
            cboReporte.TabIndex = 3;
            //
            // lblHasta
            //
            lblHasta.BackColor = Color.Transparent;
            lblHasta.Location = new Point(530, 34);
            lblHasta.Name = "lblHasta";
            lblHasta.Size = new Size(60, 18);
            lblHasta.TabIndex = 2;
            lblHasta.Text = "Hasta";
            //
            // lblDesde
            //
            lblDesde.BackColor = Color.Transparent;
            lblDesde.Location = new Point(360, 34);
            lblDesde.Name = "lblDesde";
            lblDesde.Size = new Size(120, 18);
            lblDesde.TabIndex = 1;
            lblDesde.Text = "Desde";
            //
            // lblTitulo
            //
            lblTitulo.BackColor = Color.Transparent;
            lblTitulo.Location = new Point(4, 8);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(300, 30);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Reportes";
            //
            // ucReportes
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlCard);
            Controls.Add(pnlHeader);
            Padding = new Padding(20);
            Name = "ucReportes";
            Size = new Size(1120, 630);
            pnlCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvReporte).EndInit();
            pnlHeader.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlHeader;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitulo;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblDesde;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblHasta;
        private Guna.UI2.WinForms.Guna2ComboBox cboReporte;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpInicio;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpFin;
        private Guna.UI2.WinForms.Guna2Button btnGenerar;
        private Guna.UI2.WinForms.Guna2Panel pnlCard;
        private Guna.UI2.WinForms.Guna2DataGridView dgvReporte;
    }
}
