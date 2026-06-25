using Guna.UI2.WinForms;
using System.Text.RegularExpressions;

namespace Heladeria_FMO.Utileria
{
    // Clase encargada de realizar validaciones.
    public static class Validaciones
    {
        public static void SoloLetras(KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) &&
                e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        public static void SoloNumeros(KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public static void SoloLetrasNumeros(KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar) &&
                e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        public static void SoloDecimal(KeyPressEventArgs e, Guna2TextBox txt)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && txt.Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        public static void SinCaracteresEspeciales(KeyPressEventArgs e)
        {
            if (char.IsLetterOrDigit(e.KeyChar) || char.IsControl(e.KeyChar) || e.KeyChar == ' ' ||e.KeyChar == '-' || e.KeyChar == '_')
            {
                return;
            }

            e.Handled = true;
        }

        public static bool CampoVacio(string texto)
        {
            return string.IsNullOrWhiteSpace(texto);
        }

        public static bool CorreoValido(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo))
                return false;

            return Regex.IsMatch(correo,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        public static bool TelefonoValido(string telefono)
        {
            return Regex.IsMatch(telefono, @"^[0-9]{8}$");
        }

        public static bool DuiValido(string dui)
        {
            return Regex.IsMatch(dui, @"^[0-9]{8}-[0-9]{1}$");
        }

        public static bool NitValido(string nit)
        {
            return Regex.IsMatch(nit, @"^[0-9]{4}-[0-9]{6}-[0-9]{3}-[0-9]{1}$");
        }

        public static bool DecimalPositivo(decimal valor)
        {
            return valor >= 0;
        }

        public static bool EnteroPositivo(int valor)
        {
            return valor > 0;
        }

        public static bool HorarioValido(TimeSpan inicio, TimeSpan fin)
        {
            return inicio < fin;
        }
    }
}