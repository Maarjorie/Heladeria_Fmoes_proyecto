using System;
using System.Collections.Generic;
using System.Text;
using Heladeria_FMO.Clases_Auxiliares;
using Heladeria_FMO.Modelos;
using MySql.Data.MySqlClient;

namespace Heladeria_FMO.Acceso_a_datos_db
{
    public static class VendedorDAO
    {
        public static bool InsertarVendedor(Vendedor v)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consultaSql = "CALL p_insertar_vendedor(" +
                "@p_codigo_empleado," +
                "@p_nombre," +
                "@p_fotografia," +
                "@p_dui," +
                "@p_telefono," +
                "@p_direccion)";
            using MySqlCommand cmd = new MySqlCommand(consultaSql, conn);

            cmd.Parameters.AddWithValue("@p_codigo_empleado", v.CodigoEmpleado);
            cmd.Parameters.AddWithValue("@p_nombre", v.Nombre);
            cmd.Parameters.AddWithValue("@p_fotografia", v.Fotografia);
            cmd.Parameters.AddWithValue("@p_dui", v.Dui);
            cmd.Parameters.AddWithValue("@p_telefono", v.Telefono);
            cmd.Parameters.AddWithValue("@p_direccion", v.Direccion);

            int respuesta = cmd.ExecuteNonQuery();
            return respuesta > 0;


        }

        public static bool EditarVendedor(Vendedor v)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();
            string consultaSql = "CALL p_editar_vendedor(" +
                "@p_id_vendedor," +
                "@p_codigo_empleado," +
                "@p_nombre," +
                "@p_fotografia," +
                "@p_dui," +
                "@p_telefono," +
                "@p_direccion)";
            using MySqlCommand cmd = new MySqlCommand(consultaSql, conn);


            cmd.Parameters.AddWithValue("@p_id_vendedor", v.IdVendedor);
            cmd.Parameters.AddWithValue("@p_codigo_empleado", v.CodigoEmpleado);
            cmd.Parameters.AddWithValue("@p_nombre", v.Nombre);
            cmd.Parameters.AddWithValue("@p_fotografia", v.Fotografia);
            cmd.Parameters.AddWithValue("@p_dui", v.Dui);
            cmd.Parameters.AddWithValue("@p_telefono", v.Telefono);
            cmd.Parameters.AddWithValue("@p_direccion", v.Direccion);

            int respuesta = cmd.ExecuteNonQuery();
            return respuesta > 0;
        }

        public static bool CambiarEstadoVendedor(Vendedor v)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();
            string consultaSql = "CALL p_cambiar_estado_vendedor(" +
                "@p_id_vendedor," +
                "@p_estado)";
            using MySqlCommand cmd = new MySqlCommand(consultaSql, conn);


            cmd.Parameters.AddWithValue("@p_id_vendedor", v.IdVendedor);
            cmd.Parameters.AddWithValue("@p_estado",v.Estado);


            int respuesta = cmd.ExecuteNonQuery();
            return respuesta > 0;
        }

        // Lista los vendedores (para combos y administración).
        public static List<Vendedor> ListarVendedores()
        {
            List<Vendedor> vendedores = new();

            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consultaSql = "CALL p_listar_vendedores()";

            using MySqlCommand cmd = new MySqlCommand(consultaSql, conn);
            using MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                vendedores.Add(new Vendedor
                {
                    IdVendedor = reader.GetInt32(0),
                    CodigoEmpleado = reader.GetString(1),
                    Nombre = reader.GetString(2),
                    Dui = reader.IsDBNull(3) ? "" : reader.GetString(3),
                    Telefono = reader.IsDBNull(4) ? "" : reader.GetString(4),
                    Estado = !reader.IsDBNull(5) && reader.GetString(5) == "activo"
                });
            }

            return vendedores;
        }
    }
}
