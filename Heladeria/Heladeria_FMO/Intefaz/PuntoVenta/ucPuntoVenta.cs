using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Heladeria_FMO.Acceso_a_datos_db;
using Heladeria_FMO.Modelos;
using Heladeria_FMO.Servicio;
using Heladeria_FMO.Utileria;

namespace Heladeria_FMO.Intefaz.PuntoVenta
{
    // Punto de venta: catálogo de productos a la izquierda y ticket en construcción
    // a la derecha. El armado del ticket (cantidades, validación de stock y totales)
    // se resuelve en memoria; la persistencia de la venta se hará cuando exista una
    // caja abierta en sesión (módulo de Caja).
    public partial class ucPuntoVenta : UserControl
    {
        private const decimal IvaTasa = 0.13m;

        // Catálogo cargado desde la base de datos.
        private List<Producto> _productos = new();

        // Promociones vigentes (aprobadas y dentro de fechas) aplicables al ticket.
        private List<Promocion> _promociones = new();

        // Líneas del ticket actual, indexadas por id de producto.
        private readonly Dictionary<int, LineaTicket> _ticket = new();

        // Filtro de categoría activo ("Todos" = sin filtro).
        private string _categoriaActiva = "Todos";

        private sealed class LineaTicket
        {
            public Producto Producto { get; init; }
            public int Cantidad { get; set; }
        }

        public ucPuntoVenta()
        {
            InitializeComponent();
            AplicarTema();
            CargarProductos();
            RecalcularTotales();

            // El buscador funciona también como entrada del lector de código de
            // barras: al presionar Enter, si el texto coincide exactamente con el
            // código (de barras o interno) de un producto, lo agrega al ticket.
            txtBuscar.KeyDown += TxtBuscar_KeyDown;
        }

        private void TxtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            string codigo = txtBuscar.Text.Trim();
            if (codigo.Length == 0) return;

            var producto = _productos.FirstOrDefault(p =>
                string.Equals(p.CodigoBarras, codigo, StringComparison.OrdinalIgnoreCase) ||
                string.Equals(p.Codigo, codigo, StringComparison.OrdinalIgnoreCase));

            if (producto != null)
            {
                AgregarAlTicket(producto);
                txtBuscar.Clear();
                txtBuscar.Focus();
                e.SuppressKeyPress = true;
            }
        }

        // ───────────────────────── Tema oscuro ─────────────────────────
        private void AplicarTema()
        {
            BackColor = EstilosFmo.Fondo;

            pnlProductos.FillColor = EstilosFmo.Fondo;
            pnlBarra.FillColor = EstilosFmo.Fondo;
            flpProductos.BackColor = EstilosFmo.Fondo;
            flpCategorias.BackColor = EstilosFmo.Fondo;

            EstilosFmo.CajaTexto(txtBuscar);

            EstilosFmo.Tarjeta(pnlTicket);
            pnlTotales.FillColor = EstilosFmo.Superficie;
            flpTicket.BackColor = EstilosFmo.Superficie;

            lblTicketTitulo.Font = EstilosFmo.Fuente(15F, FontStyle.Bold);
            lblTicketTitulo.ForeColor = EstilosFmo.TextoFuerte;
            lblTicketSub.Font = EstilosFmo.Fuente(9F);
            lblTicketSub.ForeColor = EstilosFmo.TextoTenue;

            foreach (var cap in new[] { lblSubtotalCap, lblIvaCap })
            {
                cap.Font = EstilosFmo.Fuente(9.5F);
                cap.ForeColor = EstilosFmo.TextoTenue;
            }
            foreach (var val in new[] { lblSubtotalVal, lblIvaVal })
            {
                val.Font = EstilosFmo.Fuente(9.5F);
                val.ForeColor = EstilosFmo.TextoCuerpo;
            }
            lblTotalCap.Font = EstilosFmo.Fuente(13F, FontStyle.Bold);
            lblTotalCap.ForeColor = EstilosFmo.TextoFuerte;
            lblTotalVal.Font = EstilosFmo.Fuente(18F, FontStyle.Bold);
            lblTotalVal.ForeColor = EstilosFmo.MentaClaro;

            EstilosFmo.BotonSecundario(btnCobrar);
        }

        // ───────────────────────── Carga de datos ─────────────────────────
        private void CargarProductos()
        {
            try
            {
                // El punto de venta solo muestra productos activos.
                _productos = ProductoDAO.ListarProductos().Where(p => p.Activo).ToList();
            }
            catch (Exception)
            {
                _productos = new List<Producto>();
                MensajeFmo.Advertencia("No se pudieron cargar los productos. Verifica la conexión con la base de datos.",
                    "Punto de venta");
            }

            // Promociones vigentes para aplicar precios con descuento en el ticket.
            try { _promociones = PromocionServicio.ListarVigentes(); }
            catch (Exception) { _promociones = new List<Promocion>(); }

            RenderCategorias();
            RenderProductos();
        }

        private void RenderCategorias()
        {
            flpCategorias.Controls.Clear();

            var categorias = new List<string> { "Todos" };
            categorias.AddRange(_productos
                .Select(p => p.NombreCategoria)
                .Where(c => !string.IsNullOrWhiteSpace(c))
                .Distinct()
                .OrderBy(c => c));

            foreach (var categoria in categorias)
            {
                bool activa = categoria == _categoriaActiva;
                var pill = new Guna2Button
                {
                    Text = categoria,
                    AutoRoundedCorners = true,
                    Font = EstilosFmo.Fuente(9.5F, FontStyle.Bold),
                    Size = new Size(Math.Max(72, TextRenderer.MeasureText(categoria, EstilosFmo.Fuente(9.5F, FontStyle.Bold)).Width + 28), 32),
                    Margin = new Padding(0, 0, 8, 0),
                    Cursor = Cursors.Hand,
                    FillColor = activa ? EstilosFmo.Fresa : EstilosFmo.SuperficieHundida,
                    ForeColor = activa ? Color.White : EstilosFmo.TextoCuerpo,
                    Tag = categoria
                };
                pill.HoverState.FillColor = activa ? EstilosFmo.FresaHover : EstilosFmo.Borde;
                pill.Click += (s, e) =>
                {
                    _categoriaActiva = (string)((Control)s).Tag;
                    RenderCategorias();
                    RenderProductos();
                };
                flpCategorias.Controls.Add(pill);
            }
        }

        private void RenderProductos()
        {
            flpProductos.SuspendLayout();
            flpProductos.Controls.Clear();

            string filtro = txtBuscar.Text.Trim();

            var visibles = _productos.Where(p =>
                (_categoriaActiva == "Todos" || p.NombreCategoria == _categoriaActiva) &&
                (filtro.Length == 0 || (p.Nombre ?? "").Contains(filtro, StringComparison.OrdinalIgnoreCase)));

            foreach (var producto in visibles)
                flpProductos.Controls.Add(CrearTarjetaProducto(producto));

            flpProductos.ResumeLayout();
        }

        // Caché de imágenes por ruta para no recargarlas del disco en cada búsqueda.
        private readonly Dictionary<string, Image> _cacheImagenes = new();

        private Image ObtenerImagen(string ruta)
        {
            if (string.IsNullOrWhiteSpace(ruta)) return null;
            if (_cacheImagenes.TryGetValue(ruta, out var img)) return img;
            try
            {
                string absoluta = AlmacenImagenes.Resolver(ruta);
                if (absoluta != null)
                {
                    using var bmp = new Bitmap(absoluta);
                    img = new Bitmap(bmp);
                    _cacheImagenes[ruta] = img;
                    return img;
                }
            }
            catch (Exception) { }
            return null;
        }

        private Guna2Panel CrearTarjetaProducto(Producto producto)
        {
            var card = new Guna2Panel
            {
                Size = new Size(168, 214),
                Margin = new Padding(0, 0, 12, 12),
                FillColor = EstilosFmo.Superficie,
                BorderColor = EstilosFmo.Borde,
                BorderThickness = 1,
                BorderRadius = 12,
                Cursor = Cursors.Hand
            };

            var imagen = ObtenerImagen(producto.ImagenRuta);
            var pic = new Guna2PictureBox
            {
                Location = new Point(12, 12),
                Size = new Size(144, 80),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = EstilosFmo.SuperficieHundida,
                BorderRadius = 8,
                Image = imagen,
                Cursor = Cursors.Hand
            };

            var nombre = new Guna2HtmlLabel
            {
                Text = producto.Nombre,
                AutoSize = false,
                Location = new Point(12, 100),
                Size = new Size(144, 34),
                Font = EstilosFmo.Fuente(10F, FontStyle.Bold),
                ForeColor = EstilosFmo.TextoFuerte,
                BackColor = Color.Transparent
            };

            var categoria = new Guna2HtmlLabel
            {
                Text = producto.NombreCategoria,
                AutoSize = false,
                Location = new Point(12, 136),
                Size = new Size(144, 16),
                Font = EstilosFmo.Fuente(8.5F),
                ForeColor = EstilosFmo.TextoTenue,
                BackColor = Color.Transparent
            };

            bool enPromo = TienePromocion(producto);
            var precio = new Guna2HtmlLabel
            {
                Text = "$" + PrecioUnitario(producto).ToString("N2"),
                AutoSize = false,
                Location = new Point(12, 156),
                Size = new Size(144, 24),
                Font = EstilosFmo.Fuente(13F, FontStyle.Bold),
                // En promoción se resalta el precio con el acento "Mango".
                ForeColor = enPromo ? EstilosFmo.Mango : EstilosFmo.TextoFuerte,
                BackColor = Color.Transparent
            };

            // Cuando hay promoción, se muestra el precio original tachado al lado.
            Guna2HtmlLabel precioAntes = null;
            if (enPromo)
            {
                precio.Size = new Size(74, 24);
                precioAntes = new Guna2HtmlLabel
                {
                    Text = $"<s>${producto.PrecioVenta:N2}</s>",
                    AutoSize = false,
                    Location = new Point(86, 160),
                    Size = new Size(70, 20),
                    Font = EstilosFmo.Fuente(9F),
                    ForeColor = EstilosFmo.TextoTenue,
                    BackColor = Color.Transparent
                };
            }

            var stock = CrearBadgeStock(producto);
            stock.Location = new Point(12, 184);

            var controles = new List<Control> { pic, nombre, categoria, precio, stock };
            if (precioAntes != null) controles.Add(precioAntes);

            foreach (Control c in controles)
            {
                c.Cursor = Cursors.Hand;
                c.Click += (s, e) => AgregarAlTicket(producto);
            }
            card.Click += (s, e) => AgregarAlTicket(producto);

            card.Controls.AddRange(controles.ToArray());
            return card;
        }

        private Guna2Button CrearBadgeStock(Producto producto)
        {
            Color color;
            string texto;
            if (producto.StockActual <= 0) { color = EstilosFmo.Cereza; texto = "Agotado"; }
            else if (producto.StockActual <= producto.StockMinimo) { color = EstilosFmo.Mango; texto = producto.StockActual + " u"; }
            else { color = EstilosFmo.Menta; texto = producto.StockActual + " u"; }

            return new Guna2Button
            {
                Text = texto,
                Size = new Size(72, 24),
                BorderRadius = 12,
                FillColor = color,
                ForeColor = Color.White,
                Font = EstilosFmo.Fuente(8F, FontStyle.Bold),
                Enabled = true
            };
        }

        // ───────────────────────── Ticket ─────────────────────────
        private void AgregarAlTicket(Producto producto)
        {
            if (producto.StockActual <= 0)
            {
                MensajeFmo.Info($"\"{producto.Nombre}\" está agotado.", "Sin stock");
                return;
            }

            if (_ticket.TryGetValue(producto.IdProducto, out var linea))
            {
                if (linea.Cantidad >= producto.StockActual)
                {
                    MensajeFmo.Advertencia($"Solo hay {producto.StockActual} unidades de \"{producto.Nombre}\".",
                        "Stock insuficiente");
                    return;
                }
                linea.Cantidad++;
            }
            else
            {
                _ticket[producto.IdProducto] = new LineaTicket { Producto = producto, Cantidad = 1 };
            }

            RenderTicket();
            RecalcularTotales();
        }

        private void CambiarCantidad(int idProducto, int delta)
        {
            if (!_ticket.TryGetValue(idProducto, out var linea)) return;

            int nueva = linea.Cantidad + delta;
            if (nueva <= 0)
            {
                _ticket.Remove(idProducto);
            }
            else if (nueva > linea.Producto.StockActual)
            {
                MensajeFmo.Advertencia($"Solo hay {linea.Producto.StockActual} unidades de \"{linea.Producto.Nombre}\".",
                    "Stock insuficiente");
                return;
            }
            else
            {
                linea.Cantidad = nueva;
            }

            RenderTicket();
            RecalcularTotales();
        }

        private void RenderTicket()
        {
            flpTicket.SuspendLayout();
            flpTicket.Controls.Clear();

            foreach (var linea in _ticket.Values)
                flpTicket.Controls.Add(CrearFilaTicket(linea));

            flpTicket.ResumeLayout();
        }

        private Guna2Panel CrearFilaTicket(LineaTicket linea)
        {
            var fila = new Guna2Panel
            {
                Size = new Size(300, 60),
                Margin = new Padding(0, 0, 0, 8),
                FillColor = EstilosFmo.SuperficieHundida,
                BorderRadius = 10
            };

            var nombre = new Guna2HtmlLabel
            {
                Text = linea.Producto.Nombre,
                Location = new Point(10, 7),
                Size = new Size(250, 18),
                Font = EstilosFmo.Fuente(9.5F, FontStyle.Bold),
                ForeColor = EstilosFmo.TextoFuerte,
                BackColor = Color.Transparent
            };

            var quitar = CrearMiniBoton("✕", EstilosFmo.Borde,
                () => CambiarCantidad(linea.Producto.IdProducto, -linea.Cantidad));
            quitar.Location = new Point(268, 6);

            var menos = CrearMiniBoton("−", EstilosFmo.Superficie,
                () => CambiarCantidad(linea.Producto.IdProducto, -1));
            menos.Location = new Point(10, 31);

            var cant = new Guna2HtmlLabel
            {
                Text = linea.Cantidad.ToString(),
                Location = new Point(38, 33),
                Size = new Size(30, 20),
                Font = EstilosFmo.Fuente(9.5F, FontStyle.Bold),
                ForeColor = EstilosFmo.TextoCuerpo,
                BackColor = Color.Transparent,
                TextAlignment = ContentAlignment.MiddleCenter
            };

            var mas = CrearMiniBoton("+", EstilosFmo.Superficie,
                () => CambiarCantidad(linea.Producto.IdProducto, +1));
            mas.Location = new Point(70, 31);

            var totalLinea = new Guna2HtmlLabel
            {
                Text = "$" + (PrecioUnitario(linea.Producto) * linea.Cantidad).ToString("N2"),
                Location = new Point(150, 33),
                Size = new Size(140, 20),
                Font = EstilosFmo.Fuente(10F, FontStyle.Bold),
                ForeColor = EstilosFmo.TextoFuerte,
                BackColor = Color.Transparent,
                TextAlignment = ContentAlignment.MiddleRight
            };

            fila.Controls.AddRange(new Control[] { nombre, quitar, menos, cant, mas, totalLinea });
            return fila;
        }

        private Guna2Panel CrearMiniBoton(string texto, Color fondo, Action alHacerClick)
        {
            var panel = new Guna2Panel
            {
                Size = new Size(26, 22),
                BorderRadius = 6,
                FillColor = fondo,
                Cursor = Cursors.Hand
            };

            var etiqueta = new Guna2HtmlLabel
            {
                Text = texto,
                Dock = DockStyle.Fill,
                TextAlignment = ContentAlignment.MiddleCenter,
                Font = EstilosFmo.Fuente(10F, FontStyle.Bold),
                ForeColor = EstilosFmo.TextoFuerte,
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand
            };
            panel.Controls.Add(etiqueta);

            panel.Click += (s, e) => alHacerClick();
            etiqueta.Click += (s, e) => alHacerClick();

            return panel;
        }

        // ───────────────────────── Promociones ─────────────────────────
        // Precio unitario efectivo del producto tras aplicar la mejor promoción
        // vigente (por producto o por su categoría). Si no hay, es el precio normal.
        private decimal PrecioUnitario(Producto p)
        {
            decimal mejor = p.PrecioVenta;
            foreach (var promo in _promociones)
            {
                bool aplica = (promo.IdProducto.HasValue && promo.IdProducto.Value == p.IdProducto)
                           || (promo.IdCategoria.HasValue && promo.IdCategoria.Value == p.IdCategoria);
                if (!aplica) continue;

                decimal precio = promo.TipoDescuento == "porcentaje"
                    ? p.PrecioVenta * (1 - promo.ValorDescuento / 100m)
                    : p.PrecioVenta - promo.ValorDescuento;

                precio = Math.Max(0m, Math.Round(precio, 2));
                if (precio < mejor) mejor = precio;
            }
            return mejor;
        }

        // Descuento unitario aplicado por promoción (0 si no hay).
        private decimal DescuentoUnitario(Producto p) => p.PrecioVenta - PrecioUnitario(p);

        // True si el producto tiene una promoción vigente que reduce su precio.
        private bool TienePromocion(Producto p) => DescuentoUnitario(p) > 0m;

        private void RecalcularTotales()
        {
            decimal subtotal = _ticket.Values.Sum(l => PrecioUnitario(l.Producto) * l.Cantidad);
            decimal iva = Math.Round(subtotal * IvaTasa, 2);
            decimal total = subtotal + iva;
            int unidades = _ticket.Values.Sum(l => l.Cantidad);

            lblSubtotalVal.Text = "$" + subtotal.ToString("N2");
            lblIvaVal.Text = "$" + iva.ToString("N2");
            lblTotalVal.Text = "$" + total.ToString("N2");
            lblTicketSub.Text = unidades == 1 ? "1 unidad" : $"{unidades} unidades";

            btnCobrar.Enabled = _ticket.Count > 0;
        }

        // ───────────────────────── Eventos ─────────────────────────
        private void txtBuscar_TextChanged(object sender, EventArgs e) => RenderProductos();

        private void btnCobrar_Click(object sender, EventArgs e)
        {
            if (_ticket.Count == 0)
            {
                MensajeFmo.Info("Agrega al menos un producto al ticket.", "Ticket vacío");
                return;
            }

            if (!Sesion.HayCajaAbierta)
            {
                MensajeFmo.Advertencia("No hay una caja abierta. Abre una caja en el módulo de Caja antes de cobrar.",
                    "Caja cerrada");
                return;
            }

            decimal subtotal = _ticket.Values.Sum(l => PrecioUnitario(l.Producto) * l.Cantidad);

            // Diálogo de cobro: descuento (con supervisor), efectivo y cambio.
            using var cobro = new FrmCobro(subtotal, IvaTasa);
            if (cobro.ShowDialog(this.FindForm()) != DialogResult.OK) return;

            int idVenta;
            try
            {
                idVenta = RegistrarVenta(cobro.Total, cobro.Descuento, cobro.IdAutorizadoDescuento);
            }
            catch (Exception ex)
            {
                MensajeFmo.Error($"No se pudo registrar la venta.\n{ex.Message}", "Error");
                return;
            }

            // Impresión del ticket (no bloquea el cierre de la venta si falla).
            try
            {
                ImprimirTicket(idVenta, cobro.Total, cobro.Efectivo, cobro.Cambio);
            }
            catch (Exception ex)
            {
                MensajeFmo.Advertencia($"La venta se registró, pero no se pudo imprimir el ticket.\n{ex.Message}",
                    "Impresión");
            }

            MensajeFmo.Info($"Venta registrada por ${cobro.Total:N2}. Cambio: ${cobro.Cambio:N2}.", "Venta completada");

            _ticket.Clear();
            CargarProductos();   // refresca stock tras descontar existencias
            RenderTicket();
            RecalcularTotales();
        }

        // Persiste la venta y su detalle contra la caja abierta en sesión.
        // Devuelve el id de la venta creada.
        private int RegistrarVenta(decimal total, decimal descuento, int idAutorizadoDescuento)
        {
            int idUsuario = Sesion.UsuarioActivo.id_Usuario;

            var venta = new Venta_sucursal
            {
                IdCaja = Sesion.IdCajaActiva,
                IdCajero = idUsuario,
                Descuento = 0m
            };

            int idVenta = VentaSucursalServicio.RegistrarVentaSucursal(venta);

            foreach (var linea in _ticket.Values)
            {
                var detalle = new Detalle_Venta_Sucursal
                {
                    IdVenta = idVenta,
                    IdProducto = linea.Producto.IdProducto,
                    Cantidad = linea.Cantidad,
                    // El descuento por promoción se registra a nivel de línea.
                    Descuento = DescuentoUnitario(linea.Producto) * linea.Cantidad
                };
                VentaSucursalServicio.AgregarDetalleVentaSucursal(detalle, idUsuario);
            }

            // Si un supervisor autorizó un descuento, se aplica sobre la venta.
            if (descuento > 0 && idAutorizadoDescuento > 0)
                VentaSucursalServicio.RegistrarDescuentoAutorizado(idVenta, descuento, idAutorizadoDescuento);

            // Alimenta el monto esperado del arqueo de caja.
            Sesion.TotalVendidoCaja += total;
            return idVenta;
        }

        // Arma e imprime el ticket de la venta en la impresora térmica.
        private void ImprimirTicket(int idVenta, decimal total, decimal efectivo, decimal cambio)
        {
            var lineas = _ticket.Values
                .Select(l => (l.Producto.Nombre, l.Cantidad, PrecioUnitario(l.Producto)))
                .ToList();

            ImpresionFMO.ImprimirTicketVenta(
                idVenta,
                Sesion.UsuarioActivo?.Nombre ?? "—",
                lineas,
                total,
                efectivo,
                cambio);
        }
    }
}
