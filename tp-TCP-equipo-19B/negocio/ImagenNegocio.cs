using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class ImagenNegocio
    {
        public List<Imagen> listarTodos()
        {
            List<Imagen> imagenes = new List<Imagen>();
            AccesoDatos data = new AccesoDatos();

            try
            {
                data.setearProcedimiento("sp_ListarImagenes");
                data.ejecutarLectura();
                while (data.Lector.Read())
                {
                    Imagen imagen = new Imagen();
                    imagen.Id = (int)data.Lector["Id"];
                    imagen.IdProducto = (int)data.Lector["IdProducto"];
                    imagen.UrlImagen = (string)data.Lector["UrlImagen"];
                    imagen.Activo = (bool)data.Lector["activo"];

                    imagenes.Add(imagen);
                }

                return imagenes;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar las imágenes", ex);
            }
            finally
            {
                data.cerrarConexion();
            }
        }

        public List<Imagen> listarActivos()
        {
            List<Imagen> imagenes = new List<Imagen>();
            AccesoDatos data = new AccesoDatos();

            try
            {
                data.setearProcedimiento("sp_ListarImagenes");
                data.ejecutarLectura();
                while (data.Lector.Read())
                {
                    Imagen imagen = new Imagen();

                    imagen.Id = (int)data.Lector["Id"];
                    imagen.IdProducto = (int)data.Lector["IdProducto"];
                    imagen.UrlImagen = (string)data.Lector["UrlImagen"];
                    imagen.Activo = (bool)data.Lector["activo"];

                    if (imagen.Activo)
                    {
                        imagenes.Add(imagen);
                    }
                }

                return imagenes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.cerrarConexion();
            }
        }
    }
}
