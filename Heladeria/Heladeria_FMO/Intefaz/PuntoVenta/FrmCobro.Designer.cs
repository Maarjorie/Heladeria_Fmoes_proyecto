namespace Heladeria_FMO.Intefaz.PuntoVenta
{
    partial class FrmCobro
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
            capSubtotal = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblSubtotalVal = new Guna.UI2.WinForms.Guna2HtmlLabel();
            capDescuento = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblDescuentoVal = new Guna.UI2.WinForms.Guna2HtmlLabel();
            capIva = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblIvaVal = new Guna.UI2.WinForms.Guna2HtmlLabel();
            capTotal = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblTotalVal = new Guna.UI2.WinForms.Guna2HtmlLabel();
            btnDescuento = new Guna.UI2.WinForms.Guna2Button();
            capEfectivo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtEfectivo = new Guna.UI2.WinForms.Guna2TextBox();
            capCambio = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblCambioVal = new Guna.UI2.WinForms.Guna2HtmlLabel();
            btnCobrar = new Guna.UI2.WinForms.Guna2Button();
            tarjeta.SuspendLayout();
            SuspendLayout();
            //
            // tarjeta
            //
            tarjeta.Controls.Add(titulo);
            tarjeta.Controls.Add(capSubtotal);
            tarjeta.Controls.Add(lblSubtotalVal);
            tarjeta.Controls.Add(capDescuento);
            tarjeta.Controls.Add(lblDescuentoVal);
            tarjeta.Controls.Add(capIva);
            tarjeta.Controls.Add(lblIvaVal);
            tarjeta.Controls.Add(capTotal);
            tarjeta.Controls.Add(lblTotalVal);
            tarjeta.Controls.Add(btnDescuento);
            tarjeta.Controls.Add(capEfectivo);
            tarjeta.Controls.Add(txtEfectivo);
            tarjeta.Controls.Add(capCambio);
            tarjeta.Controls.Add(lblCambioVal);
            tarjeta.Controls.Add(btnCobrar);
            tarjeta.Location = new System.Drawing.Point(20, 20);
            tarjeta.Name = "tarjeta";
            tarjeta.Size = new System.Drawing.Size(380, 380);
            tarjeta.TabIndex = 0;
            //
            // titulo
            //
            titulo.BackColor = System.Drawing.Color.Transparent;
            titulo.Location = new System.Drawing.Point(20, 16);
            titulo.Name = "titulo";
            titulo.Size = new System.Drawing.Size(200, 26);
            titulo.TabIndex = 0;
            titulo.Text = "Cobro";
            //
            // capSubtotal
            //
            capSubtotal.BackColor = System.Drawing.Color.Transparent;
            capSubtotal.Location = new System.Drawing.Point(20, 56);
            capSubtotal.Name = "capSubtotal";
            capSubtotal.Size = new System.Drawing.Size(180, 20);
            capSubtotal.TabIndex = 1;
            capSubtotal.Text = "Subtotal";
            //
            // lblSubtotalVal
            //
            lblSubtotalVal.BackColor = System.Drawing.Color.Transparent;
            lblSubtotalVal.Location = new System.Drawing.Point(190, 56);
            lblSubtotalVal.Name = "lblSubtotalVal";
            lblSubtotalVal.Size = new System.Drawing.Size(170, 20);
            lblSubtotalVal.TabIndex = 2;
            lblSubtotalVal.Text = "$0.00";
            lblSubtotalVal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            //
            // capDescuento
            //
            capDescuento.BackColor = System.Drawing.Color.Transparent;
            capDescuento.Location = new System.Drawing.Point(20, 82);
            capDescuento.Name = "capDescuento";
            capDescuento.Size = new System.Drawing.Size(180, 20);
            capDescuento.TabIndex = 3;
            capDescuento.Text = "Descuento";
            //
            // lblDescuentoVal
            //
            lblDescuentoVal.BackColor = System.Drawing.Color.Transparent;
            lblDescuentoVal.Location = new System.Drawing.Point(190, 82);
            lblDescuentoVal.Name = "lblDescuentoVal";
            lblDescuentoVal.Size = new System.Drawing.Size(170, 20);
            lblDescuentoVal.TabIndex = 4;
            lblDescuentoVal.Text = "$0.00";
            lblDescuentoVal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            //
            // capIva
            //
            capIva.BackColor = System.Drawing.Color.Transparent;
            capIva.Location = new System.Drawing.Point(20, 108);
            capIva.Name = "capIva";
            capIva.Size = new System.Drawing.Size(180, 20);
            capIva.TabIndex = 5;
            capIva.Text = "IVA";
            //
            // lblIvaVal
            //
            lblIvaVal.BackColor = System.Drawing.Color.Transparent;
            lblIvaVal.Location = new System.Drawing.Point(190, 108);
            lblIvaVal.Name = "lblIvaVal";
            lblIvaVal.Size = new System.Drawing.Size(170, 20);
            lblIvaVal.TabIndex = 6;
            lblIvaVal.Text = "$0.00";
            lblIvaVal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            //
            // capTotal
            //
            capTotal.BackColor = System.Drawing.Color.Transparent;
            capTotal.Location = new System.Drawing.Point(20, 140);
            capTotal.Name = "capTotal";
            capTotal.Size = new System.Drawing.Size(150, 28);
            capTotal.TabIndex = 7;
            capTotal.Text = "TOTAL";
            //
            // lblTotalVal
            //
            lblTotalVal.BackColor = System.Drawing.Color.Transparent;
            lblTotalVal.Location = new System.Drawing.Point(190, 138);
            lblTotalVal.Name = "lblTotalVal";
            lblTotalVal.Size = new System.Drawing.Size(170, 30);
            lblTotalVal.TabIndex = 8;
            lblTotalVal.Text = "$0.00";
            lblTotalVal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            //
            // btnDescuento
            //
            btnDescuento.Location = new System.Drawing.Point(20, 182);
            btnDescuento.Name = "btnDescuento";
            btnDescuento.Size = new System.Drawing.Size(340, 36);
            btnDescuento.TabIndex = 9;
            btnDescuento.Text = "Aplicar descuento (supervisor)";
            btnDescuento.Click += BtnDescuento_Click;
            //
            // capEfectivo
            //
            capEfectivo.BackColor = System.Drawing.Color.Transparent;
            capEfectivo.Location = new System.Drawing.Point(20, 228);
            capEfectivo.Name = "capEfectivo";
            capEfectivo.Size = new System.Drawing.Size(220, 20);
            capEfectivo.TabIndex = 10;
            capEfectivo.Text = "Efectivo recibido ($)";
            //
            // txtEfectivo
            //
            txtEfectivo.Location = new System.Drawing.Point(20, 250);
            txtEfectivo.Name = "txtEfectivo";
            txtEfectivo.PlaceholderText = "0.00";
            txtEfectivo.Size = new System.Drawing.Size(170, 38);
            txtEfectivo.TabIndex = 11;
            txtEfectivo.TextChanged += TxtEfectivo_TextChanged;
            //
            // capCambio
            //
            capCambio.BackColor = System.Drawing.Color.Transparent;
            capCambio.Location = new System.Drawing.Point(210, 228);
            capCambio.Name = "capCambio";
            capCambio.Size = new System.Drawing.Size(150, 20);
            capCambio.TabIndex = 12;
            capCambio.Text = "Cambio";
            //
            // lblCambioVal
            //
            lblCambioVal.BackColor = System.Drawing.Color.Transparent;
            lblCambioVal.Location = new System.Drawing.Point(210, 252);
            lblCambioVal.Name = "lblCambioVal";
            lblCambioVal.Size = new System.Drawing.Size(150, 30);
            lblCambioVal.TabIndex = 13;
            lblCambioVal.Text = "$0.00";
            lblCambioVal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            //
            // btnCobrar
            //
            btnCobrar.Location = new System.Drawing.Point(20, 298);
            btnCobrar.Name = "btnCobrar";
            btnCobrar.Size = new System.Drawing.Size(340, 42);
            btnCobrar.TabIndex = 14;
            btnCobrar.Text = "Confirmar cobro";
            btnCobrar.Click += BtnCobrar_Click;
            //
            // FrmCobro
            //
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(420, 420);
            Controls.Add(tarjeta);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmCobro";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Cobrar venta";
            tarjeta.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel tarjeta;
        private Guna.UI2.WinForms.Guna2HtmlLabel titulo;
        private Guna.UI2.WinForms.Guna2HtmlLabel capSubtotal;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSubtotalVal;
        private Guna.UI2.WinForms.Guna2HtmlLabel capDescuento;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblDescuentoVal;
        private Guna.UI2.WinForms.Guna2HtmlLabel capIva;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblIvaVal;
        private Guna.UI2.WinForms.Guna2HtmlLabel capTotal;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTotalVal;
        private Guna.UI2.WinForms.Guna2Button btnDescuento;
        private Guna.UI2.WinForms.Guna2HtmlLabel capEfectivo;
        private Guna.UI2.WinForms.Guna2TextBox txtEfectivo;
        private Guna.UI2.WinForms.Guna2HtmlLabel capCambio;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblCambioVal;
        private Guna.UI2.WinForms.Guna2Button btnCobrar;
    }
}
