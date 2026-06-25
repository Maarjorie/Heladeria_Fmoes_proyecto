using System;
using System.Data;
using Heladeria_FMO.Acceso_a_datos_db;

namespace Heladeria_FMO.Servicio
{
    // Capa de servicio para los reportes (pasa por aquí en lugar de llamar al DAO
    // directamente desde la interfaz).
    public static class ReporteServicio
    {
        public static DataTable VentasDia(DateTime fecha) => ReporteDAO.VentasDia(fecha);
        public static DataTable Caja(DateTime inicio, DateTime fin) => ReporteDAO.Caja(inicio, fin);
        public static DataTable VentasPorRuta(DateTime inicio, DateTime fin) => ReporteDAO.VentasPorRuta(inicio, fin);
        public static DataTable ProductosMasVendidos(DateTime inicio, DateTime fin) => ReporteDAO.ProductosMasVendidos(inicio, fin);
        public static DataTable ProductosDanados(DateTime inicio, DateTime fin) => ReporteDAO.ProductosDanados(inicio, fin);
        public static DataTable ComisionesVendedores(DateTime inicio, DateTime fin) => ReporteDAO.ComisionesVendedores(inicio, fin);
        public static DataTable RankingVendedores(DateTime inicio, DateTime fin) => ReporteDAO.RankingVendedores(inicio, fin);
        public static DataTable RentabilidadRuta(DateTime inicio, DateTime fin) => ReporteDAO.RentabilidadRuta(inicio, fin);

        // ───────── KPIs del dashboard (por fecha) ─────────
        public static decimal UtilidadBrutaDia(DateTime fecha) => ReporteDAO.KpiUtilidad(fecha);
        public static decimal ComisionesDia(DateTime fecha) => ReporteDAO.KpiComisiones(fecha);
        public static decimal EgresosCajaDia(DateTime fecha) => ReporteDAO.KpiEgresosCaja(fecha);
        public static decimal VentasMayoristasDia(DateTime fecha) => ReporteDAO.KpiVentasMayoristas(fecha);
        public static decimal VentasPorRutaDia(DateTime fecha) => ReporteDAO.KpiVentasPorRuta(fecha);
        public static (string nombre, int unidades) ProductoTopDia(DateTime fecha) => ReporteDAO.KpiProductoTop(fecha);

        // Utilidad neta = utilidad bruta − comisiones del día − egresos de caja del día.
        public static decimal UtilidadNetaDia(DateTime fecha)
            => UtilidadBrutaDia(fecha) - ComisionesDia(fecha) - EgresosCajaDia(fecha);
    }
}
