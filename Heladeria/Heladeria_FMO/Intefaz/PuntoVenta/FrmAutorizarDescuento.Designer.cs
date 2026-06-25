namespace Heladeria_FMO.Intefaz.PuntoVenta
{
    partial class FrmAutorizarDescuento
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
            lblUsuario = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtUsuario = new Guna.UI2.WinForms.Guna2TextBox();
            lblContra = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtContrasena = new Guna.UI2.WinForms.Guna2TextBox();
            lblTipo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            cboTipo = new Guna.UI2.WinForms.Guna2ComboBox();
            lblValor = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtValor = new Guna.UI2.WinForms.Guna2TextBox();
            btnAutorizar = new Guna.UI2.WinForms.Guna2Button();
            tarjeta.SuspendLayout();
            SuspendLayout();
            //
            // tarjeta
            //
            tarjeta.Controls.Add(titulo);
            tarjeta.Controls.Add(lblUsuario);
            tarjeta.Controls.Add(txtUsuario);
            tarjeta.Controls.Add(lblContra);
            tarjeta.Controls.Add(txtContrasena);
            tarjeta.Controls.Add(lblTipo);
            tarjeta.Controls.Add(cboTipo);
            tarjeta.Controls.Add(lblValor);
            tarjeta.Controls.Add(txtValor);
            tarjeta.Controls.Add(btnAutorizar);
            tarjeta.Location = new System.Drawing.Point(20, 20);
            tarjeta.Name = "tarjeta";
            tarjeta.Size = new System.Drawing.Size(380, 360);
            tarjeta.TabIndex = 0;
            //
            // titulo
            //
            titulo.BackColor = System.Drawing.Color.Transparent;
            titulo.Location = new System.Drawing.Point(20, 16);
            titulo.Name = "titulo";
            titulo.Size = new System.Drawing.Size(340, 24);
            titulo.TabIndex = 0;
            titulo.Text = "Descuento (requiere supervisor)";
            //
            // lblUsuario
            //
            lblUsuario.BackColor = System.Drawing.Color.Transparent;
            lblUsuario.Location = new System.Drawing.Point(20, 50);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new System.Drawing.Size(220, 20);
            lblUsuario.TabIndex = 1;
            lblUsuario.Text = "Usuario supervisor";
            //
            // txtUsuario
            //
            txtUsuario.Location = new System.Drawing.Point(20, 74);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.PlaceholderText = "Usuario";
            txtUsuario.Size = new System.Drawing.Size(340, 36);
            txtUsuario.TabIndex = 2;
            //
            // lblContra
            //
            lblContra.BackColor = System.Drawing.Color.Transparent;
            lblContra.Location = new System.Drawing.Point(20, 118);
            lblContra.Name = "lblContra";
            lblContra.Size = new System.Drawing.Size(220, 20);
            lblContra.TabIndex = 3;
            lblContra.Text = "Contraseña";
            //
            // txtContrasena
            //
            txtContrasena.Location = new System.Drawing.Point(20, 142);
            txtContrasena.Name = "txtContrasena";
            txtContrasena.PasswordChar = '*';
            txtContrasena.PlaceholderText = "Contraseña";
            txtContrasena.Size = new System.Drawing.Size(340, 36);
            txtContrasena.TabIndex = 4;
            //
            // lblTipo
            //
            lblTipo.BackColor = System.Drawing.Color.Transparent;
            lblTipo.Location = new System.Drawing.Point(20, 186);
            lblTipo.Name = "lblTipo";
            lblTipo.Size = new System.Drawing.Size(220, 20);
            lblTipo.TabIndex = 5;
            lblTipo.Text = "Tipo de descuento";
            //
            // cboTipo
            //
            cboTipo.Location = new System.Drawing.Point(20, 210);
            cboTipo.Name = "cboTipo";
            cboTipo.Size = new System.Drawing.Size(340, 36);
            cboTipo.TabIndex = 6;
            cboTipo.Items.AddRange(new object[] { "Monto fijo ($)", "Porcentaje (%)" });
            //
            // lblValor
            //
            lblValor.BackColor = System.Drawing.Color.Transparent;
            lblValor.Location = new System.Drawing.Point(20, 252);
            lblValor.Name = "lblValor";
            lblValor.Size = new System.Drawing.Size(220, 20);
            lblValor.TabIndex = 7;
            lblValor.Text = "Valor";
            //
            // txtValor
            //
            txtValor.Location = new System.Drawing.Point(20, 276);
            txtValor.Name = "txtValor";
            txtValor.PlaceholderText = "0.00";
            txtValor.Size = new System.Drawing.Size(340, 36);
            txtValor.TabIndex = 8;
            //
            // btnAutorizar
            //
            btnAutorizar.Location = new System.Drawing.Point(20, 320);
            btnAutorizar.Name = "btnAutorizar";
            btnAutorizar.Size = new System.Drawing.Size(340, 40);
            btnAutorizar.TabIndex = 9;
            btnAutorizar.Text = "Autorizar";
            btnAutorizar.Click += BtnAutorizar_Click;
            //
            // FrmAutorizarDescuento
            //
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(420, 400);
            Controls.Add(tarjeta);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmAutorizarDescuento";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Autorizar descuento";
            tarjeta.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel tarjeta;
        private Guna.UI2.WinForms.Guna2HtmlLabel titulo;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblUsuario;
        private Guna.UI2.WinForms.Guna2TextBox txtUsuario;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblContra;
        private Guna.UI2.WinForms.Guna2TextBox txtContrasena;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTipo;
        private Guna.UI2.WinForms.Guna2ComboBox cboTipo;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblValor;
        private Guna.UI2.WinForms.Guna2TextBox txtValor;
        private Guna.UI2.WinForms.Guna2Button btnAutorizar;
    }
}
