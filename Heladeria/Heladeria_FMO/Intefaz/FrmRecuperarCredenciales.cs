using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Heladeria_FMO.Servicio;
using Heladeria_FMO.Utileria;

namespace Heladeria_FMO.Intefaz
{
    // Dialogo de recuperación de credenciales: el usuario ingresa el correo
    // registrado y, si coincide con una cuenta activa, recibe una contraseña
    // temporal por correo (ver UsuarioServicio.RecuperarContrasena).
    public class FrmRecuperarCredenciales : Form
    {
        private Guna2TextBox txtCorreo;
        private Guna2Button btnEnviar;
        private Guna2Button btnCancelar;

        public FrmRecuperarCredenciales()
        {
            ConstruirInterfaz();
        }

        private void ConstruirInterfaz()
        {
            Text = "Recuperar credenciales";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(420, 260);
            BackColor = EstilosFmo.Fondo;

            var tarjeta = new Guna2Panel
            {
                Size = new Size(380, 220),
                Location = new Point(20, 20)
            };
            EstilosFmo.Tarjeta(tarjeta);

            var titulo = new Guna2HtmlLabel
            {
                Text = "¿Olvidaste tus credenciales?",
                Location = new Point(20, 18),
                Size = new Size(340, 26),
                Font = EstilosFmo.Fuente(13F, FontStyle.Bold),
                ForeColor = EstilosFmo.TextoFuerte,
                BackColor = Color.Transparent
            };

            var instrucciones = new Guna2HtmlLabel
            {
                Text = "Ingresa el correo registrado en tu cuenta. Te enviaremos\nuna contraseña temporal para que vuelvas a entrar.",
                Location = new Point(20, 48),
                Size = new Size(340, 40),
                Font = EstilosFmo.Fuente(9F),
                ForeColor = EstilosFmo.TextoTenue,
                BackColor = Color.Transparent
            };

            txtCorreo = new Guna2TextBox
            {
                PlaceholderText = "correo@ejemplo.com",
                Location = new Point(20, 96),
                Size = new Size(340, 40)
            };
            EstilosFmo.CajaTexto(txtCorreo);

            btnEnviar = new Guna2Button
            {
                Text = "Enviar contraseña temporal",
                Location = new Point(20, 150),
                Size = new Size(340, 42)
            };
            EstilosFmo.BotonPrimario(btnEnviar);
            btnEnviar.Click += BtnEnviar_Click;

            btnCancelar = new Guna2Button
            {
                Text = "Cancelar",
                Location = new Point(20, 198),
                Size = new Size(340, 36)
            };
            EstilosFmo.BotonContorno(btnCancelar);
            btnCancelar.Click += (s, e) => Close();

            tarjeta.Controls.Add(titulo);
            tarjeta.Controls.Add(instrucciones);
            tarjeta.Controls.Add(txtCorreo);
            tarjeta.Controls.Add(btnEnviar);
            tarjeta.Controls.Add(btnCancelar);
            Controls.Add(tarjeta);
        }

        private async void BtnEnviar_Click(object sender, EventArgs e)
        {
            string correo = txtCorreo.Text.Trim();
            if (string.IsNullOrWhiteSpace(correo))
            {
                MensajeFmo.Advertencia("Ingresa el correo asociado a tu cuenta.", "Dato requerido");
                return;
            }

            btnEnviar.Enabled = false;
            btnEnviar.Text = "Enviando...";

            try
            {
                await UsuarioServicio.RecuperarContrasena(correo);

                // El mensaje es el mismo exista o no el correo, para no revelar
                // qué cuentas están registradas en el sistema.
                MensajeFmo.Exito(
                    "Si el correo está registrado, recibirás una contraseña temporal en los próximos minutos.",
                    "Solicitud enviada");
                Close();
            }
            catch (Exception)
            {
                MensajeFmo.Error("No se pudo procesar la solicitud. Intenta nuevamente.", "Error");
            }
            finally
            {
                btnEnviar.Enabled = true;
                btnEnviar.Text = "Enviar contraseña temporal";
            }
        }
    }
}
