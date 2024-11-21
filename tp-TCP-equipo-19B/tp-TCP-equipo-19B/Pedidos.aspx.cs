using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static dominio.Enums;

namespace tp_TCP_equipo_19B
{
    public partial class Pedidos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ValidarSesion())
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarPedidos();
            }
        }

        private bool ValidarSesion()
        {
            return Session["usuario"] != null;
        }

        private void CargarPedidos()
        {
            try
            {
                dynamic usuario = Session["usuario"];
                PresupuestoNegocio presupuestoNegocio = new PresupuestoNegocio();

                // Obtener filtro de ordenamiento
                string ordenamiento = ddlOrden.SelectedValue;

                List<Presupuesto> pedidos = presupuestoNegocio.ListarPorCliente(usuario.IdCliente, ordenamiento);

                if (pedidos.Any())
                {
                    rptPedidos.DataSource = pedidos;
                    rptPedidos.DataBind();
                    pnlNoResults.Visible = false;
                }
                else
                {
                    rptPedidos.DataSource = null;
                    rptPedidos.DataBind();
                    pnlNoResults.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MostrarError("Error al cargar pedidos: " + ex.Message);
            }
        }

        protected void ddlOrden_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarPedidos();
        }

        public static string GetEstadoClass(object estado)
        {
            if (estado == null) return "badge-secondary";

            switch (Convert.ToInt32(estado))
            {
                case EnumPresupuestoEstado.Creado:
                    return "badge-pending";    // azul claro
                case EnumPresupuestoEstado.Pagado:
                    return "badge-success";    // verde
                case EnumPresupuestoEstado.Vencido:
                    return "badge-danger";     // rojo
                case EnumPresupuestoEstado.Cancelado:
                    return "badge-dark";       // negro/gris oscurito
                case EnumPresupuestoEstado.Armado:
                    return "badge-info";       // azul informativo
                case EnumPresupuestoEstado.Embalado:
                    return "badge-primary";    // azul fuerte
                case EnumPresupuestoEstado.Despachado:
                    return "badge-purple";     // violeta
                case EnumPresupuestoEstado.Entregado:
                    return "badge-success";    // verde
                default:
                    return "badge-secondary";  // gris clarito
            }
        }

        private void MostrarError(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "error",
                $"Swal.fire('Error', '{mensaje}', 'error');", true);
        }
    }
}