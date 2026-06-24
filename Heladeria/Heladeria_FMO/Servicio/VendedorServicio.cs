using Heladeria_FMO.Acceso_a_datos_db;
using Heladeria_FMO.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heladeria_FMO.Servicio
{
    public static class VendedorServicio
    {
        //Valida los campos necesarios para evitar errores en la entrada de datos como espacios en blanco o nulos, que dañarian la logica de la base de datos
        public static bool InsertarVendedor(Vendedor vendedor)
        {
            if (string.IsNullOrWhiteSpace(vendedor.CodigoEmpleado))
                throw new Exception("El código de empleado es obligatorio.");

            if (string.IsNullOrWhiteSpace(vendedor.Nombre))
                throw new Exception("El nombre del vendedor es obligatorio.");

            if (string.IsNullOrWhiteSpace(vendedor.Dui))
                throw new Exception("El DUI del vendedor es obligatorio.");

            if (string.IsNullOrWhiteSpace(vendedor.Telefono))
                throw new Exception("El teléfono del vendedor es obligatorio.");

            return VendedorDAO.InsertarVendedor(vendedor);
        }

        //Mantiene que se necesita seleccionar un vendedor, tomando el id que se selecciona como atributo de la clase Vendedor
        public static bool EditarVendedor(Vendedor vendedor)
        {
            if (vendedor.IdVendedor <= 0)
                throw new Exception("Debe seleccionar un vendedor.");

            return VendedorDAO.EditarVendedor(vendedor);
        }

        //Valida que esté seleccionado un vendedor para poder ejecutar el metodo de la clase dao de Vendedor
        public static bool CambiarEstadoVendedor(Vendedor vendedor)
        {
            if (vendedor.IdVendedor <= 0)
                throw new Exception("Debe seleccionar un vendedor.");

            return VendedorDAO.CambiarEstadoVendedor(vendedor);
        }

        //Lista los vendedores registrados
        public static List<Vendedor> ListarVendedores()
        {
            return VendedorDAO.ListarVendedores();
        }
    }
}
