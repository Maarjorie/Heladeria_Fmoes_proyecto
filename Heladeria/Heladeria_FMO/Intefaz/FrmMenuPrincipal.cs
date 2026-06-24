using Guna.UI2.WinForms;
using Heladeria_FMO.Intefaz.Autorizaciones;
using Heladeria_FMO.Intefaz.Caja;
using Heladeria_FMO.Intefaz.Inventario;
using Heladeria_FMO.Intefaz.Mayorista;
using Heladeria_FMO.Intefaz.PuntoVenta;
using Heladeria_FMO.Intefaz.Reportes;
using Heladeria_FMO.Intefaz.Vendedores;
using Heladeria_FMO.Intefaz.ucMenuInicio;
using Heladeria_FMO.Servicio;
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
            CrearBotonReportes();
            HabilitarDoubleBuffer(pnlContenedor);
            AplicarTemaShell();

            // Vista inicial: el dashboard.
            CargarVista(new ucInicio());
            MarcarNavActivo(btnInicio);

            IniciarNotificacionesAutomaticas();
        }

        private System.Windows.Forms.Timer _timerNotificaciones;

        // Arranca el sistema de notificaciones automáticas: corre una vez al entrar
        // y luego periódicamente. El propio sistema detecta los eventos (stock bajo,
        // productos por vencer) y envía las notificaciones pendientes por correo.
        private void IniciarNotificacionesAutomaticas()
        {
            _timerNotificaciones = new System.Windows.Forms.Timer { Interval = 5 * 60 * 1000 };
            _timerNotificaciones.Tick += (s, e) => _ = NotificacionServicio.ProcesarAsync();
            _timerNotificaciones.Start();

            // Corrida inicial (no bloquea la UI).
            _ = NotificacionServicio.ProcesarAsync();
        }

        // Color base del sidebar/topbar (un poco más claro que el contenido para
        // separar el chrome de la zona de trabajo).
        private static readonly Color ColorSidebar = EstilosFmo.Superficie;

        private Guna2Button btnReportes;

        private Guna2Button[] BotonesNav()
        {
            var lista = new List<Guna2Button>
            {
                btnInicio, btnVenta, btnInventario, btnMayorista, btnCaja, btnVendedores, btnAutorizacion
            };
            if (btnReportes != null) lista.Add(btnReportes);
            return lista.ToArray();
        }

        // Agrega el botón "Reportes" al sidebar (solo administrador).
        private void CrearBotonReportes()
        {
            btnReportes = new Guna2Button
            {
                Text = "Reportes",
                Size = new Size(200, 46),
                TextAlign = HorizontalAlignment.Left,
                TextOffset = new Point(10, 0),
                AutoRoundedCorners = true,
                Visible = Sesion.UsuarioActivo?.id_rol == 1
            };
            btnReportes.Click += (s, e) => CargarVista(new ucReportes());
            flowLayoutPanel1.Controls.Add(btnReportes);
        }

        // Aplica el tema oscuro al "shell" (sidebar + topbar + contenedor) para que
        // combine con las pantallas nuevas, sin rehacer el diseñador.
        private void AplicarTemaShell()
        {
            BackColor = EstilosFmo.Fondo;

            foreach (var p in new[] { guna2Panel1, guna2Panel2, guna2Panel3, guna2Panel4, guna2Panel5 })
                p.FillColor = ColorSidebar;
            flowLayoutPanel1.BackColor = ColorSidebar;
            pnlContenedor.FillColor = EstilosFmo.Fondo;

            // Topbar.
            guna2HtmlLabel3.ForeColor = EstilosFmo.TextoFuerte;
            guna2HtmlLabel3.Font = EstilosFmo.Fuente(16F, FontStyle.Bold);
            guna2HtmlLabel4.ForeColor = EstilosFmo.TextoTenue;

            // Etiquetas de sección del sidebar.
            foreach (var lbl in new[] { guna2HtmlLabel5, guna2HtmlLabel6 })
            {
                lbl.ForeColor = EstilosFmo.TextoTenue;
                lbl.Font = EstilosFmo.Fuente(10F, FontStyle.Bold);
            }

            // Pie de usuario.
            guna2HtmlLabel1.ForeColor = EstilosFmo.TextoFuerte;
            guna2HtmlLabel2.ForeColor = EstilosFmo.TextoTenue;
            btnSalir.FillColor = Color.Transparent;
            btnSalir.ForeColor = EstilosFmo.TextoTenue;

            // Botones de navegación: transparentes sobre el sidebar; al pasar el
            // mouse se oscurecen y el activo se pinta de fresa.
            foreach (var b in BotonesNav())
            {
                b.FillColor = ColorSidebar;
                b.ForeColor = EstilosFmo.TextoCuerpo;
                b.Font = EstilosFmo.Fuente(11F);
                b.HoverState.FillColor = EstilosFmo.SuperficieHundida;
                b.Click += (s, e) => MarcarNavActivo((Guna2Button)s);
            }
        }

        // Resalta el botón de navegación activo y restablece los demás.
        private void MarcarNavActivo(Guna2Button activo)
        {
            foreach (var b in BotonesNav())
            {
                bool on = b == activo;
                b.FillColor = on ? EstilosFmo.Fresa : ColorSidebar;
                b.ForeColor = on ? Color.White : EstilosFmo.TextoCuerpo;
                b.HoverState.FillColor = on ? EstilosFmo.FresaHover : EstilosFmo.SuperficieHundida;
            }
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
            btnAutorizacion.Visible = esSupervisor || esAdmin;
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
            guna2HtmlLabel3.Text = TituloDeVista(vista);
            guna2HtmlLabel4.Text = string.Empty;
        }

        // Título legible para el topbar según el UserControl cargado.
        private static string TituloDeVista(Control vista) => vista switch
        {
            ucInicio => "Inicio",
            ucPuntoVenta => "Punto de venta",
            ucInventario => "Inventario",
            ucCaja => "Caja",
            ucMayorista => "Mayorista",
            ucVendedores => "Vendedores ambulantes",
            ucAutorizaciones => "Autorizaciones",
            ucReportes => "Reportes",
            _ => vista.Name
        };

        private void btnInicio_Click(object sender, EventArgs e)
        {
            CargarVista(new ucInicio());   
        }

        private void btnVenta_Click(object sender, EventArgs e)
        {
            CargarVista(new ucPuntoVenta());
        }
        private void btnInventario_Click(object sender, EventArgs e)
        {
            CargarVista(new ucInventario());
        }
        private void btnMayorista_Click(object sender, EventArgs e)
        {
            CargarVista(new ucMayorista());
        }
        private void btnCaja_Click(object sender, EventArgs e)
        {
            CargarVista(new ucCaja());
        }
        private void btnVendedores_Click(object sender, EventArgs e)
        {
            CargarVista(new ucVendedores());
        }
        private void btnAutorizacion_Click(object sender, EventArgs e)
        {
            CargarVista(new ucAutorizaciones());
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            var confirm = (MensajeFmo.Confirmar("¿Seguro que deseas cerrar sesión?",
                "Cerrar sesión") ? DialogResult.Yes : DialogResult.No);

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
