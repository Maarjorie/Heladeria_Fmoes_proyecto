using Heladeria_FMO.Clases_Auxiliares;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Heladeria_FMO.Acceso_a_datos_db
{
    public static class Movimiento_inventarioDAO
    {
        // Registra un movimiento de inventario (entrada, salida, ajuste, devolucion, da�ado o vencido)
        public static bool RegistrarMovimiento(int idProducto, string tipo, int cantidad, string observacion)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consulta = "CALL p_registrar_movimiento_inventario(" +
                "@p_id_producto," +
                "@p_tipo," +
                "@p_cantidad," +
                "@p_observacion)";

            using MySqlCommand cmd = new MySqlCommand(consulta, conn);
            cmd.Parameters.AddWithValue("@p_id_producto", idProducto);
            cmd.Parameters.AddWithValue("@p_tipo", tipo); // entrada, salida, ajuste, devolucion, da�ado, vencido
            cmd.Parameters.AddWithValue("@p_cantidad", cantidad);
            cmd.Parameters.AddWithValue("@p_observacion", observacion);

            int resultado = cmd.ExecuteNonQuery();
            return resultado > 0;
        }

        // Solicita un ajuste de inventario: queda PENDIENTE (no toca el stock).
        // Devuelve el id del movimiento creado.
        public static int SolicitarAjuste(int idProducto, int cantidad, string observacion, int idUsuario)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consulta = "CALL p_solicitar_ajuste_inventario(" +
                "@p_id_producto," +
                "@p_cantidad," +
                "@p_observacion," +
                "@p_id_usuario," +
                "@p_id_movimiento)";

            using MySqlCommand cmd = new MySqlCommand(consulta, conn);
            cmd.Parameters.AddWithValue("@p_id_producto", idProducto);
            cmd.Parameters.AddWithValue("@p_cantidad", cantidad);
            cmd.Parameters.AddWithValue("@p_observacion", observacion);
            cmd.Parameters.AddWithValue("@p_id_usuario", idUsuario);

            MySqlParameter salida = new MySqlParameter("@p_id_movimiento", MySqlDbType.Int32)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(salida);

            cmd.ExecuteNonQuery();
            return Convert.ToInt32(salida.Value);
        }

        // Ajustes de inventario pendientes de aprobación.
        public static DataTable ListarPendientes()
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            using MySqlCommand cmd = new MySqlCommand("CALL p_listar_movimientos_inventario_pendientes()", conn);
            using MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            DataTable tabla = new DataTable();
            adapter.Fill(tabla);
            return tabla;
        }

        // Aprueba un ajuste pendiente (aplica el delta al stock).
        public static bool AprobarAjuste(int idMovimiento, int idAprobadoPor)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consulta = "CALL p_aprobar_ajuste_inventario(@p_id_movimiento,@p_id_aprobado_por)";
            using MySqlCommand cmd = new MySqlCommand(consulta, conn);
            cmd.Parameters.AddWithValue("@p_id_movimiento", idMovimiento);
            cmd.Parameters.AddWithValue("@p_id_aprobado_por", idAprobadoPor);

            return cmd.ExecuteNonQuery() >= 0;
        }

        // Rechaza un ajuste pendiente (no toca el stock).
        public static bool RechazarAjuste(int idMovimiento, int idAprobadoPor)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consulta = "CALL p_rechazar_ajuste_inventario(@p_id_movimiento,@p_id_aprobado_por)";
            using MySqlCommand cmd = new MySqlCommand(consulta, conn);
            cmd.Parameters.AddWithValue("@p_id_movimiento", idMovimiento);
            cmd.Parameters.AddWithValue("@p_id_aprobado_por", idAprobadoPor);

            return cmd.ExecuteNonQuery() > 0;
        }
    }
}
