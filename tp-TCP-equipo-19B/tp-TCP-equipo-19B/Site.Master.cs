using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;

namespace tp_TCP_equipo_19B
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ActualizarEstadoSesion();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string busqueda = searchTextBox.Text;


        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("Default.aspx");
        }

        private void ActualizarEstadoSesion()
        {
            if (Session["usuario"] != null)
            {
                // Obtener datos de la sesión
                dynamic usuario = Session["usuario"];

                // Obtener el cliente de la base de datos para tener nombre y apellido
                ClienteNegocio clienteNegocio = new ClienteNegocio();
                Cliente cliente = clienteNegocio.ObtenerClientePorId(usuario.IdCliente);

                // Actualizar la UI
                lblUsuario.Text = $"{cliente.Nombre} {cliente.Apellido}";

                // Mostrar/ocultar elementos según estado de sesión
                liLogin.Visible = false;
                liPerfil.Visible = true;
                liPedidos.Visible = true;
                liCerrarSesion.Visible = true;


                if (usuario.EsAdmin)
                {
                    adminMenu.Visible = true;
                }
                else
                {
                    adminMenu.Visible = false;
                }
            }
            else
            {
                lblUsuario.Text = "Mi Cuenta";
                liLogin.Visible = true;
                liPerfil.Visible = false;
                liPedidos.Visible = false;
                liCerrarSesion.Visible = false;
                adminMenu.Visible = false;
            }
        }
    }
}