using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Heladeria_FMO.Servicio;
using Heladeria_FMO.Utileria;

namespace Heladeria_FMO.Intefaz
{
    // Se muestra cuando el usuario inició sesión con la contraseña temporal
    // de recuperación: lo obliga a fijar una contraseña nueva permanente
    // antes de entrar al sistema.
    public class FrmEstablecerContrasena : Form
    {
        private readonly int _idUsuario;
        private Guna2TextBox txtNueva;
        private Guna2TextBox txtConfirmar;
        private Guna2Button btnGuardar;

        public FrmEstablecerContrasena(int idUsuario)
        {
            _idUsuario = idUsuario;
            ConstruirInterfaz();
        }

        private void ConstruirInterfaz()
        {
            Text = "Crear nueva contraseña";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ControlBox = false; // no puede cerrarlo sin fijar la contraseña
            ClientSize = new Size(420, 280);
            BackColor = EstilosFmo.Fondo;

            var tarjeta = new Guna2Panel
            {
                Size = new Size(380, 240),
                Location = new Point(20, 20)
            };
            EstilosFmo.Tarjeta(tarjeta);

            var titulo = new Guna2HtmlLabel
            {
                Text = "Crea tu nueva contraseña",
                Location = new Point(20, 18),
                Size = new Size(340, 26),
                Font = EstilosFmo.Fuente(13F, FontStyle.Bold),
                ForeColor = EstilosFmo.TextoFuerte,
                BackColor = Color.Transparent
            };

            var instrucciones = new Guna2HtmlLabel
            {
                Text = "Ingresaste con una contraseña temporal. Define una\ncontraseña nueva para tu cuenta.",
                Location = new Point(20, 48),
                Size = new Size(340, 40),
                Font = EstilosFmo.Fuente(9F),
                ForeColor = EstilosFmo.TextoTenue,
                BackColor = Color.Transparent
            };

            txtNueva = new Guna2TextBox
            {
                PlaceholderText = "Nueva contraseña",
                PasswordChar = '*',
                Location = new Point(20, 96),
                Size = new Size(340, 40)
            };
            EstilosFmo.CajaTexto(txtNueva);

            txtConfirmar = new Guna2TextBox
            {
                PlaceholderText = "Confirmar contraseña",
                PasswordChar = '*',
                Location = new Point(20, 146),
                Size = new Size(340, 40)
            };
            EstilosFmo.CajaTexto(txtConfirmar);

            btnGuardar = new Guna2Button
            {
                Text = "Guardar y continuar",
                Location = new Point(20, 200),
                Size = new Size(340, 42)
            };
            EstilosFmo.BotonPrimario(btnGuardar);
            btnGuardar.Click += BtnGuardar_Click;

            tarjeta.Controls.Add(titulo);
            tarjeta.Controls.Add(instrucciones);
            tarjeta.Controls.Add(txtNueva);
            tarjeta.Controls.Add(txtConfirmar);
            tarjeta.Controls.Add(btnGuardar);
            Controls.Add(tarjeta);
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            string nueva = txtNueva.Text;
            string confirmar = txtConfirmar.Text;

            if (string.IsNullOrWhiteSpace(nueva) || string.IsNullOrWhiteSpace(confirmar))
            {
                MensajeFmo.Advertencia("Completa ambos campos.", "Datos incompletos");
                return;
            }

            if (nueva.Length < 6)
            {
                MensajeFmo.Advertencia("La contraseña debe tener al menos 6 caracteres.", "Contraseña débil");
                return;
            }

            if (nueva != confirmar)
            {
                MensajeFmo.Advertencia("Las contraseñas no coinciden.", "Verifica");
                return;
            }

            bool ok = UsuarioServicio.CambiarContrasena(_idUsuario, nueva);
            if (!ok)
            {
                MensajeFmo.Error("No se pudo guardar la contraseña. Intenta nuevamente.", "Error");
                return;
            }

            MensajeFmo.Exito("Tu contraseña fue actualizada.", "Listo");
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
