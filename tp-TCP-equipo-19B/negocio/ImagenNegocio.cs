using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
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
                    imagen.ImagenUrl = (string)data.Lector["UrlImagen"];
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
                    imagen.ImagenUrl = (string)data.Lector["UrlImagen"];
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

        public void actualizarImagen(Imagen img)
        {
            AccesoDatos data = new AccesoDatos();

            try
            {
                data.setearProcedimiento("sp_ActualizarImagen");
                data.setearParametro("@Id", img.Id);
                data.setearParametro("@IdProducto", img.IdProducto);
                data.setearParametro("@UrlImagen", img.ImagenUrl);
                data.setearParametro("@activo", img.Activo);
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
                data.setearConsulta("SELECT Id, IdProducto, ImagenUrl, Activo FROM Imagen WHERE IdProducto = @IdProducto");
                data.setearParametro("@IdProducto", id);
                data.ejecutarLectura();

                while (data.Lector.Read())
                {
                    Imagen imagen = new Imagen();
                    imagen.Id = (int)data.Lector["Id"];

                    // Manejar la posibilidad de que IdProducto sea nulo
                    imagen.IdProducto = data.Lector["IdProducto"] != DBNull.Value ? (int)data.Lector["IdProducto"] : 0; // O cualquier otro valor por defecto que necesites

                    // Asegúrate de que ImagenUrl nunca sea nulo
                    imagen.ImagenUrl = (string)data.Lector["ImagenUrl"]; // Este campo no debería ser nulo, pero se puede manejar

                    // Manejar el campo Activo
                    imagen.Activo = (byte)data.Lector["Activo"] != 0; // Convertimos tinyint a bool

                    imagenes.Add(imagen);
                }

                return imagenes;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar imágenes por artículo", ex);
            }
            finally
            {
                data.cerrarConexion();
            }
        }

        public void eliminarImagenesProducto(int idProducto)
        {
            AccesoDatos data = new AccesoDatos();
            try
            {
                string query = "DELETE FROM Imagen WHERE IdProducto = @id_producto;";
                data.setearConsulta(query);
                data.setearParametro("@id_producto", idProducto);
                data.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar las imágenes del articulo" + idProducto, ex);
            }
            finally
            {
                data.cerrarConexion();
            }
        }

        public string listarUna(int idProducto)
        {
            AccesoDatos datos = new AccesoDatos(); 
            Imagen imagen = new Imagen();

            try
            {
                datos.setearConsulta("SELECT TOP 1 * FROM IMAGEN WHERE IdProducto = @IdProducto;");
                datos.setearParametro("@IdProducto", idProducto);  

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    imagen.Id = datos.Lector.GetInt32(0);
                    imagen.IdProducto = datos.Lector.GetInt32(1);

                    if (!datos.Lector.IsDBNull(datos.Lector.GetOrdinal("ImagenUrl")))
                    {
                        string url = datos.Lector.GetString(datos.Lector.GetOrdinal("ImagenUrl"));
                        if (UrlExiste(url))  
                        {
                            imagen.ImagenUrl = url;
                        }
                        else
                        {
                            imagen.ImagenUrl = "https://static.vecteezy.com/system/resources/previews/004/141/669/non_2x/no-photo-or-blank-image-icon-loading-images-or-missing-image-mark-image-not-available-or-image-coming-soon-sign-simple-nature-silhouette-in-frame-isolated-illustration-vector.jpg";
                        }
                    }
                    else
                    {
                        imagen.ImagenUrl = "https://static.vecteezy.com/system/resources/previews/004/141/669/non_2x/no-photo-or-blank-image-icon-loading-images-or-missing-image-mark-image-not-available-or-image-coming-soon-sign-simple-nature-silhouette-in-frame-isolated-illustration-vector.jpg";
                    }
                }
                else
                {
                    imagen.ImagenUrl = "https://static.vecteezy.com/system/resources/previews/004/141/669/non_2x/no-photo-or-blank-image-icon-loading-images-or-missing-image-mark-image-not-available-or-image-coming-soon-sign-simple-nature-silhouette-in-frame-isolated-illustration-vector.jpg";
                }

                datos.cerrarConexion();

                return imagen.ImagenUrl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool UrlExiste(string url)
        {
            try
            {
                Uri uri = new Uri(url);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.Method = "HEAD";

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    return response.StatusCode == HttpStatusCode.OK;
                }
            }
            catch (UriFormatException)
            {
                return false;
            }
            catch (WebException)
            {
                return false;
            }
        }


    }
}
