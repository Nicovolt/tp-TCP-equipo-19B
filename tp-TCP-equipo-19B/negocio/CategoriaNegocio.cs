using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class CategoriaNegocio
    {
        AccesoDatos AccesoDatos = new AccesoDatos();
        public void Agregar(string NuevaCategoria)
        {
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

        public void Eliminar(string Categoria)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("delete from Categoria where nombre = @Categoria ");
                datos.setearParametro("@Categoria", Categoria);
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


        public void Modificar(string Nombre, string NewNombre)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Categoria SET nombre = @NewNombre WHERE nombre =  @Nombre ");
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
