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

            if (Request.QueryString["id"] != null && !IsPostBack)
            {
                int id = int.Parse(Request.QueryString["id"]);
                ProductoNegocio ProductoNegocio = new ProductoNegocio();
                Productos Productos = ProductoNegocio.buscarPorID(id);
                inpNombrePro.Text = Productos.Nombre;
                inpDescripcion.Text = Productos.Descripcion;
                ddlCategoria.SelectedValue = Productos.Id_categoria.ToString();
                ddlMarca.SelectedValue = Productos.Id_marca.ToString();
                inpPrecio.Text = Productos.Precio.ToString();
                inpStock.Text = Productos.stock.ToString();

                ImagenNegocio imagenNegocio = new ImagenNegocio();
                List<Imagen> listadoImagenes = imagenNegocio.listaImagenesPorArticulo(id);

                foreach (Imagen img in listadoImagenes)
                {
                    Panel imgContainer = new Panel();
                    imgContainer.CssClass = "d-flex gap-2 mb-2";

                    TextBox txtImagen = new TextBox();
                    txtImagen.ID = "txtImagen_" + img.Id;
                    txtImagen.CssClass = "form-control";
                    txtImagen.Text = img.ImagenUrl;

                    CheckBox chkEliminar = new CheckBox();
                    chkEliminar.ID = "chkEliminar_" + img.Id;
                    chkEliminar.CssClass = "form-check-input ms-2";
                    chkEliminar.Text = "";

                    imgContainer.Controls.Add(txtImagen);
                    imgContainer.Controls.Add(chkEliminar);

                    pnlImagenes.Controls.Add(imgContainer);
                }
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
            try
            {

               
                if (!decimal.TryParse(inpPrecio.Text, out decimal precio) || precio <= 0)
                {
                    lblError.Text = "El precio debe ser un número mayor a 0.";
                    return;
                }

                if (!int.TryParse(inpStock.Text, out int stock) || stock <= 0)
                {
                    lblError.Text = "El stock debe ser un número mayor a 0.";
                    return;
                }


                Productos productoNuevo = new Productos();
            productoNuevo.Nombre = inpNombrePro.Text;
            productoNuevo.Descripcion = inpDescripcion.Text;
            productoNuevo.Id_categoria = int.Parse(ddlCategoria.SelectedValue);
            productoNuevo.Id_marca = int.Parse(ddlMarca.SelectedValue);
            productoNuevo.PorsentajeDescuento = 0;
                productoNuevo.Precio = precio;
                productoNuevo.stock = stock;
            productoNuevo.ListaImagenes = new List<Imagen>
                {
                    new Imagen { ImagenUrl = inpImagen.Text }
                };

            ProductoNegocio negocio = new ProductoNegocio();    
                negocio.Agregar(productoNuevo);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Producto agregado exitosamente!');", true);
                Response.Redirect("default.aspx");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error al agregar el producto: " + ex.Message + "');", true);
            }

        }

        protected void ModificarPro(object sender, EventArgs e)
        {
            Productos producto = new Productos();
            List<Imagen> imagenesModificadas = new List<Imagen>();
            ImagenNegocio imagenNegocio = new ImagenNegocio();
            ProductoNegocio negocio = new ProductoNegocio();

            try
            {
                if (!decimal.TryParse(inpPrecio.Text, out decimal precio) || precio <= 0)
                {
                    lblError.Text = "El precio debe ser un número mayor a 0.";
                    return;
                }

                // Validar que el stock sea un número válido y mayor a 0
                if (!int.TryParse(inpStock.Text, out int stock) || stock <= 0)
                {
                    lblError.Text = "El stock debe ser un número mayor a 0.";
                    return;
                }

                producto.Id_producto = int.Parse(Request.QueryString["id"]);
                producto.Nombre = inpNombrePro.Text;
                producto.Descripcion = inpDescripcion.Text;
                producto.Id_categoria = int.Parse(ddlCategoria.SelectedValue);
                producto.Id_marca = int.Parse(ddlMarca.SelectedValue);
                producto.PorsentajeDescuento = 0;
                producto.Precio = precio;
                producto.stock  = stock;
                //producto.ListaImagenes = new List<Imagen>
                //{
                //    new Imagen { ImagenUrl = inpImagen.Text }
                //};

                foreach (Control container in pnlImagenes.Controls)
                {
                    if (container is Panel imgContainer)
                    {
                        TextBox txtImagen = (TextBox)imgContainer.Controls[0];
                        CheckBox chkEliminar = (CheckBox)imgContainer.Controls[1];

                        int idImagen = int.Parse(txtImagen.ID.Split('_')[1]);

                        if (chkEliminar.Checked)
                        {
                            imagenNegocio.eliminarImagen(idImagen);
                        }
                        else if (txtImagen.Text != "")
                        {
                            Imagen img = new Imagen
                            {
                                Id = idImagen,
                                ImagenUrl = txtImagen.Text,
                                IdProducto = producto.Id_producto
                            };
                            imagenNegocio.actualizarImagen(img);
                        }
                    }
                }

                if (!string.IsNullOrEmpty(inpImagen.Text))
                {
                    Imagen nuevaImg = new Imagen
                    {
                        IdProducto = producto.Id_producto,
                        ImagenUrl = inpImagen.Text,
                        Activo = true
                    };
                    imagenesModificadas.Add(nuevaImg);
                }
                negocio.Modificar(producto);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Producto modificado exitosamente!');", true);
                Response.Redirect("default.aspx");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error al modificar el producto: " + ex.Message + "');", true);
            }
        }
    }
}