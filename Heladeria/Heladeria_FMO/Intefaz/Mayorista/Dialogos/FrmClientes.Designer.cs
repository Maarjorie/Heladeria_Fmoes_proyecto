namespace Heladeria_FMO.Intefaz.Mayorista.Dialogos
{
    partial class FrmClientes
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
            lblNit = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtNit = new Guna.UI2.WinForms.Guna2TextBox();
            lblTel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtTelefono = new Guna.UI2.WinForms.Guna2TextBox();
            lblEncargado = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtEncargado = new Guna.UI2.WinForms.Guna2TextBox();
            lblCorreo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtCorreo = new Guna.UI2.WinForms.Guna2TextBox();
            lblDireccion = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtDireccion = new Guna.UI2.WinForms.Guna2TextBox();
            lblDescuento = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtDescuento = new Guna.UI2.WinForms.Guna2TextBox();
            btnHistorial = new Guna.UI2.WinForms.Guna2Button();
            btnGuardar = new Guna.UI2.WinForms.Guna2Button();
            btnDesactivar = new Guna.UI2.WinForms.Guna2Button();
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
            marco.Controls.Add(lblNit);
            marco.Controls.Add(txtNit);
            marco.Controls.Add(lblTel);
            marco.Controls.Add(txtTelefono);
            marco.Controls.Add(lblEncargado);
            marco.Controls.Add(txtEncargado);
            marco.Controls.Add(lblCorreo);
            marco.Controls.Add(txtCorreo);
            marco.Controls.Add(lblDireccion);
            marco.Controls.Add(txtDireccion);
            marco.Controls.Add(lblDescuento);
            marco.Controls.Add(txtDescuento);
            marco.Controls.Add(btnHistorial);
            marco.Controls.Add(btnGuardar);
            marco.Controls.Add(btnDesactivar);
            marco.Dock = System.Windows.Forms.DockStyle.Fill;
            marco.Location = new System.Drawing.Point(0, 0);
            marco.Name = "marco";
            marco.Size = new System.Drawing.Size(900, 560);
            marco.TabIndex = 0;
            //
            // titulo
            //
            titulo.BackColor = System.Drawing.Color.Transparent;
            titulo.Location = new System.Drawing.Point(24, 18);
            titulo.Name = "titulo";
            titulo.Size = new System.Drawing.Size(420, 30);
            titulo.TabIndex = 0;
            titulo.Text = "Clientes mayoristas";
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
            dgv.Size = new System.Drawing.Size(430, 430);
            dgv.TabIndex = 2;
            dgv.SelectionChanged += dgv_SelectionChanged;
            dgv.CellClick += Dgv_CellClick;
            //
            // btnNuevo
            //
            btnNuevo.Location = new System.Drawing.Point(24, 500);
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
            lblNombre.Text = "Nombre comercial";
            //
            // txtNombre
            //
            txtNombre.Location = new System.Drawing.Point(482, 82);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new System.Drawing.Size(388, 34);
            txtNombre.TabIndex = 5;
            //
            // lblNit
            //
            lblNit.BackColor = System.Drawing.Color.Transparent;
            lblNit.Location = new System.Drawing.Point(482, 126);
            lblNit.Name = "lblNit";
            lblNit.Size = new System.Drawing.Size(190, 20);
            lblNit.TabIndex = 6;
            lblNit.Text = "NIT";
            //
            // txtNit
            //
            txtNit.Location = new System.Drawing.Point(482, 148);
            txtNit.Name = "txtNit";
            txtNit.Size = new System.Drawing.Size(185, 34);
            txtNit.TabIndex = 7;
            //
            // lblTel
            //
            lblTel.BackColor = System.Drawing.Color.Transparent;
            lblTel.Location = new System.Drawing.Point(685, 126);
            lblTel.Name = "lblTel";
            lblTel.Size = new System.Drawing.Size(190, 20);
            lblTel.TabIndex = 8;
            lblTel.Text = "Teléfono";
            //
            // txtTelefono
            //
            txtTelefono.Location = new System.Drawing.Point(685, 148);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new System.Drawing.Size(185, 34);
            txtTelefono.TabIndex = 9;
            //
            // lblEncargado
            //
            lblEncargado.BackColor = System.Drawing.Color.Transparent;
            lblEncargado.Location = new System.Drawing.Point(482, 192);
            lblEncargado.Name = "lblEncargado";
            lblEncargado.Size = new System.Drawing.Size(190, 20);
            lblEncargado.TabIndex = 10;
            lblEncargado.Text = "Encargado";
            //
            // txtEncargado
            //
            txtEncargado.Location = new System.Drawing.Point(482, 214);
            txtEncargado.Name = "txtEncargado";
            txtEncargado.Size = new System.Drawing.Size(388, 34);
            txtEncargado.TabIndex = 11;
            //
            // lblCorreo
            //
            lblCorreo.BackColor = System.Drawing.Color.Transparent;
            lblCorreo.Location = new System.Drawing.Point(482, 258);
            lblCorreo.Name = "lblCorreo";
            lblCorreo.Size = new System.Drawing.Size(190, 20);
            lblCorreo.TabIndex = 12;
            lblCorreo.Text = "Correo";
            //
            // txtCorreo
            //
            txtCorreo.Location = new System.Drawing.Point(482, 280);
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new System.Drawing.Size(388, 34);
            txtCorreo.TabIndex = 13;
            //
            // lblDireccion
            //
            lblDireccion.BackColor = System.Drawing.Color.Transparent;
            lblDireccion.Location = new System.Drawing.Point(482, 324);
            lblDireccion.Name = "lblDireccion";
            lblDireccion.Size = new System.Drawing.Size(190, 20);
            lblDireccion.TabIndex = 14;
            lblDireccion.Text = "Dirección";
            //
            // txtDireccion
            //
            txtDireccion.Location = new System.Drawing.Point(482, 346);
            txtDireccion.Name = "txtDireccion";
            txtDireccion.Size = new System.Drawing.Size(388, 34);
            txtDireccion.TabIndex = 15;
            //
            // lblDescuento
            //
            lblDescuento.BackColor = System.Drawing.Color.Transparent;
            lblDescuento.Location = new System.Drawing.Point(482, 388);
            lblDescuento.Name = "lblDescuento";
            lblDescuento.Size = new System.Drawing.Size(190, 20);
            lblDescuento.TabIndex = 16;
            lblDescuento.Text = "Descuento (%)";
            //
            // txtDescuento
            //
            txtDescuento.Location = new System.Drawing.Point(482, 410);
            txtDescuento.Name = "txtDescuento";
            txtDescuento.PlaceholderText = "0";
            txtDescuento.Size = new System.Drawing.Size(185, 34);
            txtDescuento.TabIndex = 17;
            //
            // btnHistorial
            //
            btnHistorial.Location = new System.Drawing.Point(685, 410);
            btnHistorial.Name = "btnHistorial";
            btnHistorial.Size = new System.Drawing.Size(185, 34);
            btnHistorial.TabIndex = 18;
            btnHistorial.Text = "Ver historial";
            btnHistorial.Click += btnHistorial_Click;
            //
            // btnGuardar
            //
            btnGuardar.Location = new System.Drawing.Point(482, 440);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new System.Drawing.Size(185, 44);
            btnGuardar.TabIndex = 19;
            btnGuardar.Text = "Guardar";
            btnGuardar.Click += btnGuardar_Click;
            //
            // btnDesactivar
            //
            btnDesactivar.Location = new System.Drawing.Point(685, 440);
            btnDesactivar.Name = "btnDesactivar";
            btnDesactivar.Size = new System.Drawing.Size(185, 44);
            btnDesactivar.TabIndex = 20;
            btnDesactivar.Text = "Dar de baja";
            btnDesactivar.Click += btnDesactivar_Click;
            //
            // FrmClientes
            //
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(900, 560);
            Controls.Add(marco);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "FrmClientes";
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
        private Guna.UI2.WinForms.Guna2HtmlLabel lblNit;
        private Guna.UI2.WinForms.Guna2TextBox txtNit;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTel;
        private Guna.UI2.WinForms.Guna2TextBox txtTelefono;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblEncargado;
        private Guna.UI2.WinForms.Guna2TextBox txtEncargado;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblCorreo;
        private Guna.UI2.WinForms.Guna2TextBox txtCorreo;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblDireccion;
        private Guna.UI2.WinForms.Guna2TextBox txtDireccion;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblDescuento;
        private Guna.UI2.WinForms.Guna2TextBox txtDescuento;
        private Guna.UI2.WinForms.Guna2Button btnHistorial;
        private Guna.UI2.WinForms.Guna2Button btnGuardar;
        private Guna.UI2.WinForms.Guna2Button btnDesactivar;
    }
}
