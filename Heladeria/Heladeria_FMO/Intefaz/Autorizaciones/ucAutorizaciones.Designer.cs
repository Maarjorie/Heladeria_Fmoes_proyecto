namespace Heladeria_FMO.Intefaz.Autorizaciones
{
    partial class ucAutorizaciones
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
            pnlCard = new Guna.UI2.WinForms.Guna2Panel();
            dgvNotificaciones = new Guna.UI2.WinForms.Guna2DataGridView();
            pnlHeader = new Guna.UI2.WinForms.Guna2Panel();
            btnMarcarLeida = new Guna.UI2.WinForms.Guna2Button();
            btnRefrescar = new Guna.UI2.WinForms.Guna2Button();
            lblSub = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblTitulo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvNotificaciones).BeginInit();
            pnlHeader.SuspendLayout();
            SuspendLayout();
            //
            // pnlCard
            //
            pnlCard.Controls.Add(dgvNotificaciones);
            pnlCard.Dock = DockStyle.Fill;
            pnlCard.Location = new Point(20, 84);
            pnlCard.Name = "pnlCard";
            pnlCard.Padding = new Padding(2);
            pnlCard.Size = new Size(1080, 526);
            pnlCard.TabIndex = 1;
            //
            // dgvNotificaciones
            //
            dgvNotificaciones.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvNotificaciones.Dock = DockStyle.Fill;
            dgvNotificaciones.Location = new Point(2, 2);
            dgvNotificaciones.Name = "dgvNotificaciones";
            dgvNotificaciones.RowHeadersVisible = false;
            dgvNotificaciones.Size = new Size(1076, 522);
            dgvNotificaciones.TabIndex = 0;
            //
            // pnlHeader
            //
            pnlHeader.Controls.Add(btnMarcarLeida);
            pnlHeader.Controls.Add(btnRefrescar);
            pnlHeader.Controls.Add(lblSub);
            pnlHeader.Controls.Add(lblTitulo);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(20, 20);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1080, 64);
            pnlHeader.TabIndex = 0;
            //
            // btnMarcarLeida
            //
            btnMarcarLeida.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMarcarLeida.Location = new Point(820, 12);
            btnMarcarLeida.Name = "btnMarcarLeida";
            btnMarcarLeida.Size = new Size(260, 42);
            btnMarcarLeida.TabIndex = 2;
            btnMarcarLeida.Text = "Marcar como leída";
            btnMarcarLeida.Click += btnMarcarLeida_Click;
            //
            // btnRefrescar
            //
            btnRefrescar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRefrescar.Location = new Point(700, 12);
            btnRefrescar.Name = "btnRefrescar";
            btnRefrescar.Size = new Size(110, 42);
            btnRefrescar.TabIndex = 1;
            btnRefrescar.Text = "Refrescar";
            btnRefrescar.Click += btnRefrescar_Click;
            //
            // lblSub
            //
            lblSub.BackColor = Color.Transparent;
            lblSub.Location = new Point(4, 40);
            lblSub.Name = "lblSub";
            lblSub.Size = new Size(400, 20);
            lblSub.TabIndex = 1;
            lblSub.Text = "Notificaciones pendientes del sistema";
            //
            // lblTitulo
            //
            lblTitulo.BackColor = Color.Transparent;
            lblTitulo.Location = new Point(4, 10);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(400, 30);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Autorizaciones";
            //
            // ucAutorizaciones
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlCard);
            Controls.Add(pnlHeader);
            Padding = new Padding(20);
            Name = "ucAutorizaciones";
            Size = new Size(1120, 630);
            pnlCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvNotificaciones).EndInit();
            pnlHeader.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlHeader;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitulo;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSub;
        private Guna.UI2.WinForms.Guna2Button btnRefrescar;
        private Guna.UI2.WinForms.Guna2Button btnMarcarLeida;
        private Guna.UI2.WinForms.Guna2Panel pnlCard;
        private Guna.UI2.WinForms.Guna2DataGridView dgvNotificaciones;
    }
}
