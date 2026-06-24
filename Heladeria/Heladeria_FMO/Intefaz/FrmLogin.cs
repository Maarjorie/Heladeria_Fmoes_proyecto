using System.Drawing;
using Heladeria_FMO.Modelos;
using Heladeria_FMO.Servicio;
using Heladeria_FMO.Utileria;

namespace Heladeria_FMO
{
    public partial class FrmLogin : Form
    {
        // Neutros cálidos del sistema de diseño (para el formulario claro).
        private static readonly Color Neutral200 = Color.FromArgb(236, 226, 218);
        private static readonly Color Neutral300 = Color.FromArgb(219, 206, 196);
        // Texto OSCURO (el panel del formulario es claro, por eso no se usa la
        // paleta de texto clara de EstilosFmo).
        private static readonly Color TextoOscuro = Color.FromArgb(36, 27, 25);
        private static readonly Color TextoOscuroSuave = Color.FromArgb(110, 89, 81);

        public FrmLogin()
        {
            InitializeComponent();
            AplicarTemaLogin();
        }

        // Reskin a la paleta del diseño: hero izquierdo con degradado fresa y el
        // formulario a la derecha sobre superficie clara. Reutiliza los controles
        // GUNA ya existentes (solo cambia colores).
        private void AplicarTemaLogin()
        {
            // Hero izquierdo (degradado fresa) con el logo monocromo.
            PnlFondo.BackColor = EstilosFmo.Fresa;
            PnlFondo.FillColor = EstilosFmo.Fresa;
            PnlFondo.FillColor2 = EstilosFmo.FresaHover;

            // Panel derecho: claro, tipo tarjeta.
            GpnlContenedor.BackColor = Color.White;
            GpnlContenedor.FillColor = Color.White;
            GpnlContenedor.FillColor2 = Color.White;
            pnlLogin.BackColor = Color.White;
            pnlLogin.FillColor = Color.White;

            // Textos del formulario en tonos oscuros (sobre fondo claro).
            lblTitulo.ForeColor = TextoOscuro;
            lblSubtitulo.ForeColor = TextoOscuroSuave;
            lblUsuario.ForeColor = TextoOscuro;
            lblContraseña.ForeColor = TextoOscuro;

            // Cajas de texto claras con texto oscuro y foco fresa.
            foreach (var t in new[] { txtUsuario, txtContraseña })
            {
                t.FillColor = Color.White;
                t.BorderColor = Neutral300;
                t.ForeColor = TextoOscuro;
                t.PlaceholderForeColor = TextoOscuroSuave;
                t.FocusedState.BorderColor = EstilosFmo.Fresa;
                t.HoverState.BorderColor = EstilosFmo.Fresa;
            }

            // Botón principal en fresa.
            btnEntrar.FillColor = EstilosFmo.Fresa;
            btnEntrar.ForeColor = Color.White;
            btnEntrar.HoverState.FillColor = EstilosFmo.FresaHover;

            // Botón salir neutro para no competir con "Entrar".
            btnSalir.FillColor = TextoOscuroSuave;
            btnSalir.ForeColor = EstilosFmo.TextoCuerpo;
            btnSalir.HoverState.FillColor = Neutral300;

            // Toggle de contraseña y enlace.
            btnMostrar.FillColor = Color.FromArgb(255, 224, 233); // fresa-100
            LlblRecuperarCredenciales.LinkColor = EstilosFmo.Arandano;
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text) || string.IsNullOrWhiteSpace(txtContraseña.Text))
            {
                MensajeFmo.Advertencia("Ingresa tu usuario y contraseña.", "Datos incompletos");
                return;
            }

            Acceso resultado = UsuarioServicio.Login(txtUsuario.Text, txtContraseña.Text, out Usuario usuarioActivo);

            switch (resultado)
            {
                case Acceso.SesionExitosa:
                    Sesion.UsuarioActivo = usuarioActivo;
                    this.Hide();
                    new FrmMenuPrincipal().Show();
                    break;

                case Acceso.ErrorCredenciales:
                    MensajeFmo.Error("Usuario o contraseña incorrectos.", "Acceso denegado");
                    break;

                case Acceso.UsuarioInactivo:
                    MensajeFmo.Advertencia("Tu cuenta esta desactivada. Contacta al administrador.", "Cuenta inactiva");
                    break;

                case Acceso.ErrorConexionDb:
                    MensajeFmo.Error("No se pudo conectar con la base de datos. Intenta nuevamente.", "Error de conexión");
                    break;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMostrar_Click(object sender, EventArgs e)
        {
            txtContraseña.PasswordChar = txtContraseña.PasswordChar == '*' ? '\0' : '*';
        }




        /*
private void AplicarDiseno()
{
Estilos.Formulario(this);

Estilos.Titulo(lblTitulo);
Estilos.Subtitulo(lblSubtitulo);

Estilos.PanelElegante(pnlContenedor);

Estilos.CajaTexto(txtUsuario);
Estilos.CajaTexto(txtContraseÃ±a);

Estilos.BotonDorado(btnEntrar);
Estilos.BotonOscuro(btnSalir);
}

private void panelLogin_Paint(object sender, PaintEventArgs e)
{
Color dorado = Color.FromArgb(212, 175, 55);

using (Pen lapiz = new Pen(dorado, 2))
{
Rectangle rectangulo = pnlLogin.ClientRectangle;
rectangulo.Width -= 1;
rectangulo.Height -= 1;

e.Graphics.DrawRectangle(lapiz, rectangulo);
}
}*/
    }
}
