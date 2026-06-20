using Heladeria_FMO.Clases_Auxiliares;
using Heladeria_FMO.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Heladeria_FMO.Acceso_a_datos_db
{
    public static class Arqueo_cajaDAO
    {
        // Registra un arqueo de caja
        public static bool InsertarArqueo(int idCaja, int idRealizadoPor, decimal montoEsperado, decimal montoContado)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consulta = "CALL p_insertar_arqueo_caja(" +
                "@p_id_caja," +
                "@p_id_realizado_por," +
                "@p_monto_esperado," +
                "@p_monto_contado)";

            using MySqlCommand cmd = new MySqlCommand(consulta, conn);
            cmd.Parameters.AddWithValue("@p_id_caja", idCaja);
            cmd.Parameters.AddWithValue("@p_id_realizado_por", idRealizadoPor);
            cmd.Parameters.AddWithValue("@p_monto_esperado", montoEsperado);
            cmd.Parameters.AddWithValue("@p_monto_contado", montoContado);

            int resultado = cmd.ExecuteNonQuery();
            return resultado > 0;
        }

        // Lista los arqueos realizados sobre una caja especifica.
        public static List<Arqueo_caja> ListarPorCaja(int idCaja)
        {
            List<Arqueo_caja> arqueos = [];

            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consulta = "CALL p_listar_arqueos_caja(" +
                "@p_id_caja)";

            using MySqlCommand cmd = new MySqlCommand(consulta, conn);
            cmd.Parameters.AddWithValue("@p_id_caja", idCaja);
            using MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Arqueo_caja arqueo = new()
                {
                    IdArqueo = reader.GetInt32(0),
                    IdCaja = reader.GetInt32(1),
                    IdRealizadoPor = reader.GetInt32(2),
                    MontoEsperado = reader.GetDecimal(3),
                    MontoContado = reader.GetDecimal(4),
                    Diferencia = reader.IsDBNull(5) ? null : reader.GetDecimal(5),
                    FechaRegistro = reader.GetDateTime(6)
                };
                arqueos.Add(arqueo);
            }

            return arqueos;
        }
    }
}
