namespace Heladeria_FMO
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblTitulo = new Label();
            lblSubtitulo = new Label();
            label3 = new Label();
            label4 = new Label();
            txtUsuario = new TextBox();
            txtContrasena = new TextBox();
            btnIniciarSesion = new Button();
            panelLogin = new Panel();
            btnSalir = new Button();
            label1 = new Label();
            panelLogin.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.BackColor = Color.Transparent;
            lblTitulo.Font = new Font("Constantia", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTitulo.Location = new Point(431, 78);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(345, 49);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "HELADERIA FMO";
            // 
            // lblSubtitulo
            // 
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.Font = new Font("Georgia", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSubtitulo.Location = new Point(395, 145);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(433, 35);
            lblSubtitulo.TabIndex = 1;
            lblSubtitulo.Text = "Sistema de gestión de heladeria";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(62, 24);
            label3.Name = "label3";
            label3.Size = new Size(59, 20);
            label3.TabIndex = 2;
            label3.Text = "Usuario";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(50, 61);
            label4.Name = "label4";
            label4.Size = new Size(83, 20);
            label4.TabIndex = 3;
            label4.Text = "Contraseña";
            // 
            // txtUsuario
            // 
            txtUsuario.Location = new Point(162, 17);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(196, 27);
            txtUsuario.TabIndex = 4;
            // 
            // txtContrasena
            // 
            txtContrasena.Location = new Point(162, 60);
            txtContrasena.Name = "txtContrasena";
            txtContrasena.Size = new Size(196, 27);
            txtContrasena.TabIndex = 5;
            // 
            // btnIniciarSesion
            // 
            btnIniciarSesion.Location = new Point(131, 105);
            btnIniciarSesion.Name = "btnIniciarSesion";
            btnIniciarSesion.Size = new Size(213, 37);
            btnIniciarSesion.TabIndex = 6;
            btnIniciarSesion.Text = "Iniciar sesión";
            btnIniciarSesion.UseVisualStyleBackColor = true;
            // 
            // panelLogin
            // 
            panelLogin.Controls.Add(btnIniciarSesion);
            panelLogin.Controls.Add(txtContrasena);
            panelLogin.Controls.Add(label4);
            panelLogin.Controls.Add(btnSalir);
            panelLogin.Controls.Add(txtUsuario);
            panelLogin.Controls.Add(label3);
            panelLogin.Location = new Point(369, 290);
            panelLogin.Name = "panelLogin";
            panelLogin.Size = new Size(460, 236);
            panelLogin.TabIndex = 7;
            panelLogin.Paint += panelLogin_Paint;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(131, 179);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(213, 32);
            btnSalir.TabIndex = 7;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.ForeColor = Color.DimGray;
            label1.Location = new Point(340, 116);
            label1.Name = "label1";
            label1.Size = new Size(459, 20);
            label1.TabIndex = 8;
            label1.Text = "___________________________________________________________________________";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Linen;
            ClientSize = new Size(1227, 646);
            Controls.Add(panelLogin);
            Controls.Add(lblSubtitulo);
            Controls.Add(lblTitulo);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            panelLogin.ResumeLayout(false);
            panelLogin.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private Label lblSubtitulo;
        private Label label3;
        private Label label4;
        private TextBox txtUsuario;
        private TextBox txtContrasena;
        private Button btnIniciarSesion;
        private Panel panelLogin;
        private Button btnSalir;
        private Label label1;
    }
}
