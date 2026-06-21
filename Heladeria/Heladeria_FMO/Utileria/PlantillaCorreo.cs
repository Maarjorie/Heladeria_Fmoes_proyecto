using System;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heladeria_FMO.Utileria
{
    //tipo de alerta disponible para los correos de notificacion
    public enum TipoAlerta
    {
        //cada enum define un color distinto para la etiqueta del correo
        Info,
        Advertencia,
        Peligro,
        Exito
    }

    // clase encargada de generar los html de los correos de notificacion
    public static class PlantillaCorreo
    {
        //Genera el html completo de un correo de notificación
        public static string Generar(TipoAlerta tipo, string titulo, string mensaje, Dictionary<string, string> datos)
        {
            (string fondo, string texto) = ObtenerColores(tipo);

            var filas = new StringBuilder();
            foreach(var dato in datos)
            {
                filas.Append($@"
                <tr>
                    <td style=""color:#888780; padding:6px 0; border-bottom:1px solid #eeeeee;"">{dato.Key}</td>
                    <td style=""text-align:right; padding:6px 0; border-bottom:1px solid #eeeeee; font-weight:bold;"">{dato.Value}</td>
                </tr>");
            }

            return $@"
            <table width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""background-color:#f1efe8; padding:24px;"">
              <tr><td align=""center"">
                <table width=""420"" cellpadding=""0"" cellspacing=""0"" style=""background-color:#ffffff; border-radius:8px; font-family:Arial,sans-serif;"">
                  <tr><td style=""background-color:#e6f1fb; color:#0c447c; padding:16px 20px; font-size:16px; font-weight:bold;"">
                    Helados La FMO
                  </td></tr>
                  <tr><td style=""padding:20px;"">
                    <span style=""display:inline-block; background-color:{fondo}; color:{texto}; padding:6px 12px; border-radius:4px; font-size:13px; font-weight:bold;"">{titulo}</span>
                    <p style=""font-size:15px; color:#2c2c2a; margin:12px 0;"">{mensaje}</p>
                    <table width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""font-size:14px; border-collapse:collapse;"">
                      {filas}
                    </table>
                    <p style=""font-size:12px; color:#888780; margin-top:16px;"">Este es un mensaje automático del sistema de gestión.</p>
                  </td></tr>
                </table>
              </td></tr>
            </table>";
        }

        private static (string fondo, string texto) ObtenerColores(TipoAlerta tipo) //metodo para asignar los colores dependiendo la alerta
        {
            switch (tipo)
            {
                case TipoAlerta.Advertencia: return ("#faeeda", "#633806");
                case TipoAlerta.Peligro: return ("#fcebeb", "#791f1f");
                case TipoAlerta.Exito: return ("#eaf3de", "#27500a");
                default: return ("#e6f1fb", "#0c447c");
            }
        }
    }
}
