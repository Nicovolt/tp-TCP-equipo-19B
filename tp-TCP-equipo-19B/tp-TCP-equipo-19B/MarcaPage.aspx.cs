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
    public partial class MarcaPage : System.Web.UI.Page
    {
        AccesoDatos datos = new AccesoDatos();  
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            string Nueva = txtMarca.Text;
            MarcaNegocio marcaNegocio = new MarcaNegocio();
           
            marcaNegocio.Agregar(Nueva);

        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            string marca = txtMarca.Text;
            MarcaNegocio marcaNegocio = new MarcaNegocio();

            marcaNegocio.Eliminar(marca);
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            string Marca = txtMarca.Text;
            string Nueva = txtNuevaMarca.Text;

            MarcaNegocio marcaNegocio = new MarcaNegocio();

            marcaNegocio.Modificar(Marca, Nueva);
        }
    }
}