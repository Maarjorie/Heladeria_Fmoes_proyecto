using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Heladeria_FMO.Servicio;
using Heladeria_FMO.Utileria;

namespace Heladeria_FMO.Intefaz.Caja
{
    // Registra un gasto (egreso) o ingreso extraordinario contra la caja
    // abierta en sesión. Usa CajaServicio.RegistrarMovimiento.
    public class FrmMovimientoCaja : Form
    {
        private Guna2ComboBox cboTipo;
        private Guna2TextBox txtConcepto;
        private Guna2TextBox txtMonto;
        private Guna2Button btnGuardar;

        public FrmMovimientoCaja()
        {
            ConstruirInterfaz();
        }

        private void ConstruirInterfaz()
        {
            Text = "Movimiento de caja";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(420, 360);
            BackColor = EstilosFmo.Fondo;

            var tarjeta = new Guna2Panel { Size = new Size(380, 320), Location = new Point(20, 20) };
            EstilosFmo.Tarjeta(tarjeta);

            var titulo = new Guna2HtmlLabel
            {
                Text = "Registrar movimiento",
                Location = new Point(20, 18),
                Size = new Size(340, 26),
                Font = EstilosFmo.Fuente(13F, FontStyle.Bold),
                ForeColor = EstilosFmo.TextoFuerte,
                BackColor = Color.Transparent
            };

            var lblTipo = Etiqueta("Tipo", new Point(20, 56));
            cboTipo = new Guna2ComboBox { Location = new Point(20, 80), Size = new Size(340, 36) };
            EstilosFmo.Combo(cboTipo);
            cboTipo.Items.AddRange(new object[] { "Ingreso", "Egreso" });
            cboTipo.SelectedIndex = 1; // por defecto egreso (gasto)

            var lblConcepto = Etiqueta("Concepto", new Point(20, 124));
            txtConcepto = new Guna2TextBox
            {
                PlaceholderText = "Ej. compra de hielo",
                Location = new Point(20, 148),
                Size = new Size(340, 36)
            };
            EstilosFmo.CajaTexto(txtConcepto);

            var lblMonto = Etiqueta("Monto ($)", new Point(20, 192));
            txtMonto = new Guna2TextBox
            {
                PlaceholderText = "0.00",
                Location = new Point(20, 216),
                Size = new Size(340, 36)
            };
            EstilosFmo.CajaTexto(txtMonto);

            btnGuardar = new Guna2Button { Text = "Guardar", Location = new Point(20, 268), Size = new Size(340, 40) };
            EstilosFmo.BotonPrimario(btnGuardar);
            btnGuardar.Click += BtnGuardar_Click;

            tarjeta.Controls.AddRange(new Control[]
            {
                titulo, lblTipo, cboTipo, lblConcepto, txtConcepto, lblMonto, txtMonto, btnGuardar
            });
            Controls.Add(tarjeta);
        }

        private Guna2HtmlLabel Etiqueta(string texto, Point ubicacion) => new()
        {
            Text = texto,
            Location = ubicacion,
            Size = new Size(200, 20),
            Font = EstilosFmo.Fuente(9F),
            ForeColor = EstilosFmo.TextoTenue,
            BackColor = Color.Transparent
        };

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
