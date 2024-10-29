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
            ImagenNegocio imagenNegocio = new ImagenNegocio(); // Instanciamos la clase que contiene la función para listar imágenes

            try
            {
                // Cambiamos a la consulta correcta
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

                    // Asignar las imágenes utilizando la función `listaImagenesPorArticulo`
                    aux.ListaImagenes = imagenNegocio.listaImagenesPorArticulo(aux.Id_producto);

                    // Añadimos el producto con las imágenes a la lista
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






        public void Agregar(string NuevoProducto)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("insert into Producto(nombre) values(@NuevoProducto)");
                datos.setearParametro("@NuevoProducto", NuevoProducto);
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

        public void Modificar(string Nombre, string NewNombre)   /// aun falta completar
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Producto SET nombre = @NewNombre WHERE nombre =  @Nombre ");
                datos.setearParametro("@NewNombre", NewNombre);
                datos.setearParametro("@Nombre", Nombre);
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
