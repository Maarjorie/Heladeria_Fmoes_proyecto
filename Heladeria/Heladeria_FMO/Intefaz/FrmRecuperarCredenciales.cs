using System;
using System.Drawing;
using System.Windows.Forms;
using Heladeria_FMO.Servicio;
using Heladeria_FMO.Utileria;

namespace Heladeria_FMO.Intefaz
{
    // Dialogo de recuperación de credenciales: el usuario ingresa el correo
    // registrado y, si coincide con una cuenta activa, recibe una contraseña
    // temporal por correo (ver UsuarioServicio.RecuperarContrasena).
    public partial class FrmRecuperarCredenciales : Form
    {
        public FrmRecuperarCredenciales()
        {
            InitializeComponent();
            AplicarTema();
        }

        // El Diseñador maneja el layout; aquí se aplica el tema oscuro de la app.
        private void AplicarTema()
        {
            BackColor = EstilosFmo.Fondo;
            EstilosFmo.Tarjeta(tarjeta);

            titulo.Font = EstilosFmo.Fuente(13F, FontStyle.Bold);
            titulo.ForeColor = EstilosFmo.TextoFuerte;

            instrucciones.Font = EstilosFmo.Fuente(9F);
            instrucciones.ForeColor = EstilosFmo.TextoTenue;

            EstilosFmo.CajaTexto(txtCorreo);
            EstilosFmo.BotonPrimario(btnEnviar);
            EstilosFmo.BotonContorno(btnCancelar);
        }

        private void BtnCancelar_Click(object sender, EventArgs e) => Close();

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
