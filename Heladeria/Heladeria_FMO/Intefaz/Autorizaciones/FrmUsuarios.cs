using System;
using System.Drawing;
using System.Windows.Forms;
using Heladeria_FMO.Acceso_a_datos_db;
using Heladeria_FMO.Modelos;
using Heladeria_FMO.Servicio;
using Heladeria_FMO.Utileria;

namespace Heladeria_FMO.Intefaz.Autorizaciones
{
    // Gestión de usuarios del sistema (master-detail): alta con contraseña,
    // edición de datos/rol y activar / dar de baja (reactivación incluida).
    public partial class FrmUsuarios : Form
    {
        private int _idSeleccionado;
        private bool _activoSeleccionado;

        public FrmUsuarios()
        {
            InitializeComponent();
            AplicarTema();
            CargarRoles();
            CargarUsuarios();
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

            foreach (var lbl in new[] { lblNombre, lblUsuario, lblRol, lblCorreo, lblContrasena })
            {
                lbl.Font = EstilosFmo.Fuente(9.5F);
                lbl.ForeColor = EstilosFmo.TextoTenue;
            }

            EstilosFmo.CajaTexto(txtNombre);
            EstilosFmo.CajaTexto(txtUsuario);
            EstilosFmo.CajaTexto(txtCorreo);
            EstilosFmo.CajaTexto(txtContrasena);
            EstilosFmo.Combo(cboRol);

            EstilosFmo.BotonPrimario(btnGuardar);
            EstilosFmo.BotonContorno(btnEstado);
            btnEstado.ForeColor = EstilosFmo.Cereza;
        }

        private void BtnCerrar_Click(object sender, EventArgs e) => Close();

        private void BtnNuevo_Click(object sender, EventArgs e) => LimpiarFormulario();

        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) dgv_SelectionChanged(sender, EventArgs.Empty);
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
