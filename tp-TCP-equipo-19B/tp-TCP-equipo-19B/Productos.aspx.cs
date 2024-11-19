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
    public partial class GestionProducto : System.Web.UI.Page
    {
        private List<string> ImagenesTemporales
        {
            get
            {
                if (ViewState["ImagenesTemporales"] == null)
                    ViewState["ImagenesTemporales"] = new List<string>();
                return (List<string>)ViewState["ImagenesTemporales"];
            }
            set
            {
                ViewState["ImagenesTemporales"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!EsUsuarioAdmin())
                {
                    Response.Redirect("Default.aspx");
                    return;
                }
                CargarCombos();
                int idProducto = ObtenerIdProducto();

                if (idProducto != -1)
                {
                    CargarProducto(idProducto);
                    ltlTitulo.Text = "Modificar Producto";
                    btnGuardar.Text = "Actualizar";
                }
                else
                {
                    ltlTitulo.Text = "Nuevo Producto";
                    btnGuardar.Text = "Agregar";
                }
            }

            MostrarImagenes();
        }

        private void CargarCombos()
        {
            try
            {
                // Cargar Categorías
                CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
                ddlCategoria.DataSource = categoriaNegocio.ListarCategorias();
                ddlCategoria.DataTextField = "Nombre";
                ddlCategoria.DataValueField = "IdCategoria";
                ddlCategoria.DataBind();
                ddlCategoria.Items.Insert(0, new ListItem("Seleccione una categoría", ""));

                // Cargar Marcas
                MarcaNegocio marcaNegocio = new MarcaNegocio();
                ddlMarca.DataSource = marcaNegocio.ListarMarcas();
                ddlMarca.DataTextField = "Nombre";
                ddlMarca.DataValueField = "IdMarca";
                ddlMarca.DataBind();
                ddlMarca.Items.Insert(0, new ListItem("Seleccione una marca", ""));
            }
            catch (Exception ex)
            {
                MostrarError("Error al cargar los combos: " + ex.Message);
            }
        }

        private void CargarProducto(int idProducto)
        {
            try
            {
                ProductoNegocio negocio = new ProductoNegocio();
                Productos producto = negocio.buscarPorID(idProducto);

                if (producto != null)
                {
                    txtNombre.Text = producto.Nombre;
                    txtDescripcion.Text = producto.Descripcion;
                    ddlCategoria.SelectedValue = producto.Id_categoria.ToString();
                    ddlMarca.SelectedValue = producto.Id_marca.ToString();
                    txtPrecio.Text = producto.Precio.ToString("0.00");
                    txtStock.Text = producto.stock.ToString();

                    // Se cargan las imagenes al viewstate
                    ImagenesTemporales = producto.ListaImagenes.Select(img => img.ImagenUrl).ToList();
                }
            }
            catch (Exception ex)
            {
                MostrarError("Error al cargar el producto: " + ex.Message);
            }
        }

        protected void btnAgregarImagen_Click(object sender, EventArgs e)
        {
            string nuevaUrl = txtNuevaImagen.Text.Trim();
            if (!string.IsNullOrEmpty(nuevaUrl))
            {
                ImagenesTemporales.Add(nuevaUrl);
                txtNuevaImagen.Text = string.Empty;
                MostrarImagenes();
            }
        }

        private void MostrarImagenes()
        {
            pnlImagenes.Controls.Clear();

            foreach (string url in ImagenesTemporales)
            {
                Panel imageItem = new Panel();
                imageItem.CssClass = "image-item";

                TextBox txtUrl = new TextBox();
                txtUrl.CssClass = "form-control";
                txtUrl.Text = url;
                txtUrl.ReadOnly = true;

                Button btnEliminar = new Button();
                btnEliminar.CssClass = "btn btn-danger btn-remove";
                btnEliminar.Text = "X";
                btnEliminar.CommandArgument = url;
                btnEliminar.Click += BtnEliminarImagen_Click;
                btnEliminar.CausesValidation = false;

                imageItem.Controls.Add(txtUrl);
                imageItem.Controls.Add(btnEliminar);
                pnlImagenes.Controls.Add(imageItem);
            }
        }

        protected void BtnEliminarImagen_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string urlAEliminar = btn.CommandArgument;
            ImagenesTemporales.Remove(urlAEliminar);
            MostrarImagenes();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            try
            {
                Productos producto = new Productos
                {
                    Nombre = txtNombre.Text,
                    Descripcion = txtDescripcion.Text,
                    Id_categoria = int.Parse(ddlCategoria.SelectedValue),
                    Id_marca = int.Parse(ddlMarca.SelectedValue),
                    Precio = decimal.Parse(txtPrecio.Text),
                    stock = int.Parse(txtStock.Text),
                    PorcentajeDescuento = 0
                };

                // Convertir las URLs en objetos Imagen
                producto.ListaImagenes = ImagenesTemporales
                    .Select(url => new Imagen { ImagenUrl = url })
                    .ToList();

                ProductoNegocio negocio = new ProductoNegocio();
                int idProducto = ObtenerIdProducto();

                if (idProducto != -1)
                {
                    producto.Id_producto = idProducto;
                    negocio.Modificar(producto);
                    MostrarExito("Producto modificado exitosamente");
                }
                else
                {
                    negocio.Agregar(producto);
                    MostrarExito("Producto agregado exitosamente");
                }

                Response.Redirect("Default.aspx");
            }
            catch (Exception ex)
            {
                MostrarError("Error al guardar el producto: " + ex.Message);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        private int ObtenerIdProducto()
        {
            if (Request.QueryString["id"] != null)
            {
                return int.TryParse(Request.QueryString["id"], out int id) ? id : -1;
            }
            return -1;
        }

        private void MostrarError(string mensaje)
        {
            mensaje = mensaje.Replace("'", "\\'");

            ScriptManager.RegisterStartupScript(this, GetType(), "error",
                $"Swal.fire({{" +
                $"  icon: 'error'," +
                $"  title: 'Error'," +
                $"  text: '{mensaje}'," +
                $"  confirmButtonColor: '#3085d6'" +
                $"}});", true);
        }

        private void MostrarExito(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "success",
               $"Swal.fire({{" +
               $"  icon: 'success'," +
               $"  title: '¡Éxito!'," +
               $"  text: '{mensaje}'," +
               $"  confirmButtonColor: '#3085d6'" +
               $"}}).then((result) => {{" +
               $"  if (result.isConfirmed) {{" +
               $"    window.location = 'Default.aspx';" +
               $"  }}" +
               $"}});", true);
        }

        private bool EsUsuarioAdmin()
        {
            if (Session["usuario"] == null) return false;
            dynamic usuario = Session["usuario"];
            if (usuario.EsAdmin)
            {
                return true;
            }
            return false;
        }
    }
}