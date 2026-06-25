using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Heladeria_FMO.Servicio;
using Heladeria_FMO.Utileria;

namespace Heladeria_FMO.Intefaz.Mayorista.Dialogos
{
    // Historial de pedidos de un cliente mayorista.
    public partial class FrmHistorialCliente : Form
    {
        private readonly int _idCliente;
        private readonly string _nombreCliente;

        public FrmHistorialCliente(int idCliente, string nombreCliente)
        {
            _idCliente = idCliente;
            _nombreCliente = nombreCliente;
            InitializeComponent();
            AplicarTema();
            Cargar();
        }

        // El Diseñador maneja el layout; aquí se aplica el tema oscuro de la app.
        private void AplicarTema()
        {
            BackColor = EstilosFmo.Superficie;
            EstilosFmo.Tarjeta(marco);

            titulo.Text = $"Historial · {_nombreCliente}";
            titulo.Font = EstilosFmo.Fuente(15F, FontStyle.Bold);
            titulo.ForeColor = EstilosFmo.TextoFuerte;

            btnCerrar.Font = EstilosFmo.Fuente(10F, FontStyle.Bold);
            EstilosFmo.BotonContorno(btnCerrar);

            EstilosFmo.Tabla(dgv);
        }

        private void BtnCerrar_Click(object sender, EventArgs e) => Close();

        private void Cargar()
        {
            try
            {
                DataTable t = PedidoMayoristaServicio.ListarPedidosPorCliente(_idCliente);
                dgv.DataSource = t;
                EstilosFmo.MostrarSoloColumnas(dgv,
                    ("fecha_pedido", "Fecha"),
                    ("codigo_retiro", "Código retiro"),
                    ("estado", "Estado"),
                    ("total", "Total"));
            }
            catch (Exception ex)
            {
                MensajeFmo.Advertencia(ex.Message, "Historial");
            }
        }
    }
}
