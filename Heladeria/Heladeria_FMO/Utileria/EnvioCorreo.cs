using System;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heladeria_FMO.Utileria
{
    public static class EnvioCorreo//metodo encargado para enviar correos de notificacion mediante SMTP   
    {
        private const string servidor = "smtp.gmail.com";
        private const int puerto = 587;
        private const string remitente = "heladeriafmo@gmail.com";
        private const string claveApp = "nnqs rmbg osma usob";

        public static async Task Enviar(string destinatario, string asunto, string cuerpoHtml)
        {
            using (var mensaje =  new MailMessage())
            {
                mensaje.From = new MailAddress(remitente, "Helados fmo");
                mensaje.To.Add(destinatario);
                mensaje.Subject = asunto;
                mensaje.Body = cuerpoHtml;
                mensaje.IsBodyHtml = true;

                using (var cliente = new SmtpClient(servidor, puerto))
                {
                    cliente.Credentials = new NetworkCredential(remitente, claveApp);
                    cliente.EnableSsl = true;
                    await cliente.SendMailAsync(mensaje);
                }
            }
        }

    }
}
