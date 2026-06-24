namespace Heladeria_FMO.Intefaz.PuntoVenta
{
    partial class ucPuntoVenta
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
            pnlProductos = new Guna.UI2.WinForms.Guna2Panel();
            flpProductos = new FlowLayoutPanel();
            pnlBarra = new Guna.UI2.WinForms.Guna2Panel();
            flpCategorias = new FlowLayoutPanel();
            txtBuscar = new Guna.UI2.WinForms.Guna2TextBox();
            pnlTicket = new Guna.UI2.WinForms.Guna2Panel();
            flpTicket = new FlowLayoutPanel();
            pnlTotales = new Guna.UI2.WinForms.Guna2Panel();
            btnCobrar = new Guna.UI2.WinForms.Guna2Button();
            lblTotalVal = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblTotalCap = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblIvaVal = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblIvaCap = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblSubtotalVal = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblSubtotalCap = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblTicketSub = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblTicketTitulo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlProductos.SuspendLayout();
            pnlBarra.SuspendLayout();
            pnlTicket.SuspendLayout();
            pnlTotales.SuspendLayout();
            SuspendLayout();
            //
            // pnlProductos
            //
            pnlProductos.Controls.Add(flpProductos);
            pnlProductos.Controls.Add(pnlBarra);
            pnlProductos.Dock = DockStyle.Fill;
            pnlProductos.Location = new Point(0, 0);
            pnlProductos.Name = "pnlProductos";
            pnlProductos.Size = new Size(760, 650);
            pnlProductos.TabIndex = 0;
            //
            // flpProductos
            //
            flpProductos.AutoScroll = true;
            flpProductos.Dock = DockStyle.Fill;
            flpProductos.Location = new Point(0, 104);
            flpProductos.Name = "flpProductos";
            flpProductos.Padding = new Padding(16, 8, 16, 16);
            flpProductos.Size = new Size(760, 546);
            flpProductos.TabIndex = 1;
            //
            // pnlBarra
            //
            pnlBarra.Controls.Add(flpCategorias);
            pnlBarra.Controls.Add(txtBuscar);
            pnlBarra.Dock = DockStyle.Top;
            pnlBarra.Location = new Point(0, 0);
            pnlBarra.Name = "pnlBarra";
            pnlBarra.Size = new Size(760, 104);
            pnlBarra.TabIndex = 0;
            //
            // flpCategorias
            //
            flpCategorias.AutoScroll = true;
            flpCategorias.Location = new Point(16, 58);
            flpCategorias.Name = "flpCategorias";
            flpCategorias.Size = new Size(728, 40);
            flpCategorias.TabIndex = 1;
            flpCategorias.WrapContents = false;
            //
            // txtBuscar
            //
            txtBuscar.Location = new Point(16, 14);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Buscar producto…";
            txtBuscar.Size = new Size(320, 36);
            txtBuscar.TabIndex = 0;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            //
            // pnlTicket
            //
            pnlTicket.Controls.Add(flpTicket);
            pnlTicket.Controls.Add(pnlTotales);
            pnlTicket.Controls.Add(lblTicketSub);
            pnlTicket.Controls.Add(lblTicketTitulo);
            pnlTicket.Dock = DockStyle.Right;
            pnlTicket.Location = new Point(760, 0);
            pnlTicket.Name = "pnlTicket";
            pnlTicket.Padding = new Padding(16, 70, 16, 16);
            pnlTicket.Size = new Size(360, 650);
            pnlTicket.TabIndex = 1;
            //
            // flpTicket
            //
            flpTicket.AutoScroll = true;
            flpTicket.Dock = DockStyle.Fill;
            flpTicket.FlowDirection = FlowDirection.TopDown;
            flpTicket.Location = new Point(16, 78);
            flpTicket.Name = "flpTicket";
            flpTicket.Size = new Size(328, 348);
            flpTicket.TabIndex = 2;
            flpTicket.WrapContents = false;
            //
            // pnlTotales
            //
            pnlTotales.Controls.Add(btnCobrar);
            pnlTotales.Controls.Add(lblTotalVal);
            pnlTotales.Controls.Add(lblTotalCap);
            pnlTotales.Controls.Add(lblIvaVal);
            pnlTotales.Controls.Add(lblIvaCap);
            pnlTotales.Controls.Add(lblSubtotalVal);
            pnlTotales.Controls.Add(lblSubtotalCap);
            pnlTotales.Dock = DockStyle.Bottom;
            pnlTotales.Location = new Point(16, 426);
            pnlTotales.Name = "pnlTotales";
            pnlTotales.Size = new Size(328, 208);
            pnlTotales.TabIndex = 3;
            //
            // btnCobrar
            //
            btnCobrar.Dock = DockStyle.Bottom;
            btnCobrar.Location = new Point(0, 156);
            btnCobrar.Name = "btnCobrar";
            btnCobrar.Size = new Size(328, 52);
            btnCobrar.TabIndex = 0;
            btnCobrar.Text = "Cobrar venta";
            btnCobrar.Click += btnCobrar_Click;
            //
            // lblTotalVal
            //
            lblTotalVal.BackColor = Color.Transparent;
            lblTotalVal.Location = new Point(190, 96);
            lblTotalVal.Name = "lblTotalVal";
            lblTotalVal.Size = new Size(138, 30);
            lblTotalVal.TabIndex = 6;
            lblTotalVal.Text = "$0.00";
            lblTotalVal.TextAlignment = ContentAlignment.MiddleRight;
            //
            // lblTotalCap
            //
            lblTotalCap.BackColor = Color.Transparent;
            lblTotalCap.Location = new Point(0, 98);
            lblTotalCap.Name = "lblTotalCap";
            lblTotalCap.Size = new Size(80, 25);
            lblTotalCap.TabIndex = 5;
            lblTotalCap.Text = "Total";
            //
            // lblIvaVal
            //
            lblIvaVal.BackColor = Color.Transparent;
            lblIvaVal.Location = new Point(208, 56);
            lblIvaVal.Name = "lblIvaVal";
            lblIvaVal.Size = new Size(120, 22);
            lblIvaVal.TabIndex = 4;
            lblIvaVal.Text = "$0.00";
            lblIvaVal.TextAlignment = ContentAlignment.MiddleRight;
            //
            // lblIvaCap
            //
            lblIvaCap.BackColor = Color.Transparent;
            lblIvaCap.Location = new Point(0, 56);
            lblIvaCap.Name = "lblIvaCap";
            lblIvaCap.Size = new Size(120, 22);
            lblIvaCap.TabIndex = 3;
            lblIvaCap.Text = "IVA (13%)";
            //
            // lblSubtotalVal
            //
            lblSubtotalVal.BackColor = Color.Transparent;
            lblSubtotalVal.Location = new Point(208, 28);
            lblSubtotalVal.Name = "lblSubtotalVal";
            lblSubtotalVal.Size = new Size(120, 22);
            lblSubtotalVal.TabIndex = 2;
            lblSubtotalVal.Text = "$0.00";
            lblSubtotalVal.TextAlignment = ContentAlignment.MiddleRight;
            //
            // lblSubtotalCap
            //
            lblSubtotalCap.BackColor = Color.Transparent;
            lblSubtotalCap.Location = new Point(0, 28);
            lblSubtotalCap.Name = "lblSubtotalCap";
            lblSubtotalCap.Size = new Size(120, 22);
            lblSubtotalCap.TabIndex = 1;
            lblSubtotalCap.Text = "Subtotal";
            //
            // lblTicketSub
            //
            lblTicketSub.BackColor = Color.Transparent;
            lblTicketSub.Location = new Point(19, 48);
            lblTicketSub.Name = "lblTicketSub";
            lblTicketSub.Size = new Size(200, 22);
            lblTicketSub.TabIndex = 1;
            lblTicketSub.Text = "0 unidades";
            //
            // lblTicketTitulo
            //
            lblTicketTitulo.BackColor = Color.Transparent;
            lblTicketTitulo.Location = new Point(19, 18);
            lblTicketTitulo.Name = "lblTicketTitulo";
            lblTicketTitulo.Size = new Size(200, 30);
            lblTicketTitulo.TabIndex = 0;
            lblTicketTitulo.Text = "Ticket actual";
            //
            // ucPuntoVenta
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlProductos);
            Controls.Add(pnlTicket);
            Name = "ucPuntoVenta";
            Size = new Size(1120, 650);
            pnlProductos.ResumeLayout(false);
            pnlBarra.ResumeLayout(false);
            pnlTicket.ResumeLayout(false);
            pnlTotales.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlProductos;
        private Guna.UI2.WinForms.Guna2Panel pnlBarra;
        private Guna.UI2.WinForms.Guna2TextBox txtBuscar;
        private FlowLayoutPanel flpCategorias;
        private FlowLayoutPanel flpProductos;
        private Guna.UI2.WinForms.Guna2Panel pnlTicket;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTicketTitulo;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTicketSub;
        private FlowLayoutPanel flpTicket;
        private Guna.UI2.WinForms.Guna2Panel pnlTotales;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSubtotalCap;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSubtotalVal;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblIvaCap;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblIvaVal;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTotalCap;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTotalVal;
        private Guna.UI2.WinForms.Guna2Button btnCobrar;
    }
}
