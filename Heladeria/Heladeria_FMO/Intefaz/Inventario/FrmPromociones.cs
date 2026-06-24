using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Heladeria_FMO.Acceso_a_datos_db;
using Heladeria_FMO.Modelos;
using Heladeria_FMO.Servicio;
using Heladeria_FMO.Utileria;

namespace Heladeria_FMO.Intefaz.Inventario
{
    // Gestión de promociones: lista las existentes (con su estado) y permite crear
    // nuevas, que quedan PENDIENTES de aprobación (se aprueban en Autorizaciones).
    public class FrmPromociones : Form
    {
        private readonly Guna2DataGridView dgv = new();
        private readonly Guna2TextBox txtNombre = new();
        private readonly Guna2ComboBox cboAplicar = new();
        private readonly Guna2ComboBox cboElemento = new();
        private readonly Guna2ComboBox cboTipo = new();
        private readonly Guna2TextBox txtValor = new();
        private readonly Guna2DateTimePicker dtpDesde = new();
        private readonly Guna2DateTimePicker dtpHasta = new();
        private readonly Guna2TextBox txtDescripcion = new();
        private readonly Guna2Button btnGuardar = new();

        public FrmPromociones()
        {
            ConstruirUi();
            CargarPromociones();
        }

        private void ConstruirUi()
        {
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            ShowInTaskbar = false;
            ClientSize = new Size(900, 548);
            BackColor = EstilosFmo.Superficie;

            var marco = new Guna2Panel { Dock = DockStyle.Fill };
            EstilosFmo.Tarjeta(marco);
            Controls.Add(marco);

            var titulo = new Guna2HtmlLabel
            {
                Text = "Promociones",
                Location = new Point(24, 18),
                Size = new Size(300, 30),
                Font = EstilosFmo.Fuente(16F, FontStyle.Bold),
                ForeColor = EstilosFmo.TextoFuerte,
                BackColor = Color.Transparent
            };
            var btnCerrar = new Guna2Button { Text = "✕", Size = new Size(32, 32), Location = new Point(844, 14), Font = EstilosFmo.Fuente(10F, FontStyle.Bold) };
            EstilosFmo.BotonContorno(btnCerrar);
            btnCerrar.Click += (s, e) => Close();

            EstilosFmo.Tabla(dgv);
            dgv.Location = new Point(24, 60);
            dgv.Size = new Size(430, 460);

            int x = 482;
            var lblNombre = Caption("Nombre", x, 60); Input(txtNombre, x, 82, 388);

            var lblAplicar = Caption("Aplicar a", x, 126);
            EstilosFmo.Combo(cboAplicar); cboAplicar.Location = new Point(x, 148); cboAplicar.Size = new Size(185, 34);
            cboAplicar.Items.AddRange(new object[] { "Producto", "Categoría" });
            cboAplicar.SelectedIndexChanged += (s, e) => CargarElementos();

            var lblTipo = Caption("Tipo de descuento", x + 203, 126);
            EstilosFmo.Combo(cboTipo); cboTipo.Location = new Point(x + 203, 148); cboTipo.Size = new Size(185, 34);
            cboTipo.Items.AddRange(new object[] { "Porcentaje (%)", "Monto fijo ($)" });

            var lblElemento = Caption("Producto / categoría", x, 192);
            EstilosFmo.Combo(cboElemento); cboElemento.Location = new Point(x, 214); cboElemento.Size = new Size(388, 34);

            var lblValor = Caption("Valor del descuento", x, 258); Input(txtValor, x, 280, 185); txtValor.PlaceholderText = "10";

            var lblDesde = Caption("Desde", x, 324);
            Fecha(dtpDesde, x, 346, 185); dtpDesde.Value = DateTime.Today;
            var lblHasta = Caption("Hasta", x + 203, 324);
            Fecha(dtpHasta, x + 203, 346, 185); dtpHasta.Value = DateTime.Today.AddDays(7);

            var lblDesc = Caption("Descripción", x, 390); Input(txtDescripcion, x, 412, 388);

            var info = new Guna2HtmlLabel
            {
                Text = "Las promociones nuevas quedan pendientes de aprobación.",
                Location = new Point(x, 452),
                Size = new Size(388, 18),
                Font = EstilosFmo.Fuente(9F),
                ForeColor = EstilosFmo.Mango,
                BackColor = Color.Transparent
            };

            btnGuardar.Text = "Crear promoción";
            EstilosFmo.BotonPrimario(btnGuardar);
            btnGuardar.Location = new Point(x, 478);
            btnGuardar.Size = new Size(388, 42);
            btnGuardar.Click += btnGuardar_Click;

            marco.Controls.AddRange(new Control[]
            {
                titulo, btnCerrar, dgv,
                lblNombre, txtNombre, lblAplicar, cboAplicar, lblTipo, cboTipo,
                lblElemento, cboElemento, lblValor, txtValor,
                lblDesde, dtpDesde, lblHasta, dtpHasta, lblDesc, txtDescripcion,
                info, btnGuardar
            });

            cboAplicar.SelectedIndex = 0;
            cboTipo.SelectedIndex = 0;
        }

        private static Guna2HtmlLabel Caption(string texto, int x, int y) => new()
        {
            Text = texto,
            Location = new Point(x, y),
            Size = new Size(190, 20),
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

        private static void Fecha(Guna2DateTimePicker d, int x, int y, int w)
        {
            d.Location = new Point(x, y);
            d.Size = new Size(w, 34);
            d.Format = DateTimePickerFormat.Short;
            d.BorderRadius = 8;
            d.FillColor = EstilosFmo.SuperficieHundida;
            d.ForeColor = EstilosFmo.TextoFuerte;
            d.BorderColor = EstilosFmo.Borde;
        }

        // ───────────────────────── Datos ─────────────────────────
        private void CargarPromociones()
        {
            try
            {
                dgv.DataSource = PromocionServicio.ListarPromociones();
                EstilosFmo.MostrarSoloColumnas(dgv,
                    ("Nombre", "Nombre"),
                    ("Objetivo", "Aplica a"),
                    ("DescuentoTexto", "Descuento"),
                    ("Estado", "Estado"));
            }
            catch (Exception ex) { MensajeFmo.Error(ex.Message, "Promociones"); }
        }

        private void CargarElementos()
        {
            try
            {
                if (cboAplicar.SelectedIndex == 1)
                {
                    cboElemento.DataSource = CategoriaDao.ListarCategorias();
                    cboElemento.DisplayMember = nameof(Categoria.Nombre);
                    cboElemento.ValueMember = nameof(Categoria.IdCategoria);
                }
                else
                {
                    cboElemento.DataSource = ProductoDAO.ListarProductos().Where(p => p.Activo).ToList();
                    cboElemento.DisplayMember = nameof(Producto.Nombre);
                    cboElemento.ValueMember = nameof(Producto.IdProducto);
                }
                cboElemento.SelectedIndex = cboElemento.Items.Count > 0 ? 0 : -1;
            }
            catch (Exception)
            {
                MensajeFmo.Advertencia("No se pudieron cargar los elementos.", "Promociones");
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (cboElemento.SelectedValue is not int idElemento || idElemento <= 0)
            {
                MensajeFmo.Advertencia("Selecciona un producto o categoría.", "Promociones");
                return;
            }
            string valorTxt = (txtValor.Text ?? "").Trim().Replace(",", ".");
            if (!decimal.TryParse(valorTxt, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal valor))
            {
                MensajeFmo.Advertencia("Ingresa un valor de descuento válido.", "Promociones");
                return;
            }

            bool esCategoria = cboAplicar.SelectedIndex == 1;
            var promo = new Promocion
            {
                Nombre = txtNombre.Text.Trim(),
                IdProducto = esCategoria ? (int?)null : idElemento,
                IdCategoria = esCategoria ? idElemento : (int?)null,
                TipoDescuento = cboTipo.SelectedIndex == 1 ? "monto_fijo" : "porcentaje",
                ValorDescuento = valor,
                FechaInicio = dtpDesde.Value.Date,
                FechaFin = dtpHasta.Value.Date,
                Descripcion = txtDescripcion.Text.Trim()
            };

            try
            {
                bool ok = PromocionServicio.RegistrarPromocion(promo);
                if (ok) MensajeFmo.Exito("Promoción creada. Queda pendiente de aprobación.", "Promociones");
                else MensajeFmo.Advertencia("No se creó la promoción.", "Promociones");
                if (ok) { CargarPromociones(); txtNombre.Clear(); txtValor.Clear(); txtDescripcion.Clear(); }
            }
            catch (Exception ex)
            {
                MensajeFmo.Advertencia(ex.Message, "Promociones");
            }
        }
    }
}
