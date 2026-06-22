using Heladeria_FMO.Utileria;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Heladeria_FMO.Utileria;
using Heladeria_FMO.Servicio;

namespace Heladeria_FMO
{
    public partial class FrmMenuPrincipal : Form
    {
        public FrmMenuPrincipal()
        {
            InitializeComponent();
            // AplicarDiseno();
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {

        }

        private void btnVenta_Click(object sender, EventArgs e)
        {

        }
        private void btnInventario_Click(object sender, EventArgs e)
        {

        }
        private void btnMayorista_Click(object sender, EventArgs e)
        {

        }
        private void btnCaja_Click(object sender, EventArgs e)
        {

        }
        private void btnVendedores_Click(object sender, EventArgs e)
        {

        }
        private void btnAutorizacion_Click(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "¿Seguro que deseas cerrar sesión?",
                "Cerrar sesión",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                Sesion.UsuarioActivo = null;
                new FrmLogin().Show();
                this.Close();
            }

        }

        /* private void AplicarDiseno()
         {
             Estilos.Formulario(this);

             Estilos.PanelMenu(panelMenu);
             Estilos.PanelSuperior(panelSuperior);
             Estilos.PanelContenedor(panelContenedor);

             Estilos.TituloMenu(lblTitulo);

             Estilos.BotonMenu(btnProductos);
             Estilos.BotonMenu(btnVentas);
             Estilos.BotonMenu(btnClientes);
             Estilos.BotonMenu(btnPedidos);
             Estilos.BotonMenu(btnRutas);
             Estilos.BotonMenu(btnCaja);
             Estilos.BotonMenu(btnReportes);
             Estilos.BotonMenu(btnCerrarSesion);

             Estilos.EfectoBotonMenu(btnProductos);
             Estilos.EfectoBotonMenu(btnVentas);
             Estilos.EfectoBotonMenu(btnClientes);
             Estilos.EfectoBotonMenu(btnPedidos);
             Estilos.EfectoBotonMenu(btnRutas);
             Estilos.EfectoBotonMenu(btnCaja);
             Estilos.EfectoBotonMenu(btnReportes);
             Estilos.EfectoBotonMenu(btnCerrarSesion);
         }
        */

    }
}
