using System;
using System.Collections.Generic;
using System.Text;

namespace Heladeria_FMO.Modelos
{
    public class Movimiento_caja
    {
        public int IdMovimientoCaja { get; set; }
        public int IdCaja { get; set; }
        public int IdUsuario { get; set; }
        public string Tipo { get; set; } 
        public string Concepto { get; set; } 
        public decimal Monto { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
