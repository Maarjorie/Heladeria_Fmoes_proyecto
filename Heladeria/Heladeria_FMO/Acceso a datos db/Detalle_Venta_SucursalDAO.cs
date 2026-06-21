using System;
using System.Collections.Generic;
using System.Text;
using Heladeria_FMO.Clases_Auxiliares;
using Heladeria_FMO.Modelos;
using MySql.Data.MySqlClient;

namespace Heladeria_FMO.Acceso_a_datos_db
{
    public static class Detalle_Venta_SucursalDAO
    {
        public static bool AgregarDetalleVentaSucursal(Detalle_Venta_Sucursal d, int idUsuario)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consultaSql ="CALL p_agregar_detalle_venta_sucursal(" +"@p_id_venta," +"@p_id_producto," +"@p_cantidad," +"@p_descuento," +"@p_id_usuario)";

            using MySqlCommand cmd = new MySqlCommand(consultaSql, conn);

            cmd.Parameters.AddWithValue("@p_id_venta", d.IdVenta);
            cmd.Parameters.AddWithValue("@p_id_producto", d.IdProducto);
            cmd.Parameters.AddWithValue("@p_cantidad", d.Cantidad);
            cmd.Parameters.AddWithValue("@p_descuento", d.Descuento);
            cmd.Parameters.AddWithValue("@p_id_usuario", idUsuario);

            int resultado = cmd.ExecuteNonQuery();
            return resultado > 0;
        }
    }
}
