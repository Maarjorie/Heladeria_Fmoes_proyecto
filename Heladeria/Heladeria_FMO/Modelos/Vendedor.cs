using System;
using System.Collections.Generic;
using System.Text;

namespace Heladeria_FMO.Modelos
{
    public class Vendedor
    {
        public int IdVendedor { get; set; }
        public string CodigoEmpleado { get; set; } 
        public string Nombre { get; set; } 
        public string Fotografia { get; set; }
        public string Dui { get; set; } 
        public string Telefono { get; set; } 
        public string Direccion { get; set; } 
        public bool Estado { get; set; } 
        public DateTime FechaRegistro { get; set; }
    }
}
