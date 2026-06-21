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
