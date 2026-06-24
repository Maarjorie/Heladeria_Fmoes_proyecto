using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Heladeria_FMO.Acceso_a_datos_db;
using Heladeria_FMO.Modelos;
using Heladeria_FMO.Servicio;
using Heladeria_FMO.Utileria;

namespace Heladeria_FMO.Intefaz.Autorizaciones
{
    // Gestión de usuarios del sistema (master-detail): alta con contraseña,
    // edición de datos/rol y activar / dar de baja (reactivación incluida).
    public class FrmUsuarios : Form
    {
        private readonly Guna2DataGridView dgv = new();
        private readonly Guna2TextBox txtNombre = new();
        private readonly Guna2TextBox txtUsuario = new();
        private readonly Guna2TextBox txtCorreo = new();
        private readonly Guna2TextBox txtContrasena = new();
        private readonly Guna2ComboBox cboRol = new();
        private readonly Guna2Button btnGuardar = new();
        private readonly Guna2Button btnEstado = new();
        private Guna2HtmlLabel lblContrasena;

        private int _idSeleccionado;
        private bool _activoSeleccionado;

        public FrmUsuarios()
        {
            ConstruirUi();
            CargarRoles();
            CargarUsuarios();
        }

        private void ConstruirUi()
        {
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            ShowInTaskbar = false;
            ClientSize = new Size(900, 520);
            BackColor = EstilosFmo.Superficie;

            var marco = new Guna2Panel { Dock = DockStyle.Fill };
            EstilosFmo.Tarjeta(marco);
            Controls.Add(marco);

            var titulo = new Guna2HtmlLabel
            {
                Text = "Usuarios",
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
            dgv.Size = new Size(430, 400);
            dgv.SelectionChanged += dgv_SelectionChanged;
            dgv.CellClick += (s, e) => { if (e.RowIndex >= 0) dgv_SelectionChanged(s, EventArgs.Empty); };

            var btnNuevo = new Guna2Button { Text = "+ Nuevo", Location = new Point(24, 472), Size = new Size(130, 40) };
            EstilosFmo.BotonContorno(btnNuevo);
            btnNuevo.Click += (s, e) => LimpiarFormulario();

            int x = 482;
            var lblNombre = Caption("Nombre", x, 60); Input(txtNombre, x, 82, 388);
            var lblUsuario = Caption("Usuario", x, 126); Input(txtUsuario, x, 148, 185);
            var lblRol = Caption("Rol", x + 203, 126);
            EstilosFmo.Combo(cboRol); cboRol.Location = new Point(x + 203, 148); cboRol.Size = new Size(185, 34);
            var lblCorreo = Caption("Correo", x, 192); Input(txtCorreo, x, 214, 388);
            lblContrasena = Caption("Contraseña", x, 258); Input(txtContrasena, x, 280, 388);
            txtContrasena.PasswordChar = '●';

            btnGuardar.Text = "Guardar";
            EstilosFmo.BotonPrimario(btnGuardar);
            btnGuardar.Location = new Point(x, 360);
            btnGuardar.Size = new Size(185, 44);
            btnGuardar.Click += btnGuardar_Click;

            EstilosFmo.BotonContorno(btnEstado);
            btnEstado.ForeColor = EstilosFmo.Cereza;
            btnEstado.Location = new Point(x + 203, 360);
            btnEstado.Size = new Size(185, 44);
            btnEstado.Text = "Dar de baja";
            btnEstado.Click += btnEstado_Click;

            marco.Controls.AddRange(new Control[]
            {
                titulo, btnCerrar, dgv, btnNuevo,
                lblNombre, txtNombre, lblUsuario, txtUsuario, lblRol, cboRol,
                lblCorreo, txtCorreo, lblContrasena, txtContrasena,
                btnGuardar, btnEstado
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
        private void CargarRoles()
        {
            try
            {
                cboRol.DataSource = RolDAO.ObtenerRoles();
                cboRol.DisplayMember = nameof(Rol.Nombre);
                cboRol.ValueMember = nameof(Rol.Id_Rol);
                cboRol.SelectedIndex = -1;
            }
            catch (Exception)
            {
                MensajeFmo.Advertencia("No se pudieron cargar los roles.", "Usuarios");
            }
        }

        private void CargarUsuarios()
        {
            try
            {
                dgv.DataSource = UsuarioServicio.ListarUsuarios();
                EstilosFmo.MostrarSoloColumnas(dgv,
                    ("Usuario_", "Usuario"),
                    ("Nombre", "Nombre"),
                    ("NombreRol", "Rol"),
                    ("EstadoTexto", "Estado"));
                LimpiarFormulario();
            }
            catch (Exception)
            {
                MensajeFmo.Advertencia("No se pudieron cargar los usuarios.", "Usuarios");
            }
        }

        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv.CurrentRow?.DataBoundItem is not Usuario u) return;

            _idSeleccionado = u.id_Usuario;
            _activoSeleccionado = u.Activo;
            txtNombre.Text = u.Nombre;
            txtUsuario.Text = u.Usuario_;
            txtCorreo.Text = u.Correo;
            cboRol.SelectedValue = u.id_rol;

            // En edición no se cambian usuario ni contraseña desde aquí.
            txtUsuario.Enabled = false;
            txtContrasena.Enabled = false;
            txtContrasena.Clear();
            txtContrasena.PlaceholderText = "(no se cambia al editar)";

            ActualizarBotonEstado();
        }

        private void LimpiarFormulario()
        {
            _idSeleccionado = 0;
            _activoSeleccionado = false;
            txtNombre.Clear();
            txtUsuario.Clear();
            txtCorreo.Clear();
            txtContrasena.Clear();
            cboRol.SelectedIndex = -1;
            txtUsuario.Enabled = true;
            txtContrasena.Enabled = true;
            txtContrasena.PlaceholderText = "";
            ActualizarBotonEstado();
            txtNombre.Focus();
        }

        private void ActualizarBotonEstado()
        {
            if (_idSeleccionado > 0 && !_activoSeleccionado)
            {
                btnEstado.Text = "Activar";
                btnEstado.ForeColor = EstilosFmo.Menta;
            }
            else
            {
                btnEstado.Text = "Dar de baja";
                btnEstado.ForeColor = EstilosFmo.Cereza;
            }
        }

        // ───────────────────────── Acciones ─────────────────────────
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MensajeFmo.Advertencia("Ingresa el nombre.", "Usuarios");
                return;
            }
            if (cboRol.SelectedValue is not int idRol || idRol <= 0)
            {
                MensajeFmo.Advertencia("Selecciona un rol.", "Usuarios");
                return;
            }

            try
            {
                bool ok;
                if (_idSeleccionado > 0)
                {
                    ok = UsuarioServicio.EditarUsuario(_idSeleccionado, txtNombre.Text.Trim(), txtCorreo.Text.Trim(), idRol);
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(txtUsuario.Text))
                    {
                        MensajeFmo.Advertencia("Ingresa el nombre de usuario.", "Usuarios");
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(txtContrasena.Text))
                    {
                        MensajeFmo.Advertencia("Ingresa una contraseña.", "Usuarios");
                        return;
                    }
                    ok = UsuarioServicio.RegistrarUsuario(idRol, txtNombre.Text.Trim(), txtUsuario.Text.Trim(), txtContrasena.Text, txtCorreo.Text.Trim());
                }

                if (ok) MensajeFmo.Exito("Usuario guardado.", "Usuarios"); else MensajeFmo.Advertencia("No se guardó el usuario.", "Usuarios");
                if (ok) CargarUsuarios();
            }
            catch (Exception ex)
            {
                MensajeFmo.Error(ex.Message, "Usuarios");
            }
        }

        private void btnEstado_Click(object sender, EventArgs e)
        {
            if (_idSeleccionado <= 0)
            {
                MensajeFmo.Info("Selecciona un usuario de la lista.", "Usuarios");
                return;
            }
            if (_idSeleccionado == Sesion.UsuarioActivo?.id_Usuario && _activoSeleccionado)
            {
                MensajeFmo.Advertencia("No puedes darte de baja a ti mismo.", "Usuarios");
                return;
            }

            bool nuevo = !_activoSeleccionado;
            string accion = nuevo ? "activar" : "dar de baja";
            if (!MensajeFmo.Confirmar($"¿Seguro que deseas {accion} a \"{txtNombre.Text}\"?", "Usuarios"))
                return;

            try
            {
                bool ok = UsuarioServicio.CambiarEstadoUsuario(_idSeleccionado, nuevo);
                if (ok) MensajeFmo.Exito(nuevo ? "Usuario activado." : "Usuario dado de baja.", "Usuarios"); else MensajeFmo.Advertencia("No se pudo cambiar el estado.", "Usuarios");
                if (ok) CargarUsuarios();
            }
            catch (Exception ex)
            {
                MensajeFmo.Error(ex.Message, "Usuarios");
            }
        }
    }
}
