using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Heladeria_FMO.Intefaz.Mayorista.Dialogos;
using Heladeria_FMO.Modelos;
using Heladeria_FMO.Servicio;
using Heladeria_FMO.Utileria;

namespace Heladeria_FMO.Intefaz.Mayorista
{
    // Listado de pedidos mayoristas con acciones de confirmar y entregar, y
    // apertura del diálogo para crear un nuevo pedido.
    public partial class ucMayorista : UserControl
    {
        // Nombre real de la columna que contiene el id del pedido en el DataTable.
        private string _colIdPedido;
        private string _colCodRetiro;

        public ucMayorista()
        {
            InitializeComponent();
            AplicarTema();

            // El lector de código de retiro escribe el código y envía Enter.
            txtCodigoEntrega.KeyDown += (s, e) =>
            {
                if (e.KeyCode != Keys.Enter) return;
                e.SuppressKeyPress = true;
                IntentarEntregar(txtCodigoEntrega.Text.Trim());
            };

            // Solo el administrador gestiona (alta/edición) de clientes mayoristas.
            btnClientes.Visible = Sesion.EsAdministrador;

            CargarPedidos();
        }

        // Abre la gestión de clientes mayoristas (catálogo).
        private void btnClientes_Click(object sender, EventArgs e)
        {
            using var dialogo = new Dialogos.FrmClientes();
            dialogo.ShowDialog(this.FindForm());
        }

        private void AplicarTema()
        {
            BackColor = EstilosFmo.Fondo;
            pnlHeader.FillColor = EstilosFmo.Fondo;
            EstilosFmo.Tarjeta(pnlCard);
            EstilosFmo.Tabla(dgvPedidos);

            lblTitulo.Font = EstilosFmo.Fuente(18F, FontStyle.Bold);
            lblTitulo.ForeColor = EstilosFmo.TextoFuerte;

            EstilosFmo.BotonPrimario(btnNuevoPedido);
            EstilosFmo.BotonSecundario(btnConfirmar);
            EstilosFmo.BotonContorno(btnEntregar);
            EstilosFmo.BotonContorno(btnRefrescar);
            EstilosFmo.BotonContorno(btnClientes);
            EstilosFmo.CajaTexto(txtCodigoEntrega);
        }

        private void CargarPedidos()
        {
            try
            {
                DataTable pedidos = PedidoMayoristaServicio.ListarPedidos();
                dgvPedidos.DataSource = pedidos;
                _colIdPedido = EstilosFmo.ColumnaPorCandidatos(pedidos, "id_pedido", "idpedido", "id");
                _colCodRetiro = EstilosFmo.ColumnaPorCandidatos(pedidos, "codigo_retiro", "codigoretiro");
            }
            catch (Exception)
            {
                MensajeFmo.Advertencia("No se pudieron cargar los pedidos. Verifica la conexión con la base de datos.",
                    "Mayorista");
            }
        }

        // Devuelve el id del pedido seleccionado, o 0 si no hay selección válida.
        private int ObtenerIdPedidoSeleccionado()
        {
            if (dgvPedidos.CurrentRow == null || _colIdPedido == null)
                return 0;

            object valor = dgvPedidos.CurrentRow.Cells[_colIdPedido].Value;
            return valor != null && int.TryParse(valor.ToString(), out int id) ? id : 0;
        }

        private void btnNuevoPedido_Click(object sender, EventArgs e)
        {
            using (var dialogo = new FrmNuevoPedido())
            {
                dialogo.ShowDialog(this.FindForm());
            }
            CargarPedidos();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            int id = ObtenerIdPedidoSeleccionado();
            if (id <= 0)
            {
                MensajeFmo.Info("Selecciona un pedido de la tabla.", "Mayorista");
                return;
            }

            try
            {
                bool ok = PedidoMayoristaServicio.ConfirmarPedido(new Pedido_mayorista { IdPedido = id });
                if (ok) MensajeFmo.Exito("Pedido confirmado.", "Mayorista"); else MensajeFmo.Advertencia("No se pudo confirmar el pedido.", "Mayorista");
                CargarPedidos();
            }
            catch (Exception ex)
            {
                MensajeFmo.Error(ex.Message, "Mayorista");
            }
        }

        private void btnEntregar_Click(object sender, EventArgs e)
        {
            IntentarEntregar(txtCodigoEntrega?.Text?.Trim() ?? "");
        }

        // Entrega el pedido seleccionado solo si el código presentado coincide
        // con el código de retiro del pedido (validación por escaneo/QR).
        private void IntentarEntregar(string codigoPresentado)
        {
            int id = ObtenerIdPedidoSeleccionado();
            if (id <= 0)
            {
                MensajeFmo.Info("Selecciona un pedido de la tabla.", "Mayorista");
                return;
            }

            string codigoPedido = CodigoRetiroSeleccionado();
            if (string.IsNullOrWhiteSpace(codigoPresentado))
            {
                MensajeFmo.Advertencia("Ingresa o escanea el código de retiro del cliente.", "Mayorista");
                return;
            }

            if (!string.Equals(codigoPresentado, codigoPedido, StringComparison.OrdinalIgnoreCase))
            {
                MensajeFmo.Error("El código presentado no coincide con el del pedido seleccionado.", "Código inválido");
                return;
            }

            try
            {
                var pedido = new Pedido_mayorista
                {
                    IdPedido = id,
                    IdEntregadoPor = Sesion.UsuarioActivo?.id_Usuario ?? 0
                };
                bool ok = PedidoMayoristaServicio.EntregarPedido(pedido);
                if (ok) MensajeFmo.Exito("Pedido entregado.", "Mayorista"); else MensajeFmo.Advertencia("No se pudo entregar el pedido.", "Mayorista");
                if (txtCodigoEntrega != null) txtCodigoEntrega.Clear();
                CargarPedidos();
            }
            catch (Exception ex)
            {
                MensajeFmo.Error(ex.Message, "Mayorista");
            }
        }

        // Código de retiro del pedido seleccionado en la tabla.
        private string CodigoRetiroSeleccionado()
        {
            if (dgvPedidos.CurrentRow == null || _colCodRetiro == null) return "";
            object v = dgvPedidos.CurrentRow.Cells[_colCodRetiro].Value;
            return v?.ToString() ?? "";
        }

        private void btnRefrescar_Click(object sender, EventArgs e) => CargarPedidos();

        private void dgvPedidos_SelectionChanged(object sender, EventArgs e) { /* reservado */ }
    }
}
