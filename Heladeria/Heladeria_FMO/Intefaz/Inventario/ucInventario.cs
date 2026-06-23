using Guna.UI2.WinForms;
using Heladeria_FMO.Acceso_a_datos_db;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Heladeria_FMO.Intefaz.Inventario
{
    public partial class ucInventario : UserControl
    {
        public ucInventario()
        {
            InitializeComponent();
            CargarProductos();
        }
        private void CargarProductos()
        {
            var lista = ProductoDAO.ListarProductos();
            dgvProductos.DataSource = lista;
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            using (var frmAgregarProducto = new FrmAgregarProducto())
            {
                frmAgregarProducto.ShowDialog(this.FindForm());
            }

            CargarProductos();
        }
    }
}
