using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class ProductoNegocio
    {
        AccesoDatos AccesoDatos = new AccesoDatos();


        public List<Productos> listar()
        {
            List<Productos> lista = new List<Productos>();
            AccesoDatos datos = new AccesoDatos();
            ImagenNegocio imagenNegocio = new ImagenNegocio(); 

            try
            {
                datos.setearConsulta("SELECT id_producto, nombre, descripcion, precio, porcentaje_descuento, id_marca, id_categoria FROM Producto");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Productos aux = new Productos();
                    aux.Id_producto = (int)datos.Lector["id_producto"];
                    aux.Nombre = (string)datos.Lector["nombre"];
                    aux.Descripcion = (string)datos.Lector["descripcion"];
                    aux.Precio = (decimal)datos.Lector["precio"];  // Convertir explícitamente a double
                    aux.PorsentajeDescuento = datos.Lector["porcentaje_descuento"] != DBNull.Value ? (int)(byte)datos.Lector["porcentaje_descuento"] : 0; // Manejo de tinyint
                    aux.Id_marca = datos.Lector["id_marca"] != DBNull.Value ? (int)datos.Lector["id_marca"] : 0;
                    aux.Id_categoria = datos.Lector["id_categoria"] != DBNull.Value ? (int)datos.Lector["id_categoria"] : 0;

                    aux.ListaImagenes = imagenNegocio.listaImagenesPorArticulo(aux.Id_producto);

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }






        public void Agregar(Productos producto)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO Producto(nombre, descripcion, precio, id_marca, id_categoria, stock) " +
                                     "VALUES(@nombre, @Descripcion, @Precio, @Id_marca, @Id_categoria, @stock); " +
                                     "SELECT SCOPE_IDENTITY();");  

                datos.setearParametro("@nombre", producto.Nombre);
                datos.setearParametro("@Descripcion", producto.Descripcion);
                datos.setearParametro("@Precio", producto.Precio);
                datos.setearParametro("@Id_marca", producto.Id_marca);
                datos.setearParametro("@Id_categoria", producto.Id_categoria);
                datos.setearParametro("@stock", producto.stock);

                datos.ejecutarLectura();
                int idProducto = 0;
                if (datos.Lector.Read())
                {
                    idProducto = Convert.ToInt32(datos.Lector[0]);
                }

                AgregarImagenes(idProducto, producto.ListaImagenes);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el producto y sus imágenes: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void AgregarImagenes(int idProducto, List<Imagen> imagenes)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // poner imagenes asociadas al producto
                foreach (var imagen in imagenes)
                {
                    datos.setearConsulta("INSERT INTO Imagen(idProducto, ImagenUrl) VALUES(@idProducto, @ImagenUrl)");
                    datos.setearParametro("@idProducto", idProducto); 
                    datos.setearParametro("@ImagenUrl", imagen.ImagenUrl);  
                    datos.ejecutarAccion(); 
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar las imágenes: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public Productos buscarPorID(int Id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT id_producto, nombre, descripcion, precio, porcentaje_descuento, id_marca, id_categoria FROM Producto where id_producto = @ID");
                datos.setearParametro("@ID", Id);
                datos.ejecutarLectura();

                Productos producto = null;
                ImagenNegocio imagenNegocio = new ImagenNegocio();
                while (datos.Lector.Read())
                {
                    if (producto == null)
                    {
                        producto = new Productos();
                        producto.Id_producto = (int)datos.Lector["id_producto"];
                        producto.Nombre = (string)datos.Lector["nombre"];
                        producto.Descripcion = (string)datos.Lector["descripcion"];
                        producto.Id_categoria = (int)datos.Lector["id_categoria"];
                        producto.Id_marca = (int)datos.Lector["id_marca"];
                        producto.Precio = (decimal)datos.Lector["precio"];

                        producto.ListaImagenes = imagenNegocio.listaImagenesPorArticulo(producto.Id_producto);
                    }

                }

                return producto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

        }



        public void Eliminar(string producto)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("delete from Producto where nombre = @producto ");
                datos.setearParametro("@producto", producto);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Modificar(Productos pro)  
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Producto SET nombre = @NewNombre,  descripcion = @NewDes, precio = @NewPre,stock = @NewStock, id_marca = @NewMarc, id_categoria = @NewCat WHERE id_producto =  @id ");
                datos.setearParametro("@NewNombre", pro.Nombre);
                datos.setearParametro("@NewDes", pro.Descripcion);
                datos.setearParametro("@NewPre", pro.Precio);
                datos.setearParametro("@NewMarc", pro.Id_marca);
                datos.setearParametro("@NewCat", pro.Id_categoria);
                datos.setearParametro("@NewStock", pro.stock);
                datos.setearParametro("@id", pro.Id_producto);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
