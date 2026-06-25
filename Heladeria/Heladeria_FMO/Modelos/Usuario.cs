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
        public string Contrasenia_hash { get; set; }
        public string Contrasenia_salt { get; set; }
        public string Correo { get; set; }
        public string NombreRol {  get; set; }
        public bool Activo { get; set; }
        public DateTime fecha_registro {  get; set; }

        // Contrasena temporal de recuperacion (no destruye la real). Valida
        // unicamente mientras ContraseniaTempExpira sea futura.
        public string Contrasenia_temp_hash { get; set; }
        public string Contrasenia_temp_salt { get; set; }
        public DateTime? Contrasenia_temp_expira { get; set; }

        // Texto legible del estado (para mostrar en tablas sin checkbox).
        public string EstadoTexto => Activo ? "Activo" : "Inactivo";
    }
}
