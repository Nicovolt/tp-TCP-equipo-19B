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
    public partial class _Default : Page
    {

        public List<Productos> ListProductos = new List<Productos>();
        private ProductoNegocio ProductoNegocio = new ProductoNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProductos(); // Método para cargar los productos


                ListProductos = ProductoNegocio.listar();
                repProductos.DataSource = ListProductos;
                repProductos.DataBind();
            }
        }

        private void CargarProductos()
        {
            ProductoNegocio productoNegocio = new ProductoNegocio();
            List<Productos> productos = productoNegocio.listar(); // Llama al método listar
            repProductos.DataSource = productos; // Asigna la fuente de datos
            repProductos.DataBind(); // Realiza el enlace de datos
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
                string ProductoID = e.CommandArgument.ToString();
                EliminarProducto(ProductoID);
                CargarProductos();
             
            }
        }

       
        private void EliminarProducto(string productoId)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
           
                try
                {
                    accesoDatos.setearConsulta("DELETE FROM producto WHERE Id_producto = @Id_producto");
                    accesoDatos.setearParametro("@Id_producto", productoId);

                    accesoDatos.ejecutarAccion();
                }
                catch (Exception ex)
                {
      
                }
                finally
                {
                    accesoDatos.cerrarConexion(); 
                }
        }
    }
}
