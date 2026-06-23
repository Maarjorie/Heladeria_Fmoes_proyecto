using Heladeria_FMO.Clases_Auxiliares;
using Heladeria_FMO.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Heladeria_FMO.Acceso_a_datos_db
{
    public static class ProductoDAO
    {
        // Inserta un nuevo producto en el catalogo
        public static bool InsertarProducto(Producto producto)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();
            string consulta = "CALL p_insertar_producto(" +
                "@p_codigo," +
                "@p_codigo_barras," +
                "@p_nombre," +
                "@p_id_categoria," +
                "@p_presentacion," +
                "@p_precio_compra," +
                "@p_precio_venta," +
                "@p_stock_actual," +
                "@p_stock_minimo," +
                "@p_fecha_ingreso," +
                "@p_fecha_vencimiento," +
                "@p_imagen_ruta)";

            using MySqlCommand cmd = new MySqlCommand(consulta, conn);
            cmd.Parameters.AddWithValue("@p_codigo", producto.Codigo);
            cmd.Parameters.AddWithValue("@p_codigo_barras", producto.CodigoBarras);
            cmd.Parameters.AddWithValue("@p_nombre", producto.Nombre);
            cmd.Parameters.AddWithValue("@p_id_categoria", producto.IdCategoria);
            cmd.Parameters.AddWithValue("@p_presentacion", producto.Presentacion);
            cmd.Parameters.AddWithValue("@p_precio_compra", producto.PrecioCompra);
            cmd.Parameters.AddWithValue("@p_precio_venta", producto.PrecioVenta);
            cmd.Parameters.AddWithValue("@p_stock_actual", producto.StockActual);
            cmd.Parameters.AddWithValue("@p_stock_minimo", producto.StockMinimo);
            cmd.Parameters.AddWithValue("@p_fecha_ingreso", producto.FechaIngreso);
            cmd.Parameters.AddWithValue("@p_fecha_vencimiento", producto.FechaVencimiento);
            cmd.Parameters.AddWithValue("@p_imagen_ruta", producto.ImagenRuta ?? (object)DBNull.Value);

            int resultado = cmd.ExecuteNonQuery(); //filas afectadas

            return resultado > 0;
        }

        // Edita los datos generales de un producto existente
        public static bool EditarProducto(Producto producto)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();
            string consulta = "CALL p_editar_producto(" +
                "@p_id_producto," +
                "@p_codigo," +
                "@p_codigo_barras," +
                "@p_nombre," +
                "@p_id_categoria," +
                "@p_presentacion," +
                "@p_precio_compra," +
                "@p_precio_venta," +
                "@p_stock_minimo," +
                "@p_fecha_vencimiento)";

            using MySqlCommand cmd = new MySqlCommand(consulta, conn);
            cmd.Parameters.AddWithValue("@p_id_producto", producto.IdProducto);
            cmd.Parameters.AddWithValue("@p_codigo", producto.Codigo);
            cmd.Parameters.AddWithValue("@p_codigo_barras", producto.CodigoBarras);
            cmd.Parameters.AddWithValue("@p_nombre", producto.Nombre);
            cmd.Parameters.AddWithValue("@p_id_categoria", producto.IdCategoria);
            cmd.Parameters.AddWithValue("@p_presentacion", producto.Presentacion);
            cmd.Parameters.AddWithValue("@p_precio_compra", producto.PrecioCompra);
            cmd.Parameters.AddWithValue("@p_precio_venta", producto.PrecioVenta);
            cmd.Parameters.AddWithValue("@p_stock_minimo", producto.StockMinimo);
            cmd.Parameters.AddWithValue("@p_fecha_vencimiento", producto.FechaVencimiento);

            int resultado = cmd.ExecuteNonQuery();

            return resultado > 0;
        }

        //Activa o desactiva un producto sin tocar el resto de los datos
        public static bool CambiarEstadoProducto(int idProducto, bool activo)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();
            string consulta = "CALL p_cambiar_estado_producto(" +
                "@p_id_producto," +
                "@p_activo)";

            using MySqlCommand cmd = new MySqlCommand(consulta, conn);
            cmd.Parameters.AddWithValue("@p_id_producto", idProducto);
            cmd.Parameters.AddWithValue("@p_activo", activo);

            int resultado = cmd.ExecuteNonQuery();

            return resultado > 0;
        }

        // Lista todos los productos
        public static List<Producto> ListarProductos()
        {
            List<Producto> productos = [];

            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();
            string consulta = "CAll p_listar_productos()";

            using MySqlCommand cmd = new MySqlCommand(consulta,conn);
            using MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Producto producto = new() 
                {
                    IdProducto = reader.GetInt32(0),
                    Codigo = reader.GetString(1),
                    CodigoBarras = reader.GetString(2),
                    Nombre = reader.GetString(3),
                    NombreCategoria = reader.GetString(4),
                    Presentacion = reader.GetString(5),
                    PrecioCompra = reader.GetDecimal(6),
                    PrecioVenta = reader.GetDecimal(7),
                    StockActual = reader.GetInt32(8),
                    StockMinimo = reader.GetInt32(9),
                    FechaVencimiento = reader.GetDateTime(10),
                    ImagenRuta = reader.IsDBNull(11) ? null : reader.GetString(11)
                };
                productos.Add(producto);
            }

            return productos;
        }

        //Lista los productos que su stock actual ya alcanzo o esta por debajo de su stock minimo.
        public static List<Producto> ProductoBajoStock()
        {
            List<Producto> productos = [];

            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();
            string consulta = "CAll p_productos_bajo_stock()";

            using MySqlCommand cmd = new MySqlCommand(consulta, conn);
            using MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Producto producto = new()
                {
                    IdProducto = reader.GetInt32(0),
                    IdCategoria = reader.GetInt32(1),
                    Codigo = reader.GetString(2),
                    CodigoBarras = reader.GetString(3),
                    Nombre = reader.GetString(4 ),
                    Presentacion = reader.GetString(5),
                    PrecioCompra = reader.GetDecimal(6),
                    PrecioVenta = reader.GetDecimal(7),
                    StockActual = reader.GetInt32(8),
                    StockMinimo = reader.GetInt32(9),
                    FechaIngreso = reader.GetDateTime(10),
                    FechaVencimiento = reader.GetDateTime(11),
                    NivelAlertaVencimiento = reader.GetInt32(12),
                    Activo = reader.GetBoolean(13),
                    FechaRegistro = reader.GetDateTime(14)
                };
                productos.Add(producto);
            }

            return productos;
        }

        //lista los productos proximos a vencer
        public static List<Producto> ProductosPorVencer(int dias)
        {
            List<Producto> productos = [];

            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();
            string consulta = "CAll p_productos_por_vencer(" +
                "@p_dias)";

            using MySqlCommand cmd = new MySqlCommand(consulta, conn);
            cmd.Parameters.AddWithValue("@p_dias", dias);
            using MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Producto producto = new()
                {
                    IdProducto = reader.GetInt32(0),
                    IdCategoria = reader.GetInt32(1),
                    Codigo = reader.GetString(2),
                    CodigoBarras = reader.GetString(3),
                    Nombre = reader.GetString(4),
                    Presentacion = reader.GetString(5),
                    PrecioCompra = reader.GetDecimal(6),
                    PrecioVenta = reader.GetDecimal(7),
                    StockActual = reader.GetInt32(8),
                    StockMinimo = reader.GetInt32(9),
                    FechaIngreso = reader.GetDateTime(10),
                    FechaVencimiento = reader.GetDateTime(11),
                    NivelAlertaVencimiento = reader.GetInt32(12),
                    Activo = reader.GetBoolean(13),
                    FechaRegistro = reader.GetDateTime(14)
                };
                productos.Add(producto);
            }

            return productos;
        }

        // Actualiza solamente el contador de nivel de alerta de vencimiento (0-4) de un producto
        public static bool ActualizarNivelAlerta(int idProducto, int nivel)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consulta = "CALL p_actualizar_nivel_alerta(" +
                "@p_id_producto," +
                "@p_nivel)";

            using MySqlCommand cmd = new MySqlCommand(consulta, conn);
            cmd.Parameters.AddWithValue("@p_id_producto", idProducto);
            cmd.Parameters.AddWithValue("@p_nivel", nivel);

            int resultado = cmd.ExecuteNonQuery();
            return resultado > 0;
        }
    }
}
