using Heladeria_FMO.Clases_Auxiliares;
using Heladeria_FMO.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Heladeria_FMO.Acceso_a_datos_db
{
    public static class Liquidacion_rutaDAO
    {

        public static int LiquidarRuta(Liquidacion_ruta l)
        {
            //Almacena los datos en la tabla de liquidacion_ruta para poder calcular en bas al dinero entregado
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consultaSql ="CALL p_liquidar_ruta(" +"@p_id_salida," +"@p_hora_regreso," +"@p_dinero_entregado," +"@p_observacion," +"@p_id_liquidacion)";

            using MySqlCommand cmd = new MySqlCommand(consultaSql, conn);

            cmd.Parameters.AddWithValue("@p_id_salida", l.IdSalida);
            cmd.Parameters.AddWithValue("@p_hora_regreso", l.HoraRegreso);
            cmd.Parameters.AddWithValue("@p_dinero_entregado", l.DineroEntregado);
            cmd.Parameters.AddWithValue("@p_observacion", l.Observacion);

            MySqlParameter parametroSalida =
                new MySqlParameter("@p_id_liquidacion", MySqlDbType.Int32);

            parametroSalida.Direction = ParameterDirection.Output;

            cmd.Parameters.Add(parametroSalida);

            cmd.ExecuteNonQuery();

            return Convert.ToInt32(parametroSalida.Value);
        }

        //No modifica nada de la informacion de liquidacion, guarda quien validó la ruta y cambia el estado
        public static bool ValidarLiquidacion(int idLiquidacion, int idValidadoPor)
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consultaSql ="CALL p_validar_liquidacion(" +"@p_id_liquidacion," +"@p_id_validado_por)";

            using MySqlCommand cmd = new MySqlCommand(consultaSql, conn);

            cmd.Parameters.AddWithValue("@p_id_liquidacion", idLiquidacion);
            cmd.Parameters.AddWithValue("@p_id_validado_por", idValidadoPor);

            int resultado = cmd.ExecuteNonQuery();

            return resultado > 0;
        }

        // Lista las liquidaciones que aún no han sido validadas por un supervisor.
        public static DataTable ListarLiquidacionesPendientes()
        {
            using MySqlConnection conn = Conexion.ConexionDb();
            conn.Open();

            string consultaSql = "CALL p_listar_liquidaciones_pendientes()";

            using MySqlCommand cmd = new MySqlCommand(consultaSql, conn);
            using MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }
    }
}
