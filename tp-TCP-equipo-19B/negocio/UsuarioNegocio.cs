using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class UsuarioNegocio
    {
        public Usuario login(string mail, string pass)
        {
            AccesoDatos data = new AccesoDatos();
            ClienteNegocio clienteNegocio = new ClienteNegocio();
            try
            {
                Usuario aux = new Usuario();
                int id_cliente = clienteNegocio.getIdClienteByMail(mail);
                if (id_cliente == 0)
                {
                    return aux;
                }

                data.setearProcedimiento("sp_VerificarLogin");
                data.setearParametro("@id_cliente", id_cliente);
                data.setearParametro("@contrasena", pass);
                data.setearParametroSalida("@loginExitoso", SqlDbType.Bit);

                data.ejecutarAccion();

                // Obtenemos el valor del parámetro de salida
                bool loginExitoso = Convert.ToBoolean(data.obtenerParametroSalida("@loginExitoso"));

                if (loginExitoso)
                {
                    // Guardamos la información del usuario en la sesión
                    aux = getUserByIdCliente(id_cliente);
                }
                return aux;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al intentar realizar el login", ex);
            }
            finally
            {
                data.cerrarConexion();
            }
        }

        public bool CrearUsuario(int idCliente, string contrasena)
        {
            AccesoDatos data = new AccesoDatos();

            try
            {
                data.setearProcedimiento("sp_InsertarUsuario");
                data.setearParametro("@idCliente", idCliente);
                data.setearParametro("@contrasena", contrasena);
                data.ejecutarAccion();
                return true;
            }
            catch (Exception ex)
            {
                // Manejar el error según tu estrategia de logging
                throw new Exception("Error al crear el usuario", ex);
            }
        }

        private Usuario getUserByIdCliente(int id_cliente)
        {
            AccesoDatos data = new AccesoDatos();
            Usuario aux = new Usuario();
            try
            {
                data.setearConsulta("select u.id_usuario, u.id_cliente, c.email, u.contrasena, u.admin from Usuario u left join Cliente c on c.id_cliente = u.id_cliente where c.id_cliente = @id_cliente@;");
                data.setearParametro("@id_cliente@",id_cliente);
                data.ejecutarLectura();

                while (data.Lector.Read())
                {
                    aux.IdUsuario = (int)data.Lector["id_usuario"];
                    aux.IdCliente = (int)data.Lector["id_cliente"];
                    aux.Mail = (string)data.Lector["email"];
                    aux.Contrasena = (byte[])data.Lector["contrasena"];
                    aux.Admin = (bool)data.Lector["admin"];
                }

                return aux;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al obtener el usuario", ex);
            }
        }
    }
}
