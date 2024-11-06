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
            if (!IsPostBack)
            {
                CarcarMarca();
            }
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
            try
            {
                if (string.IsNullOrEmpty(ddlMarcape.SelectedValue))
                {
                    // Mostrar mensaje de error - debes seleccionar una marca
                    return;
                }

                if (string.IsNullOrEmpty(inpNombreMarcaNueva.Text))
                {
                    // Mostrar mensaje de error - debes ingresar un nuevo nombre
                    return;
                }


                Marca marca = new Marca();
                marca.IdMarca = int.Parse(ddlMarcape.SelectedValue);
                string Nueva = inpNombreMarcaNueva.Text;


                marcaNegocio.Modificar(marca, Nueva);
            }
            catch (Exception ex)
            {
                throw new Exception("error al modificar la marca", ex);
            }
        }

        private void CarcarMarca()
        {
            MarcaNegocio MarcaNegocio = new MarcaNegocio();
            ddlMarcape.DataSource = MarcaNegocio.ListarMarcas();
            ddlMarcape.DataTextField = "nombre";
            ddlMarcape.DataValueField = "IdMarca";
            ddlMarcape.DataBind();
            ddlMarcape.Items.Insert(0, new ListItem("Seleccione la Marca", "")); // Para que al momento de cargar aparezca este texto.
        }

        protected void Eliminar(object sender, EventArgs e)
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            try
            {
                if (string.IsNullOrEmpty(ddlMarcape.SelectedValue))
                {
                    // Mostrar mensaje de error - debes seleccionar una marca
                    return;
                }

               


              
                int id = int.Parse(ddlMarcape.SelectedValue);
                string Nueva = inpNombreMarcaNueva.Text;


                marcaNegocio.Eliminar(id);
            }
            catch (Exception ex)
            {
                throw new Exception("error al eliminar la marca", ex);
            }
        }
    }
}