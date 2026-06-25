using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Heladeria_FMO.Servicio;
using Heladeria_FMO.Utileria;

namespace Heladeria_FMO.Intefaz.Mayorista.Dialogos
{
    // Historial de pedidos de un cliente mayorista.
    public class FrmHistorialCliente : Form
    {
        private readonly int _idCliente;
        private readonly string _nombreCliente;
        private readonly Guna2DataGridView dgv = new();

        public FrmHistorialCliente(int idCliente, string nombreCliente)
        {
            _idCliente = idCliente;
            _nombreCliente = nombreCliente;
            ConstruirUi();
            Cargar();
        }

        private void ConstruirUi()
        {
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            ShowInTaskbar = false;
            ClientSize = new Size(680, 480);
            BackColor = EstilosFmo.Superficie;

            var marco = new Guna2Panel { Dock = DockStyle.Fill };
            EstilosFmo.Tarjeta(marco);
            Controls.Add(marco);

            var titulo = new Guna2HtmlLabel
            {
                Text = $"Historial · {_nombreCliente}",
                Location = new Point(24, 18),
                Size = new Size(560, 30),
                Font = EstilosFmo.Fuente(15F, FontStyle.Bold),
                ForeColor = EstilosFmo.TextoFuerte,
                BackColor = Color.Transparent
            };

            var btnCerrar = new Guna2Button { Text = "✕", Size = new Size(32, 32), Location = new Point(624, 14), Font = EstilosFmo.Fuente(10F, FontStyle.Bold) };
            EstilosFmo.BotonContorno(btnCerrar);
            btnCerrar.Click += (s, e) => Close();

            EstilosFmo.Tabla(dgv);
            dgv.Location = new Point(24, 60);
            dgv.Size = new Size(632, 400);

            marco.Controls.AddRange(new Control[] { titulo, btnCerrar, dgv });
        }

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
