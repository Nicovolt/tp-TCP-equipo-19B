using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class MarcaNegocio
    {

        public List<Marca> ListarMarcas()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Marca> marcas = new List<Marca>();
            datos.setearConsulta("select * from Marca");
            try
            {
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Marca marca = new Marca();
                    marca.IdMarca = (int)datos.Lector["id_marca"];
                    marca.Nombre = (string)datos.Lector["nombre"];
                    marcas.Add(marca);
                }
                return marcas;
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
       
        public void Agregar(string nuevaMarca)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("insert into Marca(nombre) values(@nuevaMarca)");
                datos.setearParametro("@nuevaMarca", nuevaMarca);
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

        public void Eliminar(string Marca)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("delete from Marca where nombre = @Marca ");
                datos.setearParametro("@Marca", Marca);
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

        public void Modificar(Marca id, string NewNombre)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Marca SET nombre = @NewNombre WHERE id_marca =  @id ");
                datos.setearParametro("@NewNombre", NewNombre);
                datos.setearParametro("@id", id.IdMarca);
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
