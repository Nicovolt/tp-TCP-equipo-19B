using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class PresupuestoDetalleNegocio
    {
        public void AgregarDetallePresupuesto(int idPresupuesto, List<Productos> detalles, int idUser)
        {
      
            foreach (var producto in detalles)
            {
                AccesoDatos data = new AccesoDatos();

                try
                {
                    decimal subtotal = producto.Precio * producto.Cantidad;

                    data.setearConsulta("INSERT INTO presupuesto_detalle (id_presupuesto, id_producto, precio_unitario, cantidad, subtotal, agregado_id_usuario)" +
                        "VALUES (@id_presupuesto, @id_producto, @precio, @cantidad, @subtotal, @usuario)");
                    data.setearParametro("@id_presupuesto", idPresupuesto);
                    data.setearParametro("@id_producto",producto.Id_producto);
                    data.setearParametro("@precio",producto.Precio);
                    data.setearParametro("@cantidad",producto.Cantidad);
                    data.setearParametro("@subtotal", subtotal);
                    data.setearParametro("@usuario",idUser);

                    data.ejecutarAccion();

                 
                }
                catch (Exception ex)
                {

                    throw new Exception("Error al crear el detalle del presupuesto: " + producto.Id_producto + ex.Message);
                }
                finally
                {
                    data.cerrarConexion();
                }


            }
          
        }


    }
}
