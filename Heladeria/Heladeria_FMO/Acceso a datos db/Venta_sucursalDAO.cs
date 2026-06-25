using Heladeria_FMO.Clases_Auxiliares;
using Heladeria_FMO.Modelos;
using MySql.Data.MySqlClient;
using System.Data;

namespace Heladeria_FMO.Acceso_a_datos_db
{
    public static class Venta_sucursalDAO
    {
        //Recupera de la clase venta sucursal sus apartados para agregarlos a la base de datos
        public static int RegistrarVentaSucursal(Venta_sucursal v) 
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consultaSql = "CALL p_registrar_venta_sucursal(" + "@p_id_caja," + "@p_id_cajero," + "@p_descuento," + "@p_id_venta)";

            using MySqlCommand cmd = new MySqlCommand(consultaSql, conn);

            cmd.Parameters.AddWithValue("@p_id_caja", v.IdCaja);
            cmd.Parameters.AddWithValue("@p_id_cajero", v.IdCajero);
            cmd.Parameters.AddWithValue("@p_descuento", v.Descuento);

            MySqlParameter parametroSalida = new MySqlParameter("@p_id_venta", MySqlDbType.Int32);
            parametroSalida.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(parametroSalida);

            cmd.ExecuteNonQuery();

            return Convert.ToInt32(parametroSalida.Value);
        }

        // Registra un descuento autorizado por un supervisor sobre una venta ya
        // creada: actualiza descuento, recalcula total y guarda quién autorizó.
        public static bool RegistrarDescuentoAutorizado(int idVenta, decimal descuento, int idAutorizadoPor)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consultaSql = "CALL p_registrar_descuento_venta(" +
                "@p_id_venta," +
                "@p_descuento," +
                "@p_id_autorizado_por)";

            using MySqlCommand cmd = new MySqlCommand(consultaSql, conn);
            cmd.Parameters.AddWithValue("@p_id_venta", idVenta);
            cmd.Parameters.AddWithValue("@p_descuento", descuento);
            cmd.Parameters.AddWithValue("@p_id_autorizado_por", idAutorizadoPor);

            return cmd.ExecuteNonQuery() > 0;
        }

        //Si el cliente desea anular la solicitud de compra se añade un mensaje de motivo
        public static bool AnularVentaSucursal(Venta_sucursal v, int idUsuario, string motivo)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consultaSql =
                "CALL p_anular_venta_sucursal(" +
                "@p_id_venta," +
                "@p_id_usuario," +
                "@p_motivo)";

            using MySqlCommand cmd = new MySqlCommand(consultaSql, conn);

            cmd.Parameters.AddWithValue("@p_id_venta", v.IdVenta);
            cmd.Parameters.AddWithValue("@p_id_usuario", idUsuario);
            cmd.Parameters.AddWithValue("@p_motivo", motivo);

            int resultado = cmd.ExecuteNonQuery();
            return resultado > 0;
        }
        //Muestra todas las ventas en sucursal
        public static DataTable ListarVentasSucursal()
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consultaSql = "CALL p_listar_ventas_sucursal()";

            using MySqlCommand cmd = new MySqlCommand(consultaSql, conn);
            using MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }
    }
}      