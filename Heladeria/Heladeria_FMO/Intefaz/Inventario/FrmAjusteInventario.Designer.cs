namespace Heladeria_FMO.Intefaz.Inventario
{
    partial class FrmAjusteInventario
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
            lblProducto = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblTipo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            cboTipo = new Guna.UI2.WinForms.Guna2ComboBox();
            lblCantidad = new Guna.UI2.WinForms.Guna2HtmlLabel();
            numCantidad = new Guna.UI2.WinForms.Guna2NumericUpDown();
            lblObs = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtObservacion = new Guna.UI2.WinForms.Guna2TextBox();
            btnEnviar = new Guna.UI2.WinForms.Guna2Button();
            tarjeta.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numCantidad).BeginInit();
            SuspendLayout();
            //
            // tarjeta
            //
            tarjeta.Controls.Add(titulo);
            tarjeta.Controls.Add(lblProducto);
            tarjeta.Controls.Add(lblTipo);
            tarjeta.Controls.Add(cboTipo);
            tarjeta.Controls.Add(lblCantidad);
            tarjeta.Controls.Add(numCantidad);
            tarjeta.Controls.Add(lblObs);
            tarjeta.Controls.Add(txtObservacion);
            tarjeta.Controls.Add(btnEnviar);
            tarjeta.Location = new System.Drawing.Point(20, 20);
            tarjeta.Name = "tarjeta";
            tarjeta.Size = new System.Drawing.Size(380, 320);
            tarjeta.TabIndex = 0;
            //
            // titulo
            //
            titulo.BackColor = System.Drawing.Color.Transparent;
            titulo.Location = new System.Drawing.Point(20, 16);
            titulo.Name = "titulo";
            titulo.Size = new System.Drawing.Size(340, 24);
            titulo.TabIndex = 0;
            titulo.Text = "Solicitar ajuste de stock";
            //
            // lblProducto
            //
            lblProducto.BackColor = System.Drawing.Color.Transparent;
            lblProducto.Location = new System.Drawing.Point(20, 46);
            lblProducto.Name = "lblProducto";
            lblProducto.Size = new System.Drawing.Size(340, 22);
            lblProducto.TabIndex = 1;
            //
            // lblTipo
            //
            lblTipo.BackColor = System.Drawing.Color.Transparent;
            lblTipo.Location = new System.Drawing.Point(20, 80);
            lblTipo.Name = "lblTipo";
            lblTipo.Size = new System.Drawing.Size(220, 20);
            lblTipo.TabIndex = 2;
            lblTipo.Text = "Tipo de ajuste";
            //
            // cboTipo
            //
            cboTipo.Location = new System.Drawing.Point(20, 104);
            cboTipo.Name = "cboTipo";
            cboTipo.Size = new System.Drawing.Size(340, 36);
            cboTipo.TabIndex = 3;
            cboTipo.Items.AddRange(new object[] { "Sobrante (sumar al stock)", "Merma (restar del stock)" });
            //
            // lblCantidad
            //
            lblCantidad.BackColor = System.Drawing.Color.Transparent;
            lblCantidad.Location = new System.Drawing.Point(20, 148);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.Size = new System.Drawing.Size(220, 20);
            lblCantidad.TabIndex = 4;
            lblCantidad.Text = "Cantidad";
            //
            // numCantidad
            //
            numCantidad.Location = new System.Drawing.Point(20, 172);
            numCantidad.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numCantidad.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numCantidad.Name = "numCantidad";
            numCantidad.Size = new System.Drawing.Size(340, 36);
            numCantidad.TabIndex = 5;
            numCantidad.Value = new decimal(new int[] { 1, 0, 0, 0 });
            //
            // lblObs
            //
            lblObs.BackColor = System.Drawing.Color.Transparent;
            lblObs.Location = new System.Drawing.Point(20, 216);
            lblObs.Name = "lblObs";
            lblObs.Size = new System.Drawing.Size(220, 20);
            lblObs.TabIndex = 6;
            lblObs.Text = "Motivo / observación";
            //
            // txtObservacion
            //
            txtObservacion.Location = new System.Drawing.Point(20, 240);
            txtObservacion.Name = "txtObservacion";
            txtObservacion.PlaceholderText = "Ej. producto dañado en bodega";
            txtObservacion.Size = new System.Drawing.Size(340, 36);
            txtObservacion.TabIndex = 7;
            //
            // btnEnviar
            //
            btnEnviar.Location = new System.Drawing.Point(20, 288);
            btnEnviar.Name = "btnEnviar";
            btnEnviar.Size = new System.Drawing.Size(340, 40);
            btnEnviar.TabIndex = 8;
            btnEnviar.Text = "Enviar a aprobación";
            btnEnviar.Click += BtnEnviar_Click;
            //
            // FrmAjusteInventario
            //
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(420, 360);
            Controls.Add(tarjeta);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmAjusteInventario";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Ajuste de inventario";
            tarjeta.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numCantidad).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel tarjeta;
        private Guna.UI2.WinForms.Guna2HtmlLabel titulo;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblProducto;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTipo;
        private Guna.UI2.WinForms.Guna2ComboBox cboTipo;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblCantidad;
        private Guna.UI2.WinForms.Guna2NumericUpDown numCantidad;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblObs;
        private Guna.UI2.WinForms.Guna2TextBox txtObservacion;
        private Guna.UI2.WinForms.Guna2Button btnEnviar;
    }
}
