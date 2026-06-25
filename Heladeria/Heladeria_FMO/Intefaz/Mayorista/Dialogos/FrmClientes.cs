using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Heladeria_FMO.Modelos;
using Heladeria_FMO.Servicio;
using Heladeria_FMO.Utileria;

namespace Heladeria_FMO.Intefaz.Mayorista.Dialogos
{
    // Gestión de clientes mayoristas (master-detail): lista a la izquierda y
    // formulario de alta/edición a la derecha, con baja de estado.
    public partial class FrmClientes : Form
    {
        private int _idSeleccionado;
        private bool _activoSeleccionado;
        private string _cId, _cNombre, _cNit, _cTel, _cEncargado, _cCorreo, _cActivo, _cDescuento, _cDireccion;

        public FrmClientes()
        {
            InitializeComponent();
            AplicarTema();
            CargarClientes();
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

            foreach (var lbl in new[] { lblNombre, lblNit, lblTel, lblEncargado, lblCorreo, lblDireccion, lblDescuento })
            {
                lbl.Font = EstilosFmo.Fuente(9.5F);
                lbl.ForeColor = EstilosFmo.TextoTenue;
            }

            foreach (var txt in new[] { txtNombre, txtNit, txtTelefono, txtEncargado, txtCorreo, txtDireccion, txtDescuento })
                EstilosFmo.CajaTexto(txt);

            EstilosFmo.BotonContorno(btnHistorial);
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
        private void CargarClientes()
        {
            try
            {
                DataTable tabla = ClienteMayoristaServicio.ListarClientes();
                dgv.DataSource = tabla;

                _cId = EstilosFmo.ColumnaPorCandidatos(tabla, "id_cliente", "idcliente", "id");
                _cNombre = EstilosFmo.ColumnaPorCandidatos(tabla, "nombre_comercial", "nombre");
                _cNit = EstilosFmo.ColumnaPorCandidatos(tabla, "nit");
                _cTel = EstilosFmo.ColumnaPorCandidatos(tabla, "telefono");
                _cEncargado = EstilosFmo.ColumnaPorCandidatos(tabla, "encargado");
                _cCorreo = EstilosFmo.ColumnaPorCandidatos(tabla, "correo");
                _cActivo = EstilosFmo.ColumnaPorCandidatos(tabla, "activo", "estado");
                _cDescuento = EstilosFmo.ColumnaPorCandidatos(tabla, "descuento_porcentaje", "descuento");
                _cDireccion = EstilosFmo.ColumnaPorCandidatos(tabla, "direccion");

                var cols = new System.Collections.Generic.List<(string, string)>();
                void Add(string c, string h) { if (c != null) cols.Add((c, h)); }
                Add(_cNombre, "Nombre comercial");
                Add(_cNit, "NIT");
                Add(_cEncargado, "Encargado");
                Add(_cTel, "Teléfono");
                Add(_cCorreo, "Correo");
                EstilosFmo.MostrarSoloColumnas(dgv, cols.ToArray());

                LimpiarFormulario();
            }
            catch (Exception)
            {
                MensajeFmo.Advertencia("No se pudieron cargar los clientes.", "Clientes");
            }
        }

        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv.CurrentRow == null || _cId == null) return;

            object idVal = dgv.CurrentRow.Cells[_cId].Value;
            _idSeleccionado = idVal != null && int.TryParse(idVal.ToString(), out int id) ? id : 0;

            txtNombre.Text = Celda(_cNombre);
            txtNit.Text = Celda(_cNit);
            txtTelefono.Text = Celda(_cTel);
            txtEncargado.Text = Celda(_cEncargado);
            txtCorreo.Text = Celda(_cCorreo);
            txtDireccion.Text = Celda(_cDireccion);
            txtDescuento.Text = Celda(_cDescuento);
            _activoSeleccionado = ParseActivo(_cActivo);
            ActualizarBotonEstado();
        }

        private string Celda(string col)
        {
            if (col == null || dgv.CurrentRow == null) return "";
            object v = dgv.CurrentRow.Cells[col].Value;
            return v?.ToString() ?? "";
        }

        private bool ParseActivo(string col)
        {
            if (col == null || dgv.CurrentRow == null) return true;
            object v = dgv.CurrentRow.Cells[col].Value;
            if (v is bool b) return b;
            string s = v?.ToString() ?? "";
            return s == "1" || s.Equals("true", StringComparison.OrdinalIgnoreCase) || s.Equals("activo", StringComparison.OrdinalIgnoreCase);
        }

        // El botón alterna entre "Dar de baja" y "Activar" según el seleccionado.
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

        private void LimpiarFormulario()
        {
            _idSeleccionado = 0;
            _activoSeleccionado = false;
            foreach (var t in new[] { txtNombre, txtNit, txtTelefono, txtEncargado, txtCorreo, txtDireccion, txtDescuento })
                t.Clear();
            ActualizarBotonEstado();
            txtNombre.Focus();
        }

        // ───────────────────────── Acciones ─────────────────────────
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            decimal descuento = 0m;
            string descTexto = (txtDescuento.Text ?? "").Trim().Replace(",", ".");
            if (descTexto.Length > 0 &&
                (!decimal.TryParse(descTexto, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out descuento) || descuento < 0 || descuento > 100))
            {
                MensajeFmo.Advertencia("El descuento debe ser un porcentaje entre 0 y 100.", "Clientes");
                return;
            }

            var cliente = new Cliente_mayorista
            {
                IdCliente = _idSeleccionado,
                NombreComercial = txtNombre.Text.Trim(),
                Nit = txtNit.Text.Trim(),
                Encargado = txtEncargado.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),
                Correo = txtCorreo.Text.Trim(),
                Direccion = txtDireccion.Text.Trim(),
                DescuentoPorcentaje = descuento
            };

            try
            {
                bool ok = _idSeleccionado > 0
                    ? ClienteMayoristaServicio.EditarCliente(cliente)
                    : ClienteMayoristaServicio.RegistrarCliente(cliente);

                if (ok) MensajeFmo.Exito("Cliente guardado.", "Clientes"); else MensajeFmo.Advertencia("No se guardó el cliente.", "Clientes");

                if (ok) CargarClientes();
            }
            catch (Exception ex)
            {
                MensajeFmo.Advertencia(ex.Message, "Clientes");
            }
        }

        private void btnDesactivar_Click(object sender, EventArgs e)
        {
            if (_idSeleccionado <= 0)
            {
                MensajeFmo.Info("Selecciona un cliente de la lista.", "Clientes");
                return;
            }

            bool nuevo = !_activoSeleccionado;
            string accion = nuevo ? "activar" : "dar de baja";
            if (!MensajeFmo.Confirmar($"¿Seguro que deseas {accion} a \"{txtNombre.Text}\"?", "Clientes"))
                return;

            try
            {
                bool ok = ClienteMayoristaServicio.CambiarEstadoCliente(
                    new Cliente_mayorista { IdCliente = _idSeleccionado, Activo = nuevo });
                if (ok) MensajeFmo.Exito(nuevo ? "Cliente activado." : "Cliente dado de baja.", "Clientes"); else MensajeFmo.Advertencia("No se pudo cambiar el estado.", "Clientes");
                if (ok) CargarClientes();
            }
            catch (Exception ex)
            {
                MensajeFmo.Error(ex.Message, "Clientes");
            }
        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            if (_idSeleccionado <= 0)
            {
                MensajeFmo.Info("Selecciona un cliente de la lista.", "Clientes");
                return;
            }

            using var frm = new FrmHistorialCliente(_idSeleccionado, txtNombre.Text);
            frm.ShowDialog(this);
        }
    }
}
