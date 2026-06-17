using Heladeria_FMO.Acceso_a_datos_db;
using Heladeria_FMO.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heladeria_FMO.Servicio
{
    public enum Acceso //enumerador de posibles casos que pueden ocurrir al momento de logearte
    {
        ErrorCredenciales,
        UsuarioInactivo,
        ErrorConexionDb,
        SesionExitosa
    }

    public class UsuarioServicio
    {
        public static Acceso Login(string username, string password, out Usuario usuarioActual)
        {
            usuarioActual = null; //inicializamos el usuario

            try
            {
                //abrimos conexion con la db
                using MySqlConnection conn = Conexion.ConexionDb();
                conn.Open();

                //objeto usuario = usuarioDao.ObtenerPorUsername(username)
                //demas logica, faltan los dao y modelos
                
            }
            catch (Exception ex)
            {
                return Acceso.ErrorConexionDb;
            }

            return Acceso.SesionExitosa;
        }
    }

    public static class Sesion
    {
        public static Usuario UsuarioActivo { get; set; }
    }
}
