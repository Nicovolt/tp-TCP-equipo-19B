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

        public List<PresupuestoDetalle> ListarDetalles(int idPresupuesto)
        {
            AccesoDatos data = new AccesoDatos();
            List<PresupuestoDetalle> lista = new List<PresupuestoDetalle>();

            try
            {
                data.setearConsulta("SELECT id, id_presupuesto, id_producto, precio_unitario, cantidad, subtotal, fecha_agregado, agregado_id_usuario " +
                    "FROM presupuesto_detalle pd " +
                    "WHERE id_presupuesto = @id_presupuesto;");
                data.setearParametro("@id_presupuesto",idPresupuesto);
                data.ejecutarLectura();

                while (data.Lector.Read())
                {
                    PresupuestoDetalle aux = new PresupuestoDetalle();

                    aux.Id = (int)data.Lector["id"];
                    aux.IdPresupuesto = (int)data.Lector["id_presupuesto"];
                    aux.IdProducto = (int)data.Lector["id_producto"];
                    aux.PrecioUnitario = (decimal)data.Lector["precio_unitario"];
                    aux.Cantidad = (int)data.Lector["cantidad"];
                    aux.Subtotal = (decimal)data.Lector["subtotal"];
                    aux.FechaAgregado = (DateTime)data.Lector["fecha_agregado"];
                    aux.AgregadoIdUsuario = (int)data.Lector["agregado_id_usuario"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar los detalles del presupuesto: " + idPresupuesto + ex.Message);
            }
            finally
            {
                data.cerrarConexion();
            }
        }
    }
}
