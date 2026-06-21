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
    public static class RutaServicio
    {
        //Valida las entradas que puedan ser null y generar un error en nuestra base de datos, como el nombre o la zona que afectarian la recoleccion de datos
        public static bool InsertarRuta(Ruta ruta)
        {
            if (string.IsNullOrWhiteSpace(ruta.Nombre))
                throw new Exception("El nombre de la ruta es obligatorio.");

            if (string.IsNullOrWhiteSpace(ruta.Zona))
                throw new Exception("La zona de la ruta es obligatoria.");

            if (ruta.IdResponsable <= 0)
                throw new Exception("Debe seleccionar un responsable.");

            if (ruta.HorarioInicio >= ruta.HorarioFin)
                throw new Exception("La hora de inicio debe ser menor que la hora de fin."); //Logica del negocio fundamental de logica basica

            return RutaDAO.InsertarRuta(ruta); //Si todo está validado, se ejecutaa el método de la clase rutaDao donde ya están creadas las llamasdas a los procedimientos
        }

        //Valida que si esté seleccionado el id para poder ejecutar la consulta de edición que está en el metodo de la clase Dao que utiliza las propiedades de su clase modelo
        public static bool EditarRuta(Ruta ruta)
        {
            if (ruta.IdRuta <= 0)
                throw new Exception("Debe seleccionar una ruta.");

            return RutaDAO.EditarRuta(ruta);
        }

        //Mismo caso, validamos que esté seleccionado un id para poder ejecutar el metodo que contiene el procedimeito
        public static bool CambiarEstadoRuta(Ruta ruta)
        {
            if (ruta.IdRuta <= 0)
                throw new Exception("Debe seleccionar una ruta.");

            return RutaDAO.CambiarEstadoRuta(ruta);
        }

        //Este metodo no requiere validacion, pero para utilizar la misma clase en todos los llamados, pasan por esta capa de logica
        public static DataTable ListarRutas()
        {
            return RutaDAO.ListarRutas();
        }

        //Valida que buscar no esté como valor vacio o nulo, si no es asi ejecuta el metodo de buscar
        public static DataTable BuscarRutas(string busqueda)
        {
            if (string.IsNullOrWhiteSpace(busqueda))
                return RutaDAO.ListarRutas();

            return RutaDAO.BuscarRutas(busqueda);
        }

    }
}