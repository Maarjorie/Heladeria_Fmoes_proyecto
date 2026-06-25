using Heladeria_FMO.Acceso_a_datos_db;
using Heladeria_FMO.Modelos;
using Heladeria_FMO.Utileria;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heladeria_FMO.Servicio
{
    public static class PedidoMayoristaServicio
    {
        //Evita que el dao genere error al necesitar esos dos como llaves foraneas de las tablas, no tendrian relacion y generaria un error en la consulta
        public static int CrearPedido(Pedido_mayorista pedido)
        {
            if (pedido.IdCliente <= 0)
                throw new Exception("Debe seleccionar un cliente.");

            if (pedido.IdAtendidoPor <= 0)
                throw new Exception("Debe existir un usuario que atienda el pedido.");

            return Pedido_mayoristaDAO.CrearPedidoMayorista(pedido);
        }

        //Valida que esté seleccionado o exista ese id, porque ninguna tabla maneja el indice 0, Ni -1 
        public static bool ConfirmarPedido(Pedido_mayorista pedido)
        {
            if (pedido.IdPedido <= 0)
                throw new Exception("Debe seleccionar un pedido.");

            bool ok = Pedido_mayoristaDAO.ConfirmarPedidoMayorista(pedido);

            if (ok)
            {
                // Pedido confirmado = listo para retiro: se notifica a supervisión...
                NotificacionServicio.NotificarPedidoListo(pedido.IdPedido, pedido.CodigoRetiro);
                // ...y se envía el comprobante con QR al cliente mayorista.
                EnviarComprobanteAlCliente(pedido.IdPedido);
            }

            return ok;
        }

        // Envía al cliente mayorista, en segundo plano, el comprobante de retiro
        // con su código QR embebido. Nunca interrumpe el flujo si falla el correo.
        public static void EnviarComprobanteAlCliente(int idPedido)
        {
            _ = Task.Run(async () =>
            {
                try
                {
                    Comprobante_mayorista c = Pedido_mayoristaDAO.ObtenerComprobante(idPedido);
                    if (c == null || string.IsNullOrWhiteSpace(c.Correo) ||
                        string.IsNullOrWhiteSpace(c.CodigoRetiro))
                        return;

                    byte[] qr;
                    using (Bitmap bmp = ImpresionFMO.GenerarQrPedido(c.CodigoRetiro))
                    using (var ms = new MemoryStream())
                    {
                        bmp.Save(ms, ImageFormat.Png);
                        qr = ms.ToArray();
                    }

                    const string cid = "qrRetiro";
                    string html = PlantillaCorreo.ComprobanteMayorista(
                        c.Cliente, c.CodigoPedido, c.CodigoRetiro, c.Total, cid);

                    await EnvioCorreo.EnviarConImagen(
                        c.Correo, "Helados FMO · Comprobante de retiro", html, qr, cid);
                }
                catch (Exception)
                {
                    // El envío del comprobante no debe afectar la confirmación.
                }
            });
        }

        //Mantiene que no pase valores negativos o 0 al metodo de la clase statica pedido_mayorista
        public static bool EntregarPedido(Pedido_mayorista pedido)
        {
            if (pedido.IdPedido <= 0)
                throw new Exception("Debe seleccionar un pedido.");

            if (pedido.IdEntregadoPor <= 0)
                throw new Exception("Debe existir un usuario que entregue el pedido.");

            return Pedido_mayoristaDAO.EntregarPedidoMayorista(pedido);
        }

        //No tiene validaciones pero se mantiene en la logica de negocio para evitar tener que llamar a la clase dao si no es por esta capa
        public static DataTable ListarPedidos()
        {
            return Pedido_mayoristaDAO.ListarPedidosMayoristas();
        }

        //Rechaza (cancela) un pedido pendiente
        public static bool CancelarPedido(int idPedido)
        {
            if (idPedido <= 0)
                throw new Exception("Debe seleccionar un pedido.");

            return Pedido_mayoristaDAO.CancelarPedidoMayorista(idPedido);
        }

        // Fija los totales del pedido aplicando el descuento (en monto) del cliente.
        public static bool FijarTotales(int idPedido, decimal subtotal, decimal descuento)
        {
            if (idPedido <= 0) throw new Exception("Pedido inválido.");
            if (descuento < 0) descuento = 0;
            if (descuento > subtotal) descuento = subtotal;
            decimal total = subtotal - descuento;
            return Pedido_mayoristaDAO.ActualizarTotales(idPedido, subtotal, descuento, total);
        }

        // Historial de pedidos de un cliente.
        public static DataTable ListarPedidosPorCliente(int idCliente)
        {
            if (idCliente <= 0) throw new Exception("Debe seleccionar un cliente.");
            return Pedido_mayoristaDAO.ListarPedidosPorCliente(idCliente);
        }
    }
}
