using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Heladeria_FMO.Acceso_a_datos_db;
using Heladeria_FMO.Modelos;
using Heladeria_FMO.Utileria;

namespace Heladeria_FMO.Intefaz.Inventario
{
    // Formulario de producto (alta/edición) en código, con el mismo estilo oscuro
    // que el resto de los diálogos. Incluye selector de imagen con vista previa.
    public class FrmProducto : Form
    {
        private readonly Guna2TextBox txtCodigo = new();
        private readonly Guna2TextBox txtBarras = new();
        private readonly Guna2TextBox txtNombre = new();
        private readonly Guna2ComboBox cboCategoria = new();
        private readonly Guna2TextBox txtPresentacion = new();
        private readonly Guna2TextBox txtCompra = new();
        private readonly Guna2TextBox txtVenta = new();
        private readonly Guna2NumericUpDown numExistencia = new();
        private readonly Guna2NumericUpDown numMinimo = new();
        private readonly Guna2DateTimePicker dtpVence = new();
        private readonly Guna2Button btnImagen = new();
        private readonly Guna2PictureBox picImagen = new();
        private readonly Guna2Button btnGuardar = new();

        private readonly bool _modoEdicion;
        private readonly int _idProducto;
        private string _imagenRuta;

        public FrmProducto(Producto productoEditar = null)
        {
            _modoEdicion = productoEditar != null;
            _idProducto = productoEditar?.IdProducto ?? 0;

            ConstruirUi();
            CargarCategorias();

            if (_modoEdicion) CargarProducto(productoEditar);
        }

        private void ConstruirUi()
        {
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            ShowInTaskbar = false;
            ClientSize = new Size(620, 588);
            BackColor = EstilosFmo.Superficie;

            var marco = new Guna2Panel { Dock = DockStyle.Fill };
            EstilosFmo.Tarjeta(marco);
            Controls.Add(marco);

            var titulo = new Guna2HtmlLabel
            {
                Text = _modoEdicion ? "Editar producto" : "Nuevo producto",
                Location = new Point(24, 18),
                Size = new Size(420, 28),
                Font = EstilosFmo.Fuente(16F, FontStyle.Bold),
                ForeColor = EstilosFmo.TextoFuerte,
                BackColor = Color.Transparent
            };
            var subtitulo = new Guna2HtmlLabel
            {
                Text = "Registra un producto en el catálogo de inventario.",
                Location = new Point(24, 46),
                Size = new Size(480, 20),
                Font = EstilosFmo.Fuente(9.5F),
                ForeColor = EstilosFmo.TextoTenue,
                BackColor = Color.Transparent
            };
            var btnCerrar = new Guna2Button { Text = "✕", Size = new Size(32, 32), Location = new Point(564, 16), Font = EstilosFmo.Fuente(10F, FontStyle.Bold) };
            EstilosFmo.BotonContorno(btnCerrar);
            btnCerrar.Click += (s, e) => Close();

            int xa = 24, xb = 320, w = 276;

            var lblCodigo = Caption("Código", xa, 80); Input(txtCodigo, xa, 102, w); txtCodigo.PlaceholderText = "FMO-001";
            var lblBarras = Caption("Código de barras", xb, 80); Input(txtBarras, xb, 102, w); txtBarras.PlaceholderText = "7400000000000";

            var lblNombre = Caption("Nombre del producto", xa, 146); Input(txtNombre, xa, 168, 572); txtNombre.PlaceholderText = "Helado de fresa 1 L";

            var lblCat = Caption("Categoría", xa, 212);
            EstilosFmo.Combo(cboCategoria); cboCategoria.Location = new Point(xa, 234); cboCategoria.Size = new Size(w, 34);
            var lblPres = Caption("Presentación", xb, 212); Input(txtPresentacion, xb, 234, w); txtPresentacion.PlaceholderText = "1 L / unidad";

            var lblCompra = Caption("Precio de compra ($)", xa, 278); Input(txtCompra, xa, 300, w); txtCompra.PlaceholderText = "2.50";
            var lblVenta = Caption("Precio de venta ($)", xb, 278); Input(txtVenta, xb, 300, w); txtVenta.PlaceholderText = "4.25";

            var lblExist = Caption("Existencia", xa, 344); Numerico(numExistencia, xa, 366, 130);
            var lblMin = Caption("Stock mínimo", 170, 344); Numerico(numMinimo, 170, 366, 130);
            var lblVence = Caption("Vencimiento", xb, 344);
            dtpVence.Location = new Point(xb, 366); dtpVence.Size = new Size(w, 36);
            dtpVence.Format = DateTimePickerFormat.Short; dtpVence.BorderRadius = 8;
            dtpVence.FillColor = EstilosFmo.SuperficieHundida; dtpVence.ForeColor = EstilosFmo.TextoFuerte; dtpVence.BorderColor = EstilosFmo.Borde;
            dtpVence.Value = DateTime.Today.AddMonths(6);

            var lblImg = Caption("Imagen", xb, 420);
            btnImagen.Text = "Seleccionar imagen…"; EstilosFmo.BotonContorno(btnImagen);
            btnImagen.Location = new Point(xb, 442); btnImagen.Size = new Size(180, 38);
            btnImagen.Click += btnImagen_Click;
            picImagen.Location = new Point(510, 420); picImagen.Size = new Size(86, 86);
            picImagen.SizeMode = PictureBoxSizeMode.Zoom; picImagen.BackColor = EstilosFmo.SuperficieHundida;

            var btnCancelar = new Guna2Button { Text = "Cancelar", Location = new Point(360, 528), Size = new Size(110, 44) };
            EstilosFmo.BotonContorno(btnCancelar); btnCancelar.Click += (s, e) => Close();

            btnGuardar.Text = _modoEdicion ? "Guardar cambios" : "Guardar producto";
            EstilosFmo.BotonPrimario(btnGuardar);
            btnGuardar.Location = new Point(478, 528); btnGuardar.Size = new Size(118, 44);
            btnGuardar.Click += btnGuardar_Click;

            marco.Controls.AddRange(new Control[]
            {
                titulo, subtitulo, btnCerrar,
                lblCodigo, txtCodigo, lblBarras, txtBarras,
                lblNombre, txtNombre,
                lblCat, cboCategoria, lblPres, txtPresentacion,
                lblCompra, txtCompra, lblVenta, txtVenta,
                lblExist, numExistencia, lblMin, numMinimo, lblVence, dtpVence,
                lblImg, btnImagen, picImagen,
                btnCancelar, btnGuardar
            });
        }

        private static Guna2HtmlLabel Caption(string texto, int x, int y) => new()
        {
            Text = texto,
            Location = new Point(x, y),
            Size = new Size(270, 20),
            Font = EstilosFmo.Fuente(9.5F),
            ForeColor = EstilosFmo.TextoTenue,
            BackColor = Color.Transparent
        };

        private static void Input(Guna2TextBox txt, int x, int y, int w)
        {
            EstilosFmo.CajaTexto(txt);
            txt.Location = new Point(x, y);
            txt.Size = new Size(w, 34);
        }

        private static void Numerico(Guna2NumericUpDown num, int x, int y, int w)
        {
            num.Location = new Point(x, y);
            num.Size = new Size(w, 36);
            num.Minimum = 0;
            num.Maximum = 1000000;
            num.BorderRadius = 8;
            num.FillColor = EstilosFmo.SuperficieHundida;
            num.ForeColor = EstilosFmo.TextoFuerte;
            num.BorderColor = EstilosFmo.Borde;
        }

        // ───────────────────────── Datos ─────────────────────────
        private void CargarCategorias()
        {
            try
            {
                cboCategoria.DataSource = CategoriaDao.ListarCategorias();
                cboCategoria.DisplayMember = nameof(Categoria.Nombre);
                cboCategoria.ValueMember = nameof(Categoria.IdCategoria);
                cboCategoria.SelectedIndex = -1;
            }
            catch (Exception)
            {
                MensajeFmo.Advertencia("No se pudieron cargar las categorías.", "Producto");
            }
        }

        private void CargarProducto(Producto p)
        {
            txtCodigo.Text = p.Codigo;
            txtBarras.Text = p.CodigoBarras;
            txtNombre.Text = p.Nombre;
            txtPresentacion.Text = p.Presentacion;
            txtCompra.Text = p.PrecioCompra.ToString("0.00", CultureInfo.InvariantCulture);
            txtVenta.Text = p.PrecioVenta.ToString("0.00", CultureInfo.InvariantCulture);
            numExistencia.Value = Math.Min(numExistencia.Maximum, Math.Max(numExistencia.Minimum, p.StockActual));
            numMinimo.Value = Math.Min(numMinimo.Maximum, Math.Max(numMinimo.Minimum, p.StockMinimo));
            if (p.FechaVencimiento >= dtpVence.MinDate && p.FechaVencimiento <= dtpVence.MaxDate)
                dtpVence.Value = p.FechaVencimiento;
            numExistencia.Enabled = false; // el stock cambia por movimientos, no por edición
            _imagenRuta = p.ImagenRuta;
            CargarPreview(_imagenRuta);
            SeleccionarCategoria(p);
        }

        private void SeleccionarCategoria(Producto p)
        {
            if (p.IdCategoria > 0) { cboCategoria.SelectedValue = p.IdCategoria; return; }
            for (int i = 0; i < cboCategoria.Items.Count; i++)
                if (cboCategoria.Items[i] is Categoria c && c.Nombre == p.NombreCategoria) { cboCategoria.SelectedIndex = i; return; }
        }

        // ───────────────────────── Imagen ─────────────────────────
        private void btnImagen_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog
            {
                Title = "Seleccionar imagen del producto",
                Filter = "Imágenes|*.jpg;*.jpeg;*.png;*.bmp"
            };
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                _imagenRuta = ofd.FileName;
                CargarPreview(_imagenRuta);
            }
        }

        private void CargarPreview(string ruta)
        {
            picImagen.Image?.Dispose();
            if (!string.IsNullOrWhiteSpace(ruta) && System.IO.File.Exists(ruta))
                using (var bmp = new Bitmap(ruta)) picImagen.Image = new Bitmap(bmp);
            else
                picImagen.Image = null;
        }

        // ───────────────────────── Guardar ─────────────────────────
        private static bool TryDecimal(string texto, out decimal valor)
        {
            texto = (texto ?? "").Trim().Replace(",", ".");
            return decimal.TryParse(texto, NumberStyles.Number, CultureInfo.InvariantCulture, out valor);
        }

        private Producto LeerFormulario()
        {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text)) { Aviso("Ingresa el código del producto.", txtCodigo); return null; }
            if (string.IsNullOrWhiteSpace(txtNombre.Text)) { Aviso("Ingresa el nombre del producto.", txtNombre); return null; }
            if (cboCategoria.SelectedValue is not int idCategoria || idCategoria <= 0) { Aviso("Selecciona una categoría.", cboCategoria); return null; }
            if (!TryDecimal(txtCompra.Text, out decimal compra) || compra < 0) { Aviso("Precio de compra inválido.", txtCompra); return null; }
            if (!TryDecimal(txtVenta.Text, out decimal venta) || venta <= 0) { Aviso("Precio de venta inválido (mayor que cero).", txtVenta); return null; }
            if (venta < compra) { Aviso("El precio de venta no debería ser menor que el de compra.", txtVenta); return null; }

            // No se permite dar de alta un producto que ya venció o que vence hoy.
            if (!_modoEdicion && dtpVence.Value.Date <= DateTime.Today)
            {
                Aviso("La fecha de vencimiento debe ser posterior a hoy.", dtpVence);
                return null;
            }

            // Código de barras: si no se ingresa uno (p. ej. de fábrica), se genera
            // uno interno único (prefijo EAN '2', reservado para uso interno) para
            // que el producto siempre se pueda escanear en el punto de venta.
            string barras = txtBarras.Text.Trim();
            if (string.IsNullOrEmpty(barras))
            {
                barras = "2" + DateTime.Now.ToString("yyMMddHHmmss");
                txtBarras.Text = barras;
            }

            return new Producto
            {
                IdProducto = _idProducto,
                Codigo = txtCodigo.Text.Trim(),
                CodigoBarras = barras,
                Nombre = txtNombre.Text.Trim(),
                IdCategoria = idCategoria,
                Presentacion = txtPresentacion.Text.Trim(),
                PrecioCompra = compra,
                PrecioVenta = venta,
                StockActual = (int)numExistencia.Value,
                StockMinimo = (int)numMinimo.Value,
                FechaIngreso = DateTime.Today,
                FechaVencimiento = dtpVence.Value,
                Activo = true,
                ImagenRuta = string.IsNullOrWhiteSpace(_imagenRuta) ? null : _imagenRuta
            };
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Producto producto = LeerFormulario();
            if (producto == null) return;

            try
            {
                bool ok = _modoEdicion
                    ? ProductoDAO.EditarProducto(producto)
                    : ProductoDAO.InsertarProducto(producto);

                if (!ok) { MensajeFmo.Advertencia("No se guardó el producto.", "Producto"); return; }

                MensajeFmo.Exito(_modoEdicion ? "Producto actualizado." : "Producto registrado.", "Producto");
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MensajeFmo.Error($"No se pudo guardar el producto.\n{ex.Message}", "Producto");
            }
        }

        private static void Aviso(string mensaje, Control foco)
        {
            MensajeFmo.Advertencia(mensaje, "Datos incompletos");
            foco?.Focus();
        }
    }
}
