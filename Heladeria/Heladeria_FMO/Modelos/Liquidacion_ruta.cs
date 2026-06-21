using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heladeria_FMO.Modelos
{
    public class Liquidacion_ruta
    {
        public int IdLiquidacion { get; set; }
        public int IdSalida { get; set; }
        public int IdValidadoPor { get; set; }
        public TimeSpan HoraRegreso { get; set; }
        public decimal TotalVendido { get; set; }
        public decimal ComisionGenerada { get; set; }
        public decimal DineroEntregado { get; set; }
        public decimal Diferencia { get; set; }
        public string Observacion { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
