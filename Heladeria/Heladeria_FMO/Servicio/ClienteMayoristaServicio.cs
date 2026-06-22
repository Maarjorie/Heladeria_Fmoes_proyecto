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
    public static class ClienteMayoristaServicio
    {
        //Validación de campos necesarios para evitar erroes de valores nulos o vacios
        public static bool ValidarCliente(Cliente_mayorista cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente.NombreComercial))
                throw new Exception("El nombre comercial es obligatorio.");

            if (string.IsNullOrWhiteSpace(cliente.Nit))
                throw new Exception("El NIT es obligatorio.");

            if (string.IsNullOrWhiteSpace(cliente.Encargado))
                throw new Exception("Debe ingresar el encargado.");

            if (string.IsNullOrWhiteSpace(cliente.Telefono))
                throw new Exception("Debe ingresar un teléfono.");

            return Cliente_mayoristaDAO.InsertarClienteMayorista(cliente);
        }


        //Verificar que un dato de fila o columna está seleccionado
        public static bool EditarCliente(Cliente_mayorista cliente)
        {
            if (cliente.IdCliente <= 0)
                throw new Exception("Debe seleccionar un cliente.");

            ValidarCliente(cliente);

            return Cliente_mayoristaDAO.EditarClienteMayorista(cliente);
        }

        public static bool CambiarEstadoCliente(Cliente_mayorista cliente)
        {
            if (cliente.IdCliente <= 0)
                throw new Exception("Debe seleccionar un cliente.");

            return Cliente_mayoristaDAO.CambiarEstadoClienteMayorista(cliente);
        }

        //Mostrar los clientes mayoristas, no requiere validacion pero para poder llamar solamente esta clase como capa extra de seguridad
        public static DataTable ListarClientes()
        {
            return Cliente_mayoristaDAO.ListarClientesMayoristas();
        }
    }
}
