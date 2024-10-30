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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProductos(); // Método para cargar los productos
            }
        }

        private void CargarProductos()
        {
            ProductoNegocio productoNegocio = new ProductoNegocio();
            List<Productos> productos = productoNegocio.listar(); // Llama al método listar
            repProductosSorteo.DataSource = productos; // Asigna la fuente de datos
            repProductosSorteo.DataBind(); // Realiza el enlace de datos
        }

        
    }
}