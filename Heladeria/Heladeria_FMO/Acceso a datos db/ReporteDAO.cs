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

        // ───────── KPIs del dashboard (un valor escalar por fecha) ─────────
        private static decimal EscalarDecimal(string proc, DateTime fecha)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            using MySqlCommand cmd = new MySqlCommand($"CALL {proc}(@p_fecha)", conn);
            cmd.Parameters.AddWithValue("@p_fecha", fecha.Date);

            object r = cmd.ExecuteScalar();
            return (r == null || r == DBNull.Value) ? 0m : Convert.ToDecimal(r);
        }

        public static decimal KpiUtilidad(DateTime fecha) => EscalarDecimal("p_kpi_utilidad_dia", fecha);
        public static decimal KpiComisiones(DateTime fecha) => EscalarDecimal("p_kpi_comisiones_dia", fecha);
        public static decimal KpiEgresosCaja(DateTime fecha) => EscalarDecimal("p_kpi_egresos_caja_dia", fecha);
        public static decimal KpiVentasMayoristas(DateTime fecha) => EscalarDecimal("p_kpi_ventas_mayoristas_dia", fecha);
        public static decimal KpiVentasPorRuta(DateTime fecha) => EscalarDecimal("p_kpi_ventas_por_ruta_dia", fecha);

        // Producto más vendido del día: (nombre, unidades). Vacío si no hay ventas.
        public static (string nombre, int unidades) KpiProductoTop(DateTime fecha)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            using MySqlCommand cmd = new MySqlCommand("CALL p_kpi_producto_top_dia(@p_fecha)", conn);
            cmd.Parameters.AddWithValue("@p_fecha", fecha.Date);

            using MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string nombre = reader.IsDBNull(0) ? "—" : reader.GetString(0);
                int unidades = reader.IsDBNull(1) ? 0 : Convert.ToInt32(reader.GetValue(1));
                return (nombre, unidades);
            }
            return ("—", 0);
        }
    }
}
