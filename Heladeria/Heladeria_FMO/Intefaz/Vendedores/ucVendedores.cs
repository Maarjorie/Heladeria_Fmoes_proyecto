using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Heladeria_FMO.Acceso_a_datos_db;
using Heladeria_FMO.Intefaz.Vendedores.Dialogos;
using Heladeria_FMO.Servicio;
using Heladeria_FMO.Utileria;

namespace Heladeria_FMO.Intefaz.Vendedores
{
    // Módulo de vendedores ambulantes: fichas de salida y liquidaciones pendientes
    // de validación por un supervisor.
    public partial class ucVendedores : UserControl
    {
        private string _colIdLiquidacion;
        private Guna.UI2.WinForms.Guna2Button btnRutas;
        private Guna.UI2.WinForms.Guna2Button btnGestionVendedores;

        public ucVendedores()
        {
            InitializeComponent();
            AplicarTema();
            CrearBotonesCatalogo();
            CargarDatos();
        }

        // Botones para abrir los catálogos relacionados (rutas, etc.).
        private void CrearBotonesCatalogo()
        {
            btnRutas = new Guna.UI2.WinForms.Guna2Button
            {
                Text = "Rutas",
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new System.Drawing.Point(610, 12),
                Size = new System.Drawing.Size(120, 42)
            };
            EstilosFmo.BotonContorno(btnRutas);
            btnRutas.Click += (s, e) =>
            {
                using var dialogo = new Dialogos.FrmRutas();
                dialogo.ShowDialog(this.FindForm());
            };
            pnlHeader.Controls.Add(btnRutas);

            btnGestionVendedores = new Guna.UI2.WinForms.Guna2Button
            {
                Text = "Vendedores",
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new System.Drawing.Point(480, 12),
                Size = new System.Drawing.Size(120, 42),
                // Solo el administrador gestiona (alta/edición) de empleados.
                Visible = Sesion.EsAdministrador
            };
            EstilosFmo.BotonContorno(btnGestionVendedores);
            btnGestionVendedores.Click += (s, e) =>
            {
                using var dialogo = new FrmVendedores();
                dialogo.ShowDialog(this.FindForm());
            };
            pnlHeader.Controls.Add(btnGestionVendedores);
        }

        private void AplicarTema()
        {
            BackColor = EstilosFmo.Fondo;
            pnlHeader.FillColor = EstilosFmo.Fondo;
            tlp.BackColor = EstilosFmo.Fondo;

            EstilosFmo.Tarjeta(pnlFichas);
            EstilosFmo.Tarjeta(pnlLiq);
            EstilosFmo.Tabla(dgvSalidas);
            EstilosFmo.Tabla(dgvLiquidaciones);

            lblTitulo.Font = EstilosFmo.Fuente(18F, FontStyle.Bold);
            lblTitulo.ForeColor = EstilosFmo.TextoFuerte;

            foreach (var t in new[] { lblFichasTit, lblLiqTit })
            {
                t.Font = EstilosFmo.Fuente(13F, FontStyle.Bold);
                t.ForeColor = EstilosFmo.TextoFuerte;
            }

            EstilosFmo.BotonPrimario(btnNuevaFicha);
            EstilosFmo.BotonContorno(btnRefrescar);
            EstilosFmo.BotonSecundario(btnValidar);
        }

        private void CargarDatos()
        {
            try
            {
                dgvSalidas.DataSource = SalidaDAO.ListarSalidasRuta();
            }
            catch (Exception)
            {
                MensajeFmo.Advertencia("No se pudieron cargar las fichas de salida.", "Vendedores");
            }

            try
            {
                DataTable liquidaciones = LiquidacionRutaServicio.ListarPendientes();
                dgvLiquidaciones.DataSource = liquidaciones;
                _colIdLiquidacion = EstilosFmo.ColumnaPorCandidatos(liquidaciones, "id_liquidacion", "idliquidacion", "id");
            }
            catch (Exception)
            {
                MensajeFmo.Advertencia("No se pudieron cargar las liquidaciones.", "Vendedores");
            }
        }

        private int ObtenerIdLiquidacionSeleccionada()
        {
            if (dgvLiquidaciones.CurrentRow == null || _colIdLiquidacion == null)
                return 0;

            object valor = dgvLiquidaciones.CurrentRow.Cells[_colIdLiquidacion].Value;
            return valor != null && int.TryParse(valor.ToString(), out int id) ? id : 0;
        }

        private void btnNuevaFicha_Click(object sender, EventArgs e)
        {
            using (var dialogo = new FrmNuevaFicha())
            {
                dialogo.ShowDialog(this.FindForm());
            }
            CargarDatos();
        }

        private void btnValidar_Click(object sender, EventArgs e)
        {
            int idLiquidacion = ObtenerIdLiquidacionSeleccionada();
            if (idLiquidacion <= 0)
            {
                MensajeFmo.Info("Selecciona una liquidación de la tabla.", "Vendedores");
                return;
            }

            var confirmar = (MensajeFmo.Confirmar("¿Validar la liquidación seleccionada?", "Validar liquidación") ? DialogResult.Yes : DialogResult.No);
            if (confirmar != DialogResult.Yes) return;

            try
            {
                bool ok = LiquidacionRutaServicio.ValidarLiquidacion(idLiquidacion, Sesion.UsuarioActivo?.id_Usuario ?? 0);
                if (ok) MensajeFmo.Exito("Liquidación validada.", "Vendedores"); else MensajeFmo.Advertencia("No se pudo validar la liquidación.", "Vendedores");
                CargarDatos();
            }
            catch (Exception ex)
            {
                MensajeFmo.Error(ex.Message, "Vendedores");
            }
        }

        private void btnRefrescar_Click(object sender, EventArgs e) => CargarDatos();
    }
}
