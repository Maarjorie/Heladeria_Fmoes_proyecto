using System;
using System.Collections.Generic;
using System.Text;

namespace Heladeria_FMO.Modelos
{
    public class Detalle_salida_ruta
    {
        public int IdDetalleSalida { get; set; }
        public int IdSalida { get; set; }
        public int IdProducto { get; set; }
        public int CantidadCarga { get; set; }
        public decimal PrecioVenta { get; set; }
    }
}
