using Heladeria_FMO.Acceso_a_datos_db;
using Heladeria_FMO.Modelos;
using Heladeria_FMO.Utileria;
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

    public static class UsuarioServicio
    {
        public static Acceso Login(string username, string password, out Usuario usuarioActual)
        {
            usuarioActual = null; //inicializamos el parametro de salida
            Usuario usuario;

            try// intentamos obtener el usuario de la base de datos
            {
                usuario = UsuarioDao.ObtenerUsuarioPorNombre(username);   
            }
            catch (Exception ex)
            {
                //retona en caso falle el acceso a la base de datos
                return Acceso.ErrorConexionDb;
            }

            // valida la existencia del usuario
            if (usuario == null) return Acceso.ErrorCredenciales;

            // validacion de seguidad por medio de comparacion hash
            if (!Seguridad.VerificarHash(password, usuario.Contrasenia_hash, usuario.Contrasenia_salt)) return Acceso.ErrorCredenciales;

            // validacion del estado de la cuenta
            if (!usuario.Activo) return Acceso.UsuarioInactivo;

            // asignamos el usuairo autenticado al parametro de salida
            usuarioActual = usuario;

            return Acceso.SesionExitosa;
        }

        // Registra un nuevo usuario
        public static bool RegistrarUsuario(int idRol, string nombre, string username, string contrasena, string correo)
        {
            string salt;
            string hash = Seguridad.GenerarHash(contrasena, out salt);

            Usuario usuario = new()
            {
                id_rol = idRol,
                Nombre = nombre,
                Usuario_ = username,
                Contrasenia_hash = hash,
                Contrasenia_salt = salt,
                Correo = correo
            };

            return UsuarioDao.InsertarUsuario(usuario);
        }

        // Modifica datos generalesd del usuairo
        public static bool EditarUsuario(int idUsuario, string nombre, string correo, int idRol)
        {
            Usuario usuario = new()
            {
                id_Usuario = idUsuario,
                Nombre = nombre,
                Correo = correo,
                id_rol = idRol
            };

            return UsuarioDao.EditarUsuario(usuario);
        }

        // Activa o desactiva un usuario
        public static bool CambiarEstadoUsuario(int idUsuario, bool activo)
        {
            return UsuarioDao.CambiarEstadoUsuario(idUsuario, activo);
        }

        //lista los usuarios activos
        public static List<Usuario> ListarUsuariosActivos()
        {
            return UsuarioDao.ListarUsuariosActivos();
        }

        //lista todos los usuarios (activos e inactivos)
        public static List<Usuario> ListarUsuarios()
        {
            return UsuarioDao.ListarUsuarios();
        }
    }

    public static class Sesion
    {
        // establece la entidad del usuario activo en la sesion actual
        public static Usuario UsuarioActivo { get; set; }

        // ---- Caja abierta en la sesion actual --------------------------------
        // IdCajaActiva = 0 significa que no hay ninguna caja abierta.
        public static int IdCajaActiva { get; set; }
        public static decimal FondoCajaActiva { get; set; }

        // Total vendido acumulado desde que se abrio la caja (alimenta el
        // "monto esperado" del arqueo). Lo incrementa el punto de venta al cobrar.
        public static decimal TotalVendidoCaja { get; set; }

        public static bool HayCajaAbierta => IdCajaActiva > 0;
    }
}
