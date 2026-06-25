namespace Heladeria_FMO.Modelos
{
    // Datos mínimos para emitir el comprobante de retiro (con QR) de un pedido
    // mayorista que se envía por correo al cliente.
    public class Comprobante_mayorista
    {
        public string CodigoPedido { get; set; }
        public string CodigoRetiro { get; set; }
        public decimal Total { get; set; }
        public string Cliente { get; set; }
        public string Correo { get; set; }
    }
}
