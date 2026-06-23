using Guna.UI2.WinForms;
using Heladeria_FMO.Intefaz.Inventario;
using Heladeria_FMO.Intefaz.ucMenuInicio;
using Heladeria_FMO.Servicio;
using Heladeria_FMO.Utileria;
using Heladeria_FMO.Utileria;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Heladeria_FMO
{
    public partial class FrmMenuPrincipal : Form
    {
        public FrmMenuPrincipal()
        {
            InitializeComponent();
            MostrarDatosUsuario();
            AplicarPermisosPorRol();
            HabilitarDoubleBuffer(pnlContenedor);

            // AplicarDiseno();
        }

        // El panel contenedor no tiene doble buffer por defecto, lo que provoca
        // que al cargar un UserControl con muchos controles Guna se vea el dibujado
        // control por control en vez de aparecer de una sola vez.
        private static void HabilitarDoubleBuffer(Control control)
        {
            typeof(Control).InvokeMember(
                "DoubleBuffered",
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null,
                control,
                new object[] { true });
        }

        // Muestra el nombre y rol del usuario logueado en el panel inferior del sidebar
        private void MostrarDatosUsuario()
        {
            if (Sesion.UsuarioActivo == null) return;

            guna2HtmlLabel1.Text = Sesion.UsuarioActivo.Nombre;
            guna2HtmlLabel2.Text = Sesion.UsuarioActivo.NombreRol ?? "Rol " + Sesion.UsuarioActivo.id_rol;
        }

        // Oculta los botones del sidebar que no le corresponden al rol activo
        private void AplicarPermisosPorRol()
        {
            if (Sesion.UsuarioActivo == null) return;

            int rol = Sesion.UsuarioActivo.id_rol;

            bool esAdmin = rol == 1;
            bool esSupervisor = rol == 2;
            bool esCajero = rol == 3;
            bool esVendedor = rol == 4;

            // Inicio — todos los roles lo ven
            btnInicio.Visible = true;

            // Punto de venta — Cajero y Admin
            btnVenta.Visible = esCajero || esAdmin;

            // Inventario — Admin
            btnInventario.Visible = esAdmin;

            // Mayorista — Cajero (entregas) y Admin
            btnMayorista.Visible = esCajero || esAdmin;

            // Caja — Cajero y Admin
            btnCaja.Visible = esCajero || esAdmin;

            // Vendedores — Admin y Supervisor
            btnVendedores.Visible = esAdmin || esSupervisor;

            // Autorización — Supervisor y Admin
            btnVendedores.Visible = esSupervisor || esAdmin;
        }

        // Carga un UserControl en el panel de contenido
        private void CargarVista(Control vista)
        {
            pnlContenedor.SuspendLayout();
            pnlContenedor.Visible = false;

            pnlContenedor.Controls.Clear();
            vista.Dock = DockStyle.Fill;
            pnlContenedor.Controls.Add(vista);

            pnlContenedor.ResumeLayout();
            pnlContenedor.Visible = true;

            // Actualiza el titulo y descripcion del header
            guna2HtmlLabel3.Text = vista.Name;
            guna2HtmlLabel4.Text = string.Empty;
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            CargarVista(new ucInicio());   
        }

        private void btnVenta_Click(object sender, EventArgs e)
        {

        }
        private void btnInventario_Click(object sender, EventArgs e)
        {
            CargarVista(new ucInventario());
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
