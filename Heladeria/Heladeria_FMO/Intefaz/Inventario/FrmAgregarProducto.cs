using System.Windows.Forms;
using Heladeria_FMO.Modelos;

namespace Heladeria_FMO.Intefaz.Inventario
{
    // Formulario "flotante" sin bordes que aloja el UserControl ucAgregarProducto,
    // para poder mostrarlo centrado en pantalla como una ventana independiente.
    // Si se pasa un producto, el formulario se abre en modo edición.
    public class FrmAgregarProducto : Form
    {
        public FrmAgregarProducto(Producto productoEditar = null)
        {
            var ucAgregarProducto = new ucAgregarProducto { Dock = DockStyle.Fill };

            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            ShowInTaskbar = false;
            ClientSize = ucAgregarProducto.Size;

            Controls.Add(ucAgregarProducto);

            if (productoEditar != null)
                ucAgregarProducto.CargarParaEditar(productoEditar);
        }

        private void InitializeComponent()
        {

        }
    }
}
