using System;
using System.Collections.Generic;
using System.Data;
using Heladeria_FMO.Acceso_a_datos_db;
using Heladeria_FMO.Modelos;

namespace Heladeria_FMO.Servicio
{
    // Lógica de promociones: alta (queda 'pendiente'), listado y flujo de
    // aprobación (aprobar / rechazar).
    public static class PromocionServicio
    {
        public static bool RegistrarPromocion(Promocion p)
        {
            if (string.IsNullOrWhiteSpace(p.Nombre))
                throw new Exception("El nombre de la promoción es obligatorio.");

            bool tieneProducto = p.IdProducto.HasValue && p.IdProducto > 0;
            bool tieneCategoria = p.IdCategoria.HasValue && p.IdCategoria > 0;
            if (tieneProducto == tieneCategoria)
                throw new Exception("Selecciona un producto O una categoría (solo uno).");

            if (p.ValorDescuento <= 0)
                throw new Exception("El valor del descuento debe ser mayor que cero.");

            if (p.TipoDescuento == "porcentaje" && p.ValorDescuento > 100)
                throw new Exception("El porcentaje no puede ser mayor que 100.");

            if (p.FechaInicio > p.FechaFin)
                throw new Exception("La fecha de inicio no puede ser mayor que la de fin.");

            return PromocionDao.InsertarPromocion(p);
        }

        public static List<Promocion> ListarPromociones() => PromocionDao.ListarPromocion();

        public static DataTable ListarPendientes() => PromocionDao.ListarPendientes();

        public static bool AprobarPromocion(int idPromocion, int idAprobadoPor)
        {
            if (idAprobadoPor <= 0) throw new Exception("No hay un usuario en sesión.");
            return PromocionDao.AprobarPromocion(idPromocion, idAprobadoPor);
        }

        public static bool RechazarPromocion(int idPromocion, int idAprobadoPor)
        {
            if (idAprobadoPor <= 0) throw new Exception("No hay un usuario en sesión.");
            return PromocionDao.RechazarPromocion(idPromocion, idAprobadoPor);
        }

        public static bool CambiarEstado(int idPromocion, bool activo) => PromocionDao.CambiarEstadoPromocion(idPromocion, activo);
    }
}
