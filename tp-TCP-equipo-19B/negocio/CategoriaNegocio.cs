using dominio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class CategoriaNegocio
    {
        public List<Categoria> ListarCategorias()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Categoria> lista = new List<Categoria>();
            datos.setearConsulta("select * from Categoria");
            try
            {
                datos.ejecutarAccion();
                while (datos.Lector.Read())
                {
                    Categoria categoria = new Categoria();
                    categoria.IdCategoria = (int)datos.Lector["id_categoria"];
                    categoria.Nombre = (string)datos.Lector["nombre"];
                    lista.Add(categoria);
                }
                return lista;
            }
            catch (Exception)
            {

                throw;
            }
            finally 
            {
                datos.cerrarConexion(); 
            }
        }

        public Categoria BuscarPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT id_categoria, nombre FROM Categoria where id_categoria = @ID");
                datos.setearParametro("@ID", id);
                datos.ejecutarLectura();

                Categoria categoria = null;
           
                while (datos.Lector.Read())
                {
                    if (categoria == null)
                    {
                        categoria = new Categoria();
                        categoria.IdCategoria = (int)datos.Lector["id_categoria"];
                        categoria.Nombre = (string)datos.Lector["nombre"];
                    }

                }


                return categoria;
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

        public void Agregar(string NuevaCategoria)
        {
            if (ExisteCategoria(NuevaCategoria))
            {
                throw new Exception("La categoría ya existe.");
            }


            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("insert into Categoria(nombre) values(@NuevaCategoria)");
                datos.setearParametro("@NuevaCategoria", NuevaCategoria);
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

        public void Eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("delete from Categoria where id_categoria = @id ");
                datos.setearParametro("@id", id);
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


        public void Modificar(Categoria id, string NewNombre)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Categoria SET nombre = @NewNombre WHERE id_categoria =  @id ");
                datos.setearParametro("@NewNombre", NewNombre);
                datos.setearParametro("@id", id.IdCategoria);
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

        public bool ExisteCategoria(string nombreCategoria)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM Categoria WHERE nombre = @NombreCategoria");
                datos.setearParametro("@NombreCategoria", nombreCategoria);
                datos.ejecutarLectura();
                if (datos.Lector.Read())
                {
                    return (int)datos.Lector[0] > 0;
                }
                return false;
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
