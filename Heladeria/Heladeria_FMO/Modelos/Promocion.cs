using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heladeria_FMO.Modelos
{
    public class Promocion
    {
        public int IdPromocion { get; set; }
        public int? IdProducto { get; set; }
        public int? IdCategoria { get; set; }
        public string Nombre { get; set; } 
        public string Descripcion { get; set; } 
        public string TipoDescuento { get; set; } 
        public decimal ValorDescuento { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
