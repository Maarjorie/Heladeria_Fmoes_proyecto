using Heladeria_FMO.Acceso_a_datos_db;
using Heladeria_FMO.Modelos;
using Heladeria_FMO.Utileria;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heladeria_FMO.Servicio
{
    public static class InventarioServicio
    {
        //Revisa productos con stock por debajo del minimo y genera una notificacion pendiente por cada uno
        public static void DetectarBajoStock()
        {
            List<Producto> productos = ProductoDAO.ProductoBajoStock();

            foreach (Producto producto in productos)
            {
                string mensaje = $"El producto {producto.Nombre} tiene {producto.StockActual} unidades, por debajo del minimo de {producto.StockMinimo}.";

                NotificacionDAO.InsertarNotificacion("bajo_stock", producto.IdProducto, mensaje);
            }
        }


        /* Revisa productos proximos a vencer y genera UNA sola notificacion
            por producto con el nivel de urgencia mas alto que aplique
            (7 > 15 > 30 > 60 dias), usando NivelAlertaVencimiento (0-4)
            para no repetir la misma alerta una vez ya generada.*/
        public static void DetectarProductosPorVencer()
        {
            List<Producto> productos = ProductoDAO.ProductosPorVencer(60);

            foreach (Producto producto in productos)
            {
                int dias = (producto.FechaVencimiento - DateTime.Now).Days;
                int nivelActual = producto.NivelAlertaVencimiento;
                int nuevoNivel = 0;

                if (dias <= 7 && nivelActual < 4)
                {
                    nuevoNivel = 4;
                }
                else if (dias <= 15 && nivelActual < 3)
                {
                    nuevoNivel = 3;
                }
                else if (dias <= 30 && nivelActual < 2)
                {
                    nuevoNivel = 2;
                }
                else if (dias <= 60 && nivelActual < 1)
                {
                    nuevoNivel = 1;
                }

                if (nuevoNivel > 0)
                {
                    string mensaje = $"El producto {producto.Nombre} vence en {dias} dias ({producto.FechaVencimiento:dd/MM/yyyy}).";
                    NotificacionDAO.InsertarNotificacion("prox_vencer", producto.IdProducto, mensaje);
                    ProductoDAO.ActualizarNivelAlerta(producto.IdProducto, nuevoNivel);
                }
            }
        }

        // Envia por correo todas las notificaciones que no se han envias
        public static async Task EnviarNotificacionesPendientes(string correoDestino)
        {
            List<Notificacion> pendientes = NotificacionDAO.ListarPendientes();

            foreach (Notificacion notificacion in pendientes)
            {
                TipoAlerta tipo = ObtenerTipoAlerta(notificacion.Tipo);

                var datos = new Dictionary<string, string>
                {
                    { "Detalle", notificacion.Mensaje },
                    { "Fecha", notificacion.FechaRegistro.ToString("dd/MM/yyyy HH:mm") }
                };

                string cuerpo = PlantillaCorreo.Generar(
                    tipo,
                    ObtenerTitulo(notificacion.Tipo),
                    notificacion.Mensaje,
                    datos
                );

                await EnvioCorreo.Enviar(correoDestino, "Heladeria La FMO - Notificacion", cuerpo);

                NotificacionDAO.MarcarEnviada(notificacion.IdNotificacion);
            }
        }
        // mapea el tipo de notificacion guardado en BD al color de alerta del correo
        private static TipoAlerta ObtenerTipoAlerta(string tipo)
        {
            switch (tipo)
            {
                case "bajo_stock":
                case "prox_vencer":
                    return TipoAlerta.Advertencia;
                case "diferencia_liq":
                case "arqueo_inconsistente":
                    return TipoAlerta.Peligro;
                case "pedido_listo":
                    return TipoAlerta.Info;
                default:
                    return TipoAlerta.Info;
            }
        }

        // mapea el tipo de notificacion a un titulo legible para el correo
        private static string ObtenerTitulo(string tipo)
        {
            switch (tipo)
            {
                case "bajo_stock": return "Alerta: bajo stock";
                case "prox_vencer": return "Alerta: producto proximo a vencer";
                case "diferencia_liq": return "Alerta: diferencia en liquidacion";
                case "arqueo_inconsistente": return "Alerta: inconsistencia de caja";
                case "pedido_listo": return "Pedido mayorista listo para retiro";
                default: return "Notificacion del sistema";
            }
        }
    }
}
