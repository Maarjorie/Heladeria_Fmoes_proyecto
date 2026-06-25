using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Heladeria_FMO.Modelos;
using Heladeria_FMO.Servicio;
using Heladeria_FMO.Utileria;

namespace Heladeria_FMO.Intefaz.PuntoVenta
{
    // Autoriza un descuento en una venta de sucursal exigiendo las credenciales
    // de un supervisor o administrador EN EL MOMENTO (no es una cola de
    // aprobación). Calcula el monto del descuento sobre el subtotal recibido.
    public class FrmAutorizarDescuento : Form
    {
        private readonly decimal _subtotal;

        private Guna2TextBox txtUsuario;
        private Guna2TextBox txtContrasena;
        private Guna2ComboBox cboTipo;
        private Guna2TextBox txtValor;
        private Guna2Button btnAutorizar;

        // Resultados (válidos solo si DialogResult == OK).
        public decimal DescuentoMonto { get; private set; }
        public int IdAutorizadoPor { get; private set; }

        public FrmAutorizarDescuento(decimal subtotal)
        {
            _subtotal = subtotal;
            ConstruirInterfaz();
        }

        private void ConstruirInterfaz()
        {
            Text = "Autorizar descuento";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(420, 400);
            BackColor = EstilosFmo.Fondo;

            var tarjeta = new Guna2Panel { Size = new Size(380, 360), Location = new Point(20, 20) };
            EstilosFmo.Tarjeta(tarjeta);

            var titulo = new Guna2HtmlLabel
            {
                Text = "Descuento (requiere supervisor)",
                Location = new Point(20, 16),
                Size = new Size(340, 24),
                Font = EstilosFmo.Fuente(12.5F, FontStyle.Bold),
                ForeColor = EstilosFmo.TextoFuerte,
                BackColor = Color.Transparent
            };

            var lblUsuario = Etiqueta("Usuario supervisor", new Point(20, 50));
            txtUsuario = Caja("Usuario", new Point(20, 74));

            var lblContra = Etiqueta("Contraseña", new Point(20, 118));
            txtContrasena = Caja("Contraseña", new Point(20, 142));
            txtContrasena.PasswordChar = '*';

            var lblTipo = Etiqueta("Tipo de descuento", new Point(20, 186));
            cboTipo = new Guna2ComboBox { Location = new Point(20, 210), Size = new Size(340, 36) };
            EstilosFmo.Combo(cboTipo);
            cboTipo.Items.AddRange(new object[] { "Monto fijo ($)", "Porcentaje (%)" });
            cboTipo.SelectedIndex = 0;

            var lblValor = Etiqueta("Valor", new Point(20, 252));
            txtValor = Caja("0.00", new Point(20, 276));

            btnAutorizar = new Guna2Button { Text = "Autorizar", Location = new Point(20, 320), Size = new Size(340, 40) };
            EstilosFmo.BotonPrimario(btnAutorizar);
            btnAutorizar.Click += BtnAutorizar_Click;

            tarjeta.Controls.AddRange(new Control[]
            {
                titulo, lblUsuario, txtUsuario, lblContra, txtContrasena,
                lblTipo, cboTipo, lblValor, txtValor, btnAutorizar
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

        private Guna2TextBox Caja(string placeholder, Point ubicacion)
        {
            var t = new Guna2TextBox { PlaceholderText = placeholder, Location = ubicacion, Size = new Size(340, 36) };
            EstilosFmo.CajaTexto(t);
            return t;
        }

        private void BtnAutorizar_Click(object sender, EventArgs e)
        {
            // 1) Validar credenciales del supervisor/administrador.
            if (string.IsNullOrWhiteSpace(txtUsuario.Text) || string.IsNullOrWhiteSpace(txtContrasena.Text))
            {
                MensajeFmo.Advertencia("Ingresa usuario y contraseña del supervisor.", "Autorización");
                return;
            }

            Acceso resultado = UsuarioServicio.Login(txtUsuario.Text, txtContrasena.Text, out Usuario autorizador);

            if (resultado != Acceso.SesionExitosa || autorizador == null)
            {
                MensajeFmo.Error("Credenciales inválidas o cuenta inactiva.", "Autorización denegada");
                return;
            }

            // Solo administrador (1) o supervisor (2) pueden autorizar descuentos.
            if (autorizador.id_rol != 1 && autorizador.id_rol != 2)
            {
                MensajeFmo.Advertencia("El usuario no tiene permisos para autorizar descuentos.", "Sin permiso");
                return;
            }

            // 2) Validar y calcular el monto del descuento.
            string valorTexto = (txtValor.Text ?? "").Trim().Replace(",", ".");
            if (!decimal.TryParse(valorTexto, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal valor) || valor <= 0)
            {
                MensajeFmo.Advertencia("Ingresa un valor de descuento válido mayor que 0.", "Datos inválidos");
                return;
            }

            decimal descuento = cboTipo.SelectedIndex == 1
                ? Math.Round(_subtotal * (valor / 100m), 2)  // porcentaje
                : valor;                                      // monto fijo

            if (descuento > _subtotal)
            {
                MensajeFmo.Advertencia("El descuento no puede ser mayor que el subtotal.", "Descuento inválido");
                return;
            }

            DescuentoMonto = descuento;
            IdAutorizadoPor = autorizador.id_Usuario;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
