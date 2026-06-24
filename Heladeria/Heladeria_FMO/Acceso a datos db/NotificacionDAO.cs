using Heladeria_FMO.Clases_Auxiliares;
using Heladeria_FMO.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heladeria_FMO.Acceso_a_datos_db
{
    public static class NotificacionDAO
    {
        // Guarda una notificacion en la base de datos
        public static bool InsertarNotificacion(string tipo, int referenciaId, string mensaje)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consulta = "CALL p_insertar_notificacion(" +
                "@p_tipo," +
                "@p_referencia_id," +
                "@p_mensaje)";

            using MySqlCommand cmd = new MySqlCommand(consulta, conn);
            cmd.Parameters.AddWithValue("@p_tipo", tipo);
            cmd.Parameters.AddWithValue("@p_referencia_id", referenciaId);
            cmd.Parameters.AddWithValue("@p_mensaje", mensaje);

            int resultado = cmd.ExecuteNonQuery();
            return resultado > 0;
        }

        // Indica si ya existe una notificacion del mismo tipo y referencia dentro de
        // las ultimas 'horas' (para evitar duplicar alertas en la deteccion automatica).
        public static bool ExisteReciente(string tipo, int referenciaId, int horas)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consulta = "CALL p_existe_notificacion_reciente(" +
                "@p_tipo," +
                "@p_referencia_id," +
                "@p_horas)";

            using MySqlCommand cmd = new MySqlCommand(consulta, conn);
            cmd.Parameters.AddWithValue("@p_tipo", tipo);
            cmd.Parameters.AddWithValue("@p_referencia_id", referenciaId);
            cmd.Parameters.AddWithValue("@p_horas", horas);

            object scalar = cmd.ExecuteScalar();
            return scalar != null && Convert.ToInt32(scalar) > 0;
        }

        // Lista las notificaciones que todavia no se han enviado por correo (enviado = 0)
        public static List<Notificacion> ListarPendientes()
        {
            List<Notificacion> notificaciones = [];

            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consulta = "CALL p_listar_notificaciones_pendientes()";
            using MySqlCommand cmd = new MySqlCommand(consulta, conn);
            using MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Notificacion notificacion = new()
                {
                    IdNotificacion = reader.GetInt32(0),
                    Tipo = reader.GetString(1),
                    ReferenciaId = reader.GetInt32(2),
                    Mensaje = reader.GetString(3),
                    Enviado = reader.GetBoolean(4),
                    Leido = reader.GetBoolean(5),
                    FechaRegistro = reader.GetDateTime(6)
                };
                notificaciones.Add(notificacion);
            }

            return notificaciones;
        }

        // Lista las notificaciones no leidas (bandeja de autorizaciones del supervisor)
        public static List<Notificacion> ListarNoLeidas()
        {
            List<Notificacion> notificaciones = [];

            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consulta = "CALL p_listar_notificaciones_no_leidas()";
            using MySqlCommand cmd = new MySqlCommand(consulta, conn);
            using MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                notificaciones.Add(new Notificacion
                {
                    IdNotificacion = reader.GetInt32(0),
                    Tipo = reader.GetString(1),
                    ReferenciaId = reader.GetInt32(2),
                    Mensaje = reader.GetString(3),
                    Enviado = reader.GetBoolean(4),
                    Leido = reader.GetBoolean(5),
                    FechaRegistro = reader.GetDateTime(6)
                });
            }

            return notificaciones;
        }

        // Marca una notificacion como ya enviada por correo
        public static bool MarcarEnviada(int idNotificacion)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consulta = "CALL p_marcar_notificacion_enviada(" +
                "@p_id_notificacion)";

            using MySqlCommand cmd = new MySqlCommand(consulta, conn);
            cmd.Parameters.AddWithValue("@p_id_notificacion", idNotificacion);

            int resultado = cmd.ExecuteNonQuery();
            return resultado > 0;
        }

        // Marca una notificacion como leida dentro del sistema (distinto de "enviada", que es sobre el correo).
        public static bool MarcarLeida(int idNotificacion)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consulta = "CALL p_marcar_notificacion_leida(" +
                "@p_id_notificacion)";

            using MySqlCommand cmd = new MySqlCommand(consulta, conn);
            cmd.Parameters.AddWithValue("@p_id_notificacion", idNotificacion);

            int resultado = cmd.ExecuteNonQuery();
            return resultado > 0;
        }
    }
}
