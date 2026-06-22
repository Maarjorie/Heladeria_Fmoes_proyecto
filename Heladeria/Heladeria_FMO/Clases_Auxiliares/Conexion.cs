using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heladeria_FMO.Clases_Auxiliares
{
    public static class Conexion
    {
        //parametros para acceder a la base de datos
        const string servidor = "127.0.0.1";
        const string user = "root";
        const string clave = "";
        const string db = "db_heladeria";

        public static MySqlConnection ConexionDb() //metodo estatico para establecer conexion con la base de datos
        {
            const string cadena = $"Server={servidor};Database={db};user={user};pwd={clave}";
            return new MySqlConnection(cadena);
        }

    }
}
