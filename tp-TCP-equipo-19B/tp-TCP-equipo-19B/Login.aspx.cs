using Antlr.Runtime.Tree;
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
        public enum TipoMensaje
        {
            Error,
            Exito,
            Informacion,
            Advertencia
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (VerificarSesion())
                {
                    Response.Redirect("Cuenta.aspx");
                }
            }

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Page.Validate("LoginGroup");
            if (!Page.IsValid)
            {
                MostrarMensaje("Por favor, complete los campos de login correctamente.", TipoMensaje.Advertencia);
                return;
            }
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            try
            {
                Usuario user = usuarioNegocio.login(txtMail.Text, txtPassword.Text);

                if (user.IdUsuario != 0)
                {
                    GuardarSesion(user.IdCliente, user.Mail, user.Admin);
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    MostrarMensaje("Usuario o contraseña incorrectos", TipoMensaje.Advertencia);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al intentar iniciar sesión: " + ex.Message, TipoMensaje.Error);
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Page.Validate("RegisterGroup");
            if (!Page.IsValid)
            {
                MostrarMensaje("Por favor, complete todos los campos de registro correctamente.", TipoMensaje.Advertencia);
                return;
            }

            ClienteNegocio clienteNegocio = new ClienteNegocio();
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();

            try
            {
                int id = clienteNegocio.getIdClienteByMail(txtRegEmail.Text);
                if (id != 0)
                {
                    MostrarMensaje("El correo electronico ingresado ya se encuentra asociado a una cuenta.", TipoMensaje.Advertencia);
                    return;
                }

                int id_cliente = clienteNegocio.crearCliente(tbxNombre.Text, tbxApellido.Text, txtRegEmail.Text, tbxTelefono.Text);

                if (usuarioNegocio.CrearUsuario(id_cliente, txtRegPassword.Text))
                {
                    MostrarMensaje("Usuario creado correctamente, loguese por favor!", TipoMensaje.Exito);
                    LimpiarCamposRegistro();
                }
            }
            catch (Exception ex)
            {

                MostrarMensaje("Error al intentar registrarse: " + ex.Message, TipoMensaje.Error);
            }
        }

        private void LimpiarCamposRegistro()
        {
            tbxNombre.Text = string.Empty;
            tbxApellido.Text = string.Empty;
            tbxTelefono.Text = string.Empty;
            txtRegEmail.Text = string.Empty;
            txtRegPassword.Text = string.Empty;
        }

        private void MostrarMensaje(string mensaje, TipoMensaje tipo = TipoMensaje.Error)
        {
            pnlMensajes.Visible = true;
            lblMensaje.Text = mensaje;
            string cssClass = "";

            // Asignar la clase de Bootstrap según el tipo de mensaje
            switch (tipo)
            {
                case TipoMensaje.Error:
                    cssClass = "alert alert-danger";
                    break;
                case TipoMensaje.Exito:
                    cssClass = "alert alert-success";
                    break;
                case TipoMensaje.Informacion:
                    cssClass = "alert alert-info";
                    break;
                case TipoMensaje.Advertencia:
                    cssClass = "alert alert-warning";
                    break;
            }

            pnlMensajes.CssClass = cssClass;

            ScriptManager.RegisterStartupScript(this, GetType(), "scrollToTop",
                "window.scrollTo(0, 0);", true);
        }

        public void GuardarSesion(int id_cliente, string mail, bool admin)
        {
            // Crear objeto con información básica del usuario
            var sesionUsuario = new
            {
                IdCliente = id_cliente,
                Mail = mail,
                FechaLogin = DateTime.Now,
                EsAdmin = admin,
            };

            Session.Add("usuario",sesionUsuario);
        }

        private bool validarCamposTxt(TextBox textBox)
        {
            if (textBox == null) { return false; }
            if (textBox.Text.Length == 0) { return false; }
            return true;
        }

        private bool VerificarSesion()
        {
            return Session["usuario"] != null;
        }
    }
}