namespace Heladeria_FMO.Intefaz
{
    partial class FrmRecuperarCredenciales
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
            txtCorreo = new Guna.UI2.WinForms.Guna2TextBox();
            btnEnviar = new Guna.UI2.WinForms.Guna2Button();
            btnCancelar = new Guna.UI2.WinForms.Guna2Button();
            tarjeta.SuspendLayout();
            SuspendLayout();
            //
            // tarjeta
            //
            tarjeta.Controls.Add(titulo);
            tarjeta.Controls.Add(instrucciones);
            tarjeta.Controls.Add(txtCorreo);
            tarjeta.Controls.Add(btnEnviar);
            tarjeta.Controls.Add(btnCancelar);
            tarjeta.Location = new System.Drawing.Point(20, 20);
            tarjeta.Name = "tarjeta";
            tarjeta.Size = new System.Drawing.Size(380, 256);
            tarjeta.TabIndex = 0;
            //
            // titulo
            //
            titulo.BackColor = System.Drawing.Color.Transparent;
            titulo.Location = new System.Drawing.Point(20, 18);
            titulo.Name = "titulo";
            titulo.Size = new System.Drawing.Size(340, 26);
            titulo.TabIndex = 0;
            titulo.Text = "¿Olvidaste tus credenciales?";
            //
            // instrucciones
            //
            instrucciones.BackColor = System.Drawing.Color.Transparent;
            instrucciones.Location = new System.Drawing.Point(20, 48);
            instrucciones.Name = "instrucciones";
            instrucciones.Size = new System.Drawing.Size(340, 40);
            instrucciones.TabIndex = 1;
            instrucciones.Text = "Ingresa el correo registrado en tu cuenta. Te enviaremos\r\nuna contraseña temporal para que vuelvas a entrar.";
            //
            // txtCorreo
            //
            txtCorreo.Location = new System.Drawing.Point(20, 96);
            txtCorreo.Name = "txtCorreo";
            txtCorreo.PlaceholderText = "correo@ejemplo.com";
            txtCorreo.Size = new System.Drawing.Size(340, 40);
            txtCorreo.TabIndex = 2;
            //
            // btnEnviar
            //
            btnEnviar.Location = new System.Drawing.Point(20, 150);
            btnEnviar.Name = "btnEnviar";
            btnEnviar.Size = new System.Drawing.Size(340, 42);
            btnEnviar.TabIndex = 3;
            btnEnviar.Text = "Enviar contraseña temporal";
            btnEnviar.Click += BtnEnviar_Click;
            //
            // btnCancelar
            //
            btnCancelar.Location = new System.Drawing.Point(20, 198);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new System.Drawing.Size(340, 36);
            btnCancelar.TabIndex = 4;
            btnCancelar.Text = "Cancelar";
            btnCancelar.Click += BtnCancelar_Click;
            //
            // FrmRecuperarCredenciales
            //
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(420, 296);
            Controls.Add(tarjeta);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmRecuperarCredenciales";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Recuperar credenciales";
            tarjeta.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel tarjeta;
        private Guna.UI2.WinForms.Guna2HtmlLabel titulo;
        private Guna.UI2.WinForms.Guna2HtmlLabel instrucciones;
        private Guna.UI2.WinForms.Guna2TextBox txtCorreo;
        private Guna.UI2.WinForms.Guna2Button btnEnviar;
        private Guna.UI2.WinForms.Guna2Button btnCancelar;
    }
}
