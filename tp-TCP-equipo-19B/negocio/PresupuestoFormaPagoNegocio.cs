using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class PresupuestoFormaPagoNegocio
    {
        public List<PresupuestoFormaPago> ListarActivas()
        {
            AccesoDatos data = new AccesoDatos();
            List<PresupuestoFormaPago> lista = new List<PresupuestoFormaPago>();

            try
            {
                data.setearConsulta("SELECT * FROM presupuesto_forma_pago WHERE activo = 1");
                data.ejecutarLectura();

                while (data.Lector.Read())
                {
                    PresupuestoFormaPago aux = new PresupuestoFormaPago();
                    aux.Id = (int)data.Lector["id"];
                    aux.Nombre = (string)data.Lector["nombre"];
                    aux.Descripcion = (string)data.Lector["descripcion"];
                    aux.Activo = (bool)data.Lector["activo"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los metodos de pagos activos" + ex.Message);
            }
            finally
            {
                data.cerrarConexion();
            }
        }
    }
}
