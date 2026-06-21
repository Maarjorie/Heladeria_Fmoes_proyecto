using Heladeria_FMO.Clases_Auxiliares;
using Heladeria_FMO.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heladeria_FMO.Acceso_a_datos_db
{
    public static class Detalle_liquidacionDAO
    {
        //Inserta valores en detalles_liquidacion y actualiza la liquidacion que se trata en ese momento de pasar los parametro 
        public static bool AgregarDetalleLiquidacion(Detalle_liquidacion d)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consultaSql ="CALL p_agregar_detalle_liquidacion(" +"@p_id_liquidacion," +"@p_id_producto," +"@p_cantidad_vendida," +"@p_cantidad_devuelta," +"@p_cantidad_daniada)";

            using MySqlCommand cmd = new MySqlCommand(consultaSql, conn);

            cmd.Parameters.AddWithValue("@p_id_liquidacion", d.IdLiquidacion);
            cmd.Parameters.AddWithValue("@p_id_producto", d.IdProducto);
            cmd.Parameters.AddWithValue("@p_cantidad_vendida", d.CantidadVendida);
            cmd.Parameters.AddWithValue("@p_cantidad_devuelta", d.CantidadDevuelta);
            cmd.Parameters.AddWithValue("@p_cantidad_daniada", d.CantidadDañada);

            int resultado = cmd.ExecuteNonQuery();
            return resultado > 0;
        }
    }
}
