using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heladeria_FMO.Modelos
{
    public class Notificacion
    {
        public int IdNotificacion { get; set; }
        public string Tipo { get; set; } 
        public int ReferenciaId { get; set; }
        public string Mensaje { get; set; }
        public bool Enviado { get; set; }
        public bool Leido { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
