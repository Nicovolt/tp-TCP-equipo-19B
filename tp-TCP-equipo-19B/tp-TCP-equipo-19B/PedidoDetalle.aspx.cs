using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using static dominio.Enums;

namespace tp_TCP_equipo_19B
{
    public partial class PedidoDetalle : System.Web.UI.Page
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
                CargarDetallePedido();
            }
        }

        private bool ValidarSesion()
        {
            return Session["usuario"] != null;
        }

        private void CargarDetallePedido()
        {
            try
            {
                if (!int.TryParse(Request.QueryString["id"], out int idPresupuesto))
                {
                    Response.Redirect("Pedidos.aspx");
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
                //PresupuestoDetalleNegocio presupuestoDetalleNegocio = new PresupuestoDetalleNegocio();
                //List<PresupuestoDetalle> detalle = presupuestoDetalleNegocio.ListarDetalles(pedido.Id);

                //pedido.Detalles = detalle;


                ltlNroPedido.Text = pedido.Id.ToString();
                ltlFecha.Text = pedido.FechaCreacion.ToString("dd/MM/yyyy HH:mm");
                ltlEstado.Text = pedido.Estado.Nombre;
                ltlUltimaActualizacion.Text = pedido.UltimaActualizacion.ToString("dd/MM/yyyy HH:mm");

                badgeEstado.Attributes["class"] = $"badge {GetEstadoClass(pedido.IdEstado)}";
                badgeEstado.InnerText = pedido.Estado.Nombre;


                rptDetalles.DataSource = pedido.Detalles;
                rptDetalles.DataBind();

                ltlMetodoEnvio.Text = pedido.MetodoEnvio.Nombre;
                ltlCostoEnvio.Text = pedido.CostoEnvio.ToString("N2");

                var direccion = pedido.DomicilioEnvio;
                ltlDireccion.Text = $"{direccion.Calle} {direccion.Altura}, " +
                                   $"{(!string.IsNullOrEmpty(direccion.Departamento) ? $"Depto {direccion.Departamento}, " : "")}" +
                                   $"{direccion.Localidad}, {direccion.Provincia}";

                ltlMetodoPago.Text = pedido.FormaPago.Nombre;

                decimal subtotal = pedido.Detalles.Sum(d => d.Subtotal);
                ltlSubtotal.Text = subtotal.ToString("N2");
                ltlEnvio.Text = pedido.CostoEnvio.ToString("N2");
                ltlTotal.Text = pedido.Total.ToString("N2");
            }
            catch (Exception ex)
            {
                MostrarError("Error al cargar el detalle del pedido: " + ex.Message);
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pedidos.aspx");
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