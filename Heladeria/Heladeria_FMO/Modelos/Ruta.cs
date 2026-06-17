using System;
using System.Collections.Generic;
using System.Text;

namespace Heladeria_FMO.Modelos
{
    public class Ruta
    {
        public int IdRuta { get; set; }
        public string Nombre { get; set; } 
        public string Zona { get; set; }
        public int IdResponsable { get; set; }
        public TimeSpan HorarioInicio { get; set; }
        public TimeSpan HorarioFin { get; set; }
        public bool Activo { get; set; }
    }
}
