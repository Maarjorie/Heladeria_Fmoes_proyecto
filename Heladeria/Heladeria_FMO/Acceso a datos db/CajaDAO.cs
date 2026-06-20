using Heladeria_FMO.Clases_Auxiliares;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Heladeria_FMO.Acceso_a_datos_db
{
    public static class CajaDAO
    {
        // Abre una nueva caja y devuelve el id_caja generado
        public static int AbrirCaja(int idCajero, decimal fondoInicial)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consulta = "CALL p_abrir_caja(" +
                "@p_id_cajero," +
                "@p_fondo_inicial," +
                "@p_id_caja)";

            using MySqlCommand cmd = new MySqlCommand(consulta, conn);
            cmd.Parameters.AddWithValue("@p_id_cajero", idCajero);
            cmd.Parameters.AddWithValue("@p_fondo_inicial", fondoInicial);

            // parametro de salida: el procedimiento nos devuelve el id_caja generado
            MySqlParameter outIdCaja = new MySqlParameter("@p_id_caja", MySqlDbType.Int32)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(outIdCaja);

            cmd.ExecuteNonQuery();

            return Convert.ToInt32(outIdCaja.Value);
        }

        // Cierra una caja, calculando total vendido y diferencia contra el monto contado por el cajero.
        public static bool CerrarCaja(int idCaja, decimal montoContado)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consulta = "CALL p_cerrar_caja(" +
                "@p_id_caja," +
                "@p_monto_contado)";

            using MySqlCommand cmd = new MySqlCommand(consulta, conn);
            cmd.Parameters.AddWithValue("@p_id_caja", idCaja);
            cmd.Parameters.AddWithValue("@p_monto_contado", montoContado);

            int resultado = cmd.ExecuteNonQuery();
            return resultado > 0;
        }
    }
}
