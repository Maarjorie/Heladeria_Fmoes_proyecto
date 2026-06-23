using System.Windows.Forms;

namespace Heladeria_FMO.Intefaz.Inventario
{
    // Formulario "flotante" sin bordes que aloja el UserControl ucAgregarProducto,
    // para poder mostrarlo centrado en pantalla como una ventana independiente.
    public class FrmAgregarProducto : Form
    {
        public FrmAgregarProducto()
        {
            var ucAgregarProducto = new ucAgregarProducto { Dock = DockStyle.Fill };

            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            ShowInTaskbar = false;
            ClientSize = ucAgregarProducto.Size;

            Controls.Add(ucAgregarProducto);
        }

        private void InitializeComponent()
        {

        }
    }
}
