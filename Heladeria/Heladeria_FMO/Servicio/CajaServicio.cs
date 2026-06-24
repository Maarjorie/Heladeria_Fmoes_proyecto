using Heladeria_FMO.Acceso_a_datos_db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heladeria_FMO.Servicio
{
    public static class CajaServicio
    {
        public static int AbrirCaja(int idCajero, decimal fondoInicial)
        {
            return CajaDAO.AbrirCaja(idCajero, fondoInicial);
        }

        public static bool CerrarCaja(int idCaja, decimal montoContado)
        {
            return CajaDAO.CerrarCaja(idCaja, montoContado);
        }

        /*Registra un arqueo de caja, si la diferencia entre lo esperado
        y lo contado no es cero, genera una notificacion de inconsistencia*/
        public static bool RealizarArqueo(int idCaja, int idRealizadoPor, decimal montoEsperado, decimal montoContado)
        {
            bool resultado = Arqueo_cajaDAO.InsertarArqueo(idCaja, idRealizadoPor, montoEsperado, montoContado);

            decimal diferencia = montoContado - montoEsperado;
            if (resultado && diferencia != 0)
            {
                string mensaje = $"El arqueo de la caja #{idCaja} presenta una diferencia de {diferencia:0.00}.";
                NotificacionDAO.InsertarNotificacion("arqueo_inconsistente", idCaja, mensaje);
            }

            return resultado;
        }

        public static bool RegistrarMovimiento(int idCaja, int idUsuario, string tipo, string concepto, decimal monto)
        {
            return Movimiento_cajaDAO.InsertarMovimiento(idCaja, idUsuario, tipo, concepto, monto);
        }

        // Arqueos pendientes de autorización por un supervisor/admin.
        public static System.Data.DataTable ListarArqueosPendientes()
        {
            return Arqueo_cajaDAO.ListarPendientes();
        }

        public static bool AutorizarArqueo(int idArqueo, int idAutorizadoPor)
        {
            if (idAutorizadoPor <= 0)
                throw new Exception("No hay un usuario autorizado en sesión.");

            return Arqueo_cajaDAO.AutorizarArqueo(idArqueo, idAutorizadoPor);
        }
    }
}
