using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tp_TCP_equipo_19B
{
    public partial class Formulario_web1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ValidarSesion())
                    Response.Redirect("Login.aspx");
                if (!ValidarCarrito())
                    Response.Redirect("CarritoCompras.aspx");

                CargarCarrito();
                CargarDirecciones();
                CargarMetodosEnvio();
                CargarFormasPago();
                MostrarTotales();
            }

        }

        private bool ValidarSesion()
        {
            return Session["usuario"] != null;
        }

        private bool ValidarCarrito()
        {
            return Session["CarritoCompras"] != null;
        }

        private void CargarCarrito()
        {
            var carrito = Session["CarritoCompras"] as List<Productos>;
            rptCarrito.DataSource = carrito;
            rptCarrito.DataBind();
        }

        private void CargarDirecciones()
        {
            try
            {
                var usuario = (dynamic)Session["usuario"];
                ClienteDomicilioEnvioNegocio negocio = new ClienteDomicilioEnvioNegocio();
                var direcciones = negocio.ListarPorCliente(usuario.IdCliente);

                if (direcciones == null || !direcciones.Any())
                {
                    MostrarMensaje("No tienes direcciones guardadas.", "warning");
                    Response.Redirect("Cuenta.aspx");
                }

                rptDirecciones.DataSource = direcciones;
                rptDirecciones.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar direcciones: " + ex.Message, "danger");
            }
        }

        private int ObtenerDireccionSeleccionada()
        {
            foreach (RepeaterItem item in rptDirecciones.Items)
            {
                RadioButton rb = (RadioButton)item.FindControl("rbDireccion");
                if (rb != null && rb.Checked)
                {
                    return int.Parse(rb.Attributes["Value"]);
                }
            }
            throw new Exception("Debe seleccionar una dirección de envío");
        }

        private void CargarMetodosEnvio()
        {
            try
            {
                EnvioNegocio negocio = new EnvioNegocio();
                var metodosEnvio = negocio.ListarActivos();
                rptMetodosEnvio.DataSource = metodosEnvio;
                rptMetodosEnvio.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar métodos de envío: " + ex.Message, "danger");
            }
        }

        private void CargarFormasPago()
        {
            try
            {
                PresupuestoFormaPagoNegocio negocio = new PresupuestoFormaPagoNegocio();
                var formasPago = negocio.ListarActivas();
                rptFormasPago.DataSource = formasPago;
                rptFormasPago.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar formas de pago: " + ex.Message, "danger");
            }
        }

        private decimal calcularTotal()
        {
            var carrito = Session["CarritoCompras"] as List<Productos>;
            decimal subtotal = carrito.Sum(p => p.Precio * p.Cantidad);
            decimal costoEnvio = ObtenerCostoEnvio();
            decimal total = subtotal + costoEnvio;
            return total;
        }

        private void MostrarTotales()
        {
            try
            {
                var carrito = Session["CarritoCompras"] as List<Productos>;
                decimal subtotal = carrito.Sum(p => p.Precio * p.Cantidad);
                decimal costoEnvio = ObtenerCostoEnvio();
                decimal total = subtotal + costoEnvio;

                lblSubtotal.Text = subtotal.ToString("N2");
                lblCostoEnvio.Text = costoEnvio.ToString("N2");
                lblTotal.Text = total.ToString("N2");
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al calcular totales: " + ex.Message, "danger");
            }
        }

        private decimal ObtenerCostoEnvio()
        {
            try
            {
                int idMetodoEnvio = ObtenerMetodoEnvioSeleccionado();
                if (idMetodoEnvio > 0)
                {
                    EnvioNegocio negocio = new EnvioNegocio();
                    return negocio.getCostoByIdEnvio(idMetodoEnvio);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al obtener los costos de envio" + ex.Message, "danger");
            }
            return 0;
        }

        protected void ddlDirecciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            MostrarTotales();
        }

        protected void rbEnvio_CheckedChanged(object sender, EventArgs e)
        {
            MostrarTotales();
        }

        private int ObtenerMetodoEnvioSeleccionado()
        {
            foreach (RepeaterItem item in rptMetodosEnvio.Items)
            {
                RadioButton rb = (RadioButton)item.FindControl("rbEnvio");
                if (rb != null && rb.Checked)
                    return int.Parse(rb.Attributes["Value"]);
            }
            return 0;
        }

        private int ObtenerFormaPagoSeleccionada()
        {
            foreach (RepeaterItem item in rptFormasPago.Items)
            {
                RadioButton rb = (RadioButton)item.FindControl("rbPago");
                if (rb != null && rb.Checked)
                    return int.Parse(rb.Attributes["Value"]);
            }
            throw new Exception("Debe seleccionar una forma de pago");
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                MostrarMensaje("Por favor complete todos los campos requeridos", "warning");
                return;
            }

            try
            {

                int metodoEnvio = ObtenerMetodoEnvioSeleccionado();
                if (metodoEnvio == 0)
                    throw new Exception("Debe seleccionar un método de envío");

                dynamic usuario = Session["usuario"];
                int idFormaPago = ObtenerFormaPagoSeleccionada();
                int idDomicilio = ObtenerDireccionSeleccionada();
                var carrito = Session["CarritoCompras"] as List<Productos>;

                // Crear presupuesto
                PresupuestoNegocio presupuestoNegocio = new PresupuestoNegocio();
                Presupuesto presupuesto = presupuestoNegocio.Crear(usuario.IdCliente, metodoEnvio, idFormaPago, idDomicilio, carrito);

                //actualizar total
                presupuestoNegocio.ActualizarTotal(presupuesto.Id, calcularTotal());

                // Agregar detalles
                PresupuestoDetalleNegocio detalleNegocio = new PresupuestoDetalleNegocio();
                detalleNegocio.AgregarDetallePresupuesto(presupuesto.Id, carrito, usuario.IdUsuario);

                restarStock(carrito);

                // Limpiar carrito y redirigir
                Session["CarritoCompras"] = null;

                EnviarMailConfirmacionCompra(usuario.IdCliente);


                Response.Redirect("Default.aspx");
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al procesar la compra: " + ex.Message, "danger");
            }
        }

        protected void restarStock(List<Productos> produ)
        {
            ProductoNegocio pro = new ProductoNegocio();
            foreach (var producto in produ)
            {

                var productoActualizado = pro.buscarPorID(producto.Id_producto);
                int sto, newsto;
                sto = productoActualizado.stock;
                newsto = producto.Cantidad;

                productoActualizado.stock = sto - newsto;

                pro.ModificarStock(productoActualizado);
            }
        }
        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("CarritoCompras.aspx");
        }

        private void MostrarMensaje(string mensaje, string tipo)
        {
            pnlMensaje.CssClass = $"alert alert-{tipo} alert-dismissible fade show";
            lblMensaje.Text = mensaje;
            pnlMensaje.Visible = true;
        }

        public string GetDireccionCompleta(ClienteDomicilioEnvio dir)
        {
            return $"{dir.Calle} {dir.Altura}, {dir.Localidad}, {dir.Provincia} " +
                   $"{(!string.IsNullOrEmpty(dir.Departamento) ? $"Depto: {dir.Departamento}" : "")} " +
                   $"{(dir.Piso.HasValue ? $"Piso: {dir.Piso}" : "")}";
        }

        private void EnviarMailConfirmacionCompra(int idCliente)
        {
            ClienteNegocio clienteNegocio = new ClienteNegocio();
            ServicioEmail servicioEmail = new ServicioEmail();

            try
            {
                Cliente cliente = new Cliente();
                cliente = clienteNegocio.ObtenerClientePorId(idCliente);
                servicioEmail.ConfirmarCompra(cliente.Mail, cliente.Nombre, cliente.Apellido, cliente.Telefono);
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al enviar correo del cliente: " + ex.Message, "danger");
            }
        }
    }
}