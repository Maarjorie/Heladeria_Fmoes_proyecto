using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heladeria_FMO.Modelos
{
    public class Detalle_liquidacion
    {
        public int IdDetalleLiquidacion { get; set; }
        public int IdLiquidacion { get; set; }
        public int IdProducto { get; set; }
        public int CantidadCarga { get; set; }
        public int CantidadVendida { get; set; }
        public int CantidadDevuelta { get; set; }
        public int CantidadDañada { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal Subtotal { get; set; }
    }
}
