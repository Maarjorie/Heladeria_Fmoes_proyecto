using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Heladeria_FMO.Acceso_a_datos_db;
using Heladeria_FMO.Modelos;
using Heladeria_FMO.Servicio;
using Heladeria_FMO.Utileria;

namespace Heladeria_FMO.Intefaz.Vendedores.Dialogos
{
    // Diálogo flotante para registrar una ficha de salida: vendedor, ruta, vehículo,
    // comisión y la carga de productos que sale a la calle. La carga se descuenta
    // del inventario al confirmar (lo hace el procedimiento de carga).
    public class FrmNuevaFicha : Form
    {
        private sealed class LineaCarga
        {
            public int IdProducto { get; init; }
            public string Nombre { get; init; }
            public int Cantidad { get; set; }
        }

        private sealed class Item
        {
            public int Id { get; init; }
            public string Nombre { get; init; }
            public override string ToString() => Nombre;
        }

        private readonly Guna2ComboBox cboVendedor = new();
        private readonly Guna2ComboBox cboRuta = new();
        private readonly Guna2TextBox txtVehiculo = new();
        private readonly Guna2TextBox txtComision = new();
        private readonly Guna2ComboBox cboProducto = new();
        private readonly Guna2TextBox txtCantidad = new();
        private readonly Guna2Button btnAgregar = new();
        private readonly Guna2DataGridView dgvCarga = new();
        private readonly Guna2Button btnCrear = new();
        private readonly Guna2Button btnCancelar = new();

        private readonly List<LineaCarga> _carga = new();

        public FrmNuevaFicha()
        {
            ConstruirUi();
            CargarVendedores();
            CargarRutas();
            CargarProductos();
            RefrescarCarga();
        }

        private void ConstruirUi()
        {
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            ShowInTaskbar = false;
            ClientSize = new Size(560, 620);
            BackColor = EstilosFmo.Superficie;

            var marco = new Guna2Panel { Dock = DockStyle.Fill };
            EstilosFmo.Tarjeta(marco);
            Controls.Add(marco);

            var titulo = new Guna2HtmlLabel
            {
                Text = "Nueva ficha de salida",
                Location = new Point(24, 20),
                Size = new Size(420, 30),
                Font = EstilosFmo.Fuente(16F, FontStyle.Bold),
                ForeColor = EstilosFmo.TextoFuerte,
                BackColor = Color.Transparent
            };
            var subtitulo = new Guna2HtmlLabel
            {
                Text = "Asigna ruta, vendedor y carga de productos para la jornada.",
                Location = new Point(24, 50),
                Size = new Size(470, 20),
                Font = EstilosFmo.Fuente(9.5F),
                ForeColor = EstilosFmo.TextoTenue,
                BackColor = Color.Transparent
            };
            var btnCerrar = new Guna2Button
            {
                Text = "✕",
                Size = new Size(32, 32),
                Font = EstilosFmo.Fuente(10F, FontStyle.Bold),
                Location = new Point(500, 16)
            };
            EstilosFmo.BotonContorno(btnCerrar);
            btnCerrar.Click += (s, e) => Close();

            var lblVendedor = Caption("Vendedor ambulante", 24, 84);
            EstilosFmo.Combo(cboVendedor);
            cboVendedor.Location = new Point(24, 106);
            cboVendedor.Size = new Size(240, 34);

            var lblRuta = Caption("Ruta asignada", 288, 84);
            EstilosFmo.Combo(cboRuta);
            cboRuta.Location = new Point(288, 106);
            cboRuta.Size = new Size(224, 34);

            var lblVehiculo = Caption("Vehículo / carreta", 24, 150);
            EstilosFmo.CajaTexto(txtVehiculo);
            txtVehiculo.Location = new Point(24, 172);
            txtVehiculo.Size = new Size(240, 34);
            txtVehiculo.PlaceholderText = "Carreta 03";

            var lblComision = Caption("Comisión (%)", 288, 150);
            EstilosFmo.CajaTexto(txtComision);
            txtComision.Location = new Point(288, 172);
            txtComision.Size = new Size(224, 34);
            txtComision.PlaceholderText = "15";

            var lblProducto = Caption("Producto", 24, 216);
            EstilosFmo.Combo(cboProducto);
            cboProducto.Location = new Point(24, 238);
            cboProducto.Size = new Size(240, 34);

            var lblCant = Caption("Cantidad", 288, 216);
            EstilosFmo.CajaTexto(txtCantidad);
            txtCantidad.Location = new Point(288, 238);
            txtCantidad.Size = new Size(110, 34);
            txtCantidad.PlaceholderText = "100";

            btnAgregar.Text = "Agregar";
            EstilosFmo.BotonContorno(btnAgregar);
            btnAgregar.Location = new Point(414, 237);
            btnAgregar.Size = new Size(98, 36);
            btnAgregar.Click += btnAgregar_Click;

            EstilosFmo.Tabla(dgvCarga);
            dgvCarga.Location = new Point(24, 288);
            dgvCarga.Size = new Size(488, 200);

            var info = new Guna2HtmlLabel
            {
                Text = "La carga se descuenta del inventario al confirmar la ficha.",
                Location = new Point(24, 498),
                Size = new Size(488, 22),
                Font = EstilosFmo.Fuente(9F),
                ForeColor = EstilosFmo.Mango,
                BackColor = Color.Transparent
            };

            btnCancelar.Text = "Cancelar";
            EstilosFmo.BotonContorno(btnCancelar);
            btnCancelar.Location = new Point(300, 564);
            btnCancelar.Size = new Size(100, 40);
            btnCancelar.Click += (s, e) => Close();

            btnCrear.Text = "Confirmar ficha";
            EstilosFmo.BotonPrimario(btnCrear);
            btnCrear.Location = new Point(408, 564);
            btnCrear.Size = new Size(104, 40);
            btnCrear.Click += btnCrear_Click;

            marco.Controls.AddRange(new Control[]
            {
                titulo, subtitulo, btnCerrar,
                lblVendedor, cboVendedor, lblRuta, cboRuta,
                lblVehiculo, txtVehiculo, lblComision, txtComision,
                lblProducto, cboProducto, lblCant, txtCantidad, btnAgregar,
                dgvCarga, info, btnCancelar, btnCrear
            });
        }

        private static Guna2HtmlLabel Caption(string texto, int x, int y) => new()
        {
            Text = texto,
            Location = new Point(x, y),
            Size = new Size(240, 20),
            Font = EstilosFmo.Fuente(9.5F),
            ForeColor = EstilosFmo.TextoTenue,
            BackColor = Color.Transparent
        };

        // ───────────────────────── Carga de combos ─────────────────────────
        private void CargarVendedores()
        {
            try
            {
                List<Vendedor> vendedores = VendedorServicio.ListarVendedores();
                var items = vendedores
                    .Where(v => v.Estado)
                    .Select(v => new Item { Id = v.IdVendedor, Nombre = v.Nombre })
                    .ToList();

                cboVendedor.DataSource = items;
                cboVendedor.DisplayMember = nameof(Item.Nombre);
                cboVendedor.ValueMember = nameof(Item.Id);
                cboVendedor.SelectedIndex = items.Count > 0 ? 0 : -1;
            }
            catch (Exception)
            {
                MensajeFmo.Advertencia("No se pudieron cargar los vendedores.", "Nueva ficha");
            }
        }

        private void CargarRutas()
        {
            try
            {
                DataTable tabla = RutaServicio.ListarRutas();
                string colId = EstilosFmo.ColumnaPorCandidatos(tabla, "id_ruta", "idruta", "id");
                string colNombre = EstilosFmo.ColumnaPorCandidatos(tabla, "nombre", "ruta");

                var items = new List<Item>();
                foreach (DataRow fila in tabla.Rows)
                {
                    int id = colId != null && int.TryParse(fila[colId]?.ToString(), out int v) ? v : 0;
                    string nombre = colNombre != null ? fila[colNombre]?.ToString() : $"Ruta {id}";
                    items.Add(new Item { Id = id, Nombre = nombre });
                }

                cboRuta.DataSource = items;
                cboRuta.DisplayMember = nameof(Item.Nombre);
                cboRuta.ValueMember = nameof(Item.Id);
                cboRuta.SelectedIndex = items.Count > 0 ? 0 : -1;
            }
            catch (Exception)
            {
                MensajeFmo.Advertencia("No se pudieron cargar las rutas.", "Nueva ficha");
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
                MensajeFmo.Advertencia("No se pudieron cargar los productos.", "Nueva ficha");
            }
        }

        // ───────────────────────── Carga en memoria ─────────────────────────
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cboProducto.SelectedItem is not Producto producto)
            {
                MensajeFmo.Info("Selecciona un producto.", "Nueva ficha");
                return;
            }

            if (!int.TryParse(txtCantidad.Text.Trim(), out int cantidad) || cantidad <= 0)
            {
                MensajeFmo.Advertencia("Ingresa una cantidad válida (mayor que cero).", "Datos inválidos");
                txtCantidad.Focus();
                return;
            }

            var existente = _carga.FirstOrDefault(l => l.IdProducto == producto.IdProducto);
            if (existente != null)
                existente.Cantidad += cantidad;
            else
                _carga.Add(new LineaCarga { IdProducto = producto.IdProducto, Nombre = producto.Nombre, Cantidad = cantidad });

            txtCantidad.Clear();
            RefrescarCarga();
        }

        private void RefrescarCarga()
        {
            var tabla = new DataTable();
            tabla.Columns.Add("Producto");
            tabla.Columns.Add("Cantidad", typeof(int));

            foreach (var linea in _carga)
                tabla.Rows.Add(linea.Nombre, linea.Cantidad);

            dgvCarga.DataSource = tabla;
        }

        // ───────────────────────── Confirmar ─────────────────────────
        private void btnCrear_Click(object sender, EventArgs e)
        {
            if (cboVendedor.SelectedValue is not int idVendedor || idVendedor <= 0)
            {
                MensajeFmo.Info("Selecciona un vendedor.", "Nueva ficha");
                return;
            }

            if (cboRuta.SelectedValue is not int idRuta || idRuta <= 0)
            {
                MensajeFmo.Info("Selecciona una ruta.", "Nueva ficha");
                return;
            }

            string comisionTexto = (txtComision.Text ?? "").Trim().Replace(",", ".");
            if (!decimal.TryParse(comisionTexto, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal comision) || comision < 0)
            {
                MensajeFmo.Advertencia("Ingresa una comisión válida (mayor o igual a 0).", "Datos inválidos");
                txtComision.Focus();
                return;
            }

            if (_carga.Count == 0)
            {
                MensajeFmo.Info("Agrega al menos un producto a la carga.", "Nueva ficha");
                return;
            }

            try
            {
                int idUsuario = Sesion.UsuarioActivo?.id_Usuario ?? 0;

                var salida = new Salida_Ruta
                {
                    IdVendedor = idVendedor,
                    IdRuta = idRuta,
                    IdUsuario = idUsuario,
                    Fecha = DateTime.Today,
                    HoraSalida = DateTime.Now.TimeOfDay,
                    Vehiculo = txtVehiculo.Text.Trim(),
                    Comision = comision
                };

                int idSalida = SalidaDAO.RegistrarSalida(salida);

                foreach (var linea in _carga)
                {
                    Detalle_salida_rutaDAO.RegistrarCargaProductoRuta(new Detalle_salida_ruta
                    {
                        IdSalida = idSalida,
                        IdProducto = linea.IdProducto,
                        CantidadCarga = linea.Cantidad
                    }, idUsuario);
                }

                MensajeFmo.Info($"Ficha de salida #{idSalida} registrada con {_carga.Count} producto(s).",
                    "Ficha creada");

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MensajeFmo.Error($"No se pudo registrar la ficha.\n{ex.Message}", "Error");
            }
        }
    }
}
