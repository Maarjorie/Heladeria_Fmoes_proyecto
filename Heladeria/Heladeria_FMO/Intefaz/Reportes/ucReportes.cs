using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Heladeria_FMO.Servicio;
using Heladeria_FMO.Utileria;

namespace Heladeria_FMO.Intefaz.Reportes
{
    // Módulo de reportes del administrador: selecciona el reporte y el rango de
    // fechas, y muestra el resultado en una tabla.
    public partial class ucReportes : UserControl
    {
        public ucReportes()
        {
            InitializeComponent();
            AplicarTema();
            CargarOpciones();
        }

        private void AplicarTema()
        {
            BackColor = EstilosFmo.Fondo;
            pnlHeader.FillColor = EstilosFmo.Fondo;
            EstilosFmo.Tarjeta(pnlCard);
            EstilosFmo.Tabla(dgvReporte);
            EstilosFmo.Combo(cboReporte);
            EstilosFmo.BotonPrimario(btnGenerar);

            lblTitulo.Font = EstilosFmo.Fuente(18F, FontStyle.Bold);
            lblTitulo.ForeColor = EstilosFmo.TextoFuerte;
            foreach (var l in new[] { lblDesde, lblHasta })
            {
                l.Font = EstilosFmo.Fuente(9.5F);
                l.ForeColor = EstilosFmo.TextoTenue;
            }

            foreach (var d in new[] { dtpInicio, dtpFin })
            {
                d.FillColor = EstilosFmo.SuperficieHundida;
                d.ForeColor = EstilosFmo.TextoFuerte;
                d.BorderColor = EstilosFmo.Borde;
                d.BorderRadius = 8;
            }
        }

        private void CargarOpciones()
        {
            cboReporte.Items.AddRange(new object[]
            {
                "Ventas del día",
                "Resumen de caja",
                "Ventas por ruta",
                "Productos más vendidos",
                "Productos dañados",
                "Comisiones de vendedores",
                "Ranking de vendedores",
                "Rentabilidad por ruta"
            });
            cboReporte.SelectedIndex = 0;
            cboReporte.SelectedIndexChanged += (s, e) => AjustarFechas();
            AjustarFechas();
        }

        // "Ventas del día" usa una sola fecha; el resto usa rango.
        private void AjustarFechas()
        {
            bool rango = cboReporte.SelectedIndex != 0;
            lblHasta.Visible = rango;
            dtpFin.Visible = rango;
            lblDesde.Text = rango ? "Desde" : "Fecha";
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            DateTime inicio = dtpInicio.Value.Date;
            DateTime fin = dtpFin.Value.Date;

            if (cboReporte.SelectedIndex != 0 && fin < inicio)
            {
                MensajeFmo.Advertencia("La fecha 'Hasta' no puede ser menor que 'Desde'.", "Reportes");
                return;
            }

            try
            {
                DataTable datos = cboReporte.SelectedIndex switch
                {
                    0 => ReporteServicio.VentasDia(inicio),
                    1 => ReporteServicio.Caja(inicio, fin),
                    2 => ReporteServicio.VentasPorRuta(inicio, fin),
                    3 => ReporteServicio.ProductosMasVendidos(inicio, fin),
                    4 => ReporteServicio.ProductosDanados(inicio, fin),
                    5 => ReporteServicio.ComisionesVendedores(inicio, fin),
                    6 => ReporteServicio.RankingVendedores(inicio, fin),
                    7 => ReporteServicio.RentabilidadRuta(inicio, fin),
                    _ => null
                };

                dgvReporte.DataSource = datos;

                if (datos == null || datos.Rows.Count == 0)
                    MensajeFmo.Info("El reporte no tiene datos para el período seleccionado.", "Reportes");
            }
            catch (Exception ex)
            {
                MensajeFmo.Error($"No se pudo generar el reporte.\n{ex.Message}", "Reportes");
            }
        }
    }
}
