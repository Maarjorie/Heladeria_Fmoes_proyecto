namespace Heladeria_FMO.Intefaz.Autorizaciones
{
    partial class FrmUsuarios
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
            btnNuevo = new Guna.UI2.WinForms.Guna2Button();
            lblNombre = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtNombre = new Guna.UI2.WinForms.Guna2TextBox();
            lblUsuario = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtUsuario = new Guna.UI2.WinForms.Guna2TextBox();
            lblRol = new Guna.UI2.WinForms.Guna2HtmlLabel();
            cboRol = new Guna.UI2.WinForms.Guna2ComboBox();
            lblCorreo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtCorreo = new Guna.UI2.WinForms.Guna2TextBox();
            lblContrasena = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtContrasena = new Guna.UI2.WinForms.Guna2TextBox();
            btnGuardar = new Guna.UI2.WinForms.Guna2Button();
            btnEstado = new Guna.UI2.WinForms.Guna2Button();
            marco.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgv).BeginInit();
            SuspendLayout();
            //
            // marco
            //
            marco.Controls.Add(titulo);
            marco.Controls.Add(btnCerrar);
            marco.Controls.Add(dgv);
            marco.Controls.Add(btnNuevo);
            marco.Controls.Add(lblNombre);
            marco.Controls.Add(txtNombre);
            marco.Controls.Add(lblUsuario);
            marco.Controls.Add(txtUsuario);
            marco.Controls.Add(lblRol);
            marco.Controls.Add(cboRol);
            marco.Controls.Add(lblCorreo);
            marco.Controls.Add(txtCorreo);
            marco.Controls.Add(lblContrasena);
            marco.Controls.Add(txtContrasena);
            marco.Controls.Add(btnGuardar);
            marco.Controls.Add(btnEstado);
            marco.Dock = System.Windows.Forms.DockStyle.Fill;
            marco.Location = new System.Drawing.Point(0, 0);
            marco.Name = "marco";
            marco.Size = new System.Drawing.Size(900, 520);
            marco.TabIndex = 0;
            //
            // titulo
            //
            titulo.BackColor = System.Drawing.Color.Transparent;
            titulo.Location = new System.Drawing.Point(24, 18);
            titulo.Name = "titulo";
            titulo.Size = new System.Drawing.Size(300, 30);
            titulo.TabIndex = 0;
            titulo.Text = "Usuarios";
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
            dgv.Size = new System.Drawing.Size(430, 400);
            dgv.TabIndex = 2;
            dgv.SelectionChanged += dgv_SelectionChanged;
            dgv.CellClick += Dgv_CellClick;
            //
            // btnNuevo
            //
            btnNuevo.Location = new System.Drawing.Point(24, 472);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new System.Drawing.Size(130, 40);
            btnNuevo.TabIndex = 3;
            btnNuevo.Text = "+ Nuevo";
            btnNuevo.Click += BtnNuevo_Click;
            //
            // lblNombre
            //
            lblNombre.BackColor = System.Drawing.Color.Transparent;
            lblNombre.Location = new System.Drawing.Point(482, 60);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new System.Drawing.Size(190, 20);
            lblNombre.TabIndex = 4;
            lblNombre.Text = "Nombre";
            //
            // txtNombre
            //
            txtNombre.Location = new System.Drawing.Point(482, 82);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new System.Drawing.Size(388, 34);
            txtNombre.TabIndex = 5;
            //
            // lblUsuario
            //
            lblUsuario.BackColor = System.Drawing.Color.Transparent;
            lblUsuario.Location = new System.Drawing.Point(482, 126);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new System.Drawing.Size(190, 20);
            lblUsuario.TabIndex = 6;
            lblUsuario.Text = "Usuario";
            //
            // txtUsuario
            //
            txtUsuario.Location = new System.Drawing.Point(482, 148);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new System.Drawing.Size(185, 34);
            txtUsuario.TabIndex = 7;
            //
            // lblRol
            //
            lblRol.BackColor = System.Drawing.Color.Transparent;
            lblRol.Location = new System.Drawing.Point(685, 126);
            lblRol.Name = "lblRol";
            lblRol.Size = new System.Drawing.Size(190, 20);
            lblRol.TabIndex = 8;
            lblRol.Text = "Rol";
            //
            // cboRol
            //
            cboRol.Location = new System.Drawing.Point(685, 148);
            cboRol.Name = "cboRol";
            cboRol.Size = new System.Drawing.Size(185, 34);
            cboRol.TabIndex = 9;
            //
            // lblCorreo
            //
            lblCorreo.BackColor = System.Drawing.Color.Transparent;
            lblCorreo.Location = new System.Drawing.Point(482, 192);
            lblCorreo.Name = "lblCorreo";
            lblCorreo.Size = new System.Drawing.Size(190, 20);
            lblCorreo.TabIndex = 10;
            lblCorreo.Text = "Correo";
            //
            // txtCorreo
            //
            txtCorreo.Location = new System.Drawing.Point(482, 214);
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new System.Drawing.Size(388, 34);
            txtCorreo.TabIndex = 11;
            //
            // lblContrasena
            //
            lblContrasena.BackColor = System.Drawing.Color.Transparent;
            lblContrasena.Location = new System.Drawing.Point(482, 258);
            lblContrasena.Name = "lblContrasena";
            lblContrasena.Size = new System.Drawing.Size(190, 20);
            lblContrasena.TabIndex = 12;
            lblContrasena.Text = "Contraseña";
            //
            // txtContrasena
            //
            txtContrasena.Location = new System.Drawing.Point(482, 280);
            txtContrasena.Name = "txtContrasena";
            txtContrasena.PasswordChar = '●';
            txtContrasena.Size = new System.Drawing.Size(388, 34);
            txtContrasena.TabIndex = 13;
            //
            // btnGuardar
            //
            btnGuardar.Location = new System.Drawing.Point(482, 360);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new System.Drawing.Size(185, 44);
            btnGuardar.TabIndex = 14;
            btnGuardar.Text = "Guardar";
            btnGuardar.Click += btnGuardar_Click;
            //
            // btnEstado
            //
            btnEstado.Location = new System.Drawing.Point(685, 360);
            btnEstado.Name = "btnEstado";
            btnEstado.Size = new System.Drawing.Size(185, 44);
            btnEstado.TabIndex = 15;
            btnEstado.Text = "Dar de baja";
            btnEstado.Click += btnEstado_Click;
            //
            // FrmUsuarios
            //
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(900, 520);
            Controls.Add(marco);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "FrmUsuarios";
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
        private Guna.UI2.WinForms.Guna2Button btnNuevo;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblNombre;
        private Guna.UI2.WinForms.Guna2TextBox txtNombre;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblUsuario;
        private Guna.UI2.WinForms.Guna2TextBox txtUsuario;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblRol;
        private Guna.UI2.WinForms.Guna2ComboBox cboRol;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblCorreo;
        private Guna.UI2.WinForms.Guna2TextBox txtCorreo;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblContrasena;
        private Guna.UI2.WinForms.Guna2TextBox txtContrasena;
        private Guna.UI2.WinForms.Guna2Button btnGuardar;
        private Guna.UI2.WinForms.Guna2Button btnEstado;
    }
}
