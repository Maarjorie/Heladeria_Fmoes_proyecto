using Heladeria_FMO.Clases_Auxiliares;
using Heladeria_FMO.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heladeria_FMO.Acceso_a_datos_db
{
    public static class CategoriaDao
    {
        // Lista las categorias activas
        public static List<Categoria> ListarCategorias()
        {
            List<Categoria> categorias = [];

            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consulta = "CALL p_listar_categorias()";

            using MySqlCommand cmd = new MySqlCommand(consulta, conn);
            using MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Categoria categoria = new() 
                {
                    IdCategoria = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    Descripcion = reader.IsDBNull(2) ? null : reader.GetString(2),
                    Activo = reader.GetBoolean(3)
                };
                categorias.Add(categoria);
            }
            return categorias;
        }
    }
}
