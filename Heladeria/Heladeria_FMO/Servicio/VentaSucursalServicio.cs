using Heladeria_FMO.Acceso_a_datos_db;
using Heladeria_FMO.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heladeria_FMO.Servicio
{
    internal class VentaSucursalServicio
    {
        public static int RegistrarVentaSucursal(Venta_sucursal venta)
        {
            if (venta.IdCaja <= 0)
                throw new Exception("Debe haber una caja abierta.");

            if (venta.IdCajero <= 0)
                throw new Exception("Debe existir un cajero asignado.");

            if (venta.Descuento < 0)
                throw new Exception("El descuento no puede ser negativo.");

            return Venta_sucursalDAO.RegistrarVentaSucursal(venta);
        }

        public static bool AgregarDetalleVentaSucursal(Detalle_Venta_Sucursal detalle, int idUsuario)
        {
            if (detalle.IdVenta <= 0)
                throw new Exception("Primero debe registrar la venta.");

            if (detalle.IdProducto <= 0)
                throw new Exception("Debe seleccionar un producto.");

            if (detalle.Cantidad <= 0)
                throw new Exception("La cantidad debe ser mayor que cero.");

            if (detalle.Descuento < 0)
                throw new Exception("El descuento no puede ser negativo.");

            if (idUsuario <= 0)
                throw new Exception("No hay usuario activo.");

            return Detalle_Venta_SucursalDAO.AgregarDetalleVentaSucursal(detalle, idUsuario);
        }

        public static bool AnularVentaSucursal(Venta_sucursal venta, int idUsuario, string motivo)
        {
            if (venta.IdVenta <= 0)
                throw new Exception("Debe seleccionar una venta.");

            if (idUsuario <= 0)
                throw new Exception("No hay usuario activo.");

            if (string.IsNullOrWhiteSpace(motivo))
                throw new Exception("Debe ingresar el motivo de anulación.");

            return Venta_sucursalDAO.AnularVentaSucursal(venta, idUsuario, motivo);
        }

        public static DataTable ListarVentasSucursal()
        {
            return Venta_sucursalDAO.ListarVentasSucursal();
        }
    }
}
