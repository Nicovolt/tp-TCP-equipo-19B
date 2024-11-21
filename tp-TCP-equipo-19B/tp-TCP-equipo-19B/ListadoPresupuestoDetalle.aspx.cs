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
    public partial class ListadoPresupuestos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ValidarSesion() || !EsUsuarioAdmin())
            {
                Response.Redirect("Default.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarEstados();
                CargarDetallePresupuesto();
            }
        }

        private bool ValidarSesion()
        {
            return Session["usuario"] != null;
        }

        private bool EsUsuarioAdmin()
        {
            if (Session["usuario"] == null) return false;
            dynamic usuario = Session["usuario"];
            return usuario.EsAdmin;
        }

        private void CargarEstados()
        {
            try
            {
                var negocio = new PresupuestoEstadoNegocio();
                ddlEstado.DataSource = negocio.ListarEstados();
                ddlEstado.DataTextField = "Nombre";
                ddlEstado.DataValueField = "Id";
                ddlEstado.DataBind();
            }
            catch (Exception ex)
            {
                MostrarError("Error al cargar estados: " + ex.Message);
            }
        }

        private void CargarDetallePresupuesto()
        {
            try
            {
                if (!int.TryParse(Request.QueryString["id"], out int idPresupuesto))
                {
                    Response.Redirect("ListadoPresupuesto.aspx");
                    return;
                }

                dynamic usuario = Session["usuario"];
                PresupuestoNegocio negocio = new PresupuestoNegocio();
                Presupuesto pedido = negocio.ObtenerPorId(idPresupuesto);

                if (pedido == null || pedido.IdCliente != usuario.IdCliente)
                {
                    Response.Redirect("Pedidos.aspx");
                    return;
                }

                if (pedido == null)
                {
                    Response.Redirect("ListadoPresupuesto.aspx");
                    return;
                }

                ltlNroPresupuesto.Text = pedido.Id.ToString();
                ltlCliente.Text = $"{pedido.Cliente.Nombre} {pedido.Cliente.Apellido}";
                ltlFecha.Text = pedido.FechaCreacion.ToString("dd/MM/yyyy HH:mm");
                ltlEstado.Text = pedido.Estado.Nombre;
                ltlUltimaActualizacion.Text = pedido.UltimaActualizacion.ToString("dd/MM/yyyy HH:mm");

                ddlEstado.SelectedValue = pedido.IdEstado.ToString();
                badgeEstado.Attributes["class"] = $"badge {GetEstadoClass(pedido.IdEstado)}";
                badgeEstado.InnerText = pedido.Estado.Nombre;

                rptProductos.DataSource = pedido.Detalles;
                rptProductos.DataBind();

                ltlMetodoPago.Text = pedido.FormaPago.Nombre;
                ltlTotal.Text = pedido.Total.ToString("N2");
            }
            catch (Exception ex)
            {
                MostrarError("Error al cargar el detalle: " + ex.Message);
            }
        }

        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idPresupuesto = int.Parse(Request.QueryString["id"]);
                int nuevoEstado = int.Parse(ddlEstado.SelectedValue);

                PresupuestoNegocio negocio = new PresupuestoNegocio();
                negocio.ActualizarEstado(idPresupuesto, nuevoEstado);

                CargarDetallePresupuesto();
            }
            catch (Exception ex)
            {
                MostrarError("Error al actualizar estado: " + ex.Message);
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListadoPresupuesto.aspx");
        }

        private string GetEstadoClass(object estado)
        {
            if (estado == null) return "badge-secondary";

            switch (Convert.ToInt32(estado))
            {
                case EnumPresupuestoEstado.Creado:
                    return "badge-pending";
                case EnumPresupuestoEstado.Pagado:
                    return "badge-success";
                case EnumPresupuestoEstado.Vencido:
                    return "badge-danger";
                case EnumPresupuestoEstado.Cancelado:
                    return "badge-dark";
                case EnumPresupuestoEstado.Armado:
                    return "badge-info";
                case EnumPresupuestoEstado.Embalado:
                    return "badge-primary";
                case EnumPresupuestoEstado.Despachado:
                    return "badge-purple";
                case EnumPresupuestoEstado.Entregado:
                    return "badge-success";
                default:
                    return "badge-secondary";
            }
        }

        private void MostrarError(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "error",
                $"Swal.fire('Error', '{mensaje}', 'error');", true);
        }
    }
}