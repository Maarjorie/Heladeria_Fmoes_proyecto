using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Heladeria_FMO.Modelos;
using Heladeria_FMO.Servicio;
using Heladeria_FMO.Utileria;

namespace Heladeria_FMO.Intefaz.PuntoVenta
{
    // Autoriza un descuento en una venta de sucursal exigiendo las credenciales
    // de un supervisor o administrador EN EL MOMENTO (no es una cola de
    // aprobación). Calcula el monto del descuento sobre el subtotal recibido.
    public partial class FrmAutorizarDescuento : Form
    {
        private readonly decimal _subtotal;

        // Resultados (válidos solo si DialogResult == OK).
        public decimal DescuentoMonto { get; private set; }
        public int IdAutorizadoPor { get; private set; }

        public FrmAutorizarDescuento(decimal subtotal)
        {
            _subtotal = subtotal;
            InitializeComponent();
            AplicarTema();
            cboTipo.SelectedIndex = 0;
        }

        // El Diseñador maneja el layout; aquí se aplica el tema oscuro de la app.
        private void AplicarTema()
        {
            BackColor = EstilosFmo.Fondo;
            EstilosFmo.Tarjeta(tarjeta);

            titulo.Font = EstilosFmo.Fuente(12.5F, FontStyle.Bold);
            titulo.ForeColor = EstilosFmo.TextoFuerte;

            foreach (var lbl in new[] { lblUsuario, lblContra, lblTipo, lblValor })
            {
                lbl.Font = EstilosFmo.Fuente(9F);
                lbl.ForeColor = EstilosFmo.TextoTenue;
            }

            EstilosFmo.CajaTexto(txtUsuario);
            EstilosFmo.CajaTexto(txtContrasena);
            EstilosFmo.Combo(cboTipo);
            EstilosFmo.CajaTexto(txtValor);
            EstilosFmo.BotonPrimario(btnAutorizar);
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
