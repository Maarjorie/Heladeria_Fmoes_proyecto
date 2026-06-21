using System;
using System.Collections.Generic;
using System.Text;
using Heladeria_FMO.Modelos;
using Heladeria_FMO.Clases_Auxiliares;
using MySql.Data.MySqlClient;
using System.Data;

namespace Heladeria_FMO.Acceso_a_datos_db
{
    public static class RutaDAO
    {
        public static bool InsertarRuta(Ruta r)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consulta ="CALL p_insertar_ruta(" +"@p_nombre," +"@p_zona," + "@p_id_responsable," + "@p_horario_inicio," + "@p_horario_fin)";

            using MySqlCommand cmd = new MySqlCommand(consulta, conn);

            cmd.Parameters.AddWithValue("@p_nombre", r.Nombre);
            cmd.Parameters.AddWithValue("@p_zona", r.Zona);
            cmd.Parameters.AddWithValue("@p_id_responsable", r.IdResponsable);
            cmd.Parameters.AddWithValue("@p_horario_inicio", r.HorarioInicio);
            cmd.Parameters.AddWithValue("@p_horario_fin", r.HorarioFin);

            int resultado = cmd.ExecuteNonQuery();
            return resultado > 0;
        }

        public static bool EditarRuta(Ruta r)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consulta ="CALL p_editar_ruta(" +"@p_id_ruta," +"@p_nombre," +"@p_zona," +"@p_id_responsable," + "@p_horario_inicio," +"@p_horario_fin)";

            using MySqlCommand cmd = new MySqlCommand(consulta, conn);

            cmd.Parameters.AddWithValue("@p_id_ruta", r.IdRuta);
            cmd.Parameters.AddWithValue("@p_nombre", r.Nombre);
            cmd.Parameters.AddWithValue("@p_zona", r.Zona);
            cmd.Parameters.AddWithValue("@p_id_responsable", r.IdResponsable);
            cmd.Parameters.AddWithValue("@p_horario_inicio", r.HorarioInicio);
            cmd.Parameters.AddWithValue("@p_horario_fin", r.HorarioFin);

            int resultado = cmd.ExecuteNonQuery();
            return resultado > 0;
        }

        public static bool CambiarEstadoRuta(Ruta r)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consulta = "CALL p_cambiar_estado_ruta(" +"@p_id_ruta," +"@p_activo)";

            using MySqlCommand cmd = new MySqlCommand(consulta, conn);

            cmd.Parameters.AddWithValue("@p_id_ruta", r.IdRuta);
            cmd.Parameters.AddWithValue("@p_activo", r.Activo);

            int resultado = cmd.ExecuteNonQuery();
            return resultado > 0;
        }

        public static DataTable ListarRutas()
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consulta = "CALL p_listar_rutas()";

            using MySqlCommand cmd = new MySqlCommand(consulta, conn);
            using MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }

        public static DataTable BuscarRutas(string busqueda)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consulta = "CALL p_buscar_rutas(@p_busqueda)";

            using MySqlCommand cmd = new MySqlCommand(consulta, conn);

            cmd.Parameters.AddWithValue("@p_busqueda", busqueda);

            using MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }
    }
}
