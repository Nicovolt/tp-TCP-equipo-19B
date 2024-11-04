using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tp_TCP_equipo_19B
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Si ya hay sesión activa, redirigir al home
                if (VerificarSesion())
                {
                    Response.Redirect("Cuenta.aspx");
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                Usuario user = usuarioNegocio.login(txtMail.Text, txtPassword.Text);

                if (user.IdUsuario != 0)
                {
                    GuardarSesion(user.IdCliente,user.Mail);
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    MostrarMensaje("Usuario o contraseña incorrectos");
                }
            }
            catch (Exception)
            {
                MostrarMensaje("Error al intentar iniciar sesión");
            }
        }

        private void MostrarMensaje(string mensaje)
        {
            pnlMensajes.Visible = true;
            lblMensaje.Text = mensaje;
        }

        public void GuardarSesion(int id_cliente, string mail)
        {
            // Crear objeto con información básica del usuario
            var sesionUsuario = new
            {
                IdCliente = id_cliente,
                Mail = mail,
                FechaLogin = DateTime.Now
            };

            Session.Add("user",sesionUsuario);
        }

        private bool VerificarSesion()
        {
            return Session["user"] != null;
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            ClienteNegocio clienteNegocio = new ClienteNegocio();
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            try
            {
                int id_cliente = clienteNegocio.crearCliente(tbxNombre.Text, tbxApellido.Text, txtMail.Text, tbxTelefono.Text);

                if (usuarioNegocio.CrearUsuario(id_cliente, txtPassword.Text))
                {
                    MostrarMensaje("Usuario creado correctamente, loguese por favor!");
                }
            }
            catch (Exception)
            {

                MostrarMensaje("Error al intentar registrarse");
            }
        }
    }
}