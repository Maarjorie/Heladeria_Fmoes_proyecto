using Heladeria_FMO.Clases_Auxiliares;
using Heladeria_FMO.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Heladeria_FMO.Acceso_a_datos_db
{
    public static class Pedido_mayoristaDAO
    {
        //Crea en la tabla pedido mayorista obteniendo el id del cliente para referenciar el pedido
        public static int CrearPedidoMayorista(Pedido_mayorista p)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consultaSql ="CALL p_crear_pedido_mayorista(" +"@p_id_cliente," + "@p_id_atendido_por," +"@p_fecha_entrega_programada," +"@p_id_pedido)";

            using MySqlCommand cmd = new MySqlCommand(consultaSql, conn);

            cmd.Parameters.AddWithValue("@p_id_cliente", p.IdCliente);
            cmd.Parameters.AddWithValue("@p_id_atendido_por", p.IdAtendidoPor);
            cmd.Parameters.AddWithValue("@p_fecha_entrega_programada",
                (object)p.FechaEntregaProgramada ?? DBNull.Value);

            MySqlParameter parametroSalida = new MySqlParameter("@p_id_pedido", MySqlDbType.Int32);
            parametroSalida.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(parametroSalida);

            cmd.ExecuteNonQuery();

            return Convert.ToInt32(parametroSalida.Value);
        }

        // Fija subtotal/descuento/total del pedido (tras agregar el detalle).
        public static bool ActualizarTotales(int idPedido, decimal subtotal, decimal descuento, decimal total)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consultaSql = "CALL p_actualizar_totales_pedido_mayorista(" +
                "@p_id_pedido,@p_subtotal,@p_descuento,@p_total)";

            using MySqlCommand cmd = new MySqlCommand(consultaSql, conn);
            cmd.Parameters.AddWithValue("@p_id_pedido", idPedido);
            cmd.Parameters.AddWithValue("@p_subtotal", subtotal);
            cmd.Parameters.AddWithValue("@p_descuento", descuento);
            cmd.Parameters.AddWithValue("@p_total", total);

            return cmd.ExecuteNonQuery() > 0;
        }

        // Historial de pedidos de un cliente mayorista específico.
        public static DataTable ListarPedidosPorCliente(int idCliente)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            using MySqlCommand cmd = new MySqlCommand("CALL p_listar_pedidos_mayoristas_cliente(@p_id_cliente)", conn);
            cmd.Parameters.AddWithValue("@p_id_cliente", idCliente);
            using MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        //Con el id del pedido se puede confirmar el pedido para generar la venta
        public static bool ConfirmarPedidoMayorista(Pedido_mayorista p)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consultaSql ="CALL p_confirmar_pedido_mayorista(" +"@p_id_pedido)";

            using MySqlCommand cmd = new MySqlCommand(consultaSql, conn);

            cmd.Parameters.AddWithValue("@p_id_pedido", p.IdPedido);

            int resultado = cmd.ExecuteNonQuery();
            return resultado > 0;
        }

        //  Hace posible el ingreso 
        public static bool EntregarPedidoMayorista(Pedido_mayorista p)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consultaSql ="CALL p_entregar_pedido_mayorista(" +"@p_id_pedido," +"@p_id_usuario)";

            using MySqlCommand cmd = new MySqlCommand(consultaSql, conn);

            cmd.Parameters.AddWithValue("@p_id_pedido", p.IdPedido);
            cmd.Parameters.AddWithValue("@p_id_usuario", p.IdEntregadoPor);

            int resultado = cmd.ExecuteNonQuery();
            return resultado > 0;
        }
        //Rechaza (cancela) un pedido mayorista pendiente
        public static bool CancelarPedidoMayorista(int idPedido)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            using MySqlCommand cmd = new MySqlCommand("CALL p_cancelar_pedido_mayorista(@p_id_pedido)", conn);
            cmd.Parameters.AddWithValue("@p_id_pedido", idPedido);

            return cmd.ExecuteNonQuery() > 0;
        }

        //Muestra los pedidos de la tabla pedidos_mayoristas
        public static DataTable ListarPedidosMayoristas()
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consultaSql = "CALL p_listar_pedidos_mayoristas()";

            using MySqlCommand cmd = new MySqlCommand(consultaSql, conn);
            using MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }

    }
}
