using System;
using System.Collections.Generic;
using System.Text;
using Heladeria_FMO.Clases_Auxiliares;
using Heladeria_FMO.Modelos;
using Heladeria_FMO.Utileria;
using MySql.Data.MySqlClient;

namespace Heladeria_FMO.Acceso_a_datos_db
{
    public class UsuarioDao
    {
        public static Usuario ObtenerUsuarioPorNombre(string username)
        {
            using MySqlConnection conexion = Conexion.ConexionDb();//hacemos conexion con la base de datos
            conexion.Open();

            string query = "CALL p_login_usuario(@p_usuario)";//llamamos el procedimiento

            //configuramos el comando y asignamos el parametro
            using MySqlCommand cmd = new MySqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@p_usuario", username);//

            using MySqlDataReader reader = cmd.ExecuteReader();//ejecuta la consulta y lee los resultados

            if (reader.Read())
            {
                //mapeamos los datos del lector a la entidad Usuario
                Usuario usuario = new()
                {
                    id_Usuario = reader.GetInt32(0),
                    id_rol = reader.GetInt32(1),
                    Nombre = reader.GetString(2),
                    Usuario_ = reader.GetString(3),
                    Contrasenia_hash = reader.GetString(4),
                    Contrasenia_salt = reader.GetString(5),
                    Correo = reader.GetString(6),
                    Activo = reader.GetBoolean(7),
                };

                return usuario;
            }
            //retorna null en caso que no se encuentre ningun registro que coincida
            return null;
        }

        // Inserta un nuevo usuario
        public static bool InsertarUsuario(Usuario usuario)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consulta = "CALL p_insertar_usuario(" +
                "@p_id_rol," +
                "@p_nombre," +
                "@p_usuario," +
                "@p_contrasena_hash," +
                "@p_contrasena_salt," +
                "@p_correo)";

            using MySqlCommand cmd = new MySqlCommand(consulta, conn);
            cmd.Parameters.AddWithValue("@p_id_rol", usuario.id_rol);
            cmd.Parameters.AddWithValue("@p_nombre", usuario.Nombre);
            cmd.Parameters.AddWithValue("@p_usuario", usuario.Usuario_);
            cmd.Parameters.AddWithValue("@p_contrasena_hash", usuario.Contrasenia_hash);
            cmd.Parameters.AddWithValue("@p_contrasena_salt", usuario.Contrasenia_salt);
            cmd.Parameters.AddWithValue("@p_correo", usuario.Correo);

            int resultado = cmd.ExecuteNonQuery();
            return resultado > 0;
        }

        // Edita datos generales del usuario.
        public static bool EditarUsuario(Usuario usuario)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consulta = "CALL p_editar_usuario(" +
                "@p_id_usuario," +
                "@p_nombre," +
                "@p_correo," +
                "@p_id_rol)";

            using MySqlCommand cmd = new MySqlCommand(consulta, conn);
            cmd.Parameters.AddWithValue("@p_id_usuario", usuario.id_Usuario);
            cmd.Parameters.AddWithValue("@p_nombre", usuario.Nombre);
            cmd.Parameters.AddWithValue("@p_correo", usuario.Correo);
            cmd.Parameters.AddWithValue("@p_id_rol", usuario.id_rol);

            int resultado = cmd.ExecuteNonQuery();
            return resultado > 0;
        }

        public static bool CambiarEstadoUsuario(int idUsuario, bool activo)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consulta = "CALL p_cambiar_estado_usuario(" +
                "@p_id_usuario," +
                "@p_activo)";

            using MySqlCommand cmd = new MySqlCommand(consulta, conn);
            cmd.Parameters.AddWithValue("@p_id_usuario", idUsuario);
            cmd.Parameters.AddWithValue("@p_activo", activo);

            int resultado = cmd.ExecuteNonQuery();
            return resultado > 0;
        }

        // Lista TODOS los usuarios (activos e inactivos) con el nombre de su rol
        public static List<Usuario> ListarUsuarios()
        {
            List<Usuario> usuarios = [];

            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consulta = "CALL p_listar_usuarios()";
            using MySqlCommand cmd = new MySqlCommand(consulta, conn);
            using MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                usuarios.Add(new Usuario
                {
                    id_Usuario = reader.GetInt32(0),
                    Usuario_ = reader.GetString(1),
                    Nombre = reader.GetString(2),
                    Correo = reader.IsDBNull(3) ? "" : reader.GetString(3),
                    NombreRol = reader.GetString(4),
                    id_rol = reader.GetInt32(5),
                    Activo = reader.GetBoolean(6)
                });
            }

            return usuarios;
        }

        // Lista los usuarios activos con el nombre de su rol
        public static List<Usuario> ListarUsuariosActivos()
        {
            List<Usuario> usuarios = [];

            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consulta = "CALL p_listar_usuarios_activos()";
            using MySqlCommand cmd = new MySqlCommand(consulta, conn);
            using MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Usuario usuario = new()
                {
                    id_Usuario = reader.GetInt32(0),
                    Usuario_ = reader.GetString(1),
                    Nombre = reader.GetString(2),
                    Correo = reader.GetString(3),
                    NombreRol = reader.GetString(4)
                };
                usuarios.Add(usuario);
            }

            return usuarios;
        }
    }
}
