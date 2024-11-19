using dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class ClienteDomicilioEnvioNegocio
    {
        public List<ClienteDomicilioEnvio> ListarPorCliente(int idCliente)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT * FROM cliente_dom_envio WHERE id_cliente = @idCliente AND activo = 1");
                datos.setearParametro("@idCliente", idCliente);
                datos.ejecutarLectura();

                List<ClienteDomicilioEnvio> lista = new List<ClienteDomicilioEnvio>();
                while (datos.Lector.Read())
                {
                    lista.Add(CargarDomicilio(datos.Lector));
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar direcciones", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Agregar(ClienteDomicilioEnvio domicilio)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "INSERT INTO cliente_dom_envio (id_cliente, calle, entre_calles, altura, piso, " +
                    "departamento, localidad, provincia, cp, observaciones, activo) VALUES " +
                    "(@idCliente, @calle, @entreCalles, @altura, @piso, @departamento, @localidad, @provincia, " +
                    "@cp, @observaciones, 1)";

                datos.setearConsulta(consulta);
                CargarParametros(datos, domicilio);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar dirección", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Modificar(ClienteDomicilioEnvio domicilio)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "UPDATE cliente_dom_envio SET calle = @calle, entre_calles = @entreCalles, " +
                    "altura = @altura, piso = @piso, departamento = @departamento, localidad = @localidad, " +
                    "provincia = @provincia, cp = @cp, observaciones = @observaciones " +
                    "WHERE id = @id";

                datos.setearConsulta(consulta);
                CargarParametros(datos, domicilio);
                datos.setearParametro("@id", domicilio.Id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar dirección", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE cliente_dom_envio SET activo = 0 WHERE id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar dirección", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public ClienteDomicilioEnvio ObtenerPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT * FROM cliente_dom_envio WHERE id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarLectura();

                ClienteDomicilioEnvio domicilio = null;
                if (datos.Lector.Read())
                {
                    domicilio = CargarDomicilio(datos.Lector);
                }
                return domicilio;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener dirección", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        private ClienteDomicilioEnvio CargarDomicilio(SqlDataReader lector)
        {
            return new ClienteDomicilioEnvio
            {
                Id = (int)lector["id"],
                IdCliente = (int)lector["id_cliente"],
                Calle = (string)lector["calle"],
                EntreCalles = lector["entre_calles"] != DBNull.Value ? (string)lector["entre_calles"] : null,
                Altura = (int)lector["altura"],
                Piso = lector["piso"] != DBNull.Value ? (int?)lector["piso"] : null,
                Departamento = lector["departamento"] != DBNull.Value ? (string)lector["departamento"] : null,
                Localidad = (string)lector["localidad"],
                Provincia = (string)lector["provincia"],
                CodigoPostal = (string)lector["cp"],
                Observaciones = lector["observaciones"] != DBNull.Value ? (string)lector["observaciones"] : null,
                Activo = (bool)lector["activo"]
            };
        }

        private void CargarParametros(AccesoDatos datos, ClienteDomicilioEnvio domicilio)
        {
            datos.setearParametro("@idCliente", domicilio.IdCliente);
            datos.setearParametro("@calle", domicilio.Calle);
            datos.setearParametro("@entreCalles", domicilio.EntreCalles);
            datos.setearParametro("@altura", domicilio.Altura);
            datos.setearParametro("@piso", (object)domicilio.Piso);
            datos.setearParametro("@departamento", domicilio.Departamento);
            datos.setearParametro("@localidad", domicilio.Localidad);
            datos.setearParametro("@provincia", domicilio.Provincia);
            datos.setearParametro("@cp", domicilio.CodigoPostal);
            datos.setearParametro("@observaciones", domicilio.Observaciones);
        }
    }
}
