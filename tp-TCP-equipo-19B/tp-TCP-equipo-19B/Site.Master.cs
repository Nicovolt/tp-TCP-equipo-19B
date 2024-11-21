using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using System.Reflection.Emit;

namespace tp_TCP_equipo_19B
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<dominio.Productos> carritoActual = (List<dominio.Productos>)Session["CarritoCompras"];
                int cantArticulos = carritoActual != null ? carritoActual.Count : 0;

                ActualizarEstadoSesion();
                ActualizarContadorCarrito(cantArticulos);
            }
        }

        public void ActualizarContadorCarrito(int contador)
        {
            LabelCompras.Text = contador.ToString();
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
                
                dynamic usuario = Session["usuario"];

                
                ClienteNegocio clienteNegocio = new ClienteNegocio();
                Cliente cliente = clienteNegocio.ObtenerClientePorId(usuario.IdCliente);

                lblUsuario.Text = $"{cliente.Nombre} {cliente.Apellido}";

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