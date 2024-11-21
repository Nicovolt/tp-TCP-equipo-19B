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
    public partial class Formulario_web16 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var presupuestos = new PresupuestoNegocio().ObtenerPresupuestosConDetalles();

                RepeaterPresupuestos.DataSource = presupuestos;
                RepeaterPresupuestos.DataBind();
            }
        }

        
    }
}