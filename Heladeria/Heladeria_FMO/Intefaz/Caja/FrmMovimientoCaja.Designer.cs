namespace Heladeria_FMO.Intefaz.Caja
{
    partial class FrmMovimientoCaja
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
            tarjeta = new Guna.UI2.WinForms.Guna2Panel();
            titulo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblTipo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            cboTipo = new Guna.UI2.WinForms.Guna2ComboBox();
            lblConcepto = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtConcepto = new Guna.UI2.WinForms.Guna2TextBox();
            lblMonto = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtMonto = new Guna.UI2.WinForms.Guna2TextBox();
            btnGuardar = new Guna.UI2.WinForms.Guna2Button();
            tarjeta.SuspendLayout();
            SuspendLayout();
            //
            // tarjeta
            //
            tarjeta.Controls.Add(titulo);
            tarjeta.Controls.Add(lblTipo);
            tarjeta.Controls.Add(cboTipo);
            tarjeta.Controls.Add(lblConcepto);
            tarjeta.Controls.Add(txtConcepto);
            tarjeta.Controls.Add(lblMonto);
            tarjeta.Controls.Add(txtMonto);
            tarjeta.Controls.Add(btnGuardar);
            tarjeta.Location = new System.Drawing.Point(20, 20);
            tarjeta.Name = "tarjeta";
            tarjeta.Size = new System.Drawing.Size(380, 320);
            tarjeta.TabIndex = 0;
            //
            // titulo
            //
            titulo.BackColor = System.Drawing.Color.Transparent;
            titulo.Location = new System.Drawing.Point(20, 18);
            titulo.Name = "titulo";
            titulo.Size = new System.Drawing.Size(340, 26);
            titulo.TabIndex = 0;
            titulo.Text = "Registrar movimiento";
            //
            // lblTipo
            //
            lblTipo.BackColor = System.Drawing.Color.Transparent;
            lblTipo.Location = new System.Drawing.Point(20, 56);
            lblTipo.Name = "lblTipo";
            lblTipo.Size = new System.Drawing.Size(200, 20);
            lblTipo.TabIndex = 1;
            lblTipo.Text = "Tipo";
            //
            // cboTipo
            //
            cboTipo.Location = new System.Drawing.Point(20, 80);
            cboTipo.Name = "cboTipo";
            cboTipo.Size = new System.Drawing.Size(340, 36);
            cboTipo.TabIndex = 2;
            cboTipo.Items.AddRange(new object[] { "Ingreso", "Egreso" });
            //
            // lblConcepto
            //
            lblConcepto.BackColor = System.Drawing.Color.Transparent;
            lblConcepto.Location = new System.Drawing.Point(20, 124);
            lblConcepto.Name = "lblConcepto";
            lblConcepto.Size = new System.Drawing.Size(200, 20);
            lblConcepto.TabIndex = 3;
            lblConcepto.Text = "Concepto";
            //
            // txtConcepto
            //
            txtConcepto.Location = new System.Drawing.Point(20, 148);
            txtConcepto.Name = "txtConcepto";
            txtConcepto.PlaceholderText = "Ej. compra de hielo";
            txtConcepto.Size = new System.Drawing.Size(340, 36);
            txtConcepto.TabIndex = 4;
            //
            // lblMonto
            //
            lblMonto.BackColor = System.Drawing.Color.Transparent;
            lblMonto.Location = new System.Drawing.Point(20, 192);
            lblMonto.Name = "lblMonto";
            lblMonto.Size = new System.Drawing.Size(200, 20);
            lblMonto.TabIndex = 5;
            lblMonto.Text = "Monto ($)";
            //
            // txtMonto
            //
            txtMonto.Location = new System.Drawing.Point(20, 216);
            txtMonto.Name = "txtMonto";
            txtMonto.PlaceholderText = "0.00";
            txtMonto.Size = new System.Drawing.Size(340, 36);
            txtMonto.TabIndex = 6;
            //
            // btnGuardar
            //
            btnGuardar.Location = new System.Drawing.Point(20, 268);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new System.Drawing.Size(340, 40);
            btnGuardar.TabIndex = 7;
            btnGuardar.Text = "Guardar";
            btnGuardar.Click += BtnGuardar_Click;
            //
            // FrmMovimientoCaja
            //
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(420, 360);
            Controls.Add(tarjeta);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmMovimientoCaja";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Movimiento de caja";
            tarjeta.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel tarjeta;
        private Guna.UI2.WinForms.Guna2HtmlLabel titulo;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTipo;
        private Guna.UI2.WinForms.Guna2ComboBox cboTipo;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblConcepto;
        private Guna.UI2.WinForms.Guna2TextBox txtConcepto;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblMonto;
        private Guna.UI2.WinForms.Guna2TextBox txtMonto;
        private Guna.UI2.WinForms.Guna2Button btnGuardar;
    }
}
