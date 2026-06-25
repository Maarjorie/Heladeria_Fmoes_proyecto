using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Heladeria_FMO.Acceso_a_datos_db;
using Heladeria_FMO.Servicio;
using Heladeria_FMO.Utileria;

namespace Heladeria_FMO.Intefaz.ucMenuInicio
{
    // Dashboard de inicio: tarjetas con indicadores del día (ventas, tickets,
    // productos por vencer y stock bajo) y una tabla con las ventas recientes.
    public partial class ucInicio : UserControl
    {
        private Guna2DataGridView _dgvRecientes;

        // Valores de las tarjetas KPI gerenciales (creadas en código).
        private Guna2HtmlLabel _lblUtilidadBruta, _lblUtilidadNeta, _lblComisiones,
            _lblVentasRuta, _lblVentasMayoristas, _lblProductoTop;

        public ucInicio()
        {
            InitializeComponent();
            AplicarTema();
            ConstruirTarjetasGerenciales();
            ConstruirVentasRecientes();
            CargarDatos();
        }

        // Tarjetas KPI gerenciales (utilidad, comisiones, ventas por canal, etc.).
        // Se crean en código y se insertan en el flow antes de "Ventas recientes".
        private void ConstruirTarjetasGerenciales()
        {
            flowLayoutPanel1.AutoScroll = true;

            _lblUtilidadBruta = AgregarTarjetaKpi("Utilidad bruta (hoy)", EstilosFmo.MentaClaro);
            _lblUtilidadNeta = AgregarTarjetaKpi("Utilidad neta (hoy)", EstilosFmo.Menta);
            _lblComisiones = AgregarTarjetaKpi("Comisiones (hoy)", EstilosFmo.Mango);
            _lblVentasRuta = AgregarTarjetaKpi("Ventas por ruta (hoy)", EstilosFmo.Arandano);
            _lblVentasMayoristas = AgregarTarjetaKpi("Ventas mayoristas (hoy)", EstilosFmo.Fresa);
            _lblProductoTop = AgregarTarjetaKpi("Producto top (hoy)", EstilosFmo.TextoFuerte);
        }

        private Guna2HtmlLabel AgregarTarjetaKpi(string caption, Color acento)
        {
            var card = new Guna2CustomGradientPanel
            {
                Size = new Size(200, 150),
                Margin = new Padding(8),
                FillColor = EstilosFmo.Superficie,
                FillColor2 = EstilosFmo.SuperficieHundida,
                BorderColor = EstilosFmo.Borde,
                BorderRadius = 14
            };

            var lblCap = new Guna2HtmlLabel
            {
                Text = caption,
                Location = new Point(16, 16),
                Size = new Size(168, 40),
                Font = EstilosFmo.Fuente(9.5F, FontStyle.Bold),
                ForeColor = EstilosFmo.TextoTenue,
                BackColor = Color.Transparent
            };

            var lblVal = new Guna2HtmlLabel
            {
                Text = "—",
                Location = new Point(16, 66),
                Size = new Size(168, 50),
                Font = EstilosFmo.Fuente(18F, FontStyle.Bold),
                ForeColor = acento,
                BackColor = Color.Transparent
            };

            card.Controls.Add(lblCap);
            card.Controls.Add(lblVal);

            flowLayoutPanel1.Controls.Add(card);
            return lblVal;
        }

        private void AplicarTema()
        {
            BackColor = EstilosFmo.Fondo;
            flowLayoutPanel1.BackColor = EstilosFmo.Fondo;

            // Contenedores externos de cada tarjeta.
            foreach (var p in new[] { guna2Panel1, guna2Panel3, guna2Panel5, guna2Panel7, guna2Panel2 })
                p.FillColor = EstilosFmo.Fondo;

            // Paneles con degradado de cada tarjeta -> superficie oscura.
            foreach (var g in new[] { guna2CustomGradientPanel1, guna2CustomGradientPanel2, guna2CustomGradientPanel3, guna2CustomGradientPanel4 })
            {
                g.FillColor = EstilosFmo.Superficie;
                g.FillColor2 = EstilosFmo.SuperficieHundida;
                g.BorderColor = EstilosFmo.Borde;
            }

            // Títulos (captions) y subtítulos de las tarjetas.
            foreach (var cap in new[] { guna2HtmlLabel3, guna2HtmlLabel14, guna2HtmlLabel15, guna2HtmlLabel16, guna2HtmlLabel7, guna2HtmlLabel9 })
            {
                cap.ForeColor = EstilosFmo.TextoTenue;
                cap.Font = EstilosFmo.Fuente(9.5F, FontStyle.Bold);
            }

            // Valores grandes, cada uno con su acento.
            guna2HtmlLabel2.ForeColor = EstilosFmo.MentaClaro;   // ventas de hoy
            guna2HtmlLabel8.ForeColor = EstilosFmo.Fresa;        // tickets
            guna2HtmlLabel10.ForeColor = EstilosFmo.Mango;       // próximos a vencer
            guna2HtmlLabel13.ForeColor = EstilosFmo.Arandano;    // stock bajo
            foreach (var val in new[] { guna2HtmlLabel2, guna2HtmlLabel8, guna2HtmlLabel10, guna2HtmlLabel13 })
                val.Font = EstilosFmo.Fuente(18F, FontStyle.Bold);

            // Ocultamos el indicador de variación (no se calcula aún) y el UC placeholder.
            guna2HtmlLabel1.Visible = false;
            guna2HtmlLabel4.Visible = false;
            ucTarjetas_de_Inicio1.Visible = false;
        }

        private void ConstruirVentasRecientes()
        {
            var titulo = new Guna2HtmlLabel
            {
                Text = "Ventas recientes",
                Location = new Point(16, 12),
                Size = new Size(300, 28),
                Font = EstilosFmo.Fuente(13F, FontStyle.Bold),
                ForeColor = EstilosFmo.TextoFuerte,
                BackColor = Color.Transparent
            };

            _dgvRecientes = new Guna2DataGridView
            {
                Location = new Point(16, 50),
                Size = new Size(guna2Panel2.Width - 32, guna2Panel2.Height - 66),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };
            EstilosFmo.Tabla(_dgvRecientes);

            guna2Panel2.Controls.Add(titulo);
            guna2Panel2.Controls.Add(_dgvRecientes);
        }

        private void CargarDatos()
        {
            CargarIndicadoresVentas();

            try { guna2HtmlLabel10.Text = ProductoDAO.ProductosPorVencer(5).Count.ToString(); }
            catch (Exception) { guna2HtmlLabel10.Text = "—"; }

            try { guna2HtmlLabel13.Text = ProductoDAO.ProductoBajoStock().Count.ToString(); }
            catch (Exception) { guna2HtmlLabel13.Text = "—"; }

            CargarIndicadoresGerenciales();
        }

        private void CargarIndicadoresGerenciales()
        {
            DateTime hoy = DateTime.Today;

            try { _lblUtilidadBruta.Text = "$" + ReporteServicio.UtilidadBrutaDia(hoy).ToString("N2"); }
            catch (Exception) { _lblUtilidadBruta.Text = "—"; }

            try { _lblUtilidadNeta.Text = "$" + ReporteServicio.UtilidadNetaDia(hoy).ToString("N2"); }
            catch (Exception) { _lblUtilidadNeta.Text = "—"; }

            try { _lblComisiones.Text = "$" + ReporteServicio.ComisionesDia(hoy).ToString("N2"); }
            catch (Exception) { _lblComisiones.Text = "—"; }

            try { _lblVentasRuta.Text = "$" + ReporteServicio.VentasPorRutaDia(hoy).ToString("N2"); }
            catch (Exception) { _lblVentasRuta.Text = "—"; }

            try { _lblVentasMayoristas.Text = "$" + ReporteServicio.VentasMayoristasDia(hoy).ToString("N2"); }
            catch (Exception) { _lblVentasMayoristas.Text = "—"; }

            try
            {
                var top = ReporteServicio.ProductoTopDia(hoy);
                _lblProductoTop.Text = top.unidades > 0 ? $"{top.nombre} ({top.unidades})" : "—";
                _lblProductoTop.Font = EstilosFmo.Fuente(13F, FontStyle.Bold);
            }
            catch (Exception) { _lblProductoTop.Text = "—"; }
        }

        private void CargarIndicadoresVentas()
        {
            try
            {
                DataTable ventas = VentaSucursalServicio.ListarVentasSucursal();
                _dgvRecientes.DataSource = ventas;
                EstilosFmo.MostrarSoloColumnas(_dgvRecientes,
                    ("fecha_registro", "Fecha"),
                    ("cajero", "Cajero"),
                    ("subtotal", "Subtotal"),
                    ("descuento", "Descuento"),
                    ("total", "Total"));

                string colFecha = EstilosFmo.ColumnaPorCandidatos(ventas, "fecha_registro", "fecha");
                string colTotal = EstilosFmo.ColumnaPorCandidatos(ventas, "total");
                string colActivo = EstilosFmo.ColumnaPorCandidatos(ventas, "activo", "estado");

                decimal ventasHoy = 0m;
                int tickets = 0;

                foreach (DataRow fila in ventas.Rows)
                {
                    if (colActivo != null && fila[colActivo] != DBNull.Value)
                    {
                        string a = fila[colActivo].ToString();
                        if (a == "0" || a.Equals("false", StringComparison.OrdinalIgnoreCase)) continue;
                    }

                    if (colFecha == null || !DateTime.TryParse(fila[colFecha]?.ToString(), out DateTime fecha))
                        continue;

                    if (fecha.Date != DateTime.Today) continue;

                    tickets++;
                    if (colTotal != null && decimal.TryParse(fila[colTotal]?.ToString(), out decimal total))
                        ventasHoy += total;
                }

                guna2HtmlLabel2.Text = "$" + ventasHoy.ToString("N2");
                guna2HtmlLabel8.Text = tickets.ToString();
            }
            catch (Exception)
            {
                guna2HtmlLabel2.Text = "—";
                guna2HtmlLabel8.Text = "—";
            }
        }
    }
}
