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
    }
}
