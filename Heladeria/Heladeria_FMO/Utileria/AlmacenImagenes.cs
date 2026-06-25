using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

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
        public const string Productos = "Imagenes/Productos";
        public const string CodigosBarras = "Imagenes/CodigosBarras";

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

        // Guarda una imagen generada por el sistema en una subcarpeta interna y
        // devuelve la ruta relativa. Si se proporciona un nombre base, se reutiliza.
        public static string GuardarBitmap(Bitmap imagen, string subcarpeta, string nombreBase)
        {
            if (imagen == null) return "";

            string destinoDir = Path.Combine(Raiz, subcarpeta);
            Directory.CreateDirectory(destinoDir);

            string nombreLimpio = LimpiarNombreArchivo(nombreBase);
            if (string.IsNullOrWhiteSpace(nombreLimpio))
                nombreLimpio = Guid.NewGuid().ToString("N");

            string nombreArchivo = nombreLimpio + ".png";
            string destino = Path.Combine(destinoDir, nombreArchivo);
            imagen.Save(destino, ImageFormat.Png);

            return Path.Combine(subcarpeta, nombreArchivo).Replace('\\', '/');
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

        private static string LimpiarNombreArchivo(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre)) return "";

            char[] invalidos = Path.GetInvalidFileNameChars();
            string limpio = new string(nombre
                .Trim()
                .Select(c => invalidos.Contains(c) ? '_' : c)
                .ToArray());

            return limpio.Length > 80 ? limpio.Substring(0, 80) : limpio;
        }
    }
}
