using System;
using System.Data;
using Heladeria_FMO.Clases_Auxiliares;
using MySql.Data.MySqlClient;

namespace Heladeria_FMO.Acceso_a_datos_db
{
    // Acceso a los procedimientos de reportes (todos devuelven un DataTable).
    public static class ReporteDAO
    {
        private static DataTable EjecutarRango(string proc, DateTime inicio, DateTime fin)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            using MySqlCommand cmd = new MySqlCommand($"CALL {proc}(@p_fecha_inicio, @p_fecha_fin)", conn);
            cmd.Parameters.AddWithValue("@p_fecha_inicio", inicio.Date);
            cmd.Parameters.AddWithValue("@p_fecha_fin", fin.Date);

            using MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataTable VentasDia(DateTime fecha)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            using MySqlCommand cmd = new MySqlCommand("CALL p_reporte_ventas_dia(@p_fecha)", conn);
            cmd.Parameters.AddWithValue("@p_fecha", fecha.Date);

            using MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public static DataTable Caja(DateTime inicio, DateTime fin) => EjecutarRango("p_reporte_caja", inicio, fin);
        public static DataTable VentasPorRuta(DateTime inicio, DateTime fin) => EjecutarRango("p_reporte_ventas_por_ruta", inicio, fin);
        public static DataTable ProductosMasVendidos(DateTime inicio, DateTime fin) => EjecutarRango("p_reporte_productos_mas_vendidos", inicio, fin);
        public static DataTable ProductosDanados(DateTime inicio, DateTime fin) => EjecutarRango("p_reporte_productos_danados", inicio, fin);
        public static DataTable ComisionesVendedores(DateTime inicio, DateTime fin) => EjecutarRango("p_reporte_comisiones_vendedores", inicio, fin);
        public static DataTable RankingVendedores(DateTime inicio, DateTime fin) => EjecutarRango("p_reporte_ranking_vendedores", inicio, fin);
        public static DataTable RentabilidadRuta(DateTime inicio, DateTime fin) => EjecutarRango("p_reporte_rentabilidad_ruta", inicio, fin);
    }
}
