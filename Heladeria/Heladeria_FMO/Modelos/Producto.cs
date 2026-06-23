using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Heladeria_FMO.Modelos
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public int IdCategoria { get; set; }
        public string Codigo { get; set; } 
        public string CodigoBarras { get; set; } 
        public string Nombre { get; set; } 
        public string NombreCategoria { get; set; }
        public string Presentacion { get; set; } 
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public int StockActual { get; set; }
        public int StockMinimo { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int NivelAlertaVencimiento { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string ImagenRuta { get; set; }
    }
}
