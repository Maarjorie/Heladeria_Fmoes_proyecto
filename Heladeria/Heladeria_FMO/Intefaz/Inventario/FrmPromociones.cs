using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Heladeria_FMO.Acceso_a_datos_db;
using Heladeria_FMO.Modelos;
using Heladeria_FMO.Servicio;
using Heladeria_FMO.Utileria;

namespace Heladeria_FMO.Intefaz.Inventario
{
    // Gestión de promociones: lista las existentes (con su estado) y permite crear
    // nuevas, que quedan PENDIENTES de aprobación (se aprueban en Autorizaciones).
    public partial class FrmPromociones : Form
    {
        public FrmPromociones()
        {
            InitializeComponent();
            AplicarTema();

            dtpDesde.Value = DateTime.Today;
            dtpHasta.Value = DateTime.Today.AddDays(7);
            cboAplicar.SelectedIndex = 0; // dispara la carga de elementos
            cboTipo.SelectedIndex = 0;

            CargarPromociones();
        }

        // El Diseñador maneja el layout; aquí se aplica el tema oscuro de la app.
        private void AplicarTema()
        {
            BackColor = EstilosFmo.Superficie;
            EstilosFmo.Tarjeta(marco);

            titulo.Font = EstilosFmo.Fuente(16F, FontStyle.Bold);
            titulo.ForeColor = EstilosFmo.TextoFuerte;

            btnCerrar.Font = EstilosFmo.Fuente(10F, FontStyle.Bold);
            EstilosFmo.BotonContorno(btnCerrar);

            EstilosFmo.Tabla(dgv);

            foreach (var lbl in new[] { lblNombre, lblAplicar, lblTipo, lblElemento, lblValor, lblDesde, lblHasta, lblDesc })
            {
                lbl.Font = EstilosFmo.Fuente(9.5F);
                lbl.ForeColor = EstilosFmo.TextoTenue;
            }

            info.Font = EstilosFmo.Fuente(9F);
            info.ForeColor = EstilosFmo.Mango;

            EstilosFmo.CajaTexto(txtNombre);
            EstilosFmo.CajaTexto(txtValor);
            EstilosFmo.CajaTexto(txtDescripcion);
            EstilosFmo.Combo(cboAplicar);
            EstilosFmo.Combo(cboTipo);
            EstilosFmo.Combo(cboElemento);

            EstiloFecha(dtpDesde);
            EstiloFecha(dtpHasta);

            EstilosFmo.BotonPrimario(btnGuardar);
        }

        private static void EstiloFecha(Guna.UI2.WinForms.Guna2DateTimePicker d)
        {
            d.BorderRadius = 8;
            d.FillColor = EstilosFmo.SuperficieHundida;
            d.ForeColor = EstilosFmo.TextoFuerte;
            d.BorderColor = EstilosFmo.Borde;
        }

        private void BtnCerrar_Click(object sender, EventArgs e) => Close();

        private void cboAplicar_SelectedIndexChanged(object sender, EventArgs e) => CargarElementos();

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
