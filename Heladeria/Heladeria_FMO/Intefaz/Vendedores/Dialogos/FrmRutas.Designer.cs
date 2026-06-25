namespace Heladeria_FMO.Intefaz.Vendedores.Dialogos
{
    partial class FrmRutas
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
            lblZona = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtZona = new Guna.UI2.WinForms.Guna2TextBox();
            lblResp = new Guna.UI2.WinForms.Guna2HtmlLabel();
            cboResponsable = new Guna.UI2.WinForms.Guna2ComboBox();
            lblInicio = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtInicio = new Guna.UI2.WinForms.Guna2TextBox();
            lblFin = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtFin = new Guna.UI2.WinForms.Guna2TextBox();
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
            marco.Controls.Add(lblZona);
            marco.Controls.Add(txtZona);
            marco.Controls.Add(lblResp);
            marco.Controls.Add(cboResponsable);
            marco.Controls.Add(lblInicio);
            marco.Controls.Add(txtInicio);
            marco.Controls.Add(lblFin);
            marco.Controls.Add(txtFin);
            marco.Controls.Add(btnGuardar);
            marco.Controls.Add(btnDesactivar);
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
            titulo.Text = "Rutas";
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
            dgv.Size = new System.Drawing.Size(430, 390);
            dgv.TabIndex = 2;
            dgv.SelectionChanged += dgv_SelectionChanged;
            dgv.CellClick += Dgv_CellClick;
            //
            // btnNuevo
            //
            btnNuevo.Location = new System.Drawing.Point(24, 462);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new System.Drawing.Size(130, 40);
            btnNuevo.TabIndex = 3;
            btnNuevo.Text = "+ Nueva";
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
            // lblZona
            //
            lblZona.BackColor = System.Drawing.Color.Transparent;
            lblZona.Location = new System.Drawing.Point(482, 126);
            lblZona.Name = "lblZona";
            lblZona.Size = new System.Drawing.Size(190, 20);
            lblZona.TabIndex = 6;
            lblZona.Text = "Zona";
            //
            // txtZona
            //
            txtZona.Location = new System.Drawing.Point(482, 148);
            txtZona.Name = "txtZona";
            txtZona.Size = new System.Drawing.Size(388, 34);
            txtZona.TabIndex = 7;
            //
            // lblResp
            //
            lblResp.BackColor = System.Drawing.Color.Transparent;
            lblResp.Location = new System.Drawing.Point(482, 192);
            lblResp.Name = "lblResp";
            lblResp.Size = new System.Drawing.Size(190, 20);
            lblResp.TabIndex = 8;
            lblResp.Text = "Responsable";
            //
            // cboResponsable
            //
            cboResponsable.Location = new System.Drawing.Point(482, 214);
            cboResponsable.Name = "cboResponsable";
            cboResponsable.Size = new System.Drawing.Size(388, 34);
            cboResponsable.TabIndex = 9;
            //
            // lblInicio
            //
            lblInicio.BackColor = System.Drawing.Color.Transparent;
            lblInicio.Location = new System.Drawing.Point(482, 258);
            lblInicio.Name = "lblInicio";
            lblInicio.Size = new System.Drawing.Size(190, 20);
            lblInicio.TabIndex = 10;
            lblInicio.Text = "Horario inicio (HH:mm)";
            //
            // txtInicio
            //
            txtInicio.Location = new System.Drawing.Point(482, 280);
            txtInicio.Name = "txtInicio";
            txtInicio.PlaceholderText = "08:00";
            txtInicio.Size = new System.Drawing.Size(185, 34);
            txtInicio.TabIndex = 11;
            //
            // lblFin
            //
            lblFin.BackColor = System.Drawing.Color.Transparent;
            lblFin.Location = new System.Drawing.Point(685, 258);
            lblFin.Name = "lblFin";
            lblFin.Size = new System.Drawing.Size(190, 20);
            lblFin.TabIndex = 12;
            lblFin.Text = "Horario fin (HH:mm)";
            //
            // txtFin
            //
            txtFin.Location = new System.Drawing.Point(685, 280);
            txtFin.Name = "txtFin";
            txtFin.PlaceholderText = "17:00";
            txtFin.Size = new System.Drawing.Size(185, 34);
            txtFin.TabIndex = 13;
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
            // btnDesactivar
            //
            btnDesactivar.Location = new System.Drawing.Point(685, 360);
            btnDesactivar.Name = "btnDesactivar";
            btnDesactivar.Size = new System.Drawing.Size(185, 44);
            btnDesactivar.TabIndex = 15;
            btnDesactivar.Text = "Dar de baja";
            btnDesactivar.Click += btnDesactivar_Click;
            //
            // FrmRutas
            //
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(900, 520);
            Controls.Add(marco);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "FrmRutas";
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
        private Guna.UI2.WinForms.Guna2HtmlLabel lblZona;
        private Guna.UI2.WinForms.Guna2TextBox txtZona;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblResp;
        private Guna.UI2.WinForms.Guna2ComboBox cboResponsable;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblInicio;
        private Guna.UI2.WinForms.Guna2TextBox txtInicio;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblFin;
        private Guna.UI2.WinForms.Guna2TextBox txtFin;
        private Guna.UI2.WinForms.Guna2Button btnGuardar;
        private Guna.UI2.WinForms.Guna2Button btnDesactivar;
    }
}
