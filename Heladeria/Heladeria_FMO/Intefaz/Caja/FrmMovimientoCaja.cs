using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Heladeria_FMO.Servicio;
using Heladeria_FMO.Utileria;

namespace Heladeria_FMO.Intefaz.Caja
{
    // Registra un gasto (egreso) o ingreso extraordinario contra la caja
    // abierta en sesión. Usa CajaServicio.RegistrarMovimiento.
    public partial class FrmMovimientoCaja : Form
    {
        public FrmMovimientoCaja()
        {
            InitializeComponent();
            AplicarTema();
            cboTipo.SelectedIndex = 1; // por defecto egreso (gasto)
        }

        // El Diseñador maneja el layout; aquí se aplica el tema oscuro de la app.
        private void AplicarTema()
        {
            BackColor = EstilosFmo.Fondo;
            EstilosFmo.Tarjeta(tarjeta);

            titulo.Font = EstilosFmo.Fuente(13F, FontStyle.Bold);
            titulo.ForeColor = EstilosFmo.TextoFuerte;

            foreach (var lbl in new[] { lblTipo, lblConcepto, lblMonto })
            {
                lbl.Font = EstilosFmo.Fuente(9F);
                lbl.ForeColor = EstilosFmo.TextoTenue;
            }

            EstilosFmo.Combo(cboTipo);
            EstilosFmo.CajaTexto(txtConcepto);
            EstilosFmo.CajaTexto(txtMonto);
            EstilosFmo.BotonPrimario(btnGuardar);
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (!Sesion.HayCajaAbierta)
            {
                MensajeFmo.Advertencia("No hay una caja abierta.", "Caja");
                return;
            }

            string tipo = (cboTipo.SelectedItem?.ToString() ?? "").ToLowerInvariant(); // "ingreso" / "egreso"
            string concepto = txtConcepto.Text.Trim();

            if (string.IsNullOrWhiteSpace(concepto))
            {
                MensajeFmo.Advertencia("Ingresa el concepto del movimiento.", "Datos incompletos");
                return;
            }

            string montoTexto = (txtMonto.Text ?? "").Trim().Replace(",", ".");
            if (!decimal.TryParse(montoTexto, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal monto) || monto <= 0)
            {
                MensajeFmo.Advertencia("Ingresa un monto válido mayor que 0.", "Datos inválidos");
                return;
            }

            try
            {
                bool ok = CajaServicio.RegistrarMovimiento(
                    Sesion.IdCajaActiva, Sesion.UsuarioActivo.id_Usuario, tipo, concepto, monto);

                if (ok)
                {
                    MensajeFmo.Exito("Movimiento registrado.", "Caja");
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MensajeFmo.Advertencia("No se pudo registrar el movimiento.", "Caja");
                }
            }
            catch (Exception ex)
            {
                MensajeFmo.Error(ex.Message, "Caja");
            }
        }
    }
}
