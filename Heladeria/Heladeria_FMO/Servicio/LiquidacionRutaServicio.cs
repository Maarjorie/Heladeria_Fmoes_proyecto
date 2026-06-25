using Heladeria_FMO.Acceso_a_datos_db;
using Heladeria_FMO.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heladeria_FMO.Servicio
{
    public static class LiquidacionRutaServicio
    {
        //Verifica que si hay un id seleccionado que fue asignado al modelo de liquidación_ruta, y que el dinero no sea negativo, porque  si puede no haber vendido nada
        public static int LiquidarRuta(Liquidacion_ruta liquidacion)
        {
            if (liquidacion.IdSalida <= 0)
                throw new Exception("Debe seleccionar una salida.");

            if (liquidacion.DineroEntregado < 0)
                throw new Exception("El dinero entregado no puede ser negativo.");

            int idLiquidacion = Liquidacion_rutaDAO.LiquidarRuta(liquidacion);

            // Si la liquidación presenta diferencia, se alerta a supervisión.
            if (idLiquidacion > 0 && liquidacion.Diferencia != 0)
                NotificacionServicio.NotificarDiferenciaLiquidacion(idLiquidacion, liquidacion.Diferencia);

            return idLiquidacion;
        }

        //Evita que la clase dao genere error al querer recuperar esos dos datos y sean negativos o 0, al ser indices si son 0 es porque no están seleccionados o no existen
        public static bool ValidarLiquidacion(int idLiquidacion, int idValidadoPor)
        {
            if (idLiquidacion <= 0)
                throw new Exception("Debe seleccionar una liquidación.");

            if (idValidadoPor <= 0)
                throw new Exception("Debe existir un usuario validador.");

            return Liquidacion_rutaDAO.ValidarLiquidacion(
                idLiquidacion,
                idValidadoPor);
        }

        //Lista las liquidaciones pendientes de validación
        public static System.Data.DataTable ListarPendientes()
        {
            return Liquidacion_rutaDAO.ListarLiquidacionesPendientes();
        }
    }
}
