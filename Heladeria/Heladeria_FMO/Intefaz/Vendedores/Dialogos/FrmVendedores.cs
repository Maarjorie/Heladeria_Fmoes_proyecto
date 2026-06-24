using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Heladeria_FMO.Modelos;
using Heladeria_FMO.Servicio;
using Heladeria_FMO.Utileria;

namespace Heladeria_FMO.Intefaz.Vendedores.Dialogos
{
    // Gestión de vendedores ambulantes (master-detail): lista + alta/edición y
    // baja de estado.
    public class FrmVendedores : Form
    {
        private readonly Guna2DataGridView dgv = new();
        private readonly Guna2TextBox txtCodigo = new();
        private readonly Guna2TextBox txtNombre = new();
        private readonly Guna2TextBox txtDui = new();
        private readonly Guna2TextBox txtTelefono = new();
        private readonly Guna2TextBox txtDireccion = new();
        private readonly Guna2Button btnGuardar = new();
        private readonly Guna2Button btnDesactivar = new();
        private readonly Guna2Button btnFoto = new();
        private readonly Guna2PictureBox picFoto = new();

        private int _idSeleccionado;
        private string _fotoRuta;
        private bool _activoSeleccionado;

        public FrmVendedores()
        {
            ConstruirUi();
            CargarVendedores();
        }

        private void ConstruirUi()
        {
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            ShowInTaskbar = false;
            ClientSize = new Size(900, 540);
            BackColor = EstilosFmo.Superficie;

            var marco = new Guna2Panel { Dock = DockStyle.Fill };
            EstilosFmo.Tarjeta(marco);
            Controls.Add(marco);

            var titulo = new Guna2HtmlLabel
            {
                Text = "Vendedores",
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
            dgv.Size = new Size(430, 410);
            dgv.SelectionChanged += dgv_SelectionChanged;
            dgv.CellClick += (s, e) => { if (e.RowIndex >= 0) dgv_SelectionChanged(s, EventArgs.Empty); };

            var btnNuevo = new Guna2Button { Text = "+ Nuevo", Location = new Point(24, 482), Size = new Size(130, 40) };
            EstilosFmo.BotonContorno(btnNuevo);
            btnNuevo.Click += (s, e) => LimpiarFormulario();

            int x = 482;
            var lblCodigo = Caption("Código de empleado", x, 60);
            Input(txtCodigo, x, 82, 185);
            var lblDui = Caption("DUI", x + 203, 60);
            Input(txtDui, x + 203, 82, 185);
            var lblNombre = Caption("Nombre", x, 126);
            Input(txtNombre, x, 148, 388);
            var lblTel = Caption("Teléfono", x, 192);
            Input(txtTelefono, x, 214, 388);
            var lblDir = Caption("Dirección", x, 258);
            Input(txtDireccion, x, 280, 388);

            // Fotografía del empleado.
            var lblFoto = Caption("Fotografía", x, 324);
            btnFoto.Text = "Seleccionar foto…";
            EstilosFmo.BotonContorno(btnFoto);
            btnFoto.Location = new Point(x, 346);
            btnFoto.Size = new Size(185, 38);
            btnFoto.Click += btnFoto_Click;

            picFoto.Location = new Point(x + 203, 320);
            picFoto.Size = new Size(96, 96);
            picFoto.SizeMode = PictureBoxSizeMode.Zoom;
            picFoto.BackColor = EstilosFmo.SuperficieHundida;

            btnGuardar.Text = "Guardar";
            EstilosFmo.BotonPrimario(btnGuardar);
            btnGuardar.Location = new Point(x, 430);
            btnGuardar.Size = new Size(185, 44);
            btnGuardar.Click += btnGuardar_Click;

            btnDesactivar.Text = "Dar de baja";
            EstilosFmo.BotonContorno(btnDesactivar);
            btnDesactivar.ForeColor = EstilosFmo.Cereza;
            btnDesactivar.Location = new Point(x + 203, 430);
            btnDesactivar.Size = new Size(185, 44);
            btnDesactivar.Click += btnDesactivar_Click;

            marco.Controls.AddRange(new Control[]
            {
                titulo, btnCerrar, dgv, btnNuevo,
                lblCodigo, txtCodigo, lblDui, txtDui, lblNombre, txtNombre,
                lblTel, txtTelefono, lblDir, txtDireccion,
                lblFoto, btnFoto, picFoto,
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
        private void CargarVendedores()
        {
            try
            {
                List<Vendedor> vendedores = VendedorServicio.ListarVendedores();
                dgv.DataSource = vendedores;
                EstilosFmo.MostrarSoloColumnas(dgv,
                    ("CodigoEmpleado", "Código"),
                    ("Nombre", "Nombre"),
                    ("Dui", "DUI"),
                    ("Telefono", "Teléfono"));
                LimpiarFormulario();
            }
            catch (Exception)
            {
                MensajeFmo.Advertencia("No se pudieron cargar los vendedores.", "Vendedores");
            }
        }

        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv.CurrentRow?.DataBoundItem is not Vendedor v) return;

            _idSeleccionado = v.IdVendedor;
            txtCodigo.Text = v.CodigoEmpleado;
            txtNombre.Text = v.Nombre;
            txtDui.Text = v.Dui;
            txtTelefono.Text = v.Telefono;
            // La dirección no viene en el listado; queda en blanco al editar.
            txtDireccion.Text = v.Direccion ?? "";
            _fotoRuta = v.Fotografia;
            _activoSeleccionado = v.Estado;
            CargarPreviewFoto(_fotoRuta);
            ActualizarBotonEstado();
        }

        private void LimpiarFormulario()
        {
            _idSeleccionado = 0;
            _activoSeleccionado = false;
            foreach (var t in new[] { txtCodigo, txtNombre, txtDui, txtTelefono, txtDireccion })
                t.Clear();
            _fotoRuta = null;
            CargarPreviewFoto(null);
            ActualizarBotonEstado();
            txtCodigo.Focus();
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

        // Carga la imagen en el preview sin bloquear el archivo en disco.
        private void CargarPreviewFoto(string ruta)
        {
            picFoto.Image?.Dispose();
            if (!string.IsNullOrWhiteSpace(ruta) && System.IO.File.Exists(ruta))
                using (var bmp = new Bitmap(ruta)) picFoto.Image = new Bitmap(bmp);
            else
                picFoto.Image = null;
        }

        private void btnFoto_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog
            {
                Title = "Seleccionar fotografía",
                Filter = "Imágenes|*.jpg;*.jpeg;*.png;*.bmp"
            };
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                _fotoRuta = ofd.FileName;
                CargarPreviewFoto(_fotoRuta);
            }
        }

        // ───────────────────────── Acciones ─────────────────────────
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            var vendedor = new Vendedor
            {
                IdVendedor = _idSeleccionado,
                CodigoEmpleado = txtCodigo.Text.Trim(),
                Nombre = txtNombre.Text.Trim(),
                Dui = txtDui.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),
                Direccion = txtDireccion.Text.Trim(),
                Fotografia = _fotoRuta ?? ""
            };

            try
            {
                bool ok = _idSeleccionado > 0
                    ? VendedorServicio.EditarVendedor(vendedor)
                    : VendedorServicio.InsertarVendedor(vendedor);

                if (ok) MensajeFmo.Exito("Vendedor guardado.", "Vendedores"); else MensajeFmo.Advertencia("No se guardó el vendedor.", "Vendedores");

                if (ok) CargarVendedores();
            }
            catch (Exception ex)
            {
                MensajeFmo.Advertencia(ex.Message, "Vendedores");
            }
        }

        private void btnDesactivar_Click(object sender, EventArgs e)
        {
            if (_idSeleccionado <= 0)
            {
                MensajeFmo.Info("Selecciona un vendedor de la lista.", "Vendedores");
                return;
            }

            bool nuevo = !_activoSeleccionado;
            string accion = nuevo ? "activar" : "dar de baja";
            if (!MensajeFmo.Confirmar($"¿Seguro que deseas {accion} a \"{txtNombre.Text}\"?", "Vendedores"))
                return;

            try
            {
                bool ok = VendedorServicio.CambiarEstadoVendedor(
                    new Vendedor { IdVendedor = _idSeleccionado, Estado = nuevo });
                if (ok) MensajeFmo.Exito(nuevo ? "Vendedor activado." : "Vendedor dado de baja.", "Vendedores"); else MensajeFmo.Advertencia("No se pudo cambiar el estado.", "Vendedores");
                if (ok) CargarVendedores();
            }
            catch (Exception ex)
            {
                MensajeFmo.Error(ex.Message, "Vendedores");
            }
        }
    }
}
