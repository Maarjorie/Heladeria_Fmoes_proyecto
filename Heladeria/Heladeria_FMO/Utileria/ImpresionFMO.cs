using BarcodeStandard;
using SkiaSharp;
using QRCoder;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using Heladeria_FMO.Modelos;
using System.Collections.Generic;

namespace Heladeria_FMO.Utileria
{
    public static class ImpresionFMO
    {
        private const int AnchoTicket = 280;
        private const string NombreNegocio = "Helados La FMO";
        private const string LineaSimple = "--------------------------------";
        private const string LineaDoble = "================================";

        // Imprime el ticket de una venta en sucursal.
        public static void ImprimirTicketVenta(
            int idVenta,
            string cajero,
            List<(string nombre, int cantidad, decimal precio)> lineas,
            decimal total,
            decimal efectivo,
            decimal cambio)
        {
            string texto = ConstruirTicketVenta(idVenta, cajero, lineas, total, efectivo, cambio);
            Imprimir(texto);
        }

        // Imprime el comprobante de un pedido mayorista
        public static void ImprimirComprobanteMayorista(
            string codigoPedido,
            string codigoRetiro,
            string cliente,
            decimal total)
        {
            var sb = new StringBuilder();
            sb.AppendLine(LineaDoble);
            sb.AppendLine(Centrar(NombreNegocio));
            sb.AppendLine(Centrar("PEDIDO MAYORISTA"));
            sb.AppendLine(LineaDoble);
            sb.AppendLine("");
            sb.AppendLine("Pedido: " + codigoPedido);
            sb.AppendLine("Cliente: " + cliente);
            sb.AppendLine("Fecha: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            sb.AppendLine("Total: $" + total.ToString("N2"));
            sb.AppendLine("");
            sb.AppendLine(LineaSimple);
            sb.AppendLine("CODIGO DE RETIRO:");
            sb.AppendLine("");
            sb.AppendLine(Centrar(codigoRetiro));
            sb.AppendLine("");
            sb.AppendLine("Presente este comprobante");
            sb.AppendLine("al retirar su pedido.");
            sb.AppendLine("");
            sb.AppendLine(LineaDoble);
            Imprimir(sb.ToString());
        }

        // Genera una imagen QR con el codigo de retiro del pedido.;
        public static Bitmap GenerarQrPedido(string codigoRetiro)
        {
            using var qrGenerator = new QRCodeGenerator();
            var qrData = qrGenerator.CreateQrCode(codigoRetiro, QRCodeGenerator.ECCLevel.M);
            using var qrCode = new QRCode(qrData);
            return qrCode.GetGraphic(5, Color.Black, Color.White, true);
        }

        // Genera una imagen de codigo de barras Code128 con el
        // codigo_barras del producto
        public static Bitmap GenerarCodigoBarras(string codigoBarras)
        {
            if (string.IsNullOrWhiteSpace(codigoBarras))
                throw new ArgumentException("El codigo de barras no puede estar vacio.");

            var barcode = new Barcode();
            using SKImage imagen = barcode.Encode(BarcodeStandard.Type.Code128, codigoBarras, SKColors.Black, SKColors.White, 300, 100);
            using SKData datos = imagen.Encode(SKEncodedImageFormat.Png, 100);
            using var flujo = new MemoryStream(datos.ToArray());
            return new Bitmap(flujo);
        }

        public static string GuardarCodigoBarras(string codigoBarras, string nombreProducto = null)
        {
            using Bitmap bitmap = GenerarCodigoBarras(codigoBarras);
            string nombreArchivo = string.IsNullOrWhiteSpace(nombreProducto)
                ? codigoBarras
                : $"{nombreProducto}_{codigoBarras}";
            return AlmacenImagenes.GuardarBitmap(bitmap, AlmacenImagenes.CodigosBarras, nombreArchivo);
        }

        
        public static void ActivarEscaner(
            System.Windows.Forms.TextBox txtEscaner,
            Action<string> onCodigoLeido)
        {
            txtEscaner.KeyDown += (s, e) =>
            {
                if (e.KeyCode == System.Windows.Forms.Keys.Enter)
                {
                    string codigo = txtEscaner.Text.Trim();
                    txtEscaner.Clear();
                    txtEscaner.Focus();
                    e.SuppressKeyPress = true;

                    if (!string.IsNullOrEmpty(codigo))
                        onCodigoLeido(codigo);
                }
            };
            txtEscaner.Focus();
        }

        // Helpers privados
        private static string ConstruirTicketVenta(
            int idVenta,
            string cajero,
            List<(string nombre, int cantidad, decimal precio)> lineas,
            decimal total, decimal efectivo, decimal cambio)
        {
            var sb = new StringBuilder();
            sb.AppendLine(LineaDoble);
            sb.AppendLine(Centrar(NombreNegocio));
            sb.AppendLine(LineaDoble);
            sb.AppendLine("");
            sb.AppendLine("Venta No: " + idVenta);
            sb.AppendLine("Fecha: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            sb.AppendLine("Cajero: " + cajero);
            sb.AppendLine("");
            sb.AppendLine(LineaSimple);

            foreach (var (nombre, cantidad, precio) in lineas)
            {
                sb.AppendLine(nombre);
                sb.AppendLine($"  {cantidad} x ${precio:N2}  =  ${(cantidad * precio):N2}");
                sb.AppendLine("");
            }

            sb.AppendLine(LineaSimple);
            sb.AppendLine("TOTAL:    $" + total.ToString("N2"));
            sb.AppendLine("EFECTIVO: $" + efectivo.ToString("N2"));
            sb.AppendLine("CAMBIO:   $" + cambio.ToString("N2"));
            sb.AppendLine("");
            sb.AppendLine(Centrar("Gracias por su compra"));
            sb.AppendLine("");
            sb.AppendLine(LineaDoble);
            return sb.ToString();
        }

        private static void Imprimir(string texto)
        {
            var pd = new PrintDocument();
            pd.PrintPage += (s, e) =>
            {
                var fuente = new Font("Consolas", 9);
                e.Graphics.DrawString(texto, fuente, Brushes.Black, new RectangleF(0, 0, AnchoTicket, 3000));
            };
            pd.Print();
        }

        private static string Centrar(string texto, int ancho = 32)
        {
            if (texto.Length >= ancho) return texto;
            int espacios = (ancho - texto.Length) / 2;
            return new string(' ', espacios) + texto;
        }
    }
}
