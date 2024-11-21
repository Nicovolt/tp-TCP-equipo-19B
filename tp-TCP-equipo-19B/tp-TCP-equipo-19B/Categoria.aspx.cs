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

            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

            try
            {
                string nueva = inpCat.Text;

                if (string.IsNullOrWhiteSpace(nueva))
                {
                    lblError.Text = "El nombre de la categoría no puede estar vacío.";
                    lblError.Visible = true;
                    return;
                }

                categoriaNegocio.Agregar(nueva);

                inpCat.Text = "";
                lblError.Visible = false;
                CargarCategoria();
            }
            catch (Exception ex)
            {
                if (ex.Message == "La categoría ya existe.")
                {
                    lblError.Text = "Ya existe una categoría con este nombre.";
                }
                else
                {
                    lblError.Text = "Ocurrió un error al agregar la categoría.";
                }
                lblError.Visible = true;
            }

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
                if (ex.Message.Contains("REFERENCE constraint"))
                {
                    // Muestra un mensaje al usuario
                    lblMensajeError.Text = "Hay productos con esta Categoria. Primero elimina los productos asociados.";
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



