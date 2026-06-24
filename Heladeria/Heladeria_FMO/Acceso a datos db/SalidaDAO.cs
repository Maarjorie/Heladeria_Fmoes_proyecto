using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using Heladeria_FMO.Modelos;
using Heladeria_FMO.Clases_Auxiliares;
using System.Data;


namespace Heladeria_FMO.Acceso_a_datos_db
{
    public static class SalidaDAO
    {
        public static int RegistrarSalida(Salida_Ruta s)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consultaSql = "CALL p_registrar_salida_ruta(" +
                "@p_id_vendedor," +
                "@p_id_ruta," +
                "@p_id_usuario," +
                "@p_fecha," +
                "@p_hora_salida," +
                "@p_vehiculo," +
                "@p_comision," +
                "@p_id_salida)";

            using MySqlCommand cmd = new MySqlCommand(consultaSql, conn);

            cmd.Parameters.AddWithValue("@p_id_vendedor", s.IdVendedor);
            cmd.Parameters.AddWithValue("@p_id_ruta", s.IdRuta);
            cmd.Parameters.AddWithValue("@p_id_usuario", s.IdUsuario);
            cmd.Parameters.AddWithValue("@p_fecha", s.Fecha);
            cmd.Parameters.AddWithValue("@p_hora_salida", s.HoraSalida);
            cmd.Parameters.AddWithValue("@p_vehiculo", s.Vehiculo);
            cmd.Parameters.AddWithValue("@p_comision", s.Comision);

            // Parámetro de salida: el procedimiento devuelve el id_salida generado.
            MySqlParameter parametroSalida = new MySqlParameter("@p_id_salida", MySqlDbType.Int32)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(parametroSalida);

            cmd.ExecuteNonQuery();

            return Convert.ToInt32(parametroSalida.Value);
        }

        // Lista las fichas de salida con el nombre del vendedor y de la ruta.
        public static DataTable ListarSalidasRuta()
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consultaSql = "CALL p_listar_salidas_ruta()";

            using MySqlCommand cmd = new MySqlCommand(consultaSql, conn);
            using MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }
    }
}
