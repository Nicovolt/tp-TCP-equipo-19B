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
    public partial class Formulario_web1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarcarCategoria();
                CarcarMarca();
            }


        }



        private void CarcarCategoria()
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            ddlCategoria.DataSource = categoriaNegocio.ListarCategorias();
            ddlCategoria.DataTextField = "nombre";
            ddlCategoria.DataValueField = "IdCategoria";
            ddlCategoria.DataBind();

        }

        private void CarcarMarca()
        {
            MarcaNegocio MarcaNegocio = new MarcaNegocio();
            ddlMarca.DataSource = MarcaNegocio.ListarMarcas();
            ddlMarca.DataTextField = "nombre";
            ddlMarca.DataValueField = "IdMarca";
            ddlMarca.DataBind();

        }

        protected void AgregarPro(object sender, EventArgs e)
        {

            Productos productoNuevo = new Productos();
            productoNuevo.Nombre = inpNombrePro.Text;
            productoNuevo.Descripcion = inpDescripcion.Text;
            productoNuevo.Id_categoria = int.Parse(ddlCategoria.SelectedValue);
            productoNuevo.Id_marca = int.Parse(ddlMarca.SelectedValue);
            productoNuevo.PorsentajeDescuento = 0;
            productoNuevo.Precio = decimal.Parse(inpPrecio.Text);
            productoNuevo.stock = int.Parse(inpStock.Text);
            productoNuevo.ListaImagenes = new List<Imagen>
                {
                    new Imagen { ImagenUrl = inpImagen.Text }
                };

            ProductoNegocio negocio = new ProductoNegocio();    
            negocio.Agregar(productoNuevo);
            try
            {
               

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Producto agregado exitosamente!');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error al agregar el producto: " + ex.Message + "');", true);
            }

        }
    }
}