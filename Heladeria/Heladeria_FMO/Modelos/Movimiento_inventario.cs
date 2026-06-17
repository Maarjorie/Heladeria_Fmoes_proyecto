using System;
using System.Collections.Generic;
using System.Text;

namespace Heladeria_FMO.Modelos
{
    public class Movimiento_inventario
    {
        public int IdMovimiento { get; set; }
        public int IdProducto { get; set; }
        public int IdUsuario { get; set; }
        public string Tipo { get; set; } 
        public int Cantidad { get; set; }
        public decimal CostoUnitario { get; set; }
        public string Referencia { get; set; } 
        public string Observacion { get; set; } 
        public DateTime FechaRegistro { get; set; }
    }
}
