using Heladeria_FMO.Clases_Auxiliares;
using Heladeria_FMO.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heladeria_FMO.Acceso_a_datos_db
{
    public static class Detalle_salida_rutaDAO
    {
        //Ingresa datos a la tabla detalle_salida y almacena la cantidad de producto que sale y poder asignar esa salida a un usuaior
        public static bool RegistrarCargaProductoRuta(Detalle_salida_ruta d, int idUsuario)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consultaSql ="CALL p_registrar_carga_producto_ruta(" +"@p_id_salida," +"@p_id_producto," +"@p_cantidad_carga," + "@p_id_usuario)";

            using MySqlCommand cmd = new MySqlCommand(consultaSql, conn);

            cmd.Parameters.AddWithValue("@p_id_salida", d.IdSalida);
            cmd.Parameters.AddWithValue("@p_id_producto", d.IdProducto);
            cmd.Parameters.AddWithValue("@p_cantidad_carga", d.CantidadCarga);
            cmd.Parameters.AddWithValue("@p_id_usuario", idUsuario);

            int resultado = cmd.ExecuteNonQuery();
            return resultado > 0;
        }
    }
}
