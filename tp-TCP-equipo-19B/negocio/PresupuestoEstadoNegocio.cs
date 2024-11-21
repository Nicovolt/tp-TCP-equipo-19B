using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class PresupuestoEstadoNegocio
    {
        public List<PresupuestoEstado> ListarEstados()
        {
            AccesoDatos data = new AccesoDatos();
            List<PresupuestoEstado> lista = new List<PresupuestoEstado>();
            try
            {
                data.setearConsulta("SELECT id, nombre, descripcion, final, cancelado, vencido, orden FROM presupuesto_estado");
                data.ejecutarLectura();

                while (data.Lector.Read())
                {
                    PresupuestoEstado aux = new PresupuestoEstado();

                    aux.Id = (short)data.Lector["id"];
                    aux.Nombre = (string)data.Lector["nombre"];
                    aux.Descripcion = (string)data.Lector["descripcion"];
                    aux.Final = (bool)data.Lector["final"];
                    aux.Cancelado = (bool)data.Lector["cancelado"];
                    aux.Vencido = (bool)data.Lector["vencido"];
                    aux.Orden = (short)data.Lector["orden"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al listar los estados de los presupuestos: " + ex.Message);
            }
        }
    }
}
