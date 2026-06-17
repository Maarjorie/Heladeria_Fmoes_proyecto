using System;
using System.Collections.Generic;
using System.Text;

namespace Heladeria_FMO.Modelos
{
    public class Venta_sucursal
    {
        public int IdVenta { get; set; }
        public int IdCaja { get; set; }
        public int IdCajero { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Descuento { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
