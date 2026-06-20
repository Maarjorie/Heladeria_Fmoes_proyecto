using Heladeria_FMO.Utileria;

namespace Heladeria_FMO
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AplicarDiseno();
        }

        private void AplicarDiseno()
        {
            Estilos.Formulario(this);

            Estilos.Titulo(lblTitulo);
            Estilos.Subtitulo(lblSubtitulo);

            Estilos.PanelElegante(panelLogin);

            Estilos.CajaTexto(txtUsuario);
            Estilos.CajaTexto(txtContrasena);

            Estilos.BotonDorado(btnIniciarSesion);
            Estilos.BotonOscuro(btnSalir);
        }

        private void panelLogin_Paint(object sender, PaintEventArgs e)
        {
            Color dorado = Color.FromArgb(212, 175, 55);

            using (Pen lapiz = new Pen(dorado, 2))
            {
                Rectangle rectangulo = panelLogin.ClientRectangle;
                rectangulo.Width -= 1;
                rectangulo.Height -= 1;

                e.Graphics.DrawRectangle(lapiz, rectangulo);
            }
        }
    }
}
