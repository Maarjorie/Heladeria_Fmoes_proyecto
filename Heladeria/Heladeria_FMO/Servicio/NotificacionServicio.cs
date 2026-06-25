using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Heladeria_FMO.Acceso_a_datos_db;
using Heladeria_FMO.Modelos;
using Heladeria_FMO.Utileria;

namespace Heladeria_FMO.Servicio
{
    // Sistema de notificaciones AUTOMÁTICO: el propio sistema detecta condiciones
    // (stock bajo, productos por vencer) y registra notificaciones, luego envía por
    // correo las pendientes a administradores y supervisores. El usuario no dispara
    // notificaciones manualmente; estas se generan solas.
    //
    // (Las inconsistencias de arqueo se registran en el momento del arqueo desde
    //  CajaServicio; aquí solo se detectan las condiciones que dependen del tiempo
    //  o del estado del inventario y se envían todas las pendientes.)
    public static class NotificacionServicio
    {
        // Ventana para no repetir la misma alerta (tipo + referencia) seguido.
        private const int VentanaDedupHoras = 24;
        private const int DiasPorVencer = 5;

        // Ejecuta el ciclo completo en segundo plano: detectar y enviar.
        public static Task ProcesarAsync()
        {
            return Task.Run(async () =>
            {
                try
                {
                    DetectarEventos();
                    await EnviarPendientesAsync();
                }
                catch (Exception)
                {
                    // El proceso automático nunca debe interrumpir la aplicación.
                }
            });
        }

        // ───────────────────────── Bandeja (no leídas) ─────────────────────────
        // Notificaciones no leídas, para la pantalla de Autorizaciones.
        public static List<Notificacion> ListarNoLeidas() => NotificacionDAO.ListarNoLeidas();

        // Marca una notificación como leída dentro del sistema.
        public static bool MarcarLeida(int idNotificacion) => NotificacionDAO.MarcarLeida(idNotificacion);

        // ──────────── Eventos de negocio (registran y despachan envío) ────────────
        // Los siguientes métodos los invocan los servicios cuando ocurre el evento;
        // registran la notificación y disparan el envío inmediato en segundo plano
        // (sin esperar al temporizador periódico).

        // Diferencia detectada en un arqueo de caja.
        public static void NotificarArqueoInconsistente(int idCaja, decimal diferencia)
        {
            string mensaje = $"El arqueo de la caja #{idCaja} presenta una diferencia de {diferencia:0.00}.";
            RegistrarYDespachar("arqueo_inconsistente", idCaja, mensaje);
        }

        // Diferencia entre lo esperado y lo entregado en una liquidación de ruta.
        public static void NotificarDiferenciaLiquidacion(int idLiquidacion, decimal diferencia)
        {
            string mensaje = $"La liquidación #{idLiquidacion} presenta una diferencia de {diferencia:0.00}.";
            RegistrarYDespachar("diferencia_liq", idLiquidacion, mensaje);
        }

        // Pedido mayorista confirmado y listo para retiro (incluye su código).
        public static void NotificarPedidoListo(int idPedido, string codigoRetiro)
        {
            string codigo = string.IsNullOrWhiteSpace(codigoRetiro) ? "(sin código)" : codigoRetiro;
            string mensaje = $"El pedido mayorista #{idPedido} está listo para retiro. Código: {codigo}.";
            RegistrarYDespachar("pedido_listo", idPedido, mensaje);
        }

        // Registra la notificación (evitando repetir la misma seguido) y dispara el
        // envío en segundo plano. Nunca propaga errores al flujo de negocio.
        private static void RegistrarYDespachar(string tipo, int idReferencia, string mensaje)
        {
            try
            {
                if (!NotificacionDAO.ExisteReciente(tipo, idReferencia, VentanaDedupHoras))
                    NotificacionDAO.InsertarNotificacion(tipo, idReferencia, mensaje);
            }
            catch (Exception)
            {
                // Registrar la alerta no debe interrumpir la operación principal.
            }

            _ = ProcesarAsync();
        }

        // ───────────────────────── Detección ─────────────────────────
        private static void DetectarEventos()
        {
            DetectarStockBajo();
            DetectarPorVencer();
        }

        private static void DetectarStockBajo()
        {
            foreach (var p in ProductoDAO.ProductoBajoStock())
            {
                if (NotificacionDAO.ExisteReciente("bajo_stock", p.IdProducto, VentanaDedupHoras))
                    continue;

                string mensaje = $"El producto \"{p.Nombre}\" tiene stock bajo " +
                                 $"({p.StockActual} u, mínimo {p.StockMinimo}).";
                NotificacionDAO.InsertarNotificacion("bajo_stock", p.IdProducto, mensaje);
            }
        }

        private static void DetectarPorVencer()
        {
            foreach (var p in ProductoDAO.ProductosPorVencer(DiasPorVencer))
            {
                if (NotificacionDAO.ExisteReciente("prox_vencer", p.IdProducto, VentanaDedupHoras))
                    continue;

                string mensaje = $"El producto \"{p.Nombre}\" vence el " +
                                 $"{p.FechaVencimiento:yyyy-MM-dd}.";
                NotificacionDAO.InsertarNotificacion("prox_vencer", p.IdProducto, mensaje);
            }
        }

        // ───────────────────────── Envío por correo ─────────────────────────
        private static async Task EnviarPendientesAsync()
        {
            List<Notificacion> pendientes = NotificacionDAO.ListarPendientes();
            if (pendientes.Count == 0) return;

            List<string> destinatarios = ObtenerDestinatarios();
            if (destinatarios.Count == 0) return;

            foreach (var n in pendientes)
            {
                (TipoAlerta alerta, string titulo) = Mapear(n.Tipo);

                var datos = new Dictionary<string, string>
                {
                    ["Tipo"] = titulo,
                    ["Registrado"] = n.FechaRegistro.ToString("yyyy-MM-dd HH:mm")
                };
                string html = PlantillaCorreo.Generar(alerta, titulo, n.Mensaje, datos);

                bool enviado = false;
                foreach (var correo in destinatarios)
                {
                    try
                    {
                        await EnvioCorreo.Enviar(correo, $"Helados FMO · {titulo}", html);
                        enviado = true;
                    }
                    catch (Exception)
                    {
                        // Si falla un destinatario, se intenta con los demás.
                    }
                }

                if (enviado)
                    NotificacionDAO.MarcarEnviada(n.IdNotificacion);
            }
        }

        // Administradores y supervisores activos con correo.
        private static List<string> ObtenerDestinatarios()
        {
            return UsuarioServicio.ListarUsuariosActivos()
                .Where(u => !string.IsNullOrWhiteSpace(u.Correo) && EsResponsable(u.NombreRol))
                .Select(u => u.Correo)
                .Distinct()
                .ToList();
        }

        private static bool EsResponsable(string nombreRol)
        {
            if (string.IsNullOrEmpty(nombreRol)) return false;
            return nombreRol.IndexOf("admin", StringComparison.OrdinalIgnoreCase) >= 0
                || nombreRol.IndexOf("supervis", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        // Mapea el tipo de notificación a estilo y título del correo.
        private static (TipoAlerta, string) Mapear(string tipo) => tipo switch
        {
            "bajo_stock" => (TipoAlerta.Advertencia, "Stock bajo"),
            "prox_vencer" => (TipoAlerta.Advertencia, "Producto por vencer"),
            "arqueo_inconsistente" => (TipoAlerta.Peligro, "Arqueo con diferencia"),
            "diferencia_liq" => (TipoAlerta.Peligro, "Diferencia en liquidación"),
            "pedido_listo" => (TipoAlerta.Info, "Pedido listo"),
            _ => (TipoAlerta.Info, "Notificación")
        };
    }
}
