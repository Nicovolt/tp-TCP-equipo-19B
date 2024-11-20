using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class EnvioNegocio
    {
        public List<EnvioTipo> ListarActivos()
        {
            AccesoDatos datos = new AccesoDatos();
            List<EnvioTipo> lista = new List<EnvioTipo>();
            try
            {
                datos.setearConsulta("SELECT id, nombre, url_imagen, costo FROM envio_tipo WHERE activo = 1;");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    lista.Add(new EnvioTipo
                    {
                        Id = (int)datos.Lector["id"],
                        Nombre = (string)datos.Lector["nombre"],
                        UrlImagen = (string)datos.Lector["url_imagen"],
                        Costo = (decimal)datos.Lector["costo"]
                    });
                }

                return lista;
            }
            catch(Exception ex)
            {
                throw new Exception("Error listar los servicios de mensajeria: " + ex.Message);
            }
        }

        public decimal getCostoByIdEnvio(int id)
        {
            AccesoDatos data = new AccesoDatos();
            decimal costo = 0;
            try
            {
                data.setearConsulta("SELECT costo FROM envio_tipo WHERE id = @id;");
                data.setearParametro("@id",id);
                data.ejecutarLectura();

                while (data.Lector.Read())
                {
                    costo = (decimal)data.Lector["costo"];
                }

                return costo;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al recopilar el costo del servicio de mensajeria: " + ex.Message);
            }
        }

        public EnvioTipo getEnvioById(int id)
        {
            AccesoDatos data = new AccesoDatos();
            EnvioTipo envio = new EnvioTipo();

            try
            {
                data.setearConsulta("SELECT id, nombre, url_imagen, costo FROM envio_tipo WHERE id = @id;");
                data.setearParametro("@id", id);
                data.ejecutarLectura();

                while (data.Lector.Read())
                {
                    envio.Id = (int)data.Lector["id"];
                    envio.Nombre = (string)data.Lector["costo"];
                    envio.UrlImagen = (string)data.Lector["costo"];
                    envio.Costo = (decimal)data.Lector["costo"];
                }

                return envio;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al recopilar el servicio de mensajeria: " + ex.Message);
            }
        }
    }
}
