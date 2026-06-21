using Heladeria_FMO.Modelos;
using Heladeria_FMO.Servicio;
using Heladeria_FMO.Utileria;

namespace Heladeria_FMO
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
            //AplicarDiseno();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text) || string.IsNullOrWhiteSpace(txtContraseña.Text))
            {
                MessageBox.Show("Ingresa tu usuario y contraseña.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Acceso resultado = UsuarioServicio.Login(txtUsuario.Text, txtContraseña.Text, out Usuario usuarioActivo);

            switch (resultado)
            {
                case Acceso.SesionExitosa:
                    Sesion.UsuarioActivo = usuarioActivo;
                    MessageBox.Show($"Bienvenido, {usuarioActivo.Nombre}.", "Acceso correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;

                case Acceso.ErrorCredenciales:
                    MessageBox.Show("Usuario o contraseña incorrectos.", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case Acceso.UsuarioInactivo:
                    MessageBox.Show("Tu cuenta está desactivada. Contacta al administrador.", "Cuenta inactiva", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case Acceso.ErrorConexionDb:
                    MessageBox.Show("No se pudo conectar con la base de datos. Intenta nuevamente.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }




        /*
private void AplicarDiseno()
{
Estilos.Formulario(this);

Estilos.Titulo(lblTitulo);
Estilos.Subtitulo(lblSubtitulo);

Estilos.PanelElegante(pnlContenedor);

Estilos.CajaTexto(txtUsuario);
Estilos.CajaTexto(txtContraseña);

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
=======
using Heladeria_FMO.Utileria;

namespace Heladeria_FMO
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
            //AplicarDiseno();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            
        }


        /*
private void AplicarDiseno()
{
Estilos.Formulario(this);

Estilos.Titulo(lblTitulo);
Estilos.Subtitulo(lblSubtitulo);

Estilos.PanelElegante(pnlContenedor);

Estilos.CajaTexto(txtUsuario);
Estilos.CajaTexto(txtContraseña);

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
>>>>>>> ab54ff87b97c77c76c8fb775facf67563c44696c
