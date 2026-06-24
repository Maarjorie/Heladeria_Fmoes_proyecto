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
    public static class PedidoMayoristaServicio
    {
        //Evita que el dao genere error al necesitar esos dos como llaves foraneas de las tablas, no tendrian relacion y generaria un error en la consulta
        public static int CrearPedido(Pedido_mayorista pedido)
        {
            if (pedido.IdCliente <= 0)
                throw new Exception("Debe seleccionar un cliente.");

            if (pedido.IdAtendidoPor <= 0)
                throw new Exception("Debe existir un usuario que atienda el pedido.");

            return Pedido_mayoristaDAO.CrearPedidoMayorista(pedido);
        }

        //Valida que esté seleccionado o exista ese id, porque ninguna tabla maneja el indice 0, Ni -1 
        public static bool ConfirmarPedido(Pedido_mayorista pedido)
        {
            if (pedido.IdPedido <= 0)
                throw new Exception("Debe seleccionar un pedido.");

            return Pedido_mayoristaDAO.ConfirmarPedidoMayorista(pedido);
        }

        //Mantiene que no pase valores negativos o 0 al metodo de la clase statica pedido_mayorista
        public static bool EntregarPedido(Pedido_mayorista pedido)
        {
            if (pedido.IdPedido <= 0)
                throw new Exception("Debe seleccionar un pedido.");

            if (pedido.IdEntregadoPor <= 0)
                throw new Exception("Debe existir un usuario que entregue el pedido.");

            return Pedido_mayoristaDAO.EntregarPedidoMayorista(pedido);
        }

        //No tiene validaciones pero se mantiene en la logica de negocio para evitar tener que llamar a la clase dao si no es por esta capa
        public static DataTable ListarPedidos()
        {
            return Pedido_mayoristaDAO.ListarPedidosMayoristas();
        }

        //Rechaza (cancela) un pedido pendiente
        public static bool CancelarPedido(int idPedido)
        {
            if (idPedido <= 0)
                throw new Exception("Debe seleccionar un pedido.");

            return Pedido_mayoristaDAO.CancelarPedidoMayorista(idPedido);
        }
    }
}
