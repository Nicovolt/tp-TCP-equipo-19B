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

        public List<UsuarioDetalle> listarUsuariosDetalle()
        {
            List<UsuarioDetalle> list = new List<UsuarioDetalle>();
            AccesoDatos data = new AccesoDatos();

            try
            {
                string query = "SELECT u.id_usuario, u.id_cliente, CONCAT(c.apellido,' ', c.nombre) as nombre_completo, c.email, u.admin FROM Usuario u LEFT JOIN Cliente c ON C.id_cliente = U.id_cliente;";

                data.setearConsulta(query);
                data.ejecutarLectura();

                while (data.Lector.Read())
                {
                    UsuarioDetalle aux = new UsuarioDetalle();
                    aux.IdUsuario = (int)data.Lector["id_usuario"];
                    aux.IdCliente = (int)data.Lector["id_cliente"];
                    aux.NombreCompleto = (string)data.Lector["nombre_completo"];
                    aux.Mail = (string)data.Lector["email"];
                    aux.Admin = (bool)data.Lector["admin"];

                    list.Add(aux);
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar usuarios", ex);
            }
            finally
            {
                data.cerrarConexion();
            }
        }

        public void ActualizarEstadoAdmin(int idUsuario, bool esAdmin)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "UPDATE Usuario SET admin = @admin WHERE id_usuario = @id";
                datos.setearConsulta(consulta);
                datos.setearParametro("@admin", esAdmin ? 1 : 0);
                datos.setearParametro("@id", idUsuario);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar estado de administrador", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public bool ValidarPassword(int idCliente, string password)
        {
            AccesoDatos datos = new AccesoDatos();
            {
                try
                {
                    datos.setearProcedimiento("sp_VerificarLogin");
                    datos.setearParametro("@id_cliente", idCliente);
                    datos.setearParametro("@contrasena", password);
                    datos.setearParametro("@loginExitoso", SqlDbType.Bit);

                    datos.ejecutarAccion();

                    bool loginExitoso = Convert.ToBoolean(datos.obtenerParametroSalida("@loginExitoso"));

                    return loginExitoso;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al validar la contrasena", ex);
                }
                finally
                {
                    datos.cerrarConexion();
                }
            }
        }

        public void CambiarPassword(int idCliente, string nuevaPassword)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Usuario SET contrasena = HASHBYTES('SHA2_256', @password) WHERE id_cliente = @idCliente");
                datos.setearParametro("@idCliente", idCliente);
                datos.setearParametro("@password", nuevaPassword);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cambiar la contraseña", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
