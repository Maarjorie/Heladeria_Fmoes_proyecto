using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Heladeria_FMO.Utileria
{
    public static class Seguridad
    {
        public static string GenerarHash(string pw, out string cadenaAleatoria)//metodo para hashear la contraseña
        {
            //generamos valores aleatorios en binario
            byte[] salt = RandomNumberGenerator.GetBytes(32);

            string saltStr = Convert.ToBase64String(salt);//convertimos a string

            string hash = pw + saltStr;//concatenamos la contraseña con los valores aleatorios para evitar hash duplicados de diferentes contreñas de usuarios

            using SHA256 sha256 = SHA256.Create();
            byte[] hashBinaria = Encoding.UTF8.GetBytes(hash); //convertimos a byte
            string getHash = Convert.ToBase64String(sha256.ComputeHash(hashBinaria));//pasamos a cadena de texto
            cadenaAleatoria = saltStr;
            return getHash;
        }

        public static bool VerificarHash(string pw, string hashGuardado, string saltGuardado)
        {
            string hashCombinado = pw + saltGuardado;
            byte[] hashBinaria = Encoding.UTF8.GetBytes(hashCombinado);
            using SHA256 sha256 = SHA256.Create();
            string resultado = Convert.ToBase64String(sha256.ComputeHash(hashBinaria));

            return resultado == saltGuardado;
        }
    }
}
