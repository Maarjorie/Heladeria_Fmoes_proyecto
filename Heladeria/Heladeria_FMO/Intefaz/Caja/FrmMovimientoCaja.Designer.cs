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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
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
            tarjeta.CustomizableEdges = customizableEdges9;
            tarjeta.Location = new Point(20, 20);
            tarjeta.Name = "tarjeta";
            tarjeta.ShadowDecoration.CustomizableEdges = customizableEdges10;
            tarjeta.Size = new Size(380, 320);
            tarjeta.TabIndex = 0;
            // 
            // titulo
            // 
            titulo.BackColor = Color.Transparent;
            titulo.Location = new Point(20, 18);
            titulo.Name = "titulo";
            titulo.Size = new Size(117, 17);
            titulo.TabIndex = 0;
            titulo.Text = "Registrar movimiento";
            // 
            // lblTipo
            // 
            lblTipo.BackColor = Color.Transparent;
            lblTipo.Location = new Point(20, 56);
            lblTipo.Name = "lblTipo";
            lblTipo.Size = new Size(27, 17);
            lblTipo.TabIndex = 1;
            lblTipo.Text = "Tipo";
            // 
            // cboTipo
            // 
            cboTipo.BackColor = Color.Transparent;
            cboTipo.CustomizableEdges = customizableEdges1;
            cboTipo.DrawMode = DrawMode.OwnerDrawFixed;
            cboTipo.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTipo.FocusedColor = Color.Empty;
            cboTipo.Font = new Font("Segoe UI", 10F);
            cboTipo.ForeColor = Color.FromArgb(68, 88, 112);
            cboTipo.ItemHeight = 30;
            cboTipo.Items.AddRange(new object[] { "Ingreso", "Egreso" });
            cboTipo.Location = new Point(20, 80);
            cboTipo.Name = "cboTipo";
            cboTipo.ShadowDecoration.CustomizableEdges = customizableEdges2;
            cboTipo.Size = new Size(340, 36);
            cboTipo.TabIndex = 2;
            // 
            // lblConcepto
            // 
            lblConcepto.BackColor = Color.Transparent;
            lblConcepto.Location = new Point(20, 124);
            lblConcepto.Name = "lblConcepto";
            lblConcepto.Size = new Size(55, 17);
            lblConcepto.TabIndex = 3;
            lblConcepto.Text = "Concepto";
            // 
            // txtConcepto
            // 
            txtConcepto.CustomizableEdges = customizableEdges3;
            txtConcepto.DefaultText = "";
            txtConcepto.Font = new Font("Segoe UI", 9F);
            txtConcepto.Location = new Point(20, 148);
            txtConcepto.Name = "txtConcepto";
            txtConcepto.PlaceholderText = "Ej. compra de hielo";
            txtConcepto.SelectedText = "";
            txtConcepto.ShadowDecoration.CustomizableEdges = customizableEdges4;
            txtConcepto.Size = new Size(340, 36);
            txtConcepto.TabIndex = 4;
            // 
            // lblMonto
            // 
            lblMonto.BackColor = Color.Transparent;
            lblMonto.Location = new Point(20, 192);
            lblMonto.Name = "lblMonto";
            lblMonto.Size = new Size(56, 17);
            lblMonto.TabIndex = 5;
            lblMonto.Text = "Monto ($)";
            // 
            // txtMonto
            // 
            txtMonto.CustomizableEdges = customizableEdges5;
            txtMonto.DefaultText = "";
            txtMonto.Font = new Font("Segoe UI", 9F);
            txtMonto.Location = new Point(20, 216);
            txtMonto.Name = "txtMonto";
            txtMonto.PlaceholderText = "0.00";
            txtMonto.SelectedText = "";
            txtMonto.ShadowDecoration.CustomizableEdges = customizableEdges6;
            txtMonto.Size = new Size(340, 36);
            txtMonto.TabIndex = 6;
            txtMonto.TextChanged += txtMonto_TextChanged;
            txtMonto.KeyPress += txtMonto_KeyPress;
            // 
            // btnGuardar
            // 
            btnGuardar.CustomizableEdges = customizableEdges7;
            btnGuardar.Font = new Font("Segoe UI", 9F);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(20, 268);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnGuardar.Size = new Size(340, 40);
            btnGuardar.TabIndex = 7;
            btnGuardar.Text = "Guardar";
            btnGuardar.Click += BtnGuardar_Click;
            // 
            // FrmMovimientoCaja
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(420, 360);
            Controls.Add(tarjeta);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmMovimientoCaja";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Movimiento de caja";
            tarjeta.ResumeLayout(false);
            tarjeta.PerformLayout();
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
