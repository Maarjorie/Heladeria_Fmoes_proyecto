namespace Heladeria_FMO
{
    partial class FrmLogin
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges17 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges18 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges29 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges30 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges19 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges20 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges21 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges22 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges23 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges24 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges25 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges26 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges27 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges28 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges31 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges32 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            pnlContenedor = new Guna.UI2.WinForms.Guna2Panel();
            tlpLogin = new TableLayoutPanel();
            pnlLogin = new Guna.UI2.WinForms.Guna2Panel();
            LlblRecuperarCredenciales = new LinkLabel();
            btnMostrar = new Guna.UI2.WinForms.Guna2Button();
            btnSalir = new Guna.UI2.WinForms.Guna2Button();
            btnEntrar = new Guna.UI2.WinForms.Guna2Button();
            txtContraseña = new Guna.UI2.WinForms.Guna2TextBox();
            lblContraseña = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtUsuario = new Guna.UI2.WinForms.Guna2TextBox();
            lblUsuario = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblSubtitulo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblTitulo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            PnlFondo = new Guna.UI2.WinForms.Guna2GradientPanel();
            pnlContenedor.SuspendLayout();
            tlpLogin.SuspendLayout();
            pnlLogin.SuspendLayout();
            SuspendLayout();
            // 
            // pnlContenedor
            // 
            pnlContenedor.BackColor = Color.FromArgb(0, 91, 153);
            pnlContenedor.Controls.Add(tlpLogin);
            pnlContenedor.CustomizableEdges = customizableEdges17;
            pnlContenedor.Dock = DockStyle.Right;
            pnlContenedor.Location = new Point(311, 0);
            pnlContenedor.Name = "pnlContenedor";
            pnlContenedor.ShadowDecoration.CustomizableEdges = customizableEdges18;
            pnlContenedor.Size = new Size(919, 840);
            pnlContenedor.TabIndex = 6;
            // 
            // tlpLogin
            // 
            tlpLogin.ColumnCount = 3;
            tlpLogin.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28.0912361F));
            tlpLogin.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 71.90876F));
            tlpLogin.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 85F));
            tlpLogin.Controls.Add(pnlLogin, 1, 1);
            tlpLogin.Dock = DockStyle.Fill;
            tlpLogin.Location = new Point(0, 0);
            tlpLogin.Name = "tlpLogin";
            tlpLogin.RowCount = 3;
            tlpLogin.RowStyles.Add(new RowStyle(SizeType.Percent, 14.9295778F));
            tlpLogin.RowStyles.Add(new RowStyle(SizeType.Percent, 85.07042F));
            tlpLogin.RowStyles.Add(new RowStyle(SizeType.Absolute, 92F));
            tlpLogin.Size = new Size(919, 840);
            tlpLogin.TabIndex = 7;
            // 
            // pnlLogin
            // 
            pnlLogin.Controls.Add(LlblRecuperarCredenciales);
            pnlLogin.Controls.Add(btnMostrar);
            pnlLogin.Controls.Add(btnSalir);
            pnlLogin.Controls.Add(btnEntrar);
            pnlLogin.Controls.Add(txtContraseña);
            pnlLogin.Controls.Add(lblContraseña);
            pnlLogin.Controls.Add(txtUsuario);
            pnlLogin.Controls.Add(lblUsuario);
            pnlLogin.Controls.Add(lblSubtitulo);
            pnlLogin.Controls.Add(lblTitulo);
            pnlLogin.CustomizableEdges = customizableEdges29;
            pnlLogin.Dock = DockStyle.Fill;
            pnlLogin.Location = new Point(237, 114);
            pnlLogin.Name = "pnlLogin";
            pnlLogin.ShadowDecoration.CustomizableEdges = customizableEdges30;
            pnlLogin.Size = new Size(593, 630);
            pnlLogin.TabIndex = 8;
            // 
            // LlblRecuperarCredenciales
            // 
            LlblRecuperarCredenciales.AutoSize = true;
            LlblRecuperarCredenciales.Font = new Font("Segoe UI", 10F);
            LlblRecuperarCredenciales.LinkColor = Color.FromArgb(255, 255, 128);
            LlblRecuperarCredenciales.Location = new Point(93, 400);
            LlblRecuperarCredenciales.Name = "LlblRecuperarCredenciales";
            LlblRecuperarCredenciales.Size = new Size(303, 23);
            LlblRecuperarCredenciales.TabIndex = 3;
            LlblRecuperarCredenciales.TabStop = true;
            LlblRecuperarCredenciales.Text = "¿Olvidaste alguna de tus credenciales?";
            // 
            // btnMostrar
            // 
            btnMostrar.BackColor = Color.Transparent;
            btnMostrar.BorderRadius = 22;
            btnMostrar.CustomizableEdges = customizableEdges19;
            btnMostrar.DisabledState.BorderColor = Color.DarkGray;
            btnMostrar.DisabledState.CustomBorderColor = Color.DarkGray;
            btnMostrar.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnMostrar.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnMostrar.FillColor = Color.FromArgb(168, 218, 220);
            btnMostrar.Font = new Font("Segoe UI", 9F);
            btnMostrar.ForeColor = Color.White;
            btnMostrar.Image = Properties.Resources.eye;
            btnMostrar.Location = new Point(402, 334);
            btnMostrar.Name = "btnMostrar";
            btnMostrar.ShadowDecoration.CustomizableEdges = customizableEdges20;
            btnMostrar.Size = new Size(46, 44);
            btnMostrar.TabIndex = 2;
            // 
            // btnSalir
            // 
            btnSalir.AutoRoundedCorners = true;
            btnSalir.BorderRadius = 34;
            btnSalir.CustomizableEdges = customizableEdges21;
            btnSalir.DisabledState.BorderColor = Color.DarkGray;
            btnSalir.DisabledState.CustomBorderColor = Color.DarkGray;
            btnSalir.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnSalir.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnSalir.FillColor = Color.FromArgb(0, 51, 102);
            btnSalir.Font = new Font("Segoe UI", 17F);
            btnSalir.ForeColor = Color.White;
            btnSalir.Location = new Point(0, 539);
            btnSalir.Name = "btnSalir";
            btnSalir.ShadowDecoration.CustomizableEdges = customizableEdges22;
            btnSalir.Size = new Size(396, 70);
            btnSalir.TabIndex = 5;
            btnSalir.Text = "Salir";
            btnSalir.Click += btnSalir_Click;
            // 
            // btnEntrar
            // 
            btnEntrar.AutoRoundedCorners = true;
            btnEntrar.BorderRadius = 34;
            btnEntrar.CustomizableEdges = customizableEdges23;
            btnEntrar.DisabledState.BorderColor = Color.DarkGray;
            btnEntrar.DisabledState.CustomBorderColor = Color.DarkGray;
            btnEntrar.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnEntrar.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnEntrar.FillColor = Color.FromArgb(0, 51, 102);
            btnEntrar.Font = new Font("Segoe UI", 17F);
            btnEntrar.ForeColor = Color.White;
            btnEntrar.Location = new Point(0, 463);
            btnEntrar.Name = "btnEntrar";
            btnEntrar.ShadowDecoration.CustomizableEdges = customizableEdges24;
            btnEntrar.Size = new Size(396, 70);
            btnEntrar.TabIndex = 4;
            btnEntrar.Text = "Entrar";
            btnEntrar.Click += btnEntrar_Click;
            // 
            // txtContraseña
            // 
            txtContraseña.AutoRoundedCorners = true;
            txtContraseña.BorderColor = Color.FromArgb(0, 91, 153);
            txtContraseña.BorderRadius = 41;
            txtContraseña.CustomizableEdges = customizableEdges25;
            txtContraseña.DefaultText = "";
            txtContraseña.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtContraseña.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtContraseña.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtContraseña.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtContraseña.FillColor = Color.FromArgb(254, 250, 224);
            txtContraseña.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtContraseña.Font = new Font("Segoe UI", 12F);
            txtContraseña.ForeColor = Color.Black;
            txtContraseña.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtContraseña.IconLeft = Properties.Resources._lock;
            txtContraseña.IconLeftOffset = new Point(10, 0);
            txtContraseña.IconLeftSize = new Size(25, 25);
            txtContraseña.Location = new Point(-1, 310);
            txtContraseña.Margin = new Padding(4, 6, 4, 6);
            txtContraseña.Name = "txtContraseña";
            txtContraseña.PasswordChar = '*';
            txtContraseña.PlaceholderForeColor = Color.Gray;
            txtContraseña.PlaceholderText = "Contraseña123";
            txtContraseña.SelectedText = "";
            txtContraseña.ShadowDecoration.CustomizableEdges = customizableEdges26;
            txtContraseña.Size = new Size(396, 84);
            txtContraseña.TabIndex = 1;
            // 
            // lblContraseña
            // 
            lblContraseña.BackColor = Color.Transparent;
            lblContraseña.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblContraseña.ForeColor = Color.White;
            lblContraseña.Location = new Point(0, 269);
            lblContraseña.Name = "lblContraseña";
            lblContraseña.Size = new Size(120, 32);
            lblContraseña.TabIndex = 10;
            lblContraseña.Text = "Contraseña";
            // 
            // txtUsuario
            // 
            txtUsuario.AutoRoundedCorners = true;
            txtUsuario.BorderColor = Color.FromArgb(0, 91, 153);
            txtUsuario.CustomizableEdges = customizableEdges27;
            txtUsuario.DefaultText = "";
            txtUsuario.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtUsuario.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtUsuario.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtUsuario.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtUsuario.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtUsuario.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtUsuario.ForeColor = Color.Black;
            txtUsuario.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtUsuario.IconLeft = Properties.Resources.user;
            txtUsuario.IconLeftOffset = new Point(10, 0);
            txtUsuario.IconLeftSize = new Size(25, 25);
            txtUsuario.Location = new Point(0, 170);
            txtUsuario.Margin = new Padding(4, 6, 4, 6);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.PlaceholderForeColor = Color.Gray;
            txtUsuario.PlaceholderText = "Usuario";
            txtUsuario.SelectedText = "";
            txtUsuario.ShadowDecoration.CustomizableEdges = customizableEdges28;
            txtUsuario.Size = new Size(396, 85);
            txtUsuario.TabIndex = 0;
            // 
            // lblUsuario
            // 
            lblUsuario.BackColor = Color.Transparent;
            lblUsuario.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblUsuario.ForeColor = Color.White;
            lblUsuario.Location = new Point(3, 129);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(82, 32);
            lblUsuario.TabIndex = 11;
            lblUsuario.Text = "Usuario";
            // 
            // lblSubtitulo
            // 
            lblSubtitulo.BackColor = Color.Transparent;
            lblSubtitulo.Font = new Font("Segoe UI", 14F);
            lblSubtitulo.ForeColor = Color.White;
            lblSubtitulo.Location = new Point(0, 72);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(396, 33);
            lblSubtitulo.TabIndex = 12;
            lblSubtitulo.Text = "Bienvenido de nuevo a heladerí\r\na FMO";
            // 
            // lblTitulo
            // 
            lblTitulo.BackColor = Color.Transparent;
            lblTitulo.Font = new Font("Segoe UI", 27F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(0, 3);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(277, 63);
            lblTitulo.TabIndex = 13;
            lblTitulo.Text = "Iniciar sesión";
            // 
            // PnlFondo
            // 
            PnlFondo.BackColor = Color.FromArgb(0, 51, 102);
            PnlFondo.CustomizableEdges = customizableEdges31;
            PnlFondo.Dock = DockStyle.Fill;
            PnlFondo.Location = new Point(0, 0);
            PnlFondo.Name = "PnlFondo";
            PnlFondo.ShadowDecoration.CustomizableEdges = customizableEdges32;
            PnlFondo.Size = new Size(1230, 840);
            PnlFondo.TabIndex = 14;
            // 
            // FrmLogin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Linen;
            ClientSize = new Size(1230, 840);
            Controls.Add(pnlContenedor);
            Controls.Add(PnlFondo);
            Name = "FrmLogin";
            Text = "Helados FMO - Sistema de Gestión";
            WindowState = FormWindowState.Maximized;
            pnlContenedor.ResumeLayout(false);
            tlpLogin.ResumeLayout(false);
            pnlLogin.ResumeLayout(false);
            pnlLogin.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Guna.UI2.WinForms.Guna2Panel pnlContenedor;
        private TableLayoutPanel tlpLogin;
        private Guna.UI2.WinForms.Guna2Panel pnlLogin;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitulo;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSubtitulo;
        private Guna.UI2.WinForms.Guna2GradientPanel PnlFondo;
        private Guna.UI2.WinForms.Guna2TextBox txtUsuario;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblUsuario;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblContraseña;
        private Guna.UI2.WinForms.Guna2Button btnEntrar;
        private Guna.UI2.WinForms.Guna2Button btnSalir;
        private Guna.UI2.WinForms.Guna2TextBox txtContraseña;
        private Guna.UI2.WinForms.Guna2Button btnMostrar;
        private LinkLabel LlblRecuperarCredenciales;
    }
}
