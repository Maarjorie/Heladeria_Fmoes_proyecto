using System;
using System.Collections.Generic;
using System.Text;

namespace Heladeria_FMO.Modelos
{
    public class Arqueo_caja
    {
        public int IdArqueo { get; set; }
        public int IdCaja { get; set; }
        public int IdRealizadoPor { get; set; }
        public decimal MontoEsperado { get; set; }
        public decimal MontoContado { get; set; }
        public decimal? Diferencia { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
