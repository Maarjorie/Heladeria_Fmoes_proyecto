using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Heladeria_FMO.Servicio;
using Heladeria_FMO.Utileria;

namespace Heladeria_FMO.Intefaz.Caja
{
    // Apertura, arqueo y cierre de caja. El estado de la caja abierta vive en
    // Sesion (Sesion.IdCajaActiva), de modo que el punto de venta pueda registrar
    // ventas contra la caja activa.
    public partial class ucCaja : UserControl
    {
        public ucCaja()
        {
            InitializeComponent();
            AplicarTema();

            txtCajero.Text = Sesion.UsuarioActivo?.Nombre ?? "—";
            ActualizarEstado();
        }

        // ───────────────────────── Tema oscuro ─────────────────────────
        private void AplicarTema()
        {
            BackColor = EstilosFmo.Fondo;
            flpCajas.BackColor = EstilosFmo.Fondo;

            EstilosFmo.Tarjeta(pnlAbrir);
            EstilosFmo.Tarjeta(pnlArqueo);

            foreach (var titulo in new[] { lblAbrirTitulo, lblArqueoTitulo })
            {
                titulo.Font = EstilosFmo.Fuente(16F, FontStyle.Bold);
                titulo.ForeColor = EstilosFmo.TextoFuerte;
            }

            foreach (var cap in new[] { lblCajeroCap, lblFondoCap, lblEsperadoCap, lblContadoCap, lblDiferenciaCap })
            {
                cap.Font = EstilosFmo.Fuente(10F);
                cap.ForeColor = EstilosFmo.TextoTenue;
            }

            foreach (var val in new[] { lblEsperadoVal, lblDiferenciaVal })
            {
                val.Font = EstilosFmo.Fuente(12F, FontStyle.Bold);
                val.ForeColor = EstilosFmo.TextoFuerte;
            }

            lblEstado.Font = EstilosFmo.Fuente(9.5F, FontStyle.Bold);
            lblEstado.ForeColor = EstilosFmo.TextoTenue;

            EstilosFmo.CajaTexto(txtCajero);
            EstilosFmo.CajaTexto(txtFondo);
            EstilosFmo.CajaTexto(txtContado);

            EstilosFmo.BotonPrimario(btnAbrir);
            EstilosFmo.BotonSecundario(btnCerrar);
            EstilosFmo.BotonContorno(btnArqueo);

            // Tarjeta de movimientos (gastos/ingresos extraordinarios).
            EstilosFmo.Tarjeta(pnlMovimientos);
            lblMovTitulo.Font = EstilosFmo.Fuente(16F, FontStyle.Bold);
            lblMovTitulo.ForeColor = EstilosFmo.TextoFuerte;
            EstilosFmo.BotonContorno(btnNuevoMovimiento);
            EstilosFmo.Tabla(dgvMovimientos);
        }

        // ───────────────────────── Estado UI ─────────────────────────
        private decimal MontoEsperado => Sesion.FondoCajaActiva + Sesion.TotalVendidoCaja;

        private void ActualizarEstado()
        {
            bool abierta = Sesion.HayCajaAbierta;

            txtFondo.Enabled = !abierta;
            btnAbrir.Enabled = !abierta;

            txtContado.Enabled = abierta;
            btnCerrar.Enabled = abierta;
            btnArqueo.Enabled = abierta;
            btnNuevoMovimiento.Enabled = abierta;

            if (abierta)
            {
                lblEstado.Text = $"Caja #{Sesion.IdCajaActiva} abierta · fondo ${Sesion.FondoCajaActiva:N2}";
                lblEstado.ForeColor = EstilosFmo.MentaClaro;
                lblEsperadoVal.Text = "$" + MontoEsperado.ToString("N2");
                CargarMovimientos();
            }
            else
            {
                lblEstado.Text = "Sin caja abierta";
                lblEstado.ForeColor = EstilosFmo.TextoTenue;
                lblEsperadoVal.Text = "$0.00";
                txtContado.Clear();
                dgvMovimientos.DataSource = null;
            }

            ActualizarDiferencia();
        }

        private void CargarMovimientos()
        {
            try
            {
                dgvMovimientos.DataSource = CajaServicio.ListarMovimientos(Sesion.IdCajaActiva);
                EstilosFmo.MostrarSoloColumnas(dgvMovimientos,
                    ("Tipo", "Tipo"),
                    ("Concepto", "Concepto"),
                    ("Monto", "Monto"),
                    ("FechaRegistro", "Fecha"));
            }
            catch (Exception)
            {
                MensajeFmo.Advertencia("No se pudieron cargar los movimientos de caja.", "Caja");
            }
        }

        private void btnNuevoMovimiento_Click(object sender, EventArgs e)
        {
            if (!Sesion.HayCajaAbierta)
            {
                MensajeFmo.Info("Primero abre una caja.", "Caja");
                return;
            }

            using (var dialogo = new FrmMovimientoCaja())
            {
                dialogo.ShowDialog(this.FindForm());
            }
            CargarMovimientos();
        }

        private void ActualizarDiferencia()
        {
            if (!Sesion.HayCajaAbierta || !TryLeerDecimal(txtContado.Text, out decimal contado))
            {
                lblDiferenciaVal.Text = "—";
                lblDiferenciaVal.ForeColor = EstilosFmo.TextoTenue;
                return;
            }

            decimal diferencia = contado - MontoEsperado;
            lblDiferenciaVal.Text = "$" + diferencia.ToString("N2");
            lblDiferenciaVal.ForeColor = diferencia == 0 ? EstilosFmo.MentaClaro : EstilosFmo.Cereza;
        }

        // Acepta coma o punto como separador decimal.
        private static bool TryLeerDecimal(string texto, out decimal valor)
        {
            texto = (texto ?? "").Trim().Replace(",", ".");
            return decimal.TryParse(texto, NumberStyles.Number, CultureInfo.InvariantCulture, out valor);
        }

        // ───────────────────────── Acciones ─────────────────────────
        private void btnAbrir_Click(object sender, EventArgs e)
        {
            if (Sesion.UsuarioActivo == null)
            {
                MensajeFmo.Advertencia("No hay un usuario en sesión.", "Caja");
                return;
            }

            if (!TryLeerDecimal(txtFondo.Text, out decimal fondo) || fondo < 0)
            {
                MensajeFmo.Advertencia("Ingresa un fondo inicial válido (mayor o igual a 0).", "Datos inválidos");
                txtFondo.Focus();
                return;
            }

            try
            {
                int idCaja = CajaServicio.AbrirCaja(Sesion.UsuarioActivo.id_Usuario, fondo);

                Sesion.IdCajaActiva = idCaja;
                Sesion.FondoCajaActiva = fondo;
                Sesion.TotalVendidoCaja = 0m;

                MensajeFmo.Info($"Caja #{idCaja} abierta con un fondo de ${fondo:N2}.", "Caja abierta");
                ActualizarEstado();
            }
            catch (Exception)
            {
                MensajeFmo.Error("No se pudo abrir la caja. Verifica la conexión con la base de datos.",
                    "Error");
            }
        }

        private void btnArqueo_Click(object sender, EventArgs e)
        {
            if (!Sesion.HayCajaAbierta) return;

            if (!TryLeerDecimal(txtContado.Text, out decimal contado) || contado < 0)
            {
                MensajeFmo.Advertencia("Ingresa el monto contado (mayor o igual a 0).", "Datos inválidos");
                txtContado.Focus();
                return;
            }

            try
            {
                bool ok = CajaServicio.RealizarArqueo(
                    Sesion.IdCajaActiva, Sesion.UsuarioActivo.id_Usuario, MontoEsperado, contado);

                if (ok)
                {
                    decimal dif = contado - MontoEsperado;
                    string detalle = dif == 0 ? "sin diferencias." : $"con una diferencia de ${dif:N2}.";
                    MensajeFmo.Info($"Arqueo registrado {detalle}", "Arqueo de caja");
                }
                else
                {
                    MensajeFmo.Advertencia("No se pudo registrar el arqueo.", "Caja");
                }
            }
            catch (Exception)
            {
                MensajeFmo.Error("No se pudo registrar el arqueo. Verifica la conexión con la base de datos.",
                    "Error");
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            if (!Sesion.HayCajaAbierta) return;

            if (!TryLeerDecimal(txtContado.Text, out decimal contado) || contado < 0)
            {
                MensajeFmo.Advertencia("Ingresa el monto contado para cerrar la caja.", "Datos inválidos");
                txtContado.Focus();
                return;
            }

            var confirmar = (MensajeFmo.Confirmar($"¿Cerrar la caja #{Sesion.IdCajaActiva} con ${contado:N2} contados?",
                "Cerrar caja") ? DialogResult.Yes : DialogResult.No);
            if (confirmar != DialogResult.Yes) return;

            try
            {
                bool ok = CajaServicio.CerrarCaja(Sesion.IdCajaActiva, contado);
                if (ok)
                {
                    MensajeFmo.Info($"Caja #{Sesion.IdCajaActiva} cerrada.", "Caja cerrada");

                    Sesion.IdCajaActiva = 0;
                    Sesion.FondoCajaActiva = 0m;
                    Sesion.TotalVendidoCaja = 0m;
                    ActualizarEstado();
                }
                else
                {
                    MensajeFmo.Advertencia("No se pudo cerrar la caja.", "Caja");
                }
            }
            catch (Exception)
            {
                MensajeFmo.Error("No se pudo cerrar la caja. Verifica la conexión con la base de datos.",
                    "Error");
            }
        }

        private void txtContado_TextChanged(object sender, EventArgs e) => ActualizarDiferencia();
    }
}
