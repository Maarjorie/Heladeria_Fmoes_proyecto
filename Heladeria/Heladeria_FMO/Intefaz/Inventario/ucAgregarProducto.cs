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
    public partial class ucAgregarProducto : UserControl
    {
        private bool _modoEdicion;
        private int _idProducto;

        public ucAgregarProducto()
        {
            InitializeComponent();
            AplicarTema();
            CargarCategorias();
            btnAgregarProducto.Click += btnGuardar_Click;
        }

        // Aplica el tema oscuro al modal (reutiliza los controles del diseñador).
        private void AplicarTema()
        {
            BackColor = EstilosFmo.Superficie;
            guna2CustomGradientPanel1.FillColor = EstilosFmo.Superficie;
            guna2CustomGradientPanel1.FillColor2 = EstilosFmo.Superficie;
            guna2Panel1.FillColor = EstilosFmo.SuperficieHundida;

            foreach (Control c in guna2CustomGradientPanel1.Controls)
                if (c is Guna2HtmlLabel l) { l.ForeColor = EstilosFmo.TextoCuerpo; l.BackColor = Color.Transparent; }
            foreach (Control c in guna2Panel1.Controls)
                if (c is Guna2HtmlLabel l) { l.ForeColor = EstilosFmo.TextoCuerpo; l.BackColor = Color.Transparent; }

            guna2HtmlLabel1.ForeColor = EstilosFmo.TextoFuerte;
            guna2HtmlLabel1.Font = EstilosFmo.Fuente(15F, FontStyle.Bold);
            guna2HtmlLabel2.ForeColor = EstilosFmo.TextoTenue;

            foreach (var t in new[] { guna2TextBox1, guna2TextBox2, guna2TextBox3, guna2TextBox4, guna2TextBox5, guna2TextBox6 })
                EstilosFmo.CajaTexto(t);

            EstilosFmo.Combo(guna2ComboBox1);

            foreach (var n in new[] { guna2NumericUpDown1, guna2NumericUpDown2 })
            {
                n.FillColor = EstilosFmo.SuperficieHundida;
                n.ForeColor = EstilosFmo.TextoFuerte;
                n.BorderColor = EstilosFmo.Borde;
            }

            guna2DateTimePicker1.FillColor = EstilosFmo.SuperficieHundida;
            guna2DateTimePicker1.ForeColor = EstilosFmo.TextoFuerte;
            guna2DateTimePicker1.BorderColor = EstilosFmo.Borde;

            EstilosFmo.BotonPrimario(btnAgregarProducto);
            EstilosFmo.BotonContorno(btnCancelar);
            EstilosFmo.BotonContorno(btnInsertarImagen);
            btnCerrar.FillColor = Color.Transparent;
            btnCerrar.ForeColor = EstilosFmo.TextoTenue;
        }

        // Llena el combo de categorías desde la base de datos.
        private void CargarCategorias()
        {
            try
            {
                guna2ComboBox1.DataSource = CategoriaDao.ListarCategorias();
                guna2ComboBox1.DisplayMember = nameof(Categoria.Nombre);
                guna2ComboBox1.ValueMember = nameof(Categoria.IdCategoria);
                guna2ComboBox1.SelectedIndex = -1;
            }
            catch (Exception)
            {
                MensajeFmo.Advertencia("No se pudieron cargar las categorías.", "Producto");
            }
        }

        // Prepara el formulario para editar un producto existente.
        public void CargarParaEditar(Producto p)
        {
            _modoEdicion = true;
            _idProducto = p.IdProducto;

            guna2HtmlLabel1.Text = "Editar producto";
            guna2HtmlLabel2.Text = "Modifica los datos del producto seleccionado.";
            btnAgregarProducto.Text = "Guardar cambios";

            guna2TextBox1.Text = p.Codigo;
            guna2TextBox2.Text = p.CodigoBarras;
            guna2TextBox3.Text = p.Nombre;
            guna2TextBox4.Text = p.Presentacion;
            guna2TextBox5.Text = p.PrecioCompra.ToString("0.00", CultureInfo.InvariantCulture);
            guna2TextBox6.Text = p.PrecioVenta.ToString("0.00", CultureInfo.InvariantCulture);
            guna2NumericUpDown1.Value = Math.Max(guna2NumericUpDown1.Minimum, p.StockActual);
            guna2NumericUpDown2.Value = Math.Max(guna2NumericUpDown2.Minimum, p.StockMinimo);
            if (p.FechaVencimiento >= guna2DateTimePicker1.MinDate && p.FechaVencimiento <= guna2DateTimePicker1.MaxDate)
                guna2DateTimePicker1.Value = p.FechaVencimiento;

            // En edición el stock actual no se modifica (cambia por movimientos).
            guna2NumericUpDown1.Enabled = false;

            SeleccionarCategoria(p);
        }

        private void SeleccionarCategoria(Producto p)
        {
            if (p.IdCategoria > 0)
            {
                guna2ComboBox1.SelectedValue = p.IdCategoria;
                return;
            }

            for (int i = 0; i < guna2ComboBox1.Items.Count; i++)
                if (guna2ComboBox1.Items[i] is Categoria c && c.Nombre == p.NombreCategoria)
                {
                    guna2ComboBox1.SelectedIndex = i;
                    return;
                }
        }

        private static bool TryDecimal(string texto, out decimal valor)
        {
            texto = (texto ?? "").Trim().Replace(",", ".");
            return decimal.TryParse(texto, NumberStyles.Number, CultureInfo.InvariantCulture, out valor);
        }

        // Valida y arma el objeto Producto desde los controles.
        private Producto LeerFormulario()
        {
            if (string.IsNullOrWhiteSpace(guna2TextBox1.Text))
            {
                Aviso("Ingresa el código del producto.", guna2TextBox1);
                return null;
            }
            if (string.IsNullOrWhiteSpace(guna2TextBox3.Text))
            {
                Aviso("Ingresa el nombre del producto.", guna2TextBox3);
                return null;
            }
            if (guna2ComboBox1.SelectedValue is not int idCategoria || idCategoria <= 0)
            {
                Aviso("Selecciona una categoría.", guna2ComboBox1);
                return null;
            }
            if (!TryDecimal(guna2TextBox5.Text, out decimal precioCompra) || precioCompra < 0)
            {
                Aviso("Precio de compra inválido.", guna2TextBox5);
                return null;
            }
            if (!TryDecimal(guna2TextBox6.Text, out decimal precioVenta) || precioVenta <= 0)
            {
                Aviso("Precio de venta inválido (mayor que cero).", guna2TextBox6);
                return null;
            }
            if (precioVenta < precioCompra)
            {
                Aviso("El precio de venta no debería ser menor que el de compra.", guna2TextBox6);
                return null;
            }

            return new Producto
            {
                IdProducto = _idProducto,
                Codigo = guna2TextBox1.Text.Trim(),
                CodigoBarras = guna2TextBox2.Text.Trim(),
                Nombre = guna2TextBox3.Text.Trim(),
                IdCategoria = idCategoria,
                Presentacion = guna2TextBox4.Text.Trim(),
                PrecioCompra = precioCompra,
                PrecioVenta = precioVenta,
                StockActual = (int)guna2NumericUpDown1.Value,
                StockMinimo = (int)guna2NumericUpDown2.Value,
                FechaIngreso = DateTime.Today,
                FechaVencimiento = guna2DateTimePicker1.Value,
                Activo = guna2ToggleSwitch1.Checked,
                ImagenRuta = null
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

                if (!ok)
                {
                    MensajeFmo.Advertencia("No se guardó el producto.", "Producto");
                    return;
                }

                MensajeFmo.Info(_modoEdicion ? "Producto actualizado." : "Producto registrado.",
                    "Producto");

                var form = this.FindForm();
                if (form != null) form.DialogResult = DialogResult.OK;
                form?.Close();
            }
            catch (Exception ex)
            {
                MensajeFmo.Error($"No se pudo guardar el producto.\n{ex.Message}", "Error");
            }
        }

        private static void Aviso(string mensaje, Control foco)
        {
            MensajeFmo.Advertencia(mensaje, "Datos incompletos");
            foco?.Focus();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.FindForm()?.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.FindForm()?.Close();
        }
    }
}
