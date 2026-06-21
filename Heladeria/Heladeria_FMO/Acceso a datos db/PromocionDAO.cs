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
    public static class PromocionDao
    {
        // Inserta una nueva promocion
        public static bool InsertarPromocion(Promocion promocion)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();
            string consulta = "CALL p_insertar_promocion(" +
                "@p_id_producto," +
                "@p_id_categoria," +
                "@p_nombre," +
                "@p_descripcion," +
                "@p_tipo_descuento," +
                "@p_valor_descuento," +
                "@p_fecha_inicio," +
                "@p_fecha_fin)";

            using MySqlCommand cmd = new MySqlCommand(consulta, conn);
            cmd.Parameters.AddWithValue("@p_id_producto", promocion.IdProducto);
            cmd.Parameters.AddWithValue("@p_id_categoria", promocion.IdCategoria);
            cmd.Parameters.AddWithValue("@p_nombre", promocion.Nombre);
            cmd.Parameters.AddWithValue("@p_descripcion", promocion.Descripcion);
            cmd.Parameters.AddWithValue("@p_tipo_descuento", promocion.TipoDescuento);
            cmd.Parameters.AddWithValue("@p_valor_descuento", promocion.ValorDescuento);
            cmd.Parameters.AddWithValue("@p_fecha_inicio", promocion.FechaInicio);
            cmd.Parameters.AddWithValue("@p_fecha_fin", promocion.FechaFin);

            int resultado = cmd.ExecuteNonQuery();

            return resultado > 0;
        }

        // Edita datos generalesd de una promo existente
        public static bool EditarPromocion(Promocion promocion)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();
            string consulta = "CALL p_editar_promocion(" +
                "@p_id_promocion," +
                "@p_id_producto," +
                "@p_id_categoria," +
                "@p_nombre," +
                "@p_descripcion," +
                "@p_tipo_descuento," +
                "@p_valor_descuento," +
                "@p_fecha_inicio," +
                "@p_fecha_fin)";

            using MySqlCommand cmd = new MySqlCommand(consulta, conn);
            cmd.Parameters.AddWithValue("@p_id_promocion", promocion.IdPromocion);
            cmd.Parameters.AddWithValue("@p_id_producto", promocion.IdProducto);
            cmd.Parameters.AddWithValue("@p_id_categoria", promocion.IdCategoria);
            cmd.Parameters.AddWithValue("@p_nombre", promocion.Nombre);
            cmd.Parameters.AddWithValue("@p_descripcion", promocion.Descripcion);
            cmd.Parameters.AddWithValue("@p_tipo_descuento", promocion.TipoDescuento);
            cmd.Parameters.AddWithValue("@p_valor_descuento", promocion.ValorDescuento);
            cmd.Parameters.AddWithValue("@p_fecha_inicio", promocion.FechaInicio);
            cmd.Parameters.AddWithValue("@p_fecha_fin", promocion.FechaFin);

            int resultado = cmd.ExecuteNonQuery();

            return resultado > 0;
        }

        // Activa o desactiva un promo
        public static bool CambiarEstadoPromocion(int idPromocion, bool activo)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();
            string consulta = "CALL p_cambiar_estado_promocion(" +
                "@p_id_promocion," +
                "@p_activo)";

            using MySqlCommand cmd = new MySqlCommand(consulta, conn);
            cmd.Parameters.AddWithValue("@p_id_promocion", idPromocion);
            cmd.Parameters.AddWithValue("@p_activo", activo);

            int resultado = cmd.ExecuteNonQuery();

            return resultado > 0; 
        }

        // Enlista todas las promociones acticas
        public static List<Promocion> ListarPromocion()
        {
            List<Promocion> promociones = [];

            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();
            string consulta = "CALL p_listar_promociones()";

            using MySqlCommand cmd = new MySqlCommand(consulta, conn);
            using MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Promocion promocion = new()
                {
                    IdPromocion = reader.GetInt32(0),
                    IdProducto = reader.IsDBNull(1) ? null : reader.GetInt32(1),
                    IdCategoria = reader.IsDBNull(2) ? null : reader.GetInt32(2),
                    Nombre = reader.GetString(3),
                    Descripcion = reader.GetString(4),
                    TipoDescuento = reader.GetString(5),
                    ValorDescuento = reader.GetDecimal(6),
                    FechaInicio = reader.GetDateTime(7),
                    FechaFin = reader.GetDateTime(8),
                    Activo = reader.GetBoolean(9),
                    FechaRegistro = reader.GetDateTime(10)
                };
                promociones.Add(promocion);
            }

            return promociones;
        }
    }
}
