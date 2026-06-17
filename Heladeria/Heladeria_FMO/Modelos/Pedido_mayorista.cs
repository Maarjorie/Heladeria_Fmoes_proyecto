using System;
using System.Collections.Generic;
using System.Text;

namespace Heladeria_FMO.Modelos
{
    public class Pedido_mayorista
    {
        public int IdPedido { get; set; }
        public int IdCliente { get; set; }
        public int IdAtendidoPor { get; set; }
        public int IdEntregadoPor { get; set; }
        public string CodigoPedido { get; set; } 
        public string CodigoRetiro { get; set; } 
        public string Estado { get; set; } 
        public decimal Subtotal { get; set; }
        public decimal Descuento { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaPedido { get; set; }
        public DateTime FechaConfirmacion { get; set; }
        public DateTime FechaEntrega { get; set; }
    }
}
