using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Heladeria_FMO.Acceso_a_datos_db;
using Heladeria_FMO.Modelos;
using Heladeria_FMO.Servicio;
using Heladeria_FMO.Utileria;

namespace Heladeria_FMO.Intefaz.Mayorista.Dialogos
{
    // Diálogo flotante para registrar un nuevo pedido mayorista: selecciona cliente,
    // agrega líneas de producto + cantidad y crea el pedido con su detalle.
    // Nota: los procedimientos de pedido no reciben descuento ni método de pago,
    // por eso esos campos del diseño no se incluyen (no se persistirían).
    public class FrmNuevoPedido : Form
    {
        private sealed class LineaPedido
        {
            public int IdProducto { get; init; }
            public string Nombre { get; init; }
            public decimal Precio { get; init; }
            public int Cantidad { get; set; }
        }

        private sealed class ClienteItem
        {
            public int Id { get; init; }
            public string Nombre { get; init; }
            public decimal Descuento { get; init; } // % fijo del cliente
            public override string ToString() => Nombre;
        }

        private readonly Guna2ComboBox cboCliente = new();
        private readonly Guna2ComboBox cboProducto = new();
        private readonly Guna2TextBox txtCantidad = new();
        private readonly Guna2Button btnAgregar = new();
        private readonly Guna2DateTimePicker dtpEntrega = new();
        private readonly Guna2ToggleSwitch tglProgramar = new();
        private readonly Guna2DataGridView dgvDetalle = new();
        private readonly Guna2HtmlLabel lblTotalVal = new();
        private readonly Guna2Button btnCrear = new();
        private readonly Guna2Button btnCancelar = new();

        private readonly List<LineaPedido> _lineas = new();

        public FrmNuevoPedido()
        {
            ConstruirUi();
            CargarClientes();
            CargarProductos();
            RefrescarDetalle();
        }

        private void ConstruirUi()
        {
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            ShowInTaskbar = false;
            ClientSize = new Size(560, 580);
            BackColor = EstilosFmo.Superficie;

            // Borde sutil alrededor del diálogo.
            var marco = new Guna2Panel { Dock = DockStyle.Fill };
            EstilosFmo.Tarjeta(marco);
            marco.Padding = new Padding(24);
            Controls.Add(marco);

            var titulo = new Guna2HtmlLabel
            {
                Text = "Nuevo pedido mayorista",
                Location = new Point(24, 20),
                Size = new Size(400, 30),
                Font = EstilosFmo.Fuente(16F, FontStyle.Bold),
                ForeColor = EstilosFmo.TextoFuerte,
                BackColor = Color.Transparent
            };

            var subtitulo = new Guna2HtmlLabel
            {
                Text = "Registra un pedido grande con código de retiro.",
                Location = new Point(24, 50),
                Size = new Size(440, 20),
                Font = EstilosFmo.Fuente(9.5F),
                ForeColor = EstilosFmo.TextoTenue,
                BackColor = Color.Transparent
            };

            var btnCerrar = CrearBoton("✕", 32, 32);
            EstilosFmo.BotonContorno(btnCerrar);
            btnCerrar.Location = new Point(500, 16);
            btnCerrar.Click += (s, e) => Close();

            var lblCliente = CrearCaption("Cliente mayorista", 24, 84);
            EstilosFmo.Combo(cboCliente);
            cboCliente.Location = new Point(24, 106);
            cboCliente.Size = new Size(290, 34);

            var lblEntrega = CrearCaption("Entrega programada", 324, 84);
            tglProgramar.Location = new Point(470, 86);
            tglProgramar.Size = new Size(42, 22);
            dtpEntrega.Location = new Point(324, 106);
            dtpEntrega.Size = new Size(188, 34);
            dtpEntrega.FillColor = EstilosFmo.SuperficieHundida;
            dtpEntrega.ForeColor = EstilosFmo.TextoFuerte;
            dtpEntrega.BorderColor = EstilosFmo.Borde;
            dtpEntrega.BorderRadius = 8;
            dtpEntrega.Enabled = false;
            dtpEntrega.MinDate = DateTime.Today;
            dtpEntrega.Value = DateTime.Today.AddDays(1);
            tglProgramar.CheckedChanged += (s, e) => dtpEntrega.Enabled = tglProgramar.Checked;

            var lblProducto = CrearCaption("Producto", 24, 152);
            EstilosFmo.Combo(cboProducto);
            cboProducto.Location = new Point(24, 174);
            cboProducto.Size = new Size(290, 34);

            var lblCant = CrearCaption("Cantidad", 324, 152);
            EstilosFmo.CajaTexto(txtCantidad);
            txtCantidad.Location = new Point(324, 174);
            txtCantidad.Size = new Size(80, 34);
            txtCantidad.PlaceholderText = "0";

            btnAgregar.Text = "Agregar";
            EstilosFmo.BotonContorno(btnAgregar);
            btnAgregar.Location = new Point(414, 173);
            btnAgregar.Size = new Size(98, 36);
            btnAgregar.Click += btnAgregar_Click;

            EstilosFmo.Tabla(dgvDetalle);
            dgvDetalle.Location = new Point(24, 224);
            dgvDetalle.Size = new Size(488, 244);
            dgvDetalle.AllowUserToResizeRows = false;

            var lblTotalCap = new Guna2HtmlLabel
            {
                Text = "Total estimado",
                Location = new Point(24, 484),
                Size = new Size(200, 26),
                Font = EstilosFmo.Fuente(13F, FontStyle.Bold),
                ForeColor = EstilosFmo.TextoFuerte,
                BackColor = Color.Transparent
            };

            lblTotalVal.Text = "$0.00";
            lblTotalVal.Location = new Point(312, 482);
            lblTotalVal.Size = new Size(200, 30);
            lblTotalVal.Font = EstilosFmo.Fuente(18F, FontStyle.Bold);
            lblTotalVal.ForeColor = EstilosFmo.MentaClaro;
            lblTotalVal.BackColor = Color.Transparent;
            lblTotalVal.TextAlignment = ContentAlignment.MiddleRight;

            btnCancelar.Text = "Cancelar";
            EstilosFmo.BotonContorno(btnCancelar);
            btnCancelar.Location = new Point(300, 524);
            btnCancelar.Size = new Size(100, 40);
            btnCancelar.Click += (s, e) => Close();

            btnCrear.Text = "Crear pedido";
            EstilosFmo.BotonPrimario(btnCrear);
            btnCrear.Location = new Point(408, 524);
            btnCrear.Size = new Size(104, 40);
            btnCrear.Click += btnCrear_Click;

            marco.Controls.AddRange(new Control[]
            {
                titulo, subtitulo, btnCerrar,
                lblCliente, cboCliente, lblEntrega, tglProgramar, dtpEntrega,
                lblProducto, cboProducto, lblCant, txtCantidad, btnAgregar,
                dgvDetalle, lblTotalCap, lblTotalVal,
                btnCancelar, btnCrear
            });
        }

        private Guna2HtmlLabel CrearCaption(string texto, int x, int y) => new()
        {
            Text = texto,
            Location = new Point(x, y),
            Size = new Size(260, 20),
            Font = EstilosFmo.Fuente(9.5F),
            ForeColor = EstilosFmo.TextoTenue,
            BackColor = Color.Transparent
        };

        private static Guna2Button CrearBoton(string texto, int w, int h) => new()
        {
            Text = texto,
            Size = new Size(w, h),
            Font = EstilosFmo.Fuente(10F, FontStyle.Bold)
        };

        // ───────────────────────── Carga ─────────────────────────
        private void CargarClientes()
        {
            try
            {
                DataTable tabla = Cliente_mayoristaDAO.ListarClientesMayoristas();
                string colId = EstilosFmo.ColumnaPorCandidatos(tabla, "id_cliente", "idcliente", "id");
                string colNombre = EstilosFmo.ColumnaPorCandidatos(tabla, "nombre_comercial", "nombre", "comercial");
                string colDesc = EstilosFmo.ColumnaPorCandidatos(tabla, "descuento_porcentaje", "descuento");

                var clientes = new List<ClienteItem>();
                foreach (DataRow fila in tabla.Rows)
                {
                    int id = colId != null && int.TryParse(fila[colId]?.ToString(), out int v) ? v : 0;
                    string nombre = colNombre != null ? fila[colNombre]?.ToString() : $"Cliente {id}";
                    decimal desc = 0m;
                    if (colDesc != null) decimal.TryParse(fila[colDesc]?.ToString(), out desc);
                    clientes.Add(new ClienteItem { Id = id, Nombre = nombre, Descuento = desc });
                }

                cboCliente.DataSource = clientes;
                cboCliente.DisplayMember = nameof(ClienteItem.Nombre);
                cboCliente.ValueMember = nameof(ClienteItem.Id);
                cboCliente.SelectedIndex = clientes.Count > 0 ? 0 : -1;
            }
            catch (Exception)
            {
                MensajeFmo.Advertencia("No se pudieron cargar los clientes mayoristas.", "Nuevo pedido");
            }
        }

        private void CargarProductos()
        {
            try
            {
                List<Producto> productos = ProductoDAO.ListarProductos();
                cboProducto.DataSource = productos;
                cboProducto.DisplayMember = nameof(Producto.Nombre);
                cboProducto.ValueMember = nameof(Producto.IdProducto);
                cboProducto.SelectedIndex = productos.Count > 0 ? 0 : -1;
            }
            catch (Exception)
            {
                MensajeFmo.Advertencia("No se pudieron cargar los productos.", "Nuevo pedido");
            }
        }

        // ───────────────────────── Detalle en memoria ─────────────────────────
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cboProducto.SelectedItem is not Producto producto)
            {
                MensajeFmo.Info("Selecciona un producto.", "Nuevo pedido");
                return;
            }

            if (!int.TryParse(txtCantidad.Text.Trim(), out int cantidad) || cantidad <= 0)
            {
                MensajeFmo.Advertencia("Ingresa una cantidad válida (mayor que cero).", "Datos inválidos");
                txtCantidad.Focus();
                return;
            }

            var existente = _lineas.FirstOrDefault(l => l.IdProducto == producto.IdProducto);
            if (existente != null)
                existente.Cantidad += cantidad;
            else
                _lineas.Add(new LineaPedido
                {
                    IdProducto = producto.IdProducto,
                    Nombre = producto.Nombre,
                    Precio = producto.PrecioVenta,
                    Cantidad = cantidad
                });

            txtCantidad.Clear();
            RefrescarDetalle();
        }

        private void RefrescarDetalle()
        {
            var tabla = new DataTable();
            tabla.Columns.Add("Producto");
            tabla.Columns.Add("Cantidad", typeof(int));
            tabla.Columns.Add("Precio", typeof(decimal));
            tabla.Columns.Add("Subtotal", typeof(decimal));

            foreach (var linea in _lineas)
                tabla.Rows.Add(linea.Nombre, linea.Cantidad, linea.Precio, linea.Precio * linea.Cantidad);

            dgvDetalle.DataSource = tabla;

            decimal total = _lineas.Sum(l => l.Precio * l.Cantidad);
            lblTotalVal.Text = "$" + total.ToString("N2");
        }

        // ───────────────────────── Crear ─────────────────────────
        private void btnCrear_Click(object sender, EventArgs e)
        {
            if (cboCliente.SelectedValue is not int idCliente || idCliente <= 0)
            {
                MensajeFmo.Info("Selecciona un cliente mayorista.", "Nuevo pedido");
                return;
            }

            if (_lineas.Count == 0)
            {
                MensajeFmo.Info("Agrega al menos un producto al pedido.", "Nuevo pedido");
                return;
            }

            try
            {
                var pedido = new Pedido_mayorista
                {
                    IdCliente = idCliente,
                    IdAtendidoPor = Sesion.UsuarioActivo?.id_Usuario ?? 0,
                    FechaEntregaProgramada = tglProgramar.Checked ? dtpEntrega.Value : (DateTime?)null
                };

                int idPedido = PedidoMayoristaServicio.CrearPedido(pedido);

                foreach (var linea in _lineas)
                {
                    Detalle_pedido_mayoristaDAO.AgregarDetallePedidoMayorista(new Detalle_pedido_mayorista
                    {
                        IdPedido = idPedido,
                        IdProducto = linea.IdProducto,
                        Cantidad = linea.Cantidad
                    });
                }

                // Aplica el descuento fijo del cliente y fija los totales del pedido.
                decimal subtotal = _lineas.Sum(l => l.Precio * l.Cantidad);
                decimal pct = (cboCliente.SelectedItem as ClienteItem)?.Descuento ?? 0m;
                decimal descuento = Math.Round(subtotal * (pct / 100m), 2);
                PedidoMayoristaServicio.FijarTotales(idPedido, subtotal, descuento);

                string detalleDesc = descuento > 0 ? $" Descuento del cliente: ${descuento:N2}." : "";
                MensajeFmo.Info($"Pedido #{idPedido} creado con {_lineas.Count} producto(s).{detalleDesc}",
                    "Pedido creado");

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MensajeFmo.Error($"No se pudo crear el pedido.\n{ex.Message}", "Error");
            }
        }
    }
}
