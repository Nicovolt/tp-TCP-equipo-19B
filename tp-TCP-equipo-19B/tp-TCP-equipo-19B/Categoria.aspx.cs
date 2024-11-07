using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tp_TCP_equipo_19B
{
    public partial class Formulario_web12 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCategoria();
            }

        }
        private void CargarCategoria()
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            ddlCategoria.DataSource = categoriaNegocio.ListarCategorias();
            ddlCategoria.DataTextField = "Nombre";
            ddlCategoria.DataValueField = "IdCategoria";
            ddlCategoria.DataBind();
            ddlCategoria.Items.Insert(0, new ListItem("Seleccione la Categoria", ""));
        }

        protected void Agregar(object sender, EventArgs e)
        {
            CategoriaNegocio categoria = new CategoriaNegocio();

            string nueva = inpCat.Text;

            categoria.Agregar(nueva);

            inpCat.Text = "";
            CargarCategoria();

        }

        protected void Modificar(object sender, EventArgs e)
        {
            CategoriaNegocio categorianegocio = new CategoriaNegocio();
            try
            {
                if (string.IsNullOrEmpty(ddlCategoria.SelectedValue))
                {
                    return;
                }

                Categoria cat = new Categoria();
                cat.IdCategoria = int.Parse(ddlCategoria.SelectedValue);
                string nueva = inpNombreCategoriaNueva.Text;

                categorianegocio.Modificar(cat, nueva);
                CargarCategoria();

            }
            catch (Exception ex)
            {

                throw new Exception("error al modificar la categoria", ex);
            }
        }

        protected void Eliminar(object sender, EventArgs e)
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

            try
            {
                if (string.IsNullOrEmpty(ddlCategoria.SelectedValue))
                {
                    return;
                }

                int id = int.Parse(ddlCategoria.SelectedValue);
                categoriaNegocio.Eliminar(id);
                CargarCategoria();


            }
            catch (SqlException ex)
            {
                // Verifica si la excepción es debido a la restricción de clave externa
                if (ex.Message.Contains("REFERENCE constraint"))
                {
                    // Muestra un mensaje al usuario
                    lblMensajeError.Text = "Hay productos con esta categoría. Primero elimina los productos asociados.";
                    lblMensajeError.Visible = true;
                }

            }
            catch (Exception ex)
            {

                throw new Exception("error al eliminar la categoria", ex);
            }
        }
    }
}



