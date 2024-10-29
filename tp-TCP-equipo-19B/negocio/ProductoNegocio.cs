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

            try
            {
                datos.setearConsulta("SELECT P.id_producto, P.nombre, P.descripcion, P.precio, P.id_marca, M.Descripcion AS MarcaDescripcion, P.id_categoria, C.Descripcion AS CategoriaDescripcion, P.porcentaje_descuento, P.stock, P.activo FROM PRODUCTOS P, CATEGORIAS C, MARCAS M WHERE P.id_marca = M.Id AND P.id_categoria = C.Id;");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Productos aux = new Productos();
                    aux.Id_producto = (int)datos.Lector["id_producto"]; // Asignando el id del producto
                    aux.Nombre = (string)datos.Lector["nombre"];
                    aux.Descripcion = (string)datos.Lector["descripcion"];
                    aux.Precio = (decimal)datos.Lector["precio"]; // Usamos decimal para precios
                    aux.Id_categoria = (int)datos.Lector["id_categoria"];
                    aux.Id_marca = (int)datos.Lector["id_marca"];

                    // Si tienes un método para obtener las imágenes, lo asignamos
                   // aux.ListarImagen = imagenNegocio.listaImagenesPorArticulo(aux.Id_producto);

                    // Añadimos el producto a la lista
                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex; // Propagamos la excepción si algo sale mal
            }
            finally
            {
                datos.cerrarConexion(); // Cerramos la conexión a la base de datos
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
