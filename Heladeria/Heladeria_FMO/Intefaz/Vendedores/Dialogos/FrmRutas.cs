using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Heladeria_FMO.Modelos;
using Heladeria_FMO.Servicio;
using Heladeria_FMO.Utileria;

namespace Heladeria_FMO.Intefaz.Vendedores.Dialogos
{
    // Gestión de rutas (master-detail): lista + alta/edición con responsable y
    // horario, más baja de estado.
    public partial class FrmRutas : Form
    {
        private int _idSeleccionado;
        private bool _activoSeleccionado;
        private string _cId, _cNombre, _cZona, _cResp, _cInicio, _cFin, _cActivo;

        public FrmRutas()
        {
            InitializeComponent();
            AplicarTema();
            CargarResponsables();
            CargarRutas();
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
            EstilosFmo.BotonContorno(btnNuevo);

            foreach (var lbl in new[] { lblNombre, lblZona, lblResp, lblInicio, lblFin })
            {
                lbl.Font = EstilosFmo.Fuente(9.5F);
                lbl.ForeColor = EstilosFmo.TextoTenue;
            }

            EstilosFmo.CajaTexto(txtNombre);
            EstilosFmo.CajaTexto(txtZona);
            EstilosFmo.CajaTexto(txtInicio);
            EstilosFmo.CajaTexto(txtFin);
            EstilosFmo.Combo(cboResponsable);

            EstilosFmo.BotonPrimario(btnGuardar);
            EstilosFmo.BotonContorno(btnDesactivar);
            btnDesactivar.ForeColor = EstilosFmo.Cereza;
        }

        private void BtnCerrar_Click(object sender, EventArgs e) => Close();

        private void BtnNuevo_Click(object sender, EventArgs e) => LimpiarFormulario();

        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) dgv_SelectionChanged(sender, EventArgs.Empty);
        }

        // ───────────────────────── Datos ─────────────────────────
        private void CargarResponsables()
        {
            try
            {
                cboResponsable.DataSource = UsuarioServicio.ListarUsuariosActivos();
                cboResponsable.DisplayMember = nameof(Usuario.Nombre);
                cboResponsable.ValueMember = nameof(Usuario.id_Usuario);
                cboResponsable.SelectedIndex = -1;
            }
            catch (Exception)
            {
                MensajeFmo.Advertencia("No se pudieron cargar los responsables.", "Rutas");
            }
        }

        private void CargarRutas()
        {
            try
            {
                DataTable tabla = RutaServicio.ListarRutas();
                dgv.DataSource = tabla;

                _cId = EstilosFmo.ColumnaPorCandidatos(tabla, "id_ruta", "idruta", "id");
                _cNombre = EstilosFmo.ColumnaPorCandidatos(tabla, "nombre");
                _cZona = EstilosFmo.ColumnaPorCandidatos(tabla, "zona");
                _cResp = EstilosFmo.ColumnaPorCandidatos(tabla, "id_responsable", "responsable");
                _cInicio = EstilosFmo.ColumnaPorCandidatos(tabla, "horario_inicio", "inicio");
                _cFin = EstilosFmo.ColumnaPorCandidatos(tabla, "horario_fin", "fin");
                _cActivo = EstilosFmo.ColumnaPorCandidatos(tabla, "activo", "estado");

                var cols = new System.Collections.Generic.List<(string, string)>();
                void Add(string c, string h) { if (c != null) cols.Add((c, h)); }
                Add(_cNombre, "Nombre");
                Add(_cZona, "Zona");
                Add(_cInicio, "Inicio");
                Add(_cFin, "Fin");
                EstilosFmo.MostrarSoloColumnas(dgv, cols.ToArray());

                LimpiarFormulario();
            }
            catch (Exception)
            {
                MensajeFmo.Advertencia("No se pudieron cargar las rutas.", "Rutas");
            }
        }

        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv.CurrentRow == null || _cId == null) return;

            object idVal = dgv.CurrentRow.Cells[_cId].Value;
            _idSeleccionado = idVal != null && int.TryParse(idVal.ToString(), out int id) ? id : 0;

            txtNombre.Text = Celda(_cNombre);
            txtZona.Text = Celda(_cZona);
            txtInicio.Text = Hora(_cInicio);
            txtFin.Text = Hora(_cFin);

            if (_cResp != null && dgv.CurrentRow.Cells[_cResp].Value is { } respVal
                && int.TryParse(respVal.ToString(), out int idResp))
                cboResponsable.SelectedValue = idResp;

            _activoSeleccionado = ParseActivo(_cActivo);
            ActualizarBotonEstado();
        }

        private string Celda(string col)
        {
            if (col == null || dgv.CurrentRow == null) return "";
            return dgv.CurrentRow.Cells[col].Value?.ToString() ?? "";
        }

        private bool ParseActivo(string col)
        {
            if (col == null || dgv.CurrentRow == null) return true;
            object v = dgv.CurrentRow.Cells[col].Value;
            if (v is bool b) return b;
            string s = v?.ToString() ?? "";
            return s == "1" || s.Equals("true", StringComparison.OrdinalIgnoreCase) || s.Equals("activo", StringComparison.OrdinalIgnoreCase);
        }

        private void ActualizarBotonEstado()
        {
            if (_idSeleccionado > 0 && !_activoSeleccionado)
            {
                btnDesactivar.Text = "Activar";
                btnDesactivar.ForeColor = EstilosFmo.Menta;
            }
            else
            {
                btnDesactivar.Text = "Dar de baja";
                btnDesactivar.ForeColor = EstilosFmo.Cereza;
            }
        }

        private string Hora(string col)
        {
            if (col == null || dgv.CurrentRow == null) return "";
            object v = dgv.CurrentRow.Cells[col].Value;
            if (v is TimeSpan ts) return ts.ToString(@"hh\:mm");
            if (v != null && TimeSpan.TryParse(v.ToString(), out TimeSpan parsed)) return parsed.ToString(@"hh\:mm");
            return v?.ToString() ?? "";
        }

        private void LimpiarFormulario()
        {
            _idSeleccionado = 0;
            _activoSeleccionado = false;
            txtNombre.Clear();
            txtZona.Clear();
            txtInicio.Clear();
            txtFin.Clear();
            cboResponsable.SelectedIndex = -1;
            ActualizarBotonEstado();
            txtNombre.Focus();
        }

        // ───────────────────────── Acciones ─────────────────────────
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (cboResponsable.SelectedValue is not int idResponsable || idResponsable <= 0)
            {
                MensajeFmo.Info("Selecciona un responsable.", "Rutas");
                return;
            }

            if (!TimeSpan.TryParse(txtInicio.Text.Trim(), CultureInfo.InvariantCulture, out TimeSpan inicio) ||
                !TimeSpan.TryParse(txtFin.Text.Trim(), CultureInfo.InvariantCulture, out TimeSpan fin))
            {
                MensajeFmo.Advertencia("Ingresa horarios válidos en formato HH:mm.", "Rutas");
                return;
            }

            var ruta = new Ruta
            {
                IdRuta = _idSeleccionado,
                Nombre = txtNombre.Text.Trim(),
                Zona = txtZona.Text.Trim(),
                IdResponsable = idResponsable,
                HorarioInicio = inicio,
                HorarioFin = fin
            };

            try
            {
                bool ok = _idSeleccionado > 0
                    ? RutaServicio.EditarRuta(ruta)
                    : RutaServicio.InsertarRuta(ruta);

                if (ok) MensajeFmo.Exito("Ruta guardada.", "Rutas"); else MensajeFmo.Advertencia("No se guardó la ruta.", "Rutas");

                if (ok) CargarRutas();
            }
            catch (Exception ex)
            {
                MensajeFmo.Advertencia(ex.Message, "Rutas");
            }
        }

        private void btnDesactivar_Click(object sender, EventArgs e)
        {
            if (_idSeleccionado <= 0)
            {
                MensajeFmo.Info("Selecciona una ruta de la lista.", "Rutas");
                return;
            }

            bool nuevo = !_activoSeleccionado;
            string accion = nuevo ? "activar" : "dar de baja";
            if (!MensajeFmo.Confirmar($"¿Seguro que deseas {accion} la ruta \"{txtNombre.Text}\"?", "Rutas"))
                return;

            try
            {
                bool ok = RutaServicio.CambiarEstadoRuta(new Ruta { IdRuta = _idSeleccionado, Activo = nuevo });
                if (ok) MensajeFmo.Exito(nuevo ? "Ruta activada." : "Ruta dada de baja.", "Rutas"); else MensajeFmo.Advertencia("No se pudo cambiar el estado.", "Rutas");
                if (ok) CargarRutas();
            }
            catch (Exception ex)
            {
                MensajeFmo.Error(ex.Message, "Rutas");
            }
        }
    }
}
