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
        public decimal montoEsperado { get; set; }
        public decimal montoContado { get; set; }
        public decimal? diferencia { get; set; }
        public DateTime fechaRegistro { get; set; }
    }
}
