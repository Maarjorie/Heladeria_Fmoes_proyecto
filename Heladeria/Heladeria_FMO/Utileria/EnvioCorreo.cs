using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

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
            using (var mensaje = new MailMessage())
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

        // Envía un correo HTML con una imagen embebida (inline) referenciada en el
        // HTML mediante "cid:idImagen". Es la forma que respetan clientes como Gmail
        // (que bloquean imágenes en base64), ideal para incrustar el QR.
        public static async Task EnviarConImagen(string destinatario, string asunto,
            string cuerpoHtml, byte[] imagenPng, string idImagen)
        {
            using (var mensaje = new MailMessage())
            {
                mensaje.From = new MailAddress(remitente, "Helados fmo");
                mensaje.To.Add(destinatario);
                mensaje.Subject = asunto;
                mensaje.IsBodyHtml = true;

                var vista = AlternateView.CreateAlternateViewFromString(
                    cuerpoHtml, null, MediaTypeNames.Text.Html);

                var recurso = new LinkedResource(new MemoryStream(imagenPng), "image/png")
                {
                    ContentId = idImagen,
                    TransferEncoding = TransferEncoding.Base64,
                    ContentType = { Name = idImagen + ".png" }
                };
                vista.LinkedResources.Add(recurso);
                mensaje.AlternateViews.Add(vista);

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