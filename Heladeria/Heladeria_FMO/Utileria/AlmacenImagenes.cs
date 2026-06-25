using System;
using System.IO;

namespace Heladeria_FMO.Utileria
{
    // Centraliza el almacenamiento de imágenes del sistema (fotos de empleados,
    // etc.). Las imágenes se copian a una carpeta propia junto al ejecutable y en
    // la base de datos solo se guarda una RUTA RELATIVA, de modo que el sistema
    // siga funcionando aunque se mueva la carpeta o cambie el equipo.
    public static class AlmacenImagenes
    {
        // Subcarpetas estándar.
        public const string Empleados = "Imagenes/Empleados";

        // Carpeta raíz: junto al .exe de la aplicación.
        private static string Raiz => AppDomain.CurrentDomain.BaseDirectory;

        // Copia la imagen seleccionada por el usuario a la subcarpeta indicada y
        // devuelve la ruta RELATIVA que debe persistirse. Si la ruta de origen no
        // es válida devuelve cadena vacía.
        public static string Guardar(string rutaOrigen, string subcarpeta)
        {
            if (string.IsNullOrWhiteSpace(rutaOrigen) || !File.Exists(rutaOrigen))
                return "";

            string destinoDir = Path.Combine(Raiz, subcarpeta);
            Directory.CreateDirectory(destinoDir);

            string extension = Path.GetExtension(rutaOrigen);
            string nombreUnico = $"{Guid.NewGuid():N}{extension}";
            string destino = Path.Combine(destinoDir, nombreUnico);

            File.Copy(rutaOrigen, destino, overwrite: true);

            // Se guarda con separador "/" para que sea portátil.
            return Path.Combine(subcarpeta, nombreUnico).Replace('\\', '/');
        }

        // Convierte una ruta guardada (relativa o absoluta) en una ruta absoluta
        // existente lista para abrir. Devuelve null si el archivo no existe.
        public static string Resolver(string rutaGuardada)
        {
            if (string.IsNullOrWhiteSpace(rutaGuardada)) return null;

            string ruta = Path.IsPathRooted(rutaGuardada)
                ? rutaGuardada
                : Path.Combine(Raiz, rutaGuardada.Replace('/', Path.DirectorySeparatorChar));

            return File.Exists(ruta) ? ruta : null;
        }

        // True si la ruta apunta a un archivo absoluto existente, es decir, una
        // imagen recién seleccionada por el usuario que aún no se ha copiado al
        // almacén interno.
        public static bool EsArchivoExterno(string ruta)
        {
            return !string.IsNullOrWhiteSpace(ruta)
                && Path.IsPathRooted(ruta)
                && File.Exists(ruta);
        }
    }
}
