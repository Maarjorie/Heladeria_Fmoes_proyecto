using System;
using System.Collections.Generic;
using System.Text;

namespace Heladeria_FMO.Modelos
{
    public class Caja
    {
        public int IdCaja { get; set; }
        public int IdCajero { get; set; }
        public DateTime FechaApertura { get; set; }
        public decimal FondoInicial { get; set; }
        public DateTime FechaCierre { get; set; }
        public decimal TotalVentas { get; set; }
        public decimal MontoContado { get; set; }
        public decimal Diferencia { get; set; }
        public string Estado { get; set; } = "";
    }
}
