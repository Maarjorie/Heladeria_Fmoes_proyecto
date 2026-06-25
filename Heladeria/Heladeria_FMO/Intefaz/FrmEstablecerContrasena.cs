using System;
using System.Drawing;
using System.Windows.Forms;
using Heladeria_FMO.Servicio;
using Heladeria_FMO.Utileria;

namespace Heladeria_FMO.Intefaz
{
    // Se muestra cuando el usuario inició sesión con la contraseña temporal
    // de recuperación: lo obliga a fijar una contraseña nueva permanente
    // antes de entrar al sistema.
    public partial class FrmEstablecerContrasena : Form
    {
        private readonly int _idUsuario;

        public FrmEstablecerContrasena(int idUsuario)
        {
            _idUsuario = idUsuario;
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

            EstilosFmo.CajaTexto(txtNueva);
            EstilosFmo.CajaTexto(txtConfirmar);
            EstilosFmo.BotonPrimario(btnGuardar);
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
