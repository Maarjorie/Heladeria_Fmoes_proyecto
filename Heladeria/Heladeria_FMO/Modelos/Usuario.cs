using System;
using System.Collections.Generic;
using System.Text;

namespace Heladeria_FMO.Modelos
{
    public class Usuario
    {
        public int id_Usuario { get; set; }
        public int id_rol { get; set; }
        public string Nombre { get; set; }
        public string Usuario_ { get; set; }
        public string Contrasenia { get; set; }
        public string Correo { get; set; }
        public int Activo { get; set; }
    }
}
