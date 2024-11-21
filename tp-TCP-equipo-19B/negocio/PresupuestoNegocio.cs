using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
                datos.setearParametro("@id_cliente", idCliente);
                datos.setearParametro("@id_metodo_envio", idMetodoEnvio);
                datos.setearParametro("@id_estado", (byte)EnumPresupuestoEstado.Creado);
                datos.setearParametro("@id_forma_pago", idFormaPago);
                datos.setearParametro("@fecha_validez", DateTime.Now.AddDays(5));
                datos.setearParametro("@id_cliente_envio", idDomicilioEnvio);
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
                data.setearParametro("@idPresu", idPresupuesto);
                data.setearParametro("@total", total);
                data.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw new Exception("Error al actualizar el total: " + ex.Message);
            }
        }

        public List<Presupuesto> ListarPorCliente(int idCliente, string ordenamiento = "fecha_desc")
        {
            AccesoDatos data = new AccesoDatos();
            List<Presupuesto> lista = new List<Presupuesto>();
            try
            {
                string consulta = @"SELECT p.id, p.id_cliente, p.id_metodo_envio, p.id_estado, p.id_forma_pago, 
                p.fecha_creacion, p.fecha_validez, p.id_cliente_envio, p.costo_envio, 
                p.total, p.ultima_actualizacion, pe.nombre as estado_nombre, pe.descripcion as estado_descripcion 
                FROM presupuesto p 
                INNER JOIN presupuesto_estado pe ON p.id_estado = pe.id 
                WHERE p.id_cliente = @id_cliente";

                consulta += GetOrdenamiento(ordenamiento);

                data.setearConsulta(consulta);
                data.setearParametro("@id_cliente", idCliente);

                data.ejecutarLectura();

                while (data.Lector.Read())
                {
                    Presupuesto aux = new Presupuesto();
                    aux.Id = (int)data.Lector["id"];
                    aux.IdCliente = (int)data.Lector["id_cliente"];
                    aux.IdMetodoEnvio = (int)data.Lector["id_metodo_envio"];
                    aux.IdEstado = (short)data.Lector["id_estado"];
                    aux.IdFormaPago = (int)data.Lector["id_forma_pago"];
                    aux.FechaCreacion = (DateTime)data.Lector["fecha_creacion"];
                    aux.FechaValidez = (DateTime)data.Lector["fecha_validez"];
                    aux.IdClienteEnvio = (int)data.Lector["id_cliente_envio"];
                    aux.CostoEnvio = (decimal)data.Lector["costo_envio"];
                    aux.Total = (decimal)data.Lector["total"];
                    aux.UltimaActualizacion = (DateTime)data.Lector["ultima_actualizacion"];

                    // Agregar el objeto Estado
                    aux.Estado = new PresupuestoEstado
                    {
                        Id = aux.IdEstado,
                        Nombre = data.Lector["estado_nombre"].ToString(),
                        Descripcion = data.Lector["estado_descripcion"] != DBNull.Value ?
                            data.Lector["estado_descripcion"].ToString() : null
                    };

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los presupuestos del cliente: " + idCliente + ex.Message);
            }
            finally
            {
                data.cerrarConexion();
            }
        }

        private string GetOrdenamiento(string orden)
        {
            switch (orden)
            {
                case "fecha_asc":
                    return " ORDER BY p.fecha_creacion ASC";
                case "monto_desc":
                    return " ORDER BY p.total DESC";
                case "monto_asc":
                    return " ORDER BY p.total ASC";
                default: // fecha_desc
                    return " ORDER BY p.fecha_creacion DESC";
            }
        }

        public Presupuesto ObtenerPorId(int idPresupuesto)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = @"
                SELECT p.*, pe.nombre as estado_nombre, pe.descripcion as estado_descripcion,
                       et.nombre as envio_nombre, et.costo as envio_costo,
                       pfp.nombre as forma_pago_nombre, pfp.descripcion as forma_pago_descripcion,
                       cde.calle, cde.altura, cde.entre_calles, cde.piso, cde.departamento,
                       cde.localidad, cde.provincia, cde.cp as codigo_postal
                FROM presupuesto p
                INNER JOIN presupuesto_estado pe ON p.id_estado = pe.id
                INNER JOIN envio_tipo et ON p.id_metodo_envio = et.id
                INNER JOIN presupuesto_forma_pago pfp ON p.id_forma_pago = pfp.id
                INNER JOIN cliente_dom_envio cde ON p.id_cliente_envio = cde.id
                WHERE p.id = @idPresupuesto";

                datos.setearConsulta(consulta);
                datos.setearParametro("@idPresupuesto", idPresupuesto);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                    return CargarPresupuesto(datos.Lector, true);

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener presupuesto", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        private Presupuesto CargarPresupuesto(SqlDataReader lector, bool cargarCompleto = false)
        {
            Presupuesto presupuesto = new Presupuesto
            {
                Id = (int)lector["id"],
                IdCliente = (int)lector["id_cliente"],
                IdMetodoEnvio = (int)lector["id_metodo_envio"],
                IdEstado = (short)lector["id_estado"],
                IdFormaPago = (int)lector["id_forma_pago"],
                FechaCreacion = (DateTime)lector["fecha_creacion"],
                FechaValidez = (DateTime)lector["fecha_validez"],
                IdClienteEnvio = (int)lector["id_cliente_envio"],
                CostoEnvio = (decimal)lector["costo_envio"],
                Total = (decimal)lector["total"],
                UltimaActualizacion = (DateTime)lector["ultima_actualizacion"],

                // Objetos relacionados básicos
                Estado = new PresupuestoEstado
                {
                    Id = (short)lector["id_estado"],
                    Nombre = (string)lector["estado_nombre"],
                    Descripcion = lector["estado_descripcion"] != DBNull.Value ? (string)lector["estado_descripcion"] : null
                }
            };

            if (cargarCompleto)
            {
                // Cargar método de envío
                presupuesto.MetodoEnvio = new EnvioTipo
                {
                    Id = (int)lector["id_metodo_envio"],
                    Nombre = (string)lector["envio_nombre"],
                    Costo = (decimal)lector["envio_costo"]
                };

                // Cargar forma de pago
                presupuesto.FormaPago = new PresupuestoFormaPago
                {
                    Id = (int)lector["id_forma_pago"],
                    Nombre = (string)lector["forma_pago_nombre"],
                    Descripcion = lector["forma_pago_descripcion"] != DBNull.Value ? (string)lector["forma_pago_descripcion"] : null
                };

                // Cargar dirección de envío
                presupuesto.DomicilioEnvio = new ClienteDomicilioEnvio
                {
                    Id = (int)lector["id_cliente_envio"],
                    Calle = (string)lector["calle"],
                    Altura = (int)lector["altura"],
                    EntreCalles = lector["entre_calles"] != DBNull.Value ? (string)lector["entre_calles"] : null,
                    Piso = lector["piso"] != DBNull.Value ? (int?)lector["piso"] : null,
                    Departamento = lector["departamento"] != DBNull.Value ? (string)lector["departamento"] : null,
                    Localidad = (string)lector["localidad"],
                    Provincia = (string)lector["provincia"],
                    CodigoPostal = (string)lector["codigo_postal"]
                };

                // Cargar detalles
                presupuesto.Detalles = CargarDetallesPresupuesto(presupuesto.Id);
            }

            return presupuesto;
        }

        private List<PresupuestoDetalle> CargarDetallesPresupuesto(int idPresupuesto)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = @"
                SELECT pd.*, p.nombre as producto_nombre
                FROM presupuesto_detalle pd
                INNER JOIN Producto p ON pd.id_producto = p.id_producto
                WHERE pd.id_presupuesto = @idPresupuesto";

                datos.setearConsulta(consulta);
                datos.setearParametro("@idPresupuesto", idPresupuesto);
                datos.ejecutarLectura();

                List<PresupuestoDetalle> detalles = new List<PresupuestoDetalle>();
                while (datos.Lector.Read())
                {
                    detalles.Add(new PresupuestoDetalle
                    {
                        Id = (int)datos.Lector["id"],
                        IdPresupuesto = idPresupuesto,
                        IdProducto = (int)datos.Lector["id_producto"],
                        PrecioUnitario = (decimal)datos.Lector["precio_unitario"],
                        Cantidad = (int)datos.Lector["cantidad"],
                        Subtotal = (decimal)datos.Lector["subtotal"],
                        FechaAgregado = (DateTime)datos.Lector["fecha_agregado"],
                        AgregadoIdUsuario = (int)datos.Lector["agregado_id_usuario"],
                        NombreProducto = (string)datos.Lector["producto_nombre"]
                    });
                }

                return detalles;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar detalles del presupuesto", ex);
            }
            finally
            {
                datos.cerrarConexion();
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
