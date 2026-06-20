using Heladeria_FMO.Modelos;
using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using Heladeria_FMO.Clases_Auxiliares;

namespace Heladeria_FMO.Acceso_a_datos_db
{
    public static class RolDAO
    {
        // Devuelve todos los roles activos
        public static List<Rol> ObtenerRoles()
        {
            List<Rol> roles = [];
            
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consulta = "CALL p_listar_roles()";

            using MySqlCommand cmd = new MySqlCommand(consulta, conn);
            using MySqlDataReader reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                Rol rol = new()
                {
                    Id_Rol = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    Descipcion = reader.GetString(2),
                    Activo = reader.GetBoolean(3)
                };
                roles.Add(rol);
            }
            return roles;
        }
    }
}
