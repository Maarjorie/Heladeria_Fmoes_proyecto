using Heladeria_FMO.Clases_Auxiliares;
using Heladeria_FMO.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heladeria_FMO.Acceso_a_datos_db
{
    public static class Detalle_pedido_mayoristaDAO
    {
        public static bool AgregarDetallePedidoMayorista(Detalle_pedido_mayorista d)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consultaSql ="CALL p_agregar_detalle_pedido_mayorista(" +"@p_id_pedido," +"@p_id_producto," +"@p_cantidad)";

            using MySqlCommand cmd = new MySqlCommand(consultaSql, conn);

            cmd.Parameters.AddWithValue("@p_id_pedido", d.IdPedido);
            cmd.Parameters.AddWithValue("@p_id_producto", d.IdProducto);
            cmd.Parameters.AddWithValue("@p_cantidad", d.Cantidad);

            int resultado = cmd.ExecuteNonQuery();

            return resultado > 0;
        }

    }
}
