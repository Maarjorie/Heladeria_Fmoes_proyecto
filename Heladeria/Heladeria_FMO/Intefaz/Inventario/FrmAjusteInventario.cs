using System;
using System.Drawing;
using System.Windows.Forms;
using Heladeria_FMO.Modelos;
using Heladeria_FMO.Servicio;
using Heladeria_FMO.Utileria;

namespace Heladeria_FMO.Intefaz.Inventario
{
    // Solicita un ajuste de stock para un producto. El ajuste NO se aplica de
    // inmediato: queda pendiente de aprobación de un supervisor (módulo de
    // Autorizaciones). El signo lo da el tipo (sobrante = +, merma = -).
    public partial class FrmAjusteInventario : Form
    {
        private readonly Producto _producto;

        public FrmAjusteInventario(Producto producto)
        {
            _producto = producto;
            InitializeComponent();
            AplicarTema();

            lblProducto.Text = $"{_producto.Nombre} · stock actual: {_producto.StockActual}";
            cboTipo.SelectedIndex = 1;
        }

        // El Diseñador maneja el layout; aquí se aplica el tema oscuro de la app.
        private void AplicarTema()
        {
            BackColor = EstilosFmo.Fondo;
            EstilosFmo.Tarjeta(tarjeta);

            titulo.Font = EstilosFmo.Fuente(13F, FontStyle.Bold);
            titulo.ForeColor = EstilosFmo.TextoFuerte;

            lblProducto.Font = EstilosFmo.Fuente(9.5F);
            lblProducto.ForeColor = EstilosFmo.TextoTenue;

            foreach (var lbl in new[] { lblTipo, lblCantidad, lblObs })
            {
                lbl.Font = EstilosFmo.Fuente(9F);
                lbl.ForeColor = EstilosFmo.TextoTenue;
            }

            EstilosFmo.Combo(cboTipo);

            numCantidad.FillColor = EstilosFmo.SuperficieHundida;
            numCantidad.ForeColor = EstilosFmo.TextoFuerte;
            numCantidad.BorderColor = EstilosFmo.Borde;
            numCantidad.BorderRadius = 8;

            EstilosFmo.CajaTexto(txtObservacion);
            EstilosFmo.BotonPrimario(btnEnviar);
        }

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
