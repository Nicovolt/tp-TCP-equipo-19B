using negocio;
using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tp_TCP_equipo_19B
{
    public partial class Formulario_web14 : System.Web.UI.Page
    {
        private ProductoNegocio ProductoNegocio = new ProductoNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Session["CarritoCompras"] == null)
                {
                    List<dominio.Productos> Newcarrito = new List<dominio.Productos>();
                    Session.Add("CarritoCompras", Newcarrito);
                }

                CargarCarrito();
                actualizarTotalCarrito();

                if (Session["TotalCarrito"] != null)
                {
                    lblPrecioTotal.Text = Session["TotalCarrito"].ToString();
                }
            }
        }
        private void EliminarArticulo(Productos articulo)
        {
            List<dominio.Productos> carrito = new List<dominio.Productos>();
            carrito = (List<dominio.Productos>)Session["CarritoCompras"];

            for (int i = 0; i < carrito.Count; i++)
            {
                if (carrito[i].Id_producto == articulo.Id_producto)
                {
                    carrito.RemoveAt(i);
                    
                }
            }

            List<Productos> carritoActual = (List<Productos>)Session["CarritoCompras"];
            int cantArticulos = carritoActual.Count;

            SiteMaster masterPage = (SiteMaster)this.Master;
            masterPage.ActualizarContadorCarrito(cantArticulos);
        }

        private void CargarCarrito()
        {
            List<dominio.Productos> carrito = (List<dominio.Productos>)Session["CarritoCompras"];
            repeaterCarrito.DataSource = carrito;
            repeaterCarrito.DataBind();
        }

        private void actualizarTotalCarrito()
        {
            List<dominio.Productos> carrito = (List<dominio.Productos>)Session["CarritoCompras"];
            decimal totalCarrito = 0;

            foreach (dominio.Productos item in carrito)
            {
                totalCarrito += item.Cantidad * item.Precio;
            }
            Session["TotalCarrito"] = totalCarrito.ToString("0.00");

            lblPrecioTotal.Text = totalCarrito.ToString("0.00");
        }

        protected void btnAumentarCantidad_Click(object sender, EventArgs e)
        {
            int id = int.Parse(((Button)sender).CommandArgument);
            List<dominio.Productos> carrito = (List<dominio.Productos>)Session["CarritoCompras"];

            dominio.Productos articulo = carrito.FirstOrDefault(a => a.Id_producto == id);
            if (articulo != null)
            {
                articulo.Cantidad += 1;
            }

            CargarCarrito();
            actualizarTotalCarrito();
        }

        protected void btnDisminuirCantidad_Click(object sender, EventArgs e)
        {
            int id = int.Parse(((Button)sender).CommandArgument);
            List<dominio.Productos> carrito = (List<dominio.Productos>)Session["CarritoCompras"];

            dominio.Productos articulo = carrito.FirstOrDefault(a => a.Id_producto == id);
            if (articulo != null && articulo.Cantidad > 1)
            {
                articulo.Cantidad -= 1;
            }

            CargarCarrito();
            actualizarTotalCarrito();
        }

        private void ActualizarContadorCarrito(int cantArticulos)
        {
        
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            int id = int.Parse(((Button)sender).CommandArgument);
            dominio.Productos articulo = new dominio.Productos();
            articulo = ProductoNegocio.buscarPorID(id);
            List<dominio.Productos> carrito = new List<dominio.Productos>();
            carrito = (List<dominio.Productos>)Session["CarritoCompras"];

            EliminarArticulo(articulo);
            CargarCarrito();
            actualizarTotalCarrito();
            ActualizarContadorCarrito(carrito.Count);

        }

        protected void btnComprar_Click(object sender, EventArgs e)
        {
            List<Productos> carrito = (List<Productos>)Session["CarritoCompras"];

            if (carrito == null || carrito.Count == 0)
            {
                lblError.Text = "El carrito está vacío. Agrega productos antes de realizar una compra.";
                lblError.Visible = true;
                return;
            }

            if (Session["usuario"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                Session["CarritoCompras"] = null;
                Response.Redirect("Compras.aspx");
            }
        }

    }
}