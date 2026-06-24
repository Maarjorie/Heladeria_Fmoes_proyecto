using Guna.UI2.WinForms;
using Heladeria_FMO.Acceso_a_datos_db;
using Heladeria_FMO.Modelos;
using Heladeria_FMO.Utileria;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Heladeria_FMO.Intefaz.Inventario
{
    public partial class ucInventario : UserControl
    {
        private Guna2Button btnEditar;
        private Guna2Button btnDesactivar;
        private Guna2Button btnPromos;

        // Catálogo completo en memoria, para filtrar la búsqueda sin recargar.
        private List<Producto> _todos = new();

        public ucInventario()
        {
            InitializeComponent();
            AplicarTema();
            CrearBotonesCrud();
            guna2TextBox1.TextChanged += (s, e) => AplicarFiltro();
            dgvProductos.SelectionChanged += (s, e) => ActualizarBotonEstado();
            CargarProductos();
        }

        private void AplicarTema()
        {
            BackColor = EstilosFmo.Fondo;
            guna2Panel1.FillColor = EstilosFmo.Fondo;
            EstilosFmo.CajaTexto(guna2TextBox1);
            EstilosFmo.BotonPrimario(btnAgregarProducto);
            EstilosFmo.Tabla(dgvProductos);
        }

        private void CrearBotonesCrud()
        {
            btnEditar = new Guna2Button
            {
                Text = "Editar",
                Size = new Size(125, 56),
                Location = new Point(575, 27)
            };
            EstilosFmo.BotonContorno(btnEditar);
            btnEditar.Click += btnEditar_Click;

            btnDesactivar = new Guna2Button
            {
                Text = "Dar de baja",
                Size = new Size(150, 56),
                Location = new Point(712, 27)
            };
            EstilosFmo.BotonContorno(btnDesactivar);
            btnDesactivar.ForeColor = EstilosFmo.Cereza;
            btnDesactivar.Click += btnDesactivar_Click;

            btnPromos = new Guna2Button
            {
                Text = "Promociones",
                Size = new Size(150, 56),
                Location = new Point(877, 27)
            };
            EstilosFmo.BotonContorno(btnPromos);
            btnPromos.Click += (s, e) =>
            {
                using var d = new FrmPromociones();
                d.ShowDialog(this.FindForm());
            };

            guna2Panel1.Controls.Add(btnEditar);
            guna2Panel1.Controls.Add(btnDesactivar);
            guna2Panel1.Controls.Add(btnPromos);
        }

        private void CargarProductos()
        {
            try
            {
                _todos = ProductoDAO.ListarProductos();
            }
            catch (Exception)
            {
                _todos = new List<Producto>();
                MensajeFmo.Advertencia("No se pudieron cargar los productos. Verifica la conexión con la base de datos.",
                    "Inventario");
            }
            AplicarFiltro();
        }

        private void AplicarFiltro()
        {
            string filtro = guna2TextBox1.Text.Trim();
            var lista = filtro.Length == 0
                ? _todos
                : _todos.Where(p =>
                    (p.Nombre ?? "").Contains(filtro, StringComparison.OrdinalIgnoreCase) ||
                    (p.Codigo ?? "").Contains(filtro, StringComparison.OrdinalIgnoreCase)).ToList();

            dgvProductos.DataSource = lista;
            EstilosFmo.MostrarSoloColumnas(dgvProductos,
                ("Codigo", "Código"),
                ("Nombre", "Producto"),
                ("NombreCategoria", "Categoría"),
                ("PrecioVenta", "Precio"),
                ("StockActual", "Stock"),
                ("StockMinimo", "Mín."),
                ("FechaVencimiento", "Vence"),
                ("EstadoTexto", "Estado"));

            ActualizarBotonEstado();
        }

        private Producto ProductoSeleccionado() => dgvProductos.CurrentRow?.DataBoundItem as Producto;

        // El botón de estado cambia entre "Dar de baja" y "Activar" según el producto.
        private void ActualizarBotonEstado()
        {
            var p = ProductoSeleccionado();
            if (p == null || p.Activo)
            {
                btnDesactivar.Text = "Dar de baja";
                btnDesactivar.ForeColor = EstilosFmo.Cereza;
            }
            else
            {
                btnDesactivar.Text = "Activar";
                btnDesactivar.ForeColor = EstilosFmo.Menta;
            }
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmProducto())
            {
                frm.ShowDialog(this.FindForm());
            }

            CargarProductos();
        }

        private void btnEditar_Click(object sender, EventArgs e) => EditarProducto(ProductoSeleccionado());

        private void btnDesactivar_Click(object sender, EventArgs e) => CambiarEstado(ProductoSeleccionado());

        private void EditarProducto(Producto producto)
        {
            if (producto == null)
            {
                MensajeFmo.Info("Selecciona un producto de la tabla.", "Inventario");
                return;
            }

            using (var frm = new FrmProducto(producto))
            {
                frm.ShowDialog(this.FindForm());
            }

            CargarProductos();
        }

        // Alterna el estado del producto seleccionado (activar / dar de baja).
        private void CambiarEstado(Producto producto)
        {
            if (producto == null)
            {
                MensajeFmo.Info("Selecciona un producto de la tabla.", "Inventario");
                return;
            }

            bool nuevoEstado = !producto.Activo;
            string accion = nuevoEstado ? "activar" : "dar de baja";
            if (!MensajeFmo.Confirmar($"¿Seguro que deseas {accion} el producto \"{producto.Nombre}\"?", "Estado del producto"))
                return;

            try
            {
                bool ok = ProductoDAO.CambiarEstadoProducto(producto.IdProducto, nuevoEstado);
                if (ok) MensajeFmo.Exito(nuevoEstado ? "Producto activado." : "Producto dado de baja.", "Inventario");
                else MensajeFmo.Advertencia("No se pudo cambiar el estado.", "Inventario");
                CargarProductos();
            }
            catch (Exception ex)
            {
                MensajeFmo.Error(ex.Message, "Inventario");
            }
        }
    }
}
