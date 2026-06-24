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
        private Guna.UI2.WinForms.Guna2Button btnClientes;

        public ucMayorista()
        {
            InitializeComponent();
            AplicarTema();
            CrearBotonClientes();
            CargarPedidos();
        }

        // Botón para abrir la gestión de clientes mayoristas (catálogo).
        private void CrearBotonClientes()
        {
            btnClientes = new Guna.UI2.WinForms.Guna2Button
            {
                Text = "Clientes",
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new System.Drawing.Point(390, 12),
                Size = new System.Drawing.Size(120, 42),
                // Solo el administrador gestiona (alta/edición) de clientes mayoristas.
                Visible = Sesion.EsAdministrador
            };
            EstilosFmo.BotonContorno(btnClientes);
            btnClientes.Click += (s, e) =>
            {
                using var dialogo = new Dialogos.FrmClientes();
                dialogo.ShowDialog(this.FindForm());
            };
            pnlHeader.Controls.Add(btnClientes);
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
        }

        private void CargarPedidos()
        {
            try
            {
                DataTable pedidos = PedidoMayoristaServicio.ListarPedidos();
                dgvPedidos.DataSource = pedidos;
                _colIdPedido = EstilosFmo.ColumnaPorCandidatos(pedidos, "id_pedido", "idpedido", "id");
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
            int id = ObtenerIdPedidoSeleccionado();
            if (id <= 0)
            {
                MensajeFmo.Info("Selecciona un pedido de la tabla.", "Mayorista");
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
                CargarPedidos();
            }
            catch (Exception ex)
            {
                MensajeFmo.Error(ex.Message, "Mayorista");
            }
        }

        private void btnRefrescar_Click(object sender, EventArgs e) => CargarPedidos();

        private void dgvPedidos_SelectionChanged(object sender, EventArgs e) { /* reservado */ }
    }
}
