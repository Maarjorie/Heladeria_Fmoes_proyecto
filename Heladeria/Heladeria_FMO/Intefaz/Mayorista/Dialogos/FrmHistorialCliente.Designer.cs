namespace Heladeria_FMO.Intefaz.Mayorista.Dialogos
{
    partial class FrmHistorialCliente
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
            marco.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgv).BeginInit();
            SuspendLayout();
            //
            // marco
            //
            marco.Controls.Add(titulo);
            marco.Controls.Add(btnCerrar);
            marco.Controls.Add(dgv);
            marco.Dock = System.Windows.Forms.DockStyle.Fill;
            marco.Location = new System.Drawing.Point(0, 0);
            marco.Name = "marco";
            marco.Size = new System.Drawing.Size(680, 480);
            marco.TabIndex = 0;
            //
            // titulo
            //
            titulo.BackColor = System.Drawing.Color.Transparent;
            titulo.Location = new System.Drawing.Point(24, 18);
            titulo.Name = "titulo";
            titulo.Size = new System.Drawing.Size(560, 30);
            titulo.TabIndex = 0;
            //
            // btnCerrar
            //
            btnCerrar.Location = new System.Drawing.Point(624, 14);
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
            dgv.Size = new System.Drawing.Size(632, 400);
            dgv.TabIndex = 2;
            //
            // FrmHistorialCliente
            //
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(680, 480);
            Controls.Add(marco);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "FrmHistorialCliente";
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
    }
}
