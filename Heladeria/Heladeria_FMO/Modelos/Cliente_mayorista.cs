using System;
using System.Collections.Generic;
using System.Text;

namespace Heladeria_FMO.Modelos
{
    public class Cliente_mayorista
    {
        public int IdCliente { get; set; }
        public string NombreComercial { get; set; }
        public string Nit { get; set; } 
        public string Encargado { get; set; } 
        public string Direccion { get; set; } 
        public string Telefono { get; set; } 
        public string Correo { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }

        // Descuento fijo (%) que se aplica a los pedidos de este cliente.
        public decimal DescuentoPorcentaje { get; set; }
    }
}
