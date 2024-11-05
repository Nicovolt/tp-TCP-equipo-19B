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
    public partial class Formulario_web11 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CarcarMarca();
        }

        protected void Agregar(object sender, EventArgs e)
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio(); 
            


            string nueva = inpNombreMar.Text;
          

            marcaNegocio.Agregar(nueva);
        }

        protected void Modificar(object sender, EventArgs e)
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            
            Marca marca = new Marca();
            marca.IdMarca = int.Parse(ddlMarcape.SelectedValue);
            string Nueva = inpNombreMarcaNueva.Text;


            marcaNegocio.Modificar(marca, Nueva);

        }

        private void CarcarMarca()
        {
            MarcaNegocio MarcaNegocio = new MarcaNegocio();
            ddlMarcape.DataSource = MarcaNegocio.ListarMarcas();
            ddlMarcape.DataTextField = "nombre";
            ddlMarcape.DataValueField = "IdMarca";
            ddlMarcape.DataBind();

        }
    }
}