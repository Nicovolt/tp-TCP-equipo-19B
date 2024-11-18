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
    public partial class Formulario_web13 : System.Web.UI.Page
    {
        private int ObtenerElIdDelArticuloDesdeLaURL()
        {
            int idProducto = -1;
            if (Request.QueryString["id"] != null)
            {
                if (int.TryParse(Request.QueryString["id"], out idProducto))
                {
                }
            }
            return idProducto;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ObtenerElIdDelArticuloDesdeLaURL() != -1)
            {
                
                if (!IsPostBack)
                {
                    actualizarVisualizacionBotones();
                    string articuloID = Request.QueryString["id"];
                    if (!string.IsNullOrEmpty(articuloID))
                    {
                        CargarDetalleArticulo(articuloID);
                    }
                }
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }

        private void CargarDetalleArticulo(string articuloID)
        { 
            Marca marca = new Marca();
            Categoria categoria = new Categoria();
            ProductoNegocio ProductoNegocio = new ProductoNegocio();
            Productos Productos = ProductoNegocio.buscarPorID(int.Parse(articuloID));
            lblNombreArticulo.Text = Productos.Nombre;
            lblDescripcionArticulo.Text = Productos.Descripcion;
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            categoria= categoriaNegocio.BuscarPorId(Productos.Id_categoria);
            lblCategoriaArticulo.Text = categoria.Nombre.ToString();    
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            marca= marcaNegocio.BuscarPorId(Productos.Id_marca);
            lblMarcaArticulo.Text = marca.Nombre.ToString();
            lblPrecioArticulo.Text = Productos.Precio.ToString();

            repeaterImagenes.DataSource = Productos.ListaImagenes.Select(i => new { UrlImagen = i.ImagenUrl });
            repeaterImagenes.DataBind();
        }

        protected void btnCarrito_Click(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CarritoCompras"] == null)
                {
                    List<Productos> Newcarrito = new List<Productos>();
                    Session["CarritoCompras"] = Newcarrito;
                }
            }
            int id = ObtenerElIdDelArticuloDesdeLaURL();
            if (id > 0)
            {
                ProductoNegocio negocio = new ProductoNegocio();
                Productos producto = negocio.buscarPorID(id);
                producto.stock = 1;

                if (producto != null)
                {
                    List<Productos> carrito = Session["CarritoCompras"] as List<Productos>;
                    if (carrito == null)
                    {
                        carrito = new List<Productos>();
                    }
                    carrito.Add(producto);
                    Session["CarritoCompras"] = carrito;

                   
                }
            }
        }

        protected void btnBorrar_Click(object sender, EventArgs e)
        {
            ProductoNegocio proNeg = new ProductoNegocio();
            int idProducto;
            int.TryParse(Request.QueryString["id"], out idProducto);
            proNeg.Eliminar(idProducto);
            Response.Redirect("Default.aspx");
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            int idProducto;
            int.TryParse(Request.QueryString["id"], out idProducto);
            Response.Redirect($"Productos.aspx?id={idProducto}");
        }

        protected void actualizarVisualizacionBotones()
        {
            bool mostrarBotones = false;

            if (Session["usuario"] != null)
            {
                dynamic usuario = Session["usuario"];
                mostrarBotones = usuario.EsAdmin;
            }

            btnBorrar.Visible = mostrarBotones;
            btnModificar.Visible = mostrarBotones;
        }
    }
}