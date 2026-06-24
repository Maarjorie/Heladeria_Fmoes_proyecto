using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Heladeria_FMO.Modelos;
using Heladeria_FMO.Servicio;
using Heladeria_FMO.Utileria;

namespace Heladeria_FMO.Intefaz.Mayorista.Dialogos
{
    // Gestión de clientes mayoristas (master-detail): lista a la izquierda y
    // formulario de alta/edición a la derecha, con baja de estado.
    public class FrmClientes : Form
    {
        private readonly Guna2DataGridView dgv = new();
        private readonly Guna2TextBox txtNombre = new();
        private readonly Guna2TextBox txtNit = new();
        private readonly Guna2TextBox txtTelefono = new();
        private readonly Guna2TextBox txtEncargado = new();
        private readonly Guna2TextBox txtCorreo = new();
        private readonly Guna2TextBox txtDireccion = new();
        private readonly Guna2Button btnNuevo = new();
        private readonly Guna2Button btnGuardar = new();
        private readonly Guna2Button btnDesactivar = new();

        private int _idSeleccionado;
        private bool _activoSeleccionado;
        private string _cId, _cNombre, _cNit, _cTel, _cEncargado, _cCorreo, _cActivo;

        public FrmClientes()
        {
            ConstruirUi();
            CargarClientes();
        }

        private void ConstruirUi()
        {
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            ShowInTaskbar = false;
            ClientSize = new Size(900, 560);
            BackColor = EstilosFmo.Superficie;

            var marco = new Guna2Panel { Dock = DockStyle.Fill };
            EstilosFmo.Tarjeta(marco);
            Controls.Add(marco);

            var titulo = new Guna2HtmlLabel
            {
                Text = "Clientes mayoristas",
                Location = new Point(24, 18),
                Size = new Size(420, 30),
                Font = EstilosFmo.Fuente(16F, FontStyle.Bold),
                ForeColor = EstilosFmo.TextoFuerte,
                BackColor = Color.Transparent
            };
            var btnCerrar = new Guna2Button { Text = "✕", Size = new Size(32, 32), Location = new Point(844, 14), Font = EstilosFmo.Fuente(10F, FontStyle.Bold) };
            EstilosFmo.BotonContorno(btnCerrar);
            btnCerrar.Click += (s, e) => Close();

            EstilosFmo.Tabla(dgv);
            dgv.Location = new Point(24, 60);
            dgv.Size = new Size(430, 430);
            dgv.SelectionChanged += dgv_SelectionChanged;
            dgv.CellClick += (s, e) => { if (e.RowIndex >= 0) dgv_SelectionChanged(s, EventArgs.Empty); };

            btnNuevo.Text = "+ Nuevo";
            EstilosFmo.BotonContorno(btnNuevo);
            btnNuevo.Location = new Point(24, 500);
            btnNuevo.Size = new Size(130, 40);
            btnNuevo.Click += (s, e) => LimpiarFormulario();

            // Formulario (detalle)
            int x = 482;
            var lblNombre = Caption("Nombre comercial", x, 60);
            Input(txtNombre, x, 82, 388);
            var lblNit = Caption("NIT", x, 126);
            Input(txtNit, x, 148, 185);
            var lblTel = Caption("Teléfono", x + 203, 126);
            Input(txtTelefono, x + 203, 148, 185);
            var lblEncargado = Caption("Encargado", x, 192);
            Input(txtEncargado, x, 214, 388);
            var lblCorreo = Caption("Correo", x, 258);
            Input(txtCorreo, x, 280, 388);
            var lblDireccion = Caption("Dirección", x, 324);
            Input(txtDireccion, x, 346, 388);

            btnGuardar.Text = "Guardar";
            EstilosFmo.BotonPrimario(btnGuardar);
            btnGuardar.Location = new Point(x, 440);
            btnGuardar.Size = new Size(185, 44);
            btnGuardar.Click += btnGuardar_Click;

            btnDesactivar.Text = "Dar de baja";
            EstilosFmo.BotonContorno(btnDesactivar);
            btnDesactivar.ForeColor = EstilosFmo.Cereza;
            btnDesactivar.Location = new Point(x + 203, 440);
            btnDesactivar.Size = new Size(185, 44);
            btnDesactivar.Click += btnDesactivar_Click;

            marco.Controls.AddRange(new Control[]
            {
                titulo, btnCerrar, dgv, btnNuevo,
                lblNombre, txtNombre, lblNit, txtNit, lblTel, txtTelefono,
                lblEncargado, txtEncargado, lblCorreo, txtCorreo, lblDireccion, txtDireccion,
                btnGuardar, btnDesactivar
            });
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
            // La dirección no viene en el listado; se conserva en blanco al editar.
            txtDireccion.Text = "";
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
            foreach (var t in new[] { txtNombre, txtNit, txtTelefono, txtEncargado, txtCorreo, txtDireccion })
                t.Clear();
            ActualizarBotonEstado();
            txtNombre.Focus();
        }

        // ───────────────────────── Acciones ─────────────────────────
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            var cliente = new Cliente_mayorista
            {
                IdCliente = _idSeleccionado,
                NombreComercial = txtNombre.Text.Trim(),
                Nit = txtNit.Text.Trim(),
                Encargado = txtEncargado.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),
                Correo = txtCorreo.Text.Trim(),
                Direccion = txtDireccion.Text.Trim()
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
    }
}
