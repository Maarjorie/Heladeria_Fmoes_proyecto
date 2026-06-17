using System;
using System.Collections.Generic;
using System.Text;

namespace Heladeria_FMO.Modelos
{
    public class Salida_Ruta
    {
        public int IdSalida { get; set; }
        public int IdVendedor { get; set; }
        public int IdRuta { get; set; }
        public int IdUsuario { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan HoraSalida { get; set; }
        public string Vehiculo { get; set; } 
        public decimal Comision { get; set; }
        public string Estado { get; set; } 
        public DateTime FechaRegistro { get; set; }
    }
}
