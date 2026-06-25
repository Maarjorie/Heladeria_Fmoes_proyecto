namespace Heladeria_FMO.Intefaz
{
    partial class FrmEstablecerContrasena
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
            tarjeta = new Guna.UI2.WinForms.Guna2Panel();
            titulo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            instrucciones = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtNueva = new Guna.UI2.WinForms.Guna2TextBox();
            txtConfirmar = new Guna.UI2.WinForms.Guna2TextBox();
            btnGuardar = new Guna.UI2.WinForms.Guna2Button();
            tarjeta.SuspendLayout();
            SuspendLayout();
            //
            // tarjeta
            //
            tarjeta.Controls.Add(titulo);
            tarjeta.Controls.Add(instrucciones);
            tarjeta.Controls.Add(txtNueva);
            tarjeta.Controls.Add(txtConfirmar);
            tarjeta.Controls.Add(btnGuardar);
            tarjeta.Location = new System.Drawing.Point(20, 20);
            tarjeta.Name = "tarjeta";
            tarjeta.Size = new System.Drawing.Size(380, 240);
            tarjeta.TabIndex = 0;
            //
            // titulo
            //
            titulo.BackColor = System.Drawing.Color.Transparent;
            titulo.Location = new System.Drawing.Point(20, 18);
            titulo.Name = "titulo";
            titulo.Size = new System.Drawing.Size(340, 26);
            titulo.TabIndex = 0;
            titulo.Text = "Crea tu nueva contraseña";
            //
            // instrucciones
            //
            instrucciones.BackColor = System.Drawing.Color.Transparent;
            instrucciones.Location = new System.Drawing.Point(20, 48);
            instrucciones.Name = "instrucciones";
            instrucciones.Size = new System.Drawing.Size(340, 40);
            instrucciones.TabIndex = 1;
            instrucciones.Text = "Ingresaste con una contraseña temporal. Define una\r\ncontraseña nueva para tu cuenta.";
            //
            // txtNueva
            //
            txtNueva.Location = new System.Drawing.Point(20, 96);
            txtNueva.Name = "txtNueva";
            txtNueva.PasswordChar = '*';
            txtNueva.PlaceholderText = "Nueva contraseña";
            txtNueva.Size = new System.Drawing.Size(340, 40);
            txtNueva.TabIndex = 2;
            //
            // txtConfirmar
            //
            txtConfirmar.Location = new System.Drawing.Point(20, 146);
            txtConfirmar.Name = "txtConfirmar";
            txtConfirmar.PasswordChar = '*';
            txtConfirmar.PlaceholderText = "Confirmar contraseña";
            txtConfirmar.Size = new System.Drawing.Size(340, 40);
            txtConfirmar.TabIndex = 3;
            //
            // btnGuardar
            //
            btnGuardar.Location = new System.Drawing.Point(20, 200);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new System.Drawing.Size(340, 42);
            btnGuardar.TabIndex = 4;
            btnGuardar.Text = "Guardar y continuar";
            btnGuardar.Click += BtnGuardar_Click;
            //
            // FrmEstablecerContrasena
            //
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(420, 280);
            Controls.Add(tarjeta);
            ControlBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmEstablecerContrasena";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Crear nueva contraseña";
            tarjeta.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel tarjeta;
        private Guna.UI2.WinForms.Guna2HtmlLabel titulo;
        private Guna.UI2.WinForms.Guna2HtmlLabel instrucciones;
        private Guna.UI2.WinForms.Guna2TextBox txtNueva;
        private Guna.UI2.WinForms.Guna2TextBox txtConfirmar;
        private Guna.UI2.WinForms.Guna2Button btnGuardar;
    }
}
