using Heladeria_FMO.Clases_Auxiliares;
using Heladeria_FMO.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Heladeria_FMO.Acceso_a_datos_db
{
    public static class Cliente_mayoristaDAO
    {
        //Insertar cliente mayorista con los atributos de la clase Cliente Mayorista
        public static bool InsertarClienteMayorista(Cliente_mayorista c)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consultaSql ="CALL p_insertar_cliente_mayorista(" +"@p_nombre_comercial," +"@p_nit," +"@p_encargado," +"@p_direccion," +"@p_telefono," +"@p_correo)";

            using MySqlCommand cmd = new MySqlCommand(consultaSql, conn);

            cmd.Parameters.AddWithValue("@p_nombre_comercial", c.NombreComercial);
            cmd.Parameters.AddWithValue("@p_nit", c.Nit);
            cmd.Parameters.AddWithValue("@p_encargado", c.Encargado);
            cmd.Parameters.AddWithValue("@p_direccion", c.Direccion);
            cmd.Parameters.AddWithValue("@p_telefono", c.Telefono);
            cmd.Parameters.AddWithValue("@p_correo", c.Correo);

            int resultado = cmd.ExecuteNonQuery();

            return resultado > 0;
        }

        //Recupera los datos del cliente con la clase, y con el id, valida que numero de cliente es 
        public static bool EditarClienteMayorista(Cliente_mayorista c)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consultaSql =
                "CALL p_editar_cliente_mayorista(" +
                "@p_id_cliente," +
                "@p_nombre_comercial," +
                "@p_nit," +
                "@p_encargado," +
                "@p_direccion," +
                "@p_telefono," +
                "@p_correo)";

            using MySqlCommand cmd = new MySqlCommand(consultaSql, conn);

            cmd.Parameters.AddWithValue("@p_id_cliente", c.IdCliente);
            cmd.Parameters.AddWithValue("@p_nombre_comercial", c.NombreComercial);
            cmd.Parameters.AddWithValue("@p_nit", c.Nit);
            cmd.Parameters.AddWithValue("@p_encargado", c.Encargado);
            cmd.Parameters.AddWithValue("@p_direccion", c.Direccion);
            cmd.Parameters.AddWithValue("@p_telefono", c.Telefono);
            cmd.Parameters.AddWithValue("@p_correo", c.Correo);

            int resultado = cmd.ExecuteNonQuery();

            return resultado > 0;
        }
        //Cambia el estado de activo que es predeterminado en "Activo" cuando se crea
        public static bool CambiarEstadoClienteMayorista(Cliente_mayorista c)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consultaSql =
                "CALL p_cambiar_estado_cliente_mayorista(" +
                "@p_id_cliente," +
                "@p_activo)";

            using MySqlCommand cmd = new MySqlCommand(consultaSql, conn);

            cmd.Parameters.AddWithValue("@p_id_cliente", c.IdCliente);
            cmd.Parameters.AddWithValue("@p_activo", c.Activo);

            int resultado = cmd.ExecuteNonQuery();

            return resultado > 0;
        }

        //Muestra la Tabla Clientes mayoristas
        public static DataTable ListarClientesMayoristas()
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consultaSql = "CALL p_listar_clientes_mayoristas()";

            using MySqlCommand cmd = new MySqlCommand(consultaSql, conn);
            using MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            return dt;
        }
    }
}
