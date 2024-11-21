using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;
using static dominio.Enums;

namespace negocio
{
    public class PresupuestoNegocio
    {
        public Presupuesto Crear(int idCliente, int idMetodoEnvio, int idFormaPago, int idDomicilioEnvio, List<Productos> detalles)
        {
            AccesoDatos datos = new AccesoDatos();
            PresupuestoDetalleNegocio presupuestoDetalleNegocio = new PresupuestoDetalleNegocio();
            try
            {
                // Validar stock
                if (!ValidarStockDisponible(detalles))
                    throw new Exception("No hay stock disponible para realizar la compra, por favor, revise los items del carrito.");


                EnvioNegocio envioNegocio = new EnvioNegocio();
                decimal costoEnvio = envioNegocio.getCostoByIdEnvio(idMetodoEnvio);

                Presupuesto presu = new Presupuesto
                {
                    IdCliente = idCliente,
                    IdMetodoEnvio = idMetodoEnvio,
                    IdEstado = (byte)EnumPresupuestoEstado.Creado,
                    IdFormaPago = idFormaPago,
                    FechaCreacion = DateTime.Now,
                    FechaValidez = DateTime.Now.AddDays(5),
                    IdClienteEnvio = idDomicilioEnvio,
                    CostoEnvio = costoEnvio,
                    UltimaActualizacion = DateTime.Now
                };
                
                datos.setearConsulta("INSERT INTO presupuesto " +
                    "(id_cliente,id_metodo_envio,id_estado,id_forma_pago,fecha_validez,id_cliente_envio,costo_envio)" +
                    "VALUES (@id_cliente, @id_metodo_envio, @id_estado, @id_forma_pago, @fecha_validez, @id_cliente_envio, @costo_envio);" +
                    "SELECT SCOPE_IDENTITY();");
                datos.setearParametro("@id_cliente",idCliente);
                datos.setearParametro("@id_metodo_envio",idMetodoEnvio);
                datos.setearParametro("@id_estado",(byte)EnumPresupuestoEstado.Creado);
                datos.setearParametro("@id_forma_pago",idFormaPago);
                datos.setearParametro("@fecha_validez",DateTime.Now.AddDays(5));
                datos.setearParametro("@id_cliente_envio",idDomicilioEnvio);
                datos.setearParametro("@costo_envio", costoEnvio);

                datos.ejecutarLectura();

                int idPresupuesto = 0;
                if (datos.Lector.Read())
                {
                    idPresupuesto = Convert.ToInt32(datos.Lector[0]);
                }

                presu.Id = idPresupuesto;

                return presu;

            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el presupuesto: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        private bool ValidarStockDisponible(List<Productos> detalles)
        {
            ProductoNegocio negocio = new ProductoNegocio();
            foreach (var detalle in detalles)
            {
                var producto = negocio.buscarPorID(detalle.Id_producto);
                if (producto.stock < detalle.Cantidad)
                    return false;
            }
            return true;
        }

        public void ActualizarTotal(int idPresupuesto, decimal total)
        {
            AccesoDatos data = new AccesoDatos();
            try
            {
                data.setearConsulta("UPDATE presupuesto SET total=@total WHERE id=@idPresu;");
                data.setearParametro("@idPresu",idPresupuesto);
                data.setearParametro("@total", total);
                data.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw new Exception("Error al actualizar el total: " + ex.Message);
            }
        }

        public List<Presupuesto> ObtenerPresupuestosConDetalles()
        {
            List<Presupuesto> presupuestos = new List<Presupuesto>();
            AccesoDatos data = new AccesoDatos();

            try
            {
                data.setearConsulta(@"
            SELECT 
    pre.id AS IdPresupuesto,
    c.nombre AS NombreCliente,
    c.apellido AS ApellidoCliente,
    p.nombre AS NombreProducto,
    pre.total AS Total,
    fp.nombre AS FormaPago,
    pre.fecha_creacion AS FechaCreacion
FROM 
    presupuesto pre
INNER JOIN 
    cliente c ON pre.id_cliente = c.id_cliente
INNER JOIN 
    presupuesto_detalle pd ON pre.id = pd.id_presupuesto
INNER JOIN 
    producto p ON pd.id_producto = p.Id_producto
INNER JOIN 
    presupuesto_forma_pago fp ON pre.id_forma_pago = fp.id");

                data.ejecutarLectura();

                while (data.Lector.Read())
                {
                    var presupuesto = new Presupuesto
                    {
                        Id = Convert.ToInt32(data.Lector["IdPresupuesto"]),
                        Total = Convert.ToDecimal(data.Lector["Total"]),
                        FechaCreacion = Convert.ToDateTime(data.Lector["FechaCreacion"]),
                        FormaPago = new PresupuestoFormaPago
                        {
                            Nombre = data.Lector["FormaPago"].ToString()
                        },
                        Cliente = new Cliente
                        {
                            Nombre = data.Lector["NombreCliente"].ToString(),
                            Apellido = data.Lector["ApellidoCliente"].ToString()
                        },
                        Detalles = new List<PresupuestoDetalle>
        {
            new PresupuestoDetalle
            {
                Producto = new Productos
                {
                    Nombre = data.Lector["NombreProducto"].ToString()
                }
            }
        }
                    };

                    presupuestos.Add(presupuesto);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los presupuestos: " + ex.Message);
            }
            finally
            {
                data.cerrarConexion();
            }

            return presupuestos;
        }
    }
}
