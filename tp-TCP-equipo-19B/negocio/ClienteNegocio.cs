using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class ClienteNegocio
    {
        public int getIdClienteByMail(string email)
        {   
            int id = 0;
            AccesoDatos data = new AccesoDatos();

            try
            {
                data.setearConsulta("select id_cliente from Cliente c where email like @email@;");
                data.setearParametro("@email@", email);
                data.ejecutarLectura();
                while (data.Lector.Read())
                {
                    id = (int)data.Lector["id_cliente"];
                }
                return id;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al intentar obtener el cliente por el email", ex);
            }
            finally
            {
                data.cerrarConexion();
            }
        }

        public int crearCliente(string nombre, string apellido, string email, string telefono)
        {
            AccesoDatos data = new AccesoDatos();

            try
            {
                //Validamos que no exista el cliente
                if (getIdClienteByMail(email) != 0)
                {
                    return 0;
                }

                data.setearConsulta("INSERT Cliente (nombre,apellido,email,telefono,admin) VALUES (@nombre,@apellido,@email,@telefono,1);");
                data.setearParametro("@nombre",nombre);
                data.setearParametro("@apellido",apellido);
                data.setearParametro("@email",email);
                data.setearParametro("@telefono",telefono);
                data.ejecutarAccion();

                if (getIdClienteByMail(email) != 0)
                {
                    return getIdClienteByMail(email);
                }

                return 0;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al intentar crear cliente", ex);
            }
            finally
            {
                data.cerrarConexion();
            }
        }
    }
}
