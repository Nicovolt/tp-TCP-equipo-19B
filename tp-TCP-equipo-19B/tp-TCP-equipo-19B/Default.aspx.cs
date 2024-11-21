using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

            if (e.CommandName == "AgregarAlCarrito")
            {
                if (Session["CarritoCompras"] == null)
                {
                    Session["CarritoCompras"] = new List<Productos>();
                }

                int id = Convert.ToInt32(e.CommandArgument.ToString());

                Productos producto = ProductoNegocio.buscarPorID(id);
                producto.Cantidad = 1;

                if (producto != null)
                {
                    List<Productos> carrito = Session["CarritoCompras"] as List<Productos>;

                    Productos productoExistente = carrito.FirstOrDefault(p => p.Id_producto == id);

                    if (productoExistente != null)
                    {
                        productoExistente.Cantidad++;
                    }
                    else
                    {
                        producto.Cantidad = 1;
                        carrito.Add(producto);
                    }

                    Session["CarritoCompras"] = carrito;

                    List<Productos> carritoActual = (List<Productos>)Session["CarritoCompras"];
                    int cantArticulos = carritoActual.Count;


                    Button btnCarrito = (Button)e.CommandSource;
                    btnCarrito.Text = "Agregado ✓";

                    SiteMaster masterPage = (SiteMaster)this.Master;
                    masterPage.ActualizarContadorCarrito(cantArticulos);

                }

            }

        }

        private void CarcarCategoria()
        {

            if (ddlCategorias.Items.Count == 0)
            {
                ddlCategorias.Items.Add(new ListItem("Todas las categorias", "0"));
            }


            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            List<Categoria> categorias = categoriaNegocio.ListarCategorias();
            foreach (var categoria in categorias)
            {
                ddlCategorias.Items.Add(new ListItem(categoria.Nombre, categoria.IdCategoria.ToString()));
            }

        }

        private void CarcarMarca()
        {

            if (ddlMarcas.Items.Count == 0)
            {
                ddlMarcas.Items.Add(new ListItem("Todas las marcas", "0"));
            }


            MarcaNegocio marcaNegocio = new MarcaNegocio();
            List<Marca> marcas = marcaNegocio.ListarMarcas();
            foreach (var marca in marcas)
            {
                ddlMarcas.Items.Add(new ListItem(marca.Nombre, marca.IdMarca.ToString()));
            }

        }

        protected void ddlCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            int categoriaID = int.Parse(ddlCategorias.SelectedValue);


            if (categoriaID == 0)
            {
                CargarProductos();
            }
            else
            {
                ddlMarcas.SelectedIndex = 0;
                CargarProductosCategoria(categoriaID);
            }
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
            int categoriaID = int.Parse(ddlCategorias.SelectedValue);

            if (categoriaID == 0 && marcaID == 0)
            {

                CargarProductos();
            }
            else if (categoriaID != 0 && marcaID == 0)
            {

                CargarProductosCategoria(categoriaID);
            }
            else if (categoriaID == 0 && marcaID != 0)
            {

                CargarProductosMarca(marcaID);
            }
            else
            {

                CargarProductosCategoriaMarca(categoriaID, marcaID);
            }
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
        private void CargarProductosCategoriaMarca(int categoriaID, int marcaID)
        {
            ProductoNegocio productoNegocio = new ProductoNegocio();
            List<Productos> productos;


            productos = productoNegocio.listarPorCategoriaYMarca(categoriaID, marcaID);

            repProductos.DataSource = productos;
            repProductos.DataBind();
        }


        protected void ddlPrecio_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ordernPrecio = int.Parse(ddlPrecio.SelectedValue);


            if (ordernPrecio == 0)
            {
                CargarProductos();
            }
            else
            {
                CargarProductosPrecio(ordernPrecio);
            }

        }

        private void CargarProductosPrecio(int x)
        {
            ProductoNegocio productoNegocio = new ProductoNegocio();
            List<Productos> productos;

            if (x == 1)
            {

                productos = productoNegocio.listarPorMenorPrecio();
                repProductos.DataSource = productos;
            }
            if (x == 2)
            {

                productos = productoNegocio.listarPorMayorPrecio();
                repProductos.DataSource = productos;
            }
            if (x == 0)
            {
                CargarProductos();
            }


            repProductos.DataBind();
        }
    }
}
