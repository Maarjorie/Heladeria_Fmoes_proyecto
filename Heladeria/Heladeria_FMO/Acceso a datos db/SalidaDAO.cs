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
        public static bool RegistrarSalida(Salida_Ruta s)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consultaSql = "@p_registrar_salida_ruta(" + "@p_id_vendedor" + "@p_id_ruta" + "@p_id_usuario" + "@p_fecha" + "@p_hora_salida" + "@p_vehiculo" + "@p_comision" + "@p_id_salida)";

            using MySqlCommand cmd = new MySqlCommand(consultaSql, conn);

            cmd.Parameters.AddWithValue("@p_id_vendedor", s.IdVendedor);
            cmd.Parameters.AddWithValue("@p_id_ruta", s.IdRuta);
            cmd.Parameters.AddWithValue("@p_id_usuario", s.IdUsuario);
            cmd.Parameters.AddWithValue("@p_fecha", s.Fecha);
            cmd.Parameters.AddWithValue("@p_hora_salida", s.HoraSalida);
            cmd.Parameters.AddWithValue("@p_vehiculo", s.Vehiculo);
            cmd.Parameters.AddWithValue("@p_comision", s.Comision);
            cmd.Parameters.AddWithValue("@p_id_salida", s.HoraSalida);

            int resultado = cmd.ExecuteNonQuery();
            return resultado > 0;

        }
    }
}
