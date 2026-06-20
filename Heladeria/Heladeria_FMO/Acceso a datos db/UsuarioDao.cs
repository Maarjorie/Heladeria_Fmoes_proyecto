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
    }
}
