using Heladeria_FMO.Clases_Auxiliares;
using Heladeria_FMO.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heladeria_FMO.Acceso_a_datos_db
{
    public static class Movimiento_cajaDAO
    {
        // Registra un movimiento de caja (ingreso o egreso extraordinario)
        public static bool InsertarMovimiento(int idCaja, int idUsuario, string tipo, string concepto, decimal monto)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consulta = "CALL p_insertar_movimiento_caja(" +
                "@p_id_caja," +
                "@p_id_usuario," +
                "@p_tipo," +
                "@p_concepto," +
                "@p_monto)";

            using MySqlCommand cmd = new MySqlCommand(consulta, conn);
            cmd.Parameters.AddWithValue("@p_id_caja", idCaja);
            cmd.Parameters.AddWithValue("@p_id_usuario", idUsuario);
            cmd.Parameters.AddWithValue("@p_tipo", tipo); // "ingreso" o "egreso"
            cmd.Parameters.AddWithValue("@p_concepto", concepto);
            cmd.Parameters.AddWithValue("@p_monto", monto);

            int resultado = cmd.ExecuteNonQuery();
            return resultado > 0;
        }

        //Lista los movimientos de una caja especifica, mas recientes primero.
        public static List<Movimiento_caja> ListarPorCaja(int idCaja)
        {
            List<Movimiento_caja> movimientos = [];

            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consulta = "CALL p_listar_movimientos_caja(" +
                "@p_id_caja)";

            using MySqlCommand cmd = new MySqlCommand(consulta, conn);
            cmd.Parameters.AddWithValue("@p_id_caja", idCaja);
            using MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Movimiento_caja movimiento = new()
                {
                    IdMovimientoCaja = reader.GetInt32(0),
                    IdCaja = reader.GetInt32(1),
                    IdUsuario = reader.GetInt32(2),
                    Tipo = reader.GetString(3),
                    Concepto = reader.GetString(4),
                    Monto = reader.GetDecimal(5),
                    FechaRegistro = reader.GetDateTime(6)
                };
                movimientos.Add(movimiento);
            }

            return movimientos;
        }
    }
}
