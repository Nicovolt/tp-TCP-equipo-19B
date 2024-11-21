using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tp_TCP_equipo_19B
{
    public partial class Formulario_web15 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BannerNegocio bannerNegocio = new BannerNegocio();
            bannerNegocio.listar();
        }

        protected void btnAgregarImagen_Click(object sender, EventArgs e)
        {
            BannerNegocio bannerNegocio = new BannerNegocio();
            dynamic usuario = Session["usuario"];
            int id = usuario.IdUsuario;
            bannerNegocio.Agregar(txtNuevaImagen.Text, id);
        }
    }
}