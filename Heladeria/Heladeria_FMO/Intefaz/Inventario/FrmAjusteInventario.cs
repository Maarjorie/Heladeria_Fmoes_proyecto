using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Heladeria_FMO.Modelos;
using Heladeria_FMO.Servicio;
using Heladeria_FMO.Utileria;

namespace Heladeria_FMO.Intefaz.Inventario
{
    // Solicita un ajuste de stock para un producto. El ajuste NO se aplica de
    // inmediato: queda pendiente de aprobación de un supervisor (módulo de
    // Autorizaciones). El signo lo da el tipo (sobrante = +, merma = -).
    public class FrmAjusteInventario : Form
    {
        private readonly Producto _producto;

        private Guna2ComboBox cboTipo;
        private Guna2NumericUpDown numCantidad;
        private Guna2TextBox txtObservacion;
        private Guna2Button btnEnviar;

        public FrmAjusteInventario(Producto producto)
        {
            _producto = producto;
            ConstruirInterfaz();
        }

        private void ConstruirInterfaz()
        {
            Text = "Ajuste de inventario";
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
                Text = "Solicitar ajuste de stock",
                Location = new Point(20, 16),
                Size = new Size(340, 24),
                Font = EstilosFmo.Fuente(13F, FontStyle.Bold),
                ForeColor = EstilosFmo.TextoFuerte,
                BackColor = Color.Transparent
            };

            var lblProducto = new Guna2HtmlLabel
            {
                Text = $"{_producto.Nombre} · stock actual: {_producto.StockActual}",
                Location = new Point(20, 46),
                Size = new Size(340, 22),
                Font = EstilosFmo.Fuente(9.5F),
                ForeColor = EstilosFmo.TextoTenue,
                BackColor = Color.Transparent
            };

            var lblTipo = Etiqueta("Tipo de ajuste", new Point(20, 80));
            cboTipo = new Guna2ComboBox { Location = new Point(20, 104), Size = new Size(340, 36) };
            EstilosFmo.Combo(cboTipo);
            cboTipo.Items.AddRange(new object[] { "Sobrante (sumar al stock)", "Merma (restar del stock)" });
            cboTipo.SelectedIndex = 1;

            var lblCantidad = Etiqueta("Cantidad", new Point(20, 148));
            numCantidad = new Guna2NumericUpDown
            {
                Location = new Point(20, 172),
                Size = new Size(340, 36),
                Minimum = 1,
                Maximum = 100000,
                Value = 1
            };
            numCantidad.FillColor = EstilosFmo.SuperficieHundida;
            numCantidad.ForeColor = EstilosFmo.TextoFuerte;
            numCantidad.BorderColor = EstilosFmo.Borde;
            numCantidad.BorderRadius = 8;

            var lblObs = Etiqueta("Motivo / observación", new Point(20, 216));
            txtObservacion = new Guna2TextBox
            {
                PlaceholderText = "Ej. producto dañado en bodega",
                Location = new Point(20, 240),
                Size = new Size(340, 36)
            };
            EstilosFmo.CajaTexto(txtObservacion);

            btnEnviar = new Guna2Button { Text = "Enviar a aprobación", Location = new Point(20, 288), Size = new Size(340, 40) };
            EstilosFmo.BotonPrimario(btnEnviar);
            btnEnviar.Click += BtnEnviar_Click;

            tarjeta.Controls.AddRange(new Control[]
            {
                titulo, lblProducto, lblTipo, cboTipo, lblCantidad, numCantidad, lblObs, txtObservacion, btnEnviar
            });
            Controls.Add(tarjeta);
        }

        private Guna2HtmlLabel Etiqueta(string texto, Point ubicacion) => new()
        {
            Text = texto,
            Location = ubicacion,
            Size = new Size(220, 20),
            Font = EstilosFmo.Fuente(9F),
            ForeColor = EstilosFmo.TextoTenue,
            BackColor = Color.Transparent
        };

        private void BtnEnviar_Click(object sender, EventArgs e)
        {
            int cantidad = (int)numCantidad.Value;
            if (cboTipo.SelectedIndex == 1) cantidad = -cantidad; // merma = negativo

            string observacion = txtObservacion.Text.Trim();
            if (string.IsNullOrWhiteSpace(observacion))
            {
                MensajeFmo.Advertencia("Ingresa el motivo del ajuste.", "Ajuste de inventario");
                return;
            }

            try
            {
                InventarioServicio.SolicitarAjuste(
                    _producto.IdProducto, cantidad, observacion, Sesion.UsuarioActivo?.id_Usuario ?? 0);

                MensajeFmo.Exito("El ajuste fue enviado a aprobación de un supervisor.", "Ajuste de inventario");
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MensajeFmo.Error(ex.Message, "Ajuste de inventario");
            }
        }
    }
}
