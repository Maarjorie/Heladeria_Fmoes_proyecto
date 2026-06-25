using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Heladeria_FMO.Utileria;

namespace Heladeria_FMO.Intefaz.PuntoVenta
{
    // Diálogo de cobro: muestra el desglose (subtotal, descuento, IVA, total),
    // permite aplicar un descuento autorizado por supervisor, captura el
    // efectivo recibido y calcula el cambio en vivo. Valida efectivo >= total.
    public partial class FrmCobro : Form
    {
        private readonly decimal _subtotal;
        private readonly decimal _ivaTasa;

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
            InitializeComponent();
            AplicarTema();
            Recalcular();
        }

        // El Diseñador maneja el layout; aquí se aplica el tema oscuro de la app.
        private void AplicarTema()
        {
            BackColor = EstilosFmo.Fondo;
            EstilosFmo.Tarjeta(tarjeta);

            titulo.Font = EstilosFmo.Fuente(14F, FontStyle.Bold);
            titulo.ForeColor = EstilosFmo.TextoFuerte;

            // Filas de resumen (etiqueta + valor).
            foreach (var cap in new[] { capSubtotal, capDescuento, capIva })
            {
                cap.Font = EstilosFmo.Fuente(9.5F);
                cap.ForeColor = EstilosFmo.TextoTenue;
            }
            foreach (var val in new[] { lblSubtotalVal, lblDescuentoVal, lblIvaVal })
            {
                val.Font = EstilosFmo.Fuente(9.5F);
                val.ForeColor = EstilosFmo.TextoCuerpo;
            }

            capTotal.Font = EstilosFmo.Fuente(13F, FontStyle.Bold);
            capTotal.ForeColor = EstilosFmo.TextoFuerte;
            lblTotalVal.Font = EstilosFmo.Fuente(17F, FontStyle.Bold);
            lblTotalVal.ForeColor = EstilosFmo.MentaClaro;

            EstilosFmo.BotonContorno(btnDescuento);

            capEfectivo.Font = EstilosFmo.Fuente(9F);
            capEfectivo.ForeColor = EstilosFmo.TextoTenue;
            capCambio.Font = EstilosFmo.Fuente(9F);
            capCambio.ForeColor = EstilosFmo.TextoTenue;
            EstilosFmo.CajaTexto(txtEfectivo);
            lblCambioVal.Font = EstilosFmo.Fuente(15F, FontStyle.Bold);
            lblCambioVal.ForeColor = EstilosFmo.TextoFuerte;

            EstilosFmo.BotonSecundario(btnCobrar);
        }

        private void TxtEfectivo_TextChanged(object sender, EventArgs e) => Recalcular();

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
