using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Heladeria_FMO.Utileria;

namespace Heladeria_FMO.Intefaz.PuntoVenta
{
    // Diálogo de cobro: muestra el desglose (subtotal, descuento, IVA, total),
    // permite aplicar un descuento autorizado por supervisor, captura el
    // efectivo recibido y calcula el cambio en vivo. Valida efectivo >= total.
    public class FrmCobro : Form
    {
        private readonly decimal _subtotal;
        private readonly decimal _ivaTasa;

        private Guna2HtmlLabel lblSubtotalVal;
        private Guna2HtmlLabel lblDescuentoVal;
        private Guna2HtmlLabel lblIvaVal;
        private Guna2HtmlLabel lblTotalVal;
        private Guna2TextBox txtEfectivo;
        private Guna2HtmlLabel lblCambioVal;
        private Guna2Button btnDescuento;
        private Guna2Button btnCobrar;

        // Resultados (válidos solo si DialogResult == OK).
        public decimal Total { get; private set; }
        public decimal Descuento { get; private set; }
        public int IdAutorizadoDescuento { get; private set; }
        public decimal Efectivo { get; private set; }
        public decimal Cambio { get; private set; }

        public FrmCobro(decimal subtotal, decimal ivaTasa)
        {
            _subtotal = subtotal;
            _ivaTasa = ivaTasa;
            ConstruirInterfaz();
            Recalcular();
        }

        private void ConstruirInterfaz()
        {
            Text = "Cobrar venta";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(420, 420);
            BackColor = EstilosFmo.Fondo;

            var tarjeta = new Guna2Panel { Size = new Size(380, 380), Location = new Point(20, 20) };
            EstilosFmo.Tarjeta(tarjeta);

            var titulo = new Guna2HtmlLabel
            {
                Text = "Cobro",
                Location = new Point(20, 16),
                Size = new Size(200, 26),
                Font = EstilosFmo.Fuente(14F, FontStyle.Bold),
                ForeColor = EstilosFmo.TextoFuerte,
                BackColor = Color.Transparent
            };

            int y = 56;
            lblSubtotalVal = FilaResumen(tarjeta, "Subtotal", ref y);
            lblDescuentoVal = FilaResumen(tarjeta, "Descuento", ref y);
            lblIvaVal = FilaResumen(tarjeta, "IVA", ref y);

            var capTotal = new Guna2HtmlLabel
            {
                Text = "TOTAL",
                Location = new Point(20, y + 6),
                Size = new Size(150, 28),
                Font = EstilosFmo.Fuente(13F, FontStyle.Bold),
                ForeColor = EstilosFmo.TextoFuerte,
                BackColor = Color.Transparent
            };
            lblTotalVal = new Guna2HtmlLabel
            {
                Text = "$0.00",
                Location = new Point(190, y + 4),
                Size = new Size(170, 30),
                Font = EstilosFmo.Fuente(17F, FontStyle.Bold),
                ForeColor = EstilosFmo.MentaClaro,
                BackColor = Color.Transparent,
                TextAlignment = ContentAlignment.MiddleRight
            };
            tarjeta.Controls.Add(capTotal);
            tarjeta.Controls.Add(lblTotalVal);
            y += 44;

            btnDescuento = new Guna2Button
            {
                Text = "Aplicar descuento (supervisor)",
                Location = new Point(20, y + 4),
                Size = new Size(340, 36)
            };
            EstilosFmo.BotonContorno(btnDescuento);
            btnDescuento.Click += BtnDescuento_Click;
            tarjeta.Controls.Add(btnDescuento);
            y += 50;

            var capEfectivo = new Guna2HtmlLabel
            {
                Text = "Efectivo recibido ($)",
                Location = new Point(20, y),
                Size = new Size(220, 20),
                Font = EstilosFmo.Fuente(9F),
                ForeColor = EstilosFmo.TextoTenue,
                BackColor = Color.Transparent
            };
            txtEfectivo = new Guna2TextBox
            {
                PlaceholderText = "0.00",
                Location = new Point(20, y + 22),
                Size = new Size(170, 38)
            };
            EstilosFmo.CajaTexto(txtEfectivo);
            txtEfectivo.TextChanged += (s, e) => Recalcular();

            var capCambio = new Guna2HtmlLabel
            {
                Text = "Cambio",
                Location = new Point(210, y),
                Size = new Size(150, 20),
                Font = EstilosFmo.Fuente(9F),
                ForeColor = EstilosFmo.TextoTenue,
                BackColor = Color.Transparent
            };
            lblCambioVal = new Guna2HtmlLabel
            {
                Text = "$0.00",
                Location = new Point(210, y + 24),
                Size = new Size(150, 30),
                Font = EstilosFmo.Fuente(15F, FontStyle.Bold),
                ForeColor = EstilosFmo.TextoFuerte,
                BackColor = Color.Transparent,
                TextAlignment = ContentAlignment.MiddleRight
            };
            tarjeta.Controls.AddRange(new Control[] { capEfectivo, txtEfectivo, capCambio, lblCambioVal });
            y += 70;

            btnCobrar = new Guna2Button
            {
                Text = "Confirmar cobro",
                Location = new Point(20, y),
                Size = new Size(340, 42)
            };
            EstilosFmo.BotonSecundario(btnCobrar);
            btnCobrar.Click += BtnCobrar_Click;
            tarjeta.Controls.Add(btnCobrar);

            Controls.Add(tarjeta);
        }

        // Crea una fila "etiqueta ............ valor" y devuelve el label del valor.
        private Guna2HtmlLabel FilaResumen(Control padre, string etiqueta, ref int y)
        {
            var cap = new Guna2HtmlLabel
            {
                Text = etiqueta,
                Location = new Point(20, y),
                Size = new Size(180, 20),
                Font = EstilosFmo.Fuente(9.5F),
                ForeColor = EstilosFmo.TextoTenue,
                BackColor = Color.Transparent
            };
            var val = new Guna2HtmlLabel
            {
                Text = "$0.00",
                Location = new Point(190, y),
                Size = new Size(170, 20),
                Font = EstilosFmo.Fuente(9.5F),
                ForeColor = EstilosFmo.TextoCuerpo,
                BackColor = Color.Transparent,
                TextAlignment = ContentAlignment.MiddleRight
            };
            padre.Controls.Add(cap);
            padre.Controls.Add(val);
            y += 26;
            return val;
        }

        private void Recalcular()
        {
            decimal baseNeta = _subtotal - Descuento;
            if (baseNeta < 0) baseNeta = 0;

            decimal iva = Math.Round(baseNeta * _ivaTasa, 2);
            Total = baseNeta + iva;

            lblSubtotalVal.Text = "$" + _subtotal.ToString("N2");
            lblDescuentoVal.Text = "-$" + Descuento.ToString("N2");
            lblIvaVal.Text = "$" + iva.ToString("N2");
            lblTotalVal.Text = "$" + Total.ToString("N2");

            string efectivoTexto = (txtEfectivo.Text ?? "").Trim().Replace(",", ".");
            if (decimal.TryParse(efectivoTexto, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal efectivo))
            {
                decimal cambio = efectivo - Total;
                lblCambioVal.Text = "$" + cambio.ToString("N2");
                lblCambioVal.ForeColor = cambio >= 0 ? EstilosFmo.MentaClaro : EstilosFmo.Cereza;
            }
            else
            {
                lblCambioVal.Text = "$0.00";
                lblCambioVal.ForeColor = EstilosFmo.TextoTenue;
            }
        }

        private void BtnDescuento_Click(object sender, EventArgs e)
        {
            using var dlg = new FrmAutorizarDescuento(_subtotal);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                Descuento = dlg.DescuentoMonto;
                IdAutorizadoDescuento = dlg.IdAutorizadoPor;
                btnDescuento.Text = $"Descuento aplicado: -${Descuento:N2}";
                btnDescuento.Enabled = false;
                Recalcular();
            }
        }

        private void BtnCobrar_Click(object sender, EventArgs e)
        {
            string efectivoTexto = (txtEfectivo.Text ?? "").Trim().Replace(",", ".");
            if (!decimal.TryParse(efectivoTexto, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal efectivo))
            {
                MensajeFmo.Advertencia("Ingresa el efectivo recibido.", "Cobro");
                return;
            }

            if (efectivo < Total)
            {
                MensajeFmo.Advertencia("El efectivo recibido no cubre el total.", "Efectivo insuficiente");
                return;
            }

            Efectivo = efectivo;
            Cambio = efectivo - Total;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
