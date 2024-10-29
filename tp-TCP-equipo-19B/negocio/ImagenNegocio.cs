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
                data.setearProcedimiento("sp_ListarImagenesActivas");
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
                throw new Exception("Error al listar las imágenes activas", ex);
            }
            finally
            {
                data.cerrarConexion();
            }
        }

        public void agregarImagen(int idProducto, string imagenUrl, bool activo)
        {
            AccesoDatos data = new AccesoDatos();

            try
            {
                data.setearProcedimiento("sp_InsertarImagen");
                data.setearParametro("@IdProducto", idProducto);
                data.setearParametro("@ImagenUrl", imagenUrl);
                data.setearParametro("@activo", activo ? 1 : 0);
                data.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar la imagen", ex);
            }
            finally
            {
                data.cerrarConexion();
            }
        }

        public void actualizarImagen(int id, int idProducto, string urlImagen, bool activo)
        {
            AccesoDatos data = new AccesoDatos();

            try
            {
                data.setearProcedimiento("sp_ActualizarImagen");
                data.setearParametro("@Id", id);
                data.setearParametro("@IdProducto", idProducto);
                data.setearParametro("@UrlImagen", urlImagen);
                data.setearParametro("@activo", activo ? 1 : 0);
                data.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la imagen", ex);
            }
            finally
            {
                data.cerrarConexion();
            }
        }

        public void eliminarImagen(int id)
        {
            AccesoDatos data = new AccesoDatos();

            try
            {
                data.setearProcedimiento("sp_EliminarImagen");
                data.setearParametro("@Id", id);
                data.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la imagen", ex);
            }
            finally
            {
                data.cerrarConexion();
            }
        }

        public void actualizarEstadoImagen(int id, bool activo)
        {
            AccesoDatos data = new AccesoDatos();

            try
            {
                data.setearProcedimiento("sp_ActualizarEstadoImagen");
                data.setearParametro("@Id", id);
                data.setearParametro("@Activo", activo ? 1 : 0);
                data.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el estado de la imagen", ex);
            }
            finally
            {
                data.cerrarConexion();
            }
        }

        public List<Imagen> listaImagenesPorArticulo(int id)
        {
            List<Imagen> imagenes = new List<Imagen>();
            AccesoDatos data = new AccesoDatos();

            try
            {
                data.setearProcedimiento("sp_ListarImagenPorArticulo");
                data.setearParametro("@IdProducto", id);
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
                throw new Exception("Error al listar por articulo", ex);
            }
            finally
            {
                data.cerrarConexion();
            }
        }
    }
}
