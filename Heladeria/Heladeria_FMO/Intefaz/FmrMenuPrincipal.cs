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

namespace Heladeria_FMO
{
    public partial class FmrMenuPrincipal : Form
    {
        public FmrMenuPrincipal()
        {
            InitializeComponent();
            AplicarDiseno();

        }

        private void AplicarDiseno()
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


    }
}
