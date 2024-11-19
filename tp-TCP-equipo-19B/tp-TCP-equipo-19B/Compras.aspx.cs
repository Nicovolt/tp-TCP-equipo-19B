using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tp_TCP_equipo_19B
{
    public partial class Formulario_web1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnEntregaContinuar_Click(object sender, EventArgs e)
        {

            // Inicializamos los Label de error como invisibles antes de comenzar la validación
            lblErrorNombre.Visible = false;
            lblErrorCodigoPostal.Visible = false;
            lblErrorTelefono.Visible = false;
            lblErrorApellido.Visible = false;
            lblErrorMail.Visible = false;


            bool isValid = true; 
            
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                lblErrorApellido.Visible = true;
                isValid = false;
            }

            if (string.IsNullOrEmpty(txtMail.Text))
            {
                lblErrorMail.Visible = true;
                isValid = false;

            }
            
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                lblErrorNombre.Visible = true;
                isValid = false;
            }

           
            if (string.IsNullOrEmpty(txtCodigoPostal.Text))
            {
                lblErrorCodigoPostal.Visible = true;
                isValid = false;
            }

            
            if (string.IsNullOrEmpty(txtTelefono.Text))
            {
                lblErrorTelefono.Visible = true;
                isValid = false;
            }
            else if (!Regex.IsMatch(txtTelefono.Text, @"^\d+$")) 
            {
                lblErrorTelefono.Visible = true;
                lblErrorTelefono.Text = "El teléfono debe contener solo números.";
                isValid = false;
            }

            /*
             Aca va toda la logica de negocio de presupuesto. 
             */


            if (isValid)
            {
                negocio.ServicioEmail servicioEmail = new negocio.ServicioEmail();

                string nombre = txtNombre.Text;
                string apellido = txtApellido.Text;
                string mailUsuario = txtMail.Text;
                string telefono = txtTelefono.Text;

                servicioEmail.ConfirmarCompra(mailUsuario, nombre, apellido, telefono);

                Response.Redirect("Default.aspx");
            }
        }

    }
}