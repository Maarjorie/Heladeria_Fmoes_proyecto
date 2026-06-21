using Heladeria_FMO.Clases_Auxiliares;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heladeria_FMO.Acceso_a_datos_db
{
    public static class Movimiento_inventarioDAO
    {
        // Registra un movimiento de inventario (entrada, salida, ajuste, devolucion, dañado o vencido)
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
            cmd.Parameters.AddWithValue("@p_tipo", tipo); // entrada, salida, ajuste, devolucion, dañado, vencido
            cmd.Parameters.AddWithValue("@p_cantidad", cantidad);
            cmd.Parameters.AddWithValue("@p_observacion", observacion);

            int resultado = cmd.ExecuteNonQuery();
            return resultado > 0;
        }
    }
}
