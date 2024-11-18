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
    public partial class _Default : System.Web.UI.Page
    {

        public List<Productos> ListProductos = new List<Productos>();
        private ProductoNegocio ProductoNegocio = new ProductoNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProductos(); 
                CarcarCategoria();
                CarcarMarca();
                ListProductos = ProductoNegocio.listar();
                repProductos.DataSource = ListProductos;
                repProductos.DataBind();
                


            }
        }

        private void CargarProductos()
        {
            ProductoNegocio productoNegocio = new ProductoNegocio();
            List<Productos> productos = productoNegocio.listar(); 
            repProductos.DataSource = productos; 
            repProductos.DataBind(); 
        }


        protected void repeaterProducto_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "idModificar")
            {
                string ProductoID = e.CommandArgument.ToString();
                Response.Redirect($"Productos.aspx?id={ProductoID}");
            }
            if (e.CommandName == "idBorrar")
            {
                ProductoNegocio proNeg = new ProductoNegocio();
                int ProductoID = int.Parse(e.CommandArgument.ToString());
                proNeg.Eliminar(ProductoID);
                CargarProductos();
             
            }
            if (e.CommandName == "VerDetalle")
            {
                string ProductoID = e.CommandArgument.ToString();
                Response.Redirect($"VerDetalle.aspx?id={ProductoID}");
            }
        }

        private void CarcarCategoria()
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            ddlCategorias.DataSource = categoriaNegocio.ListarCategorias();
            ddlCategorias.DataTextField = "nombre";
            ddlCategorias.DataValueField = "IdCategoria";
            ddlCategorias.DataBind();

        }

        private void CarcarMarca()
        {
            MarcaNegocio MarcaNegocio = new MarcaNegocio();
            ddlMarcas.DataSource = MarcaNegocio.ListarMarcas();
            ddlMarcas.DataTextField = "nombre";
            ddlMarcas.DataValueField = "IdMarca";
            ddlMarcas.DataBind();

        }

        protected void ddlCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            int categoriaID = int.Parse(ddlCategorias.SelectedValue);
            CargarProductosCategoria(categoriaID);
        }


        private void CargarProductosCategoria(int categoriaID)
        {
            ProductoNegocio productoNegocio = new ProductoNegocio();
            List<Productos> productos;

            if (categoriaID == 0)
            {
               
                productos = productoNegocio.listar();
            }
            else
            {
               
                productos = productoNegocio.listarPorCategoria(categoriaID);
            }

            repProductos.DataSource = productos;
            repProductos.DataBind();
        }

        protected void ddlMarcas_SelectedIndexChanged(object sender, EventArgs e)
        {
            int marcaID = int.Parse(ddlMarcas.SelectedValue);
            CargarProductosMarca(marcaID);
        }

        private void CargarProductosMarca(int marcaID)
        {
            ProductoNegocio productoNegocio = new ProductoNegocio();
            List<Productos> productos;

            if (marcaID == 0)
            {

                productos = productoNegocio.listar();
            }
            else
            {

                productos = productoNegocio.listarPorMarca(marcaID);
            }

            repProductos.DataSource = productos;
            repProductos.DataBind();
        }

        protected void repProductos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Button btnBorrar = (Button)e.Item.FindControl("btnBorrar");
                Button btnModificar = (Button)e.Item.FindControl("btnModificar");
                Button btnDetalle = (Button)e.Item.FindControl("btnDetalle");
                Button btnCarrito = (Button)e.Item.FindControl("btnCarrito");
                dynamic User = Session["usuario"];
                if (User != null || User.EsAdmin==false)
                {

                    btnBorrar.Visible = false;
                    btnModificar.Visible = false;
                    btnDetalle.Visible = false;
                    btnCarrito.Visible = false;

                }
                else 
                {
                    btnBorrar.Visible = false;
                    btnModificar.Visible = false;
                    btnDetalle.Visible = false;
                    btnCarrito.Visible = false;

                }
            }
        }
    }
}
